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
    internal class BusinessValidationGuarantyProduct
    {
        public List<ModelViewGuarantyProduct> GetLisValidationProduct(ModelViewUserG objCred)
        {
            var NegocioUsuario = new BusinessUsers();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            var lista = new List<EntityValidationGuarantyProduct>();
            var NegocioLugarCompra = new BusinessShopPlace();

            if(objCred.Date== null)
            { lista = GetAll(); }
            else
            { lista = GetAll().Where(p => p.ModifyDate >= objCred.Date).ToList(); }
            return  (from a in lista
                     join b in NegocioLugarCompra.GetAll() on a.ClientID equals b.ClientID
                               select new ModelViewGuarantyProduct()
                               {
                                   ValidationGuarantyProductID = a.PK_ValidationGuarantyProductID,
                                   ProducID = a.FK_ProducID.HasValue ? a.FK_ProducID.Value : 0,
                                   Country = a.Country,
                                   Model = a.Model,
                                   ClientID = a.ClientID,
                                   Months = a.Months,
                                   ValidFrom = a.ValidFrom.Value.ToString("yyyy-MM-dd"),
                                   ValidTo = a.ValidTo.Value.ToString("yyyy-MM-dd"),
                                   ShopPlaceID = b.PK_ShopPlaceID
                               }).ToList();
        }
        public List<EntityValidationGuarantyProduct> GetAll()
        {
            return new RepositoryValidationGuarantyProduct().GetAll().Select(p => new EntityValidationGuarantyProduct()
            {
                PK_ValidationGuarantyProductID = p.PK_ValidationGuarantyProductID,
                FK_ProducID = p.FK_ProducID,
                Country = p.Country,
                Model = p.Model,
                ClientID = p.ClientID,
                Months = p.Months,
                ValidFrom = p.ValidFrom,
                ValidTo = p.ValidTo,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityValidationGuarantyProduct>();
        }

        public void BulkMerge(List<EntityValidationGuarantyProduct> data)
        {
            new RepositoryValidationGuarantyProduct().BulkMerge(data);
        }

    }
}
