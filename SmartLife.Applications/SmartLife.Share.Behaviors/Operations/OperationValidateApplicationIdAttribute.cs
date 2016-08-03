using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Web;
using e0571.web.core.Wcf;
using e0571.web.core.Utils;
using e0571.web.core.Model;
using SmartLife.Share.Models.Sys;

namespace SmartLife.Share.Behaviors.Operations
{
    public class OperationValidateApplicationIdAttribute : OperationValidateAttribute
    {
        protected override object Validate()
        {
            bool validated = false;

            string applicationId = WebOperationContext.Current.IncomingRequest.Headers[GlobalManager.ApplicationIdKey];
            if (applicationId != null && applicationId.Length == 5)
            {
                var application = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).Load<Application, ApplicationPK>(new ApplicationPK { ApplicationId = applicationId });
                if (application.ApplicationId != null)
                {
                    validated = true;
                }
            }

            if (!validated)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.Forbidden);
            }

            return null;
        }
    }
}
