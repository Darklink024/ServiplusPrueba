using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewNotification : EntityNotification
    {
        public String CreateDateString { get; set; }
        public String ModifyDateString { get; set; }
    }
}
