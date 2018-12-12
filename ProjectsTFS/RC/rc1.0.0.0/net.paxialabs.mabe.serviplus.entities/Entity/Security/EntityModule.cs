using net.paxialabs.mabe.serviplus.resource.Common;
using net.paxialabs.mabe.serviplus.resource.Security;
using System.ComponentModel.DataAnnotations;


namespace net.paxialabs.mabe.serviplus.entities.Entity.Security
{
    public class EntityModule
    {
        public int ModuleID { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "Section", ResourceType = typeof(ResourceModule))]
        public string Section { get; set; }
        [Display(Name = "Module", ResourceType = typeof(ResourceModule))]
        public string Module { get; set; }
        [Display(Name = "Description", ResourceType = typeof(ResourceModule))]
        public string Description { get; set; }
        [Display(Name = "URL", ResourceType = typeof(ResourceModule))]
        public string URL { get; set; }
        [Display(Name = "Status", ResourceType = typeof(ResourceModule))]
        public bool Status { get; set; }
    }
}
