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
   public  class FacadeGuaranty
    {

        public static List<ModelViewGuarantys> GetListGuaranty(ModelViewUserG objCred)
        {
            return new BusinessGuaranty().GetListGuaranty(objCred);
        }
        public static List<EntityGuaranty> GetAll()
        {
            return new BusinessGuaranty().GetAll();
        }
        public static EntityGuaranty GetByGuarantyID(string GuarantyID)
        {
            return new BusinessGuaranty().GetByGuarantyID(GuarantyID);
        }
        public static EntityGuaranty Get(int ID)
        {
            return new BusinessGuaranty().Get(ID);
        }
    }
}
