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
    public class RepositoryStatusVisit : BaseRepository, IRepositoryGET<EntityStatusVisit>, IRepositorySET<EntityStatusVisit>
    {
        public EntityStatusVisit Get(int Id)
        {
            var data = base.DataContext.StatusVisit.Where(p => p.PK_StatusVisitID == Id);
            if (data.Count() == 1)
                return FactoryStatusVisit.Get(data.Single());
            else
                return null;
        }

        public List<EntityStatusVisit> GetActives()
        {
            return FactoryStatusVisit.GetList(base.DataContext.StatusVisit.Where(p => p.Status == true).ToList());
        }

        public List<EntityStatusVisit> GetAll()
        {
            return FactoryStatusVisit.GetList(base.DataContext.StatusVisit.ToList());
        }

        public EntityStatusVisit Insert(EntityStatusVisit data)
        {
            try
            {
                StatusVisit dataNew = new StatusVisit()
                {
                    PK_StatusVisitID = data.PK_StatusVisitID,
                    StatusVisit1 = data.StatusVisit1,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.StatusVisit.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_StatusVisitID = dataNew.PK_StatusVisitID;

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

        public EntityStatusVisit Update(EntityStatusVisit data)
        {
            try
            {
                var dataUpdate = base.DataContext.StatusVisit.Where(p => p.PK_StatusVisitID == data.PK_StatusVisitID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.PK_StatusVisitID = data.PK_StatusVisitID;
                    dataUpdate.StatusVisit1 = data.StatusVisit1;
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
