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
    public class RepositoryHistory : BaseRepository, IRepositoryGET<EntityHistory>, IRepositorySET<EntityHistory>
    {
        public EntityHistory Get(int Id)
        {
            var data = base.DataContext.History.Where(p => p.PK_HistoryID == Id);
            if (data.Count() == 1)
                return FactoryHistory.Get(data.Single());
            else
                return null;
        }

        public List<EntityHistory> GetActives()
        {
            return FactoryHistory.GetList(base.DataContext.History.Where(p => p.Status == true).ToList());
        }

        public List<EntityHistory> GetAll()
        {
            return FactoryHistory.GetList(base.DataContext.History.ToList());
        }

        public EntityHistory Insert(EntityHistory data)
        {
            try
            {
                History dataNew = new History()
                {
                    PK_HistoryID = data.PK_HistoryID,
                    FK_InstalledBaseID = data.FK_InstalledBaseID,
                    FK_ClientID = data.FK_ClientID,
                    FK_OrderID = data.FK_OrderID,
                    OrderID = data.OrderID,
                    OrderStatus = data.OrderStatus,
                    ItemStatus = data.ItemStatus,
                    Guaranty = data.Guaranty,
                    ShopDate = data.ShopDate,
                    CloseDate = data.CloseDate,
                    FailureID1 = data.FailureID1,
                    Failure1 = data.Failure1,
                    FailureID2 = data.FailureID2,
                    Failure2 = data.Failure2,
                    FailureID3 = data.FailureID3,
                    Failure3 = data.Failure3,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.History.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_HistoryID = dataNew.PK_HistoryID;

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

        public EntityHistory Update(EntityHistory data)
        {
            try
            {
                var dataUpdate = base.DataContext.History.Where(p => p.PK_HistoryID == data.PK_HistoryID).SingleOrDefault();

                if (dataUpdate != null)
                {


                    dataUpdate.PK_HistoryID = data.PK_HistoryID;
                    dataUpdate.FK_InstalledBaseID = data.FK_InstalledBaseID;
                    dataUpdate.FK_ClientID = data.FK_ClientID;
                    dataUpdate.FK_OrderID = data.FK_OrderID;
                    dataUpdate.OrderID = data.OrderID;
                    dataUpdate.OrderStatus = data.OrderStatus;
                    dataUpdate.ItemStatus = data.ItemStatus;
                    dataUpdate.Guaranty = data.Guaranty;
                    dataUpdate.ShopDate = data.ShopDate;
                    dataUpdate.CloseDate = data.CloseDate;
                    dataUpdate.FailureID1 = data.FailureID1;
                    dataUpdate.Failure1 = data.Failure1;
                    dataUpdate.FailureID2 = data.FailureID2;
                    dataUpdate.Failure2 = data.Failure2;
                    dataUpdate.FailureID3 = data.FailureID3;
                    dataUpdate.Failure3 = data.Failure3;
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

        public List<EntityHistory> GetByOrderID(string OrderID)
        {
            return FactoryHistory.GetList(base.DataContext.History.Where(p => p.OrderID == OrderID).ToList());
        }

        public List<EntityHistory> GetByOrderID(int OrderID)
        {
            return FactoryHistory.GetList(base.DataContext.History.Where(p => p.FK_OrderID == OrderID).ToList());
        }

        public List<HistoricProduct> GetList(int OrderID)
        {
            try
            {
                base.DataContext.Database.CommandTimeout = 10800;


                return  base.DataContext.HProduct(OrderID).Select(p => new HistoricProduct()
                {
                    ODSID = OrderID.ToString(),
                    ODS = p.OrderID,
                    Status = p.Status.ToString(),
                    CloseDate = p.CloseDate.ToString("yyyy-MM-dd"),
                    Faults = p.Faults

                }
                ).ToList();


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
