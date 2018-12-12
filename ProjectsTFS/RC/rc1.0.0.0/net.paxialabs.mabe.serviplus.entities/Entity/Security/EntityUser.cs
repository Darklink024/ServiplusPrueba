using net.paxialabs.mabe.serviplus.resource.Common;
using net.paxialabs.mabe.serviplus.resource.Security;
using System;
using System.ComponentModel.DataAnnotations;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Security
{
    public class EntityUser
    {
        public int UserID { get; set; }
        public int ProfileID { get; set; }
        public  Nullable<int> ModuleID { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "UserName", ResourceType = typeof(ResourceUser))]
        public string UserName { get; set; }
        [Display(Name = "Name", ResourceType = typeof(ResourceUser))]
        public string Name { get; set; }
        [Display(Name = "Password", ResourceType = typeof(ResourceUser))]
        public string Password { get; set; }
        [Display(Name = "Email", ResourceType = typeof(ResourceUser))]
        public string Email { get; set; }
        [Display(Name = "Token", ResourceType = typeof(ResourceUser))]
        public string Token { get; set; }        
        [Display(Name = "Status", ResourceType = typeof(ResourceUser))]
        public bool Status { get; set; }
        [Display(Name = "ChangePassword", ResourceType = typeof(ResourceUser))]
        public bool ChangePassword { get; set; }
        [Display(Name = "DateLastAccess", ResourceType = typeof(ResourceUser))]
        public DateTime DateLastAccess { get; set; }
        [Display(Name = "DateCreate", ResourceType = typeof(ResourceUser))]
        public DateTime DateCreate { get; set; }
        [Display(Name = "DateModification", ResourceType = typeof(ResourceUser))]
        public DateTime DateModification { get; set; }        
    }
}
