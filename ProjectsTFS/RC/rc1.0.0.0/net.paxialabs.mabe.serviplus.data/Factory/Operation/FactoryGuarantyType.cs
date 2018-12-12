using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryGuarantyType : BaseFactory<FactoryGuarantyType, EntityGuarantyType, GuarantyType>
    {
        public override EntityGuarantyType _GetEntity(GuarantyType entidad)
        {
            return new EntityGuarantyType()
            {
                PK_GuarantyTypeID = entidad.PK_GuarantyTypeID,   
                GuarantyType1 = entidad.GuarantyType1,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
