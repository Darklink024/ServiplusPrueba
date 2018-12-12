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
    public class RepositoryClient : BaseRepository, IRepositoryGET<EntityClient>, IRepositorySET<EntityClient>
    {
        public EntityClient Get(int Id)
        {
            var data = base.DataContext.Client.Where(p => p.PK_ClientID == Id);
            if (data.Count() == 1)
                return FactoryClient.Get(data.Single());
            else
                return null;
        }

        public List<EntityClient> GetActives()
        {
            return FactoryClient.GetList(base.DataContext.Client.Where(p => p.Status == true).ToList());
        }

        public List<EntityClient> GetAll()
        {
            return FactoryClient.GetList(base.DataContext.Client.ToList());
        }

        public EntityClient Insert(EntityClient data)
        {
            try
            {
                Client dataNew = new Client()
                {
                    PK_ClientID = data.PK_ClientID,
                    ClientID = data.ClientID,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    StreetAddress = data.StreetAddress,
                    NumberExternalAddress = data.NumberExternalAddress,
                    NumberInternalAddress = data.NumberInternalAddress,
                    BoroughAddress = data.BoroughAddress,
                    MunicipalityAddress = data.MunicipalityAddress,
                    PostalCodeAddress = data.PostalCodeAddress,
                    AdditionalInfoAddress1 = data.AdditionalInfoAddress1,
                    AdditionalInfoAddress2 = data.AdditionalInfoAddress2,
                    AdditionalInfoAddress3 = data.AdditionalInfoAddress3,
                    AdditionalInfoAddress4 = data.AdditionalInfoAddress4,
                    AdditionalInfoAddress5 = data.AdditionalInfoAddress5,
                    PhoneNumber1 = data.PhoneNumber1,
                    PhoneNumber2 = data.PhoneNumber2,
                    PhoneNumber3 = data.PhoneNumber3,
                    PhoneExtension1 = data.PhoneExtension1,
                    Email = data.Email,
                    RFC = data.RCF,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Client.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_ClientID = dataNew.PK_ClientID;

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

        public EntityClient Update(EntityClient data)
        {
            try
            {
                var dataUpdate = base.DataContext.Client.Where(p => p.PK_ClientID == data.PK_ClientID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_ClientID = data.PK_ClientID;
                    dataUpdate.ClientID = data.ClientID;
                    dataUpdate.FirstName = data.FirstName;
                    dataUpdate.LastName = data.LastName;
                    dataUpdate.StreetAddress = data.StreetAddress;
                    dataUpdate.NumberExternalAddress = data.NumberExternalAddress;
                    dataUpdate.NumberInternalAddress = data.NumberInternalAddress;
                    dataUpdate.BoroughAddress = data.BoroughAddress;
                    dataUpdate.MunicipalityAddress = data.MunicipalityAddress;
                    dataUpdate.PostalCodeAddress = data.PostalCodeAddress;
                    dataUpdate.AdditionalInfoAddress1 = data.AdditionalInfoAddress1;
                    dataUpdate.AdditionalInfoAddress2 = data.AdditionalInfoAddress2;
                    dataUpdate.AdditionalInfoAddress3 = data.AdditionalInfoAddress3;
                    dataUpdate.AdditionalInfoAddress4 = data.AdditionalInfoAddress4;
                    dataUpdate.AdditionalInfoAddress5 = data.AdditionalInfoAddress5;
                    dataUpdate.PhoneNumber1 = data.PhoneNumber1;
                    dataUpdate.PhoneNumber2 = data.PhoneNumber2;
                    dataUpdate.PhoneNumber3 = data.PhoneNumber3;
                    dataUpdate.PhoneExtension1 = data.PhoneExtension1;
                    dataUpdate.Email = data.Email;
                    dataUpdate.RFC = data.RCF;
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

        public EntityClient GetByClientID(string ClientID)
        {
            var data = base.DataContext.Client.Where(p => p.ClientID == ClientID);
            if (data.Count() == 1)
                return FactoryClient.Get(data.Single());
            else
                return null;
        }
    }
}
