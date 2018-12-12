using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessGuarantyType
    {
        public List<EntityGuarantyType> GetAll()
        {
            return new RepositoryGuarantyType().GetAll().Select(p => new EntityGuarantyType()
            {
                PK_GuarantyTypeID = p.PK_GuarantyTypeID,
                GuarantyType1 = p.GuarantyType1,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList();
        }

        public EntityGuarantyType Get(int ID)
        {
            return new RepositoryGuarantyType().Get(ID);
        }
    }
}
