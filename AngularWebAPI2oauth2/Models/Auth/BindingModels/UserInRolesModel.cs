using System.Collections.Generic;

namespace AngularWebAPI2oauth2.Models.Auth.BindingModels
{
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