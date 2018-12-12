using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{

   internal class FactoryBuildOfMaterial : BaseFactory<FactoryBuildOfMaterial, EntityBuildOfMaterial, BuildOfMaterials>
    {
        public override EntityBuildOfMaterial _GetEntity(BuildOfMaterials entidad)
        {
            return new EntityBuildOfMaterial()
            {
                PK_BuildOfMaterialsID = entidad.PK_BuildOfMaterialsID,
                FK_ProductID = entidad.FK_ProductID,
                Model = entidad.Model,
                SparePartsID = entidad.SparePartsID,
                Quantity = entidad.Quantity,
                StatusBOM = entidad.StatusBOM,
                SparePartDescription = entidad.SparePartDescription,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
