using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Security
{
    internal class FactoryLogMobile : BaseFactory<FactoryLogMobile, EntityLogMobile, LogMobile>
    {
        public override EntityLogMobile _GetEntity(LogMobile entidad)
        {
            return new EntityLogMobile()
            {
                PK_LogMobileID = entidad.PK_LogMobileID,
                FK_OrderID = entidad.FK_OrderID,
                FK_UserID = entidad.FK_UserID,
                UserName = entidad.UserName,
                Name = entidad.Name,
                OrderID = entidad.OrderID,
                Module = entidad.Module,
                Message = entidad.Message,
                InnerException = entidad.InnerException,
                StackTrace = entidad.StackTrace,
                SignType = entidad.SignType,
                Battery = entidad.Battery,
                SignPercentage = entidad.SignPercentage,
                ConnectionType = entidad.ConnectionType,
                MobileModel=entidad.MobileModel,
                MobileStorage=entidad.MobileStorage,
                version = entidad.version,
                Type = entidad.Type,
                Date = entidad.Date,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
