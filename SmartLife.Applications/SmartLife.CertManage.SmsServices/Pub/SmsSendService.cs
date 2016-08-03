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
    [SmsTokenOfNodeValidate()]
    [SmsServicesValidate(ApplicationIdFrom = "CS001", ApplicationIdTo = "IP006")]
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SmsSendService : BaseWcfService
    {
        #region ClientSms标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<SmsSend> List()
        {
            CollectionInvokeResult<SmsSend> result = new CollectionInvokeResult<SmsSend> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<SmsSend>();
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
        public CollectionInvokeResult<SmsSend> Query(string strParms)
        {
            CollectionInvokeResult<SmsSend> result = new CollectionInvokeResult<SmsSend> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<SmsSend>(dictionary);
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
        public JqgridCollectionInvokeResult<SmsSend> ListForJqgrid(JqgridCollectionParam<SmsSend> param)
        {
            JqgridCollectionInvokeResult<SmsSend> result = new JqgridCollectionInvokeResult<SmsSend> { Success = true };
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

                gridCollectionPager.JqgridDoPage<SmsSend>(BuilderFactory.DefaultBulder().List<SmsSend>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        //#region EasyUIgrid数据格式的列表 ListForEasyUIgrid
        //[WebInvoke(UriTemplate = "ListForEasyUIgrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgrid(EasyUIgridCollectionParam<StringObjectDictionary> param)
        //{
        //    EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
        //    try
        //    {
        //        /***********************begin 复杂查询添加代码*********************/
        //        StringObjectDictionary filters = new StringObjectDictionary();
        //        if (param.instance != null)
        //        {
        //            filters = param.instance.ToStringObjectDictionary(false);
        //        }
        //        List<string> whereClause = new List<string>();
        //        /**********************************************************/
        //        if (param.filterFields != null)
        //        {
        //            foreach (var field in param.filterFields)
        //            {
        //                filters[field.key] = field.value;
        //            }
        //        }

        //        /***********************begin 模糊查询*********************/
        //        if (param.fuzzyFields != null)
        //        {
        //            foreach (var field in param.fuzzyFields)
        //            {
        //                whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
        //            }
        //        }
        //        /***********************end 模糊查询***********************/
        //        /***********************begin 自定义代码*******************/
        //        /***********************此处添加自定义查询代码*************/
        //        /***********************end 自定义代码*********************/
        //        if (whereClause.Count > 0)
        //        {
        //            filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
        //        }
        //        /**********************************************************/
        //        /***********************begin 排序*************************/
        //        /**********************************************************/
        //        if (!string.IsNullOrEmpty(param.sort))
        //        {
        //            filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
        //        }
        //        /***********************end 排序***************************/

        //        gridCollectionPager.EasyUIgridDoPage<StringObjectDictionary>(BuilderFactory.DefaultBulder().ListStringObjectDictionary("OldManFamilyInfo_By_OldMan_List", filters), param, ref result);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.ErrorMessage = ex.Message;
        //    }
        //    return result;
        //}
        //#endregion

        #region 创建 Create
        [WebInvoke(UriTemplate = "", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SmsSendPK> Create(SmsSend smsSend)
        {
            ModelInvokeResult<SmsSendPK> result = new ModelInvokeResult<SmsSendPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (smsSend.Id == -1)
                {
                    smsSend.Id = null;
                }
                statements.Add(new IBatisNetBatchStatement { StatementName = smsSend.GetCreateMethodName(), ParameterObject = smsSend.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SmsSendPK { Id = smsSend.Id };
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
        public ModelInvokeResult<SmsSendPK> Update(string strId, SmsSend smsSend)
        {
            ModelInvokeResult<SmsSendPK> result = new ModelInvokeResult<SmsSendPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                statements.Add(new IBatisNetBatchStatement { StatementName = smsSend.GetUpdateMethodName(), ParameterObject = smsSend.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SmsSendPK { Id = _Id };

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
        public ModelInvokeResult<SmsSendPK> Delete(string strId)
        {
            ModelInvokeResult<SmsSendPK> result = new ModelInvokeResult<SmsSendPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                SmsSendPK pk = new SmsSendPK { Id = _Id };
                statements.Add(new IBatisNetBatchStatement { StatementName = new SmsSend().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SmsSendPK { Id = _Id };
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
        public ModelInvokeResult<SmsSend> Load(string strId)
        {
            ModelInvokeResult<SmsSend> result = new ModelInvokeResult<SmsSend> { Success = true };
            try
            {
                var smsSendInfo = BuilderFactory.DefaultBulder().Load<SmsSend, SmsSendPK>(new SmsSendPK { Id = Convert.ToInt32(strId) });
                result.instance = smsSendInfo;
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
        #region 修改 Update
        [WebInvoke(UriTemplate = "Update2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult Update2(SmsSend smsSend)
        {
            InvokeResult result = new InvokeResult { Success = true };
            smsSend.SendTime = DateTime.Now;
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                statements.Add(new IBatisNetBatchStatement { StatementName = "SmsSend_Update2", ParameterObject = smsSend.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 新增 QueuedSendSms
        [WebInvoke(UriTemplate = "QueuedSendSms", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> QueuedSendSms(SmsSend smsSend)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { Status = smsSend.Status, OrderByClause = " BatchNum,CheckInTime  desc" }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("SmsSend_List2", filters);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
        #endregion

        #region 新增 QueuedUnSendSms
        [WebGet(UriTemplate = "QueuedUnSendSms", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> QueuedUnSendSms()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("UnSendSms_List");
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