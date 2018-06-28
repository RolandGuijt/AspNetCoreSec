using System;
using System.Net.Http;
using AspNetSecurity_m4.Api;
using AspNetSecurity_m4.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityModel;

namespace AspNetSecurity_m4
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", b =>
            {
                b.SignInScheme = "Cookies";
                b.Authority = "http://localhost:5000";
                b.RequireHttpsMetadata = false;

                b.ClientId = "confarchweb";
                b.ClientSecret = "secret";

                b.ResponseType = "code id_token";
                b.Scope.Add("confArchApi");
                b.Scope.Add("roles");
                b.Scope.Add("experience");
                b.SaveTokens = true;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("OrganizerAccessPolicy", policy => policy.RequireRole("organizer"));

                options.AddPolicy("SpeakerAccessPolicy",
                    policy => policy.RequireAssertion(context => context.User.IsInRole("speaker")));

                options.AddPolicy("YearsOfExperiencePolicy",
                    policy => policy.AddRequirements(new YearsOfExperienceRequirement(6)));

                options.AddPolicy("ProposalEditPolicy",
                    policy => policy.AddRequirements(new ProposalRequirement(false)));

            });

            services.AddSingleton<IAuthorizationHandler, YearsOfExperienceAuthorizationHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ConferenceApiService>();
            services.AddTransient<ProposalApiService>();
            services.AddTransient<AttendeeApiService>();
            services.AddSingleton(x => new HttpClient { BaseAddress = new Uri("http://localhost:54438") });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, HttpClient httpClient)
        {
            loggerFactory.AddConsole();

            app.UseAuthentication();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Conference}/{action=Index}/{id?}");
            });
        }
    }
}
