using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;

using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;

using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub;

namespace SmartLife.Share.Behaviors
{
    [ServiceErrorOutput]
    [ServiceLogging]
    //[ServiceAuthorization(Salt = "[leblue]", ValidateKey = "smartlife")]
    [ServiceValidateInitilize]
    public class AppBaseWcfService : BaseWcfService
    {

    }
}
