using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{

    internal class FactoryPrice : BaseFactory<FactoryPrice, EntityPrice, Prices>
    {
        public override EntityPrice _GetEntity(Prices entidad)
        {
            return new EntityPrice()
            {
                 PK_PriceID = entidad.PK_PriceID,
                 FK_BuildOfMaterialsID = entidad.FK_BuildOfMaterialsID,
                 FK_ProductID = entidad.FK_ProductID,
                 FK_WorkforceID = entidad.FK_WorkforceID,
                 TypeCondition = entidad.TypeCondition,
                 SalesOrganization = entidad.SalesOrganization,
                 DistributionChannel = entidad.DistributionChannel,
                 ListPrice = entidad.ListPrice,
                 GroupMaterial1 = entidad.GroupMaterial1,
                 GroupMaterial4 = entidad.GroupMaterial4,
                 Price = entidad.Price, 
                 Coin = entidad.Coin,
                 DateValidity = entidad.DateValidity,
                 DateValidityEnd = entidad.DateValidityEnd,
                 Policy = entidad.Policy,
                 Guaranty = entidad.Guaranty,
                 Status = entidad.Status,
                 CreateDate = entidad.CreateDate,
                 ModifyDate = entidad.ModifyDate,
                 Refaccion = entidad.BuildOfMaterials.SparePartsID
            };
        }
    }
}
