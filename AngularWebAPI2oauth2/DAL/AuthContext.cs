using System.Data.Entity;
using AngularWebAPI2oauth2.Models.Auth;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularWebAPI2oauth2.DAL
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext() : base("AuthContext") { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}