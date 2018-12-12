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
    public class RepositoryReceivers : BaseRepository, IRepositoryGET<EntityReceivers>, IRepositorySET<EntityReceivers>
    {
        public EntityReceivers Get(int Id)
        {
            throw new NotImplementedException();
        }

        public EntityReceivers Get(int ConfigurationID, int UserID)
        {
            var data = base.DataContext.Receivers.Where(p => p.FK_ConfigurationID == ConfigurationID && p.FK_UserID == UserID);
            if (data.Count() == 1)
                return FactoryReceivers.Get(data.Single());
            else
                return null;
        }

        public List<EntityReceivers> GetByConfiguration(int ConfigurationID)
        {
            return FactoryReceivers.GetList(base.DataContext.Receivers.Where(p => p.FK_ConfigurationID == ConfigurationID).ToList());
        }

        public List<EntityReceivers> GetActives()
        {
            return FactoryReceivers.GetList(base.DataContext.Receivers.Where(p => p.MessageCreate == true).ToList());
        }

        public List<EntityReceivers> GetAll()
        {
            return FactoryReceivers.GetList(base.DataContext.Receivers.ToList());
        }

        public EntityReceivers Insert(EntityReceivers data)
        {
            try
            {
                Receivers dataNew = new Receivers()
                {
                    FK_ConfigurationID = data.ConfigurationID,
                    FK_UserID = data.UserID,
                    MessageCreate = data.MessageCreate,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Receivers.Add(dataNew);
                base.DataContext.SaveChanges();
                
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

        public EntityReceivers Update(EntityReceivers data)
        {
            try
            {
                var dataUpdate = base.DataContext.Receivers.Where(p => p.FK_ConfigurationID == data.ConfigurationID && p.FK_UserID == data.UserID).SingleOrDefault();
                if (dataUpdate != null)
                {

                    dataUpdate.FK_ConfigurationID = data.ConfigurationID;
                    dataUpdate.FK_UserID = data.UserID;
                    dataUpdate.MessageCreate = data.MessageCreate;
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
        public void Delete (List<int> Users, int ConfigurationID)
        {
            try
            {

                foreach (var item in Users)
                {
                    var dataUpdate = base.DataContext.Receivers.Where(p => p.FK_ConfigurationID == ConfigurationID && p.FK_UserID == item).SingleOrDefault();
                    base.DataContext.Entry(dataUpdate).State = EntityState.Deleted;
                    base.DataContext.SaveChanges();
                }
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
