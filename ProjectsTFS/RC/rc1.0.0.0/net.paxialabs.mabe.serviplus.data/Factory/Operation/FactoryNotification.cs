using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.data.Repository.Security;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryNotification : BaseFactory<FactoryNotification, EntityNotification, Message>
    {
        public override EntityNotification _GetEntity(Message entidad)
        {
            return new EntityNotification() {
                MessageID = entidad.PK_MessageID,
                UserID = entidad.FK_UserID,
                Message = entidad.Message1,
                Title = entidad.Title,
                Url = entidad.Url,
                MessageRead = entidad.MessageRead,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate,                
                UserName = new RepositoryUser().Get(entidad.FK_UserID).UserName                
            };
        }
    }
}
