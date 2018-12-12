using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactorySparePart : BaseFactory<FactorySparePart, EntitySparePart, SpareParts>
    {
        public override EntitySparePart _GetEntity(SpareParts entidad)
        {
            return new EntitySparePart()
            {
                PK_SparePartsID = entidad.PK_SparePartsID,
                FK_OrderID = entidad.FK_OrderID,
                FK_BuildOfMaterialsID = entidad.FK_BuildOfMaterialsID,
                FK_ProductID = entidad.FK_ProductID,
                FK_WorkforceID = entidad.FK_WorkforceID,
                RefManID = entidad.RefManID,
                Quantity = entidad.Quantity,
                Price = entidad.Price,
                Status = entidad.Status,
                PosicionItem = entidad.PosicionItem,
                SparePartStatus = entidad.SparePartStatus,
                StatusSchema = entidad.StatusSchema,
                ItemRefMan = entidad.SparePartDescription,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
