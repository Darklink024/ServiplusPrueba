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
 
    public class RepositoryInvoice : BaseRepository, IRepositoryGET<EntityInvoice>, IRepositorySET<EntityInvoice>
    {
        public EntityInvoice Get(int Id)
        {
            var data = base.DataContext.Invoice.Where(p => p.PK_InvoiceID == Id);
            if (data.Count() == 1)
                return FactoryInvoice.Get(data.Single());
            else
                return null;
        }

        public EntityInvoice GetByOrderID(int OrderID)
        {
            var data = base.DataContext.Invoice.Where(p => p.FK_OrderID == OrderID);
            if (data.Count() > 0)
                return FactoryInvoice.Get(data.First());
            else
                return null;
        }
        public EntityInvoice GetPolicyInvoice(int OrderID, string Folio)
        {
            var data = base.DataContext.Invoice.Where(p => p.FK_OrderID == OrderID && p.Folio== Folio);
            if (data.Count() > 0)
                return FactoryInvoice.Get(data.First());
            else
                return null;
        }
        public EntityInvoice GetByOrderID(int OrderID, string rfc)
        {
            var data = base.DataContext.Invoice.Where(p => p.FK_OrderID == OrderID & p.RFC == rfc);
            if (data.Count() > 0)
                return FactoryInvoice.Get(data.First());
            else
                return null;
        }

        public List<EntityInvoice> GetActives()
        {
            return FactoryInvoice.GetList(base.DataContext.Invoice.Where(p => p.Status == true).ToList());
        }

        public List<EntityInvoice> GetAll()
        {
            return FactoryInvoice.GetList(base.DataContext.Invoice.ToList());
        }

        public EntityInvoice Insert(EntityInvoice data)
        {
            try
            {
                Invoice dataNew = new Invoice()
                {
                    PK_InvoiceID = data.PK_InvoiceID,
                    FK_OrderID = data.FK_OrderID,
                    BusinessName = data.BusinessName,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    RFC = data.RFC,
                    Email = data.Email,
                    CountryAddress = data.CountryAddress,
                    StateAddress = data.StateAddress,
                    CityAddress = data.CityAddress,
                    MunicipalityAddress = data.MunicipalityAddress,
                    StreetAddress = data.StreetAddress,
                    NumIntAddress = data.NumIntAddress,
                    NumExtAddress = data.NumExtAddress,
                    CPAddress = data.CPAddress,
                    Location= data.Location,
                    Reference = data.Reference,
                    PersonType = data.PersonType,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate,
                    Folio=data.Folio,
                    FK_TypeQuotation=data.TypeQuotation
                   
                };
                base.DataContext.Invoice.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_InvoiceID = dataNew.PK_InvoiceID;

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

        public EntityInvoice Update(EntityInvoice data)
        {
            try
            {
                var dataUpdate = base.DataContext.Invoice.Where(p => p.PK_InvoiceID == data.PK_InvoiceID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.BusinessName = data.BusinessName;
                    dataUpdate.FirstName = data.FirstName;
                    dataUpdate.LastName = data.LastName;
                    dataUpdate.RFC = data.RFC;
                    dataUpdate.Email = data.Email;
                    dataUpdate.CountryAddress = data.CountryAddress;
                    dataUpdate.StateAddress = data.StateAddress;
                    dataUpdate.CityAddress = data.CityAddress;
                    dataUpdate.MunicipalityAddress = data.MunicipalityAddress;
                    dataUpdate.StreetAddress = data.StreetAddress;
                    dataUpdate.NumIntAddress = data.NumIntAddress;
                    dataUpdate.NumExtAddress = data.NumExtAddress;
                    dataUpdate.CPAddress = data.CPAddress;
                    dataUpdate.Status = data.Status;
                    dataUpdate.Location = data.Location;
                    dataUpdate.Reference = data.Reference;
                    dataUpdate.PersonType = data.PersonType;
                    dataUpdate.ModifyDate = DateTime.UtcNow;
                    dataUpdate.Folio = data.Folio;
                    dataUpdate.FK_TypeQuotation = data.TypeQuotation;


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
