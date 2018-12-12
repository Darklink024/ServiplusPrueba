using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryReceivers : BaseFactory<FactoryReceivers, EntityReceivers, Receivers>
    {
        public override EntityReceivers _GetEntity(Receivers entidad)
        {
            // FK_ConfigurationID, FK_UserID, MessageCreate, CreateDate, ModifyDate
            return new EntityReceivers() {
                ConfigurationID = entidad.FK_ConfigurationID,
                UserID = entidad.FK_UserID,
                MessageCreate = entidad.MessageCreate,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
