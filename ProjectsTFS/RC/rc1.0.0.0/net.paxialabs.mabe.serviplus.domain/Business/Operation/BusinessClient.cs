using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessClient
    {
       
        public ODSCLIENTE Insert(ODSCLIENTE model)
        {
            var objRepository = new RepositoryClient();

            EntityClient data = new EntityClient()
            {
                PK_ClientID = 0,
                ClientID = model.IDCliente,
                FirstName = model.Nombrecliente,
                LastName = model.ApellidoCliente,
                StreetAddress = model.Calle,
                NumberExternalAddress = model.Numero,
                NumberInternalAddress = model.NumeroInterior,
                BoroughAddress = model.Colonia,
                MunicipalityAddress = model.Delegacion,
                PostalCodeAddress = model.CodigoPosta,
                AdditionalInfoAddress1 = model.Referencia1,
                AdditionalInfoAddress2 = model.Referencia2,
                AdditionalInfoAddress3 = model.Referencia3,
                AdditionalInfoAddress4 = model.Referencia4,
                AdditionalInfoAddress5 = model.Referencia5,
                PhoneNumber1 = model.CELULAR,
                PhoneNumber2 = model.TEL_CASA,
                PhoneNumber3 = model.TEL_TRABAJO,
                PhoneExtension1 = model.EXT_TRABAJO,
                Email = model.email,
                RCF = model.RFCCEDULA,
                Status = true,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                ModifyDate = DateTime.UtcNow.ToLocalTime()

            };
            data = objRepository.Insert(data);
            return model;
        }
        public ODSCLIENTE Update(ODSCLIENTE model, EntityClient cliente)
        {
            var objRepository = new RepositoryClient();

            EntityClient data = new EntityClient()
            {
                PK_ClientID = cliente.PK_ClientID,
                ClientID = model.IDCliente,
                FirstName = model.Nombrecliente,
                LastName = model.ApellidoCliente,
                StreetAddress = model.Calle,
                NumberExternalAddress = model.Numero,
                NumberInternalAddress = model.NumeroInterior,
                BoroughAddress = model.Colonia,
                MunicipalityAddress = model.Delegacion,
                PostalCodeAddress = model.CodigoPosta,
                AdditionalInfoAddress1 = model.Referencia1,
                AdditionalInfoAddress2 = model.Referencia2,
                AdditionalInfoAddress3 = model.Referencia3,
                AdditionalInfoAddress4 = model.Referencia4,
                AdditionalInfoAddress5 = model.Referencia5,
                PhoneNumber1 = model.CELULAR,
                PhoneNumber2 = model.TEL_CASA,
                PhoneNumber3 = model.TEL_TRABAJO,
                PhoneExtension1 = model.EXT_TRABAJO,
                Email = model.email,
                RCF = model.RFCCEDULA,
                Status = true,
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow

            };
            data = objRepository.Update(data);
            return model;
        }
        public List<EntityClient> GetAll()
        {
            return new RepositoryClient().GetAll().Select(p => new EntityClient()
            {
                PK_ClientID = p.PK_ClientID,
                ClientID = p.ClientID,
                FirstName = p.FirstName,
                LastName = p.LastName,
                StreetAddress = p.StreetAddress,
                NumberExternalAddress = p.NumberExternalAddress,
                NumberInternalAddress = p.NumberInternalAddress,
                BoroughAddress = p.BoroughAddress,
                MunicipalityAddress = p.MunicipalityAddress,
                PostalCodeAddress = p.PostalCodeAddress,
                AdditionalInfoAddress1 = p.AdditionalInfoAddress1,
                AdditionalInfoAddress2 = p.AdditionalInfoAddress2,
                AdditionalInfoAddress3 = p.AdditionalInfoAddress3,
                AdditionalInfoAddress4 = p.AdditionalInfoAddress4,
                AdditionalInfoAddress5 = p.AdditionalInfoAddress5,
                PhoneNumber1 = p.PhoneNumber1,
                PhoneNumber2 = p.PhoneNumber2,
                PhoneNumber3 = p.PhoneNumber3,
                PhoneExtension1 = p.PhoneExtension1,
                Email = p.Email,
                RCF = p.RCF,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityClient>();
        }

        public EntityClient GetByID(int FK_ClientID)
        {
            var objRepository = new RepositoryClient();
            return objRepository.Get(FK_ClientID);
        }

        public EntityClient GetByClientID(string ClientID)
        {
            var objRepository = new RepositoryClient();
            return objRepository.GetByClientID(ClientID);
        }
    }
}
