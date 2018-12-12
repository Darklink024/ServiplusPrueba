using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
   public class EntityInstalledBase
    {
        public int PK_InstalledBaseID { get; set; }
        public int FK_ClientID { get; set; }
        public Nullable<int> FK_ProductID { get; set; }
        public Nullable<int> FK_ShopPlaceID { get; set; }
        public string InstalledBaseID { get; set; }
        public string SerialNumber { get; set; }
        public Nullable<System.DateTime> ShopDate { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<bool> ShopPlaceIDFlag { get; set; }
        public Nullable<bool> SerialNumberFlag { get; set; }
        public Nullable<bool> ShopDateFlag { get; set; }
        public Nullable<bool> ProductIDFlag { get; set; }
        public string Model { get; set; }
        public string ProductName { get; set; }
    }
}
