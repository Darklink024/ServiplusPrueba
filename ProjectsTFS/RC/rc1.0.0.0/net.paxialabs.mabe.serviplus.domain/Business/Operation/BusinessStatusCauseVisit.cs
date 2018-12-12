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
    internal class BusinessStatusCauseVisit
    {
        public List<EntityStatusVisit> GetAll()
        {
            return new RepositoryStatusVisit().GetAll().Select(p => new EntityStatusVisit()
            {
                PK_StatusVisitID = p.PK_StatusVisitID,
                StatusVisit1 = p.StatusVisit1,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityStatusVisit>();
        }

        public List<ModelViewCauseVisit> GetListCauseOrder(ModelViewUserG objCred)
        {
            var NegocioUsuario = new BusinessUsers();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");
            var StatusVisit = new List<ModelViewCauseVisit>();
            if (objCred.Date == null)
            { StatusVisit = GetAll().Select(p => new
            ModelViewCauseVisit()
            {
                StatusVisitID = p.PK_StatusVisitID,
                StatusVisit = p.StatusVisit1,
                Status = p.Status

            }).ToList();
            }
            else
            { StatusVisit = GetAll().Where(p => p.ModifyDate >= objCred.Date).Select(p=> new
            ModelViewCauseVisit()
            {
                StatusVisitID = p.PK_StatusVisitID,
                StatusVisit = p.StatusVisit1,
                Status = p.Status

            }).ToList(); }
            return StatusVisit;
        }
    }
}
