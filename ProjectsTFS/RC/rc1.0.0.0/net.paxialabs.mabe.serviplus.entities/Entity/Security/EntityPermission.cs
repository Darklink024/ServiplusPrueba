using net.paxialabs.mabe.serviplus.resource.Common;
using net.paxialabs.mabe.serviplus.resource.Security;
using System.ComponentModel.DataAnnotations;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Security
{
    public class EntityPermission
    {
        public int ModuleID { get; set; }
        public int ProfileID { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "Access", ResourceType = typeof(ResourcePermission))]
        public bool Access { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "Read", ResourceType = typeof(ResourcePermission))]
        public bool Read { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "Add", ResourceType = typeof(ResourcePermission))]
        public bool Add { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "Update", ResourceType = typeof(ResourcePermission))]
        public bool Update { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "Delete", ResourceType = typeof(ResourcePermission))]
        public bool Delete { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "Export", ResourceType = typeof(ResourcePermission))]
        public bool Export { get; set; }
    }
}
