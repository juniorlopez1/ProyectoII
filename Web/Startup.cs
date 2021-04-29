using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using Web.Data;
using Web.Services;
using Web.Servicios;

namespace Web
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
            services.AddControllersWithViews();

            var baseURL = @"https://localhost:6001/api";


            services.AddScoped<IClienteService>(p => new ClienteService(baseURL));
            services.AddScoped<IRestablecimientoService>(p => new RestablecimientoService(baseURL, p.GetService<IEmailService>()));


            #region Ocultar contrasena

            services.AddScoped<IEmailService>((p) => new GmailEmailService("smtp.gmail.com", "andresaguilar1125@gmail.com", "Andresl11."));

            #endregion

            /* mongodb --------------------------------------------------------------- */
            services.Configure<MongodbSettings>(
                Configuration.GetSection(nameof(MongodbSettings)));
            services.AddSingleton<IMongodbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongodbSettings>>().Value);
            services.AddScoped<IBitacoraWebService, BitacoraWebService>();


            //Agregando mas servicios
            services.AddScoped<ITarjetaService>(p => new TarjetaService(baseURL));
            services.AddScoped<ITransaccionService>(p => new TransaccionService(baseURL));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                    {
                        options.SlidingExpiration = true;
                        options.ExpireTimeSpan = new TimeSpan(0, 1, 0);
                        options.LoginPath = "/cliente/login";
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(); //!
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Cliente}/{action=Login}/{id?}");
            });
        }
    }
}
