﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión del motor en tiempo de ejecución:2.0.50727.8784
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.3038.
// 
namespace mx {
    using System.Xml.Serialization;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Diagnostics;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SI_AvailabilityStock_OutBinding", Namespace="http://mabe.com/MW/HD/CYM/ATP")]
    public partial class SI_AvailabilityStock_OutService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SI_AvailabilityStock_OutOperationCompleted;
        
        /// <remarks/>
        public SI_AvailabilityStock_OutService() {
            this.Url = "http://srvmabetest05.mabenet.corpmabe.com:50000/XISOAPAdapter/MessageServlet?send" +
                "erParty=&senderService=BS_MABE_HOMEDEPOT_QAS&receiverParty=&receiverService=&int" +
                "erface=SI_AvailabilityStock_Out&interfaceNamespace=http%3A%2F%2Fmabe.com%2FMW%2F" +
                "HD%2FCYM%2FATP";
        }
        
        /// <remarks/>
        public event SI_AvailabilityStock_OutCompletedEventHandler SI_AvailabilityStock_OutCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlArrayAttribute("MT_AvailabilityStockResponse", Namespace="http://mabe.com/MW/HD/CYM/ATP")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public DT_AvailabilityStockResponseItem[] SI_AvailabilityStock_Out([System.Xml.Serialization.XmlElementAttribute(Namespace="http://mabe.com/MW/HD/CYM/ATP")] DT_AvailabilityStock MT_AvailabilityStock) {
            object[] results = this.Invoke("SI_AvailabilityStock_Out", new object[] {
                        MT_AvailabilityStock});
            return ((DT_AvailabilityStockResponseItem[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSI_AvailabilityStock_Out(DT_AvailabilityStock MT_AvailabilityStock, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SI_AvailabilityStock_Out", new object[] {
                        MT_AvailabilityStock}, callback, asyncState);
        }
        
        /// <remarks/>
        public DT_AvailabilityStockResponseItem[] EndSI_AvailabilityStock_Out(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((DT_AvailabilityStockResponseItem[])(results[0]));
        }
        
        /// <remarks/>
        public void SI_AvailabilityStock_OutAsync(DT_AvailabilityStock MT_AvailabilityStock) {
            this.SI_AvailabilityStock_OutAsync(MT_AvailabilityStock, null);
        }
        
        /// <remarks/>
        public void SI_AvailabilityStock_OutAsync(DT_AvailabilityStock MT_AvailabilityStock, object userState) {
            if ((this.SI_AvailabilityStock_OutOperationCompleted == null)) {
                this.SI_AvailabilityStock_OutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSI_AvailabilityStock_OutOperationCompleted);
            }
            this.InvokeAsync("SI_AvailabilityStock_Out", new object[] {
                        MT_AvailabilityStock}, this.SI_AvailabilityStock_OutOperationCompleted, userState);
        }
        
        private void OnSI_AvailabilityStock_OutOperationCompleted(object arg) {
            if ((this.SI_AvailabilityStock_OutCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SI_AvailabilityStock_OutCompleted(this, new SI_AvailabilityStock_OutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mabe.com/MW/HD/CYM/ATP")]
    public partial class DT_AvailabilityStock {
        
        private string destinoField;
        
        private DT_AvailabilityStockItem[] itemField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Destino {
            get {
                return this.destinoField;
            }
            set {
                this.destinoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DT_AvailabilityStockItem[] item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/HD/CYM/ATP")]
    public partial class DT_AvailabilityStockItem {
        
        private string mATERIALField;
        
        private string sTOREField;
        
        private string qUANTITYField;
        
        private string dATEField;
        
        private string pLANTField;
        
        private string sTORAGELOCATIONField;
        
        private string cUSTOMERField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MATERIAL {
            get {
                return this.mATERIALField;
            }
            set {
                this.mATERIALField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string STORE {
            get {
                return this.sTOREField;
            }
            set {
                this.sTOREField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string QUANTITY {
            get {
                return this.qUANTITYField;
            }
            set {
                this.qUANTITYField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DATE {
            get {
                return this.dATEField;
            }
            set {
                this.dATEField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PLANT {
            get {
                return this.pLANTField;
            }
            set {
                this.pLANTField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string STORAGELOCATION {
            get {
                return this.sTORAGELOCATIONField;
            }
            set {
                this.sTORAGELOCATIONField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CUSTOMER {
            get {
                return this.cUSTOMERField;
            }
            set {
                this.cUSTOMERField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/HD/CYM/ATP")]
    public partial class DT_AvailabilityStockResponseItem {
        
        private string pLANTField;
        
        private string mATERIALField;
        
        private string sTOREField;
        
        private string qUANTITYField;
        
        private string sTORAGELOCATIONField;
        
        private System.DateTime aVAILABILITYDATEField;
        
        private bool aVAILABILITYDATEFieldSpecified;
        
        private string eRRORField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PLANT {
            get {
                return this.pLANTField;
            }
            set {
                this.pLANTField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MATERIAL {
            get {
                return this.mATERIALField;
            }
            set {
                this.mATERIALField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string STORE {
            get {
                return this.sTOREField;
            }
            set {
                this.sTOREField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string QUANTITY {
            get {
                return this.qUANTITYField;
            }
            set {
                this.qUANTITYField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string STORAGELOCATION {
            get {
                return this.sTORAGELOCATIONField;
            }
            set {
                this.sTORAGELOCATIONField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime AVAILABILITYDATE {
            get {
                return this.aVAILABILITYDATEField;
            }
            set {
                this.aVAILABILITYDATEField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AVAILABILITYDATESpecified {
            get {
                return this.aVAILABILITYDATEFieldSpecified;
            }
            set {
                this.aVAILABILITYDATEFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ERROR {
            get {
                return this.eRRORField;
            }
            set {
                this.eRRORField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void SI_AvailabilityStock_OutCompletedEventHandler(object sender, SI_AvailabilityStock_OutCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SI_AvailabilityStock_OutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SI_AvailabilityStock_OutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public DT_AvailabilityStockResponseItem[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((DT_AvailabilityStockResponseItem[])(this.results[0]));
            }
        }
    }
}
