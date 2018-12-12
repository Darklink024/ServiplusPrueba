using net.paxialabs.mabe.serviplus.data.Factory.Operation;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{

    public class RepositoryEmployee : BaseRepository, IRepositoryGET<EntityEmployee>, IRepositorySET<EntityEmployee>
    {
        public EntityEmployee Get(int Id)
        {
            var data = base.DataContext.Employee.Where(p => p.PK_EmployeeID == Id);
            if (data.Count() == 1)
                return FactoryEmployee.Get(data.Single());
            else
                return null;
        }
        

        public List<EntityEmployee> GetActives()
        {
            return FactoryEmployee.GetList(base.DataContext.Employee.Where(p => p.Status == true).ToList());
        }

        public List<EntityEmployee> GetAll()
        {
            return FactoryEmployee.GetList(base.DataContext.Employee.ToList());
        }

        public EntityEmployee Insert(EntityEmployee data)
        {
            try
            {
                Employee dataNew = new Employee()
                {
                    PK_EmployeeID = data.PK_EmployeeID,
                    FK_ModuleID = data.FK_ModuleID,
                    FK_UserID = data.FK_UserID,
                    EmployeeID = data.EmployeeID,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Interlocutor = data.Interlocutor,
                    Society = data.Society,
                    EmployeeType = data.EmployeeType,
                    StoreProp = data.StoreProp,
                    DifferentiatorWorkshop = data.DifferentiatorWorkshop,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Employee.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_EmployeeID = dataNew.PK_EmployeeID;

                return data;
            }
            catch (DbException dbex)
            {
                throw dbex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityEmployee Update(EntityEmployee data)
        {
            try
            {
                var dataUpdate = base.DataContext.Employee.Where(p => p.PK_EmployeeID == data.PK_EmployeeID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_EmployeeID = data.PK_EmployeeID;
                    dataUpdate.FK_ModuleID = data.FK_ModuleID;
                    dataUpdate.FK_UserID = data.FK_UserID;
                    dataUpdate.EmployeeID = data.EmployeeID;
                    dataUpdate.FirstName = data.FirstName;
                    dataUpdate.LastName = data.LastName;
                    dataUpdate.Interlocutor = data.Interlocutor;
                    dataUpdate.Society = data.Society;
                    dataUpdate.EmployeeType = data.EmployeeType;
                    dataUpdate.StoreProp = data.StoreProp;
                    dataUpdate.DifferentiatorWorkshop = data.DifferentiatorWorkshop;
                    dataUpdate.Status = data.Status;
                    dataUpdate.CreateDate = data.CreateDate;
                    dataUpdate.ModifyDate = data.ModifyDate;


                    base.DataContext.Entry(dataUpdate).State = EntityState.Modified;
                    base.DataContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el registro en la base de datos a modificar.");
                }

                return data;
            }
            catch (DbException dbex)
            {
                throw dbex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityEmployee GetByUserID(int UserID, int ModuleID)
        {
            var data = base.DataContext.Employee.Where(p => p.FK_UserID == UserID && p.FK_ModuleID == ModuleID);
            if (data.Count() == 1)
                return FactoryEmployee.Get(data.Single());
            else
                return null;
        }

        public EntityEmployee GetEmployeeID(string EmployeeID)
        {
            var data = base.DataContext.Employee.Where(p => p.EmployeeID == EmployeeID);
            if (data.Count() == 1)
                return FactoryEmployee.Get(data.Single());
            else
                return null;
        }

        public List<EntityEmployee> GetEmployeUser(int UserID)
        {
            return FactoryEmployee.GetList(base.DataContext.Employee.Where(p => p.FK_UserID == UserID).ToList());
        }
        public List<EntityEmployee> GetByUserID(int UserID)
        {
            return FactoryEmployee.GetList(base.DataContext.Employee.Where(p => p.FK_UserID == UserID).ToList());
        }
        public List<EntityEmployee> GetEByUserID(int UserID)
        {

            return FactoryEmployee.GetList(base.DataContext.Employee.Where(p => p.FK_UserID == UserID).ToList());
           
        }
    }
}
