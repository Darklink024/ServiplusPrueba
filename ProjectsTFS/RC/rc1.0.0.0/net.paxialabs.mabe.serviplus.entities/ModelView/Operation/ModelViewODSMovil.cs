using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewODSMovil
    {
        public int VisitID { get; set; }
        public int Secuence { get; set; }
        public string BoroghtName { get; set; }
        public string PostalCode { get; set; }
        public string Municipality { get; set; }
        public string ArrivalTime { get; set; }
        public string ODS { get; set; }
        public string ProductModel { get; set; }
        public object DetailClient  { get; set; }
        public object DetailODS { get; set; }
        public List<HistoricProduct> HistoricProduct { get; set; }
        public List<HistoricClient> HistoricClient { get; set; }
        public List<SparePart> SpareParts { get; set; }
        public List<SymptomsODS> SymptomsODS { get; set; }
        public bool FutureService { get; set; }
        public string OrderExecuteDate { get; set; }



    }

    public class ModelViewODSMovilUploadList
    {
        public List<ModelViewODSMovilUpload> Visits { get; set; }
    }

    public class ModelViewODSMovilUpload : Entity.Operation.EntityMonitorOrder
    {
        
        public DetailODS Detail { get; set; }
        public List<SparePart> SpareParts { get; set; }
        public SymptomsODS SymptomsODS { get; set; }
        public Payment PaymentODS { get; set; }
    }

    public class DetailClient
    {
        public string ClientID { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string NumberExternalAddress { get; set; }
        public string NumberInternalAddress { get; set; }
        public string BoroughAddress { get; set; }
        public string MunicipalityAddress { get; set; }
        public string LatitudeAddress { get; set; }
        public string LogitudeAddress { get; set; }
        public string Town { get; set; }
        public string ODS { get; set; }
        public string ProductModel { get; set; }
        public string ClientName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Extension { get; set; }
        public string Email { get; set; }
        public string RFC { get; set; }
        

    }

    public class DetailODS
    {
        public string ODS { get; set; }
        public string ProductModel { get; set; }
        public string ProductDescription { get; set; }
        public string SerialNumber { get; set; }
        public string Fault { get; set; }
        public string ServiceType { get; set; }
        public string Notes { get; set; }
        public string PurchaseDate { get; set; }
        public string PurchasePlace { get; set; }
        public string OrderFather { get; set; }
        public string EmployeeID { get; set; }
        
    }

    public class HistoricProduct
    {
        public string ODSID { get; set; }
        public string ODS { get; set; }
        public string Status { get; set; }
        public string CloseDate { get; set; }
        public string Faults { get; set; }        
    }
    
    public class HistoricClient
    {
        public string ODSID { get; set; }
        public string Product { get; set; }
        public string ShopDate { get; set; }      

    }

    public class SymptomsODS
    {
        public string SymptomDescription { get; set; }
        public List<FaultsODS> FaultsODS { get; set; }
    }

    public class SparePart
    {
        public int PK_SparePartsID { get; set; }
        public int BuildOfMaterialsID { get; set; }
        public int WorkForceID { get; set; }
        public string SparePartsDescription { get; set; }
        public string SparePartsCode { get; set; }
        public string SparePartsStatus { get; set; }
        public string sparePartNotes { get; set; }
        public int SparePartsQuantity { get; set; }
        public double SparePartsPrice { get; set; }
        public string OrderID { get; set; }
        public string CentroSuministrador { get; set; }
        public string CentroSolicitador { get; set; }
         public string AlmacenSolicitador { get; set; }
    }

    public class FaultsODS
    {
        public int PK_CodeFailureID { get; set; }
        public string FaultDescription { get; set; }
        public string FaultCode { get; set; }
    }

    public class Payment
    {
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public string TypePaymentID { get; set; }
        public string AuthorizationPayment { get; set; }
        public DateTime DatePayment { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }

    
    



}
