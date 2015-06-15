using System.Data.Entity.Migrations;
using System.Linq;
using AngularWebAPI2oauth2.DAL;
using AngularWebAPI2oauth2.Models.Auth;
using AngularWebAPI2oauth2.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularWebAPI2oauth2.Migrations.AuthContextMigrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AuthContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\AuthContextMigrations";
        }

        protected override void Seed(AuthContext context)
        {
            context.Clients.AddOrUpdate(x => x.Id, new Client
            {
                Id = "AngularApp",
                Secret = AuthHelper.GetHash("Angular"),
                Name = "Angular Js front end appliction",
                ApplicationType = ApplicationTypes.JavaScript,
                Active = true,
                RefreshTokenLifeTime = 7200,
                AllowedOrigin = "*" //TODO: set the actual allowed origin
            });

            context.Clients.AddOrUpdate(x => x.Id, new Client
            {
                Id = "DesktopApp",
                Secret = AuthHelper.GetHash("Desktopp"),
                Name = "Test",
                ApplicationType = ApplicationTypes.NativeConfidential,
                Active = true,
                RefreshTokenLifeTime = 7200,
                AllowedOrigin = "*"
            });

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            
            /* ..:: [ ROLES ] ::.. */
            var admin = new IdentityRole("Admin");
            if (!roleManager.RoleExists(admin.Name))
                roleManager.Create(admin);

            var owner = new IdentityRole("Owner");
            if (!roleManager.RoleExists(owner.Name))
                roleManager.Create(owner);

            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
            if (userManager.Users.Any(u => u.UserName == "kalle"))
                return;

            /* ..:: [ CREATE USERS ] ::.. */
            var kalle = new IdentityUser
            {
                UserName = "kalle",
            };
            var peter = new IdentityUser
            {
                UserName = "peter",
            };
            var alban = new IdentityUser
            {
                UserName = "alban",
            };

            /* ..:: [ CONFIG USERS ] ::.. */
            userManager.Create(kalle, "kalle123");
            userManager.Create(peter, "peter123");
            userManager.Create(alban, "alban123");
            userManager.AddToRole(kalle.Id, owner.Name);
            userManager.AddToRole(kalle.Id, admin.Name);
            userManager.AddToRole(peter.Id, admin.Name);

            context.SaveChanges();
        }
    }
}
