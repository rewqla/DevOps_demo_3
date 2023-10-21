using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OilShop.Entities;
using OilShop.Models;
using OilShop.Repo.Implement;
using OilShop.Repo.Interfaces;
using OilShop.Services.Implement;
using OilShop.Services.Interfaces;

namespace OilShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(opt =>
                    opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<DbUser, DbRole>(options =>
            {
                options.Stores.MaxLengthForKeys = 128;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddMvc(setup =>
            {
            }).AddFluentValidation();

            services.AddTransient<IOilRepo, OilRepo>();
            services.AddTransient<IOilCapacityRepo, OilCapacityRepo>();
            services.AddTransient<IOilTypeRepo, OilTypeRepo>();
            services.AddTransient<ICartRepo, CartRepo>();
            services.AddTransient<IOilManafacturerRepo, OilManafacturerRepo>();
            services.AddTransient<IOrderRepo, OrderRepo>();
            services.AddTransient<IOilToleranceRepo, OilToleranceRepo>();
            services.AddTransient<IOilSpecificationRepo, OilSpecificationRepo>();
            services.AddTransient<IOilRecommendationRepo, OilRecommendationRepo>();
            services.AddTransient<IOrderDetailRepo, OrderDetailRepo>();
            services.AddTransient<IOilApplyingRepo, OilApplyinRepo>();
            services.AddTransient<ICartItemRepo, CartItemRepo>();
            services.AddTransient<IOrderStatusRepo, OrderStatusRepo>();

            services.AddScoped<IOilService, OilService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddTransient<IValidator<RegistrationViewModel>, RegistrationValidation>();
            services.AddTransient<IValidator<LoginViewModel>, LoginValidation>();
            services.AddTransient<IValidator<OrderViewModel>, CheckoutValidation>();

            services.AddControllersWithViews();

            services.AddDistributedMemoryCache();
            services.AddSession();
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
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                           name: "MyArea",
                         pattern: "{area:exists}/{controller=Goods}/{action=Index}/{id?}");

            });

            SeederDB.SeedData(app.ApplicationServices, env, this.Configuration);
        }
    }
}
