using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
   internal class BusinessPrice
    {
        public void Insert(Precio model, int FK_BuildOfMaterialsID, int FK_ProductID)
        {
            var objRepository = new RepositoryPrice();

            EntityPrice data = new EntityPrice()
            {

                PK_PriceID = 0,
                FK_BuildOfMaterialsID = FK_BuildOfMaterialsID,
                FK_ProductID = FK_ProductID,
                TypeCondition = model.TipoCondicion,
                SalesOrganization = model.OrganizacionVentas,
                DistributionChannel = model.CanalDistribucion,
                Price = double.Parse(model.PrecioMaterial),
                Coin = model.Moneda,
                DateValidity = DateTime.ParseExact(model.FechaValidez,
                                  "yyyyMMdd",
                                   CultureInfo.InvariantCulture),
                DateValidityEnd = DateTime.ParseExact(model.FechaFinValidez,
                                  "yyyyMMdd",
                                   CultureInfo.InvariantCulture),
                Policy = "",
                Guaranty = "",
                Status = true,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.UtcNow

            };
            data = objRepository.Insert(data);

        }

        public void BulkInsert(List<EntityPrice> Precios)
        {
            new RepositoryPrice().BulkInsert(Precios);
        }

        public void BulkUpdate(List<EntityPrice> Precios)
        {
            new RepositoryPrice().BulkUpdate(Precios);
        }

        public void BulkMerge(List<EntityPrice> data)
        {
            new RepositoryPrice().BulkMerge(data);
        }

        public List<ModelViewPrices> GetListPrices(ModelViewUserG objCred)
        {
            var NegocioUsuario = new BusinessUsers();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            List<int> lst = new List<int>();

            if (objCred.ProductID == 0)
            {
                lst = new BusinessOrder().GetListByProductID(dataUsuario.UserID, objCred.Date.Value);
                


                return new RepositoryPrice().GetListPrice(lst, null);
            }
            else
            {
                lst.Add(objCred.ProductID);
                return new RepositoryPrice().GetListPrice(lst, null);
            }
        }       


        public List<ModelViewPrices> GetListPriceByModel(List<int> modelos)
        {
            return new RepositoryPrice().GetAllPriceByModel(modelos).Select(p => new ModelViewPrices()
            {
                PriceID = p.PK_PriceID,
                BuildOfMaterialsID = p.FK_BuildOfMaterialsID,
                ProductID = p.FK_ProductID,
                TypeCondition = p.TypeCondition,
                SalesOrganization = p.SalesOrganization,
                DistributionChannel = p.DistributionChannel,
                ListPrice = p.ListPrice,
                GroupMaterial1 = p.GroupMaterial1,
                GroupMaterial4 = p.GroupMaterial4,
                Price = p.Price,
                Coin = p.Coin,
                Policy = p.Policy,
                Guaranty = p.Guaranty
            }).ToList<ModelViewPrices>();

        }

        public List<ModelViewWorkforcePrices> GetListPricesWorkforce(ModelViewUserG objCred)
        {
          
            var dataUsuario = new BusinessUsers().GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");
            
            return new BusinessWorkforce().GetAll(objCred.Date);
        }

        public List<EntityPrice> GetAll()
        {
            return new RepositoryPrice().GetAll();
        }

        public EntityPrice GetPrice(int FK_BuildOfMaterialsID, int FK_ProductID)
        {
            return new RepositoryPrice().GetPrice(FK_BuildOfMaterialsID, FK_ProductID);
        }
    }
}
