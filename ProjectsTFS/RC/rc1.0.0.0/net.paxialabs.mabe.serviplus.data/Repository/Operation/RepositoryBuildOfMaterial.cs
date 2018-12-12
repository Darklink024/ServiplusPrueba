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
    static class PagingUtils
    {
        public static IEnumerable<T> Page<T>(this IEnumerable<T> en, int pageSize, int page)
        {
            return en.Skip(page * pageSize).Take(pageSize);
        }
        public static IQueryable<T> Page<T>(this IQueryable<T> en, int pageSize, int page)
        {
            return en.Skip(page * pageSize).Take(pageSize);
        }
    }
    public class RepositoryBuildOfMaterial : BaseRepository, IRepositoryGET<EntityBuildOfMaterial>, IRepositorySET<EntityBuildOfMaterial>
    {

        
        public EntityBuildOfMaterial Get(int Id)
        {
            var data = base.DataContext.BuildOfMaterials.Where(p => p.PK_BuildOfMaterialsID == Id);
            if (data.Count() == 1)
                return FactoryBuildOfMaterial.Get(data.Single());
            else
                return null;
        }

        public List<EntityBuildOfMaterial> GetActives()
        {
            return FactoryBuildOfMaterial.GetList(base.DataContext.BuildOfMaterials.Where(p => p.Status == true).ToList());
        }

        public List<EntityBuildOfMaterial> GetAll()
        {
            this.DataContext.Database.CommandTimeout = 180;
            return FactoryBuildOfMaterial.GetList(base.DataContext.BuildOfMaterials.ToList());
        }

        public EntityBuildOfMaterial GetByBOMID(int PK_BuildOfMaterialsID)
        {
            var data = base.DataContext.BuildOfMaterials.Where(p => p.PK_BuildOfMaterialsID == PK_BuildOfMaterialsID);
            if (data.Count() == 1)
                return FactoryBuildOfMaterial.Get(data.Single());
            else
                return null;
        }


        public List<EntityBuildOfMaterial> GetAllSparePart(List<string> SparePartsID)
        {
            this.DataContext.Database.CommandTimeout = 180;
            return FactoryBuildOfMaterial.GetList(base.DataContext.BuildOfMaterials.Where(p => SparePartsID.Contains(p.SparePartsID)).ToList());
        }


        public List<EntityBuildOfMaterial> GetAllSparePartByModel(List<int> models)
        {
            this.DataContext.Database.CommandTimeout = 180;
            return FactoryBuildOfMaterial.GetList(base.DataContext.BuildOfMaterials.Where(p => models.Contains(p.FK_ProductID)).ToList());
        }


        public void BulkMerge(List<EntityBuildOfMaterial> data)
        {

            base.DataContext.BulkMerge<BuildOfMaterials>(data.Select(p => new BuildOfMaterials()
            {
                PK_BuildOfMaterialsID = p.PK_BuildOfMaterialsID,
                FK_ProductID = p.FK_ProductID,
                Model = p.Model,
                SparePartsID = p.SparePartsID,
                Quantity = p.Quantity,
                StatusBOM = p.StatusBOM,
                SparePartDescription = p.SparePartDescription,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }), options => options.ColumnPrimaryKeyExpression = c => new { c.FK_ProductID, c.Model, c.SparePartsID, c.Quantity });
        }
        

        public EntityBuildOfMaterial Insert(EntityBuildOfMaterial data)
        {
            try
            {
                BuildOfMaterials dataNew = new BuildOfMaterials()
                {
                    PK_BuildOfMaterialsID = data.PK_BuildOfMaterialsID,
                    FK_ProductID = data.FK_ProductID,
                    Model = data.Model,
                    SparePartsID = data.SparePartsID,
                    Quantity = data.Quantity,
                    StatusBOM = data.StatusBOM,
                    SparePartDescription = data.SparePartDescription,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.BuildOfMaterials.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_BuildOfMaterialsID = dataNew.PK_BuildOfMaterialsID;

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

        public void BulkInsert(List<EntityBuildOfMaterial> data)
        {
            try
            {

                base.DataContext.BulkInsert<BuildOfMaterials>(data.Select(p => new BuildOfMaterials()
                {
                    PK_BuildOfMaterialsID = p.PK_BuildOfMaterialsID,
                    FK_ProductID = p.FK_ProductID,
                    Model = p.Model,
                    SparePartsID = p.SparePartsID,
                    Quantity = p.Quantity,
                    StatusBOM = p.StatusBOM,
                    SparePartDescription = p.SparePartDescription,
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

        public void BulkUpdate(List<EntityBuildOfMaterial> data)
        {
            try
            {
                base.DataContext.BulkUpdate<BuildOfMaterials>(data.Select(p => new BuildOfMaterials()
                {
                    PK_BuildOfMaterialsID = p.PK_BuildOfMaterialsID,
                    FK_ProductID = p.FK_ProductID,
                    Model = p.Model,
                    SparePartsID = p.SparePartsID,
                    Quantity = p.Quantity,
                    StatusBOM = p.StatusBOM,
                    SparePartDescription = p.SparePartDescription,
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

        public EntityBuildOfMaterial Update(EntityBuildOfMaterial data)
        {
            try
            {
                var dataUpdate = base.DataContext.BuildOfMaterials.Where(p => p.PK_BuildOfMaterialsID == data.PK_BuildOfMaterialsID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_BuildOfMaterialsID = data.PK_BuildOfMaterialsID;
                    //dataUpdate.FK_ProductID = data.FK_ProductID;
                    dataUpdate.Model = data.Model;
                    dataUpdate.SparePartsID = data.SparePartsID;
                    dataUpdate.Quantity = data.Quantity;
                    dataUpdate.StatusBOM = data.StatusBOM;
                    dataUpdate.SparePartDescription = data.SparePartDescription;
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


        public EntityBuildOfMaterial GetMaterialByModel(string Model)
        {
            var data = base.DataContext.BuildOfMaterials.Where(p => p.Model == Model);
            if (data.Count() == 1)
                return FactoryBuildOfMaterial.Get(data.Single());
            else
                return null;
        }

        public EntityBuildOfMaterial GetMaterialBySparePart(string SparePart, int ProductID)
        {
            var data = base.DataContext.BuildOfMaterials.Where(p => p.SparePartsID == SparePart && p.FK_ProductID == ProductID).Take(1);
            if (data.Count() == 1)
                return FactoryBuildOfMaterial.Get(data.Single());
            else
                return null;
        }
    }
}
