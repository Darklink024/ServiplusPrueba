using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Notification
{
    internal class BusinessReceivers
    {
        public ModelViewReceivers Get(int Id)
        {
            return (ModelViewReceivers)new RepositoryReceivers().Get(Id);
        }

        public List<ModelViewReceivers> GetActives()
        {
            //FK_ConfigurationID, FK_UserID, MessageCreate, CreateDate, ModifyDate
            return new RepositoryReceivers().GetActives().Select(p => new ModelViewReceivers()
            {
                ConfigurationID = p.ConfigurationID,
                UserID = p.UserID,
                MessageCreate = p.MessageCreate,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<ModelViewReceivers>();
        }

        public List<ModelViewReceivers> GetAll()
        {
            // ReceiversID, Title, Message, Url, Status, Publish, CreateDate, ModifyDate
            return new RepositoryReceivers().GetAll().Select(p => new ModelViewReceivers()
            {
                ConfigurationID = p.ConfigurationID,
                UserID = p.UserID,
                MessageCreate = p.MessageCreate,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<ModelViewReceivers>();
        }

        public List<ModelViewReceivers> GetByConfiguration(int ConfigurationID)
        {
            // ReceiversID, Title, Message, Url, Status, Publish, CreateDate, ModifyDate
            return new RepositoryReceivers().GetByConfiguration(ConfigurationID).Select(p => new ModelViewReceivers()
            {
                ConfigurationID = p.ConfigurationID,
                UserID = p.UserID,
                MessageCreate = p.MessageCreate,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<ModelViewReceivers>();
        }

        public ModelViewReceivers Insert(ModelViewReceivers data)
        {
            return (ModelViewReceivers)new RepositoryReceivers().Insert((EntityReceivers)data);
        }

        public ModelViewReceivers Update(EntityReceivers data)
        {
            return (ModelViewReceivers)new RepositoryReceivers().Update((EntityReceivers)data);
        }
    }
}
