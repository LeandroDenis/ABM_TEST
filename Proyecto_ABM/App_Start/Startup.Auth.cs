using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Proyecto_ABM.WebSite.App_Start.Startup))]
namespace Proyecto_ABM.WebSite.App_Start
{
    public static class MyAuthentication
    {
        public const String ApplicationCookie = "ProyectoABMAuthenticationType";
    }

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // need to add UserManager into owin, because this is used in cookie invalidation
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = MyAuthentication.ApplicationCookie,
                LoginPath = new PathString("/Login/Index"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = "CookieABM",
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(30), // adjust to your needs
            });
        }
    }
}