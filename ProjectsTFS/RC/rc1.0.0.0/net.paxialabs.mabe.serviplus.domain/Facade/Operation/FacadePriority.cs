using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadePriority
    {
        public static List<ModelViewPriority> GetListPriority(ModelViewUserG objCred)
        {
            return new BusinessPriority().GetListPriority(objCred);
        }
        public static List<EntityPriority> GetAll()
        {
            return new BusinessPriority().GetAll();
        }

        public static void SetStatus(List<int> IDs)
        {
            new BusinessPriority().SetStatus(IDs);
        }
        public static List<EntityPriority> GetActives()
        {
            return new BusinessPriority().GetActives();
        }
    }
}
