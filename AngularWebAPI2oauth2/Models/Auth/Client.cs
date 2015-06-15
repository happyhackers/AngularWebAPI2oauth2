using System.ComponentModel.DataAnnotations;

namespace AngularWebAPI2oauth2.Models.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class Client
    {
        /// <remarks/>
        [Key]
        public string Id { get; set; }
        /// <remarks/>
        [Required]
        public string Secret { get; set; }
        /// <remarks/>
        [Required, MaxLength(100)]
        public string Name { get; set; }
        /// <remarks/>
        public ApplicationTypes ApplicationType { get; set; }
        /// <remarks/>
        public bool Active { get; set; }
        /// <remarks/>
        public int RefreshTokenLifeTime { get; set; }
        /// <remarks/>
        [MaxLength(100)]
        public string AllowedOrigin { get; set; }
    }
}