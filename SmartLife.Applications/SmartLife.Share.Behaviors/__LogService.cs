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
using SmartLife.Share.Models.Log;

namespace SmartLife.Share.Behaviors
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class __LogService : AppBaseWcfService
    {
        #region 获取设备报告
        [WebGet(UriTemplate = "GetDeviceReport/{serverId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<GetDeviceReportRet> GetDeviceReport(string serverId)
        {
            InvokeResult<GetDeviceReportRet> result = new InvokeResult<GetDeviceReportRet> { Success = true, ret = new GetDeviceReportRet() { ServerId = serverId } };

            try
            {
                result.ret.rows = BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).ListStringObjectDictionary("DeviceReport_ListByMonitor", new { Monitor = serverId }.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                //result.ErrorCode = 59997;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion
    }

    public class GetDeviceReportRet
    {
        public string ServerId { get; set; }
        public IList<StringObjectDictionary> rows { get; set; }
    }
}
