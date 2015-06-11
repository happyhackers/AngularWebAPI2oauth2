using System;

namespace AngularWebAPI2oauth2.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
    }
}