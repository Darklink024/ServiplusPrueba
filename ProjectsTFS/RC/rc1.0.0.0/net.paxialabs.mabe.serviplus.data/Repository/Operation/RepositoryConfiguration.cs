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
    public class RepositoryConfiguration : BaseRepository, IRepositoryGET<EntityConfiguration>, IRepositorySET<EntityConfiguration>
    {
        public EntityConfiguration Get(int Id)
        {
            var data = base.DataContext.Configuration.Where(p => p.PK_ConfigurationID == Id);
            if (data.Count() == 1)
                return FactoryConfiguration.Get(data.Single());
            else
                return null;
        }
        
        public List<EntityConfiguration> GetActives()
        {
            return FactoryConfiguration.GetList(base.DataContext.Configuration.Where(p => p.Status == true).ToList());
        }

        public List<EntityConfiguration> GetAll()
        {
            return FactoryConfiguration.GetList(base.DataContext.Configuration.OrderByDescending( p => p.ModifyDate ).ToList());
        }

        public EntityConfiguration Insert(EntityConfiguration data)
        {
            try
            {
                // PK_ConfigurationID, Title, Message, Url, Status, Publish, CreateDate, ModifyDate
                Configuration dataNew = new Configuration()
                {
                    PK_ConfigurationID = data.ConfigurationID,
                    Title = data.Title,
                    Message = data.Message,
                    Url = data.Url,
                    Status = data.Status,
                    Publish = data.Publish,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Configuration.Add(dataNew);
                base.DataContext.SaveChanges();

                data.ConfigurationID = dataNew.PK_ConfigurationID;

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

        public EntityConfiguration Update(EntityConfiguration data)
        {
            try
            {
                var dataUpdate = base.DataContext.Configuration.Where(p => p.PK_ConfigurationID == data.ConfigurationID).SingleOrDefault();

                // PK_ConfigurationID, Title, Message, Url, Status, Publish, CreateDate, ModifyDate
                if (dataUpdate != null)
                {

                    dataUpdate.PK_ConfigurationID = data.ConfigurationID;
                    dataUpdate.Title = data.Title;
                    dataUpdate.Message = data.Message;
                    dataUpdate.Url = data.Url;
                    dataUpdate.Publish = data.Publish;
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
