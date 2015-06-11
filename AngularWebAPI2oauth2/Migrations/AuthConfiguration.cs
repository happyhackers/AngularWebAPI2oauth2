using AngularWebAPI2oauth2.DAL;
using AngularWebAPI2oauth2.Models;
using AngularWebAPI2oauth2.Models.Auth;
using AngularWebAPI2oauth2.Providers;

namespace AngularWebAPI2oauth2.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class AuthConfiguration : DbMigrationsConfiguration<AuthContext>
    {
        public AuthConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AngularWebAPI2oauth2.Models.AuthContext";
        }

        protected override void Seed(AuthContext context)
        {
            context.Clients.AddOrUpdate(x => x.Id, new Client
            {
                Id = "AngularApp",
                Secret = Helper.GetHash("Angular"),
                Name = "Angular Js front end appliction",
                ApplicationType = ApplicationTypes.JavaScript,
                Active = true,
                RefreshTokenLifeTime = 7200,
                AllowedOrigin = "*"
            });

            context.Clients.AddOrUpdate(x => x.Id, new Client
            {
                Id = "DESscanner",
                Secret = Helper.GetHash("ScannerApp"),
                Name = "Mobile appliation",
                ApplicationType = ApplicationTypes.NativeConfidential,
                Active = true,
                RefreshTokenLifeTime = 7200,
                AllowedOrigin = "*"
            });

            context.SaveChanges();
        }
    }
}
