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
    public class RepositoryStates : BaseRepository, IRepositoryGET<EntityStates>, IRepositorySET<EntityStates>
    {
        public EntityStates Get(int Id)
        {
            var data = base.DataContext.States.Where(p => p.PK_StateID == Id);
            if (data.Count() == 1)
                return FactoryStates.Get(data.Single());
            else
                return null;
        }

        public EntityStates GetByState(string State)
        {
            var data = base.DataContext.States.Where(p => p.StateName == State);
            if (data.Count() == 1)
                return FactoryStates.Get(data.Single());
            else
                return null;
        }

        public List<EntityStates> GetActives()
        {
            return FactoryStates.GetList(base.DataContext.States.Where(p => p.Status == true).ToList());
        }

        public List<EntityStates> GetAll()
        {
            return FactoryStates.GetList(base.DataContext.States.ToList());
        }

        public EntityStates Insert(EntityStates data)
        {
            try
            {
                States dataNew = new States()
                {
                    PK_StateID = 0,
                    FK_CountryID = data.CountryID,
                    StateName = data.StateName,
                    Abbreviation = data.Abbreviation,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.States.Add(dataNew);
                base.DataContext.SaveChanges();

                data.StateID = dataNew.PK_StateID;

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

        public EntityStates Update(EntityStates data)
        {
            try
            {
                var dataUpdate = base.DataContext.States.Where(p => p.PK_StateID == data.StateID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_ShopPlaceID = data.PK_ShopPlaceID;
                    dataUpdate.FK_CountryID = data.CountryID;
                    dataUpdate.StateName = data.StateName;
                    dataUpdate.Abbreviation = data.Abbreviation;
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
