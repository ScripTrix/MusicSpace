using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicSpace.Domain;
using MusicSpace.Domain.Entities;
using MusicSpace.Domain.Repositories.Abstract;
using MusicSpace.Domain.Repositories.EntityFramework;
using MusicSpace.Service;

namespace MusicSpace
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //подключаем конфиг из appsettings.json
            Configuration.Bind("Project", new Config());

            //подключаем нужный функционал приложения в качестве сервисов
            services.AddTransient<IAlbumPerformersRepository, EFAlbumPerformersRepository>();
            services.AddTransient<IAlbumsRepository, EFAlbumsRepository>();
            services.AddTransient<IApplicationUsersRepository, EFApplicationUsersRepository>();
            services.AddTransient<IChartSongsRepository, EFChartSongsRepository>();
            services.AddTransient<IPerformersRepository, EFPerformersRepository>();
            services.AddTransient<ISongPerformersRepository, EFSongPerformersRepository>();
            services.AddTransient<ISongsRepository, EFSongsRepository>();
            services.AddTransient<IUserLibraryAlbumsRepository, EFUserLibraryAlbumsRepository>();
            services.AddTransient<IUserLibrarySongsRepository, EFUserLibrarySongsRepository>();
            services.AddTransient<IWeeklyChartsRepository, EFWeeklyChartsRepository>();
            services.AddTransient<DataManager>();

            //подключаем контекст БД
            services.AddDbContext<AppDbContext>(x => x.UseNpgsql(Config.ConnectionString));

            //настраиваем identity систему
            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
                {
                    opts.User.RequireUniqueEmail = true;
                    opts.Password.RequiredLength = 6;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //настраиваем authentication cookie
            services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.Name = "musicSpaceAuth";
                    options.Cookie.HttpOnly = true;
                    options.LoginPath = "/account/login";
                    options.AccessDeniedPath = "/account/accessdenied";
                    options.SlidingExpiration = true;
                });

            //настраиваем политику авторизации для areas
            services.AddAuthorization(x =>
                {
                    x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
                    x.AddPolicy("UserArea", policy => { policy.RequireRole("user", "admin"); });
                });

            //добавляем поддержку контроллеров и представлений (MVC)
            services.AddControllersWithViews(x =>
                {
                    x.Conventions.Add(new AreaAuthorization("Admin", "AdminArea"));
                    x.Conventions.Add(new AreaAuthorization("User", "UserArea"));
                })
                //выставляем совместимость с asp.net core 3.0
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // !!! порядок регистрации Middleware очень важен
            if (env.IsDevelopment()) 
                app.UseDeveloperExceptionPage();

            // подключаем поддержку статичных файлов в приложении (css, js и т.д.)
            app.UseStaticFiles();

            //подключаем систему маршрутизации
            app.UseRouting();

            //подключаем аутентификацию и авторизацию
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            // регистрируем нужные нам маршруты (эндпоинты)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "admin",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    "user",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
