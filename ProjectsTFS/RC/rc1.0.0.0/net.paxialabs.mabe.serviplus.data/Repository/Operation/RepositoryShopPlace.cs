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
     
    public class RepositoryShopPlace : BaseRepository, IRepositoryGET<EntityShopPlace>, IRepositorySET<EntityShopPlace>
    {
        public EntityShopPlace Get(int Id)
        {
            var data = base.DataContext.ShopPlace.Where(p => p.PK_ShopPlaceID == Id);
            if (data.Count() == 1)
                return FactoryShopPlace.Get(data.Single());
            else
                return null;
        }

        public List<EntityShopPlace> GetActives()
        {
            return FactoryShopPlace.GetList(base.DataContext.ShopPlace.Where(p => p.Status == true).ToList());
        }

        public List<EntityShopPlace> GetAll()
        {
            return FactoryShopPlace.GetList(base.DataContext.ShopPlace.ToList());
        }

        public EntityShopPlace Insert(EntityShopPlace data)
        {
            try
            {
                ShopPlace dataNew = new ShopPlace()
                {
                    PK_ShopPlaceID = data.PK_ShopPlaceID,
                    FK_StateID = data.FK_StateID,
                    ShopPlaceID = data.ShopPlaceID,
                    ShopPlace1 = data.ShopPlace1,
                    CountryAddress = data.CountryAddress,
                    StateAddress = data.StateAddress,
                    CityAddress = data.CityAddress,
                    MunicipalityAddress = data.MunicipalityAddress,
                    StreetAddress = data.StreetAddress,
                    ClientID = data.ClientID,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.ShopPlace.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_ShopPlaceID = dataNew.PK_ShopPlaceID;

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

        public void BulkInsert(List<EntityShopPlace> data)
        {
            try
            {
                base.DataContext.BulkInsert<ShopPlace>(data.Select(p => new ShopPlace()
                {
                    PK_ShopPlaceID = 0,
                    FK_StateID = p.FK_StateID,
                    ShopPlaceID = p.ShopPlaceID,
                    ShopPlace1 = p.ShopPlace1,
                    CountryAddress = p.CountryAddress,
                    StateAddress = p.StateAddress,
                    CityAddress = p.CityAddress,
                    MunicipalityAddress = p.MunicipalityAddress,
                    StreetAddress = p.StreetAddress,
                    ClientID = p.ClientID,
                    Status = p.Status,
                    CreateDate = p.CreateDate,
                    ModifyDate = p.ModifyDate
                }));
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


        public void BulkMerge(List<EntityShopPlace> data)
        {

            base.DataContext.BulkMerge<ShopPlace>(data.Select(p => new ShopPlace()
            {
                PK_ShopPlaceID = 0,
                FK_StateID = p.FK_StateID,
                ShopPlaceID = p.ShopPlaceID,
                ShopPlace1 = p.ShopPlace1,
                CountryAddress = p.CountryAddress,
                StateAddress = p.StateAddress,
                CityAddress = p.CityAddress,
                MunicipalityAddress = p.MunicipalityAddress,
                StreetAddress = p.StreetAddress,
                ClientID = p.ClientID,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }), options => options.ColumnPrimaryKeyExpression = c => new { c.ShopPlaceID });
        }
        public EntityShopPlace Update(EntityShopPlace data)
        {
            try
            {
                var dataUpdate = base.DataContext.ShopPlace.Where(p => p.PK_ShopPlaceID == data.PK_ShopPlaceID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_ShopPlaceID = data.PK_ShopPlaceID;
                    dataUpdate.FK_StateID = data.FK_StateID;
                    dataUpdate.ShopPlaceID = data.ShopPlaceID;
                    dataUpdate.ShopPlace1 = data.ShopPlace1;
                    dataUpdate.CountryAddress = data.CountryAddress;
                    dataUpdate.StateAddress = data.StateAddress;
                    dataUpdate.CityAddress = data.CityAddress;
                    dataUpdate.MunicipalityAddress = data.MunicipalityAddress;
                    dataUpdate.StreetAddress = data.StreetAddress;
                    dataUpdate.ClientID = data.ClientID;
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

        public EntityShopPlace GetByShopPlace(string ShopPlaceID)
        {
            var data = base.DataContext.ShopPlace.Where(p => p.ShopPlaceID == ShopPlaceID);
            if (data.Count() == 1)
                return FactoryShopPlace.Get(data.Single());
            else
                return null;
        }
    }
}
