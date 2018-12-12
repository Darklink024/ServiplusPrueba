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
    internal class BusinessGuaranty
    {

        public List<ModelViewGuarantys> GetListGuaranty(ModelViewUserG objCred)
        {
            var NegocioUsuario = new BusinessUsers();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);

            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");


            var NegocioTipoGarantia = new BusinessGuarantyType();
            var Guaranty = new List<EntityGuaranty>();
            if (objCred.Date == null)
            { Guaranty = GetAll(); }
            else
            { Guaranty = GetAll().Where(p=> p.ModifyDate >= objCred.Date).ToList(); }
            var GuarantyType = NegocioTipoGarantia.GetAll();
            var lt = (from c in Guaranty
                      join p in GuarantyType on c.FK_GuarantyTypeID equals p.PK_GuarantyTypeID
                      select new ModelViewGuarantys()
                      {
                          GuarantyID = c.PK_GuarantyID,
                          GuarantyCode = c.GuarantyID,
                          Guaranty = c.Guaranty1,
                          Status = c.Status,
                          GuarantyTypeID = p.PK_GuarantyTypeID,
                          GurantyType = p.GuarantyType1
                      }).ToList();
            return lt;
        }
        public List<EntityGuaranty> GetAll()
        {
            return new RepositoryGuaranty().GetAll().Select(p => new EntityGuaranty()
            {
                PK_GuarantyID = p.PK_GuarantyID,
                FK_GuarantyTypeID = p.FK_GuarantyTypeID,
                GuarantyID = p.GuarantyID,
                Guaranty1 = p.Guaranty1,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityGuaranty>();
        }

        public EntityGuaranty Get(int ID)
        {
            return new RepositoryGuaranty().Get(ID);
        }

        public EntityGuaranty GetByGuarantyID(string GuarantyID)
        {
            var objRepository = new RepositoryGuaranty();
            return objRepository.GetByGuarantyID(GuarantyID);
        }
        public List<EntityGuaranty> GuarantyList(List<string> IDs)
        {
            return new RepositoryGuaranty().GetByIDs(IDs);
        }
    }
}
