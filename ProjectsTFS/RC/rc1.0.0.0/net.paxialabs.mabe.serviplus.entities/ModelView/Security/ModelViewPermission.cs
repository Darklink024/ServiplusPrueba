using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.resource.Security;
using System.ComponentModel.DataAnnotations;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Security
{
    public class ModelViewPermission : EntityPermission
    {
        [Display(Name = "Section", ResourceType = typeof(ResourceModule))]
        public string Section { get; set; }
        [Display(Name = "Module", ResourceType = typeof(ResourceModule))]
        public string Module { get; set; }
    }
}
