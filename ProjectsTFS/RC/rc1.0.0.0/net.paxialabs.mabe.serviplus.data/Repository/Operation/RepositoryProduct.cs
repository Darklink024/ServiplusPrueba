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
   public class RepositoryProduct : BaseRepository, IRepositoryGET<EntityProduct>, IRepositorySET<EntityProduct>
    {
        public EntityProduct Get(int Id)
        {
            var data = base.DataContext.Product.Where(p => p.PK_ProductID == Id);
            if (data.Count() == 1)
                return FactoryProduct.Get(data.Single());
            else
                return null;
        }

        public List<EntityProduct> GetActives()
        {
            return FactoryProduct.GetList(base.DataContext.Product.Where(p => p.Status == true).ToList());
        }

        public List<EntityProduct> GetAll()
        {
            return FactoryProduct.GetList(base.DataContext.Product.ToList());
        }

        public EntityProduct Insert(EntityProduct data)
        {
            try
            {
                Product dataNew = new Product()
                {
                    PK_ProductID = data.PK_ProductID,
                    Model = data.Model,
                    ProductName = data.ProductName,
                    BarCode = data.BarCode,
                    SaleOrganization = data.SaleOrganization,
                    DistributionChannel = data.DistributionChannel,
                    Center = data.Center,
                    MaterialGroup1 = data.MaterialGroup1,
                    MaterialGroup4 = data.MaterialGroup4,
                    ProductType = data.ProductType,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Product.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_ProductID = dataNew.PK_ProductID;

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

        public void BulkInsert(List<EntityProduct> data)
        {
            try
            {

                base.DataContext.BulkInsert<Product>(data.Select(p => new Product()
                {
                    PK_ProductID = p.PK_ProductID,
                    Model = p.Model,
                    ProductName = p.ProductName,
                    BarCode = p.BarCode,
                    SaleOrganization = p.SaleOrganization,
                    DistributionChannel = p.DistributionChannel,
                    Center = p.Center,
                    MaterialGroup1 = p.MaterialGroup1,
                    MaterialGroup4 = p.MaterialGroup4,
                    ProductType = p.ProductType,
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

        public void BulkMerge(List<EntityProduct> data)
        {

            base.DataContext.BulkMerge<Product>(data.Select(p => new Product()
            {
                PK_ProductID = p.PK_ProductID,
                Model = p.Model,
                ProductName = p.ProductName,
                BarCode = p.BarCode,
                SaleOrganization = p.SaleOrganization,
                DistributionChannel = p.DistributionChannel,
                Center = p.Center,
                MaterialGroup1 = p.MaterialGroup1,
                MaterialGroup4 = p.MaterialGroup4,
                ProductType = p.ProductType,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }), options => options.ColumnPrimaryKeyExpression = c => new { c.Model });
        }


        public void BulkUpdate(List<EntityProduct> data)
        {
            try
            {

                base.DataContext.BulkUpdate<Product>(data.Select(p => new Product()
                {
                    PK_ProductID = p.PK_ProductID,
                    Model = p.Model,
                    ProductName = p.ProductName,
                    BarCode = p.BarCode,
                    SaleOrganization = p.SaleOrganization,
                    DistributionChannel = p.DistributionChannel,
                    Center = p.Center,
                    MaterialGroup1 = p.MaterialGroup1,
                    MaterialGroup4 = p.MaterialGroup4,
                    ProductType = p.ProductType,
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
        public EntityProduct Update(EntityProduct data)
        {
            try
            {
                var dataUpdate = base.DataContext.Product.Where(p => p.PK_ProductID == data.PK_ProductID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.Model = data.Model;
                    dataUpdate.ProductName = data.ProductName;
                    dataUpdate.BarCode = data.BarCode;
                    dataUpdate.SaleOrganization = data.SaleOrganization;
                    dataUpdate.DistributionChannel = data.DistributionChannel;
                    dataUpdate.Center = data.Center;
                    dataUpdate.MaterialGroup1 = data.MaterialGroup1;
                    dataUpdate.MaterialGroup4 = data.MaterialGroup4;
                    dataUpdate.ProductType = data.ProductType;
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


        public EntityProduct GetByModel(string Model)
        {
            var data = base.DataContext.Product.Where(p => p.Model == Model);
            if (data.Count() == 1)
                return FactoryProduct.Get(data.Single());
            else
                return null;
        }
        public List<EntityProduct> GetByIDs(List<string> IDs_Products)
        {

            return FactoryProduct.GetList(base.DataContext.Product.Where(p => IDs_Products.Contains(p.Model)).ToList());

        }
        

    }
}
