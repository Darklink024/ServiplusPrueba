using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessEmployee
    {

        public EntityEmployee GetID(int EmployeeID)
        {
            return new RepositoryEmployee().Get(EmployeeID);
        }
        public List<EntityEmployee> GetEmployeeByUserID(int UserID)
        {
            return new RepositoryEmployee().GetEByUserID(UserID);
        }
        public List<EntityEmployee> GetByUserID(int UserID)
        {
            return new RepositoryEmployee().GetByUserID(UserID);
        }

        public List<EntityEmployee> GetEmployeUser(int UserID)
        {
            return new RepositoryEmployee().GetEmployeUser(UserID);
        }

        public EntityEmployee GetByUserID(int UserID, int ModuleID)
        {            
            return new RepositoryEmployee().GetByUserID(UserID, ModuleID);
        }

        public List<EntityEmployeeStore> GetEmployeeStore(int UserID)
        {
            var result = new List<EntityEmployeeStore>();

            foreach (var item in new RepositoryEmployee().GetByUserID(UserID))
            {
                result.Add(new EntityEmployeeStore() {
                    EmployeeID = item.EmployeeID,
                    InventoryModuleID = new BusinessModuleService().Get(item.FK_ModuleID).ID, 
                    InventoryStoreID = item.StoreProp
                });
            }            
            return result;
        }

        public EntityEmployee GetEmployeeID(string EmployeeID)
        {
            return new RepositoryEmployee().GetEmployeeID(EmployeeID);
        }

        public List<EntityEmployee> GetAll()
        {
            return new RepositoryEmployee().GetAll().Select(p => new EntityEmployee()
            {
                PK_EmployeeID = p.PK_EmployeeID,
                FK_ModuleID = p.FK_ModuleID,
                FK_UserID = p.FK_UserID,
                EmployeeID = p.EmployeeID,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Interlocutor = p.Interlocutor,
                Society = p.Society,
                EmployeeType = p.EmployeeType,
                StoreProp = p.StoreProp,
                DifferentiatorWorkshop = p.DifferentiatorWorkshop,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityEmployee>();
        }


        public EntityEmployee Insert(EntityEmployee model)
        {
            var data = new RepositoryEmployee().Insert(model);
            return data;
        }

        public EntityEmployee Update(EntityEmployee model)
        {
            var data = new RepositoryEmployee().Update(model);
            return data;
        }


    }
}
