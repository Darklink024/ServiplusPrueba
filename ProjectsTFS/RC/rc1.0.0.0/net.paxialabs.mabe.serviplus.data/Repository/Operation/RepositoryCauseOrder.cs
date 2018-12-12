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
    public class RepositoryCauseOrder : BaseRepository, IRepositoryGET<EntityCauseOrder>, IRepositorySET<EntityCauseOrder>
    {
        public EntityCauseOrder Get(int Id)
        {
            var data = base.DataContext.CauseOrder.Where(p => p.PK_CauseOrderID == Id);
            if (data.Count() == 1)
                return FactoryCauseOrder.Get(data.Single());
            else
                return null;
        }

        public List<EntityCauseOrder> GetActives()
        {
            return FactoryCauseOrder.GetList(base.DataContext.CauseOrder.Where(p => p.Status == true).ToList());
        }

        public List<EntityCauseOrder> GetAll()
        {
            return FactoryCauseOrder.GetList(base.DataContext.CauseOrder.ToList());
        }

        public EntityCauseOrder Insert(EntityCauseOrder data)
        {
            try
            {
                CauseOrder dataNew = new CauseOrder()
                {
                    PK_CauseOrderID = data.PK_CauseOrderID,
                    FK_StatusOrderID = data.FK_StatusOrderID,
                    CauseOrder1 = data.CauseOrder1,
                    Moment = data.Moment,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.CauseOrder.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_CauseOrderID = dataNew.PK_CauseOrderID;

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

        public EntityCauseOrder Update(EntityCauseOrder data)
        {
            try
            {
                var dataUpdate = base.DataContext.CauseOrder.Where(p => p.PK_CauseOrderID == data.PK_CauseOrderID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_CauseOrderID = data.PK_CauseOrderID;
                    //dataUpdate.FK_StatusOrderID = data.FK_StatusOrderID;
                    dataUpdate.CauseOrder1 = data.CauseOrder1;
                    dataUpdate.Moment = data.Moment;
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
