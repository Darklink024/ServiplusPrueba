using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{

    internal class FactoryQuotation : BaseFactory<FactoryQuotation, EntityQuotation, Quotation>
    {
        public override EntityQuotation _GetEntity(Quotation entidad)
        {
            return new EntityQuotation()
            {
                PK_QuotationID = entidad.PK_QuotationID,
                FK_OrdenID = entidad.FK_OrdenID,
                SubTotal = entidad.SubTotal,
                Total = entidad.Total,
                IVA = entidad.Total,
                Folio = entidad.Folio,
                URL = entidad.URL,
                OrdenVenta=String.IsNullOrEmpty(entidad.OrdenVenta)? "": entidad.OrdenVenta,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate,
                TypeQuotation = entidad.FK_TypeQuotation.HasValue  ? entidad.FK_TypeQuotation : 0,
                FK_EmployeeID=entidad.FK_EmployeeID.HasValue? entidad.FK_EmployeeID.Value : 0
            };
        }
    }
}
