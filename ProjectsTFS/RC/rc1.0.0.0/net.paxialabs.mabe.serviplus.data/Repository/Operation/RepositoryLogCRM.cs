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

    public class RepositoryLogCRM : BaseRepository, IRepositoryGET<EntityLogCRM>, IRepositorySET<EntityLogCRM>
    {
        public EntityLogCRM Get(int Id)
        {
            var data = base.DataContext.LogCRM.Where(p => p.PK_LogID == Id);
            if (data.Count() == 1)
                return FactoryLogCRM.Get(data.Single());
            else
                return null;
        }

        public List<EntityLogCRM> GetActives()
        {
            return FactoryLogCRM.GetList(base.DataContext.LogCRM.Where(p => p.Status == true).ToList());
        }

        public List<EntityLogCRM> GetAll()
        {
            return FactoryLogCRM.GetList(base.DataContext.LogCRM.ToList());
        }

        public EntityLogCRM Insert(EntityLogCRM data)
        {
            try
            {
                LogCRM dataNew = new LogCRM()
                {
                    PK_LogID = data.LogID,
                    FK_OrderID = data.OrderID,
                    UpdateBase = data.UpdateBase,
                    UpdateRefMan = data.UpdateRefMan,
                    UpdateODS = data.UpdateODS,
                    ModuleReserveSP = data.ModuleReserveSP,
                    ADRReserveSP = data.ADRReserveSP,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.LogCRM.Add(dataNew);
                base.DataContext.SaveChanges();

                data.LogID = dataNew.PK_LogID;

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

        public EntityLogCRM Update(EntityLogCRM data)
        {
            try
            {
                var dataUpdate = base.DataContext.LogCRM.Where(p => p.PK_LogID == data.LogID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.PK_LogID = data.LogID;
                    dataUpdate.FK_OrderID = data.OrderID;
                    dataUpdate.UpdateBase = data.UpdateBase;
                    dataUpdate.UpdateRefMan = data.UpdateRefMan;
                    dataUpdate.UpdateODS = data.UpdateODS;
                    dataUpdate.ModuleReserveSP = data.ModuleReserveSP;
                    dataUpdate.ADRReserveSP = data.ADRReserveSP;
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

        public EntityLogCRM GetByOrderID(int Id)
        {
            var data = base.DataContext.LogCRM.Where(p => p.FK_OrderID == Id);
            if (data.Count() == 1)
                return FactoryLogCRM.Get(data.Single());
            else
                return null;
        }

    }
}
