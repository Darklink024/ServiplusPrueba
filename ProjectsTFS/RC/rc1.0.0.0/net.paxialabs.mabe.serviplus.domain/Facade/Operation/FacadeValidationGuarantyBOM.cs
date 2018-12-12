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
    public class FacadeValidationGuarantyBOM
    {
        public static List<EntityValidationGuarantyBOM> GetAll()
        {
            return new BusinessValidationGuarantyBOM().GetAll();
        }

        public static List<ModelViewGuarantyBOM> GetLisValidationBOM(ModelViewUserG objCred)
        {
            return new BusinessValidationGuarantyBOM().GetLisValidationBOM(objCred);
        }
    }
}
