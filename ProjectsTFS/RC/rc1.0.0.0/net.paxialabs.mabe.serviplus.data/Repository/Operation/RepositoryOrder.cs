using net.paxialabs.mabe.serviplus.data.Factory.Operation;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{
    public  class RepositoryOrder : BaseRepository, IRepositoryGET<EntityOrder>, IRepositorySET<EntityOrder>
    {
        public EntityOrder Get(int Id)
        {
            var data = base.DataContext.Orders.Where(p => p.PK_OrderID == Id);
            if (data.Count() == 1)
                return FactoryOrder.Get(data.Single());
            else
                return null;
        }

        public List<int> GetListByProductID(int UserID, DateTime fh)
        {
            DateTime fhInicio = new DateTime(fh.Year, fh.Month, fh.Day, 0, 0, 0);
            DateTime fhFin = new DateTime(fh.Year, fh.Month, fh.Day, 23, 59, 59);
            List<int> arrprod = base.DataContext.Orders.Where(p => p.Employee.FK_UserID == UserID && p.OrderExecuteDate >= fhInicio && p.OrderExecuteDate <= fhFin).Select(p => p.FK_InstalledBaseID).ToList<int>();
            var arr = base.DataContext.InstalledBase.Where(p => arrprod.Contains(p.PK_InstalledBaseID)).Select(p => p.FK_ProductID.Value).Distinct().ToList<int>();
          
            return arr;
        }

        public List<EntityOrder> GetListByUser(int UserID)
        {
            return base.DataContext.Orders.Where(p => p.Employee.FK_UserID == UserID).Select(p => new EntityOrder()
            {
                PK_OrderID = p.PK_OrderID,
                FK_InstalledBaseID = p.FK_InstalledBaseID,
                FK_ClientID = p.FK_ClientID,
                FK_EmployeeID = p.FK_EmployeeID,
                FK_ModuleID = p.FK_ModuleID,
                FK_GuarantyID = p.FK_GuarantyID,
                FK_StatusSchemeID = p.FK_StatusSchemeID,
                OrderID = p.OrderID,
                TechnicalID = p.TechnicalID,
                PreOrder = p.PreOrder,
                URLPreOrder = p.URLPreOrder,
                Symptom = p.Symptom,
                Failure1 = p.Failure1,
                Failure2 = p.Failure2,
                Failure3 = p.Failure3,
                Failure4 = p.Failure4,
                Failure5 = p.Failure5,
                Note = p.Note,
                OrderFahter = p.OrderFahter,
                SendCRM = p.SendCRM,
                Status = p.Status,
                OrderCreateDate = p.OrderCreateDate,
                OrderExecuteDate = p.OrderExecuteDate,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList();
        }

        public List<EntityOrder> GetActives()
        {
            return FactoryOrder.GetList(base.DataContext.Orders.Where(p => p.Status == true).ToList());
        }

        public List<EntityOrder> GetAll(string SendCRM, DateTime syncDate)
        {
            return FactoryOrder.GetList(base.DataContext.Orders.Where(p => p.SendCRM == SendCRM & p.FK_StatusSchemeID != 44 & p.OrderExecuteDate >= syncDate).ToList());
        }

        public List<EntityOrder> GetAll()
        {
            return FactoryOrder.GetList(base.DataContext.Orders.ToList());
        }

        public List<EntityOrder> GetByRange(DateTime Inicio, DateTime Fin)
        {
            return FactoryOrder.GetList(base.DataContext.Orders.Where(p => DbFunctions.TruncateTime(p.OrderExecuteDate) >= Inicio && DbFunctions.TruncateTime(p.OrderExecuteDate) <= Fin).ToList());
        }

        public List<EntityOrder> GetAll(List<int> EmployeeID, DateTime fhExecute, bool PreODS)
        {
            return FactoryOrder.GetList(base.DataContext.Orders.Where(p => EmployeeID.Contains(p.FK_EmployeeID.Value) && DbFunctions.TruncateTime(p.OrderExecuteDate) == fhExecute && p.PreOrder == PreODS).ToList());
        }

        public List<EntityOrder> GetByEmployee(int  EmployeeID, DateTime fhExecute)
        {
            return FactoryOrder.GetList(base.DataContext.Orders.Where(p=>p.FK_EmployeeID== EmployeeID && DbFunctions.TruncateTime(p.OrderExecuteDate) == fhExecute).ToList());
        }
        public List<EntityOrder> GetAllFutures(List<int> EmployeeID, DateTime fhExecute, bool PreODS)
        {
            return FactoryOrder.GetList(base.DataContext.Orders.Where(p => EmployeeID.Contains(p.FK_EmployeeID.Value) && DbFunctions.TruncateTime(p.OrderExecuteDate) >= fhExecute && p.PreOrder == PreODS).ToList());
        }
        public List<EntityOrder> GetAllListODS(List<int> EmployeeID, DateTime fhExecute, DateTime MaxfhExecute, bool PreODS)
        {
            return FactoryOrder.GetList(base.DataContext.Orders.Where(p => EmployeeID.Contains(p.FK_EmployeeID.Value) && DbFunctions.TruncateTime(p.OrderExecuteDate) > fhExecute   && DbFunctions.TruncateTime(p.OrderExecuteDate) <= MaxfhExecute && p.PreOrder == PreODS).ToList());
        }
        
         public List<EntityOrder> GetAllListODSJourney(List<int> EmployeeID, DateTime MinfhExecute, DateTime MaxfhExecute, bool PreODS)
        {
            return FactoryOrder.GetList(base.DataContext.Orders.Where(p => EmployeeID.Contains(p.FK_EmployeeID.Value) && DbFunctions.TruncateTime(p.OrderExecuteDate) >= MinfhExecute && DbFunctions.TruncateTime(p.OrderExecuteDate) <= MaxfhExecute && p.PreOrder == PreODS).ToList());
        }

        public EntityOrder Insert(EntityOrder data)
        {
            try
            {
                Orders dataNew = new Orders()
                {
                    PK_OrderID = data.PK_OrderID,
                    FK_InstalledBaseID = data.FK_InstalledBaseID,
                    FK_ClientID = data.FK_ClientID,
                    FK_EmployeeID = data.FK_EmployeeID,
                    FK_ModuleID = data.FK_ModuleID,
                    FK_GuarantyID = data.FK_GuarantyID,
                    FK_StatusSchemeID = data.FK_StatusSchemeID,
                    OrderID = data.OrderID,
                    TechnicalID = data.TechnicalID,
                    PreOrder = data.PreOrder,
                    URLPreOrder = data.URLPreOrder,
                    Symptom = data.Symptom,
                    Failure1 = data.Failure1,
                    Failure2 = data.Failure2,
                    Failure3 = data.Failure3,
                    Failure4 = data.Failure4,
                    Failure5 = data.Failure5,
                    Note = data.Note,
                    Status = data.Status,
                    OrderFahter = data.OrderFahter,
                    SendCRM = data.SendCRM,
                    OrderCreateDate = data.OrderCreateDate,
                    OrderExecuteDate = data.OrderExecuteDate,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Orders.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_OrderID = dataNew.PK_OrderID;

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

        public EntityOrder Update(EntityOrder data)
        {
            try
            {
                var dataUpdate = base.DataContext.Orders.Where(p => p.PK_OrderID == data.PK_OrderID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    //dataUpdate.PK_OrderID = data.PK_OrderID;
                    //dataUpdate.FK_InstalledBaseID = data.FK_InstalledBaseID;
                    //dataUpdate.FK_ClienteID = data.FK_ClienteID;
                    dataUpdate.FK_EmployeeID = data.FK_EmployeeID;
                    dataUpdate.FK_ModuleID = data.FK_ModuleID;
                    dataUpdate.FK_GuarantyID = data.FK_GuarantyID;
                    dataUpdate.FK_StatusSchemeID = data.FK_StatusSchemeID;
                    //dataUpdate.OrderID = data.OrderID;
                    dataUpdate.TechnicalID = data.TechnicalID;
                    dataUpdate.PreOrder = data.PreOrder;
                    dataUpdate.URLPreOrder = data.URLPreOrder;
                    dataUpdate.Symptom = data.Symptom;
                    dataUpdate.Failure1 = data.Failure1;
                    dataUpdate.Failure2 = data.Failure2;
                    dataUpdate.Failure3 = data.Failure3;
                    dataUpdate.Failure4 = data.Failure4;
                    dataUpdate.Failure5 = data.Failure5;
                    dataUpdate.Note = data.Note;
                    dataUpdate.Status = data.Status;
                    dataUpdate.OrderFahter = data.OrderFahter;
                    dataUpdate.SendCRM = data.SendCRM;
                    dataUpdate.OrderCreateDate = data.OrderCreateDate;
                    dataUpdate.OrderExecuteDate = data.OrderExecuteDate;
                    dataUpdate.CreateDate = data.CreateDate;
                    dataUpdate.ModifyDate = data.ModifyDate;
                    dataUpdate.ExtraKilometres = data.ExtraKilometres;

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
        

        public EntityOrder GetByOrderID(string OrderID)
        {
            var data = base.DataContext.Orders.Where(p => p.OrderID == OrderID);
            if (data.Count() == 1)
                return FactoryOrder.Get(data.Single());
            else
                return null;
        }

        public EntityOrder GetByOrderID(string OrderID, DateTime OrderExecuteDate )
        {
            var data = base.DataContext.Orders.Where(p => p.OrderID == OrderID && DbFunctions.TruncateTime(p.OrderExecuteDate) == OrderExecuteDate);
            if (data.Count() == 1)
                return FactoryOrder.Get(data.Single());
            else
                return null;
        }

    }
}
