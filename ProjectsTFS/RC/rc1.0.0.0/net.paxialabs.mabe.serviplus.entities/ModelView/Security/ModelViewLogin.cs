using net.paxialabs.mabe.serviplus.resource.Common;
using net.paxialabs.mabe.serviplus.resource.Security;
using System;
using System.ComponentModel.DataAnnotations;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Security
{
    [Serializable]
    public class ModelViewLogin
    {
        //[Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        //[Display(Name = "UserName", ResourceType = typeof(ResourceUser))]
        public string UserName { get; set; }

        //[Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        //[Display(Name = "Password", ResourceType = typeof(ResourceUser))]
        public string Password { get; set; }

        public string Token { get; set; }

        public string Origin { get; set; }
    }
}
