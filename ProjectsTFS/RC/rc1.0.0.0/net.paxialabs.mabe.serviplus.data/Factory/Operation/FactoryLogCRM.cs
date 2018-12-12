using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryLogCRM : BaseFactory<FactoryLogCRM, EntityLogCRM, LogCRM>
    {
        public override EntityLogCRM _GetEntity(LogCRM entidad)
        {
            return new EntityLogCRM()
            {
                LogID = entidad.PK_LogID,
                OrderID = entidad.FK_OrderID,
                UpdateBase = entidad.UpdateBase,
                UpdateRefMan = entidad.UpdateRefMan,
                UpdateODS = entidad.UpdateODS,
                ModuleReserveSP = entidad.ModuleReserveSP,
                ADRReserveSP = entidad.ADRReserveSP,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}



