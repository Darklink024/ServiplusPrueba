using LinqKit;
using net.paxialabs.mabe.serviplus.data.Factory.Operation;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{

    public class RepositoryPrice : BaseRepository, IRepositoryGET<EntityPrice>, IRepositorySET<EntityPrice>
    {
        public EntityPrice Get(int Id)
        {
            var data = base.DataContext.Prices.Where(p => p.PK_PriceID == Id);
            if (data.Count() == 1)
                return FactoryPrice.Get(data.Single());
            else
                return null;
        }

        public List<EntityPrice> GetActives()
        {
            return FactoryPrice.GetList(base.DataContext.Prices.Where(p => p.Status == true).ToList());
        }

        public List<EntityPrice> GetAll()
        {
            var a= FactoryPrice.GetList(base.DataContext.Prices.ToList());
            return a;
        }

        public List<ModelViewPrices> GetListPrice(List<int> ProductIDs, DateTime? fh)
        {
            var predicate = PredicateBuilder.New<Prices>();

            predicate = predicate.And(i => ProductIDs.Contains(i.FK_ProductID.Value));

            if (fh.HasValue) predicate = predicate.And(i => i.ModifyDate >= fh);

            return (base.DataContext.Prices.Where(predicate).Select(p => new ModelViewPrices()
            {
                PriceID = p.PK_PriceID,
                BuildOfMaterialsID = p.FK_BuildOfMaterialsID,
                ProductID = p.FK_ProductID,
                TypeCondition = p.TypeCondition,
                SalesOrganization = p.SalesOrganization,
                DistributionChannel = p.DistributionChannel,
                ListPrice = p.ListPrice,
                GroupMaterial1 = p.GroupMaterial1,
                GroupMaterial4 = p.GroupMaterial4,
                Price = p.Price,
                Coin = p.Coin,
                Policy = p.Policy,
                Guaranty = p.Guaranty
            })).ToList();
        }

        public List<EntityPrice> GetAllPriceByModel(List<int> models)
        {
            this.DataContext.Database.CommandTimeout = 180;
            return FactoryPrice.GetList(base.DataContext.Prices.Where(p => models.Contains(p.FK_ProductID.Value)).ToList());
        }
        public EntityPrice GetPrice(int FK_BuildOfMaterialsID, int FK_ProductID)
        {
            var data = base.DataContext.Prices.Where(p => p.FK_BuildOfMaterialsID == FK_BuildOfMaterialsID && p.FK_ProductID == FK_ProductID);
            if (data.Count() == 1)
                return FactoryPrice.Get(data.Single());
            else
                return null;
        }
        public EntityPrice Insert(EntityPrice data)
        {
            try
            {
                Prices dataNew = new Prices()
                {
                    PK_PriceID = data.PK_PriceID,
                    FK_BuildOfMaterialsID = data.FK_BuildOfMaterialsID,
                    FK_ProductID = data.FK_ProductID,
                    FK_WorkforceID = data.FK_WorkforceID,
                    TypeCondition = data.TypeCondition,
                    SalesOrganization = data.SalesOrganization,
                    DistributionChannel = data.DistributionChannel,
                    ListPrice = data.ListPrice,
                    GroupMaterial1 = data.GroupMaterial1,
                    GroupMaterial4 = data.GroupMaterial4,
                    Price = data.Price,
                    Coin = data.Coin,
                    DateValidity = data.DateValidity,
                    DateValidityEnd = data.DateValidityEnd,
                    Policy = data.Policy,
                    Guaranty = data.Guaranty,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Prices.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_PriceID = dataNew.PK_PriceID;

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

        public void BulkInsert(List<EntityPrice> data)
        {
            try
            {
                
                base.DataContext.BulkInsert<Prices>(data.Select(p => new Prices() {
                    PK_PriceID = p.PK_PriceID,
                    FK_BuildOfMaterialsID = p.FK_BuildOfMaterialsID,
                    FK_ProductID = p.FK_ProductID,
                    FK_WorkforceID = p.FK_WorkforceID,
                    TypeCondition = p.TypeCondition,
                    SalesOrganization = p.SalesOrganization,
                    DistributionChannel = p.DistributionChannel,
                    ListPrice = p.ListPrice,
                    GroupMaterial1 = p.GroupMaterial1,
                    GroupMaterial4 = p.GroupMaterial4,
                    Price = p.Price,
                    Coin = p.Coin,
                    DateValidity = p.DateValidity,
                    DateValidityEnd = p.DateValidityEnd,
                    Policy = p.Policy,
                    Guaranty = p.Guaranty,
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

        public void BulkUpdate(List<EntityPrice> data)
        {
            try
            {

                base.DataContext.BulkUpdate<Prices>(data.Select(p => new Prices()
                {
                    PK_PriceID = p.PK_PriceID,
                    FK_BuildOfMaterialsID = p.FK_BuildOfMaterialsID,
                    FK_ProductID = p.FK_ProductID,
                    FK_WorkforceID = p.FK_WorkforceID,
                    TypeCondition = p.TypeCondition,
                    SalesOrganization = p.SalesOrganization,
                    DistributionChannel = p.DistributionChannel,
                    ListPrice = p.ListPrice,
                    GroupMaterial1 = p.GroupMaterial1,
                    GroupMaterial4 = p.GroupMaterial4,
                    Price = p.Price,
                    Coin = p.Coin,
                    DateValidity = p.DateValidity,
                    DateValidityEnd = p.DateValidityEnd,
                    Policy = p.Policy,
                    Guaranty = p.Guaranty,
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

        public void BulkMerge(List<EntityPrice> data)
        {

            base.DataContext.BulkMerge<Prices>(data.Select(p => new Prices()
            {
                PK_PriceID = p.PK_PriceID,
                FK_BuildOfMaterialsID = p.FK_BuildOfMaterialsID,
                FK_ProductID = p.FK_ProductID,
                FK_WorkforceID = p.FK_WorkforceID,
                TypeCondition = p.TypeCondition,
                SalesOrganization = p.SalesOrganization,
                DistributionChannel = p.DistributionChannel,
                ListPrice = p.ListPrice,
                GroupMaterial1 = p.GroupMaterial1,
                GroupMaterial4 = p.GroupMaterial4,
                Price = p.Price,
                Coin = p.Coin,
                DateValidity = p.DateValidity,
                DateValidityEnd = p.DateValidityEnd,
                Policy = p.Policy,
                Guaranty = p.Guaranty,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }), options => options.ColumnPrimaryKeyExpression = c => new { c.FK_BuildOfMaterialsID, c.FK_ProductID });
        }

        public EntityPrice Update(EntityPrice data)
        {
            try
            {
                var dataUpdate = base.DataContext.Prices.Where(p => p.PK_PriceID == data.PK_PriceID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.FK_BuildOfMaterialsID = data.FK_BuildOfMaterialsID;
                    dataUpdate.FK_ProductID = data.FK_ProductID;
                    dataUpdate.FK_WorkforceID = data.FK_WorkforceID;
                    dataUpdate.TypeCondition = data.TypeCondition;
                    dataUpdate.SalesOrganization = data.SalesOrganization;
                    dataUpdate.DistributionChannel = data.DistributionChannel;
                    dataUpdate.ListPrice = data.ListPrice;
                    dataUpdate.GroupMaterial1 = data.GroupMaterial1;
                    dataUpdate.GroupMaterial4 = data.GroupMaterial4;
                    dataUpdate.Price = data.Price;
                    dataUpdate.Coin = data.Coin;
                    dataUpdate.DateValidity = data.DateValidity;
                    dataUpdate.DateValidityEnd = data.DateValidityEnd;
                    dataUpdate.Policy = data.Policy;
                    dataUpdate.Guaranty = data.Guaranty;
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
