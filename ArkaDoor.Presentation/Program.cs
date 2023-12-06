using ArkaDoor.Application.Common.IUnitOfWork;
using ArkaDoor.Application.Services.Implementations;
using ArkaDoor.Application.Services.Interfaces;
using ArkaDoor.Domain.IRepositories.Users;
using ArkaDoor.Infrastructure.Persistence.ApplicationDbContext;
using ArkaDoor.Infrastructure.Persistence.Repositories.Users;
using ArkaDoor.Infrastructure.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ArkaDoor.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        #region Services

        var builder = WebApplication.CreateBuilder(args);

        #region MVC

        builder.Services.AddControllersWithViews();

        #endregion

        #region Add DBContext

        builder.Services.AddDbContext<AkaDoorDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("AkaDoorDbContextConnection"));
        });

        #endregion

        #region Services

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IUserQueryRepository, UsersQueryRepository>();
        builder.Services.AddScoped<IUsersCommandRepository, UsersCommandRepository>();
        builder.Services.AddScoped<IUserService, UserService>();

        #endregion

        #region Authentication

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
            // Add Cookie settings
            .AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });

        #endregion

        #endregion

        #region Midale Wares

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "area",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

        #endregion
    }
}