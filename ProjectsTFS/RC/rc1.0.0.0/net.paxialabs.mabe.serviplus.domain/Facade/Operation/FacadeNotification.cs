using net.paxialabs.mabe.serviplus.domain.Business.Notification;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System.Collections.Generic;


namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public static class FacadeNotification
    {
        public static void SendPush(List<int> UserIDs, string Title, string Body)
        {
            new BusinessNotification().SendPush(UserIDs, Title, Body);
        }

        public static ModelViewNotification Insert(ModelViewNotification model)
        {
            return new BusinessNotification().Insert( model);
        }

        public static ModelViewNotification Update(ModelViewNotification model)
        {
            return new BusinessNotification().Update(model);
        }

        public static void Status(ModelViewNotificationUpdate model)
        {
            new BusinessNotification().Status(model);
        }
               

        public static List<ModelViewNotification> GetAll()
        {
            return new BusinessNotification().GetAll();
        }

        public static List<ModelViewNotification> GetAllActives()
        {
            return new BusinessNotification().GetAllActives();
        }

        public static List<ModelViewNotification> GetByToken(EntitySecurity TokenUser)
        {
            return new BusinessNotification().GetByToken(TokenUser);
        }

        public static List<ModelViewNotification> GetByTokenActive(string TokenUser)
        {
            return new BusinessNotification().GetByTokenActive(TokenUser);
        }

        public static List<ModelViewUserNotification> GetAllNotificationUsers(string ModuleID, string Name)
        {
            return new BusinessNotification().GetAllNotificationUsers(ModuleID, Name);
        }

        public static List<ModelViewUserNotification> GetAllNotificationUsers(string Employee)
        {
            return new BusinessNotification().GetAllNotificationUsers(Employee);
        }

    }
}
