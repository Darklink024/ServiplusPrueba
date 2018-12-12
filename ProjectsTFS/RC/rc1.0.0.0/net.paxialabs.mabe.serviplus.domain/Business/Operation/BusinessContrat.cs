using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
   internal class BusinessContrat
    {
        public EntityContract GetByOrderFolio(int OrderID, string Folio)
        {
            return new RepositoryContract().GetByOrderFolio(OrderID, Folio);
        }

        public List<EntityContract> GetAll()
        {
            return new RepositoryContract().GetAll();
        }
        public EntityContract Insert(int FK_OrdenID, string Folio, string URL)
        {

            var objRepository = new RepositoryContract();
            EntityContract data = new EntityContract()
            {
                PK_ContractID = 0,
                Fk_OrderID = FK_OrdenID,
                Folio = Folio,
                Ruta =URL,                
                Status = true,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                ModifyDate = DateTime.UtcNow.ToLocalTime(),
                

            };
            data = objRepository.Insert(data);
            return data;

        }
        public EntityContract Update(EntityContract data)
        {
            return new RepositoryContract().Update(data);
        }
    }
}
