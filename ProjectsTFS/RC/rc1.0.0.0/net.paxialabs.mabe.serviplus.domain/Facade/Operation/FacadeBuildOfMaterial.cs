using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeBuildOfMaterial
    {
        public static void Insert(int PK_ProductID, string Model, string IDRefaccion,int cantidad,string DescripcionRefaccion, string EstatusMaterial)
        {
           new BusinessBuildOfMaterial().Insert(PK_ProductID, Model, IDRefaccion, cantidad, DescripcionRefaccion, EstatusMaterial);
        }
        public static void BulkInsert(List<EntityBuildOfMaterial> BOM)
        {
            new BusinessBuildOfMaterial().BulkInsert(BOM);
        }

        public void BulkUpdate(List<EntityBuildOfMaterial> BOM)
        {
            new BusinessBuildOfMaterial().BulkUpdate(BOM);
        }

        public static EntityBuildOfMaterial GetMaterialByModel(string Model)
        {
            return new BusinessBuildOfMaterial().GetMaterialByModel(Model);
        }

        public static EntityBuildOfMaterial GetMaterialBySparePart(string SparePart, int ProductID)
        {
            return new BusinessBuildOfMaterial().GetMaterialBySparePart(SparePart, ProductID);
        }

        public static void Update(EntityBuildOfMaterial Data, int Quantity)
        {
            new BusinessBuildOfMaterial().Update(Data, Quantity);
        }
        public static List<ModelViewSpareParts> GetListSpareParts(ModelViewUserG objCred)
        {
            return new BusinessBuildOfMaterial().GetListSpareParts(objCred);
        }
        public static List<ModelViewSpareParts> GetListSpareParts()
        {
            return new BusinessBuildOfMaterial().GetListSpareParts();
        }
        public static List<EntityBuildOfMaterial> GetAll()
        {
            return new BusinessBuildOfMaterial().GetAll();
        }

        //public static List<ModelViewSpareParts> GetListSparePartsComplete(ModelViewUserG objCred)
        //{
        //    return new BusinessBuildOfMaterial().GetListSparePartsComplete(objCred);
        //}
    }
}
