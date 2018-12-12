using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Users
{
    public static class FacadeUsers
    {
        public static List<ModelViewUserList> GetAll()
        {
            return new BusinessUsers().GetAll();
        }

        public static List<ModelViewUsers> GetAllList()
        {
            return new BusinessUsers().GetAllList();
        }
        public static List<ModelViewUsers> GetAllListprofile()
        {
            return new BusinessUsers().GetAllList();
        }
        public static List<ModelViewUserList> GetAllUserModuleProfile(int module , int profile)
        {
            return new BusinessUsers().GetAllUserModuleProfile(module, profile);
        }
        public static EntityUser GetByID(int UserID)
        {
            return new BusinessUsers().GetByID(UserID);
        }
        public static List<EntityUser> GetUserByModule(int ModuleID)
        {
            return new BusinessUsers().GetUserByModule(ModuleID);
        }
                        
        public static List<ModelViewUserList> GetActives()
        {
            return new BusinessUsers().GetActives();
        }



        public static ModelViewUser Insert(ModelViewUser model)
        {
            return new BusinessUsers().Insert(model);
        }

        public static ModelViewUser Update(ModelViewUser model)
        {
            return new BusinessUsers().Update(model);
        }

        public static void SetStatus(List<int> IDs)
        {
            new BusinessUsers().SetStatus(IDs);
        }

        public static EntityUser GetUserByToken(string  Token)
        {
            return new BusinessUsers().GetUserByToken(Token);
        }

        public static void RegisterFCM(ModelViewUserFCM model)
        {
            new BusinessUsers().RegisterFCM(model);
        }
    }
}
