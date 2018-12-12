using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessTypeQuotation
    {
        public List<EntityTypeQuotation> GetAll()
        {
            return new RepositoryTypeQuotation().GetAll();
        }
        public EntityTypeQuotation GetTypeQuotation(int TypeQuotation)
        {
            return new RepositoryTypeQuotation().Get(TypeQuotation);
        }
    }
}
