using net.paxialabs.mabe.serviplus.data.Factory.Security;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace net.paxialabs.mabe.serviplus.data.Repository.Security
{
    public class RepositoryAudit : BaseRepository, IRepositoryGET<EntityAudit>, IRepositorySET<EntityAudit>
    {
        public EntityAudit Get(int Id)
        {
            var data = base.DataContext.Audit.Where(p => p.AuditID == Id);
            if (data.Count() == 1)
                return FactoryAudit.Get(data.Single());
            else
                return null;
        }

        public List<EntityAudit> GetActives()
        {
            return FactoryAudit.GetList(base.DataContext.Audit.ToList());
        }

        public List<EntityAudit> GetAll()
        {
            return FactoryAudit.GetList(base.DataContext.Audit.ToList());
        }

        public EntityAudit Insert(EntityAudit data)
        {
            try
            {
                Audit dataNew = new Audit()
                {
                    AuditID = data.AuditID,
                    Action = data.Action,
                    DateAudit = data.DateAudit,
                    ModuleID = data.ModuleID,
                    UserID = data.UserID
                };
                base.DataContext.Audit.Add(dataNew);
                base.DataContext.SaveChanges();

                data.UserID = dataNew.UserID;

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

        public EntityAudit Update(EntityAudit data)
        {
            try
            {
                var dataUpdate = base.DataContext.Audit.Where(p => p.AuditID == data.AuditID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.AuditID = data.AuditID;
                    dataUpdate.ModuleID = data.ModuleID;
                    dataUpdate.UserID = data.UserID;
                    dataUpdate.Action = data.Action;
                    dataUpdate.DateAudit = data.DateAudit;
                  
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
