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
    public class FacadeValidationGuarantyProduct
    {
        public static List<EntityValidationGuarantyProduct> GetAll()
        {
            return new BusinessValidationGuarantyProduct().GetAll();
        }

        public static List<ModelViewGuarantyProduct> GetLisValidationProduct(ModelViewUserG objCred)
        {
            return new BusinessValidationGuarantyProduct().GetLisValidationProduct(objCred);
        }
    }
}
