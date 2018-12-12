using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessVisit
    {
        public void Insert(int FK_OrderID,int FK_StatusVisitID, int FK_StatusOrderID, double? LatitudeAddress, double? LogitudeAddress )
        {
            int? causeID = null;
            var NegocioOrder = new BusinessOrder();
            var order = NegocioOrder.GetByID(FK_OrderID);
            if(order != null)
            { causeID = order.PreOrder != true ? null : (int?) 47; }
            var objRepository = new RepositoryVisit();
            EntityVisit data = new EntityVisit()
            {
                PK_VisitID = 0,
                FK_OrderID = FK_OrderID,
                FK_StatusVisitID = FK_StatusVisitID,
                FK_StatusOrderID = FK_StatusOrderID,
                FK_CauseOrderID = causeID,
                SequenceVisit = null,
                StartVisitDate = null,
                EndVisitDate = null,
                StartOrderDate = null,
                EndOrderDate = null,
                StartServiceDate = null,
                EndServiceDate = null,
                LatitudeAddress = Convert.ToSingle(LatitudeAddress),
                LogitudeAddress = Convert.ToSingle(LogitudeAddress),
                LatitudeStartVisit = null,
                LogitudeStartVisit = null,
                LatitudeEndVisit = null,
                LogitudeEndVisit = null,
                LatitudeStartOrder = null,
                LogitudeStartOrder = null,
                LatitudeEndOrder = null,
                LogitudeEndOrder = null,
                DurationVisit = null,
                DurationOrder = null,
                DurationExecute = null,
                DurationTransport = null,
                NoteVisit = "",
                NoteOrder = "",
                Status = true,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Insert(data);

        }

        public void Update(EntityVisit visit)
        {       
            new RepositoryVisit().Update(visit);
        }



        public List<EntityVisit> GetAll()
        {
            return new RepositoryVisit().GetAll();
        }


        public EntityVisit GetByOrderID(int OrderID)
        {
            return new RepositoryVisit().GetByOrderID(OrderID);
        }


        public EntityVisit GetByID(int ID)
        {
            return new RepositoryVisit().Get(ID);
        }
        public EntityVisit GetByOrderID(int OrderID, int StatusVisitID)
        {
            return new RepositoryVisit().GetByOrderID(OrderID, StatusVisitID);
        }

        //public EntityVisit GetByOrderID(int OrderID)
        //{
        //    return new RepositoryVisit().GetByOrderID(OrderID);
        //}


        //public List<EntityVisit> GetAll()
        //{
        //    return new RepositoryVisit().GetAll().Select(p => new EntityVisit()
        //    {
        //        PK_VisitID = p.PK_VisitID,
        //        FK_OrderID = p.FK_OrderID,
        //        FK_StatusVisitID = p.FK_StatusVisitID,
        //        FK_StatusOrderID = p.FK_StatusOrderID,
        //        FK_CauseOrderID = p.FK_CauseOrderID,
        //        SequenceVisit = p.SequenceVisit,
        //        StartVisitDate = p.StartVisitDate,
        //        EndVisitDate = p.EndVisitDate,
        //        StartOrderDate = p.StartOrderDate,
        //        EndOrderDate = p.EndOrderDate,
        //        StartServiceDate = p.StartServiceDate,
        //        EndServiceDate = p.EndServiceDate,
        //        LatitudeAddress = p.LatitudeAddress,
        //        LogitudeAddress =  p.LogitudeAddress,
        //        LatitudeStartVisit = p.LatitudeStartVisit,
        //        LogitudeStartVisit = p.LogitudeStartVisit,
        //        LatitudeEndVisit = p.LatitudeEndVisit,
        //        LogitudeEndVisit = p.LogitudeEndVisit,
        //        LatitudeStartOrder = p.LatitudeStartOrder,
        //        LogitudeStartOrder = p.LogitudeStartOrder,
        //        LatitudeEndOrder = p.LatitudeEndOrder,
        //        LogitudeEndOrder = p.LogitudeEndOrder,
        //        DurationVisit = p.DurationVisit,
        //        DurationOrder = p.DurationOrder,
        //        DurationExecute = p.DurationExecute,
        //        DurationTransport = p.DurationTransport,
        //        NoteVisit = p.NoteVisit,
        //        NoteOrder = p.NoteOrder,
        //        Status = p.Status,
        //        CreateDate = p.CreateDate,
        //        ModifyDate = p.ModifyDate
        //    }).ToList<EntityVisit>();
        //}
    }
}
