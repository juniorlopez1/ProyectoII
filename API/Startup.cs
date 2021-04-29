
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Acceso;
using Negocio;
using Microsoft.OpenApi.Models;
using Datos;
using Microsoft.Extensions.Options;
using Negocios;

namespace API
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

            /* CORS ----------------------------------------------------------------------------------------- */
            services.AddCors(options =>
            {
                options.AddPolicy(name: "DefaultCorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });


            /* Entity Framework Context  --------------------------------------------------------------------- */
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            /* Servicios (Negocio) -------------------------------------------------------------------------- */
            services.AddScoped(p => new ClienteService(p.GetService<Context>()));
            services.AddScoped(p => new TarjetaService(p.GetService<Context>()));
            services.AddScoped(p => new TransaccionService(p.GetService<Context>()));
            services.AddScoped(p => new RestablecimientoService(p.GetService<Context>()));


            /* mongodb --------------------------------------------------------------- */

            // requires using Microsoft.Extensions.Options
            services.Configure<MongodbSettings>(
                Configuration.GetSection(nameof(MongodbSettings)));

            services.AddSingleton<IMongodbSettings>(sp => sp.GetRequiredService<IOptions<MongodbSettings>>().Value);

            //requires
            services.AddScoped<IBitacoraService, BitacoraService>();



            /* JSON ----------------------------------------------------------------------------------------- */
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            /* NETCORE ----------------------------------------------------------------------------------------- */
            services.AddControllers();


            /* SWAGGER ----------------------------------------------------------------------------------------- */
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            /* CORS ----------------------------------------------------------------------------------------- */
            app.UseCors("DefaultCorsPolicy");

            app.UseAuthorization();

            /* NETCORE routes-------------------------------------------------------------------------------- */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
