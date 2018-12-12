using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessBlackList
    {
        public List<EntityBlackList> GetAll()
        {
            return new RepositoryBlackList().GetAll().Select(p => new EntityBlackList()
            {
               PK_BlackListID=p.PK_BlackListID,
               BlackListName=p.BlackListName,
               Status=p.Status,
               CreateDate=p.CreateDate,
               ModifyDate=p.ModifyDate
               
            }).ToList<EntityBlackList>();
        }
    }
}
