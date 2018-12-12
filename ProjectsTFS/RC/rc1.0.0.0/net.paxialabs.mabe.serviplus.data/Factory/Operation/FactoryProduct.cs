using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryProduct : BaseFactory<FactoryProduct, EntityProduct, Product>
    {
        public override EntityProduct _GetEntity(Product entidad)
        {
            return new EntityProduct()
            {
         PK_ProductID = entidad.PK_ProductID,
         Model = entidad.Model,
         ProductName = entidad.ProductName,
         BarCode = entidad.BarCode,
         SaleOrganization = entidad.SaleOrganization,
         DistributionChannel = entidad.DistributionChannel,
         Center = entidad.Center,
         MaterialGroup1 = entidad.MaterialGroup1,
         MaterialGroup4 = entidad.MaterialGroup4,
         ProductType = entidad.ProductType,
         Status = entidad.Status,
         CreateDate = entidad.CreateDate,
         ModifyDate = entidad.ModifyDate
       
    };
        }
    }
}
