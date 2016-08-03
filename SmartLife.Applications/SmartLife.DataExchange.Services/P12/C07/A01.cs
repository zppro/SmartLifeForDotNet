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
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Models.Cer;
using e0571.web.core.Security;
using SmartLife.Share.Behaviors;
using SmartLife.Share.Models.Inc;

namespace SmartLife.DataExchange.Services.P12.C07
{
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class A01 : BaseWcfService
    {
        [WebGet(UriTemplate = "GetSyncOutWorkOrders", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<SyncOut_ServiceWorkOrder_120701> GetSyncOutWorkOrders()
        {
            CollectionInvokeResult<SyncOut_ServiceWorkOrder_120701> result = new CollectionInvokeResult<SyncOut_ServiceWorkOrder_120701> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().ListByPage<SyncOut_ServiceWorkOrder_120701>(new { SyncFlag = 0 }.ToStringObjectDictionary(), new ListPager { OrderByClause = "CheckInTime asc", PageNo = 1, PageSize = 10 });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebInvoke(UriTemplate = "MarkSyncOutWorkOrder", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult MarkSyncOutWorkOrder(MarkSyncOutWorkOrderParam param)
        {
            InvokeResult result = new InvokeResult() { Success = true };
            try
            {
                SyncOut_ServiceWorkOrder_120701 o = new SyncOut_ServiceWorkOrder_120701 { Id = param.Id, SyncFlag = 1, SyncOn = DateTime.Now };
                BuilderFactory.DefaultBulder().Update<SyncOut_ServiceWorkOrder_120701>(o);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

    }

    public class MarkSyncOutWorkOrderParam {
        public int Id { get; set; }
    }
}