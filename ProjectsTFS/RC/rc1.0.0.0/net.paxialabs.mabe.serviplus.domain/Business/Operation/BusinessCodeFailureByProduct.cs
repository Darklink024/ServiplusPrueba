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
    internal class BusinessCodeFailureByProduct
    {
        public void Insert(int PK_CodeFailureID, int PK_ProductID, string Complejidad)
        {
            var objRepository = new RepositoryCodeFailureByProduct();
            int? cant;
            cant = string.IsNullOrEmpty(Complejidad) ? null : (int?)Convert.ToInt32(Complejidad);
            EntityCodeFailureByProduct data = new EntityCodeFailureByProduct()
            {
                FK_CodeFailureID = PK_CodeFailureID,
                FK_ProductID = PK_ProductID,
                Complexity = cant,
                Status = true,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Insert(data);

        }

        public void BulkInsert(List<EntityCodeFailureByProduct> FallasProductos)
        {
            var objRepository = new RepositoryCodeFailureByProduct();
            objRepository.BulkInsert(FallasProductos);

        }

        public void BulkUpdate(List<EntityCodeFailureByProduct> FallasProductos)
        {
            var objRepository = new RepositoryCodeFailureByProduct();
            objRepository.BulkUpdate(FallasProductos);

        }

        public void Update(EntityCodeFailure codFa, int PK_ProductID, string Complejidad)
        {
            var objRepository = new RepositoryCodeFailureByProduct();
            int? cant;
            cant = string.IsNullOrEmpty(Complejidad) ? null : (int?)Convert.ToInt32(Complejidad);
            EntityCodeFailureByProduct data = new EntityCodeFailureByProduct()
            {
                FK_CodeFailureID = codFa.PK_CodeFailureID,
                FK_ProductID = PK_ProductID,
                Complexity = cant,
                Status = codFa.Status,
                CreateDate = codFa.CreateDate,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Update(data);

        }



        public List<ModelViewFailures> GetListCodeFailure(ModelViewUserG objCred)
        {
            var NegocioUsuario = new BusinessUsers();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);

            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");
            var NegocioOrdenes = new BusinessOrder();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioCodigoFalla = new BusinessCodeFailure();
            var NegocioBase = new BusinessInstalledBase();
            var codefailure = NegocioCodigoFalla.GetAll();
            var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
            if (objCred.ProductID == 0)
            {
                //var ordenes = NegocioOrdenes.GetAll().Where(p => empleado.Select(q => q.PK_EmployeeID).ToArray<int>().Contains(p.FK_EmployeeID.Value) && p.OrderExecuteDate >= objCred.Date.Value.Date);
                //var prod = NegocioBase.GetAll();
                //var modelos = (from c in ordenes
                //               join p in prod on c.FK_InstalledBaseID equals p.PK_InstalledBaseID
                //               select p.FK_ProductID).Distinct().ToList();
                //modelos = modelos.Where(x => x != null).ToList();
                var ordenes = NegocioOrdenes.GetAll(empleado.Select(q => q.PK_EmployeeID).ToList(), objCred.Date.Value, false);
                List<int> baseInstalada = ordenes.Select(p => p.FK_InstalledBaseID).ToList();
                var modelos = NegocioBase.GetAllByBI(baseInstalada).Select(p => p.FK_ProductID).Distinct();
                modelos = modelos.Where(x => x != null).ToList();
                List<int> modelo = modelos.Where(x => x != null).Cast<int>().ToList();
                var codfallas = new List<ModelViewFailures>();
                foreach (var item in modelos)
                {

                   var x = (from c in GetByProductID(item.Value)
                          join p in codefailure on c.FK_CodeFailureID equals p.PK_CodeFailureID
                          select new ModelViewFailures()
                          {
                              CodeFailureID = p.PK_CodeFailureID,
                              ProductID = c.FK_ProductID,
                              CodeFailure = p.CodeFailure1,
                              Failure = p.Failure,
                              Complexity = c.Complexity.GetValueOrDefault(),
                              Status = c.Status
                          }).ToList();

                    codfallas.AddRange(x);
                }
                return codfallas;
                //return (from c in modelos
                //        join p in GetAll() on c equals p.FK_ProductID
                //        join d in  codefailure on p.FK_CodeFailureID equals d.PK_CodeFailureID
                //        select new ModelViewFailures()
                //        {
                //            CodeFailureID = p.FK_CodeFailureID,
                //            ProductID = p.FK_ProductID,
                //            CodeFailure = d.CodeFailure1,
                //            Failure = d.Failure,
                //            Complexity = p.Complexity.GetValueOrDefault(),
                //            Status = p.Status
                //        }).ToList();
            }
            else
            {
                return  (from c in GetByProductID(objCred.ProductID) 
                        join p in codefailure on c.FK_CodeFailureID equals p.PK_CodeFailureID
                                 select new ModelViewFailures()
                                 {
                                     CodeFailureID = p.PK_CodeFailureID,
                                     ProductID = c.FK_ProductID,
                                     CodeFailure = p.CodeFailure1,
                                     Failure = p.Failure,
                                     Complexity = c.Complexity.GetValueOrDefault(),
                                     Status = c.Status
                                 }).ToList();
            }

         }

        public List<EntityCodeFailureByProduct> GetAll()
        {
            return new RepositoryCodeFailureByProduct().GetAll().Select(p => new EntityCodeFailureByProduct()
            {
                FK_CodeFailureID = p.FK_CodeFailureID,
                FK_ProductID = p.FK_ProductID,
                Complexity = p.Complexity,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityCodeFailureByProduct>();
        }



        public EntityCodeFailureByProduct GetFailureProduct(int FailureID, int ProductID)
        {
            var objRepository = new RepositoryCodeFailureByProduct();
            return objRepository.GetFailureProduct(FailureID, ProductID);
        }

        public List<EntityCodeFailureByProduct> GetByProductID(int ID)
        {
            return new RepositoryCodeFailureByProduct().GetByProductID(ID);
        }
    }
}
