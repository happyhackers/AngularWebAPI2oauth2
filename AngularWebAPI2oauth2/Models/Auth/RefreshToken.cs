using System;
using System.ComponentModel.DataAnnotations;

namespace AngularWebAPI2oauth2.Models.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class RefreshToken
    {
        /// <remarks/>
        [Key]
        public string Id { get; set; }
        /// <remarks/>
        [Required, MaxLength(50)]
        public string Subject { get; set; }
        /// <remarks/>
        [Required, MaxLength(50)]
        public string ClientId { get; set; }
        /// <remarks/>
        public DateTime IssuedUtc { get; set; }
        /// <remarks/>
        public DateTime ExpiresUtc { get; set; }
        /// <remarks/>
        [Required]
        public string ProtectedTicket { get; set; }
    }
}