using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeCodeFailureByProduct
    {
        public static void Insert(int PK_CodeFailureID, int PK_ProductID, string Complejidad)
        {
            new BusinessCodeFailureByProduct().Insert(PK_CodeFailureID, PK_ProductID, Complejidad);
        }

        public static void BulkInsert(List<EntityCodeFailureByProduct> FallasProductos)
        {
            new BusinessCodeFailureByProduct().BulkInsert(FallasProductos);
        }
        public static void Update(EntityCodeFailure codFa, int PK_ProductID, string Complejidad)
        {
            new BusinessCodeFailureByProduct().Update(codFa, PK_ProductID, Complejidad);
        }

        public static void BulkUpdate(List<EntityCodeFailureByProduct> FallasProductos)
        {
            new BusinessCodeFailureByProduct().BulkUpdate(FallasProductos);
        }

        public static List<ModelViewFailures> GetListCodeFailure(ModelViewUserG objCred)
        {
            return new BusinessCodeFailureByProduct().GetListCodeFailure(objCred);
        }
        public static List<EntityCodeFailureByProduct> GetAll()
        {
            return new BusinessCodeFailureByProduct().GetAll();
        }

        public static EntityCodeFailureByProduct GetFailureProduct(int FailureID, int ProductID)
        {
            return new BusinessCodeFailureByProduct().GetFailureProduct(FailureID, ProductID);
        }
    }
}
