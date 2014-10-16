﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IMessengerService")]
public interface IMessengerService
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerService/SendMessage", ReplyAction = "http://tempuri.org/IMessengerService/SendMessageResponse")]
    string SendMessage(string name);

    [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IMessengerService/SendMessage", ReplyAction = "http://tempuri.org/IMessengerService/SendMessageResponse")]
    System.IAsyncResult BeginSendMessage(string name, System.AsyncCallback callback, object asyncState);

    string EndSendMessage(System.IAsyncResult result);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IMessengerServiceChannel : IMessengerService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class MessengerServiceClient : System.ServiceModel.ClientBase<IMessengerService>, IMessengerService
{

    public MessengerServiceClient()
    {
    }

    public MessengerServiceClient(string endpointConfigurationName) :
        base(endpointConfigurationName)
    {
    }

    public MessengerServiceClient(string endpointConfigurationName, string remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    {
    }

    public MessengerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    {
    }

    public MessengerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
        base(binding, remoteAddress)
    {
    }

    public string SendMessage(string name)
    {
        return base.Channel.SendMessage(name);
    }

    public System.IAsyncResult BeginSendMessage(string name, System.AsyncCallback callback, object asyncState)
    {
        return base.Channel.BeginSendMessage(name, callback, asyncState);
    }

    public string EndSendMessage(System.IAsyncResult result)
    {
        return base.Channel.EndSendMessage(result);
    }
}