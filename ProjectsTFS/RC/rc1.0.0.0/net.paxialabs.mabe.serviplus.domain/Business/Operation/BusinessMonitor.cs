using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.security;
using net.paxialabs.mabe.serviplus.security.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessMonitor
    {
        public ModelViewMonitorOrder Get(int Id)
        {
            var p = new RepositoryMonitorOrder().Get(Id);
            return new ModelViewMonitorOrder() {
                VisitID = p.VisitID,
                OrderID = p.OrderID,
                CauseOrderID = p.CauseOrderID,
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
                StatusOrderID = p.StatusOrderID,
                StatusVisitID = p.StatusVisitID,
                ExtraKilometrer = p.ExtraKilometrer
            };
        }

        public ModelViewMonitorOrder GetByOrderID(int Id)
        {
            var p = new RepositoryMonitorOrder().GetByOrderID(Id);


            return new ModelViewMonitorOrder()
            {
                VisitID = p.VisitID,
                OrderID = p.OrderID,
                CauseOrderID = p.CauseOrderID,
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
                StatusOrderID = p.StatusOrderID,
                StatusVisitID = p.StatusVisitID,
                ExtraKilometrer = p.ExtraKilometrer
            };
        }

        public ModelViewMonitorOrder GetByOrder(string OrderID, DateTime fh)
        {
            var p = new RepositoryMonitorOrder().GetByOrder(OrderID, fh);


            return new ModelViewMonitorOrder()
            {
                VisitID = p.VisitID,
                OrderID = p.OrderID,
                CauseOrderID = p.CauseOrderID,
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
                StatusOrderID = p.StatusOrderID,
                StatusVisitID = p.StatusVisitID,
                ExtraKilometrer = p.ExtraKilometrer
            };
        }

        

        public List<EntityMonitorOrder> GetByEmployeeID(int EmployeedID, DateTime fh)
        {
            return new RepositoryMonitorOrder().GetByEmployeeID(EmployeedID, fh);
        }

        public List<EntityMonitorOrder> GetByUserID(int EmployeedID, DateTime fh)
        {
            return new RepositoryMonitorOrder().GetByUserID(EmployeedID, fh);
        }

        public List<ModelViewMonitorOrder> GetActives()
        {
            return new RepositoryMonitorOrder().GetActives().Select(p => new ModelViewMonitorOrder() {
                VisitID = p.VisitID,
                OrderID = p.OrderID,
                CauseOrderID = p.CauseOrderID,
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
                StatusOrderID = p.StatusOrderID,
                StatusVisitID = p.StatusVisitID,
                ExtraKilometrer = p.ExtraKilometrer
            }).ToList<ModelViewMonitorOrder>();
        }

        public List<ModelViewMonitorOrder> GetListAddressWithOutLocation(DateTime fhValidate)
        {
            return new RepositoryMonitorOrder().GetListAddressWithOutLocation(fhValidate);
        }


        public List<ModelViewMonitorOrder> GetAll()
        {
            return new RepositoryMonitorOrder().GetAll().Select(p => new ModelViewMonitorOrder()
            {
                VisitID = p.VisitID,
                OrderID = p.OrderID,
                CauseOrderID = p.CauseOrderID,
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
                StatusOrderID = p.StatusOrderID,
                StatusVisitID = p.StatusVisitID,
                ExtraKilometrer=p.ExtraKilometrer
                
            }).ToList<ModelViewMonitorOrder>();
        }

        public List<ModelViewMonitorOrder> GeListByOrder(List<int> OrderIDs)
        {
            return new RepositoryMonitorOrder().GeListByOrder(OrderIDs).Select(p => new ModelViewMonitorOrder()
            {
                VisitID = p.VisitID,
                OrderID = p.OrderID,
                CauseOrderID = p.CauseOrderID,
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
                StatusOrderID = p.StatusOrderID,
                StatusVisitID = p.StatusVisitID,
                ExtraKilometrer = p.ExtraKilometrer
            }).ToList<ModelViewMonitorOrder>();
        }
        

        public List<ModelViewMonitorOrder> GeListByOrderAverage(List<int> OrderIDs)
        {
            return new RepositoryMonitorOrder().GeListByOrderAverage(OrderIDs).Select(p => new ModelViewMonitorOrder()
            {
                VisitID = p.VisitID,
                OrderID = p.OrderID,
                CauseOrderID = p.CauseOrderID,
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
                StatusOrderID = p.StatusOrderID,
                StatusVisitID = p.StatusVisitID,
                ExtraKilometrer = p.ExtraKilometrer
            }).ToList<ModelViewMonitorOrder>();
        }

        public ModelViewMonitorOrder Insert(ModelViewMonitorOrder data)
        {
            EntityMonitorOrder newData = new EntityMonitorOrder() {
                VisitID = data.VisitID,
                OrderID = data.OrderID,
                CauseOrderID = data.CauseOrderID,
                CreateDate = data.CreateDate,
                DurationExecute = data.DurationExecute,
                DurationOrder = data.DurationOrder,
                DurationTransport = data.DurationTransport,
                DurationVisit = data.DurationVisit,
                EndOrderDate = data.EndOrderDate,
                EndServiceDate = data.EndServiceDate,
                EndVisitDate = data.EndVisitDate,
                LogitudeAddress = data.LogitudeAddress,
                LatitudeAddress = data.LatitudeAddress,
                LatitudeEndOrder = data.LatitudeEndOrder,
                LatitudeEndVisit = data.LatitudeEndVisit,
                LatitudeStartOrder = data.LatitudeStartOrder,
                LatitudeStartVisit = data.LatitudeStartVisit,
                LogitudeEndOrder = data.LogitudeEndOrder,
                LogitudeEndVisit = data.LogitudeEndVisit,
                LogitudeStartOrder = data.LogitudeStartOrder,
                LogitudeStartVisit = data.LogitudeStartVisit,
                ModifyDate = data.ModifyDate,
                NoteOrder = data.NoteOrder,
                NoteVisit = data.NoteVisit,
                SequenceVisit = data.SequenceVisit,
                StartOrderDate = data.StartOrderDate,
                StartServiceDate = data.StartServiceDate,
                StartVisitDate = data.StartVisitDate,
                Status = data.Status,
                StatusOrderID = data.StatusOrderID,
                StatusVisitID = data.StatusVisitID,
                ExtraKilometrer = data.ExtraKilometrer
            };
            return (ModelViewMonitorOrder) new RepositoryMonitorOrder().Insert(newData);
        }

        public ModelViewMonitorOrder Update(ModelViewMonitorOrder data)
        {
            EntityMonitorOrder newData = new EntityMonitorOrder()
            {
                VisitID = data.VisitID,
                OrderID = data.OrderID,
                CauseOrderID = data.CauseOrderID,
                CreateDate = data.CreateDate,
                DurationExecute = data.DurationExecute,
                DurationOrder = data.DurationOrder,
                DurationTransport = data.DurationTransport,
                DurationVisit = data.DurationVisit,
                EndOrderDate = data.EndOrderDate,
                EndServiceDate = data.EndServiceDate,
                EndVisitDate = data.EndVisitDate,                
                LatitudeEndOrder = data.LatitudeEndOrder,
                LatitudeEndVisit = data.LatitudeEndVisit,
                LatitudeStartOrder = data.LatitudeStartOrder,
                LatitudeStartVisit = data.LatitudeStartVisit,
                LogitudeEndOrder = data.LogitudeEndOrder,
                LogitudeEndVisit = data.LogitudeEndVisit,
                LogitudeStartOrder = data.LogitudeStartOrder,
                LogitudeStartVisit = data.LogitudeStartVisit,
                ModifyDate = data.ModifyDate,
                NoteOrder = data.NoteOrder,
                NoteVisit = data.NoteVisit,
                SequenceVisit = data.SequenceVisit,
                StartOrderDate = data.StartOrderDate,
                StartServiceDate = data.StartServiceDate,
                StartVisitDate = data.StartVisitDate,
                Status = data.Status,
                StatusOrderID = data.StatusOrderID,
                StatusVisitID = data.StatusVisitID,
                LatitudeAddress = data.LatitudeAddress,
                LogitudeAddress = data.LogitudeAddress,
                ExtraKilometrer = data.ExtraKilometrer
            };
            new RepositoryMonitorOrder().Update(newData);
            return data;
        }

        public List<ModelViewODS> GetList(string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, entities.Entity.Security.EntityUser User)
        {
            return new RepositoryMonitorOrder().GetList(StatusVisitID, ModuleID, PriorityID, StatusOrderID, ServiceID, OrderID, Employee, StartDate, EndDate, User);
        }

        public List<ModelViewODS> GetListVisitAll(string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, string User)
        {
            
            var NegocioUser = new BusinessUsers();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioOrdenes = new BusinessOrder();
            var NegocioEstatusOrden = new BusinessStatusCauseOrder();
            var NegocioGarantia = new BusinessGuaranty();
            var NegocioModulosMabe = new BusinessModuleService();
            var NegocioEvidencia = new BusinessOrderEvidence();
            var NegocioCausaOrder = new BusinessCauseOrder();
            var NegocioEstatusEsquema = new BusinessStatusScheme();
             var estatusorden = NegocioEstatusOrden.GetAll();
            var causaOrder = NegocioCausaOrder.GetAll();
            int profileuser = NegocioUser.GetProfileByToken(User).ProfileID;
            var ModuleUser = NegocioUser.GetProfileByToken(User).ModuleID;
            var ordenes = new List<EntityOrder>();
            
            var user = new List<ModelViewUserList>();
            var garantia = new List<EntityGuaranty>();
            var modulos = new List<EntityModuleService>();
            var estatus = new List<EntityStatusOrder>();
            var empleado = new List<EntityEmployee>();
            var visitas = new List<ModelViewMonitorOrder>();
            List<ModelViewODS> list = new List<ModelViewODS>();


            user = NegocioUser.GetAll();
            

            if (StartDate != "" && EndDate != "")
            {
                DateTime inicio = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime fin = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ordenes = NegocioOrdenes.GetByRange(inicio, fin);
                
                if (OrderID != "")
                { ordenes = ordenes.Where(p => p.OrderID.Contains(OrderID)).ToList(); }
                if (profileuser == 7)
                {
                  
                    ordenes = (from a in ordenes where a.FK_ModuleID == ModuleUser
                               select a).ToList();



                }

                //ordenes = NegocioOrdenes.GetAll().Where(p => p.OrderExecuteDate >= inicio && p.OrderExecuteDate <= fin).ToList();
                //if (OrderID != "")
                //{ ordenes = ordenes.Where(p => p.OrderID.Contains(OrderID)).ToList(); }
            }
            
            else
            { ordenes = NegocioOrdenes.GetAll();}

            if (StatusVisitID != "")
            {
                int[] nums = StatusVisitID.Split(',').Select(int.Parse).ToArray();
                visitas = GetAll().Where(p => nums.Contains(p.StatusVisitID.Value)).ToList();
            }
            else
            { visitas = GeListByOrder(ordenes.Select(p=> p.PK_OrderID).ToList()); }

            var ordenesFactura = (from a in ordenes
                                  join b in new BusinessInvoice().GetAll() on a.PK_OrderID equals b.FK_OrderID into total
                                 from p in total.DefaultIfEmpty()
                                 select new { a, Invoice = p == null ? "No" : "Si" }).ToList();
            var complete = (from a in ordenes
                            join b in visitas on a.PK_OrderID equals b.OrderID
                            join c  in ordenesFactura on a.PK_OrderID equals c.a.PK_OrderID
                            select new { a, b, c.Invoice }).ToList();

            if (Employee != "")
            {
                var fullEmployee = NegocioEmpleado.GetAll().Select(p => new { PK_EmployeeID = p.PK_EmployeeID, Name = p.FirstName.ToLower() + " " + p.LastName.ToLower() }).ToList();
                var EmployeeID = fullEmployee.Where(p => p.Name.Contains(Employee.ToLower())).ToList();
                empleado = NegocioEmpleado.GetAll().Where(p => EmployeeID.Select(a => a.PK_EmployeeID).ToList<int>().Contains(p.PK_EmployeeID) && p.FK_UserID != null).ToList();
            }
            else
            { empleado = NegocioEmpleado.GetAll().Where(p=> p.FK_UserID != null).ToList(); }

            var completeEmpl = (from a in complete
                                join b in empleado
                                on a.a.FK_EmployeeID equals b.PK_EmployeeID
                                select new { a, b }).ToList();

            if (ServiceID != "")
            {
                int[] nums = ServiceID.Split(',').Select(int.Parse).ToArray();
                garantia = NegocioGarantia.GetAll().Where(p => nums.Contains(p.PK_GuarantyID)).ToList();
            }
            else
            { garantia = NegocioGarantia.GetAll(); }

            var completeSer = (from a in completeEmpl
                               join b in garantia
                                on a.a.a.FK_GuarantyID equals b.PK_GuarantyID
                               select new { a, b }).ToList();

            if (ModuleID != "")
            {
                int[] nums = ModuleID.Split(',').Select(int.Parse).ToArray();
                modulos = NegocioModulosMabe.GetAll().Where(p => nums.Contains(p.ModuleID)).ToList();
            }
            else
            { modulos = NegocioModulosMabe.GetAll(); }

            var completeMod = (from a in completeSer
                               join b in modulos
                               on a.a.a.a.FK_ModuleID equals b.ModuleID
                               select new { a, b }).ToList();

            if (StatusOrderID != "")
            {
                int[] nums = StatusOrderID.Split(',').Select(int.Parse).ToArray();
                estatus = NegocioEstatusOrden.GetAll().Where(p => nums.Contains(p.PK_StatusOrderID)).ToList();
            }
            else
            { estatus = NegocioEstatusOrden.GetAll(); }

            var completeEst = (from a in completeMod
                               join b in estatus
                               on a.a.a.a.b.StatusOrderID equals b.PK_StatusOrderID
                               select new { a, b }).ToList();

            //var ordenesemple = (from a in visitas
            //                    join b in estatus on a.StatusOrderID equals b.PK_StatusOrderID
            //                    join c in ordenes on a.OrderID equals c.PK_OrderID
            //                    join d in empleado on c.TechnicalID equals d.EmployeeID
            //                    select new
            //                    {
            //                        EmployeeID = d.PK_EmployeeID,
            //                        Status = b.StatusOrder1,
            //                        StatusID = a.StatusVisitID,
            //                        LatitudeAddress = a.LatitudeAddress ,
            //                        LogitudeAddress = a.LogitudeAddress,
            //                        LatitudeStartVisit = a.LatitudeStartVisit,
            //                        LogitudeStartVisit = a.LogitudeStartVisit,
            //                        LatitudeEndVisit = a.LatitudeEndVisit,
            //                        LogitudeEndVisit = a.LogitudeEndVisit,
            //                        OrderID = c.OrderID,
            //                        StarTime = a.StartVisitDate,
            //                        EndTime = a.EndVisitDate,
            //                        OrderExceute = c.OrderExecuteDate,
            //                        Secuence = a.SequenceVisit
            //                    });

            var completeEstEsq = (from a in completeEst
                                  join b in NegocioEstatusEsquema.GetAll()
                                  on a.a.a.a.a.a.FK_StatusSchemeID equals b.PK_StatusSchemeID
                                  select new { a, b }).ToList();

            
            var lt = completeEstEsq.Select(p => new ModelViewODS()
            {
                MonitorID = p.a.a.a.a.a.b.VisitID,
                ModuleID = p.a.a.b.ModuleID,
                StatusVisitID = p.a.a.a.a.a.b.StatusVisitID.Value,
                Module = p.a.a.b.ID + " - " + p.a.a.b.Base,
                StatusOrderID = p.a.b.PK_StatusOrderID,
                StatusODS = p.a.b.StatusOrder1,
                CauseOrder = p.b.Description,
                OrderID = p.a.a.a.a.a.a.PreOrder == false ? p.a.a.a.a.a.a.OrderID : "PreOrden",
                EmployeeName = p.a.a.a.a.b.FirstName + " " + p.a.a.a.a.b.LastName,
                ServiceType = p.a.a.a.b.GuarantyID + " " + p.a.a.a.b.Guaranty1,
                StartVisitODS = p.a.a.a.a.a.b.StartVisitDate.ToString(),
                StartTryODS = p.a.a.a.a.a.b.StartServiceDate.ToString(),
                StartRunODS = p.a.a.a.a.a.b.StartOrderDate.ToString(),
                EndVisitODS = p.a.a.a.a.a.b.EndVisitDate.ToString(),
                EndTryODS = p.a.a.a.a.a.b.EndServiceDate.ToString(),
                EndRunODS = p.a.a.a.a.a.b.EndOrderDate.ToString(),
                LatitudeStartVisit = p.a.a.a.a.a.b.LatitudeStartVisit.ToString(),
                LogitudeStartVisit = p.a.a.a.a.a.b.LogitudeStartVisit.ToString(),
                LatitudeEndVisit = p.a.a.a.a.a.b.LatitudeEndVisit.ToString(),
                LogitudeEndVisit = p.a.a.a.a.a.b.LogitudeEndVisit.ToString(),
                LatitudeStartOrder = p.a.a.a.a.a.b.LatitudeStartOrder.ToString(),
                LogitudeStartOrder = p.a.a.a.a.a.b.LogitudeStartOrder.ToString(),
                LatitudeEndOrder = p.a.a.a.a.a.b.LatitudeEndOrder.ToString(),
                LogitudeEndOrder = p.a.a.a.a.a.b.LogitudeEndOrder.ToString(),
                DurationVisit = p.a.a.a.a.a.b.DurationVisit.ToString(),
                DurationExecute = p.a.a.a.a.a.b.DurationExecute.ToString(),
                Notes = p.a.a.a.a.a.b.NoteOrder,
                URL = p.a.a.a.a.a.a.URLPreOrder,
                SendCRM = p.a.a.a.a.a.a.SendCRM,
                OrderExecuteDate = p.a.a.a.a.a.a.OrderExecuteDate,
                Invoice = p.a.a.a.a.a.Invoice,
                Evidence = NegocioEvidencia.GetEvidence(p.a.a.a.a.a.b.VisitID, "StartODS").Select(c => new EvidenceOrder
                { TypeEvidence = c.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + c.URLEvidence.Substring(2) }).Take(3).ToList(),
               // Address = ordenesemple.Where(a => a.EmployeeID == p.a.a.a.a.b.PK_EmployeeID && a.OrderExceute.Date == p.a.a.a.a.a.a.OrderExecuteDate.Date).Select(b => new AddressV
               // {
               //     StatusOrderID = b.StatusID.Value,
               //     LatitudeStartVisit = b.LatitudeStartVisit,
               //     LogitudeStartVisit = b.LogitudeStartVisit,
               //     LatitudeEndVisit = b.LatitudeEndVisit,
               //     LogitudeEndVisit = b.LogitudeEndVisit,
               //     LatitudeAddress = b.LatitudeAddress,
               //     LogitudeAddress = b.LogitudeAddress,
               //     Status = b.Status,
               //     OrderID = b.OrderID,
               //     StarTime = b.StarTime.ToString(),
               //     EndTime = b.EndTime.ToString(),
               //     Secuence = b.Secuence.HasValue ? b.Secuence.Value : 0,
               //}).ToList()
           }).OrderByDescending(p => p.OrderExecuteDate).ToList();

            return lt; 
        }

        public List<ModelViewQuotation> GetListAllQuotation1(string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, string User, string TypeQuotation)
        {
            int profileuser = new BusinessUsers().GetProfileByToken(User).ProfileID;

            if (profileuser == 7) ModuleID = new BusinessUsers().GetProfileByToken(User).ModuleID.ToString();

            DateTime fh1 = DateTime.Parse(StartDate);
            DateTime fh2 = DateTime.Parse(EndDate);

            User = "";
            return new RepositoryQuotation().GetList(StatusVisitID, ModuleID, PriorityID, StatusOrderID, ServiceID, OrderID, Employee, fh1.ToString("yyyy-MM-dd"), fh2.ToString("yyyy-MM-dd"), User, TypeQuotation);
        }

        public List<ModelViewQuotation> GetListAllQuotation(string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, string User,string TypeQuotation)
        {
            var NegocioUser = new BusinessUsers();
            var NegocioCiente = new BusinessClient();
            var NegocioCotizacion = new BusinessQuotation();
            var cotiza = new BusinessQuotation();
            var NegocioPolizas = new BusinessPolicy().GetAll();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioMonitor = new BusinessMonitor();
            var NegocioOrdenes = new BusinessOrder();
            var NegocioEstatusOrden = new BusinessStatusCauseOrder();
            var NegocioGarantia = new BusinessGuaranty();
            var NegocioModulosMabe = new BusinessModuleService();
            var NegocioCauseOrder = new BusinessCauseOrder();
            var NegocioTypeQuotation = new BusinessTypeQuotation();
            var NegocioContrat = new BusinessContrat();
            var estatusorden = NegocioEstatusOrden.GetAll();
            int profileuser = NegocioUser.GetProfileByToken(User).ProfileID;
            var ordenes = new List<EntityOrder>();
            var polizas = new List<EntityPolicy>();
            var user = new List<ModelViewUserList>();
            var garantia = new List<EntityGuaranty>();
            var modulos = new List<EntityModuleService>();
            var estatus = new List<EntityStatusOrder>();
            var empleado = new List<EntityEmployee>();
            var visitas = new List<ModelViewMonitorOrder>();
            var tipo = new List<EntityTypeQuotation>();
            var Cotizaciones = new List<EntityQuotation>();
            var contrat = new List<EntityContract>();

            var ModuleUser = NegocioUser.GetProfileByToken(User).ModuleID;

            //contrat = NegocioContrat.GetAll();
            user = NegocioUser.GetAll();

                if (StartDate != "" && EndDate != "")
            {
                DateTime inicio = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime fin = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //ordenes = NegocioOrdenes.GetAll().Where(p => p.OrderExecuteDate >= inicio && p.OrderExecuteDate <= fin).ToList();
                ordenes = NegocioOrdenes.GetByRange(inicio, fin);
                if (OrderID != "")
                { ordenes = NegocioOrdenes.GetAll().Where(p => p.OrderID.Contains(OrderID)).ToList(); }
                if (profileuser == 7)
                {

                    ordenes = (from a in ordenes
                               where a.FK_ModuleID == ModuleUser
                               select a).ToList();
                }
            }
            else
            { ordenes = NegocioOrdenes.GetAll(); }

            if (StatusVisitID != "")
            {
                int[] nums = StatusVisitID.Split(',').Select(int.Parse).ToArray();
                visitas = GetAll().Where(p => nums.Contains(p.StatusVisitID.Value)).ToList();
            }
            else
            { visitas = GeListByOrder(ordenes.Select(p => p.PK_OrderID).ToList()); }

            if (Employee != "")
            {
                empleado = NegocioEmpleado.GetAll().Where(p => p.FirstName.ToLower().Contains(Employee.ToLower()) || p.LastName.ToLower().Contains(Employee.ToLower())).ToList();
            }
            else
            { empleado = NegocioEmpleado.GetAll(); }

            if (ServiceID != "")
            {
                int[] nums = ServiceID.Split(',').Select(int.Parse).ToArray();
                garantia = NegocioGarantia.GetAll().Where(p => nums.Contains(p.PK_GuarantyID)).ToList();
            }
            else
            { garantia = NegocioGarantia.GetAll(); }

            if (ModuleID != "")
            {
                int[] nums = ModuleID.Split(',').Select(int.Parse).ToArray();
                modulos = NegocioModulosMabe.GetAll().Where(p => nums.Contains(p.ModuleID)).ToList();
            }
            else
            { modulos = NegocioModulosMabe.GetAll(); }
            if (StatusOrderID != "")
            {
                int[] nums = StatusOrderID.Split(',').Select(int.Parse).ToArray();
                estatus = NegocioEstatusOrden.GetAll().Where(p => nums.Contains(p.PK_StatusOrderID)).ToList();
            }
            else
            { estatus = NegocioEstatusOrden.GetAll(); }
            if (TypeQuotation != "")
            {

                int[] nums = TypeQuotation.Split(',').Select(int.Parse).ToArray();
                tipo = NegocioTypeQuotation.GetAll().Where(p => nums.Contains(p.PK_TypeQuotation)).ToList();
            }
            else
            { tipo = NegocioTypeQuotation.GetAll(); }

            Cotizaciones = NegocioCotizacion.GeListByOrder(ordenes.Select(p => p.PK_OrderID).ToList());

            //var x = NegocioCotizacion.GetAll().Select(p => p.TypeQuotation).Distinct();
            List<ModelViewQuotation> x = (from c in Cotizaciones
                    join j in tipo on c.TypeQuotation equals j.PK_TypeQuotation
                    join p in ordenes on c.FK_OrdenID equals p.PK_OrderID
                    join e in NegocioCiente.GetAll() on p.FK_ClientID equals e.PK_ClientID
                    join t in empleado on p.TechnicalID equals t.EmployeeID
                    join m in NegocioMonitor.GetAll() on p.PK_OrderID equals m.OrderID
                    join b in estatus on m.StatusOrderID equals b.PK_StatusOrderID
                    join g in garantia on p.FK_GuarantyID equals g.PK_GuarantyID
                    join f in modulos on p.FK_ModuleID equals f.ModuleID
                    

                    join h in NegocioCauseOrder.GetAll() on m.CauseOrderID equals h.PK_CauseOrderID
                    


                    select new ModelViewQuotation()
                    {
                        FK_OrderID=c.FK_OrdenID.Value,
                        QuotationID = c.PK_QuotationID,
                        TypeQuotation = j.Description,
                        OrdenVenta = c.OrdenVenta,
                        OrderID = p.OrderID,
                        StatusQuotation = h.PK_CauseOrderID,
                        Folio = c.Folio,
                        ClientName = e.FirstName + " " + e.LastName,
                        EmployeeName = t.FirstName + " " + t.LastName,
                        Total = "$ " + c.Total,
                        URL = c.URL,
                        
                        


                    }).OrderByDescending(p => p.QuotationID).ToList();
            //var z = (from r in x
            //         join y in contrat on r.FK_OrderID equals y.Fk_OrderID
            //         into ps
            //         from sub in ps.DefaultIfEmpty()

            //         select new ModelViewQuotation()
            //         {
            //             FK_OrderID = r.FK_OrderID,
            //             QuotationID = r.QuotationID,
            //             TypeQuotation = r.TypeQuotation,
            //             OrdenVenta = r.OrdenVenta,
            //             OrderID = r.OrderID,
            //             StatusQuotation = r.StatusQuotation,
            //             Folio = r.Folio,
            //             ClientName = r.ClientName,
            //             EmployeeName = r.EmployeeName,
            //             Total = r.Total,
            //             URL = r.URL,
            //             Contract = sub?.Ruta ?? String.Empty
            //         }).GroupBy(w => new { w.FK_OrderID, w.Folio }).Select(w => w.First()).ToList();






            return x;
            //return z;

        }

        public List<ModelViewEvidence> GetLisEvidencetAll(string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, string User)
        {
            var NegocioUser = new BusinessUsers();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioOrdenes = new BusinessOrder();
            var NegocioEstatusOrden = new BusinessStatusCauseOrder();
            var NegocioGarantia = new BusinessGuaranty();
            var NegocioModulosMabe = new BusinessModuleService();
            var NegocioEvidencia = new BusinessOrderEvidence();
            //var visitas = GetAll();
            var user = new List<ModelViewUserList>();
            var estatusorden = NegocioEstatusOrden.GetAll();
            int profileuser = NegocioUser.GetProfileByToken(User).ProfileID;
            var ordenes = new List<EntityOrder>();
            var garantia = new List<EntityGuaranty>();
            var modulos = new List<EntityModuleService>();
            var estatus = new List<EntityStatusOrder>();
            var empleado = new List<EntityEmployee>();
            var visitas = new List<ModelViewMonitorOrder>();
            user = NegocioUser.GetAll();

            if (StartDate != "" && EndDate != "")
            {
                DateTime inicio = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime fin = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //ordenes = NegocioOrdenes.GetAll().Where(p => p.OrderExecuteDate >= inicio && p.OrderExecuteDate <= fin && p.FK_StatusSchemeID != 44).ToList();
                ordenes = NegocioOrdenes.GetByRange(inicio, fin).Where(p=> p.FK_StatusSchemeID != 44).ToList();
                if (OrderID != "")
                { ordenes = NegocioOrdenes.GetAll().Where(p => p.OrderID.Contains(OrderID)).ToList(); }
                if (profileuser == 7)
                {

                    ordenes = (from a in ordenes
                               join b in user on a.FK_ModuleID equals b.FK_ModuleID
                               where b.ProfileID == profileuser
                               select a).ToList();
                }
            }
            else
            { ordenes = NegocioOrdenes.GetAll(); }

            if (StatusVisitID != "")
            {
                int[] nums = StatusVisitID.Split(',').Select(int.Parse).ToArray();
                visitas = GetAll().Where(p => nums.Contains(p.StatusVisitID.Value)).ToList();
            }
            else
            { visitas = GeListByOrder(ordenes.Select(p => p.PK_OrderID).ToList()); }



            if (Employee != "")
            {
                empleado = NegocioEmpleado.GetAll().Where(p => p.FirstName.ToLower().Contains(Employee.ToLower()) || p.LastName.ToLower().Contains(Employee.ToLower())).ToList();
            }
            else
            { empleado = NegocioEmpleado.GetAll(); }


            if (ServiceID != "")
            {
                int[] nums = ServiceID.Split(',').Select(int.Parse).ToArray();
                garantia = NegocioGarantia.GetAll().Where(p => nums.Contains(p.PK_GuarantyID)).ToList();
            }
            else
            { garantia = NegocioGarantia.GetAll(); }

            if (ModuleID != "")
            {
                int[] nums = ModuleID.Split(',').Select(int.Parse).ToArray();
                modulos = NegocioModulosMabe.GetAll().Where(p => nums.Contains(p.ModuleID)).ToList();
            }
            else
            { modulos = NegocioModulosMabe.GetAll(); }
            if (StatusOrderID != "")
            {
                int[] nums = StatusOrderID.Split(',').Select(int.Parse).ToArray();
                estatus = NegocioEstatusOrden.GetAll().Where(p => nums.Contains(p.PK_StatusOrderID)).ToList();
            }
            else
            { estatus = NegocioEstatusOrden.GetAll(); }


            var lt = (from a in visitas
                      join b in estatus on a.StatusOrderID equals b.PK_StatusOrderID
                      join c in ordenes on a.OrderID equals c.PK_OrderID
                      join d in empleado on c.TechnicalID equals d.EmployeeID
                      join e in garantia on c.FK_GuarantyID equals e.PK_GuarantyID
                      join f in modulos on c.FK_ModuleID equals f.ModuleID
                      select new ModelViewEvidence()
                      {
                          OrderID = c.OrderID,
                          EmployeeName = d.FirstName + " " + d.LastName,
                          Date = c.OrderExecuteDate.ToString("dd/MM/yyyy"),
                          EvidenceBadUse = NegocioEvidencia.GetEvidence(a.VisitID, "BadUse").Select(p => new EvidenceOrder { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                          EvidenceFinish = NegocioEvidencia.GetEvidence(a.VisitID, "Finish").Select(p => new EvidenceOrder { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                          EvidenceNoSeriesNumber = NegocioEvidencia.GetEvidence(a.VisitID,"NoSeriesNumber").Select(p => new EvidenceOrder { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                          EvidencePurchaseNote = NegocioEvidencia.GetEvidence(a.VisitID, "PurchaseNote").Select(p => new EvidenceOrder { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                          EvidenceStartODS = NegocioEvidencia.GetEvidence(a.VisitID, "StartODS").Select(p => new EvidenceOrder { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).Take(3).ToList(),
                          EvidenceSummary = NegocioEvidencia.GetEvidence(a.VisitID, "Summary").Select(p => new EvidenceOrder { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList()
                          // NegocioEvidencia.GetEvidence
                          //EvidenceBadUse = NegocioEvidencia.GetAll().Where(p => p.MonitorOrdersID == a.VisitID && p.TypeEvidence == "BadUse").Select(p => new EvidenceOrder { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                          //EvidenceFinish = NegocioEvidencia.GetAll().Where(p => p.MonitorOrdersID == a.VisitID && p.TypeEvidence == "Finish").Select(p => new EvidenceOrder { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                          //EvidenceNoSeriesNumber = NegocioEvidencia.GetAll().Where(p => p.MonitorOrdersID == a.VisitID && p.TypeEvidence == "NoSeriesNumber").Select(p => new EvidenceOrder { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                          //EvidencePurchaseNote = NegocioEvidencia.GetAll().Where(p => p.MonitorOrdersID == a.VisitID && p.TypeEvidence == "PurchaseNote").Select(p => new EvidenceOrder { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList(),
                          //EvidenceStartODS = NegocioEvidencia.GetAll().Where(p => p.MonitorOrdersID == a.VisitID && p.TypeEvidence == "StartODS").Select(p => new EvidenceOrder { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).Take(3).ToList(),
                          //EvidenceSummary = NegocioEvidencia.GetAll().Where(p => p.MonitorOrdersID == a.VisitID && p.TypeEvidence == "Summary").Select(p => new EvidenceOrder { TypeEvidence = p.TypeEvidence, URLEvidence = GlobalConfiguration.urlRequest + p.URLEvidence.Substring(2) }).ToList()
                      }).ToList();


             return lt;
        }


    }
}
