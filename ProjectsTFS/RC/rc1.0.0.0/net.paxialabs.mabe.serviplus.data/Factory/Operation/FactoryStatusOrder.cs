using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryStatusOrder : BaseFactory<FactoryStatusOrder, EntityStatusOrder, StatusOrder>
    {
        public override EntityStatusOrder _GetEntity(StatusOrder entidad)
        {
            return new EntityStatusOrder()
            {
                PK_StatusOrderID = entidad.PK_StatusOrderID,
                StatusOrder1 = entidad.StatusOrder1,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
