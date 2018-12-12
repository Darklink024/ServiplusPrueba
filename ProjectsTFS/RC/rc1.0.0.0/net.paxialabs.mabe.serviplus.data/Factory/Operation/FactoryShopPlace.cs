using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryShopPlace : BaseFactory<FactoryShopPlace, EntityShopPlace, ShopPlace>
    {
        public override EntityShopPlace _GetEntity(ShopPlace entidad)
        {
            return new EntityShopPlace()
            {
                PK_ShopPlaceID = entidad.PK_ShopPlaceID,
                FK_StateID = entidad.FK_StateID,
                ShopPlaceID = entidad.ShopPlaceID,
                ShopPlace1 = entidad.ShopPlace1,
                CountryAddress = entidad.CountryAddress,
                StateAddress = entidad.StateAddress,
                CityAddress = entidad.CityAddress,
                MunicipalityAddress = entidad.MunicipalityAddress,
                StreetAddress = entidad.StreetAddress,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate,
                ClientID = entidad.ClientID
            };
        }
    }
}
