using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Notification
{
    internal class BusinessConfiguration
    {
        public ModelViewConfiguration Get(int Id)
        {
            return (ModelViewConfiguration) new RepositoryConfiguration().Get(Id);
        }

        public List<ModelViewConfiguration> GetActives()
        {
            // ConfigurationID, Title, Message, Url, Status, Publish, CreateDate, ModifyDate
            return new RepositoryConfiguration().GetActives().Select(p => new ModelViewConfiguration() {
                ConfigurationID = p.ConfigurationID,
                Title = p.Title,
                Message = p.Message,
                Url = p.Url,
                Status = p.Status,
                Publish = p.Publish,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<ModelViewConfiguration>();
        }

        public List<ModelViewConfiguration> GetAll()
        {
            // ConfigurationID, Title, Message, Url, Status, Publish, CreateDate, ModifyDate
            return new RepositoryConfiguration().GetAll().Select(p => new ModelViewConfiguration()
            {
                ConfigurationID = p.ConfigurationID,
                Title = p.Title,
                Message = p.Message,
                Url = p.Url,
                Status = p.Status,
                Publish = p.Publish,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<ModelViewConfiguration>();
        }


        public ModelViewConfiguration InsertURLY(ModelViewConfiguration data, int[] Usuarios)
        {
            var objRepository = new RepositoryConfiguration();
            var objRepositoryMsj = new RepositoryReceivers();
            EntityConfiguration config = new EntityConfiguration()
            {
                ConfigurationID = data.ConfigurationID,
                Title = data.Title,
                Message = data.Message,
                Url = data.Url,
                Status = true,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.UtcNow
            };
            config = objRepository.Insert(config);
            foreach (var item in Usuarios)
            {
                EntityReceivers receivers = new EntityReceivers()
                {
                    ConfigurationID = config.ConfigurationID,
                    UserID = item,
                    MessageCreate = true,
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.UtcNow
                };
                receivers = objRepositoryMsj.Insert(receivers);
            }

            return data;

        }

        public ModelViewConfiguration UpdateURLY(ModelViewConfiguration data, int[] Usuarios)
        {
            var objRepository = new RepositoryConfiguration();
            var objRepositoryMsj = new RepositoryReceivers();
            var config = objRepository.Get(data.ConfigurationID);
                config.Title = data.Title;
                config.Message = data.Message;
                config.Url = data.Url;
                config.ModifyDate = DateTime.UtcNow;
                objRepository.Update(config);

            List<int> IDS =  objRepositoryMsj.GetByConfiguration(config.ConfigurationID).Select(a=> a.UserID).ToList();
            objRepositoryMsj.Delete(IDS, config.ConfigurationID);
            foreach (var item in Usuarios)
            {
                EntityReceivers receivers = new EntityReceivers()
                {
                    ConfigurationID = config.ConfigurationID,
                    UserID = item,
                    MessageCreate = true,
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.UtcNow
                };
                receivers = objRepositoryMsj.Insert(receivers);
            }

            return data;
        }
        public ModelViewConfiguration Insert(string fileUpload, ModelViewConfiguration data, string path, int[] Usuarios)
        {
                string fileName = data.Url;
                string combinePath = path + fileName;
                System.IO.File.Copy(fileUpload, combinePath, true);
                System.IO.File.Delete(fileUpload);
                var objRepository = new RepositoryConfiguration();
                var objRepositoryMsj = new RepositoryReceivers();
                EntityConfiguration config = new EntityConfiguration()
                {
                    ConfigurationID = data.ConfigurationID,
                    Title = data.Title,
                    Message = data.Message,
                    Url = GlobalConfiguration.urlRequest + "Content/Notification/" + fileName,
                    Status = true,
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.UtcNow
                };
                config = objRepository.Insert(config);
                foreach(var item in Usuarios)
               {
                EntityReceivers receivers = new EntityReceivers()
                {
                    ConfigurationID = config.ConfigurationID,
                    UserID = item,
                    MessageCreate = true,
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.UtcNow
                };
                receivers = objRepositoryMsj.Insert(receivers);
               }
                return data;
        }


        public ModelViewConfiguration Update(string fileUpload, ModelViewConfiguration data, string path, int[] Usuarios)
        {
            string fileName = data.Url;
            if(File.Exists(fileUpload))
            {
                string combinePath = path + fileName;
                System.IO.File.Copy(fileUpload, combinePath, true);
                System.IO.File.Delete(fileUpload);
            }
           
            var objRepository = new RepositoryConfiguration();
            var objRepositoryMsj = new RepositoryReceivers();
            var config = objRepository.Get(data.ConfigurationID);
            config.Title = data.Title;
            config.Message = data.Message;
            config.Url = File.Exists(fileUpload) ? GlobalConfiguration.urlRequest + "Content/Notification/" + data.Url: config.Url;
            config.ModifyDate = DateTime.UtcNow;
            objRepository.Update(config);
            List<int> IDS = objRepositoryMsj.GetByConfiguration(config.ConfigurationID).Select(a => a.UserID).ToList();
            objRepositoryMsj.Delete(IDS, config.ConfigurationID);
            foreach (var item in Usuarios)
            {
                EntityReceivers receivers = new EntityReceivers()
                {
                    ConfigurationID = config.ConfigurationID,
                    UserID = item,
                    MessageCreate = true,
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.UtcNow
                };
                receivers = objRepositoryMsj.Insert(receivers);
            }
            return data;
        }

        public ModelViewConfiguration Update(EntityConfiguration data)
        {
            return (ModelViewConfiguration)new RepositoryConfiguration().Update((EntityConfiguration) data);
        }

        public List<ModelViewConfiguration> GetListAll()
        {
            var NegocioDestinatarios = new BusinessReceivers();

            return GetAll().Select(p => new ModelViewConfiguration
            {
                ConfigurationID = p.ConfigurationID,
                Title = p.Title,
                Message = p.Message,
                Url = p.Url,
                Status = p.Status,
                Publish = p.Publish,
                Users = string.Join(",", NegocioDestinatarios.GetAll().Where(a => a.ConfigurationID == p.ConfigurationID).Select(a => a.UserID))
            }).ToList();
                  
        }
    }
}
