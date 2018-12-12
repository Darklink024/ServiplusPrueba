using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessModuleService
    {
        public EntityModuleService Get(int Id)
        {
            return new RepositoryModuleService().Get(Id);
        }

        public List<EntityModuleService> GetAll()
        {
            return new RepositoryModuleService().GetAll();
        }
        public EntityModuleService GetAllBYModule(int module)
        {
            return new RepositoryModuleService().GetAllBYModule(module);
        }
        public List<EntityModuleService> GetAllBYModuleList(int module)
        {
            return new RepositoryModuleService().GetAllBYModuleList(module);
        }
    }
}
