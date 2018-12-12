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
     internal class BusinessProduct
    {
        
        public EntityProduct Insert(Material model)
        {
            var objRepository = new RepositoryProduct();

            EntityProduct data = new EntityProduct()
            {
                Model = model.IDMaterial,
                ProductName = model.DescripcionRefaccion,
                SaleOrganization = model.OrganizacionVentas,
                DistributionChannel =model.CanalDistribucion,
                Center = model.Centro,
                MaterialGroup1 = model.GrupoMaterial1,
                MaterialGroup4 = model.GrupoMaterial4,
                ProductType = model.TipoProducto,
                BarCode = "",
                Status = true,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Insert(data);
            return data;
        }

        public EntityProduct Update(Material model, EntityProduct prod)
        {
            var objRepository = new RepositoryProduct();

            EntityProduct data = new EntityProduct()
            {
                PK_ProductID = prod.PK_ProductID,
                Model = model.IDMaterial,
                ProductName = model.DescripcionRefaccion,
                SaleOrganization = model.OrganizacionVentas,
                DistributionChannel = model.CanalDistribucion,
                Center = model.Centro,
                MaterialGroup1 = model.GrupoMaterial1,
                MaterialGroup4 = model.GrupoMaterial4,
                ProductType = model.TipoProducto,
                BarCode = prod.BarCode,
                Status = prod.Status,
                CreateDate = prod.CreateDate,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Update(data);
            return data;
        }

        public void BulkInsert(List<EntityProduct> Productos)
        {
            var objRepository = new RepositoryProduct();
            objRepository.BulkInsert(Productos);

        }

        public void BulkMerge(List<EntityProduct> data)
        {
            new RepositoryProduct().BulkMerge(data);
        }

        public void BulkUpdate(List<EntityProduct> Productos)
        {
            var objRepository = new RepositoryProduct();
            objRepository.BulkUpdate(Productos);

        }


        public List<ModelViewProducts> GetListProduct(ModelViewUserG objCred)
        {
            var NegocioUsuario = new BusinessUsers();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);

            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");
            var producto = new List<EntityProduct>();
            if (objCred.Date == null)
            {
                producto = GetAll();
            }
            else
            {
                producto = GetAll().Where(p => p.ModifyDate >= objCred.Date).ToList();
            }
           
            return producto.Select(p => new ModelViewProducts()
            {
                ProductID = p.PK_ProductID,
                Model = p.Model,
                ProductName = p.ProductName,
                BarCode = p.BarCode,
                //ListPrice = p.li
                GroupMaterial1 = p.MaterialGroup1,
                GroupMaterial4 = p.MaterialGroup4,
                Status = p.Status
            }).ToList();
        }
        public List<EntityProduct> GetAll()
        {
            return new RepositoryProduct().GetAll().Select(p => new EntityProduct()
            {
                PK_ProductID = p.PK_ProductID,
                Model = p.Model,
                ProductName = p.ProductName,
                BarCode = p.BarCode,
                SaleOrganization = p.SaleOrganization,
                DistributionChannel = p.DistributionChannel,
                Center = p.Center,
                MaterialGroup1 = p.MaterialGroup1,
                MaterialGroup4 = p.MaterialGroup4,
                ProductType = p.ProductType,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityProduct>();
        }

        public EntityProduct GetByID(int FK_ProductID)
        {
            var objRepository = new RepositoryProduct();
            return objRepository.Get(FK_ProductID);
        }

        public List<EntityProduct> GetByIDs (List<string> IDs_Products)
        {

            return new RepositoryProduct().GetByIDs(IDs_Products);
        }

        public EntityProduct GetByModel(string Model)
        {
            var objRepository = new RepositoryProduct();
            return objRepository.GetByModel(Model);
        }

    }
}
