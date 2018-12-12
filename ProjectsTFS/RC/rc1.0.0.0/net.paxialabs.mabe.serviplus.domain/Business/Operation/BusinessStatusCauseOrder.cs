using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessStatusCauseOrder
    {
        public List<EntityStatusOrder> GetAll()
        {
            return new RepositoryStatusOrder().GetAll().Select(p => new EntityStatusOrder()
            {
                PK_StatusOrderID = p.PK_StatusOrderID,
                StatusOrder1 = p.StatusOrder1,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityStatusOrder>();
        }
    }
}
