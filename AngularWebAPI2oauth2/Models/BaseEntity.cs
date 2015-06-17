using System;

namespace AngularWebAPI2oauth2.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseEntity
    {
        /// <remarks/>
        public int Id { get; set; }
        /// <remarks/>
        public DateTime? Created { get; set; }
        /// <remarks/>
        public DateTime? Modified { get; set; }
        /// <remarks/>
        public string Creator { get; set; }
        /// <remarks/>
        public string Modifier { get; set; }
    }
}