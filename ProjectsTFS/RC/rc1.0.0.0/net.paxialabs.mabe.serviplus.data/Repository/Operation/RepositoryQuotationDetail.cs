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
     public class RepositoryQuotationDetail : BaseRepository, IRepositoryGET<EntityQuotationDetail>, IRepositorySET<EntityQuotationDetail>

    {
        public EntityQuotationDetail Get(int Id)
        {
            var data = base.DataContext.QuotationDetail.Where(p => p.PK_QuotationDetailID == Id);
            if (data.Count() == 1)
                return FactoryQuotationDetail.Get(data.Single());
            else
                return null;
        }

        public EntityQuotationDetail GetDetail(int FK_QuotationID, int FK_BuildOfMaterialsID)
        {
            var data = base.DataContext.QuotationDetail.Where(p => p.FK_QuotationID == FK_QuotationID && p.FK_itemID== FK_BuildOfMaterialsID);
            if (data.Count() == 1)
                return FactoryQuotationDetail.Get(data.Single());
            else
                return null;
        }

        public List<EntityQuotationDetail> GetActives()
        {
            return FactoryQuotationDetail.GetList(base.DataContext.QuotationDetail.Where(p => p.Status == true).ToList());
        }

        public List<EntityQuotationDetail> GetByID(int Fk_QuotationDetail)
        {
            return FactoryQuotationDetail.GetList(base.DataContext.QuotationDetail.Where(p => p.PK_QuotationDetailID == Fk_QuotationDetail).ToList());
        }

        public List<EntityQuotationDetail> GetAll()
        {
            return FactoryQuotationDetail.GetList(base.DataContext.QuotationDetail.ToList());
        }

        public EntityQuotationDetail Insert(EntityQuotationDetail data)
        {
            try
            {
                QuotationDetail dataNew = new QuotationDetail()
                {
                    PK_QuotationDetailID = data.PK_QuotationDetailID,
                    FK_QuotationID = data.FK_QuotationID,
                    FK_itemID = data.FK_itemID,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.QuotationDetail.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_QuotationDetailID = dataNew.PK_QuotationDetailID;

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

        public EntityQuotationDetail Update(EntityQuotationDetail data)
        {
            try
            {
                var dataUpdate = base.DataContext.QuotationDetail.Where(p => p.PK_QuotationDetailID == data.PK_QuotationDetailID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.PK_QuotationDetailID = data.PK_QuotationDetailID;
                    dataUpdate.FK_QuotationID = data.FK_QuotationID;
                    dataUpdate.FK_itemID = data.FK_itemID;
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
