using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularWebAPI2oauth2.Models.Auth.BindingModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangePasswordBindingModel
    {
        /// <remarks/>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }
        /// <remarks/>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
        /// <remarks/>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

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

    /// <summary>
    /// 
    /// </summary>
    public class CreateRoleBindingModel
    {
        /// <remarks/>
        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserInRolesModel
    {
        /// <remarks/>
        public string UserName { get; set; }
        /// <remarks/>
        public List<string> EnrolledRoles { get; set; }
        /// <remarks/>
        public List<string> RemovedRoles { get; set; }
    }
}