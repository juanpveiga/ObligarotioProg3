﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClienteWCF.ServicioObtenerEmprendimientos {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DTOEmprendimiento", Namespace="http://schemas.datacontract.org/2004/07/WCF")]
    [System.SerializableAttribute()]
    public partial class DTOEmprendimiento : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CodIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double CostoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescripcionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PuntajeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TiempoEjecucionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TituloField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CodId {
            get {
                return this.CodIdField;
            }
            set {
                if ((this.CodIdField.Equals(value) != true)) {
                    this.CodIdField = value;
                    this.RaisePropertyChanged("CodId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Costo {
            get {
                return this.CostoField;
            }
            set {
                if ((this.CostoField.Equals(value) != true)) {
                    this.CostoField = value;
                    this.RaisePropertyChanged("Costo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Descripcion {
            get {
                return this.DescripcionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescripcionField, value) != true)) {
                    this.DescripcionField = value;
                    this.RaisePropertyChanged("Descripcion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Puntaje {
            get {
                return this.PuntajeField;
            }
            set {
                if ((this.PuntajeField.Equals(value) != true)) {
                    this.PuntajeField = value;
                    this.RaisePropertyChanged("Puntaje");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TiempoEjecucion {
            get {
                return this.TiempoEjecucionField;
            }
            set {
                if ((this.TiempoEjecucionField.Equals(value) != true)) {
                    this.TiempoEjecucionField = value;
                    this.RaisePropertyChanged("TiempoEjecucion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Titulo {
            get {
                return this.TituloField;
            }
            set {
                if ((object.ReferenceEquals(this.TituloField, value) != true)) {
                    this.TituloField = value;
                    this.RaisePropertyChanged("Titulo");
                }
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicioObtenerEmprendimientos.IObtenerEmprendimientos")]
    public interface IObtenerEmprendimientos {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IObtenerEmprendimientos/mostrarEmprendimientos", ReplyAction="http://tempuri.org/IObtenerEmprendimientos/mostrarEmprendimientosResponse")]
        ClienteWCF.ServicioObtenerEmprendimientos.DTOEmprendimiento[] mostrarEmprendimientos();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IObtenerEmprendimientos/mostrarEmprendimientos", ReplyAction="http://tempuri.org/IObtenerEmprendimientos/mostrarEmprendimientosResponse")]
        System.Threading.Tasks.Task<ClienteWCF.ServicioObtenerEmprendimientos.DTOEmprendimiento[]> mostrarEmprendimientosAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IObtenerEmprendimientosChannel : ClienteWCF.ServicioObtenerEmprendimientos.IObtenerEmprendimientos, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ObtenerEmprendimientosClient : System.ServiceModel.ClientBase<ClienteWCF.ServicioObtenerEmprendimientos.IObtenerEmprendimientos>, ClienteWCF.ServicioObtenerEmprendimientos.IObtenerEmprendimientos {
        
        public ObtenerEmprendimientosClient() {
        }
        
        public ObtenerEmprendimientosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ObtenerEmprendimientosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ObtenerEmprendimientosClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ObtenerEmprendimientosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ClienteWCF.ServicioObtenerEmprendimientos.DTOEmprendimiento[] mostrarEmprendimientos() {
            return base.Channel.mostrarEmprendimientos();
        }
        
        public System.Threading.Tasks.Task<ClienteWCF.ServicioObtenerEmprendimientos.DTOEmprendimiento[]> mostrarEmprendimientosAsync() {
            return base.Channel.mostrarEmprendimientosAsync();
        }
    }
}
