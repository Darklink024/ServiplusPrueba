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
    public class RepositoryCountries : BaseRepository, IRepositoryGET<EntityCountries>, IRepositorySET<EntityCountries>
    {
        public EntityCountries Get(int Id)
        {
            var data = base.DataContext.Countries.Where(p => p.PK_CountryID == Id);
            if (data.Count() == 1)
                return FactoryCountries.Get(data.Single());
            else
                return null;
        }

        public List<EntityCountries> GetActives()
        {
            return FactoryCountries.GetList(base.DataContext.Countries.Where(p => p.Status == true).ToList());
        }

        public List<EntityCountries> GetAll()
        {
            return FactoryCountries.GetList(base.DataContext.Countries.ToList());
        }

        public EntityCountries Insert(EntityCountries data)
        {
            try
            {
                Countries dataNew = new Countries()
                {
                    PK_CountryID = 0,
                    CountryName = data.CountryName,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Countries.Add(dataNew);
                base.DataContext.SaveChanges();

                data.CountryID = dataNew.PK_CountryID;

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

        public EntityCountries Update(EntityCountries data)
        {
            try
            {
                var dataUpdate = base.DataContext.Countries.Where(p => p.PK_CountryID == data.CountryID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.CountryName = data.CountryName;
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
