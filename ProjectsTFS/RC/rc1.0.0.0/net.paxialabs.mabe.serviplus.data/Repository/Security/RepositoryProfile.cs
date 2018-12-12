using net.paxialabs.mabe.serviplus.data.Factory.Security;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Security
{
    public class RepositoryProfile : BaseRepository, IRepositoryGET<EntityProfile>, IRepositorySET<EntityProfile>
    {
        public EntityProfile Get(int Id)
        {
            var data = base.DataContext.Profile.Where(p => p.ProfileID == Id);
            if (data.Count() == 1)
                return FactoryProfile.Get(data.Single());
            else
                return null;
        }

        public List<EntityProfile> GetActives()
        {
            return FactoryProfile.GetList(base.DataContext.Profile.Where(p => p.Status == true).ToList());
        }

        public List<EntityProfile> GetAll()
        {
            return FactoryProfile.GetList(base.DataContext.Profile.ToList());
        }

        public EntityProfile Insert(EntityProfile data)
        {
            try
            {
                Profile dataNew = new Profile()
                {
                    ProfileID = data.ProfileID,
                    Profile1 = data.Profile,
                    Description = data.Description,                   
                    Status = data.Status
                };
                base.DataContext.Profile.Add(dataNew);
                base.DataContext.SaveChanges();

                data.ProfileID = dataNew.ProfileID;

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

        public EntityProfile Update(EntityProfile data)
        {
            try
            {
                var dataUpdate = base.DataContext.Profile.Where(p => p.ProfileID == data.ProfileID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.ProfileID = data.ProfileID;
                    dataUpdate.Profile1 = data.Profile;
                    dataUpdate.Description = data.Description;
                    dataUpdate.Status = data.Status;
                    
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
