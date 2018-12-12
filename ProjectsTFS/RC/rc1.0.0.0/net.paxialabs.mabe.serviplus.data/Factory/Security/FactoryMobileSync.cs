using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Security
{
    internal class FactoryMobileSync : BaseFactory<FactoryMobileSync, EntityMobileSync, MobileSync>
    {
        public override EntityMobileSync _GetEntity(MobileSync entidad)
        {
            return new EntityMobileSync()
            {
                PK_MobileSyncID = entidad.PK_MobileSyncID,
                FK_UserID = entidad.FK_UserID,
                UserName = entidad.UserName,
                Name = entidad.Name,
                ODS = entidad.ODS,
                Type = entidad.Type,
                SyncDate = entidad.SyncDate,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
