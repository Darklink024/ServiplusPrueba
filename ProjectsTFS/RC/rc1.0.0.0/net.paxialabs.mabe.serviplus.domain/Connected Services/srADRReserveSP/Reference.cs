﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace net.paxialabs.mabe.serviplus.domain.srADRReserveSP {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/Process_RPL", ConfigurationName="srADRReserveSP.SI_Process_RPL_OA")]
    public interface SI_Process_RPL_OA {
        
        // CODEGEN: Generating message contract since the operation SI_Process_RPL_OA is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://sap.com/xi/WebService/soap1.1", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OAResponse SI_Process_RPL_OA(net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OARequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sap.com/xi/WebService/soap1.1", ReplyAction="*")]
        System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OAResponse> SI_Process_RPL_OAAsync(net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OARequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/Process_RPL")]
    public partial class DT_ProcessRPL_Req : object, System.ComponentModel.INotifyPropertyChanged {
        
        private DT_ProcessRPL_ReqSessionReference sessionReferenceField;
        
        private DT_ProcessRPL_ReqDestinoSistema destinoSistemaField;
        
        private DT_ProcessRPL_ReqTipo_de_operacionItem[] tipo_de_operacionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public DT_ProcessRPL_ReqSessionReference SessionReference {
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
        public DT_ProcessRPL_ReqDestinoSistema DestinoSistema {
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
        [System.Xml.Serialization.XmlArrayItemAttribute("item", typeof(DT_ProcessRPL_ReqTipo_de_operacionItem), Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public DT_ProcessRPL_ReqTipo_de_operacionItem[] Tipo_de_operacion {
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/Process_RPL")]
    public partial class DT_ProcessRPL_ReqSessionReference : object, System.ComponentModel.INotifyPropertyChanged {
        
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/Process_RPL")]
    public partial class DT_ProcessRPL_Res : object, System.ComponentModel.INotifyPropertyChanged {
        
        private DT_ProcessRPL_ResItem[] itemField;
        
        private DT_ProcessRPL_ResReturn[] returnField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public DT_ProcessRPL_ResItem[] item {
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
        public DT_ProcessRPL_ResReturn[] Return {
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/Process_RPL")]
    public partial class DT_ProcessRPL_ResItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string iDOrdenField;
        
        private string iDOrdenCompraField;
        
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
        public string IDOrdenCompra {
            get {
                return this.iDOrdenCompraField;
            }
            set {
                this.iDOrdenCompraField = value;
                this.RaisePropertyChanged("IDOrdenCompra");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/Process_RPL")]
    public partial class DT_ProcessRPL_ResReturn : object, System.ComponentModel.INotifyPropertyChanged {
        
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/Process_RPL")]
    public partial class DT_ProcessRPL_ReqDestinoSistema : object, System.ComponentModel.INotifyPropertyChanged {
        
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mabe.com/MW/CONFINAL/Mobile/Process_RPL")]
    public partial class DT_ProcessRPL_ReqTipo_de_operacionItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string iDOrdenField;
        
        private string materialField;
        
        private string cantidadField;
        
        private string organizacionComprasField;
        
        private string grupoComprasField;
        
        private string sociedadField;
        
        private string centroSuministradorField;
        
        private string centroSolictadorField;
        
        private string almacenSolictadorField;
        
        private string clasePedidoField;
        
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
        public string OrganizacionCompras {
            get {
                return this.organizacionComprasField;
            }
            set {
                this.organizacionComprasField = value;
                this.RaisePropertyChanged("OrganizacionCompras");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string GrupoCompras {
            get {
                return this.grupoComprasField;
            }
            set {
                this.grupoComprasField = value;
                this.RaisePropertyChanged("GrupoCompras");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string Sociedad {
            get {
                return this.sociedadField;
            }
            set {
                this.sociedadField = value;
                this.RaisePropertyChanged("Sociedad");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public string CentroSuministrador {
            get {
                return this.centroSuministradorField;
            }
            set {
                this.centroSuministradorField = value;
                this.RaisePropertyChanged("CentroSuministrador");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public string CentroSolictador {
            get {
                return this.centroSolictadorField;
            }
            set {
                this.centroSolictadorField = value;
                this.RaisePropertyChanged("CentroSolictador");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=8)]
        public string AlmacenSolictador {
            get {
                return this.almacenSolictadorField;
            }
            set {
                this.almacenSolictadorField = value;
                this.RaisePropertyChanged("AlmacenSolictador");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=9)]
        public string ClasePedido {
            get {
                return this.clasePedidoField;
            }
            set {
                this.clasePedidoField = value;
                this.RaisePropertyChanged("ClasePedido");
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
    public partial class SI_Process_RPL_OARequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/Process_RPL", Order=0)]
        public net.paxialabs.mabe.serviplus.domain.srADRReserveSP.DT_ProcessRPL_Req MT_ProcessRPL_Req;
        
        public SI_Process_RPL_OARequest() {
        }
        
        public SI_Process_RPL_OARequest(net.paxialabs.mabe.serviplus.domain.srADRReserveSP.DT_ProcessRPL_Req MT_ProcessRPL_Req) {
            this.MT_ProcessRPL_Req = MT_ProcessRPL_Req;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SI_Process_RPL_OAResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://mabe.com/MW/CONFINAL/Mobile/Process_RPL", Order=0)]
        public net.paxialabs.mabe.serviplus.domain.srADRReserveSP.DT_ProcessRPL_Res MT_ProcessRPL_Res;
        
        public SI_Process_RPL_OAResponse() {
        }
        
        public SI_Process_RPL_OAResponse(net.paxialabs.mabe.serviplus.domain.srADRReserveSP.DT_ProcessRPL_Res MT_ProcessRPL_Res) {
            this.MT_ProcessRPL_Res = MT_ProcessRPL_Res;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SI_Process_RPL_OAChannel : net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OA, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SI_Process_RPL_OAClient : System.ServiceModel.ClientBase<net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OA>, net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OA {
        
        public SI_Process_RPL_OAClient() {
        }
        
        public SI_Process_RPL_OAClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SI_Process_RPL_OAClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SI_Process_RPL_OAClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SI_Process_RPL_OAClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OAResponse net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OA.SI_Process_RPL_OA(net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OARequest request) {
            return base.Channel.SI_Process_RPL_OA(request);
        }
        
        public net.paxialabs.mabe.serviplus.domain.srADRReserveSP.DT_ProcessRPL_Res SI_Process_RPL_OA(net.paxialabs.mabe.serviplus.domain.srADRReserveSP.DT_ProcessRPL_Req MT_ProcessRPL_Req) {
            net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OARequest inValue = new net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OARequest();
            inValue.MT_ProcessRPL_Req = MT_ProcessRPL_Req;
            net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OAResponse retVal = ((net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OA)(this)).SI_Process_RPL_OA(inValue);
            return retVal.MT_ProcessRPL_Res;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OAResponse> net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OA.SI_Process_RPL_OAAsync(net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OARequest request) {
            return base.Channel.SI_Process_RPL_OAAsync(request);
        }
        
        public System.Threading.Tasks.Task<net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OAResponse> SI_Process_RPL_OAAsync(net.paxialabs.mabe.serviplus.domain.srADRReserveSP.DT_ProcessRPL_Req MT_ProcessRPL_Req) {
            net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OARequest inValue = new net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OARequest();
            inValue.MT_ProcessRPL_Req = MT_ProcessRPL_Req;
            return ((net.paxialabs.mabe.serviplus.domain.srADRReserveSP.SI_Process_RPL_OA)(this)).SI_Process_RPL_OAAsync(inValue);
        }
    }
}
