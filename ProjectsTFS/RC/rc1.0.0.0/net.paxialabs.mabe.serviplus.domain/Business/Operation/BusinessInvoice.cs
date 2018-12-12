using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessInvoice
    {
        public List<EntityInvoice> GetAll()
        {
            return new RepositoryInvoice().GetAll();
        }

        public ModelViewInvoicing Get(int ID)
        {
            var data = new RepositoryInvoice().Get(ID);
            return new ModelViewInvoicing() {
                InvoicingID = data.PK_InvoiceID,
                BusinessName = data.RFC.Length == 13 ? data.FirstName : data.BusinessName,
                LastName = data.LastName,
                CityAddress = data.CityAddress,
                CountryAddress = data.CountryAddress,
                CPAddress = data.CPAddress,
                Email = data.Email,
                MunicipalityAddress = data.MunicipalityAddress,
                NumExtAddress = data.NumExtAddress,
                NumIntAddress = data.NumIntAddress,
                OrderID = new RepositoryOrder().Get(data.FK_OrderID).OrderID,
                RFC = data.RFC,
                Location = data.Location,
                Reference = data.Reference,
                PersonType = data.PersonType,
                StateAddress = data.StateAddress,
                StreetAddress = data.StreetAddress                
            };
        }

        public ModelViewInvoicing GetByOrderID(int OrderID)
        {
           
            var data = new RepositoryInvoice().GetByOrderID(OrderID);

            if (data != null)
                return new ModelViewInvoicing()
                {
                    InvoicingID = data.PK_InvoiceID,
                    BusinessName = data.BusinessName,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    CityAddress = data.CityAddress,
                    CountryAddress = data.CountryAddress,
                    CPAddress = data.CPAddress,
                    Email = data.Email,
                    MunicipalityAddress = data.MunicipalityAddress,
                    NumExtAddress = data.NumExtAddress,
                    NumIntAddress = data.NumIntAddress,
                    OrderID = new RepositoryOrder().Get(data.FK_OrderID).OrderID,
                    RFC = data.RFC,
                    Location = data.Location,
                    Reference = data.Reference,
                    PersonType = data.PersonType,
                    StateAddress = data.StateAddress,
                    StreetAddress = data.StreetAddress,
                    Folio=data.Folio,
                    EstimatedType= data.TypeQuotation
                    
                };
            else
                return new ModelViewInvoicing();
           
        }

        public ModelViewInvoicing GetPolicyInvoice(int OrderID, string Folio)
        {

            var data = new RepositoryInvoice().GetPolicyInvoice(OrderID,Folio);

            if (data != null)
                return new ModelViewInvoicing()
                {
                    InvoicingID = data.PK_InvoiceID,
                    BusinessName = data.BusinessName,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    CityAddress = data.CityAddress,
                    CountryAddress = data.CountryAddress,
                    CPAddress = data.CPAddress,
                    Email = data.Email,
                    MunicipalityAddress = data.MunicipalityAddress,
                    NumExtAddress = data.NumExtAddress,
                    NumIntAddress = data.NumIntAddress,
                    OrderID = new RepositoryOrder().Get(data.FK_OrderID).OrderID,
                    RFC = data.RFC,
                    Location = data.Location,
                    Reference = data.Reference,
                    PersonType = data.PersonType,
                    StateAddress = data.StateAddress,
                    StreetAddress = data.StreetAddress
                };
            else
                return new ModelViewInvoicing();

        }

        public ModelViewInvoicing GetByOrderID(int OrderID, string rfc)
        {

            var data = new RepositoryInvoice().GetByOrderID(OrderID, rfc);

            if (data != null)
                return new ModelViewInvoicing()
                {
                    InvoicingID = data.PK_InvoiceID,
                    BusinessName = data.RFC.Length == 13 ? data.FirstName : data.BusinessName,
                    LastName = data.LastName,
                    CityAddress = data.CityAddress,
                    CountryAddress = data.CountryAddress,
                    CPAddress = data.CPAddress,
                    Email = data.Email,
                    MunicipalityAddress = data.MunicipalityAddress,
                    NumExtAddress = data.NumExtAddress,
                    NumIntAddress = data.NumIntAddress,
                    OrderID = new RepositoryOrder().Get(data.FK_OrderID).OrderID,
                    RFC = data.RFC,
                    Location = data.Location,
                    Reference = data.Reference,
                    PersonType = data.PersonType,
                    StateAddress = data.StateAddress,
                    StreetAddress = data.StreetAddress
                };
            else
                return new ModelViewInvoicing();

        }

        public void Insert(EntityInvoice Factura)
        {
            new RepositoryInvoice().Insert(Factura);
        }
        public void Update(EntityInvoice Factura)
        {
            new RepositoryInvoice().Update(Factura);
        }
    }
}
