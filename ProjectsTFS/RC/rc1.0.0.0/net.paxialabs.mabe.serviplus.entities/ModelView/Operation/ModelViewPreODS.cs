using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
   public class ModelViewPreODS
    {
        //BaseInstalada
        public int FK_ProductID { get; set; }
        public int FK_ShopPlaceID { get; set; }
        public string SerialNumber { get; set; }
        public string ShopDate { get; set; }
        //Order
        public string OrderID { get; set; }
        public string ServiceType { get; set; }
        public string Note { get; set; }

        //Monitor
        public int? StatusVisitID { get; set; }
        public int? StatusOrderID { get; set; }
        public int? CauseOrderID { get; set; }
        public DateTime? StartVisitDate { get; set; }
        public DateTime? EndVisitDate { get; set; }
        public DateTime? StartOrderDate { get; set; }
        public DateTime? EndOrderDate { get; set; }
        public DateTime? StartServiceDate { get; set; }
        public DateTime? EndServiceDate { get; set; }
        public Single? LatitudeAddress { get; set; }
        public Single? LogitudeAddress { get; set; }
        public Single? LatitudeStartVisit { get; set; }
        public Single? LogitudeStartVisit { get; set; }
        public Single? LatitudeEndVisit { get; set; }
        public Single? LogitudeEndVisit { get; set; }
        public Single? LatitudeStartOrder { get; set; }
        public Single? LogitudeStartOrder { get; set; }
        public Single? LatitudeEndOrder { get; set; }
        public Single? LogitudeEndOrder { get; set; }
        public Nullable<System.TimeSpan> DurationVisit { get; set; }
        public Nullable<System.TimeSpan> DurationOrder { get; set; }
        public Nullable<System.TimeSpan> DurationExecute { get; set; }
        //public Nullable<System.TimeSpan> DurationTransport { get; set; }

        public List<SparePartDetail> SparePartDetails { get; set; }
        public List<Failure> Failures { get; set; }
    }

    public class SparePartDetail
    {
        public int BuildOfMaterialID { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
    }


}
