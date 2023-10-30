using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Rectangle.API.Middleware;
using Rectangle.DomainServices; 
using Rectangle.Persistence;
using Serilog;

namespace Rectangle.API
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string allowSpecificOrigins = "allowedOrigins";

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(this.Configuration.GetSection("AzureAdB2C"));
              
            services.AddPersistenceServices(this.Configuration);

            // use temporarily only for users
            services.AddMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: this.allowSpecificOrigins,
                    builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });

            services.AddHttpContextAccessor();
            services.AddControllers(); 
            services.AddApplicationInsightsTelemetry();
            services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 268435456; });
 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseRouting();
            app.UseCors(this.allowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL()
                    .WithOptions(new HotChocolate.AspNetCore.GraphQLServerOptions()
                    {
                        EnableBatching = true,
                        Tool =
                        {
                            Enable = true
                        }
                    }).RequireAuthorization();
                endpoints.MapGraphQL("/public", schemaName: "publicSchema")
                    .WithOptions(new HotChocolate.AspNetCore.GraphQLServerOptions()
                    {
                        EnableBatching = true,
                        Tool =
                        {
                            Enable = true
                        }
                    });
            });
            app.UseSerilogRequestLogging();

            Log.Information("Application configured");
        }
    }
}