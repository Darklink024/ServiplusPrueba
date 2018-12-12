using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeClient
    {
        public static ODSCLIENTE Insert(ODSCLIENTE model)
        {
            return new BusinessClient().Insert(model);
        }

        public static ODSCLIENTE Update(ODSCLIENTE model, EntityClient cliente)
        {
            return new BusinessClient().Update(model, cliente);
        }

        public static List<EntityClient> GetAll()
        {
            return new BusinessClient().GetAll();
        }

        public static EntityClient GetByID(int FK_ClientID)
        {
            return new BusinessClient().GetByID(FK_ClientID);
        }

        public static EntityClient GetByClientID(string ClientID)
        {
            return new BusinessClient().GetByClientID(ClientID);
        }

    }
}
