using net.paxialabs.mabe.serviplus.resource.Common;
using net.paxialabs.mabe.serviplus.resource.Security;
using System.ComponentModel.DataAnnotations;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Security
{
    public class ModelViewChangePassword
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "OldPassword", ResourceType = typeof(ResourceUser))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "NewPassword", ResourceType = typeof(ResourceUser))]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(ResourceUser))]
        public string ConfirmPassword { get; set; }

        public string TokenApp { get; set; }

        public string TokenUser { get; set; }
    }
}
