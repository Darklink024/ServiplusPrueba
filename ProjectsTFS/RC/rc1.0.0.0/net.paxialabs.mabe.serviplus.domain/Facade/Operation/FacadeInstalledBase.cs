using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeInstalledBase
    {
        public static EntityInstalledBase Insert(int FK_ClientID, int? PK_ProductID, int? FK_ShopPlaceID, string IDBaseInstalada, string NumeroSerie, string fechaCompra,  string Model, string ProductName)
        {
            return new BusinessInstalledBase().Insert(FK_ClientID, PK_ProductID, FK_ShopPlaceID, IDBaseInstalada, NumeroSerie, fechaCompra, Model, ProductName);
        }

        public static EntityInstalledBase GetByID(int FK_InstalledBaseID)
        {
            return new BusinessInstalledBase().GetByID(FK_InstalledBaseID);
        }
       


        public static List<EntityInstalledBase> GetAll()
        {
            return new BusinessInstalledBase().GetAll();
        }

    }
}
