using System;
using System.Net.Http;
using AspNetSecurity_m4.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
                b.GetClaimsFromUserInfoEndpoint = true;
                b.ClientId = "confarchweb";
                b.ClientSecret = "secret";

                b.ResponseType = "code id_token";
                b.Scope.Add("confArchApi");
                b.SaveTokens = true;
});

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ConferenceApiService>();
            services.AddTransient<ProposalApiService>();
            services.AddTransient<AttendeeApiService>();
            services.AddSingleton(x => new HttpClient {BaseAddress = new Uri("http://localhost:54438")});
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
