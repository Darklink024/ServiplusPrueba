using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryGuaranty : BaseFactory<FactoryGuaranty, EntityGuaranty, Guaranty>
    {
        public override EntityGuaranty _GetEntity(Guaranty entidad)
        {
            return new EntityGuaranty()
            {
                PK_GuarantyID = entidad.PK_GuarantyID,
                FK_GuarantyTypeID = entidad.FK_GuarantyTypeID,
                GuarantyID = entidad.GuarantyID,
                Guaranty1 = entidad.Guaranty1,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
