using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AngularWebAPI2oauth2.Models;

namespace AngularWebAPI2oauth2.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("name=ApplicationContext") { }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = HttpContext.Current != null && HttpContext.Current.User != null
                ? HttpContext.Current.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).Created = DateTime.Now;
                    ((BaseEntity)entity.Entity).Creator = currentUsername;
                }

                ((BaseEntity)entity.Entity).Modified = DateTime.Now;
                ((BaseEntity)entity.Entity).Modifier = currentUsername;
            }

            return base.SaveChanges();
        }
    }
}