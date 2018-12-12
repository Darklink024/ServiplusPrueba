using net.paxialabs.mabe.serviplus.resource.Common;
using net.paxialabs.mabe.serviplus.resource.Security;
using System.ComponentModel.DataAnnotations;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Security
{
    public class ModelViewRecovery
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "Email", ResourceType = typeof(ResourceUser))]
        public string Email { get; set; }
        public string TokenApp { get; set; }
    }
}
