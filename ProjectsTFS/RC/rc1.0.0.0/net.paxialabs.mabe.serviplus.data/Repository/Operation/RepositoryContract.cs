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
    public  class RepositoryContract: BaseRepository , IRepositoryGET<EntityContract>,IRepositorySET<EntityContract>
    {
        public EntityContract Get(int Id)
        {
            var data = base.DataContext.ContratReceipt.Where(p => p.PK_ContratID == Id);
            if (data.Count() == 1)
                return FactoryContrat.Get(data.Single());
            else
                return null;
        }
        public List<EntityContract> GetActives()
        {
            return FactoryContrat.GetList(base.DataContext.ContratReceipt.Where(p => p.Status == true).ToList());
        }

        public List<EntityContract> GetAll()
        {
            return FactoryContrat.GetList(base.DataContext.ContratReceipt.ToList());
        }

        public EntityContract GetByOrderFolio(int OrderID, string Folio)
        {
            var data = base.DataContext.ContratReceipt.Where(p => p.Fk_OrderID == OrderID && p.Folio == Folio);
            if (data.Count() == 1)
                return FactoryContrat.Get(data.Single());
            else
                return null;
        }

        public EntityContract Insert(EntityContract data)
        {
            try
            {
                ContratReceipt dataNew = new ContratReceipt()
                {
                    PK_ContratID = data.PK_ContractID,
                    Fk_OrderID = data.Fk_OrderID,
                    Folio = data.Folio,
                    Ruta = data.Ruta,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.ContratReceipt.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_ContractID = dataNew.PK_ContratID;

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

        public EntityContract Update(EntityContract data)
        {
            try
            {
                var dataUpdate = base.DataContext.ContratReceipt.Where(p => p.PK_ContratID == data.PK_ContractID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    
                    dataUpdate.PK_ContratID= data.PK_ContractID;
                    dataUpdate.Fk_OrderID = data.Fk_OrderID;
                    dataUpdate.Folio = data.Folio;
                    dataUpdate.Ruta = data.Ruta;
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
