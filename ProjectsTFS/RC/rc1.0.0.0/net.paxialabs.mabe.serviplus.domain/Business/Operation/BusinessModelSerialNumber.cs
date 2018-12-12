using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessModelSerialNumber
    {

        public List<ModelViewSerialNumber> GetListModel(ModelViewUserG objCred)
        {
            var NegocioBase = new BusinessInstalledBase();
            var NegocioUsuario = new BusinessUsers();
            var NegocioOrdenes = new BusinessOrder();
            var NegocioValidacionSN = new BusinessValidationsSerialNumber();
            var NegocioEmpleado = new BusinessEmployee();
            var user = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            var empleado = NegocioEmpleado.GetByUserID(user.UserID);
            var ordenes = NegocioOrdenes.GetAll().Where(p => empleado.Select(q => q.PK_EmployeeID).ToList<int>().Contains(p.FK_EmployeeID.Value) && p.OrderExecuteDate >= objCred.Date);
            var prod = NegocioBase.GetAll();
            var SerialNumber = GetAll();
            var Validation = NegocioValidacionSN.GetAll();
            if(objCred.ProductID == 0)
            {
                
              var modelos = (from c in ordenes
                             join p in prod on c.FK_InstalledBaseID equals p.PK_InstalledBaseID
                             select p.FK_ProductID).Distinct().ToList();
                modelos = modelos.Where(x => x != null).ToList();
                var lt = (from c in SerialNumber
                          join p in Validation on c.PK_ModelSerialNumberID equals p.FK_ModelSerialNumberID
                          select new ModelViewSerialNumber()
                          {
                              ModelSerialNumberID = c.PK_ModelSerialNumberID,
                              ProductID = c.FK_ProducID,
                              Model = c.Model,
                              ValidationFormatID = c.ValidationFormatID,
                              ValidDate = c.ValidDate,
                              InitialDate = c.InitialDate != null ? c.InitialDate.Value.ToString("yyyy-MM-dd") : "",
                              EndDate = c.EndDate != null ? c.EndDate.Value.ToString("yyyy-MM-dd") : "",
                              ValidationsSerialNumberID = p.PK_ValidationsSerialNumberID,
                              ValidationName = p.ValidationName,
                              InitialPosition = p.InitialPosition,
                              FinalPosition = p.FinalPosition,
                              Allowed = p.Allowed,
                              RankID = p.RankID
                          }).ToList();


                return (from c in modelos
                        join p in lt on c equals p.ProductID
                        select p).ToList();

            }
            else
            {
                return (from c in SerialNumber.Where(p=> p.FK_ProducID== objCred.ProductID)
                        join p in Validation on c.PK_ModelSerialNumberID equals p.FK_ModelSerialNumberID
                        select new ModelViewSerialNumber()
                        {
                            ModelSerialNumberID = c.PK_ModelSerialNumberID,
                            ProductID = c.FK_ProducID,
                            Model = c.Model,
                            ValidationFormatID = c.ValidationFormatID,
                            ValidDate = c.ValidDate,
                            InitialDate = c.InitialDate != null ? c.InitialDate.Value.ToString("yyyy-MM-dd") : "",
                            EndDate = c.EndDate != null ? c.EndDate.Value.ToString("yyyy-MM-dd") : "",
                            ValidationsSerialNumberID = p.PK_ValidationsSerialNumberID,
                            ValidationName = p.ValidationName,
                            InitialPosition = p.InitialPosition,
                            FinalPosition = p.FinalPosition,
                            Allowed = p.Allowed,
                            RankID = p.RankID
                        }).ToList();


            }



            //return lt;
        }
        public List<EntityModelSerialNumber> GetAll()
        {
            return new RepositoryModelSerialNumber().GetAll().Select(p => new EntityModelSerialNumber()
            {
                PK_ModelSerialNumberID = p.PK_ModelSerialNumberID,
                FK_ProducID = p.FK_ProducID,
                Model = p.Model,
                ValidationFormatID = p.ValidationFormatID,
                InitialDate = p.InitialDate,
                EndDate = p.EndDate,
                ValidDate = p.ValidDate,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityModelSerialNumber>();
        }

        public void BulkMerge(List<EntityModelSerialNumber> data)
        {
            new RepositoryModelSerialNumber().BulkMerge(data);
        }

    }
}
