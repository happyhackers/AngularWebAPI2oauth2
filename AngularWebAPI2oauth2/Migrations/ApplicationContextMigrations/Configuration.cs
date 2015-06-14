using AngularWebAPI2oauth2.DAL;
using System.Data.Entity.Migrations;

namespace AngularWebAPI2oauth2.Migrations.ApplicationContextMigrations
{
    

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations/ApplicationContextMigrations";
        }

        protected override void Seed(ApplicationContext context)
        {
        }
    }
}
