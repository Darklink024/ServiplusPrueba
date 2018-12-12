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
    internal class BusinessStates
    {

        public List<ModelViewStates> GetListStates(ModelViewUserG objCred)
        {
            var NegocioUsuario = new BusinessUsers();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            var lt = new List<EntityStates>();
            if (objCred.Date == null)
            {
                lt = GetAll();
            }
            else
            {
                lt = GetAll().Where(p => p.ModifyDate >= objCred.Date).ToList();
            }
            return lt.Select(p => new ModelViewStates()
            {
                StateID = p.StateID,
                CountryID = p.CountryID,
                StateName = p.StateName
            }).ToList();
        }
        public List<EntityStates> GetAll()
        {
            return new RepositoryStates().GetAll();
        }
        public EntityStates GetBYID(int StateID) 
        {
            return new RepositoryStates().Get(StateID);
        }
        public EntityStates GetByState(string StateName)
        {
            return new RepositoryStates().GetByState(StateName);
        }
    }
}
