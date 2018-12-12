using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessQuotationDetail
    {
        public void Insert(int FK_QuotationID, int FK_BuildOfMaterialsID)
        {

            var objRepository = new RepositoryQuotationDetail();
            EntityQuotationDetail data = new EntityQuotationDetail()
            {
                PK_QuotationDetailID = 0,
                FK_QuotationID = FK_QuotationID,
                FK_itemID = FK_BuildOfMaterialsID,
                Status = true,
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Insert(data);
        }
        public List<EntityQuotationDetail> GetList(int FK_QuotationID)
        {
            return new RepositoryQuotationDetail().GetByID(FK_QuotationID);
        }
        public EntityQuotationDetail GetDetail(int FK_QuotationID, int FK_BuildOfMaterialsID)
        {
            return new RepositoryQuotationDetail().GetDetail(FK_QuotationID, FK_BuildOfMaterialsID);
        }

    }
}
