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
    internal class BusinessValidationGuarantyBOM
    {
        public List<ModelViewGuarantyBOM> GetLisValidationBOM(ModelViewUserG objCred)
        {
            var NegocioUsuario = new BusinessUsers();
            var NegocioEmpleado = new BusinessEmployee();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            //var lista = new List<EntityValidationGuarantyBOM>();

            var lt = new List<EntityValidationGuarantyBOM>();
            if (objCred.Date == null)
            {
                lt = GetAll();
            }
            else
            {
                lt = GetAll().Where(p => p.ModifyDate >= objCred.Date).ToList();
            }
            return lt.Select(p => new ModelViewGuarantyBOM()
            {
                ValidationGuarantySparePartID = p.PK_ValidationGuarantySparePartID,
                ProductID = p.FK_ProducID.HasValue ? p.FK_ProducID.Value : 0,
                BuildOfMaterialsID = p.FK_BuildOfMaterialsID.HasValue ? p.FK_BuildOfMaterialsID.Value : 0,
                SalesOrganization = p.SalesOrganization,
                SparePartsID = p.SparePartsID,
                Model = p.Model,
                ClientID = p.ClientID,
                Months = p.Months,
                ValidFrom = p.ValidFrom.Value.ToString("yyyy-MM-dd"),
                ValidTo = p.ValidTo.Value.ToString("yyyy-MM-dd"),
            }).ToList<ModelViewGuarantyBOM>();


            //var NegocioOrdenes = new BusinessOrder();
            //var NegocioBase = new BusinessInstalledBase();
            //var ordenes = NegocioOrdenes.GetAll().Where(p => empleado.Select(q => q.PK_EmployeeID).ToArray<int>().Contains(p.FK_EmployeeID.Value) && p.OrderExecuteDate >= objCred.Date.Value.Date);
            //var prod = NegocioBase.GetAll();
            //var modelos = (from c in ordenes
            //               join p in prod on c.FK_InstalledBaseID equals p.PK_InstalledBaseID
            //               select p.FK_ProductID).Distinct();
            //var lista = GetAll();
            //return (from d in lista
            //        join e in modelos on d.FK_ProducID equals e
            //        select new ModelViewGuarantyBOM()
            //        {
            //            ValidationGuarantySparePartID = d.PK_ValidationGuarantySparePartID,
            //            ProductID = d.FK_ProducID.HasValue ? d.FK_ProducID.Value : 0,
            //            BuildOfMaterialsID = d.FK_BuildOfMaterialsID.HasValue ? d.FK_BuildOfMaterialsID.Value : 0,
            //            SalesOrganization = d.SalesOrganization,
            //            SparePartsID = d.SparePartsID,
            //            Model = d.Model,
            //            ClientID = d.ClientID,
            //            Months = d.Months,
            //            ValidFrom = d.ValidFrom.Value.ToString("yyyy-MM-dd"),
            //            ValidTo = d.ValidTo.Value.ToString("yyyy-MM-dd"),
            //        }).ToList<ModelViewGuarantyBOM>();
        }
        public List<EntityValidationGuarantyBOM> GetAll()
        {
            return new RepositoryValidationGuarantyBOM().GetAll().Select(p => new EntityValidationGuarantyBOM()
            {
                PK_ValidationGuarantySparePartID = p.PK_ValidationGuarantySparePartID,
                FK_BuildOfMaterialsID = p.FK_BuildOfMaterialsID,
                FK_ProducID = p.FK_ProducID,
                Model = p.Model,
                SalesOrganization = p.SalesOrganization,
                SparePartsID = p.SparePartsID,
                ClientID = p.ClientID,
                Months = p.Months,
                ValidFrom = p.ValidFrom,
                ValidTo = p.ValidTo,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityValidationGuarantyBOM>();
        }

        public void BulkMerge(List<EntityValidationGuarantyBOM> data)
        {
            new RepositoryValidationGuarantyBOM().BulkMerge(data);
        }
    }
}
