using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{

    internal class FactoryValidationGuarantyProduct : BaseFactory<FactoryValidationGuarantyProduct, EntityValidationGuarantyProduct, ValidationGuarantyProduct>
    {
        public override EntityValidationGuarantyProduct _GetEntity(ValidationGuarantyProduct entidad)
        {
            return new EntityValidationGuarantyProduct()
            {
                PK_ValidationGuarantyProductID = entidad.PK_ValidationGuarantyProductID,
                FK_ProducID = entidad.FK_ProducID,
                Country = entidad.Country,
                Model = entidad.Model,
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
