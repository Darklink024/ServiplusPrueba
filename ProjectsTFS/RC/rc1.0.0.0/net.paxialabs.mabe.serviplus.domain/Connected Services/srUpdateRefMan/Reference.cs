﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace net.paxialabs.mabe.serviplus.domain.srUpdateRefMan {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/ActRefMan", ConfigurationName="srUpdateRefMan.SI_ActRefMan_OS")]
    public interface SI_ActRefMan_OS {
        
        // CODEGEN: Generating message contract since the operation SI_ActRefMan_OS is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://sap.com/xi/WebService/soap1.1", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSResponse SI_ActRefMan_OS(net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sap.com/xi/WebService/soap1.1", ReplyAction="*")]
        System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSResponse> SI_ActRefMan_OSAsync(net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/ActRefMan")]
    public partial class DT_ActRefMan_Out : object, System.ComponentModel.INotifyPropertyChanged {
        
        private DT_ActRefMan_OutSessionReference sessionReferenceField;
        
        private DT_ActRefMan_OutDestinoSistema destinoSistemaField;
        
        private DT_ActRefMan_OutItem[] itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public DT_ActRefMan_OutSessionReference SessionReference {
            get {
                return this.sessionReferenceField;
            }
            set {
                this.sessionReferenceField = value;
                this.RaisePropertyChanged("SessionReference");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public DT_ActRefMan_OutDestinoSistema DestinoSistema {
            get {
                return this.destinoSistemaField;
            }
            set {
                this.destinoSistemaField = value;
                this.RaisePropertyChanged("DestinoSistema");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public DT_ActRefMan_OutItem[] Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
                this.RaisePropertyChanged("Item");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/ActRefMan")]
    public partial class DT_ActRefMan_OutSessionReference : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string sateliteField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Satelite {
            get {
                return this.sateliteField;
            }
            set {
                this.sateliteField = value;
                this.RaisePropertyChanged("Satelite");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
                this.RaisePropertyChanged("Value");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/ActRefMan")]
    public partial class DT_ActRefMan_OutDestinoSistema : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string mandanteField;
        
        private string idiomaField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Mandante {
            get {
                return this.mandanteField;
            }
            set {
                this.mandanteField = value;
                this.RaisePropertyChanged("Mandante");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Idioma {
            get {
                return this.idiomaField;
            }
            set {
                this.idiomaField = value;
                this.RaisePropertyChanged("Idioma");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
                this.RaisePropertyChanged("Value");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/ActRefMan")]
    public partial class DT_ActRefMan_OutItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string iDOrdenField;
        
        private DT_ActRefMan_OutItemRefMan[] refManField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string IDOrden {
            get {
                return this.iDOrdenField;
            }
            set {
                this.iDOrdenField = value;
                this.RaisePropertyChanged("IDOrden");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RefMan", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public DT_ActRefMan_OutItemRefMan[] RefMan {
            get {
                return this.refManField;
            }
            set {
                this.refManField = value;
                this.RaisePropertyChanged("RefMan");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/ActRefMan")]
    public partial class DT_ActRefMan_OutItemRefMan : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string item_Ref_ManField;
        
        private string numeroPosicionField;
        
        private string cantidadField;
        
        private string estatusItem_RefManField;
        
        private string motivoRechazoField;
        
        private string proveedorField;
        
        private string numeroFacturaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string Item_Ref_Man {
            get {
                return this.item_Ref_ManField;
            }
            set {
                this.item_Ref_ManField = value;
                this.RaisePropertyChanged("Item_Ref_Man");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string NumeroPosicion {
            get {
                return this.numeroPosicionField;
            }
            set {
                this.numeroPosicionField = value;
                this.RaisePropertyChanged("NumeroPosicion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string Cantidad {
            get {
                return this.cantidadField;
            }
            set {
                this.cantidadField = value;
                this.RaisePropertyChanged("Cantidad");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string EstatusItem_RefMan {
            get {
                return this.estatusItem_RefManField;
            }
            set {
                this.estatusItem_RefManField = value;
                this.RaisePropertyChanged("EstatusItem_RefMan");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string MotivoRechazo {
            get {
                return this.motivoRechazoField;
            }
            set {
                this.motivoRechazoField = value;
                this.RaisePropertyChanged("MotivoRechazo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string Proveedor {
            get {
                return this.proveedorField;
            }
            set {
                this.proveedorField = value;
                this.RaisePropertyChanged("Proveedor");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public string NumeroFactura {
            get {
                return this.numeroFacturaField;
            }
            set {
                this.numeroFacturaField = value;
                this.RaisePropertyChanged("NumeroFactura");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/ActRefMan")]
    public partial class DT_ActRefMan_InItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string mensajeCRMField;
        
        private string iDOrdenField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string MensajeCRM {
            get {
                return this.mensajeCRMField;
            }
            set {
                this.mensajeCRMField = value;
                this.RaisePropertyChanged("MensajeCRM");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string IDOrden {
            get {
                return this.iDOrdenField;
            }
            set {
                this.iDOrdenField = value;
                this.RaisePropertyChanged("IDOrden");
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
    public partial class SI_ActRefMan_OSRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/ActRefMan", Order=0)]
        public net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.DT_ActRefMan_Out MT_ActRefMan_Out;
        
        public SI_ActRefMan_OSRequest() {
        }
        
        public SI_ActRefMan_OSRequest(net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.DT_ActRefMan_Out MT_ActRefMan_Out) {
            this.MT_ActRefMan_Out = MT_ActRefMan_Out;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SI_ActRefMan_OSResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/ActRefMan", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.DT_ActRefMan_InItem[] MT_ActRefMan_In;
        
        public SI_ActRefMan_OSResponse() {
        }
        
        public SI_ActRefMan_OSResponse(net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.DT_ActRefMan_InItem[] MT_ActRefMan_In) {
            this.MT_ActRefMan_In = MT_ActRefMan_In;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SI_ActRefMan_OSChannel : net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OS, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SI_ActRefMan_OSClient : System.ServiceModel.ClientBase<net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OS>, net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OS {
        
        public SI_ActRefMan_OSClient() {
        }
        
        public SI_ActRefMan_OSClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SI_ActRefMan_OSClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SI_ActRefMan_OSClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SI_ActRefMan_OSClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSResponse net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OS.SI_ActRefMan_OS(net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSRequest request) {
            return base.Channel.SI_ActRefMan_OS(request);
        }
        
        public net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.DT_ActRefMan_InItem[] SI_ActRefMan_OS(net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.DT_ActRefMan_Out MT_ActRefMan_Out) {
            net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSRequest inValue = new net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSRequest();
            inValue.MT_ActRefMan_Out = MT_ActRefMan_Out;
            net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSResponse retVal = ((net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OS)(this)).SI_ActRefMan_OS(inValue);
            return retVal.MT_ActRefMan_In;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSResponse> net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OS.SI_ActRefMan_OSAsync(net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSRequest request) {
            return base.Channel.SI_ActRefMan_OSAsync(request);
        }
        
        public System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSResponse> SI_ActRefMan_OSAsync(net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.DT_ActRefMan_Out MT_ActRefMan_Out) {
            net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSRequest inValue = new net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OSRequest();
            inValue.MT_ActRefMan_Out = MT_ActRefMan_Out;
            return ((net.paxialabs.mabe.serviplus.domain.srUpdateRefMan.SI_ActRefMan_OS)(this)).SI_ActRefMan_OSAsync(inValue);
        }
    }
}