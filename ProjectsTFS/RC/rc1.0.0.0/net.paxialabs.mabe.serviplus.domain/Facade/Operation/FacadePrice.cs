using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
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
    public class FacadePrice
    {
        public static void Insert(Precio model, int PK_BuildOfMaterialsID, int FK_ProductID)
        {
            new BusinessPrice().Insert(model, PK_BuildOfMaterialsID, FK_ProductID);
        }

        public static void BulkInsert(List<EntityPrice> Precios)
        {
            new BusinessPrice().BulkInsert(Precios);
        }

        public static List<ModelViewPrices> GetListPrices(ModelViewUserG objCred)
        {
            return new BusinessPrice().GetListPrices(objCred);
        }

        public static List<ModelViewWorkforcePrices> GetListPricesWorkforce(ModelViewUserG objCred)
        {
            return new BusinessPrice().GetListPricesWorkforce(objCred);
        }

    }
}
