using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryInvoice : BaseFactory<FactoryInvoice, EntityInvoice, Invoice>
    {
        public override EntityInvoice _GetEntity(Invoice entidad)
        {
            return new EntityInvoice()
            {
                PK_InvoiceID = entidad.PK_InvoiceID,
                FK_OrderID = entidad.FK_OrderID,
                BusinessName = entidad.BusinessName,
                FirstName = entidad.FirstName,
                LastName = entidad.LastName,
                RFC = entidad.RFC,
                Email = entidad.Email,
                CountryAddress = entidad.CountryAddress,
                StateAddress = entidad.StateAddress,
                CityAddress = entidad.CityAddress,
                MunicipalityAddress = entidad.MunicipalityAddress,
                StreetAddress = entidad.StreetAddress,
                NumIntAddress = entidad.NumIntAddress,
                NumExtAddress = entidad.NumExtAddress,
                CPAddress = entidad.CPAddress,
                Location = entidad.Location,
                Reference = entidad.Reference,
                PersonType = entidad.PersonType,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
