using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeModuleService
    {
        public static List<EntityModuleService> GetAll()
        {
            return new BusinessModuleService().GetAll();
        }
        public static EntityModuleService GetAllBYModule(int moduleID)
        {
            return new BusinessModuleService().GetAllBYModule(moduleID);
        }
        public static List<EntityModuleService> GetAllBYModuleList(int moduleID)
        {
            return new BusinessModuleService().GetAllBYModuleList(moduleID);
        }
    }
}
