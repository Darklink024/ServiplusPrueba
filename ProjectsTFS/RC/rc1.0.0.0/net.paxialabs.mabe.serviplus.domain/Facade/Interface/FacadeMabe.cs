using net.paxialabs.mabe.serviplus.domain.Business.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.SAP;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Interface
{
    public static class FacadeMabe
    {
        public static List<srHistoryODS.DT_BusquedaODS_InItem> HistoryODSByClient(string ClientID, string InstalledBaseID)
        {
            return new BusinessMabe().HistoryODSByClient(ClientID, InstalledBaseID);
        }

        public static void SendCRM(DateTime fh, int maxProcess, bool reintent, bool extraKM)
        {
            new BusinessMabe(extraKM).ProcessODS(fh, maxProcess, reintent);
        }

        public static void SAPADRReserveSP(EntitySAPADRReserveSPRequest data)
        {
            new BusinessWSADRReserveSP().Send(data);
        }

        //public static EntitySAPOrdenVentaResult  SendPolicy(string ODS)
        //{
        //  return new BusinessMabe().Orden_Venta(ODS);
        // }
    }
}
