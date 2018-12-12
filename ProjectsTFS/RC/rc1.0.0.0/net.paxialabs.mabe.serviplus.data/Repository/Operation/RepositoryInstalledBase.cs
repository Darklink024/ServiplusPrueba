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

    public class RepositoryInstalledBase : BaseRepository, IRepositoryGET<EntityInstalledBase>, IRepositorySET<EntityInstalledBase>
    {
        public EntityInstalledBase Get(int Id)
        {
            var data = base.DataContext.InstalledBase.Where(p => p.PK_InstalledBaseID == Id);
            if (data.Count() == 1)
                return FactoryInstalledBase.Get(data.Single());
            else
                return null;
        }

        public EntityInstalledBase GetByInstalledBase(string InstalledBaseID)
        {
            var data = base.DataContext.InstalledBase.Where(p => p.InstalledBaseID == InstalledBaseID);
            if (data.Count() == 1)
                return FactoryInstalledBase.Get(data.Single());
            else
                return null;
        }

        public List<EntityInstalledBase> GetActives()
        {
            return FactoryInstalledBase.GetList(base.DataContext.InstalledBase.Where(p => p.Status == true).ToList());
        }

        public List<EntityInstalledBase> GetAll()
        {
            return FactoryInstalledBase.GetList(base.DataContext.InstalledBase.ToList());
        }

        public List<EntityInstalledBase> GetAllByBI(List<int> baseInt)
        {
            this.DataContext.Database.CommandTimeout = 180;
            return FactoryInstalledBase.GetList(base.DataContext.InstalledBase.Where(p => baseInt.Contains(p.PK_InstalledBaseID)).ToList());
        }

        public List<EntityInstalledBase> GetByClient(int ClientID)
        {
            return FactoryInstalledBase.GetList(base.DataContext.InstalledBase.Where(p => p.FK_ClientID == ClientID).ToList());
        }

        public EntityInstalledBase Insert(EntityInstalledBase data)
        {
            try
            {
                InstalledBase dataNew = new InstalledBase()
                {
                    PK_InstalledBaseID = data.PK_InstalledBaseID,
                    FK_ClientID = data.FK_ClientID,
                    FK_ProductID = data.FK_ProductID,
                    FK_ShopPlaceID = data.FK_ShopPlaceID,
                    InstalledBaseID = data.InstalledBaseID,
                    SerialNumber = data.SerialNumber,
                    ShopDate = data.ShopDate,
                    ShopPlaceIDFlag = data.ShopPlaceIDFlag,
                    ShopDateFlag = data.ShopDateFlag,
                    SerialNumberFlag = data.SerialNumberFlag,
                    ProductIDFlag = data.ProductIDFlag,
                    Model = data.Model,
                    ProductName = data.ProductName,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.InstalledBase.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_InstalledBaseID = dataNew.PK_InstalledBaseID;

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

        public EntityInstalledBase Update(EntityInstalledBase data)
        {
            try
            {
                var dataUpdate = base.DataContext.InstalledBase.Where(p => p.PK_InstalledBaseID == data.PK_InstalledBaseID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.PK_InstalledBaseID = data.PK_InstalledBaseID;
                    dataUpdate.FK_ClientID = data.FK_ClientID;
                    dataUpdate.FK_ProductID = data.FK_ProductID;
                    dataUpdate.FK_ShopPlaceID = data.FK_ShopPlaceID;
                    dataUpdate.InstalledBaseID = data.InstalledBaseID;
                    dataUpdate.SerialNumber = data.SerialNumber;
                    dataUpdate.ShopDate = data.ShopDate;
                    dataUpdate.ShopPlaceIDFlag = data.ShopPlaceIDFlag;
                    dataUpdate.ShopDateFlag = data.ShopDateFlag;
                    dataUpdate.SerialNumberFlag = data.SerialNumberFlag;
                    dataUpdate.ProductIDFlag = data.ProductIDFlag;
                    dataUpdate.Model = data.Model;
                    dataUpdate.ProductName = data.ProductName;
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
