using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.SAP;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
   
    public static class FacadePolicy
    {

        public static List<EntityPolicy> GetAll()
        {
            return new BusinessPolicy().GetAll();
        }
        public static string SetPolicyQuotation(ModelViewUserG objCred, ModelViewBilling obj)
        {
            return new BusinessPolicy().SetPolicyQuotation(objCred, obj);
        }
        public static string SetRefQuotation(ModelViewUserG objCred, ModelViewBilling obj)
        {
            return new BusinessPolicy().SetRefQuotation(objCred, obj);
        }
        public static string CreateContrat(ModelViewContrat obj)
        {
            return new BusinessPolicy().CreateContract(obj);
        }


        //public static void SendSMS()
        //{
        //    new BusinessPolicy().SendNotification();
        //}
        public static string CreateReceipt(ModelViewUserG objCred, ModelViewReceiptRef obj)
        {
            return new BusinessPolicy().CreateReceipt(objCred, obj);
        }

    }
}
