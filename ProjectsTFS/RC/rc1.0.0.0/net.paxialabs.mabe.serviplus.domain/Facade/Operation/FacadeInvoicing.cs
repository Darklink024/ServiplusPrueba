using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public static class FacadeInvoicing
    {

        public static ModelViewInvoicing Get(int InvoicingID)
        {
            return new BusinessInvoice().Get(InvoicingID);
        }

        public static ModelViewInvoicing GetByOrderID(int OrderID)
        {
            return new BusinessInvoice().GetByOrderID(OrderID);
        }

        public static ModelViewInvoicing GetPolicyInvoice(int OrderID, string Folio) {

            return new BusinessInvoice().GetPolicyInvoice(OrderID, Folio);
        }

        public static ModelViewResultREST SetInvoicing(ModelViewInvoicing model)
        {
            try
            {

                var dataUsuario = new BusinessUsers().GetUserByToken(model.TokenUser);
                if (model.TokenApp != GlobalConfiguration.TokenWEB)
                    if (model.TokenApp != GlobalConfiguration.TokenMobile)
                        throw new Exception("TokenInvalid");
                if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

                

                if( !string.IsNullOrEmpty(model.RFC) && !string.IsNullOrEmpty(model.BusinessName))
                { 

                int OrderID = new BusinessOrder().GetByOrderID(model.OrderID).PK_OrderID;
                   

                var dataInvoice = new BusinessInvoice().GetPolicyInvoice(OrderID,model.Folio);
                if (dataInvoice.InvoicingID == 0)
                {
                        new BusinessInvoice().Insert(new entities.Entity.Operation.EntityInvoice()
                        {
                        PK_InvoiceID = 0,
                        FK_OrderID = OrderID,
                        BusinessName = model.PersonType == "Moral" ? model.BusinessName : "",
                        FirstName  = model.PersonType != "Moral" ? model.BusinessName : "",
                        LastName = model.LastName,
                        RFC = model.RFC,
                        Email = model.Email,
                        CountryAddress = model.CountryAddress,
                        StateAddress = model.StateAddress,
                        CityAddress = model.CityAddress,
                        MunicipalityAddress = model.MunicipalityAddress,
                        StreetAddress = model.StreetAddress,
                        NumIntAddress = model.NumIntAddress,
                        NumExtAddress = model.NumExtAddress,
                        CPAddress = model.CPAddress,
                        Location = model.Location,
                        Reference = model.Reference,
                        PersonType = model.PersonType,
                        Status = true,
                        CreateDate = DateTime.UtcNow,
                        ModifyDate = DateTime.UtcNow,
                        Folio= model.Folio,
                        TypeQuotation=model.EstimatedType                        
                    });
                }
                else
                {
                    new BusinessInvoice().Update(new entities.Entity.Operation.EntityInvoice()
                    {
                        PK_InvoiceID = dataInvoice.InvoicingID,
                        FK_OrderID = OrderID,
                        BusinessName = model.RFC.Length == 13 ? "" : model.BusinessName,
                        FirstName = model.RFC.Length == 13 ? model.BusinessName : "",
                        LastName = model.LastName,
                        RFC = model.RFC,
                        Email = model.Email,
                        CountryAddress = model.CountryAddress,
                        StateAddress = model.StateAddress,
                        CityAddress = model.CityAddress,
                        MunicipalityAddress = model.MunicipalityAddress,
                        StreetAddress = model.StreetAddress,
                        NumIntAddress = model.NumIntAddress,
                        NumExtAddress = model.NumExtAddress,
                        CPAddress = model.CPAddress,
                        Location = model.Location,
                        Reference = model.Reference,
                        PersonType = model.PersonType,
                        Status = true,
                        CreateDate = DateTime.UtcNow,
                        ModifyDate = DateTime.UtcNow,
                        Folio=model.Folio,
                        TypeQuotation= model.EstimatedType
                    });
                }
                }
                return new ModelViewResultREST() { Result = "Success" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
