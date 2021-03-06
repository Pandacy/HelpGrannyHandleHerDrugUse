using MaakJeNietDrugDAL.ClassesDB;
using MaakJeNietDrugLogic.Handlers.MedicineHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MaakJeNietDrugLogic.Handlers.IntakeMomentHandlers;
using Microsoft.EntityFrameworkCore;
using System;

namespace MaakJeNietDrug
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
            services.AddControllers();
            // todo: uncomment to use MySQL
            //services.AddDbContextPool<DataBaseContext>(
            //    options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")
            //));
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddDbContextPool<DataBaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("dev")));

            DataBaseSeeder.SeedMedicine();


            services.AddScoped<IGetMedicinesHandler, GetMedicinesHandler>();
            services.AddScoped<IAddMedicineHandler, AddMedicineHandler>();
            services.AddScoped<IDeleteMedicineHandler, DeleteMedicineHandler>();
            services.AddScoped<IPutMedicineHandler, PutMedicineHandler>();

            services.AddScoped<IGetIntakeMomentHandler, GetIntakeMomentHandler>();
            services.AddScoped<IAddIntakeMomentHandler, AddIntakeMomentHandler>();
            services.AddScoped<IDeleteIntakeMomentHandler, DeleteIntakeMomentHandler>();
            services.AddScoped<IPutIntakeMomentHandler, PutIntakeMomentHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });           
        }
    }
}
