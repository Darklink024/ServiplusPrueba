using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{

    internal class FactoryQuotationDetail : BaseFactory<FactoryQuotationDetail, EntityQuotationDetail, QuotationDetail>
    {
        public override EntityQuotationDetail _GetEntity(QuotationDetail entidad)
        {
            return new EntityQuotationDetail()
            {
                PK_QuotationDetailID = entidad.PK_QuotationDetailID,
                FK_QuotationID = entidad.FK_QuotationID,
                FK_itemID = entidad.FK_itemID,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
