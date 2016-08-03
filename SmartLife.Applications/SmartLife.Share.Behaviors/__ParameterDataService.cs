using System;
using System.Collections.Generic;
using System.Linq;
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

using SmartLife.Share.Behaviors;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub; 

namespace SmartLife.Share.Behaviors
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class __ParameterDataService : AppBaseWcfService
    {
        #region 获取参数
        [WebGet(UriTemplate = "{parameterId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<StringValueRet> GetParameterValue(string parameterId)
        {
            
            InvokeResult<StringValueRet> result = new InvokeResult<StringValueRet>() { Success = true, ret = new StringValueRet() };
            result.ret.Value = TypeConverter.ChangeString(CacheManager.ParmeterCacheProvider.Get(parameterId));
            return result;
        }
        #endregion
    } 
}
