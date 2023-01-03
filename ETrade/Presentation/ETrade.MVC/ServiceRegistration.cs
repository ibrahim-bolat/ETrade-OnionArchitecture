using System.Net;
using System.Text.Json.Serialization;
using ETrade.Application.Model;
using ETrade.MVC.Configurations.RateLimit;
using ETrade.MVC.Configurations.SeriLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using NpgsqlTypes;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;


namespace ETrade.MVC;

public static class ServiceRegistration
{
    public static void AddPresentationServices(this IServiceCollection serviceCollection,IConfiguration configuration,IHostBuilder host)
    {
        //hot reload
        serviceCollection.AddControllersWithViews().AddRazorRuntimeCompilation();
        
        //for Ignore Cycles
        serviceCollection.AddControllers().AddJsonOptions(options => 
        { 
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;//ilk karakter büyük olsun
        });
        
        //cookie configuration
        serviceCollection.ConfigureApplicationCookie(cookieOptions =>
        {
            cookieOptions.LoginPath = new PathString("/Admin/Account/Login");
            cookieOptions.LogoutPath = new PathString("/Admin/Account/Logout");
            cookieOptions.Cookie = new CookieBuilder
            {
                Name = "ETradeCookie", 
                HttpOnly = false, 
                SameSite = SameSiteMode.Lax, 
                SecurePolicy = CookieSecurePolicy.Always 
            };
            cookieOptions.SlidingExpiration = true; 
            cookieOptions.ExpireTimeSpan = TimeSpan.FromDays(30);
            cookieOptions.AccessDeniedPath = new PathString($"/Error/Index?statusCode={401}");
        });
        
        //facebook login authenticate
        serviceCollection.AddAuthentication().AddFacebook(faceOptions =>
        {
            faceOptions.AppId = configuration["FacebookAppId"];
            faceOptions.AppSecret = configuration["FacebookAppSecret"];
            faceOptions.AccessDeniedPath = new PathString("/Admin/Account/Login");
            faceOptions.ReturnUrlParameter = "";
        })
        .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = configuration["GoogleClientId"];
            googleOptions.ClientSecret = configuration["GoogleClientSecret"];
            googleOptions.AccessDeniedPath = new PathString("/Admin/Account/Login");
            googleOptions.ReturnUrlParameter = "";
        });;
        
        //all project authorize
        serviceCollection.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        }); 
        
        //for fix token error
        serviceCollection.Configure<RouteOptions>(options =>
        {
            options.LowercaseQueryStrings = true; 
        });
        
        //seri log configuration
        Logger log = new LoggerConfiguration()
            //.WriteTo.Console()
            //.WriteTo.File("logs/log.txt")
            .WriteTo.PostgreSQL(configuration.GetConnectionString("DefaultConnection"), "Logs",
                needAutoCreateTable: true,
                columnOptions: new Dictionary<string, ColumnWriterBase>
                {
                    {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
                    {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
                    {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)},
                    {"time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
                    {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
                    {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)},
                    {"userId", new UserIdColumnWriter(NpgsqlDbType.Integer)}
                })
            //.WriteTo.Seq(configuration["Seq:ServerURL"])
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .CreateLogger();
        
        //use above seri log configuration
        host.UseSerilog(log);

        //include http logs 
        serviceCollection.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.RequestHeaders.Add("sec-ch-ua");
            logging.MediaTypeOptions.AddText("application/javascript");
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
        });
        
        //configure RateLimitSettings
        serviceCollection.Configure<RateLimitSettings>(configuration.GetSection("RateLimitSettings"));
        serviceCollection.AddRateLimiter(options =>
        {
            options.AddPolicy<IPAddress, CustomRateLimitPolicy>("CustomRateLimitPolicy");
        });
    }
}