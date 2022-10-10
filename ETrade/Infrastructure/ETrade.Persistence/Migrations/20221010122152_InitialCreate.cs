using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ETrade.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdNo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AdTitle = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    AdDate = table.Column<DateTime>(type: "date", nullable: false),
                    AdVehicleStatus = table.Column<string>(type: "text", nullable: false),
                    AdFromWho = table.Column<string>(type: "text", nullable: false),
                    AdSwapStatus = table.Column<string>(type: "text", nullable: false),
                    DamageStatus = table.Column<string>(type: "text", nullable: false),
                    AdVehiclePrice = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    AdDetail = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    GenderType = table.Column<string>(type: "text", nullable: false),
                    UserIdendityNo = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    AddressTitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NeighborhoodOrVillage = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    District = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    City = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    AddressDetails = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleAddresses_Ads_Id",
                        column: x => x.Id,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageTitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ImagePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ImageAltText = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    AdId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleImages_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    AddressTitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AddressType = table.Column<string>(type: "text", nullable: false),
                    NeighborhoodOrVillage = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    District = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    City = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    AddressDetails = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DefaultAddress = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageTitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ImagePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ImageAltText = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Profil = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    MainCategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_MainCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "MainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    SubCategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    EngineType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EngineCapacity = table.Column<string>(type: "text", nullable: false),
                    EnginePower = table.Column<string>(type: "text", nullable: false),
                    EquipmentVariant = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ModelYear = table.Column<int>(type: "integer", fixedLength: true, maxLength: 4, nullable: false),
                    FuelType = table.Column<string>(type: "text", nullable: false),
                    GearType = table.Column<string>(type: "text", nullable: false),
                    Kilometer = table.Column<int>(type: "integer", nullable: false),
                    BodyType = table.Column<string>(type: "text", nullable: false),
                    TractionType = table.Column<string>(type: "text", nullable: false),
                    ModelColour = table.Column<string>(type: "text", nullable: false),
                    GuaranteeStatus = table.Column<string>(type: "text", nullable: false),
                    PlateNationality = table.Column<string>(type: "text", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Ads_Id",
                        column: x => x.Id,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Models_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ads",
                columns: new[] { "Id", "AdDate", "AdDetail", "AdFromWho", "AdNo", "AdSwapStatus", "AdTitle", "AdVehiclePrice", "AdVehicleStatus", "CreatedByName", "CreatedTime", "DamageStatus", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedTime", "Note" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(3170), "ÇOK GÜZEL ARABA", "ByOwner", "123456789", "Yes", "Sahibinden Tertemiz Araba", 350000.50m, "FirstHand", "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(3152), "Unspecified", false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(3159), null },
                    { 2, new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(3178), "ÇOK GÜZEL ARABA", "ByOwner", "234567891", "No", "Sahibinden Boyasız Araba", 150000.7840m, "SecondHand", "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(3175), "HeavilyDamaged", false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(3176), null },
                    { 3, new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(3184), "ÇOK GÜZEL ARABA", "FromAuthorizedDealer", "345678912", "Yes", "İTHAL ARAÇ", 1000000.50m, "ImportedFirstHand", "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(3181), "Unspecified", false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(3182), null },
                    { 4, new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(3189), "ÇOK GÜZEL ARABA", "FromTheGalery", "456789123", "No", "Galeriden Temiz Araç", 2500000.80m, "FirstHand", "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(3186), "WithoutDamageRegistration", false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(3187), null }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedByName", "CreatedTime", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedTime", "Name", "NormalizedName", "Note" },
                values: new object[] { 1, "57fd2e93-45ec-4bfd-8e6f-644618c16d10", "Owner", new DateTime(2022, 10, 10, 15, 21, 51, 476, DateTimeKind.Local).AddTicks(5342), true, false, "Owner", new DateTime(2022, 10, 10, 15, 21, 51, 476, DateTimeKind.Local).AddTicks(5368), "Admin", "ADMİN", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedByName", "CreatedTime", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "GenderType", "IsActive", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedByName", "ModifiedTime", "NormalizedEmail", "NormalizedUserName", "Note", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserIdendityNo", "UserName" },
                values: new object[] { 1, 0, "74269e6f-4f4b-4f78-b65e-3728995812e6", "Owner", new DateTime(2022, 10, 10, 15, 21, 51, 477, DateTimeKind.Local).AddTicks(746), null, "bolat6606@hotmail.com", true, "İbrahim", "Unspecified", true, false, "Bolat", false, null, "Owner", new DateTime(2022, 10, 10, 15, 21, 51, 477, DateTimeKind.Local).AddTicks(753), "BOLAT6606@HOTMAIL.COM", "BOLAT6606", null, "AQAAAAEAACcQAAAAEIGPdKp+0fHBtnChtFg5CVkoY0mV92uCMHsizUl6jA9l0/BaA5qKS0twAAterKRdFA==", "+90(532)575-79-66", false, "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA", false, null, "bolat6606" });

            migrationBuilder.InsertData(
                table: "MainCategories",
                columns: new[] { "Id", "CreatedByName", "CreatedTime", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedTime", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(6999), false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(7004), "Araçlar", null },
                    { 2, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(7013), false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(7014), "Yedek Parçalar", null }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AddressDetails", "AddressTitle", "AddressType", "City", "CreatedByName", "CreatedTime", "DefaultAddress", "District", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "ModifiedByName", "ModifiedTime", "NeighborhoodOrVillage", "Note", "PhoneNumber", "PostalCode", "UserId" },
                values: new object[,]
                {
                    { 1, "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye", "Evim", "Home", "Ankara", "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 482, DateTimeKind.Local).AddTicks(1297), false, "Yenimahalle", "bolatcan@email.com", "ibo", false, false, "BOL", "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 482, DateTimeKind.Local).AddTicks(1304), "Naci Bekir", null, "+90(532)5757966", "06500", 1 },
                    { 2, "Mustafa Kemal Mahallesi ,Eskişehir Yolu  Kütahya Sok. No:280/7 06500 Çankaya/Ankara/Türkiye", "İş", "Work", "Ankara", "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 482, DateTimeKind.Local).AddTicks(1322), true, "Çankaya", "bolatcan@email.com", "ibo", false, false, "BOLAT", "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 482, DateTimeKind.Local).AddTicks(1323), "Mustafa Kemal", null, "+90(532)5757966", "06100", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CreatedByName", "CreatedTime", "IsActive", "IsDeleted", "MainCategoryId", "ModifiedByName", "ModifiedTime", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(8260), false, false, 1, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(8266), "Otomobil", null },
                    { 2, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(8274), false, false, 1, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(8275), "Motorsiklet", null },
                    { 3, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(8278), false, false, 1, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(8279), "Minivan & Panelvan", null },
                    { 4, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(8282), false, false, 1, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(8283), "Arazi, SUV & Pickup", null }
                });

            migrationBuilder.InsertData(
                table: "UserImages",
                columns: new[] { "Id", "CreatedByName", "CreatedTime", "ImageAltText", "ImagePath", "ImageTitle", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedTime", "Note", "Profil", "UserId" },
                values: new object[] { 1, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 482, DateTimeKind.Local).AddTicks(2290), "Profil", "/admin/images/userimages/1/profil.jpg", "ProfilResmi", true, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 482, DateTimeKind.Local).AddTicks(2295), null, true, 1 });

            migrationBuilder.InsertData(
                table: "VehicleAddresses",
                columns: new[] { "Id", "AddressDetails", "AddressTitle", "City", "CreatedByName", "CreatedTime", "District", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedTime", "NeighborhoodOrVillage", "Note", "PostalCode" },
                values: new object[,]
                {
                    { 1, "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye", "Evim", "Ankara", "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(2595), "Yenimahalle", false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(2600), "Naci Bekir", null, "06500" },
                    { 2, "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye", "Evim", "Ankara", "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(2609), "Yenimahalle", false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(2610), "Naci Bekir", null, "06500" },
                    { 3, "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye", "Evim", "Ankara", "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(2613), "Yenimahalle", false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(2614), "Naci Bekir", null, "06500" },
                    { 4, "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye", "Evim", "Ankara", "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(2616), "Yenimahalle", false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(2618), "Naci Bekir", null, "06500" }
                });

            migrationBuilder.InsertData(
                table: "VehicleImages",
                columns: new[] { "Id", "AdId", "CreatedByName", "CreatedTime", "ImageAltText", "ImagePath", "ImageTitle", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedTime", "Note" },
                values: new object[,]
                {
                    { 1, 1, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(3630), "Profil", "/admin/images/userimages/profil.png", "ProfilResmi", false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(3635), null },
                    { 2, 2, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(3643), "Profil", "/admin/images/userimages/profil.png", "ProfilResmi", false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(3644), null },
                    { 3, 3, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(3647), "Profil", "/admin/images/userimages/profil.png", "ProfilResmi", false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(3648), null },
                    { 4, 4, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(3650), "Profil", "/admin/images/userimages/profil.png", "ProfilResmi", false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(3651), null }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedByName", "CreatedTime", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedTime", "Name", "Note", "SubCategoryId" },
                values: new object[,]
                {
                    { 1, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(5520), false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(5526), "Wolkswagen", null, 1 },
                    { 2, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(5535), false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(5536), "Honda", null, 2 },
                    { 3, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(5550), false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(5551), "Fiat", null, 3 },
                    { 4, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(5553), false, false, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 484, DateTimeKind.Local).AddTicks(5555), "Nissan", null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BodyType", "BrandId", "CreatedByName", "CreatedTime", "EngineCapacity", "EnginePower", "EngineType", "EquipmentVariant", "FuelType", "GearType", "GuaranteeStatus", "IsActive", "IsDeleted", "Kilometer", "ModelColour", "ModelYear", "ModifiedByName", "ModifiedTime", "Name", "Note", "PlateNationality", "TractionType" },
                values: new object[,]
                {
                    { 1, "Hatchback5Door", 1, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(1289), "Cm13011600", "Hp101125", "1.6 TDI", "Confortline", "Diesel", "Automatic", "No", false, false, 102000, "White", 2015, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(1295), "Golf", null, "TurkeyPlate", "FrontDrive" },
                    { 2, "Hatchback5Door", 2, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(1311), "Cm13011600", "Hp101125", "1.4 TSI", "Confortline", "Diesel", "Automatic", "No", false, false, 102000, "White", 2015, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(1312), "City", null, "TurkeyPlate", "FrontDrive" },
                    { 3, "Hatchback5Door", 3, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(1318), "Cm13011600", "Hp101125", "1.6 TDI", "Confortline", "Diesel", "Automatic", "No", false, false, 102000, "White", 2015, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(1319), "Egea", null, "TurkeyPlate", "FrontDrive" },
                    { 4, "Hatchback5Door", 4, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(1388), "Cm13011600", "Hp101125", "1.6 Düz", "Confortline", "Diesel", "Automatic", "No", false, false, 102000, "White", 2015, "Admin", new DateTime(2022, 10, 10, 15, 21, 51, 485, DateTimeKind.Local).AddTicks(1389), "Qashqai", null, "TurkeyPlate", "FrontDrive" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_SubCategoryId",
                table: "Brands",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId",
                table: "Models",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_MainCategoryId",
                table: "SubCategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleImages_AdId",
                table: "VehicleImages",
                column: "AdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "VehicleAddresses");

            migrationBuilder.DropTable(
                name: "VehicleImages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Ads");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "MainCategories");
        }
    }
}
