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
    public class RepositoryDevicePhone : BaseRepository, IRepositoryGET<EntityDevicePhone>, IRepositorySET<EntityDevicePhone>
    {
        public EntityDevicePhone Get(int Id)
        {
            var data = base.DataContext.DevicePhone.Where(p => p.DevicePhoneID == Id);
            if (data.Count() == 1)
                return FactoryDevicePhone.Get(data.Single());
            else
                return null;
        }

        public EntityDevicePhone GetByIMEI(string IMEI)
        {
            var data = base.DataContext.DevicePhone.Where(p => p.IMEI == IMEI);
            if (data.Count() == 1)
                return FactoryDevicePhone.Get(data.Single());
            else
                return null;
        }

        public List<EntityDevicePhone> GetActives()
        {
            return FactoryDevicePhone.GetList(base.DataContext.DevicePhone.Where(p => p.Status == true).ToList());
        }

        public List<EntityDevicePhone> GetAll()
        {
            return FactoryDevicePhone.GetList(base.DataContext.DevicePhone.ToList());
        }

        public EntityDevicePhone Insert(EntityDevicePhone data)
        {
            try
            {
                DevicePhone dataNew = new DevicePhone()
                {
                    DevicePhoneID = data.DevicePhoneID,
                    Phone = data.Phone,
                    IMEI = data.IMEI,
                    FCM = data.FCM,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.DevicePhone.Add(dataNew);
                base.DataContext.SaveChanges();

                data.DevicePhoneID = dataNew.DevicePhoneID;

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

        public EntityDevicePhone Update(EntityDevicePhone data)
        {
            try
            {
                var dataUpdate = base.DataContext.DevicePhone.Where(p => p.DevicePhoneID == data.DevicePhoneID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.DevicePhoneID = data.DevicePhoneID;
                    dataUpdate.Phone = data.Phone;
                    dataUpdate.IMEI = data.IMEI;
                    dataUpdate.FCM = data.FCM;
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
