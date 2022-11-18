using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace ETrade.MVC.Configurations.SeriLog;

public class UserIdColumnWriter : ColumnWriterBase
{
    public UserIdColumnWriter(NpgsqlDbType dbType, int? columnLength = null) : base(dbType, columnLength)
    {
        
    }

    public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
    {
        var properties = logEvent.Properties;
        LogEventPropertyValue value;
        if (properties.TryGetValue("userId", out value) && value is ScalarValue sv && sv.Value is string userId)
        {
            return Convert.ToInt32(userId);
        }
        return null;
    }
}