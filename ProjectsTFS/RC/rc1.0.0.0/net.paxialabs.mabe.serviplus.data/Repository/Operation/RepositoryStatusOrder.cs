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
    public class RepositoryStatusOrder : BaseRepository, IRepositoryGET<EntityStatusOrder>, IRepositorySET<EntityStatusOrder>
    {
        public EntityStatusOrder Get(int Id)
        {
            var data = base.DataContext.StatusOrder.Where(p => p.PK_StatusOrderID == Id);
            if (data.Count() == 1)
                return FactoryStatusOrder.Get(data.Single());
            else
                return null;
        }

        public List<EntityStatusOrder> GetActives()
        {
            return FactoryStatusOrder.GetList(base.DataContext.StatusOrder.Where(p => p.Status == true).ToList());
        }

        public List<EntityStatusOrder> GetAll()
        {
            return FactoryStatusOrder.GetList(base.DataContext.StatusOrder.ToList());
        }

        public EntityStatusOrder Insert(EntityStatusOrder data)
        {
            try
            {
                StatusOrder dataNew = new StatusOrder()
                {
                    PK_StatusOrderID = data.PK_StatusOrderID,
                    StatusOrder1 = data.StatusOrder1,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.StatusOrder.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_StatusOrderID = dataNew.PK_StatusOrderID;

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

        public EntityStatusOrder Update(EntityStatusOrder data)
        {
            try
            {
                var dataUpdate = base.DataContext.StatusOrder.Where(p => p.PK_StatusOrderID == data.PK_StatusOrderID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.PK_StatusOrderID = data.PK_StatusOrderID;
                    dataUpdate.StatusOrder1 = data.StatusOrder1;
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
