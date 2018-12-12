using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryConfiguration : BaseFactory<FactoryConfiguration, EntityConfiguration, Configuration>
    {
        public override EntityConfiguration _GetEntity(Configuration entidad)
        {
            // PK_ConfigurationID, Title, Message, Url, Status, Publish, CreateDate, ModifyDate
            return new EntityConfiguration() {
                ConfigurationID = entidad.PK_ConfigurationID,
                Title = entidad.Title,
                Message = entidad.Message,
                Url = entidad.Url,
                Status = entidad.Status,
                Publish = entidad.Publish,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
