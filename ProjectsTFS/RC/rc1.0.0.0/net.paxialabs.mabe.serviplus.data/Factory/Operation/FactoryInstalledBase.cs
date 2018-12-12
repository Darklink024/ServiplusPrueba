using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{

    internal class FactoryInstalledBase : BaseFactory<FactoryInstalledBase, EntityInstalledBase, InstalledBase>
    {
        public override EntityInstalledBase _GetEntity(InstalledBase entidad)
        {
            return new EntityInstalledBase()
            {
             PK_InstalledBaseID = entidad.PK_InstalledBaseID,
             FK_ClientID = entidad.FK_ClientID,
             FK_ProductID = entidad.FK_ProductID,
             FK_ShopPlaceID = entidad.FK_ShopPlaceID,
             InstalledBaseID = entidad.InstalledBaseID,
             SerialNumber = entidad.SerialNumber,
             ShopDate = entidad.ShopDate,
             ShopPlaceIDFlag = entidad.ShopPlaceIDFlag,
             ShopDateFlag = entidad.ShopDateFlag,
             SerialNumberFlag = entidad.SerialNumberFlag,
             ProductIDFlag = entidad.ProductIDFlag,
             Model = entidad.Model,
             ProductName = entidad.ProductName,
             Status = entidad.Status,
             CreateDate = entidad.CreateDate,
             ModifyDate = entidad.ModifyDate
            };
        }
    }
}
