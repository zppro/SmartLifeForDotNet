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
using SmartLife.Share.Models.Pub;

namespace SmartLife.CertManage.SmsServices.Pub
{

    public class ReminderInfo {
        public string CallNo { get; set; }
        public string LastTime { get; set; }
        public string SourceType { get; set; }
        public string RemindContent { get; set; }
    }

    [SmsTokenValidate]
    [SmsServicesValidate(ApplicationIdFrom = "CS001", ApplicationIdTo = "IP006")]
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReminderService : BaseWcfService
    {
        #region ClientSms标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Reminder> List()
        {
            CollectionInvokeResult<Reminder> result = new CollectionInvokeResult<Reminder> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<Reminder>();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 查询列表 Query
        [WebGet(UriTemplate = "Query?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Reminder> Query(string strParms)
        {
            CollectionInvokeResult<Reminder> result = new CollectionInvokeResult<Reminder> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<Reminder>(dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region Jqgrid数据格式的列表 ListForJqgrid
        [WebInvoke(UriTemplate = "ListForJqgrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public JqgridCollectionInvokeResult<Reminder> ListForJqgrid(JqgridCollectionParam<Reminder> param)
        {
            JqgridCollectionInvokeResult<Reminder> result = new JqgridCollectionInvokeResult<Reminder> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }
                List<string> whereClause = new List<string>();
                /**********************************************************/
                /***********************begin 模糊查询*********************/
                if (param.fuzzyFields != null)
                {
                    foreach (var field in param.fuzzyFields)
                    {
                        whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                    }
                }
                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/
                /**********************************************************/
                if (!string.IsNullOrEmpty(param.sidx))
                {
                    filters.Add("OrderByClause", param.sidx + " " + param.sord ?? "ASC");
                }
                /***********************end 排序***************************/

                gridCollectionPager.JqgridDoPage<Reminder>(BuilderFactory.DefaultBulder().List<Reminder>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 创建 Create
        [WebInvoke(UriTemplate = "", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ReminderPK> Create(Reminder reminder)
        {
            ModelInvokeResult<ReminderPK> result = new ModelInvokeResult<ReminderPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (reminder.Id == -1)
                {
                    reminder.Id = null;
                }
                statements.Add(new IBatisNetBatchStatement { StatementName = reminder.GetCreateMethodName(), ParameterObject = reminder.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ReminderPK { Id = reminder.Id };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 修改 Update
        [WebInvoke(UriTemplate = "{strId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ReminderPK> Update(string strId, Reminder reminder)
        {
            ModelInvokeResult<ReminderPK> result = new ModelInvokeResult<ReminderPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                statements.Add(new IBatisNetBatchStatement { StatementName = reminder.GetUpdateMethodName(), ParameterObject = reminder.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ReminderPK { Id = _Id };

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 删除 Delete
        [WebInvoke(UriTemplate = "{strId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ReminderPK> Delete(string strId)
        {
            ModelInvokeResult<ReminderPK> result = new ModelInvokeResult<ReminderPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                ReminderPK pk = new ReminderPK { Id = _Id };
                statements.Add(new IBatisNetBatchStatement { StatementName = new Reminder().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ReminderPK { Id = _Id };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Reminder> Load(string strId)
        {
            ModelInvokeResult<Reminder> result = new ModelInvokeResult<Reminder> { Success = true };
            try
            {
                var reminder = BuilderFactory.DefaultBulder().Load<Reminder, ReminderPK>(new ReminderPK { Id = Convert.ToInt32(strId) });
                result.instance = reminder;
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

        #region 扩展接口
        #region 新增 CreateReminderByCall
        [WebInvoke(UriTemplate = "CreateReminderByCall", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CreateReminderByCall(ReminderInfo reminderInfo)
        {
            InvokeResult result = new InvokeResult { Success = true };
            CollectionInvokeResult<StringObjectDictionary> result2 = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            reminderInfo.CallNo = base.GetHttpHeader("MobileNo");
            try
            {
                StringObjectDictionary filters = new { RemindTime = reminderInfo.LastTime, SourceType = reminderInfo.SourceType, CallNo = reminderInfo.CallNo, OrderByClause = " RemindTime desc" }.ToStringObjectDictionary();
                result2.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("Reminder_By_CallNo_List", filters);

                Reminder reminder = new Reminder();
                foreach (var item in result2.rows)
                {
                    reminder.Id = item["Id"] == null ? 0 : Int32.Parse(item["Id"].ToString());
                    reminder.SourceKey = item["OldManId"].ToString();
                }
                reminder.SourceTable = "Oca_OldManConfigInfo";
                reminder.SourceColumn = "OldManId";
                reminder.SourceType = reminderInfo.SourceType;
                reminder.RemindTime = Convert.ToDateTime(reminderInfo.LastTime);
                reminder.RemindContent = reminderInfo.RemindContent;

                if (reminder.Id > 0)
                {
                    result.Success = false;
                }
                else
                {
                    reminder.Id = null;
                    List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                    statements.Add(new IBatisNetBatchStatement { StatementName = reminder.GetCreateMethodName(), ParameterObject = reminder.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
        #endregion
        #endregion
    }
}