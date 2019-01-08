using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API_BoilerPlate.API.Helper;
using API_BoilerPlate.BRL.Common;
using API_BoilerPlate.BRL.Interfaces;
using API_BoilerPlate.DAL.DapperRepositories;
using API_BoilerPlate.DAL.Interfaces;
using API_BoilerPlate.DAL.DapperRepositories;
using API_BoilerPlate.DAL.Entities;
using API_BoilerPlate.DAL.Repository;
using API_BoilerPlate.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace API_BoilerPlate.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser() //this will lock down all endpoints unless otherwise stated on each  eg allowanonymous
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyOrigin();
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                    {
                        Version = "v1.0",
                        Title = "My API",
                        Description = "My API Description",
                        TermsOfService = "",
                        Contact = new Contact
                        {
                            Name = "Andrew Mitchell",
                            Email = "yes@no.com",
                            Url = "yes-no.com"
                        }
                    }
                );
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "My.API.xml"));
            });

            services.AddSingleton<HttpClient>();

            services.AddDbContext<TestContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection1")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IShoesService, ShoesService>();
             services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IHttpService, HttpService>();

            //  services.AddTransient<IShoesRepository>(f => new ShoesRepository(Configuration.GetConnectionString("MyConnection1")));
            //  services.AddTransient<IOrdersRepository>(f => new OrdersRepository(Configuration.GetConnectionString("MyConnection1"))) ;

            //Automapper profile
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret); //used to validate the token
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    //Used to pull token from querystring (for signalR)
                    //x.Events = new JwtBearerEvents
                    //{
                    //    OnMessageReceived = context =>
                    //    {
                    //        var accessToken = context.Request.Query["access_token"];

                    //        // If the request is for our hub...
                    //        var path = context.HttpContext.Request.Path;
                    //        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/notify") || path.StartsWithSegments("/broadcast")))
                    //        {
                    //            // Read the token out of the query string
                    //            context.Token = accessToken;
                    //        }
                    //        return Task.CompletedTask;
                    //    }
                    //};
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("BasicUser",
                    policy =>
                    {
                        policy.RequireRole("BASIC_USER", "ADMIN_USER");
                    });

                options.AddPolicy("AdminUser",
                   policy =>
                   {

                       policy.RequireRole("ADMIN_USER");
                   });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var swaggerEndpoint = "/services/swagger/v1/swagger.json";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                swaggerEndpoint = "/MyAPI/swagger/v1/swagger.json";
            }

            app.UseAuthentication();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerEndpoint, "My API v1.0");
            });

            //logging
            loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Warning);

            app.UseCors("CorsPolicy");

        //    app.UseSignalR(routes =>
        //    {
                // routes.MapHub<NotifyHub>("/notify");
         //   });

            app.UseMvc();
        }
    }
}
