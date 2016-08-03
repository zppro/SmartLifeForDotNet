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

namespace SmartLife.CertManage.MerchantServices.Pub
{
    //[MerchantTokenValidate()]
    [MerchantServicesValidate(ApplicationIdFrom = "CB001", ApplicationIdTo = "IP005")]
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReminderService : BaseWcfService
    {
        #region 业务接口

        #region 获取提醒统计信息
        [WebGet(UriTemplate = "GetReminderStatGroupBySourceType/{sourceTable},{sourceColumn}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetReminderStatGroupBySourceType(string sourceTable, string sourceColumn)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                StringObjectDictionary filters = new { SourceTable = sourceTable, SourceColumn = sourceColumn, ObjectType = "Merchant", ObjectKey = stationId }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("ReminderStat_Group_By_SourceType", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion 

        #region 获取提醒详细信息
        [WebGet(UriTemplate = "GetReminderItems/{sourceTable},{sourceColumn}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetReminderItems(string sourceTable, string sourceColumn)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                StringObjectDictionary filters = new { SourceTable = sourceTable, SourceColumn = sourceColumn, ObjectType = "Merchant", ObjectKey = stationId }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("ReminderList_By_Merchant", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 响应提醒
        [WebInvoke(UriTemplate = "IgnoreReminder", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult IgnoreReminder(IgnoreReminderParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                object p = param;
                //if(param.SourceKey==null){
                p = param.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL);
                //}
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(new IBatisNetBatchStatement { StatementName = "ReponseReminder", ParameterObject = p, Type = SqlExecuteType.UPDATE });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #endregion
    }

    public class IgnoreReminderParam
    {
        public string ResponseAppType { get; set; }
        public string ObjectType { get; set; }
        public string ObjectKey { get; set; } 
        public string SourceTable { get; set; }
        public string SourceColumn { get; set; }
        public string SourceType { get; set; }
        public string SourceKey { get; set; }
    }
}