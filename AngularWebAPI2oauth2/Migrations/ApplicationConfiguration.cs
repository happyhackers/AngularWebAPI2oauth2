using AngularWebAPI2oauth2.DAL;
using System.Data.Entity.Migrations;

namespace AngularWebAPI2oauth2.Migrations
{


    internal sealed class ApplicationConfiguration : DbMigrationsConfiguration<ApplicationContext>
    {
        public ApplicationConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationContext context)
        {
        }
    }
}
