using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
 internal class FactoryValidationGuarantyBOM : BaseFactory<FactoryValidationGuarantyBOM, EntityValidationGuarantyBOM, ValidationGuarantyBOM>

    {
        public override EntityValidationGuarantyBOM _GetEntity(ValidationGuarantyBOM entidad)
        {
            return new EntityValidationGuarantyBOM()
            {

                PK_ValidationGuarantySparePartID = entidad.PK_ValidationGuarantySparePartID,
                FK_BuildOfMaterialsID = entidad.FK_BuildOfMaterialsID,
                FK_ProducID = entidad.FK_ProducID,
                Model = entidad.Model,
                SalesOrganization = entidad.SalesOrganization,
                SparePartsID = entidad.SparePartsID,
                ClientID = entidad.ClientID,
                Months = entidad.Months,
                ValidFrom = entidad.ValidFrom,
                ValidTo = entidad.ValidTo,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
