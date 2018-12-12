using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
   internal class FactoryContrat: BaseFactory<FactoryContrat,EntityContract,ContratReceipt>
    {
        public override EntityContract _GetEntity(ContratReceipt entidad)
        {
            return new EntityContract()
            {
              PK_ContractID=entidad.PK_ContratID,
              Fk_OrderID=entidad.Fk_OrderID,
              Folio=entidad.Folio,
              Ruta=entidad.Ruta,
              Status=entidad.Status,
              CreateDate=entidad.CreateDate,
              ModifyDate=entidad.ModifyDate
            };
        }
    }
}
