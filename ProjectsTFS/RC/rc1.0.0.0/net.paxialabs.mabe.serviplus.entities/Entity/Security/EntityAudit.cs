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
    public enum AuditAction
    {
        Access,
        Read,
        Add,
        Update,
        Delete,
        Export
    }

    public class EntityAudit
    {
        public int AuditID { get; set; }
        public int UserID { get; set; }
        public int ModuleID { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "DateAudit", ResourceType = typeof(ResourceAudit))]
        public DateTime DateAudit { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceValidation))]
        [Display(Name = "Action", ResourceType = typeof(ResourceAudit))]
        public string Action { get; set; }
    }
}
