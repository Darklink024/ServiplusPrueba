using net.paxialabs.mabe.serviplus.domain.Business.Interface;
using net.paxialabs.mabe.serviplus.entities.ModelView.Interface;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Interface
{
    public static class FacadeInterface
    {
        public static void Download(bool removeOrigin, out List<string> arrFiles, out List<string> arrFilesOK, out string DownloadFolder)
        {
            new BusinessInterface().DownloadFromSFTP(removeOrigin, out arrFiles, out arrFilesOK, out DownloadFolder);
        }

        public static void Process(string diretoryDown)
        {
            new BusinessInterface().ReadFiles(diretoryDown);
        }

        public static void Process()
        {
            new BusinessInterface().Process();
        }

        public static void Import(string diretoryDown)
        {
            new BusinessInterface().ImportFiles(diretoryDown);
        }

        public static void Geolocation()
        {
            new BusinessInterface().getGeolocation();
        }

        public static void GetWSAdds()
        {
            new BusinessInterface().GetFromWS();
        }

        public static void Files()
        {
            new BusinessInterface().ReadFiles(security.GlobalConfiguration.MabeSFTPFolderLocal);
        }

        public static void Test(string ods)
        {
            new BusinessMabe().ProcessODS(ods);
        }

        public static ModelViewResultREST SetModuleReserveSP(List<ModelViewApartadoModulo> data)
        {
           
            List<srModuleReserveSP.DT_CrearReserva_ReqTipo_de_operacionItem> items = new List<srModuleReserveSP.DT_CrearReserva_ReqTipo_de_operacionItem>();

            foreach (var item in data)
            {
                items.Add(new srModuleReserveSP.DT_CrearReserva_ReqTipo_de_operacionItem() {
                    AlmacenOrigen = item.AlmacenOrigen,
                    AlmacenReceptor = item.AlmacenReceptor,
                    Cantidad = item.Cantidad,
                    Centro = item.Centro,
                    IDOrden = item.OrderID,
                    Material = item.MaterialID,
                    TipoMovimiento = "915"
                });
            }

            return new BusinessMabe().ModuleReserveSP(items);
            
        }

        public static ModelViewResultREST SetADRReserveSP(List<ModelViewApartadoADR> data)
        {
            List<srADRReserveSP.DT_ProcessRPL_ReqTipo_de_operacionItem> items = new List<srADRReserveSP.DT_ProcessRPL_ReqTipo_de_operacionItem>();

            foreach (var item in data)
            {
                items.Add(new srADRReserveSP.DT_ProcessRPL_ReqTipo_de_operacionItem() {
                    AlmacenSolictador = item.AlmacenSolicitador,
                    Cantidad = item.Cantidad,
                    CentroSolictador = item.CentroSolicitador,
                    CentroSuministrador = item.CentroSunimistrador,
                    ClasePedido = "ZUFR",
                    GrupoCompras = "A03",
                    IDOrden = item.OrderID,
                    Material = item.MaterialID,
                    OrganizacionCompras = "PUMX",
                    Sociedad = item.Sociedad
                });
            }   

            return new BusinessMabe().ADRReserveSP(items);
        }
    }
}
