using AspNetCoreRateLimit;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using Hangfire;
using Hangfire.AspNetCore;
using Hangfire.SqlServer;
using LoggerService;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Service;
using Service.Contracts;
using System.Text;
using Quartz;

namespace SmartAttendance.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        public static void ConfigureEmailService(this IServiceCollection services) =>
            services.AddTransient<IEmailService, EmailService>();

        public static void ConfigureResponseCaching(this IServiceCollection services) =>
            services.AddResponseCaching();

        public static void ConfigueHttpCacheHeaders(this IServiceCollection services) =>
            services.AddHttpCacheHeaders(
                (expirationOpt) =>
                {
                    expirationOpt.MaxAge = 65;
                    expirationOpt.CacheLocation = CacheLocation.Public;
                },
                (validationOpt) =>
                {
                    validationOpt.MustRevalidate = true;
                });

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 100,
                    Period = "5m"
                }
            };

            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules;
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSqlServer<RepositoryContext>((configuration.GetConnectionString("sqlConnection")));

        public static void AddHangfireConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(configuration.GetConnectionString("sqlConnection"))
                .UseDashboardMetric(SqlServerStorage.ActiveConnections);
            });

            services.AddHangfireServer();
        }

        public static void ConfigureQuartz(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
               var jobKey = new JobKey("AutomatedAttendanceJob");

                q.AddJob<AutomatedAttendanceJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("AutomatedAttendanceTrigger")
                .WithCronSchedule("0 */5 7-17 ? * MON-FRI", x => x
                    .WithMisfireHandlingInstructionDoNothing()
                    .InTimeZone(TimeZoneInfo.Local))
            );
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureTokenProtection(this IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(10);
            });
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = new JwtConfiguration();
            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

            var key = Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtConfiguration.ValidIssuer,
                        ValidAudience = jwtConfiguration.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Smart Attendance System API", 
                    Version = "v1",
                    Description = "Smart Attendance System for Institution",
                    TermsOfService = new Uri("https://example/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ibrahim Michael",
                        Email = "segunmichael24@gmail.com",
                        Url = new Uri("https://simportfolio.netlify.app/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Simcodes API",
                        Url = new Uri("https://example.com/license")
                    }
                });

                var xmlFile = $"{typeof(Presentation.AssemblyReference).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer"
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
