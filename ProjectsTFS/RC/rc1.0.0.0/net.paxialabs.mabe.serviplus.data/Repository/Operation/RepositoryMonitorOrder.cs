using LinqKit;
using net.paxialabs.mabe.serviplus.data.Factory.Operation;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{
    public class RepositoryMonitorOrder : BaseRepository, IRepositoryGET<EntityMonitorOrder>, IRepositorySET<EntityMonitorOrder>
    {
        public EntityMonitorOrder Get(int Id)
        {
            var data = base.DataContext.MonitorOrders.Where(p => p.PK_MonitorOrdersID == Id);
            if (data.Count() == 1)
                return FactoryMonitorOrder.Get(data.Single());
            else
                return null;
        }
        public EntityMonitorOrder GetByOrderID(int Id)
        {
            var data = base.DataContext.MonitorOrders.Where(p => p.FK_OrderID == Id);
            if (data.Count() == 1)
                return FactoryMonitorOrder.Get(data.Single());
            else
                return null;
        }
   

        public EntityMonitorOrder GetByOrder(string OrderID, DateTime fh)
        {
            var fhInicio = new DateTime(fh.Year, fh.Month, fh.Day, 0, 0, 0);
            var fhFin = new DateTime(fh.Year, fh.Month, fh.Day, 23, 59, 59);
            var data = base.DataContext.MonitorOrders.Where(p => p.Orders.OrderID == OrderID && p.Orders.OrderExecuteDate >= fhInicio && p.Orders.OrderExecuteDate <= fhFin);
            if (data.Count() == 1)
                return FactoryMonitorOrder.Get(data.Single());
            else
                return null;
        }
              

        public List<ModelViewODS> GetList(string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, entities.Entity.Security.EntityUser User)
        {
            var predicate = PredicateBuilder.New<MonitorOrders>();

            if (StatusVisitID.Trim().Length > 0)
            {
                List<int> iStatusVisitID = StatusVisitID.Split(',').Select(int.Parse).ToList();
                if (iStatusVisitID.Count() > 0) predicate = predicate.And(i => iStatusVisitID.Contains(i.FK_StatusVisitID.Value));
            }

            if (ModuleID.Trim().Length > 0)
            {
                List<int> iModuleID = ModuleID.Split(',').Select(int.Parse).ToList();
                if (iModuleID.Count() > 0) predicate = predicate.And(i => iModuleID.Contains(i.Orders.FK_ModuleID.Value));
            }

            if (PriorityID.Trim().Length > 0)
            {
                List<int> iPriorityID = PriorityID.Split(',').Select(int.Parse).ToList();
                if (iPriorityID.Count() > 0) predicate = predicate.And(i => iPriorityID.Contains(i.SequenceVisit.Value));
            }

            if (StatusOrderID.Trim().Length > 0)
            {
                List<int> iStatusOrderID = StatusOrderID.Split(',').Select(int.Parse).ToList();
                if (iStatusOrderID.Count() > 0) predicate = predicate.And(i => iStatusOrderID.Contains(i.FK_StatusOrderID.Value));
            }

            if (ServiceID.Trim().Length > 0)
            {
                List<int> iServiceID = ServiceID.Split(',').Select(int.Parse).ToList();
                if (iServiceID.Count() > 0) predicate = predicate.And(i => iServiceID.Contains(i.Orders.FK_GuarantyID.Value));
            }
           
            if (OrderID.Trim().Length > 0) predicate = predicate.And(i => i.Orders.OrderID.Contains(OrderID));

            if (Employee.Trim().Length > 0) predicate = predicate.And(i => (i.Orders.Employee.FirstName + " " + i.Orders.Employee.LastName).Contains(Employee));

            DateTime fhIni = DateTime.Today;
            if (DateTime.TryParse(StartDate, out fhIni)) predicate = predicate.And(i => i.Orders.OrderExecuteDate >= fhIni);

            DateTime fhFin = DateTime.Today;
            if (DateTime.TryParse(EndDate, out fhFin))
            {
                fhFin = fhFin.AddHours(23).AddMinutes(59).AddSeconds(59);
                predicate = predicate.And(i => i.Orders.OrderExecuteDate <= fhFin);
            }

            if(User.ModuleID.HasValue)
            {
                predicate = predicate.And(i => i.Orders.FK_ModuleID == User.ModuleID.Value);
            }

            base.DataContext.Database.CommandTimeout = 1600;

            return base.DataContext.MonitorOrders.Where(predicate).Select(q => new ModelViewODS()
            {
                MonitorID = q.PK_MonitorOrdersID,
                StatusOrderID = q.FK_StatusOrderID.Value,
                StatusVisitID = q.FK_StatusVisitID.Value,
                ModuleID = q.Orders.FK_ModuleID.Value,
                StatusODS = q.CauseOrder.StatusOrder.StatusOrder1,
                CauseOrder = q.CauseOrder.CauseOrder1,
                Module = q.Orders.Employee.ModuleMabe.ModuleID + " " + q.Orders.Employee.ModuleMabe.Base,
                OrderID = q.Orders.OrderID,
                EmployeeName = q.Orders.Employee.FirstName + " " + q.Orders.Employee.LastName,
                ServiceType = q.Orders.Guaranty.GuarantyID + " " + q.Orders.Guaranty.Guaranty1,
                StatusVisit = q.StatusVisit.StatusVisit1,
                StartVisitODS = q.StartVisitDate.ToString(),
                StartTryODS = q.StartServiceDate.ToString(),
                StartRunODS = q.StartOrderDate.ToString(),
                EndVisitODS = q.EndVisitDate.ToString(),
                EndTryODS = q.EndServiceDate.ToString(),
                EndRunODS = q.EndOrderDate.ToString(),
                LatitudeStartVisit = q.LatitudeStartVisit.HasValue ? q.LatitudeStartVisit.Value.ToString() : "",
                LogitudeStartVisit = q.LogitudeStartVisit.HasValue ? q.LogitudeStartVisit.Value.ToString() : "",
                LatitudeEndVisit = q.LatitudeEndVisit.HasValue ? q.LatitudeEndVisit.Value.ToString() : "",
                LogitudeEndVisit = q.LogitudeEndVisit.HasValue ? q.LogitudeEndVisit.Value.ToString() : "",
                LatitudeStartOrder = q.LatitudeStartOrder.HasValue ? q.LatitudeStartOrder.Value.ToString() : "",
                LogitudeStartOrder = q.LogitudeStartOrder.HasValue ? q.LogitudeStartOrder.Value.ToString() : "",
                LatitudeEndOrder = q.LatitudeEndOrder.HasValue ? q.LatitudeEndOrder.Value.ToString() : "",
                LogitudeEndOrder = q.LogitudeEndOrder.HasValue ? q.LogitudeEndOrder.Value.ToString() : "",
                DurationVisit = q.DurationVisit.HasValue ? q.DurationVisit.Value.ToString() : "",
                DurationExecute = q.DurationExecute.HasValue ? q.DurationExecute.Value.ToString() : "",
                Notes = q.NoteOrder,
                URL = q.Orders.URLPreOrder,
                SendCRM = q.Orders.SendCRM,
                OrderExecuteDate = q.Orders.OrderExecuteDate,
                Invoice = q.Orders.Invoice.Count() > 0 ? "Si" : "No",
                Evidence = q.Evidence.Where(p => p.TypeEvidence == "StartODS").Select(p => new EvidenceOrder() { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).Take(3).ToList(),
                DurationOrder = ""
            }).ToList();
        }

        public List<EntityMonitorOrder> GetActives()
        {
            return FactoryMonitorOrder.GetList(base.DataContext.MonitorOrders.Where(p => p.Status == true).ToList());
        }

        public List<EntityMonitorOrder> GetAll()
        {
            return FactoryMonitorOrder.GetList(base.DataContext.MonitorOrders.ToList());
        }

        public List<EntityMonitorOrder> GeListByOrder(List<int> OrderIDs)
        {
            return FactoryMonitorOrder.GetList(base.DataContext.MonitorOrders.Where(p=> OrderIDs.Contains(p.FK_OrderID)).ToList());
        }

        public List<EntityMonitorOrder> GeListByOrderAverage(List<int> OrderIDs)
        {
            return FactoryMonitorOrder.GetList(base.DataContext.MonitorOrders.Where(p => OrderIDs.Contains(p.FK_OrderID) && p.FK_StatusOrderID == 4).ToList());
        }

        public EntityMonitorOrder Insert(EntityMonitorOrder data)
        {
            try
            {
                MonitorOrders dataNew = new MonitorOrders()
                {
                    PK_MonitorOrdersID = data.VisitID,
                    FK_OrderID = data.OrderID,
                    FK_StatusVisitID = data.StatusVisitID,
                    FK_StatusOrderID = data.StatusOrderID,
                    FK_CauseOrderID = data.CauseOrderID,
                    SequenceVisit = data.SequenceVisit,

                    StartVisitDate = data.StartVisitDate,
                    EndVisitDate = data.EndVisitDate,
                    StartOrderDate = data.StartOrderDate,
                    EndOrderDate = data.EndOrderDate,
                    StartServiceDate = data.StartServiceDate,
                    EndServiceDate = data.EndServiceDate,
                    LatitudeStartVisit = data.LatitudeStartVisit,
                    LogitudeStartVisit = data.LogitudeStartVisit,
                    LatitudeEndVisit = data.LatitudeEndVisit,
                    LogitudeEndVisit = data.LogitudeEndVisit,
                    LatitudeStartOrder = data.LatitudeStartOrder,
                    LogitudeStartOrder = data.LogitudeStartOrder,
                    LatitudeEndOrder = data.LatitudeEndOrder,
                    LogitudeEndOrder = data.LogitudeEndOrder,

                    DurationVisit = data.DurationVisit,
                    DurationOrder = data.DurationOrder,
                    DurationExecute = data.DurationExecute,
                    DurationTransport = data.DurationTransport,

                    NoteVisit = data.NoteVisit,
                    NoteOrder = data.NoteOrder,

                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.MonitorOrders.Add(dataNew);
                base.DataContext.SaveChanges();

                data.VisitID = dataNew.PK_MonitorOrdersID;

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

        public EntityMonitorOrder Update(EntityMonitorOrder data)
        {
            try
            {
                var dataUpdate = base.DataContext.MonitorOrders.Where(p => p.PK_MonitorOrdersID == data.VisitID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.PK_MonitorOrdersID = data.VisitID;
                    dataUpdate.FK_OrderID = data.OrderID;
                    dataUpdate.FK_StatusVisitID = data.StatusVisitID;
                    dataUpdate.FK_StatusOrderID = data.StatusOrderID;
                    dataUpdate.FK_CauseOrderID = data.CauseOrderID;
                    dataUpdate.SequenceVisit = data.SequenceVisit;

                    dataUpdate.StartVisitDate = data.StartVisitDate;
                    dataUpdate.EndVisitDate = data.EndVisitDate;
                    dataUpdate.StartOrderDate = data.StartOrderDate;
                    dataUpdate.EndOrderDate = data.EndOrderDate;
                    dataUpdate.StartServiceDate = data.StartServiceDate;
                    dataUpdate.EndServiceDate = data.EndServiceDate;
                    dataUpdate.LatitudeStartVisit = data.LatitudeStartVisit;
                    dataUpdate.LogitudeStartVisit = data.LogitudeStartVisit;
                    dataUpdate.LatitudeEndVisit = data.LatitudeEndVisit;
                    dataUpdate.LogitudeEndVisit = data.LogitudeEndVisit;
                    dataUpdate.LatitudeStartOrder = data.LatitudeStartOrder;
                    dataUpdate.LogitudeStartOrder = data.LogitudeStartOrder;
                    dataUpdate.LatitudeEndOrder = data.LatitudeEndOrder;
                    dataUpdate.LogitudeEndOrder = data.LogitudeEndOrder;
                    dataUpdate.DurationExecute = data.DurationExecute;
                    dataUpdate.DurationVisit = data.DurationVisit; //new TimeSpan(int.Parse(data.DurationVisit)); //data.DurationVisit
                    dataUpdate.DurationOrder = data.DurationOrder; //new TimeSpan(int.Parse(data.DurationOrder)); //data.DurationOrder

                    dataUpdate.DurationTransport = data.DurationTransport; //new TimeSpan(0, int.Parse(data.DurationTransport), 0); //data.DurationTransport

                    dataUpdate.NoteVisit = data.NoteVisit;
                    dataUpdate.NoteOrder = data.NoteOrder;

                    dataUpdate.Status = data.Status;
                    dataUpdate.CreateDate = data.CreateDate;
                    dataUpdate.ModifyDate = data.ModifyDate;

                    dataUpdate.LogitudeAddress = data.LogitudeAddress;
                    dataUpdate.LatitudeAddress = data.LatitudeAddress;
                    dataUpdate.ExtraKilometrer = data.ExtraKilometrer;
                    
                    
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

        public List<ModelViewMonitorOrder> GetListAddressWithOutLocation(DateTime? fhValidate)
        {
            var predicate = PredicateBuilder.New<MonitorOrders>();

            if (fhValidate.HasValue) predicate = predicate.And(i => i.Orders.OrderExecuteDate >= fhValidate.Value);

            predicate = predicate.And(i => i.Orders.FK_StatusSchemeID == 44);
            predicate = predicate.And(i => i.LatitudeAddress == null);

            return base.DataContext.MonitorOrders.Where(predicate).Select(p => new ModelViewMonitorOrder()
            {
                VisitID = p.PK_MonitorOrdersID,
                OrderID = p.FK_OrderID,
                CauseOrderID = p.FK_CauseOrderID,
                CreateDate = p.CreateDate,
                DurationExecute = p.DurationExecute,
                DurationOrder = p.DurationOrder,
                DurationTransport = p.DurationTransport,
                DurationVisit = p.DurationVisit,
                EndOrderDate = p.EndOrderDate,
                EndServiceDate = p.EndServiceDate,
                EndVisitDate = p.EndVisitDate,
                LatitudeAddress = p.LatitudeAddress,
                LogitudeAddress = p.LogitudeAddress,
                LatitudeEndOrder = p.LatitudeEndOrder,
                LatitudeEndVisit = p.LatitudeEndVisit,
                LatitudeStartOrder = p.LatitudeStartOrder,
                LatitudeStartVisit = p.LatitudeStartVisit,
                LogitudeEndOrder = p.LogitudeEndOrder,
                LogitudeEndVisit = p.LogitudeEndVisit,
                LogitudeStartOrder = p.LogitudeStartOrder,
                LogitudeStartVisit = p.LogitudeStartVisit,
                ModifyDate = p.ModifyDate,
                NoteOrder = p.NoteOrder,
                NoteVisit = p.NoteVisit,
                SequenceVisit = p.SequenceVisit,
                StartOrderDate = p.StartOrderDate,
                StartServiceDate = p.StartServiceDate,
                StartVisitDate = p.StartVisitDate,
                Status = p.Status,
                StatusOrderID = p.FK_StatusOrderID,
                StatusVisitID = p.FK_StatusVisitID,
                ExtraKilometrer = p.ExtraKilometrer
            }).ToList();
        }
        public List<EntityMonitorOrder> GetByEmployeeID(int EmployeedID, DateTime fh)
        {
            DateTime fhInico = new DateTime(fh.Year, fh.Month, fh.Day, 0, 0, 0);
            DateTime fhFin = new DateTime(fh.Year, fh.Month, fh.Day, 23, 59, 59);
            return FactoryMonitorOrder.GetList((from p in base.DataContext.MonitorOrders
                                                where p.Orders.FK_EmployeeID == EmployeedID
                                                                                && p.Orders.OrderExecuteDate >= fhInico
                                                                                && p.Orders.OrderExecuteDate <= fhFin
                                                select p).ToList());
        }

        public List<EntityMonitorOrder> GetByUserID(int UserID, DateTime fh)
        {
            DateTime fhInico = new DateTime(fh.Year, fh.Month, fh.Day, 0, 0, 0);
            DateTime fhFin = new DateTime(fh.Year, fh.Month, fh.Day, 23, 59, 59);
            return FactoryMonitorOrder.GetList((from p in base.DataContext.MonitorOrders
                                                where p.Orders.Employee.FK_UserID == UserID
                                                                                && p.Orders.OrderExecuteDate >= fhInico
                                                                                && p.Orders.OrderExecuteDate <= fhFin
                                                select p).ToList());
        }
    }
}
