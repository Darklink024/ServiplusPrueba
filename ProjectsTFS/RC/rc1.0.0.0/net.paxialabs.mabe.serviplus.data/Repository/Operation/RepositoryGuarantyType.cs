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
          public class RepositoryGuarantyType : BaseRepository, IRepositoryGET<EntityGuarantyType>, IRepositorySET<EntityGuarantyType>
    {
        public EntityGuarantyType Get(int Id)
        {
            var data = base.DataContext.GuarantyType.Where(p => p.PK_GuarantyTypeID == Id);
            if (data.Count() == 1)
                return FactoryGuarantyType.Get(data.Single());
            else
                return null;
        }

        public List<EntityGuarantyType> GetActives()
        {
            return FactoryGuarantyType.GetList(base.DataContext.GuarantyType.Where(p => p.Status == true).ToList());
        }

        public List<EntityGuarantyType> GetAll()
        {
            return FactoryGuarantyType.GetList(base.DataContext.GuarantyType.ToList());
        }

        public EntityGuarantyType Insert(EntityGuarantyType data)
        {
            try
            {
                GuarantyType dataNew = new GuarantyType()
                {
                   
                    PK_GuarantyTypeID = data.PK_GuarantyTypeID,
                    GuarantyType1 = data.GuarantyType1,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.GuarantyType.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_GuarantyTypeID = dataNew.PK_GuarantyTypeID;

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

        public EntityGuarantyType Update(EntityGuarantyType data)
        {
            try
            {
                var dataUpdate = base.DataContext.GuarantyType.Where(p => p.PK_GuarantyTypeID == data.PK_GuarantyTypeID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_GuarantyTypeID = data.PK_GuarantyTypeID;
                    dataUpdate.GuarantyType1 = data.GuarantyType1;
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
