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
    public class RepositoryValidationGuarantyProduct : BaseRepository, IRepositoryGET<EntityValidationGuarantyProduct>, IRepositorySET<EntityValidationGuarantyProduct>
    {
        public EntityValidationGuarantyProduct Get(int Id)
        {
            var data = base.DataContext.ValidationGuarantyProduct.Where(p => p.PK_ValidationGuarantyProductID == Id);
            if (data.Count() == 1)
                return FactoryValidationGuarantyProduct.Get(data.Single());
            else
                return null;
        }

        public List<EntityValidationGuarantyProduct> GetActives()
        {
            return FactoryValidationGuarantyProduct.GetList(base.DataContext.ValidationGuarantyProduct.Where(p => p.Status == true).ToList());
        }

        public List<EntityValidationGuarantyProduct> GetAll()
        {
            return FactoryValidationGuarantyProduct.GetList(base.DataContext.ValidationGuarantyProduct.ToList());
        }

        public void BulkMerge(List<EntityValidationGuarantyProduct> data)
        {

            base.DataContext.BulkMerge<ValidationGuarantyProduct>(data.Select(p => new ValidationGuarantyProduct()
            {
                PK_ValidationGuarantyProductID = 0,
                FK_ProducID = p.FK_ProducID,
                Country = p.Country,
                Model = p.Model,
                ClientID = p.ClientID,
                Months = p.Months,
                ValidFrom = p.ValidFrom,
                ValidTo = p.ValidTo,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }), options => options.ColumnPrimaryKeyExpression = c => new { c.FK_ProducID, c.Country, c.ClientID});
        }

        public EntityValidationGuarantyProduct Insert(EntityValidationGuarantyProduct data)
        {
            try
            {
                ValidationGuarantyProduct dataNew = new ValidationGuarantyProduct()
                {
                    PK_ValidationGuarantyProductID = data.PK_ValidationGuarantyProductID,
                    FK_ProducID = data.FK_ProducID,
                    Country = data.Country,
                    Model = data.Model,
                    ClientID = data.ClientID,
                    Months = data.Months,
                    ValidFrom = data.ValidFrom,
                    ValidTo = data.ValidTo,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.ValidationGuarantyProduct.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_ValidationGuarantyProductID = dataNew.PK_ValidationGuarantyProductID;

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

        public EntityValidationGuarantyProduct Update(EntityValidationGuarantyProduct data)
        {
            try
            {
                var dataUpdate = base.DataContext.ValidationGuarantyProduct.Where(p => p.PK_ValidationGuarantyProductID == data.PK_ValidationGuarantyProductID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_ValidationGuarantyProductID = data.PK_ValidationGuarantyProductID;
                    //dataUpdate.FK_ProducID = data.FK_ProducID;
                    dataUpdate.Country = data.Country;
                    dataUpdate.Model = data.Model;
                    dataUpdate.ClientID = data.ClientID;
                    dataUpdate.Months = data.Months;
                    dataUpdate.ValidFrom = data.ValidFrom;
                    dataUpdate.ValidTo = data.ValidTo;
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
