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
    public class RepositoryVisit : BaseRepository, IRepositoryGET<EntityVisit>, IRepositorySET<EntityVisit>
    {
        public EntityVisit Get(int Id)
        {
            var data = base.DataContext.MonitorOrders.Where(p => p.PK_MonitorOrdersID == Id);
            if (data.Count() == 1)
                return FactoryVisit.Get(data.Single());
            else
                return null;
        }

        public List<EntityVisit> GetActives()
        {
            return FactoryVisit.GetList(base.DataContext.MonitorOrders.Where(p => p.Status == true).ToList());
        }

        public List<EntityVisit> GetAll()
        {
            return FactoryVisit.GetList(base.DataContext.MonitorOrders.ToList());
        }


        public EntityVisit GetByOrderID (int OrderID, int StatusVisitID)
        {
            var data = base.DataContext.MonitorOrders.Where(p => p.FK_OrderID == OrderID && p.FK_StatusVisitID == StatusVisitID);
            if (data.Count() == 1)
                return FactoryVisit.Get(data.Single());
            else
                return null;
        }


        public EntityVisit GetByOrderID(int OrderID)
        {
            var data = base.DataContext.MonitorOrders.Where(p => p.FK_OrderID == OrderID);
            if (data.Count() == 1)
                return FactoryVisit.Get(data.Single());
            else
                return null;
        }

        public EntityVisit Insert(EntityVisit data)
        {
            try
            {
                MonitorOrders dataNew = new MonitorOrders()
                {
                    PK_MonitorOrdersID = data.PK_VisitID,
                    FK_OrderID = data.FK_OrderID,
                    FK_StatusVisitID = data.FK_StatusVisitID,
                    FK_StatusOrderID = data.FK_StatusOrderID,
                    FK_CauseOrderID = data.FK_CauseOrderID,
                    SequenceVisit = data.SequenceVisit,
                    StartVisitDate = data.StartVisitDate,
                    EndVisitDate = data.EndVisitDate,
                    StartOrderDate = data.StartOrderDate,
                    EndOrderDate = data.EndOrderDate,
                    StartServiceDate = data.StartServiceDate,
                    EndServiceDate = data.EndServiceDate,
                    LatitudeAddress = data.LatitudeAddress,
                    LogitudeAddress = data.LogitudeAddress,
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

                data.PK_VisitID = dataNew.PK_MonitorOrdersID;

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

        public EntityVisit Update(EntityVisit data)
        {
            try
            {
                var dataUpdate = base.DataContext.MonitorOrders.Where(p => p.PK_MonitorOrdersID == data.PK_VisitID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_MonitorOrdersID = data.PK_VisitID;
                    dataUpdate.FK_OrderID = data.FK_OrderID;
                    dataUpdate.FK_StatusVisitID = data.FK_StatusVisitID;
                    dataUpdate.FK_StatusOrderID = data.FK_StatusOrderID;
                    dataUpdate.FK_CauseOrderID = data.FK_CauseOrderID;
                    dataUpdate.SequenceVisit = data.SequenceVisit;
                    dataUpdate.StartVisitDate = data.StartVisitDate;
                    dataUpdate.EndVisitDate = data.EndVisitDate;
                    dataUpdate.StartOrderDate = data.StartOrderDate;
                    dataUpdate.EndOrderDate = data.EndOrderDate;
                    dataUpdate.StartServiceDate = data.StartServiceDate;
                    dataUpdate.EndServiceDate = data.EndServiceDate;
                    dataUpdate.LatitudeAddress = data.LatitudeAddress;
                    dataUpdate.LogitudeAddress = data.LogitudeAddress;
                    dataUpdate.LatitudeStartVisit = data.LatitudeStartVisit;
                    dataUpdate.LogitudeStartVisit = data.LogitudeStartVisit;
                    dataUpdate.LatitudeEndVisit = data.LatitudeEndVisit;
                    dataUpdate.LogitudeEndVisit = data.LogitudeEndVisit;
                    dataUpdate.LatitudeStartOrder = data.LatitudeStartOrder;
                    dataUpdate.LogitudeStartOrder = data.LogitudeStartOrder;
                    dataUpdate.LatitudeEndOrder = data.LatitudeEndOrder;
                    dataUpdate.LogitudeEndOrder = data.LogitudeEndOrder;
                    dataUpdate.DurationVisit = data.DurationVisit;
                    dataUpdate.DurationOrder = data.DurationOrder;
                    dataUpdate.DurationExecute = data.DurationExecute;
                    dataUpdate.DurationTransport = data.DurationTransport;
                    dataUpdate.NoteVisit = data.NoteVisit;
                    dataUpdate.NoteOrder = data.NoteOrder;
                    dataUpdate.Status = data.Status;
                    dataUpdate.CreateDate = data.CreateDate;
                    dataUpdate.ModifyDate = data.ModifyDate;

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

    }
}
