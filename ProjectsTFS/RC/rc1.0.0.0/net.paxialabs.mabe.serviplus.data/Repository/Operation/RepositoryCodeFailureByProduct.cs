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

    public class RepositoryCodeFailureByProduct : BaseRepository, IRepositoryGET<EntityCodeFailureByProduct>, IRepositorySET<EntityCodeFailureByProduct>
    {
        public EntityCodeFailureByProduct Get(int Id)
        {
            var data = base.DataContext.CodeFailureByProduct.Where(p => p.FK_CodeFailureID == Id);
            if (data.Count() == 1)
                return FactoryCodeFailureByProduct.Get(data.Single());
            else
                return null;
        }

        public List<EntityCodeFailureByProduct> GetByProductID(int Id)
        {
            return FactoryCodeFailureByProduct.GetList(base.DataContext.CodeFailureByProduct.Where(p => p.FK_ProductID == Id).ToList());
        }

        public EntityCodeFailureByProduct GetFailureProduct(int FailureID, int ProductID)
        {
            var data = base.DataContext.CodeFailureByProduct.Where(p => p.FK_CodeFailureID == FailureID && p.FK_ProductID == ProductID);
            if (data.Count() == 1)
                return FactoryCodeFailureByProduct.Get(data.Single());
            else
                return null;
        }

        public List<EntityCodeFailureByProduct> GetActives()
        {
            return FactoryCodeFailureByProduct.GetList(base.DataContext.CodeFailureByProduct.Where(p => p.Status == true).ToList());
        }

        public List<EntityCodeFailureByProduct> GetAll()
        {
            return FactoryCodeFailureByProduct.GetList(base.DataContext.CodeFailureByProduct.ToList());
        }

        public EntityCodeFailureByProduct Insert(EntityCodeFailureByProduct data)
        {
            try
            {
                CodeFailureByProduct dataNew = new CodeFailureByProduct()
                {
                    FK_CodeFailureID = data.FK_CodeFailureID,
                    FK_ProductID = data.FK_ProductID,
                    Complexity = data.Complexity,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.CodeFailureByProduct.Add(dataNew);
                base.DataContext.SaveChanges();

                data.FK_CodeFailureID = dataNew.FK_CodeFailureID;

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

        public void BulkInsert(List<EntityCodeFailureByProduct> data)
        {
            try
            {

                base.DataContext.BulkInsert<CodeFailureByProduct>(data.Select(p => new CodeFailureByProduct()
                {
                    FK_CodeFailureID = p.FK_CodeFailureID,
                    FK_ProductID = p.FK_ProductID,
                    Complexity = p.Complexity,
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


        public void BulkUpdate(List<EntityCodeFailureByProduct> data)
        {
            try
            {

                base.DataContext.BulkUpdate<CodeFailureByProduct>(data.Select(p => new CodeFailureByProduct()
                {
                    FK_CodeFailureID = p.FK_CodeFailureID,
                    FK_ProductID = p.FK_ProductID,
                    Complexity = p.Complexity,
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

        public EntityCodeFailureByProduct Update(EntityCodeFailureByProduct data)
        {
            try
            {
                var dataUpdate = base.DataContext.CodeFailureByProduct.Where(p => p.FK_CodeFailureID == data.FK_CodeFailureID && p.FK_ProductID == data.FK_ProductID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    //dataUpdate.FK_CodeFailureID = data.FK_CodeFailureID;
                    dataUpdate.FK_ProductID = data.FK_ProductID;
                    dataUpdate.Complexity = data.Complexity;
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
