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
    public class FacadeShopPlace
    {
        public static LugComp Insert(LugComp model)
        {
            return new BusinessShopPlace().Insert(model);
        }

        public static void BulkInsert(List<EntityShopPlace> ShopPlace)
        {
            new BusinessShopPlace().BulkInsert(ShopPlace);
        }

        public static LugComp Update(LugComp model, EntityShopPlace shop)
        {
            return new BusinessShopPlace().Update(model, shop);
        }
        public static List<ModelViewShopPlace> GetListShopPlace(ModelViewUserG objCred)
        {
            return new BusinessShopPlace().GetListShopPlace(objCred);
        }
        public static List<EntityShopPlace> GetAll()
        {
            return new BusinessShopPlace().GetAll();
        }
        public static EntityShopPlace Get(int ShopPlaceID)
        {
            return new BusinessShopPlace().GetByShopPlaceID(ShopPlaceID);
        }
        public static EntityShopPlace GetByShopPlace(string ShopPlaceID)
        {
            return new BusinessShopPlace().GetByShopPlace(ShopPlaceID);
        }

    }
}
