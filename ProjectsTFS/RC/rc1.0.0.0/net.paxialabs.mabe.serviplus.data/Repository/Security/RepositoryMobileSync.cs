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

    public class RepositoryMobileSync : BaseRepository, IRepositoryGET<EntityMobileSync>, IRepositorySET<EntityMobileSync>
    {
        public EntityMobileSync Get(int Id)
        {
            var data = base.DataContext.MobileSync.Where(p => p.PK_MobileSyncID == Id);
            if (data.Count() == 1)
                return FactoryMobileSync.Get(data.Single());
            else
                return null;
        }

        public List<EntityMobileSync> GetActives()
        {
            return FactoryMobileSync.GetList(base.DataContext.MobileSync.ToList());
        }

        public List<EntityMobileSync> GetAll()
        {
            return FactoryMobileSync.GetList(base.DataContext.MobileSync.ToList());
        }

        public EntityMobileSync Insert(EntityMobileSync data)
        {
            try
            {
                MobileSync dataNew = new MobileSync()
                {
                    PK_MobileSyncID = data.PK_MobileSyncID,
                    FK_UserID = data.FK_UserID,
                    UserName = data.UserName,
                    Name = data.Name,
                    ODS = data.ODS,
                    Type = data.Type,
                    SyncDate = data.SyncDate,         
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.MobileSync.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_MobileSyncID = dataNew.PK_MobileSyncID;

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

        public EntityMobileSync Update(EntityMobileSync data)
        {
            try
            {
                var dataUpdate = base.DataContext.MobileSync.Where(p => p.PK_MobileSyncID == data.PK_MobileSyncID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.PK_MobileSyncID = data.PK_MobileSyncID;
                    dataUpdate.FK_UserID = data.FK_UserID;
                    dataUpdate.UserName = data.UserName;
                    dataUpdate.Name = data.Name;
                    dataUpdate.ODS = data.ODS;
                    dataUpdate.Type = data.Type;
                    dataUpdate.SyncDate = data.SyncDate;
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
