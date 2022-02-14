﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HostServiceApplication.LoginReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LoginData", Namespace="http://schemas.datacontract.org/2004/07/LoginServiceApplication")]
    [System.SerializableAttribute()]
    public partial class LoginData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessField;
        
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
        public System.Nullable<int> Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success {
            get {
                return this.SuccessField;
            }
            set {
                if ((this.SuccessField.Equals(value) != true)) {
                    this.SuccessField = value;
                    this.RaisePropertyChanged("Success");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LoginReference.ILoginService")]
    public interface ILoginService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/LoginUser", ReplyAction="http://tempuri.org/ILoginService/LoginUserResponse")]
        bool LoginUser(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/LoginUser", ReplyAction="http://tempuri.org/ILoginService/LoginUserResponse")]
        System.Threading.Tasks.Task<bool> LoginUserAsync(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/LoginHost", ReplyAction="http://tempuri.org/ILoginService/LoginHostResponse")]
        HostServiceApplication.LoginReference.LoginData LoginHost(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/LoginHost", ReplyAction="http://tempuri.org/ILoginService/LoginHostResponse")]
        System.Threading.Tasks.Task<HostServiceApplication.LoginReference.LoginData> LoginHostAsync(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/LoginAdmin", ReplyAction="http://tempuri.org/ILoginService/LoginAdminResponse")]
        bool LoginAdmin(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/LoginAdmin", ReplyAction="http://tempuri.org/ILoginService/LoginAdminResponse")]
        System.Threading.Tasks.Task<bool> LoginAdminAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/CreateUser", ReplyAction="http://tempuri.org/ILoginService/CreateUserResponse")]
        int CreateUser(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/CreateUser", ReplyAction="http://tempuri.org/ILoginService/CreateUserResponse")]
        System.Threading.Tasks.Task<int> CreateUserAsync(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/CreateHost", ReplyAction="http://tempuri.org/ILoginService/CreateHostResponse")]
        int CreateHost(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/CreateHost", ReplyAction="http://tempuri.org/ILoginService/CreateHostResponse")]
        System.Threading.Tasks.Task<int> CreateHostAsync(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/GetUserEmail", ReplyAction="http://tempuri.org/ILoginService/GetUserEmailResponse")]
        string GetUserEmail(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/GetUserEmail", ReplyAction="http://tempuri.org/ILoginService/GetUserEmailResponse")]
        System.Threading.Tasks.Task<string> GetUserEmailAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/GetHostEmail", ReplyAction="http://tempuri.org/ILoginService/GetHostEmailResponse")]
        string GetHostEmail(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/GetHostEmail", ReplyAction="http://tempuri.org/ILoginService/GetHostEmailResponse")]
        System.Threading.Tasks.Task<string> GetHostEmailAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginServiceChannel : HostServiceApplication.LoginReference.ILoginService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginServiceClient : System.ServiceModel.ClientBase<HostServiceApplication.LoginReference.ILoginService>, HostServiceApplication.LoginReference.ILoginService {
        
        public LoginServiceClient() {
        }
        
        public LoginServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LoginServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoginServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoginServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool LoginUser(string email, string password) {
            return base.Channel.LoginUser(email, password);
        }
        
        public System.Threading.Tasks.Task<bool> LoginUserAsync(string email, string password) {
            return base.Channel.LoginUserAsync(email, password);
        }
        
        public HostServiceApplication.LoginReference.LoginData LoginHost(string email, string password) {
            return base.Channel.LoginHost(email, password);
        }
        
        public System.Threading.Tasks.Task<HostServiceApplication.LoginReference.LoginData> LoginHostAsync(string email, string password) {
            return base.Channel.LoginHostAsync(email, password);
        }
        
        public bool LoginAdmin(string username, string password) {
            return base.Channel.LoginAdmin(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> LoginAdminAsync(string username, string password) {
            return base.Channel.LoginAdminAsync(username, password);
        }
        
        public int CreateUser(string email, string password) {
            return base.Channel.CreateUser(email, password);
        }
        
        public System.Threading.Tasks.Task<int> CreateUserAsync(string email, string password) {
            return base.Channel.CreateUserAsync(email, password);
        }
        
        public int CreateHost(string email, string password) {
            return base.Channel.CreateHost(email, password);
        }
        
        public System.Threading.Tasks.Task<int> CreateHostAsync(string email, string password) {
            return base.Channel.CreateHostAsync(email, password);
        }
        
        public string GetUserEmail(int id) {
            return base.Channel.GetUserEmail(id);
        }
        
        public System.Threading.Tasks.Task<string> GetUserEmailAsync(int id) {
            return base.Channel.GetUserEmailAsync(id);
        }
        
        public string GetHostEmail(int id) {
            return base.Channel.GetHostEmail(id);
        }
        
        public System.Threading.Tasks.Task<string> GetHostEmailAsync(int id) {
            return base.Channel.GetHostEmailAsync(id);
        }
    }
}
