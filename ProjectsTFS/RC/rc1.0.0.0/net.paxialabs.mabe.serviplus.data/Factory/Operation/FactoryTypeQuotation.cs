using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryTypeQuotation : BaseFactory<FactoryTypeQuotation,EntityTypeQuotation,TypeQuotation>
    {
        public override EntityTypeQuotation _GetEntity(TypeQuotation entidad)
        {
            return new EntityTypeQuotation()
            {
                PK_TypeQuotation= entidad.PK_TypeQuotation,
                Description = entidad.Description,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
