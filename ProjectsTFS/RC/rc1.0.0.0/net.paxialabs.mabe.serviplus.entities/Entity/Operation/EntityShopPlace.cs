using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityShopPlace
    {
        public int PK_ShopPlaceID { get; set; }
        public Nullable<int> FK_StateID { get; set; }
        public string ShopPlaceID { get; set; }
        public string ShopPlace1 { get; set; }
        public string CountryAddress { get; set; }
        public string StateAddress { get; set; }
        public string CityAddress { get; set; }
        public string MunicipalityAddress { get; set; }
        public string StreetAddress { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string ClientID { get; set; }
    }
}
