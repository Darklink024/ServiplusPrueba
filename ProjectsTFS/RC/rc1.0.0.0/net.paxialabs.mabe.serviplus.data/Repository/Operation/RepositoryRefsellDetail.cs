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
    public class RepositoryRefsellDetail: BaseRepository, IRepositoryGET<EntityRefsellDetail>,IRepositorySET<EntityRefsellDetail>
    {
        public EntityRefsellDetail Get(int Id)
        {
            var data = base.DataContext.RefsellQuotation.Where(p => p.PK_RefsellDetailID == Id);
            if (data.Count() == 1)
                return FactoryRefsellDetail.Get(data.Single());
            else
                return null;
        }
        public EntityRefsellDetail GetDetail(int FK_QuotationID, int FK_BuildOfMaterialsID)
        {
            var data = base.DataContext.RefsellQuotation.Where(p => p.Fk_QuotationID == FK_QuotationID && p.Fk_ProductID == FK_BuildOfMaterialsID);
            if (data.Count() == 1)
                return FactoryRefsellDetail.Get(data.Single());
            else
                return null;
        }
        public List<EntityRefsellDetail> GetByID(int Fk_QuotationDetail)
        {
            return FactoryRefsellDetail.GetList(base.DataContext.RefsellQuotation.Where(p => p.Fk_QuotationID == Fk_QuotationDetail).OrderBy(p=>p.Origen).ToList());
        }
        public List<EntityRefsellDetail> GetAll()
        {
            return FactoryRefsellDetail.GetList(base.DataContext.RefsellQuotation.ToList());
        }
        public List<EntityRefsellDetail> GetActives()
        {
            return FactoryRefsellDetail.GetList(base.DataContext.RefsellQuotation.Where(p => p.Status == true).ToList());
        }

        public EntityRefsellDetail Insert(EntityRefsellDetail data)
        {
            try
            {
                RefsellQuotation dataNew = new RefsellQuotation()
                {
                    PK_RefsellDetailID = data.PK_RefsellDetailID,
                    Fk_QuotationID = data.Fk_QuotationID,
                    Fk_ProductID = data.Fk_ProductID,
                    Cantidad=data.Cantidad,
                    RefMan=data.RefMan,
                    Origen=data.Origen,
                    CostoRef=data.CostoRef,
                    Flete=data.Flete,
                    CostoFlete=data.CostoFlete,
                    OrdenVenta=data.OrdenVenta,

                    
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.RefsellQuotation.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_RefsellDetailID = dataNew.PK_RefsellDetailID;

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

        public EntityRefsellDetail Update(EntityRefsellDetail data)
        {
            try
            {
                var dataUpdate = base.DataContext.RefsellQuotation.Where(p => p.PK_RefsellDetailID == data.PK_RefsellDetailID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.PK_RefsellDetailID = data.PK_RefsellDetailID;
                    dataUpdate.Fk_QuotationID = data.Fk_QuotationID;
                    dataUpdate.Fk_ProductID = data.Fk_ProductID;
                    dataUpdate.Cantidad = data.Cantidad;
                    dataUpdate.RefMan = data.RefMan;
                    dataUpdate.Origen = data.Origen;
                    dataUpdate.CostoRef = data.CostoRef;
                    dataUpdate.Flete = data.Flete;
                    dataUpdate.CostoFlete = data.CostoFlete;
                    dataUpdate.OrdenVenta = data.OrdenVenta;
                    
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
