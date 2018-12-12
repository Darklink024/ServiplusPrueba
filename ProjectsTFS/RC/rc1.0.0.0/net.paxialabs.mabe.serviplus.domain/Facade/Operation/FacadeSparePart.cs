using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeSparePart
    {
        public static void Insert( int FK_OrderID, int? FK_BuildOfMaterialsID,  int? FK_ProductID, int? FK_WorkforceID, float Cantidad, string PosicionItem, string EstatusRefMan, string EstatusEsquema, string DescripcionRefMan, string RefManID, float Precio)
        {
             new BusinessSparePart().Insert( FK_OrderID, FK_BuildOfMaterialsID, FK_ProductID, FK_WorkforceID, Cantidad, PosicionItem, EstatusRefMan, EstatusEsquema, DescripcionRefMan,  RefManID, Precio);
        }

        public static void Update(EntitySparePart refa, float Cantidad, string PosicionItem, string EstatusRefMan, string EstatusEsquema, string DescripcionRefMan, string RefManID, float Precio)
        {
            new BusinessSparePart().Update(refa, Cantidad, PosicionItem, EstatusRefMan, EstatusEsquema, DescripcionRefMan,  RefManID, Precio);
        }

        public static void Update(EntitySparePart refa)
        {
            new BusinessSparePart().Update(refa);
        }

        public static List<EntitySparePart> GetAll()
        {
            return new BusinessSparePart().GetAll();
        }

        public static List<EntitySparePart> GetAll(string OrderID)
        {
            return new BusinessSparePart().GetByOrder(OrderID);
        }

        public static List<ModelViewSparePartsODS> GetByOrderID(int OrderID)
        {
            return new BusinessSparePart().GetByOrderID(OrderID);
        }
        public static EntitySparePart GetByRefManID(int OrderID, string RefManID)
        {
            return new BusinessSparePart().GetByRefManID(OrderID,RefManID);
        }


        public static void UpdateSparePartsODS(List<SparePart> SpareParts)
        {
            new  BusinessSparePart().UpdateSparePartsODS(SpareParts);
        }
    }
}
