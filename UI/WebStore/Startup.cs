using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebStore.Clients.Employees;
using WebStore.Clients.Identity;
using WebStore.Clients.Orders;
using WebStore.Clients.Products;
using WebStore.Clients.Values;
using WebStore.Domain.Identity;
using WebStore.Infrastructure.Middleware;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.TestApi;
using WebStore.Services.Products.InCookies;

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;
        public Startup(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<WebStoreDB>(opt =>
            //    opt.UseSqlServer(_Configuration.GetConnectionString("Default")));
            //services.AddTransient<WebStoreDbInitializer>();

            services.AddIdentity<User, Role>()
                .AddIdentityWebStoreWebAPIClients()
                .AddDefaultTokenProviders();

#if DEBUG
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
#endif
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.AddMvc(opt =>
                {
                    opt.Conventions.Add(new WebStoreControllerConvention());
                })
                .AddRazorRuntimeCompilation();

            services.AddSingleton<IEmployeesData, EmployeesClient>();
            services
                .AddScoped<IProductData, ProductsClient>()
                .AddScoped<ICartServices, InCookiesCartService>()
                .AddScoped<IOrderService, OrdersClient>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore.GB";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Login/SignInUser";
                opt.LogoutPath = "/Login/SignOutUser";
                opt.AccessDeniedPath = "/Login/AccesDenied";

                opt.SlidingExpiration = true;
            });

            //services.AddSingleton<IEmployeesData, DbInMemory>();
            services.AddTransient<IEmployeesData, EmployeesClient>();
            services
                //.AddTransient<IProductData, SqlProductData>()
                .AddTransient<IProductData, ProductsClient>()
                .AddScoped<ICartServices, InCookiesCartService>()
                //.AddScoped<IOrderService, SqlOrderService>();
                .AddScoped<IOrderService, OrdersClient>();
            services.AddScoped<IValuesService, ValuesClient>();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, WebStoreDbInitializer db*/)
        {
            //db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWelcomePage("/Welcom");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async ctx => await ctx.Response.WriteAsync(_Configuration["greetings"]));

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=index}/{id?}");
            });
        }
    }
}
