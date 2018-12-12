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
    public class RepositoryValidationGuarantyBOM : BaseRepository, IRepositoryGET<EntityValidationGuarantyBOM>, IRepositorySET<EntityValidationGuarantyBOM>
    {
        public EntityValidationGuarantyBOM Get(int Id)
        {
            var data = base.DataContext.ValidationGuarantyBOM.Where(p => p.PK_ValidationGuarantySparePartID == Id);
            if (data.Count() == 1)
                return FactoryValidationGuarantyBOM.Get(data.Single());
            else
                return null;
        }

        public List<EntityValidationGuarantyBOM> GetActives()
        {
            return FactoryValidationGuarantyBOM.GetList(base.DataContext.ValidationGuarantyBOM.Where(p => p.Status == true).ToList());
        }

        public List<EntityValidationGuarantyBOM> GetAll()
        {
            return FactoryValidationGuarantyBOM.GetList(base.DataContext.ValidationGuarantyBOM.ToList());
        }


        public void BulkMerge(List<EntityValidationGuarantyBOM> data)
        {

            base.DataContext.BulkMerge<ValidationGuarantyBOM>(data.Select(p => new ValidationGuarantyBOM()
            {
                 PK_ValidationGuarantySparePartID = 0,
                 FK_ProducID = p.FK_ProducID,
                 FK_BuildOfMaterialsID = p.FK_BuildOfMaterialsID,
                 Model = p.Model,
                 SalesOrganization = p.SalesOrganization,
                 SparePartsID = p.SparePartsID,
                 ClientID = p.ClientID,
                 Months = p.Months,
                 ValidFrom = p.ValidFrom,
                 ValidTo = p.ValidTo,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }), options => options.ColumnPrimaryKeyExpression = c => new { c.FK_BuildOfMaterialsID, c.FK_ProducID, c.SalesOrganization });
        }
        public EntityValidationGuarantyBOM Insert(EntityValidationGuarantyBOM data)
        {
            try
            {
                ValidationGuarantyBOM dataNew = new ValidationGuarantyBOM()
                {
                    PK_ValidationGuarantySparePartID = data.PK_ValidationGuarantySparePartID,
                    FK_BuildOfMaterialsID = data.FK_BuildOfMaterialsID,
                    FK_ProducID = data.FK_ProducID,
                    Model = data.Model,
                    SalesOrganization = data.SalesOrganization,
                    SparePartsID = data.SparePartsID,
                    ClientID = data.ClientID,
                    Months = data.Months,
                    ValidFrom = data.ValidFrom,
                    ValidTo = data.ValidTo,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.ValidationGuarantyBOM.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_ValidationGuarantySparePartID = dataNew.PK_ValidationGuarantySparePartID;

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

        public EntityValidationGuarantyBOM Update(EntityValidationGuarantyBOM data)
        {
            try
            {
                var dataUpdate = base.DataContext.ValidationGuarantyBOM.Where(p => p.PK_ValidationGuarantySparePartID == data.PK_ValidationGuarantySparePartID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_ValidationGuarantySparePartID = data.PK_ValidationGuarantySparePartID;
                    //dataUpdate.FK_BuildOfMaterialsID = data.FK_BuildOfMaterialsID;
                    //dataUpdate.FK_ProducID = data.FK_ProducID;
                    dataUpdate.Model = data.Model;
                    dataUpdate.SalesOrganization = data.SalesOrganization;
                    dataUpdate.SparePartsID = data.SparePartsID;
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
