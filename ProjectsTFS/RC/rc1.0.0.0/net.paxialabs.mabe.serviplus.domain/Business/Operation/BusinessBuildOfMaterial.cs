using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
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
    internal class BusinessBuildOfMaterial
    {
        public void Insert(int PK_ProductID, string Model, string IDRefaccion, int cantidad, string DescripcionRefaccion, string EstatusMaterial)
        {
            var objRepository = new RepositoryBuildOfMaterial();

            EntityBuildOfMaterial data = new EntityBuildOfMaterial()
            {

                FK_ProductID = PK_ProductID,
                Model = Model,
                SparePartsID = IDRefaccion,
                Quantity = cantidad,
                StatusBOM = EstatusMaterial,
                SparePartDescription = DescripcionRefaccion,
                Status = true,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.UtcNow

             };
            data = objRepository.Insert(data);

        }

        public void BulkInsert(List<EntityBuildOfMaterial> BOM)
        {
            var objRepository = new RepositoryBuildOfMaterial();
            objRepository.BulkInsert(BOM);

        }

        public void BulkUpdate(List<EntityBuildOfMaterial> BOM)
        {
            var objRepository = new RepositoryBuildOfMaterial();
            objRepository.BulkUpdate(BOM);

        }

        public void Update(EntityBuildOfMaterial Data, int Quantity)
        {
            var objRepository = new RepositoryBuildOfMaterial();

            EntityBuildOfMaterial data = new EntityBuildOfMaterial()
            {
                PK_BuildOfMaterialsID = Data.PK_BuildOfMaterialsID,
                FK_ProductID = Data.FK_ProductID,
                Model = Data.Model,
                SparePartsID = Data.SparePartsID,
                Quantity = Quantity,
                StatusBOM = Data.StatusBOM,
                Status = Data.Status,
                CreateDate = Data.CreateDate,
                ModifyDate = Data.ModifyDate

            };
            data = objRepository.Update(data);

        }

   

        public EntityBuildOfMaterial GetMaterialByModel(string Model)
        {
            var objRepository = new RepositoryBuildOfMaterial();
            return objRepository.GetMaterialByModel(Model);
        }

        public EntityBuildOfMaterial GetMaterialBySparePart(string SparePart, int ProductID)
        {
            var objRepository = new RepositoryBuildOfMaterial();
            return objRepository.GetMaterialBySparePart(SparePart, ProductID);
        }

        //public List<ModelViewSpareParts> GetListSparePartsComplete(ModelViewUserG objCred)
        //{
        //    return new RepositoryBuildOfMaterial().GetList(objCred);
        //}


        public List<ModelViewSpareParts> GetListSpareParts(ModelViewUserG objCred)
        {
            var NegocioBase = new BusinessInstalledBase();
            var NegocioUsuario = new BusinessUsers();
            var NegocioOrdenes = new BusinessOrder();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioManoObra = new BusinessWorkforce();
            var user = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            var empleado = NegocioEmpleado.GetByUserID(user.UserID);


            var lista = new List<ModelViewSpareParts>();
            if (objCred.ProductID == 0)
            {
                //var ordenes1 = NegocioOrdenes.GetAll().Where(p => empleado.Select(q => q.PK_EmployeeID).ToArray<int>().Contains(p.FK_EmployeeID.Value) && p.OrderExecuteDate >= objCred.Date);
                var ordenes = NegocioOrdenes.GetAll(empleado.Select(q => q.PK_EmployeeID).ToList(), objCred.Date.Value, false);
                List<int> baseInstalada = ordenes.Select(p => p.FK_InstalledBaseID).ToList();
                //var prod = NegocioBase.GetAll();
                //var modelos = (from c in ordenes
                //               join p in prod on c.FK_InstalledBaseID equals p.PK_InstalledBaseID
                //               select p.FK_ProductID).Distinct();
                var modelos = NegocioBase.GetAllByBI(baseInstalada).Select(p=> p.FK_ProductID).Distinct();
                modelos = modelos.Where(x => x != null).ToList();
                List<int> modelo = modelos.Where(x => x != null).Cast<int>().ToList(); 
                lista = GetListSparePartsByModel(modelo); 
                //lista = (from c in modelos
                //         join p in GetAll() on c equals p.FK_ProductID
                //         select new ModelViewSpareParts()
                //         {
                //             BuildOfMaterialsID = p.PK_BuildOfMaterialsID,
                //             ProductID = p.FK_ProductID,
                //             Model = p.Model,
                //             SparePartsID = p.SparePartsID,
                //             Quantity = p.Quantity,
                //             SpartePartDescription = p.SparePartDescription,
                //             StatusBOM = p.StatusBOM,
                //             Status = p.Status
                //         }).ToList<ModelViewSpareParts>();
            }
            else
            {
                lista = GetAll().Where(p => p.FK_ProductID == objCred.ProductID).Select(p => new ModelViewSpareParts()
                {
                    BuildOfMaterialsID = p.PK_BuildOfMaterialsID,
                    ProductID = p.FK_ProductID,
                    Model = p.Model,
                    SparePartsID = p.SparePartsID,
                    Quantity = p.Quantity,
                    SpartePartDescription = p.SparePartDescription,
                    StatusBOM = p.StatusBOM,
                    Status = p.Status
                }).ToList<ModelViewSpareParts>();
            }
            var demo2 = NegocioManoObra.GetAll().Where(p => p.WorkforceID == "8011161600000031").Select(p=> new ModelViewSpareParts
             {
                BuildOfMaterialsID = 0,
                ProductID = 0,
                Model = "",
                SparePartsID = p.WorkforceID,
                Quantity = 1,
                SpartePartDescription = p.Description,
                StatusBOM = "",
                Status = p.Status,
             }).First();

            var demo3 = NegocioManoObra.GetAll().Where(p => p.WorkforceID == "8011161600000032").Select(p => new ModelViewSpareParts
            {
                BuildOfMaterialsID = 0,
                ProductID = 0,
                Model = "",
                SparePartsID = p.WorkforceID,
                Quantity = 1,
                SpartePartDescription = p.Description,
                StatusBOM = "",
                Status = p.Status,
            }).First();

            lista.Add(demo2);
            lista.Add(demo3);

            return lista;

        }

            public List<ModelViewSpareParts> GetListSpareParts()
        {
            return GetAll().Select(p => new ModelViewSpareParts()
            {
                BuildOfMaterialsID = p.PK_BuildOfMaterialsID,
                ProductID = p.FK_ProductID,
                Model = p.Model,
                SparePartsID = p.SparePartsID,
                Quantity = p.Quantity,
                SpartePartDescription = p.SparePartDescription,
                StatusBOM = p.StatusBOM,
                Status = p.Status
            }).ToList<ModelViewSpareParts>();

        }

        public List<ModelViewSpareParts> GetListSparePartsByModel(List<int> modelos)
        {
            return new RepositoryBuildOfMaterial().GetAllSparePartByModel(modelos).Select(p => new ModelViewSpareParts()
            {
                BuildOfMaterialsID = p.PK_BuildOfMaterialsID,
                ProductID = p.FK_ProductID,
                Model = p.Model,
                SparePartsID = p.SparePartsID,
                Quantity = p.Quantity,
                SpartePartDescription = p.SparePartDescription,
                StatusBOM = p.StatusBOM,
                Status = p.Status
            }).ToList<ModelViewSpareParts>();

        }


        public List<EntityBuildOfMaterial> GetAll()
        {
            return new RepositoryBuildOfMaterial().GetAll().Select(p => new EntityBuildOfMaterial()
            {
                PK_BuildOfMaterialsID = p.PK_BuildOfMaterialsID,
                FK_ProductID = p.FK_ProductID,
                Model = p.Model,
                SparePartsID = p.SparePartsID,
                Quantity = p.Quantity,
                StatusBOM = p.StatusBOM,
                SparePartDescription = p.SparePartDescription,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityBuildOfMaterial>();
        }


        public List<EntityBuildOfMaterial> GetAllSparePart(List<string> SparePartsID)
        {
            return new RepositoryBuildOfMaterial().GetAllSparePart(SparePartsID);
        }



        public void BulkMerge(List<EntityBuildOfMaterial> data)
        {
            new RepositoryBuildOfMaterial().BulkMerge(data);
        }
        public EntityBuildOfMaterial GetByBuildofMaterial(int FK_BuildMaterial)
        {
            return new RepositoryBuildOfMaterial().GetByBOMID(FK_BuildMaterial);
        }
        public EntityBuildOfMaterial GetByID(int ID)
        {
            return new RepositoryBuildOfMaterial().Get(ID);
        }
    }
}
