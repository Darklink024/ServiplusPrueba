using net.paxialabs.mabe.serviplus.data.Factory.Security;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Security
{
    public class RepositoryDevicePhoneUser : BaseRepository, IRepositoryGET<EntityDevicePhoneUser>, IRepositorySET<EntityDevicePhoneUser>
    {
        public EntityDevicePhoneUser Get(int Id)
        {
            throw new NotImplementedException();
        }

        public EntityDevicePhoneUser Get(int DevicePhoneID, int UserID)
        {
            var data = base.DataContext.DevicePhoneUser.Where(p => p.FK_DevicePhoneID == DevicePhoneID && p.FK_UserID == UserID);
            if (data.Count() == 1)
                return FactoryDevicePhoneUser.Get(data.Single());
            else
                return null;
        }
        
        public List<EntityDevicePhoneUser> GetActives()
        {
            return FactoryDevicePhoneUser.GetList(base.DataContext.DevicePhoneUser.Where(p => p.Status == true).ToList());
        }

        public List<EntityDevicePhoneUser> GetByUser(int UserID)
        {
            return FactoryDevicePhoneUser.GetList(base.DataContext.DevicePhoneUser.Where(p => p.FK_UserID == UserID).ToList());
        }

        public List<EntityDevicePhoneUser> GetByUserIDs(List<int> UserIDs)
        {
            return FactoryDevicePhoneUser.GetList(base.DataContext.DevicePhoneUser.Where(p => UserIDs.Contains(p.FK_UserID) && p.Status == true).ToList());
        }

        public List<EntityDevicePhoneUser> GetByDevicePhone(int DevicePhoneID)
        {
            return FactoryDevicePhoneUser.GetList(base.DataContext.DevicePhoneUser.Where(p => p.FK_DevicePhoneID == DevicePhoneID).ToList());
        }

        public List<EntityDevicePhoneUser> GetAll()
        {
            return FactoryDevicePhoneUser.GetList(base.DataContext.DevicePhoneUser.ToList());
        }

        public EntityDevicePhoneUser Insert(EntityDevicePhoneUser data)
        {
            try
            {
                DevicePhoneUser dataNew = new DevicePhoneUser()
                {
                    FK_DevicePhoneID = data.DevicePhoneID,
                    FK_UserID = data.UserID,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                
                };
                base.DataContext.DevicePhoneUser.Add(dataNew);
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

        public EntityDevicePhoneUser Update(EntityDevicePhoneUser data)
        {
            try
            {
                var dataUpdate = base.DataContext.DevicePhoneUser.Where(p => p.FK_UserID == data.UserID && p.FK_DevicePhoneID == data.DevicePhoneID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.FK_DevicePhoneID = data.DevicePhoneID;
                    dataUpdate.FK_UserID = data.UserID;
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
