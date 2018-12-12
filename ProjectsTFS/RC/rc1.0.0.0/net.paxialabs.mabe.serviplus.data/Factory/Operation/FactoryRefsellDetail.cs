using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryRefsellDetail : BaseFactory<FactoryRefsellDetail,EntityRefsellDetail, RefsellQuotation>
    {
        public override EntityRefsellDetail _GetEntity(RefsellQuotation entidad)
        {
            return new EntityRefsellDetail()
            {
               PK_RefsellDetailID=entidad.PK_RefsellDetailID,
               Fk_QuotationID=entidad.Fk_QuotationID,
                Fk_ProductID=entidad.Fk_ProductID,
                Cantidad= entidad.Cantidad,
                RefMan= entidad.RefMan,
                Origen=entidad.Origen,
                CostoRef=entidad.CostoRef,
                Flete=entidad.Flete,
                CostoFlete=entidad.CostoFlete,
                OrdenVenta=entidad.OrdenVenta,
                Status=entidad.Status,
                CreateDate=entidad.CreateDate,
                ModifyDate= entidad.ModifyDate
            };
        }

    }
}
