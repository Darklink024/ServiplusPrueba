﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace net.paxialabs.mabe.serviplus.console.srInventario {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://mabe.com/MW/HD/CYM/ATP", ConfigurationName="srInventario.SI_AvailabilityStock_Out")]
    public interface SI_AvailabilityStock_Out {
        
        // CODEGEN: Generating message contract since the operation SI_AvailabilityStock_Out is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://sap.com/xi/WebService/soap1.1", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutResponse SI_AvailabilityStock_Out(net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sap.com/xi/WebService/soap1.1", ReplyAction="*")]
        System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutResponse> SI_AvailabilityStock_OutAsync(net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mabe.com/MW/HD/CYM/ATP")]
    public partial class DT_AvailabilityStock : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string destinoField;
        
        private DT_AvailabilityStockItem[] itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string Destino {
            get {
                return this.destinoField;
            }
            set {
                this.destinoField = value;
                this.RaisePropertyChanged("Destino");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public DT_AvailabilityStockItem[] item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
                this.RaisePropertyChanged("item");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/HD/CYM/ATP")]
    public partial class DT_AvailabilityStockItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string mATERIALField;
        
        private string sTOREField;
        
        private string qUANTITYField;
        
        private string dATEField;
        
        private string pLANTField;
        
        private string sTORAGELOCATIONField;
        
        private string cUSTOMERField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string MATERIAL {
            get {
                return this.mATERIALField;
            }
            set {
                this.mATERIALField = value;
                this.RaisePropertyChanged("MATERIAL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string STORE {
            get {
                return this.sTOREField;
            }
            set {
                this.sTOREField = value;
                this.RaisePropertyChanged("STORE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string QUANTITY {
            get {
                return this.qUANTITYField;
            }
            set {
                this.qUANTITYField = value;
                this.RaisePropertyChanged("QUANTITY");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string DATE {
            get {
                return this.dATEField;
            }
            set {
                this.dATEField = value;
                this.RaisePropertyChanged("DATE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string PLANT {
            get {
                return this.pLANTField;
            }
            set {
                this.pLANTField = value;
                this.RaisePropertyChanged("PLANT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string STORAGELOCATION {
            get {
                return this.sTORAGELOCATIONField;
            }
            set {
                this.sTORAGELOCATIONField = value;
                this.RaisePropertyChanged("STORAGELOCATION");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public string CUSTOMER {
            get {
                return this.cUSTOMERField;
            }
            set {
                this.cUSTOMERField = value;
                this.RaisePropertyChanged("CUSTOMER");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/HD/CYM/ATP")]
    public partial class DT_AvailabilityStockResponseItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string pLANTField;
        
        private string mATERIALField;
        
        private string sTOREField;
        
        private string qUANTITYField;
        
        private string sTORAGELOCATIONField;
        
        private System.DateTime aVAILABILITYDATEField;
        
        private bool aVAILABILITYDATEFieldSpecified;
        
        private string eRRORField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string PLANT {
            get {
                return this.pLANTField;
            }
            set {
                this.pLANTField = value;
                this.RaisePropertyChanged("PLANT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string MATERIAL {
            get {
                return this.mATERIALField;
            }
            set {
                this.mATERIALField = value;
                this.RaisePropertyChanged("MATERIAL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string STORE {
            get {
                return this.sTOREField;
            }
            set {
                this.sTOREField = value;
                this.RaisePropertyChanged("STORE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string QUANTITY {
            get {
                return this.qUANTITYField;
            }
            set {
                this.qUANTITYField = value;
                this.RaisePropertyChanged("QUANTITY");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string STORAGELOCATION {
            get {
                return this.sTORAGELOCATIONField;
            }
            set {
                this.sTORAGELOCATIONField = value;
                this.RaisePropertyChanged("STORAGELOCATION");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="date", Order=5)]
        public System.DateTime AVAILABILITYDATE {
            get {
                return this.aVAILABILITYDATEField;
            }
            set {
                this.aVAILABILITYDATEField = value;
                this.RaisePropertyChanged("AVAILABILITYDATE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AVAILABILITYDATESpecified {
            get {
                return this.aVAILABILITYDATEFieldSpecified;
            }
            set {
                this.aVAILABILITYDATEFieldSpecified = value;
                this.RaisePropertyChanged("AVAILABILITYDATESpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public string ERROR {
            get {
                return this.eRRORField;
            }
            set {
                this.eRRORField = value;
                this.RaisePropertyChanged("ERROR");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SI_AvailabilityStock_OutRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://mabe.com/MW/HD/CYM/ATP", Order=0)]
        public net.paxialabs.mabe.serviplus.console.srInventario.DT_AvailabilityStock MT_AvailabilityStock;
        
        public SI_AvailabilityStock_OutRequest() {
        }
        
        public SI_AvailabilityStock_OutRequest(net.paxialabs.mabe.serviplus.console.srInventario.DT_AvailabilityStock MT_AvailabilityStock) {
            this.MT_AvailabilityStock = MT_AvailabilityStock;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SI_AvailabilityStock_OutResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://mabe.com/MW/HD/CYM/ATP", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public net.paxialabs.mabe.serviplus.console.srInventario.DT_AvailabilityStockResponseItem[] MT_AvailabilityStockResponse;
        
        public SI_AvailabilityStock_OutResponse() {
        }
        
        public SI_AvailabilityStock_OutResponse(net.paxialabs.mabe.serviplus.console.srInventario.DT_AvailabilityStockResponseItem[] MT_AvailabilityStockResponse) {
            this.MT_AvailabilityStockResponse = MT_AvailabilityStockResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SI_AvailabilityStock_OutChannel : net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_Out, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SI_AvailabilityStock_OutClient : System.ServiceModel.ClientBase<net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_Out>, net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_Out {
        
        public SI_AvailabilityStock_OutClient() {
        }
        
        public SI_AvailabilityStock_OutClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SI_AvailabilityStock_OutClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SI_AvailabilityStock_OutClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SI_AvailabilityStock_OutClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutResponse net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_Out.SI_AvailabilityStock_Out(net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutRequest request) {
            return base.Channel.SI_AvailabilityStock_Out(request);
        }
        
        public net.paxialabs.mabe.serviplus.console.srInventario.DT_AvailabilityStockResponseItem[] SI_AvailabilityStock_Out(net.paxialabs.mabe.serviplus.console.srInventario.DT_AvailabilityStock MT_AvailabilityStock) {
            net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutRequest inValue = new net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutRequest();
            inValue.MT_AvailabilityStock = MT_AvailabilityStock;
            net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutResponse retVal = ((net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_Out)(this)).SI_AvailabilityStock_Out(inValue);
            return retVal.MT_AvailabilityStockResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutResponse> net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_Out.SI_AvailabilityStock_OutAsync(net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutRequest request) {
            return base.Channel.SI_AvailabilityStock_OutAsync(request);
        }
        
        public System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutResponse> SI_AvailabilityStock_OutAsync(net.paxialabs.mabe.serviplus.console.srInventario.DT_AvailabilityStock MT_AvailabilityStock) {
            net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutRequest inValue = new net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_OutRequest();
            inValue.MT_AvailabilityStock = MT_AvailabilityStock;
            return ((net.paxialabs.mabe.serviplus.console.srInventario.SI_AvailabilityStock_Out)(this)).SI_AvailabilityStock_OutAsync(inValue);
        }
    }
}
