using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeMonitor
    {
        public static ModelViewMonitorOrder Get(int Id)
        {
            return new BusinessMonitor().Get(Id);
        }
        public static ModelViewMonitorOrder GetByOrderID(int Id)
        {
            return new BusinessMonitor().GetByOrderID(Id);
        }
        
        public static List<ModelViewMonitorOrder> GetActives()
        {
            return new BusinessMonitor().GetActives();
        }

        public static List<ModelViewMonitorOrder> GetAll()
        {
            return new BusinessMonitor().GetAll();
        }
        public static List<ModelViewODS> GetListVisitAll(string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate,string User )
        {
            return new BusinessMonitor().GetListVisitAll(StatusVisitID, ModuleID, PriorityID, StatusOrderID, ServiceID, OrderID, Employee, StartDate, EndDate, User);
        }
        
        public static List<ModelViewQuotation> GetListAll(string StatusVisitID,string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, string User,string TypeQuotation)
        {
            return new BusinessMonitor().GetListAllQuotation(StatusVisitID,ModuleID, PriorityID, StatusOrderID, ServiceID, OrderID, Employee, StartDate, EndDate,User,TypeQuotation);
        }
        
        public ModelViewMonitorOrder Insert(ModelViewMonitorOrder data)
        {
            return new BusinessMonitor().Insert(data);
        }

        public ModelViewMonitorOrder Update(ModelViewMonitorOrder data)
        {
            return new BusinessMonitor().Update(data);
        }

        public static List<ModelViewODS> GetList(string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, entities.Entity.Security.EntityUser User)
        {
            return new BusinessMonitor().GetList(StatusVisitID, ModuleID, PriorityID, StatusOrderID, ServiceID, OrderID, Employee, StartDate, EndDate, User);
        }
    }
}
