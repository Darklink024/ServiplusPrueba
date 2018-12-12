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
    public static class FacadeProduct
    {
        public static EntityProduct Insert(Material model)
        {
            return new BusinessProduct().Insert(model);
        }
        public static void BulkInsert(List<EntityProduct> Productos)
        {
            new BusinessProduct().BulkInsert(Productos);
        }

       
        public static void Update(Material model, EntityProduct prod)
        {
         new BusinessProduct().Update(model, prod);
        }

        public static void BulkUpdate(List<EntityProduct> Productos)
        {
            new BusinessProduct().BulkUpdate(Productos);
        }


        public static List<EntityProduct> GetAll()
        {
            return new BusinessProduct().GetAll();
        }

        public static EntityProduct GetByID(int FK_ProductID)
        {
            return new BusinessProduct().GetByID(FK_ProductID);
        }

        public static EntityProduct GetByModel(string Model)
        {
            return new BusinessProduct().GetByModel(Model);
        }

        public static List<ModelViewProducts> GetListProduct(ModelViewUserG objCred)
        {
            return new BusinessProduct().GetListProduct(objCred);
        }

    }
}
