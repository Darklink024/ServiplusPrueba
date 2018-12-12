using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessCountries
    {
        public List<ModelViewCountries> GetListCountries(ModelViewUserG objCred)
        {
            var NegocioUsuario = new BusinessUsers();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            var lt = new List<EntityCountries>();
            if (objCred.Date == null)
            {
                lt = GetAll();
            }
            else
            {
                lt = GetAll().Where(p => p.ModifyDate >= objCred.Date).ToList();
            }
            return lt.Select(p => new ModelViewCountries()
            {
                CountryID = p.CountryID,
                CountryName = p.CountryName
            }).ToList();
        }


        public List<EntityCountries> GetAll()
        {
            return new RepositoryCountries().GetAll();
        }
    }
}
