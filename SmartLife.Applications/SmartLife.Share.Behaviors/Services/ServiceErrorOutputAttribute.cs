using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher; 
using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess; 

namespace SmartLife.Share.Behaviors.Services
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceErrorOutputAttribute : Attribute, IServiceBehavior, IParameterInspector
    {

        #region IServiceBehavior 成员

        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                if (channelDispatcher == null) { continue; }
                foreach (var endPoint in channelDispatcher.Endpoints)
                {
                    if (endPoint == null) { continue; }
                    foreach (var opertaion in endPoint.DispatchRuntime.Operations)
                    {
                        opertaion.ParameterInspectors.Add(this);
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        #endregion

        #region IParameterInspector 成员
        public object BeforeCall(string operationName, object[] inputs)
        {
            return null;
        }
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            InvokeResult result = returnValue as InvokeResult;
            if (result != null)
            {
                if (result.ErrorCode != 0)
                {
                    if (GlobalManager.errorCodeMessages.ContainsKey(result.ErrorCode.ToString()))
                    {
                        result.ErrorMessage = GlobalManager.errorCodeMessages[result.ErrorCode.ToString()];
                    }
                    else
                    {
                        result.ErrorMessage = result.Error;
                    }
                    result.Success = false;

                }
                result.Error = null;
            }
        }

        #endregion
    }
}
