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
    public class RepositoryRefSell: BaseRepository, IRepositoryGET<EntityRefSell>,IRepositorySET<EntityRefSell>
    {
        public EntityRefSell Get(int Id)
        {
            var data = base.DataContext.RefSell.Where(p => p.PK_RefSellID == Id);
            if (data.Count() == 1)
                return FactoryRefSell.Get(data.Single());
            else
                return null;
        }
        public EntityRefSell GetRefFolio(int OrderID, string Folio)
        {
            var data = base.DataContext.RefSell.Where(p => p.FK_OrderID == OrderID && p.IDRefSell == Folio);
            if (data.Count() > 0)
                return FactoryRefSell.Get(data.First());
            else
                return null;
        }
        public List<EntityRefSell> GetAll()
        {
            return FactoryRefSell.GetList(base.DataContext.RefSell.ToList());
        }
        public List<EntityRefSell> GetActives()
        {
            return FactoryRefSell.GetList(base.DataContext.RefSell.Where(p => p.Status == true).ToList());
        }

        public EntityRefSell GetByOrder(int OrderID)
        {
            var data = base.DataContext.RefSell.Where(p => p.FK_OrderID == OrderID);
            if (data.Count() == 1)
                return FactoryRefSell.Get(data.Single());
            else
                return null;
        }
        public List<EntityRefSell> GetByEmpoyeeDate(int UserID, DateTime date)
        {
            return FactoryRefSell.GetList(base.DataContext.RefSell.Where(p => p.FK_EmployeeID == UserID && DbFunctions.TruncateTime(p.CreateDate) == date).OrderByDescending(o=>o.CreateDate).ToList());
        }
        public EntityRefSell Insert(EntityRefSell data)
        {
            try
            {
                RefSell datanew = new RefSell()
                {
                   PK_RefSellID = data.PK_RefSellID,
                    FK_OrderID = data.FK_OrderID,
                    FK_ClientID = data.FK_ClientID,
                    FK_EmployeeID = data.FK_EmployeeID,
                    FK_PaymentID = data.FK_PaymentID,
                    FK_Invoice_ID = data.FK_Invoice_ID,
                    FK_QuotationID = data.FK_QuotationID,
                    FK_ProductID = data.FK_ProductID,
                    FK_ShopPlace=data.FK_ShopPlace,
                    IDRefSell = data.IDRefSell,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate,
                };
                base.DataContext.RefSell.Add(datanew);
                base.DataContext.SaveChanges();

                data.PK_RefSellID = datanew.PK_RefSellID;

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

        public EntityRefSell Update(EntityRefSell data)
        {
            try
            {
                var dataUpdate = base.DataContext.RefSell.Where(p => p.PK_RefSellID == data.PK_RefSellID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.FK_OrderID = data.FK_OrderID;
                    dataUpdate.FK_ClientID = data.FK_ClientID;
                    dataUpdate.FK_EmployeeID = data.FK_EmployeeID;
                    dataUpdate.FK_PaymentID = data.FK_PaymentID;
                    dataUpdate.FK_Invoice_ID = data.FK_Invoice_ID;
                    dataUpdate.FK_QuotationID = data.FK_QuotationID;
                    dataUpdate.FK_ProductID = data.FK_ProductID;
                    dataUpdate.FK_ShopPlace = data.FK_ShopPlace;
                    dataUpdate.IDRefSell = data.IDRefSell;
                    
                    dataUpdate.Status = true;
                    dataUpdate.CreateDate = DateTime.UtcNow;
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




    }
}
