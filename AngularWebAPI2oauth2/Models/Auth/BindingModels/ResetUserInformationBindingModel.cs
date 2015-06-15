using System.ComponentModel.DataAnnotations;

namespace AngularWebAPI2oauth2.Models.Auth.BindingModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ResetUserInformationBindingModel
    {
        /// <remarks/>
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        /// <remarks/>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}