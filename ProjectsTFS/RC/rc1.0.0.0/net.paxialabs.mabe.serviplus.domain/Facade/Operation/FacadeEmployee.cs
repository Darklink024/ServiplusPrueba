using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public static class FacadeEmployee
    {
        public static EntityEmployee GetID(int EmployeeID)
        {
            return new BusinessEmployee().GetID(EmployeeID);
        }
        public static List<EntityEmployee> GetEmployeeByUser(int EmployeeID)
        {
            return new BusinessEmployee().GetEmployeeByUserID(EmployeeID);
        }
    }
}
