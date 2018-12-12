using net.paxialabs.mabe.serviplus.data.Repository.Security;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessModule
    {
        public List<EntityModule> GetAll()
        {
            return new RepositoryModule().GetAll();
        }
    }
}
