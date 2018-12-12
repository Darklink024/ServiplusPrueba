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
    public class RepositoryCauseVisit : BaseRepository, IRepositoryGET<EntityCauseVisit>, IRepositorySET<EntityCauseVisit>
    {
        public EntityCauseVisit Get(int Id)
        {
            var data = base.DataContext.CauseVisit.Where(p => p.PK_CauseVisitID == Id);
            if (data.Count() == 1)
                return FactoryCauseVisit.Get(data.Single());
            else
                return null;
        }

        public List<EntityCauseVisit> GetActives()
        {
            return FactoryCauseVisit.GetList(base.DataContext.CauseVisit.Where(p => p.Status == true).ToList());
        }

        public List<EntityCauseVisit> GetAll()
        {
            return FactoryCauseVisit.GetList(base.DataContext.CauseVisit.ToList());
        }

        public EntityCauseVisit Insert(EntityCauseVisit data)
        {
            try
            {
                CauseVisit dataNew = new CauseVisit()
                {
                    PK_CauseVisitID = data.PK_CauseVisitID,
                    FK_StatusVisitID = data.FK_StatusVisitID,
                    CauseVisit1 = data.CauseVisit1,
                    Moment = data.Moment,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.CauseVisit.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_CauseVisitID = dataNew.PK_CauseVisitID;

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

        public EntityCauseVisit Update(EntityCauseVisit data)
        {
            try
            {
                var dataUpdate = base.DataContext.CauseVisit.Where(p => p.PK_CauseVisitID == data.PK_CauseVisitID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.PK_CauseVisitID = data.PK_CauseVisitID;
                    dataUpdate.FK_StatusVisitID = data.FK_StatusVisitID;
                    dataUpdate.CauseVisit1 = data.CauseVisit1;
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
