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
    internal class BusinessCauseOrder
    {

        public List<ModelViewCauseOrder> GetListCauseOrder(ModelViewUserG objCred)
        {
            var NegocioUsuario = new BusinessUsers();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            var NegocioStatusCausaOrder = new BusinessStatusCauseOrder();
            var CauseOrder = new List<EntityCauseOrder>();
            //GetAll();
            if(objCred.Date== null)
            { CauseOrder = GetAll(); }
            else
            { CauseOrder = GetAll().Where(p=> p.ModifyDate >= objCred.Date).ToList(); }
            var StatusCauseOrder = NegocioStatusCausaOrder.GetAll();
            var lt = (from c in CauseOrder
                      join p in StatusCauseOrder on c.FK_StatusOrderID equals p.PK_StatusOrderID
                      select new ModelViewCauseOrder()
                      {
                          CauseOrderID = c.PK_CauseOrderID,
                          CauseOrder = c.CauseOrder1,
                          Moment = c.Moment,
                          Status = c.Status,
                          StatusOrderID = p.PK_StatusOrderID,
                          StatusOrder = p.StatusOrder1
                      }).ToList();
            return lt;
        }
        public List<EntityCauseOrder> GetAll()
        {
            return new RepositoryCauseOrder().GetAll().Select(p => new EntityCauseOrder()
            {
                PK_CauseOrderID = p.PK_CauseOrderID,
                FK_StatusOrderID = p.FK_StatusOrderID,
                CauseOrder1 = p.CauseOrder1,
                Moment = p.Moment,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityCauseOrder>();
        }
    }
}
