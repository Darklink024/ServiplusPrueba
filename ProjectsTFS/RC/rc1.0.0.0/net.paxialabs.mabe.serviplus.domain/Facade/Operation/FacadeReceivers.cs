using net.paxialabs.mabe.serviplus.domain.Business.Notification;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeReceivers
    {
        public static ModelViewReceivers Get(int Id)
        {
            return new BusinessReceivers().Get(Id);
        }

        public static List<ModelViewReceivers> GetActives()
        {
            return new BusinessReceivers().GetActives();
        }

        public static List<ModelViewReceivers> GetAll()
        {
            return new BusinessReceivers().GetAll();
        }

        public static List<ModelViewReceivers> GetByConfiguration(int ConfigurationID)
        {
            return new BusinessReceivers().GetByConfiguration(ConfigurationID);
        }

        public static ModelViewReceivers Insert(ModelViewReceivers data)
        {
            return new BusinessReceivers().Insert(data);
        }

        public static ModelViewReceivers Update(ModelViewReceivers data)
        {
            return new BusinessReceivers().Update(data);
        }
    }
}
