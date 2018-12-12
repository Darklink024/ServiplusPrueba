using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
        internal class FactoryClient : BaseFactory<FactoryClient, EntityClient, Client>    
    {
        public override EntityClient _GetEntity(Client entidad)
        {
            return new EntityClient()
            {
                PK_ClientID = entidad.PK_ClientID,
                ClientID = entidad.ClientID,
                FirstName = entidad.FirstName,
                LastName = entidad.LastName,
                StreetAddress = entidad.StreetAddress,
                NumberExternalAddress = entidad.NumberExternalAddress,
                NumberInternalAddress = entidad.NumberInternalAddress,
                BoroughAddress = entidad.BoroughAddress,
                MunicipalityAddress = entidad.MunicipalityAddress,
                PostalCodeAddress = entidad.PostalCodeAddress,
                AdditionalInfoAddress1 = entidad.AdditionalInfoAddress1,
                AdditionalInfoAddress2 = entidad.AdditionalInfoAddress2,
                AdditionalInfoAddress3 = entidad.AdditionalInfoAddress3,
                AdditionalInfoAddress4 = entidad.AdditionalInfoAddress4,
                AdditionalInfoAddress5 = entidad.AdditionalInfoAddress5,
                PhoneNumber1 = entidad.PhoneNumber1,
                PhoneNumber2 = entidad.PhoneNumber2,
                PhoneNumber3 = entidad.PhoneNumber3,
                PhoneExtension1 = entidad.PhoneExtension1,
                Email = entidad.Email,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate,
                
                
            };
        }
    }
}
