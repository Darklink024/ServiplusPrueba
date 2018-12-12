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

    public class RepositoryStatusScheme : BaseRepository, IRepositoryGET<EntityStatusScheme>, IRepositorySET<EntityStatusScheme>
    {
        public EntityStatusScheme Get(int Id)
        {
            var data = base.DataContext.StatusScheme.Where(p => p.PK_StatusSchemeID == Id);
            if (data.Count() == 1)
                return FactoryStatusScheme.Get(data.Single());
            else
                return null;
        }

        public List<EntityStatusScheme> GetActives()
        {
            return FactoryStatusScheme.GetList(base.DataContext.StatusScheme.Where(p => p.Status == true).ToList());
        }

        public List<EntityStatusScheme> GetAll()
        {
            return FactoryStatusScheme.GetList(base.DataContext.StatusScheme.ToList());
        }
        public List<EntityStatusScheme> GetStatusSchemeID(List<string> StatusScheme, string StatusHeadboard)
        {
            return FactoryStatusScheme.GetList(base.DataContext.StatusScheme.Where(p=>StatusScheme.Contains(p.StatusScheme1)&& p.StatusHeadboard==StatusHeadboard).ToList());
        }

        public EntityStatusScheme GetStatusScheme(string StatusScheme, string StatusHeadboard)
        {
            var data = base.DataContext.StatusScheme.Where(p => p.StatusScheme1 == StatusScheme && p.StatusHeadboard == StatusHeadboard);
            if (data.Count() == 1)
                return FactoryStatusScheme.Get(data.Single());
            else
                return null;
        }

        public EntityStatusScheme Insert(EntityStatusScheme data)
        {
            try
            {
                StatusScheme dataNew = new StatusScheme()
                {
                    PK_StatusSchemeID = data.PK_StatusSchemeID,
                    StatusScheme1 = data.StatusScheme1,
                    StatusHeadboard = data.StatusHeadboard,
                    Description = data.Description,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.StatusScheme.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_StatusSchemeID = dataNew.PK_StatusSchemeID;

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

        public EntityStatusScheme Update(EntityStatusScheme data)
        {
            try
            {
                var dataUpdate = base.DataContext.StatusScheme.Where(p => p.PK_StatusSchemeID == data.PK_StatusSchemeID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //PK_StatusSchemeID = data.PK_StatusSchemeID;
                    dataUpdate.StatusScheme1 = data.StatusScheme1;
                    dataUpdate.StatusHeadboard = data.StatusHeadboard;
                    dataUpdate.Description = data.Description;
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
