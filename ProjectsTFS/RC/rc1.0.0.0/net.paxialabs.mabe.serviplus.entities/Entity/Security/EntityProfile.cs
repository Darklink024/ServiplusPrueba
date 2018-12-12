using net.paxialabs.mabe.serviplus.resource.Common;
using net.paxialabs.mabe.serviplus.resource.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Security
{
    public class EntityProfile
    {
        public int ProfileID { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "Profile", ResourceType = typeof(ResourceProfile))]
        public string Profile { get; set; }
        [Display(Name = "Description", ResourceType = typeof(ResourceProfile))]
        public string Description { get; set; }
        [Display(Name = "Status", ResourceType = typeof(ResourceProfile))]
        public bool Status { get; set; }
    }
}
