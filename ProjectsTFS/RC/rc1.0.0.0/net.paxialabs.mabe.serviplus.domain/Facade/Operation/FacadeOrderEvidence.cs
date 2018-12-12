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
    public class FacadeOrderEvidence
    {
        public static EntityOrderEvidence Get(int Id)
        {
            return new BusinessOrderEvidence().Get(Id);
        }
       
        public static EntityOrderEvidence GetByordeID(int Id)
        {
            return new BusinessOrderEvidence().GetByeOrderID(Id);
        }
        public static List<EntityOrderEvidence> GetActives()
        {
            return new BusinessOrderEvidence().GetActives();
        }
        public static List<EntityOrderEvidence> GetAll()
        {
            return new BusinessOrderEvidence().GetAll();
        }

        public static EntityOrderEvidence Insert(EntityOrderEvidence data)
        {
            return new BusinessOrderEvidence().Insert(data);
        }
        public static EntityOrderEvidence Update(EntityOrderEvidence data)
        {
            return new BusinessOrderEvidence().Update(data);
        }
        public static void RegisterEvidence(ModelViewOrderEvidenceUpload data)
        {
            new BusinessOrderEvidence().RegisterEvidence(data);
        }
        public static List<ModelViewEvidence> GetLisEvidencetAll(string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, entities.Entity.Security.EntityUser User)
        {
            return new BusinessOrderEvidence().GetList(StatusVisitID, ModuleID, PriorityID, StatusOrderID, ServiceID, OrderID, Employee, StartDate, EndDate, User);
        }
    }
}
