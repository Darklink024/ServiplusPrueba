﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace net.paxialabs.mabe.serviplus.domain.srReprogramingODS {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/ReprogramacionODS", ConfigurationName="srReprogramingODS.SI_CONFINAL_ReprogramacionODS")]
    public interface SI_CONFINAL_ReprogramacionODS {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sap.com/xi/WebService/soap1.1", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        net.paxialabs.mabe.serviplus.domain.srReprogramingODS.ReprogramacionODSResponse ReprogramacionODS(net.paxialabs.mabe.serviplus.domain.srReprogramingODS.ReprogramacionODSRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sap.com/xi/WebService/soap1.1", ReplyAction="*")]
        System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.domain.srReprogramingODS.ReprogramacionODSResponse> ReprogramacionODSAsync(net.paxialabs.mabe.serviplus.domain.srReprogramingODS.ReprogramacionODSRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/ReprogramacionODS")]
    public partial class DT_ReprogramacionODS_SendItems : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string sessionReferenceField;
        
        private string sateliteField;
        
        private DT_ReprogramacionODS_SendItemsDestino destinoField;
        
        private DT_ReprogramacionODS_SendItemsItem itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string SessionReference {
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
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public DT_ReprogramacionODS_SendItemsDestino Destino {
            get {
                return this.destinoField;
            }
            set {
                this.destinoField = value;
                this.RaisePropertyChanged("Destino");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public DT_ReprogramacionODS_SendItemsItem Item {
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/ReprogramacionODS")]
    public partial class DT_ReprogramacionODS_SendItemsDestino : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string sistemaField;
        
        private string mandanteField;
        
        private string idiomaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Sistema {
            get {
                return this.sistemaField;
            }
            set {
                this.sistemaField = value;
                this.RaisePropertyChanged("Sistema");
            }
        }
        
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/ReprogramacionODS")]
    public partial class DT_ReprogramacionODS_SendItemsItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string oDSField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string ODS {
            get {
                return this.oDSField;
            }
            set {
                this.oDSField = value;
                this.RaisePropertyChanged("ODS");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/ReprogramacionODS")]
    public partial class DT_ReprogramacionODS_ReceiveItemsReturn : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tipoMensajeField;
        
        private string claseMensajeField;
        
        private string mensajeField;
        
        private string descripcionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string TipoMensaje {
            get {
                return this.tipoMensajeField;
            }
            set {
                this.tipoMensajeField = value;
                this.RaisePropertyChanged("TipoMensaje");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string ClaseMensaje {
            get {
                return this.claseMensajeField;
            }
            set {
                this.claseMensajeField = value;
                this.RaisePropertyChanged("ClaseMensaje");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string Mensaje {
            get {
                return this.mensajeField;
            }
            set {
                this.mensajeField = value;
                this.RaisePropertyChanged("Mensaje");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string Descripcion {
            get {
                return this.descripcionField;
            }
            set {
                this.descripcionField = value;
                this.RaisePropertyChanged("Descripcion");
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
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ReprogramacionODSRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/ReprogramacionODS", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Items", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public net.paxialabs.mabe.serviplus.domain.srReprogramingODS.DT_ReprogramacionODS_SendItems[] MT_ReprogramacionODS_Out;
        
        public ReprogramacionODSRequest() {
        }
        
        public ReprogramacionODSRequest(net.paxialabs.mabe.serviplus.domain.srReprogramingODS.DT_ReprogramacionODS_SendItems[] MT_ReprogramacionODS_Out) {
            this.MT_ReprogramacionODS_Out = MT_ReprogramacionODS_Out;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ReprogramacionODSResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/ReprogramacionODS", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Items", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false, NestingLevel=1)]
        public net.paxialabs.mabe.serviplus.domain.srReprogramingODS.DT_ReprogramacionODS_ReceiveItemsReturn[][] MT_ReprogramacionODS_In;
        
        public ReprogramacionODSResponse() {
        }
        
        public ReprogramacionODSResponse(net.paxialabs.mabe.serviplus.domain.srReprogramingODS.DT_ReprogramacionODS_ReceiveItemsReturn[][] MT_ReprogramacionODS_In) {
            this.MT_ReprogramacionODS_In = MT_ReprogramacionODS_In;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SI_CONFINAL_ReprogramacionODSChannel : net.paxialabs.mabe.serviplus.domain.srReprogramingODS.SI_CONFINAL_ReprogramacionODS, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SI_CONFINAL_ReprogramacionODSClient : System.ServiceModel.ClientBase<net.paxialabs.mabe.serviplus.domain.srReprogramingODS.SI_CONFINAL_ReprogramacionODS>, net.paxialabs.mabe.serviplus.domain.srReprogramingODS.SI_CONFINAL_ReprogramacionODS {
        
        public SI_CONFINAL_ReprogramacionODSClient() {
        }
        
        public SI_CONFINAL_ReprogramacionODSClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SI_CONFINAL_ReprogramacionODSClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SI_CONFINAL_ReprogramacionODSClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SI_CONFINAL_ReprogramacionODSClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public net.paxialabs.mabe.serviplus.domain.srReprogramingODS.ReprogramacionODSResponse ReprogramacionODS(net.paxialabs.mabe.serviplus.domain.srReprogramingODS.ReprogramacionODSRequest request) {
            return base.Channel.ReprogramacionODS(request);
        }
        
        public System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.domain.srReprogramingODS.ReprogramacionODSResponse> ReprogramacionODSAsync(net.paxialabs.mabe.serviplus.domain.srReprogramingODS.ReprogramacionODSRequest request) {
            return base.Channel.ReprogramacionODSAsync(request);
        }
    }
}
