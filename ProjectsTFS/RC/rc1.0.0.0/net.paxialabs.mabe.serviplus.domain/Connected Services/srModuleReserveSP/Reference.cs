﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace net.paxialabs.mabe.serviplus.domain.srModuleReserveSP {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/Crear_Reserva", ConfigurationName="srModuleReserveSP.SI_CrearReserva_OS")]
    public interface SI_CrearReserva_OS {
        
        // CODEGEN: Generating message contract since the operation SI_CrearReserva_OS is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://sap.com/xi/WebService/soap1.1", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSResponse SI_CrearReserva_OS(net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sap.com/xi/WebService/soap1.1", ReplyAction="*")]
        System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSResponse> SI_CrearReserva_OSAsync(net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/Crear_Reserva")]
    public partial class DT_CrearReserva_Req : object, System.ComponentModel.INotifyPropertyChanged {
        
        private DT_CrearReserva_ReqSessionReference sessionReferenceField;
        
        private DT_CrearReserva_ReqDestinoSistema destinoSistemaField;
        
        private DT_CrearReserva_ReqTipo_de_operacionItem[] tipo_de_operacionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public DT_CrearReserva_ReqSessionReference SessionReference {
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
        public DT_CrearReserva_ReqDestinoSistema DestinoSistema {
            get {
                return this.destinoSistemaField;
            }
            set {
                this.destinoSistemaField = value;
                this.RaisePropertyChanged("DestinoSistema");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", typeof(DT_CrearReserva_ReqTipo_de_operacionItem), Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public DT_CrearReserva_ReqTipo_de_operacionItem[] Tipo_de_operacion {
            get {
                return this.tipo_de_operacionField;
            }
            set {
                this.tipo_de_operacionField = value;
                this.RaisePropertyChanged("Tipo_de_operacion");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/Crear_Reserva")]
    public partial class DT_CrearReserva_ReqSessionReference : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string sateliteField;
        
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/Crear_Reserva")]
    public partial class DT_CrearReserva_Res : object, System.ComponentModel.INotifyPropertyChanged {
        
        private DT_CrearReserva_ResItem[] itemField;
        
        private DT_CrearReserva_ResReturn[] returnField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public DT_CrearReserva_ResItem[] item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
                this.RaisePropertyChanged("item");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public DT_CrearReserva_ResReturn[] Return {
            get {
                return this.returnField;
            }
            set {
                this.returnField = value;
                this.RaisePropertyChanged("Return");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/Crear_Reserva")]
    public partial class DT_CrearReserva_ResItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string iDOrdenField;
        
        private string iDReservaField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string IDReserva {
            get {
                return this.iDReservaField;
            }
            set {
                this.iDReservaField = value;
                this.RaisePropertyChanged("IDReserva");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/Crear_Reserva")]
    public partial class DT_CrearReserva_ResReturn : object, System.ComponentModel.INotifyPropertyChanged {
        
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/Crear_Reserva")]
    public partial class DT_CrearReserva_ReqDestinoSistema : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string mandanteField;
        
        private string idiomaField;
        
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
        public string idioma {
            get {
                return this.idiomaField;
            }
            set {
                this.idiomaField = value;
                this.RaisePropertyChanged("idioma");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/Crear_Reserva")]
    public partial class DT_CrearReserva_ReqTipo_de_operacionItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string iDOrdenField;
        
        private string materialField;
        
        private string cantidadField;
        
        private string centroField;
        
        private string almacenReceptorField;
        
        private string almacenOrigenField;
        
        private string tipoMovimientoField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string Material {
            get {
                return this.materialField;
            }
            set {
                this.materialField = value;
                this.RaisePropertyChanged("Material");
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
        public string Centro {
            get {
                return this.centroField;
            }
            set {
                this.centroField = value;
                this.RaisePropertyChanged("Centro");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string AlmacenReceptor {
            get {
                return this.almacenReceptorField;
            }
            set {
                this.almacenReceptorField = value;
                this.RaisePropertyChanged("AlmacenReceptor");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string AlmacenOrigen {
            get {
                return this.almacenOrigenField;
            }
            set {
                this.almacenOrigenField = value;
                this.RaisePropertyChanged("AlmacenOrigen");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public string TipoMovimiento {
            get {
                return this.tipoMovimientoField;
            }
            set {
                this.tipoMovimientoField = value;
                this.RaisePropertyChanged("TipoMovimiento");
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
    public partial class SI_CrearReserva_OSRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/Crear_Reserva", Order=0)]
        public net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.DT_CrearReserva_Req MT_CrearReserva_Req;
        
        public SI_CrearReserva_OSRequest() {
        }
        
        public SI_CrearReserva_OSRequest(net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.DT_CrearReserva_Req MT_CrearReserva_Req) {
            this.MT_CrearReserva_Req = MT_CrearReserva_Req;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SI_CrearReserva_OSResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/Crear_Reserva", Order=0)]
        public net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.DT_CrearReserva_Res MT_CrearReserva_Res;
        
        public SI_CrearReserva_OSResponse() {
        }
        
        public SI_CrearReserva_OSResponse(net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.DT_CrearReserva_Res MT_CrearReserva_Res) {
            this.MT_CrearReserva_Res = MT_CrearReserva_Res;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SI_CrearReserva_OSChannel : net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OS, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SI_CrearReserva_OSClient : System.ServiceModel.ClientBase<net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OS>, net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OS {
        
        public SI_CrearReserva_OSClient() {
        }
        
        public SI_CrearReserva_OSClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SI_CrearReserva_OSClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SI_CrearReserva_OSClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SI_CrearReserva_OSClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSResponse net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OS.SI_CrearReserva_OS(net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSRequest request) {
            return base.Channel.SI_CrearReserva_OS(request);
        }
        
        public net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.DT_CrearReserva_Res SI_CrearReserva_OS(net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.DT_CrearReserva_Req MT_CrearReserva_Req) {
            net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSRequest inValue = new net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSRequest();
            inValue.MT_CrearReserva_Req = MT_CrearReserva_Req;
            net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSResponse retVal = ((net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OS)(this)).SI_CrearReserva_OS(inValue);
            return retVal.MT_CrearReserva_Res;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSResponse> net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OS.SI_CrearReserva_OSAsync(net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSRequest request) {
            return base.Channel.SI_CrearReserva_OSAsync(request);
        }
        
        public System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSResponse> SI_CrearReserva_OSAsync(net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.DT_CrearReserva_Req MT_CrearReserva_Req) {
            net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSRequest inValue = new net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OSRequest();
            inValue.MT_CrearReserva_Req = MT_CrearReserva_Req;
            return ((net.paxialabs.mabe.serviplus.domain.srModuleReserveSP.SI_CrearReserva_OS)(this)).SI_CrearReserva_OSAsync(inValue);
        }
    }
}
