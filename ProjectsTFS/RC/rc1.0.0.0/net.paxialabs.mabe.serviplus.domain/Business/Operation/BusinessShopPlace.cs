using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
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
    internal class BusinessShopPlace
    {
        public LugComp Insert(LugComp model)
        {
            var objRepository = new RepositoryShopPlace();
 
            EntityShopPlace data = new EntityShopPlace()
            {
                PK_ShopPlaceID = 0,
                ShopPlaceID = model.IDSucursal,
                ShopPlace1 = model.LugarCompra,
                CountryAddress = model.ID_PAIS,
                StateAddress = model.ESTADO,
                CityAddress = model.ESTADO,
                MunicipalityAddress = model.ESTADO,
                StreetAddress = "",
                ClientID = model.IDClienteDist,
                Status = true,
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Insert(data);
            return model;
        }

        public void BulkInsert(List<EntityShopPlace> BOM)
        {
            var objRepository = new RepositoryShopPlace();
            objRepository.BulkInsert(BOM);
        }

        public void BulkMerge(List<EntityShopPlace> data)
        {
            new RepositoryShopPlace().BulkMerge(data);
        }


        public LugComp Update(LugComp model, EntityShopPlace shop)
        {
            var objRepository = new RepositoryShopPlace();

            EntityShopPlace data = new EntityShopPlace()
            {
                PK_ShopPlaceID = shop.PK_ShopPlaceID,
                ShopPlaceID = model.IDSucursal,
                ShopPlace1 = model.LugarCompra,
                CountryAddress = model.ID_PAIS,
                StateAddress = model.ESTADO,
                CityAddress = model.ESTADO,
                MunicipalityAddress = model.ESTADO,
                StreetAddress = "",
                ClientID = model.IDClienteDist,
                Status = shop.Status,
                CreateDate = shop.CreateDate,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Update(data);
            return model;
        }

        public List<ModelViewShopPlace> GetListShopPlace(ModelViewUserG objCred)
        {
            var NegocioUsuario = new BusinessUsers();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            var lt = new List<EntityShopPlace>();
            if (objCred.Date == null)
            {
                lt = GetAll();
            }
            else
            {
                lt = GetAll().Where(p => p.ModifyDate >= objCred.Date).ToList();
            }
            return lt.Select(p=> new ModelViewShopPlace()
            {
                ShopPlaceID = p.PK_ShopPlaceID,
                StateID = p.FK_StateID.Value,
                ClientID = p.ClientID,
                ShopPlaceDescription = p.ShopPlaceID + " - " + p.ShopPlace1,
                ShopPlaceStatus = p.Status
            }).ToList();
        }
        public List<EntityShopPlace> GetAll()
        {
            return new RepositoryShopPlace().GetAll().Select(p => new EntityShopPlace()
            {
                PK_ShopPlaceID = p.PK_ShopPlaceID,
                FK_StateID = p.FK_StateID,
                ShopPlaceID = p.ShopPlaceID,
                ShopPlace1 = p.ShopPlace1,
                CountryAddress = p.CountryAddress,
                StateAddress = p.StateAddress,
                CityAddress = p.CityAddress,
                MunicipalityAddress = p.MunicipalityAddress,
                StreetAddress = p.StreetAddress,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate,
                ClientID = p.ClientID
            }).ToList<EntityShopPlace>();
        }

        public EntityShopPlace GetByShopPlace(string ShopPlaceID)
        {
            var objRepository = new RepositoryShopPlace();
            return objRepository.GetByShopPlace(ShopPlaceID);
        }

        public EntityShopPlace GetByShopPlaceID(int ShopPlaceID)
        {
            var objRepository = new RepositoryShopPlace();
            return objRepository.Get(ShopPlaceID);
        }

    }
}
