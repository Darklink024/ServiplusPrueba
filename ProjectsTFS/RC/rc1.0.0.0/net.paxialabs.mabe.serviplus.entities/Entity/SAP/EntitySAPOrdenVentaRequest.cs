using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.SAP
{
    public class EntitySAPOrdenVentaRequest
    {
        public string Destino { get; set; }
        public string Satelite { get; set; }
       
        public List<EntitySAPOrdenVentaRequestItem> Items { get; set; }
    }

    public class EntitySAPOrdenVentaRequestItem
    {
        public string OrdenWeb { get; set; }
        public string AliasNegocio { get; set; }
        public string ClaseDocumento { get; set; }
        public string OrganizacionVentas { get; set; }
        public string CanalDistribucion { get; set; }
        public string Sector { get; set; }
        public string NumCliente { get; set; }
        public string FuncionInterlocutorSP { get; set; }
        public string NumDestinatario { get; set; }
        public string FuncionInterlocutorSH { get; set; }
        public string NombreSH { get; set; }
        public string NombreSH1 { get; set; }
        public string NombreSH2 { get; set; }
        public string NombreSH3 { get; set; }
        public string CalleNum { get; set; }
        public string NumInterior { get; set; }
        public string NumExteriorSup { get; set; }
        public string ColoniaSH { get; set; }
        public string CodigoPostalSH { get; set; }
        public string Delegacion { get; set; }
        public string PaisSH { get; set; }
        public string RegionSH { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string FaxSH { get; set; }
        public string RFCSH { get; set; }
        public string EmailSH { get; set; }
        public string FuncionInterlocutorZR { get; set; }
        public string NumEjecutivoVentas { get; set; }
        public string NumOrdenCompra { get; set; }
        public string PreciosCliente { get; set; }
        public string FechaCompra { get; set; }
        public string FechaEntrega { get; set; }
        public string BloqueoEntrega { get; set; }
        public string FechaPrecio { get; set; }
        public string MonedaDoc { get; set; }
        public string MotivoPedido { get; set; }
        public string Incoterms1 { get; set; }
        public string Incoterms2 { get; set; }
        public string CondiPago { get; set; }
        public string CondiEmbarque { get; set; }
        public string ViaPago { get; set; }
        public string ClasificacionFiscal { get; set; }
        public string PosicionPedido { get; set; }
        public string MaterialSKU { get; set; }
        public string CantPedido { get; set; }
        public string UniMedida { get; set; }
        public string FechaEntregaItem { get; set; }
        public string Centro { get; set; }
        public string Almacen { get; set; }
        public string Ruta { get; set; }
        public string NumEmpleado { get; set; }
        public string NombEmpleado { get; set; }
        public string UbicacionEmpleado { get; set; }
        public string FormaPago { get; set; }
        public string TexCabecera { get; set; }
        public List<EntitySAPOrdenVentaRequestClaseCondicion> ClaseCondicion { get; set; }
        public string NombreSHFac { get; set; }
        public string NombreSH1Fac { get; set; }
        public string CalleNumFac { get; set; }
        public string NumInteriorFac { get; set; }
        public string NumExteriorSupFac { get; set; }
        public string ColoniaSHFac { get; set; }
        public string CodigoPostalSHFac { get; set; }
        public string DelegacionFac { get; set; }
        public string PaisSHFac { get; set; }
        public string RegionSHFac { get; set; }
        public string RFCSHFac { get; set; }
        public string EmailSHFac { get; set; }
        public string Telefono1Fac { get; set; }
        public string Telefono2Fac { get; set; }
        public string PersonaFisica { get; set; }
        public string TipoIdentFiscal { get; set; }
        public string GrupoMateriales4 { get; set; }
        public string GrupoCondiciones1 { get; set; }
        public string TipoListaPrecios { get; set; }
        public string PolizaSN { get; set; }

    }

    public class EntitySAPOrdenVentaRequestClaseCondicion
    {
        public string Clase { get; set; }
        public string Valor { get; set; }
    }
}
