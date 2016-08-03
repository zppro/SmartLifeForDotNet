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
    public class OperationValidateSessionAttribute : OperationValidateAttribute
    {
        protected override object Validate()
        {
            bool validated = NormalSession.Count > 0;
            if (!validated)
            {
                throw new WebFaultException<string>("Session needed", System.Net.HttpStatusCode.Conflict);
            }

            return null;
        }
    }
}
