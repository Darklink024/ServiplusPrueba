using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.resource.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Users
{
    public class ModelViewUser
    {
        public int UserID { get; set; }

        [Display(Name = "ProfileID", ResourceType = typeof(ResourceUser))]
        public int ProfileID { get; set; }
        public Nullable<int> FK_ModuleID { get; set; }
        [Required(ErrorMessage = "Nombre de Usuario requerido")]
        [Display(Name = "UserName", ResourceType = typeof(ResourceUser))]
        public string UserName { get; set; }
        [Display(Name = "Name", ResourceType = typeof(ResourceUser))]
        [Required(ErrorMessage = "Nombre requerido")]
        public string Name { get; set; }
        [Display(Name = "Email", ResourceType = typeof(ResourceUser))]
        [Required(ErrorMessage = "Email requerido")]
        public string Email { get; set; }
        public string Token { get; set; }
        public string Profile { get; set; }
        public bool ChangePassword { get; set; }
        public List<EntityEmployeeStore> EmployeeStore { get; set; }
        public Single? LatWorkshop { get; set; }
        public Single? LongWorkshop { get; set; }

    }
}
