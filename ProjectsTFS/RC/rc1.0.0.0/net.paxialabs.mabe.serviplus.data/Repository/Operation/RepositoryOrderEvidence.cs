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
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{
    public class RepositoryOrderEvidence : BaseRepository, IRepositoryGET<EntityOrderEvidence>, IRepositorySET<EntityOrderEvidence>
    {
        public EntityOrderEvidence Get(int Id)
        {
            var data = base.DataContext.Evidence.Where(p => p.PK_EvidenceID == Id);
            if (data.Count() == 1)
                return FactoryOrderEvidence.Get(data.Single());
            else
                return null;
        }
        public EntityOrderEvidence GetByOrderID(int Id)
        {
            var data = base.DataContext.Evidence.Where(p => p.FK_OrderID == Id);
            if (data.Count() == 1)
                return FactoryOrderEvidence.Get(data.Single());
            else
                return null;
        }

        public List<EntityOrderEvidence> GetActives()
        {
            return FactoryOrderEvidence.GetList(base.DataContext.Evidence.Where(p => p.Status == true).ToList());
        }

        public List<EntityOrderEvidence> GetAll()
        {
            return FactoryOrderEvidence.GetList(base.DataContext.Evidence.ToList());
        }

        public List<EntityOrderEvidence> GetEvidence(int MonitorOrdersID, string type)
        {
            return FactoryOrderEvidence.GetList(base.DataContext.Evidence.Where(p => p.FK_MonitorOrdersID == MonitorOrdersID && p.TypeEvidence == type).ToList());
        }

        public EntityOrderEvidence Insert(EntityOrderEvidence data)
        {
            try
            {
                Evidence dataNew = new Evidence()
                {
                    PK_EvidenceID = data.EvidenceID,
                    FK_MonitorOrdersID = data.MonitorOrdersID,
                    FK_OrderID = data.OrderID,
                    TypeEvidence = data.TypeEvidence,
                    URLEvidence = data.URLEvidence,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Evidence.Add(dataNew);
                base.DataContext.SaveChanges();

                data.EvidenceID = dataNew.PK_EvidenceID;

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

        public EntityOrderEvidence Update(EntityOrderEvidence data)
        {
            try
            {
                var dataUpdate = base.DataContext.Evidence.Where(p => p.PK_EvidenceID == data.EvidenceID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.PK_EvidenceID = data.EvidenceID;
                    dataUpdate.FK_MonitorOrdersID = data.MonitorOrdersID;
                    dataUpdate.FK_OrderID = data.OrderID;
                    dataUpdate.TypeEvidence = data.TypeEvidence;
                    dataUpdate.URLEvidence = data.URLEvidence;        
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

        public List<ModelViewEvidence> GetList(string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, entities.Entity.Security.EntityUser User)
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

            return base.DataContext.MonitorOrders.Where(predicate).Select(q => new ModelViewEvidence()
            {
               
                OrderID = q.Orders.OrderID,
                EmployeeName = q.Orders.Employee.FirstName + " " + q.Orders.Employee.LastName,
                Date = SqlFunctions.DateName("day", q.Orders.OrderExecuteDate) + "/" + SqlFunctions.DateName("month", q.Orders.OrderExecuteDate) + "/" + SqlFunctions.DateName("year", q.Orders.OrderExecuteDate),
                EvidenceStartODS = q.Evidence.Where(p => p.TypeEvidence == "StartODS").Select(p => new EvidenceOrder() { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                EvidenceBadUse = q.Evidence.Where(p => p.TypeEvidence == "BadUse").Select(p => new EvidenceOrder() { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                EvidenceNoSeriesNumber = q.Evidence.Where(p => p.TypeEvidence == "NoSeriesNumber").Select(p => new EvidenceOrder() { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                EvidencePurchaseNote = q.Evidence.Where(p => p.TypeEvidence == "PurchaseNote").Select(p => new EvidenceOrder() { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                EvidenceSummary = q.Evidence.Where(p => p.TypeEvidence == "Summary").Select(p => new EvidenceOrder() { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                EvidenceFinish = q.Evidence.Where(p => p.TypeEvidence == "Finish").Select(p => new EvidenceOrder() { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList()
                
            }).ToList();
        }
        
        public bool Exists(string OrderID, string TypeEvidence, string FileName)
        {
            return base.DataContext.Evidence.Where(p => p.Orders.OrderID == OrderID && p.TypeEvidence == TypeEvidence && p.URLEvidence.Equals(FileName, StringComparison.OrdinalIgnoreCase)).Count() > 1;
        }
    }
}
