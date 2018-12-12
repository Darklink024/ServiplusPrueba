using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessOrderEvidence
    {

        public List<ModelViewEvidence> GetList(string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, EntityUser User)
        {
            return new RepositoryOrderEvidence().GetList(StatusVisitID, ModuleID, PriorityID, StatusOrderID, ServiceID, OrderID, Employee, StartDate, EndDate, User);
        }
        public EntityOrderEvidence Get(int Id)
        {
            return new RepositoryOrderEvidence().Get(Id);
        }
        public EntityOrderEvidence GetByeOrderID(int Id)
        {
            return new RepositoryOrderEvidence().GetByOrderID(Id);
        }
        public List<EntityOrderEvidence> GetActives()
        {
            return new RepositoryOrderEvidence().GetActives();
        }
        public List<EntityOrderEvidence> GetAll()
        {
            return new RepositoryOrderEvidence().GetAll();
        }

        public List<EntityOrderEvidence> GetEvidence(int MonitorOrdersID, string type)
        {
            return new RepositoryOrderEvidence().GetEvidence(MonitorOrdersID, type);
        }
        public EntityOrderEvidence Insert(EntityOrderEvidence data)
        {
            return new RepositoryOrderEvidence().Insert(data);
        }
        public EntityOrderEvidence Update(EntityOrderEvidence data)
        {
            return new RepositoryOrderEvidence().Update(data);
        }

        public bool Exists(string OrderID, string TypeEvidence, string FileName)
        {
            return new RepositoryOrderEvidence().Exists(OrderID, TypeEvidence, FileName);
        }

        public void RegisterEvidence(ModelViewOrderEvidenceUpload data)
        {

            var dataODS = new BusinessOrder().GetByOrderID(data.OrderID);

            if (!Directory.Exists(Path.Combine(GlobalConfiguration.LocateEvidence, dataODS.FK_ModuleID.ToString())))
            {
                Directory.CreateDirectory(Path.Combine(GlobalConfiguration.LocateEvidence, dataODS.FK_ModuleID.ToString()));
            }

            string filePath = Path.Combine(GlobalConfiguration.LocateEvidence, dataODS.FK_ModuleID.ToString(), data.FileName);

            string relativePath = Path.Combine(GlobalConfiguration.LocateEvidenceRelative, dataODS.FK_ModuleID.ToString(), data.FileName);

            var bytes = Convert.FromBase64String(data.Content);
            using (var imageFile = new FileStream(filePath, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }

            int MonitorID = 0;
            var dataMonitor = new BusinessMonitor().GetByOrderID(dataODS.PK_OrderID); //.GetAll().Where(p => p.OrderID == dataODS.PK_OrderID).Single();
            if (dataMonitor != null) MonitorID = dataMonitor.VisitID;

            if (!Exists(data.OrderID, data.TypeEvidence, relativePath))
            {
                Insert(new EntityOrderEvidence()
                {
                    EvidenceID = 0,
                    OrderID = dataODS.PK_OrderID,
                    MonitorOrdersID = MonitorID,
                    TypeEvidence = data.TypeEvidence,
                    URLEvidence = relativePath,
                    Status = true,
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow
                });
            }
        }            
        
    }
}
