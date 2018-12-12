using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeSchedule
    {


        public static List<ModelViewPriority> GetListAll()
        {
            return new BusinessSchedule().GetListAll();
        }
        public static List<EntitySchedule> GetAll()
        {
            return new BusinessSchedule().GetAll();
        }

        public static void SetStatus(List<int> IDs)
        {
            new BusinessSchedule().SetStatus(IDs);
        }

        public static ModelViewPriority Update(ModelViewPriority model)
        {
            return new BusinessSchedule().Update(model);
        }
    }
}
