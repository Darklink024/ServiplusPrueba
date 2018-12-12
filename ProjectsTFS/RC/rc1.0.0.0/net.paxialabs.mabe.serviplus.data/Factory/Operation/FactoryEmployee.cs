using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{

    internal class FactoryEmployee : BaseFactory<FactoryEmployee, EntityEmployee, Employee>
    {
        public override EntityEmployee _GetEntity(Employee entidad)
        {
            return new EntityEmployee()
            {
                PK_EmployeeID = entidad.PK_EmployeeID,
                FK_ModuleID = entidad.FK_ModuleID,
                FK_UserID = entidad.FK_UserID,
                EmployeeID = entidad.EmployeeID,
                FirstName = entidad.FirstName,
                LastName = entidad.LastName,
                Interlocutor = entidad.Interlocutor,
                Society = entidad.Society,
                EmployeeType = entidad.EmployeeType,
                StoreProp = entidad.StoreProp,
                DifferentiatorWorkshop = entidad.DifferentiatorWorkshop,
                IDPolicy=entidad.IDPolicy,
                IDRefSell=entidad.IDRefSell,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
