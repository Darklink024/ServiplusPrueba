using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.data.Repository.Security;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessInstalledBase
    {
        public EntityInstalledBase Insert(int FK_ClientID, int? PK_ProductID,int? FK_ShopPlaceID, string IDBaseInstalada, string NumeroSerie, string fechaCompra, string Model, string ProductName)
        {
            var objRepository = new RepositoryInstalledBase();
            string numSerie = NumeroSerie != null ? NumeroSerie : "";
            DateTime? ShopDate = string.IsNullOrEmpty(fechaCompra) ? new Nullable<DateTime>() : DateTime.Parse(fechaCompra);

            EntityInstalledBase data = new EntityInstalledBase()
            {
                PK_InstalledBaseID = 0,
                FK_ClientID = FK_ClientID,
                FK_ProductID = PK_ProductID,
                FK_ShopPlaceID = FK_ShopPlaceID,
                InstalledBaseID = IDBaseInstalada,
                SerialNumber = numSerie,
                ShopDate = ShopDate,
                ShopPlaceIDFlag = false,
                ShopDateFlag = false,
                SerialNumberFlag = false,
                ProductIDFlag = false,
                Model = Model,
                ProductName = ProductName,
                Status = true,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                ModifyDate = DateTime.UtcNow.ToLocalTime()
            };
            data = objRepository.Insert(data);
            return data;
        }

        public EntityInstalledBase Update(EntityInstalledBase data)
        {
            data.ModifyDate = DateTime.UtcNow;
            return new RepositoryInstalledBase().Update(data);
        }

        public List<EntityInstalledBase> GetAll()
        {
            return new RepositoryInstalledBase().GetAll().Select(p => new EntityInstalledBase()
            {
                PK_InstalledBaseID = p.PK_InstalledBaseID,
                FK_ClientID = p.FK_ClientID,
                FK_ProductID = p.FK_ProductID,
                FK_ShopPlaceID = p.FK_ShopPlaceID,
                InstalledBaseID = p.InstalledBaseID,
                SerialNumber = p.SerialNumber,
                ShopDate = p.ShopDate,
                ShopPlaceIDFlag = p.ShopPlaceIDFlag,
                ShopDateFlag = p.ShopDateFlag,
                SerialNumberFlag = p.SerialNumberFlag,
                ProductIDFlag = p.ProductIDFlag,
                Model = p.Model,
                ProductName = p.ProductName,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityInstalledBase>();
        }
        public List<EntityInstalledBase> GetAllByBI(List<int> baseInst)
        {
            return new RepositoryInstalledBase().GetAllByBI(baseInst).Select(p => new EntityInstalledBase()
            {
                PK_InstalledBaseID = p.PK_InstalledBaseID,
                FK_ClientID = p.FK_ClientID,
                FK_ProductID = p.FK_ProductID,
                FK_ShopPlaceID = p.FK_ShopPlaceID,
                InstalledBaseID = p.InstalledBaseID,
                SerialNumber = p.SerialNumber,
                ShopDate = p.ShopDate,
                ShopPlaceIDFlag = p.ShopPlaceIDFlag,
                ShopDateFlag = p.ShopDateFlag,
                SerialNumberFlag = p.SerialNumberFlag,
                ProductIDFlag = p.ProductIDFlag,
                Model = p.Model,
                ProductName = p.ProductName,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityInstalledBase>();
        }

        public List<EntityInstalledBase> GetByClient(int ClientID)
        {
            return new RepositoryInstalledBase().GetByClient(ClientID);
        }

        public EntityInstalledBase GetByID (int FK_InstalledBaseID)
        {
            var objRepository = new RepositoryInstalledBase();
            return objRepository.Get(FK_InstalledBaseID);
        }

        public EntityInstalledBase GetByInstalledBase(string InstalledBaseID)
        {
            var objRepository = new RepositoryInstalledBase();
            return objRepository.GetByInstalledBase(InstalledBaseID);
        }
    }
}
