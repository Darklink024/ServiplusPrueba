using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessRefsellDetail
    {
        public EntityRefsellDetail GetDetail(int FK_QuotationID, int FK_BuildOfMaterialsID)
        {
            return new RepositoryRefsellDetail().GetDetail(FK_QuotationID, FK_BuildOfMaterialsID);
        }
        public List<EntityRefsellDetail> GetList(int FK_QuotationID)
        {
            return new RepositoryRefsellDetail().GetByID(FK_QuotationID);
        }
        public void Insert(int FK_QuotationID, int Fk_Product,string Cantidad, string RefMan,string Origen, string RefPrice)
        {

            var objRepository = new RepositoryRefsellDetail();
            EntityRefsellDetail data = new EntityRefsellDetail()
            {
                PK_RefsellDetailID = 0,
                Fk_QuotationID = FK_QuotationID,
                Fk_ProductID = Fk_Product,
                Cantidad = Cantidad,
                RefMan = RefMan,
                Origen = Origen,
                CostoRef=RefPrice,
                Flete = false,
                CostoFlete="",
                OrdenVenta="",
                Status = true,
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Insert(data);
        }
        public EntityRefsellDetail Update(EntityRefsellDetail data)
        {
            return new RepositoryRefsellDetail().Update(data);
        }
    }
}
