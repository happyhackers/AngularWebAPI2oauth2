using System.Data.Entity;
using AngularWebAPI2oauth2.Models.Auth;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularWebAPI2oauth2.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        /// <remarks/>
        public AuthContext() : base("AuthContext") { }
        /// <remarks/>
        public DbSet<Client> Clients { get; set; }
        /// <remarks/>
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}