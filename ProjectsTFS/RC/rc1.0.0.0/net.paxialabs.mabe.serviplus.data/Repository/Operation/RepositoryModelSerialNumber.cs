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
    public class RepositoryModelSerialNumber : BaseRepository, IRepositoryGET<EntityModelSerialNumber>, IRepositorySET<EntityModelSerialNumber>
    {
        public EntityModelSerialNumber Get(int Id)
        {
            var data = base.DataContext.ModelSerialNumber.Where(p => p.PK_ModelSerialNumberID == Id);
            if (data.Count() == 1)
                return FactoryModelSerialNumber.Get(data.Single());
            else
                return null;
        }

        public List<EntityModelSerialNumber> GetActives()
        {
            return FactoryModelSerialNumber.GetList(base.DataContext.ModelSerialNumber.Where(p => p.Status == true).ToList());
        }

        public List<EntityModelSerialNumber> GetAll()
        {
            return FactoryModelSerialNumber.GetList(base.DataContext.ModelSerialNumber.ToList());
        }
        public void BulkMerge(List<EntityModelSerialNumber> data)
        {

            base.DataContext.BulkMerge<ModelSerialNumber>(data.Select(p => new ModelSerialNumber()
            {
                       PK_ModelSerialNumberID = 0, 
                       FK_ProducID = p.FK_ProducID,
                       Model = p.Model,
                       ValidationFormatID = p.ValidationFormatID,
                       InitialDate = p.InitialDate,
                       EndDate = p.EndDate,
                       ValidDate = p.ValidDate,
                       Status = p.Status,
                       CreateDate = p.CreateDate,
                       ModifyDate = p.ModifyDate
            }), options => options.ColumnPrimaryKeyExpression = c => new { c.FK_ProducID, c.ValidationFormatID });
        }

        public EntityModelSerialNumber Insert(EntityModelSerialNumber data)
        {
            try
            {
                ModelSerialNumber dataNew = new ModelSerialNumber()
                {
                    PK_ModelSerialNumberID = data.PK_ModelSerialNumberID,
                    FK_ProducID = data.FK_ProducID,
                    Model = data.Model,
                    ValidationFormatID = data.ValidationFormatID,
                    InitialDate = data.InitialDate,
                    EndDate = data.EndDate,
                    ValidDate = data.ValidDate,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.ModelSerialNumber.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_ModelSerialNumberID = dataNew.PK_ModelSerialNumberID;

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

        public EntityModelSerialNumber Update(EntityModelSerialNumber data)
        {
            try
            {
                var dataUpdate = base.DataContext.ModelSerialNumber.Where(p => p.PK_ModelSerialNumberID == data.PK_ModelSerialNumberID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_ModelSerialNumberID = data.PK_ModelSerialNumberID;
                    //dataUpdate.FK_ProducID = data.FK_ProducID;
                    dataUpdate.Model = data.Model;
                    dataUpdate.ValidationFormatID = data.ValidationFormatID;
                    dataUpdate.InitialDate = data.InitialDate;
                    dataUpdate.EndDate = data.EndDate;
                    dataUpdate.ValidDate = data.ValidDate;
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
