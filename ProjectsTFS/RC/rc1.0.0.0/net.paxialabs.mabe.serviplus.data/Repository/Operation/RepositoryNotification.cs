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
    public class RepositoryNotification : BaseRepository, IRepositoryGET<EntityNotification>, IRepositorySET<EntityNotification>
    {
        public EntityNotification Get(int Id)
        {
            var data = base.DataContext.Message.Where(p => p.PK_MessageID == Id);
            if (data.Count() == 1)
                return FactoryNotification.Get(data.Single());
            else
                return null;
        }

        public List<EntityNotification> GetActives()
        {
            throw new NotImplementedException();
        }

        public List<EntityNotification> GetAll()
        {
            return FactoryNotification.GetList(base.DataContext.Message.ToList());
        }

        public List<EntityNotification> GetByUserID(int UserID)
        {
            return FactoryNotification.GetList(base.DataContext.Message.Where(p => p.FK_UserID == UserID).OrderByDescending(p => p.CreateDate).ToList());
        }

        public EntityNotification Insert(EntityNotification data)
        {
            try
            {
                Message dataNew = new Message()
                {
                    PK_MessageID = data.MessageID,
                    FK_UserID = data.UserID,
                    Message1 = data.Message,
                    MessageRead = data.MessageRead,
                    Title = data.Title,
                    Url = data.Url,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Message.Add(dataNew);
                base.DataContext.SaveChanges();

                data.MessageID = dataNew.PK_MessageID;

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

        public EntityNotification Update(EntityNotification data)
        {
            try
            {
                var dataUpdate = base.DataContext.Message.Where(p => p.PK_MessageID == data.MessageID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.PK_MessageID = data.MessageID;
                    dataUpdate.FK_UserID = data.UserID;
                    dataUpdate.Message1 = data.Message;
                    dataUpdate.MessageRead = data.MessageRead;
                    dataUpdate.Title = data.Title;
                    dataUpdate.Url = data.Url;
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
    }
}
