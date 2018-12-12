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
    public static class FacadeOrder
    {
        public static List<ModelViewODSMovil> GetListOrden(ModelViewUserVisits objCred)
        {
            return new BusinessOrder().GetListOrden(objCred);
        }

        public static List<ModelViewListODS> GetListODS(ModelViewUserVisits objCred)
        {
            return new BusinessOrder().GetListODS(objCred);
        }
        
        public static EntityOrder Insert(net.paxialabs.mabe.serviplus.entities.Entity.Interface.ODS model, int PK_ClientID, int FK_InstalledBaseID, int FK_GuarantyID)
        {
            return new BusinessOrder().Insert(model, PK_ClientID, FK_InstalledBaseID, FK_GuarantyID);
        }

        public static EntityOrder Update(entities.Entity.Interface.ODS model, EntityOrder ord)
        {
            return new BusinessOrder().Update(model, ord);
        }

       
        public static List<EntityOrder> GetAll()
        {
            return new BusinessOrder().GetAll();
        }
        public static EntityOrder GetByOrderID(string OrderID)
        {
            return new BusinessOrder().GetByOrderID(OrderID);
        }

        public static void SetStatus(string OrderID)
        {
         new BusinessOrder().SetStatus(OrderID);
        }

        public static EntityOrder Get(int OrderID)
        {
            return new BusinessOrder().GetByID(OrderID);
        }

        public static string SetQuotation(ModelViewUserG objCred,ModelViewBilling obj)
        {
           return new BusinessOrder().SetQuotation(objCred, obj);
        }

        public static string SetQuotation2(ModelViewUserG objCred, ModelViewBilling obj,DateTime date)
        {
            return new BusinessOrder().SetQuotation2(objCred, obj,date);
        }
        public static string Complete(ModelViewODSMovilUploadList data)
        {
            return new BusinessOrder().Complete(data);
        }

        public static EntitySAPReprogramacionODSResult GetReprogramingODS(ModelViewUserVisits objCred)
        {
            return new BusinessOrder().GetReprograminODS(objCred);
        }

        public static void SetOrderSecuence(ModelViewUserSecuence data)
        {
            new BusinessOrder().SetOrderSecuence(data);
        }

        public static void Complete(ModelViewPreODS data)
        {
            //new BusinessOrder().Complete(data);
        }

        public static void UpdateStatusOrder(ModelViewUpdateStatusOrder data)
        {
            new BusinessOrder().UpdateStatusOrder(data);
        }

        public static void UpdateStatusOrderWS(string orderID, string EstatusEsq, string EstatusCabecera, string tokenApp, string user, string password)
        {
            new BusinessOrder().UpdateStatusOrderWS(orderID, EstatusEsq, EstatusCabecera,  tokenApp, user, password);
        }

        public static List<AddressV> GetList_Address(string OrderID)
        {
            return new BusinessOrder().GetList_Address(OrderID);
        }

        public static bool SetUser(int OrderID, int EmployeeID)
        {
            return new BusinessOrder().SetUser(OrderID, EmployeeID);
        }

       
    }
}
