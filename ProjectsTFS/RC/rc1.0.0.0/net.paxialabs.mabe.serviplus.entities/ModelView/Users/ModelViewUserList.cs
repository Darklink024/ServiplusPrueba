using net.paxialabs.mabe.serviplus.resource.Security;
using System;
using System.ComponentModel.DataAnnotations;
namespace net.paxialabs.mabe.serviplus.entities.ModelView.Users
{
    public class ModelViewUserList
    {
        public int UserID { get; set; }
        public int ProfileID { get; set; }
        public Nullable<int> FK_ModuleID{get; set;}

        [Display(Name = "UserName", ResourceType = typeof(ResourceUser))]
        public string UserName { get; set; }
        [Display(Name = "Name", ResourceType = typeof(ResourceUser))]
        public string Name { get; set; }
        [Display(Name = "Email", ResourceType = typeof(ResourceUser))]
        public string Email { get; set; }        
        [Display(Name = "Status", ResourceType = typeof(ResourceUser))]
        public bool Status { get; set; }       
        [Display(Name = "DateLastAccess", ResourceType = typeof(ResourceUser))]
        public DateTime DateLastAccess { get; set; }
        [Display(Name = "DateCreate", ResourceType = typeof(ResourceUser))]
        public DateTime DateCreate { get; set; }
        [Display(Name = "DateModification", ResourceType = typeof(ResourceUser))]
        public DateTime DateModification { get; set; }

    }
}
