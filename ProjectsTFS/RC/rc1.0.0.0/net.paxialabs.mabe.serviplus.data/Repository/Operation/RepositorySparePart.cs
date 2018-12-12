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

    public class RepositorySparePart : BaseRepository, IRepositoryGET<EntitySparePart>, IRepositorySET<EntitySparePart>
    {
        public EntitySparePart Get(int Id)
        {
            var data = base.DataContext.SpareParts.Where(p => p.PK_SparePartsID == Id);
            if (data.Count() == 1)
                return FactorySparePart.Get(data.Single());
            else
                return null;
        }

        public List<EntitySparePart> GetListByRefManID(int OrderID, string RefManID)
        {
            return FactorySparePart.GetList(base.DataContext.SpareParts.Where(p => p.FK_OrderID == OrderID && p.RefManID == RefManID).ToList());           
        }

        public EntitySparePart GetByRefManID (int OrderID, string RefManID)
        {
            var data = base.DataContext.SpareParts.Where(p => p.FK_OrderID == OrderID && p.RefManID== RefManID);
            if (data.Count() == 1)
                return FactorySparePart.Get(data.Single());
            else
                return null;
        }

        public EntitySparePart GetByRefManID(int OrderID, string RefManID, string Estatus)
        {
            var data = base.DataContext.SpareParts.Where(p => p.FK_OrderID == OrderID && p.RefManID == RefManID && p.SparePartStatus == Estatus);
            if (data.Count() == 1)
                return FactorySparePart.Get(data.Single());
            else
                return null;
        }

        public List<ModelViewSparePartsODS> GetByOrderID(int OrderID)
        {
            return FactorySparePartODS.GetList(base.DataContext.SpareParts.Where(p => p.FK_OrderID == OrderID).ToList());
        }

        public List<EntitySparePart> GetByPK_OrderID(int OrderID)
        {
            return FactorySparePart.GetList(base.DataContext.SpareParts.Where(p => p.FK_OrderID == OrderID).ToList());
        }

        public List<EntitySparePart> Get(int OrderID, string SparePartsID)
        {
            return FactorySparePart.GetList(base.DataContext.SpareParts.Where(p => p.FK_OrderID == OrderID && p.BuildOfMaterials.SparePartsID == SparePartsID).ToList());
        }

        public List<EntitySparePart> GetByOrder(string OrderID)
        {
            return FactorySparePart.GetList(base.DataContext.SpareParts.Where(p => p.Orders.OrderID == OrderID).OrderBy(p => p.CreateDate).ToList());
        }

        public List<EntitySparePart> GetActives()
        {
            return FactorySparePart.GetList(base.DataContext.SpareParts.Where(p => p.Status == true).ToList());
        }

        public List<EntitySparePart> GetAll()
        {
            return FactorySparePart.GetList(base.DataContext.SpareParts.ToList());
        }
        
        public List<EntitySparePart> GetAllByOrderID(int OrderID)
        {
            return FactorySparePart.GetList(base.DataContext.SpareParts.Where(p => p.FK_OrderID == OrderID).ToList());
        }
        public EntitySparePart Insert(EntitySparePart data)
        {
            try
            {
                SpareParts dataNew = new SpareParts()
                {
                    PK_SparePartsID = data.PK_SparePartsID,
                    FK_OrderID = data.FK_OrderID,
                    FK_BuildOfMaterialsID = data.FK_BuildOfMaterialsID,
                    FK_ProductID = data.FK_ProductID,
                    FK_WorkforceID = data.FK_WorkforceID,
                    RefManID = data.RefManID,
                    SparePartDescription = data.ItemRefMan,
                    Quantity = data.Quantity,
                    Price = data.Price,
                    StatusSchema = data.StatusSchema,
                    Status = data.Status,
                    PosicionItem = data.PosicionItem,
                    SparePartStatus = data.SparePartStatus,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                   
                };
                base.DataContext.SpareParts.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_SparePartsID = dataNew.PK_SparePartsID;
                
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

        public EntitySparePart Update(EntitySparePart data)
        {
            try
            {
                var dataUpdate = base.DataContext.SpareParts.Where(p => p.PK_SparePartsID == data.PK_SparePartsID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.FK_OrderID = data.FK_OrderID;
                    dataUpdate.FK_BuildOfMaterialsID = data.FK_BuildOfMaterialsID;
                    dataUpdate.FK_ProductID = data.FK_ProductID;
                    dataUpdate.FK_WorkforceID = data.FK_WorkforceID;
                    dataUpdate.RefManID = data.RefManID;
                    dataUpdate.SparePartDescription = data.ItemRefMan;
                    dataUpdate.Quantity = data.Quantity;
                    dataUpdate.Price = data.Price;
                    dataUpdate.Status = data.Status;
                    dataUpdate.PosicionItem = data.PosicionItem;
                    dataUpdate.StatusSchema = data.StatusSchema;
                    dataUpdate.SparePartStatus = data.SparePartStatus;
                    //dataUpdate.CreateDate = data.CreateDate;
                    dataUpdate.ModifyDate = DateTime.UtcNow;


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

        public void Delete(List<EntitySparePart> data)
        {
            try
            {

                foreach (var item in data)
                {
                    var dataUpdate = base.DataContext.SpareParts.Where(p => p.PK_SparePartsID == item.PK_SparePartsID).SingleOrDefault();
                    base.DataContext.Entry(dataUpdate).State = EntityState.Deleted;
                    base.DataContext.SaveChanges();
                }
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
