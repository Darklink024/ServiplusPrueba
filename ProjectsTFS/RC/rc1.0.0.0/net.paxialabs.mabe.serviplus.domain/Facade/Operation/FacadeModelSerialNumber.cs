using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeModelSerialNumber
    {

        public static List<ModelViewSerialNumber> GetListModel(ModelViewUserG objCred)
        {
            return new BusinessModelSerialNumber().GetListModel(objCred);
        }
        public static List<EntityModelSerialNumber> GetAll()
        {
            return new BusinessModelSerialNumber().GetAll();
        }
    }
}
