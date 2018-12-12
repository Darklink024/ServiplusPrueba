using net.paxialabs.mabe.serviplus.data.Repository.Security;
using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Security
{
    internal class BusinessLogMobile
    {

        public string Insert(ModelViewLog model)
        {
            if (model.TokenApp != GlobalConfiguration.TokenWEB)
                if (model.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            var User = new BusinessUsers().GetUserByToken(model.TokenUser);
            List<string> token = new List<string>();
            foreach (var item in model.Detail)
            {
                var Ods = new BusinessOrder().GetByOrderID(item.OrderID);
                EntityLogMobile data = new EntityLogMobile()
                {
                    PK_LogMobileID = 0,
                    FK_OrderID = Ods == null ? 0 : Ods.PK_OrderID,
                    FK_UserID = User.UserID,
                    UserName = User.UserName,
                    Name = User.Name,
                    OrderID = item.OrderID,
                    Module = item.Module,
                    Message = item.Message,
                    InnerException = item.InnerException,
                    StackTrace = item.StackTrace,
                    SignType = item.SignType,
                    Battery = item.Battery,
                    SignPercentage = item.SignPercentage,
                    ConnectionType = item.ConnectionType,
                    version = item.version,
                    MobileModel=item.MobileModel,
                    MobileStorage=item.MobileStorage,
                    Type = item.Type,
                    Date = DateTime.Parse(item.Date),
                    Status = true,
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow

                };
                data = new RepositoryLogMobile().Insert(data);
                token.Add(item.TokenLog);
            }

            string TokenLog = string.Join(",", token);

            return TokenLog;
        }

    }
}
