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
    public class RepositoryLogMobile : BaseRepository, IRepositoryGET<EntityLogMobile>, IRepositorySET<EntityLogMobile>
    {
        public EntityLogMobile Get(int Id)
        {
            var data = base.DataContext.LogMobile.Where(p => p.PK_LogMobileID == Id);
            if (data.Count() == 1)
                return FactoryLogMobile.Get(data.Single());
            else
                return null;
        }

        public List<EntityLogMobile> GetActives()
        {
            return FactoryLogMobile.GetList(base.DataContext.LogMobile.ToList());
        }

        public List<EntityLogMobile>GetBillings(DateTime DateIn,DateTime DateFn ,string Message)
        {
            return FactoryLogMobile.GetList(base.DataContext.LogMobile.Where(p =>DbFunctions.TruncateTime(p.Date)>= DateIn && DbFunctions.TruncateTime(p.Date)<= DateFn && p.Message.Contains(Message)).ToList());
         }

        public List<EntityLogMobile> GetAll()
        {
            return FactoryLogMobile.GetList(base.DataContext.LogMobile.ToList());
        }

        public EntityLogMobile Insert(EntityLogMobile data)
        {
            try
            {
                LogMobile dataNew = new LogMobile()
                {
                    PK_LogMobileID = data.PK_LogMobileID,
                    FK_OrderID = data.FK_OrderID,
                    FK_UserID = data.FK_UserID,
                    UserName = data.UserName,
                    Name = data.Name,
                    OrderID = data.OrderID,
                    Module = data.Module,
                    Message = data.Message,
                    InnerException = data.InnerException,
                    StackTrace = data.StackTrace,
                    SignType = data.SignType,
                    Battery = data.Battery,
                    SignPercentage = data.SignPercentage,
                    ConnectionType = data.ConnectionType,
                    version = data.version,
                    MobileModel=data.MobileModel,
                    MobileStorage=data.MobileStorage,
                    Type = data.Type,
                    Date = data.Date,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.LogMobile.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_LogMobileID = dataNew.PK_LogMobileID;

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

        public EntityLogMobile Update(EntityLogMobile data)
        {
            try
            {
                var dataUpdate = base.DataContext.LogMobile.Where(p => p.PK_LogMobileID == data.PK_LogMobileID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.FK_OrderID = data.FK_OrderID;
                    dataUpdate.FK_UserID = data.FK_UserID;
                    dataUpdate.UserName = data.UserName;
                    dataUpdate.Name = data.Name;
                    dataUpdate.OrderID = data.OrderID;
                    dataUpdate.Module = data.Module;
                    dataUpdate.Message = data.Message;
                    dataUpdate.InnerException = data.InnerException;
                    dataUpdate.StackTrace = data.StackTrace;
                    dataUpdate.SignType = data.SignType;
                    dataUpdate.Battery = data.Battery;
                    dataUpdate.SignPercentage = data.SignPercentage;
                    dataUpdate.ConnectionType = data.ConnectionType;
                    dataUpdate.MobileModel = data.MobileModel;
                    dataUpdate.MobileStorage = data.MobileStorage;
                    dataUpdate.version = data.version;
                    dataUpdate.Type = data.Type;
                    dataUpdate.Date = data.Date;
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
