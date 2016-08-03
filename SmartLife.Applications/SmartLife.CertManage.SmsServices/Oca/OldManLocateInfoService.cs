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
using SmartLife.Share.Models.Oca;

namespace SmartLife.CertManage.SmsServices.Oca
{
    public class LocateInfo {
        public string CallNo { get; set; }
        public string LocateTime { get; set; }
        public string LatitudeS { get; set; }
        public string LongitudeS { get; set; }
    }

    [SmsTokenValidate]
    [SmsServicesValidate(ApplicationIdFrom="CS001", ApplicationIdTo="IP006")]
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class OldManLocateInfoService : BaseWcfService
    {
        #region ClientSms标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<OldManLocateInfo> List()
        {
            CollectionInvokeResult<OldManLocateInfo> result = new CollectionInvokeResult<OldManLocateInfo> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<OldManLocateInfo>();
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
        public CollectionInvokeResult<OldManLocateInfo> Query(string strParms)
        {
            CollectionInvokeResult<OldManLocateInfo> result = new CollectionInvokeResult<OldManLocateInfo> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<OldManLocateInfo>(dictionary);
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
        public JqgridCollectionInvokeResult<OldManLocateInfo> ListForJqgrid(JqgridCollectionParam<OldManLocateInfo> param)
        {
            JqgridCollectionInvokeResult<OldManLocateInfo> result = new JqgridCollectionInvokeResult<OldManLocateInfo> { Success = true };
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

                gridCollectionPager.JqgridDoPage<OldManLocateInfo>(BuilderFactory.DefaultBulder().List<OldManLocateInfo>(filters), param, ref result);
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
        public ModelInvokeResult<OldManLocateInfoPK> Create(OldManLocateInfo oldManLocateInfo)
        {
            ModelInvokeResult<OldManLocateInfoPK> result = new ModelInvokeResult<OldManLocateInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (oldManLocateInfo.Id == -1)
                {
                    oldManLocateInfo.Id = null;
                }
                statements.Add(new IBatisNetBatchStatement { StatementName = oldManLocateInfo.GetCreateMethodName(), ParameterObject = oldManLocateInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManLocateInfoPK { Id = oldManLocateInfo.Id };
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
        public ModelInvokeResult<OldManLocateInfoPK> Update(string strId, OldManLocateInfo oldManLocateInfo)
        {
            ModelInvokeResult<OldManLocateInfoPK> result = new ModelInvokeResult<OldManLocateInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                statements.Add(new IBatisNetBatchStatement { StatementName = oldManLocateInfo.GetUpdateMethodName(), ParameterObject = oldManLocateInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManLocateInfoPK { Id = _Id };

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
        public ModelInvokeResult<OldManLocateInfoPK> Delete(string strId)
        {
            ModelInvokeResult<OldManLocateInfoPK> result = new ModelInvokeResult<OldManLocateInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                OldManLocateInfoPK pk = new OldManLocateInfoPK { Id = _Id };
                statements.Add(new IBatisNetBatchStatement { StatementName = new OldManLocateInfo().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManLocateInfoPK { Id = _Id };
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
        public ModelInvokeResult<OldManLocateInfo> Load(string strId)
        {
            ModelInvokeResult<OldManLocateInfo> result = new ModelInvokeResult<OldManLocateInfo> { Success = true };
            try
            {
                var oldManLocateInfo = BuilderFactory.DefaultBulder().Load<OldManLocateInfo, OldManLocateInfoPK>(new OldManLocateInfoPK { Id = Convert.ToInt32(strId) });
                result.instance = oldManLocateInfo;
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

        #region 新增 CreateLocateByCall
        [WebInvoke(UriTemplate = "CreateLocateByCall", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CreateLocateByCall(LocateInfo locateInfo)
        {
            InvokeResult result = new InvokeResult { Success = true };
            CollectionInvokeResult<StringObjectDictionary> result2 = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            locateInfo.CallNo = base.GetHttpHeader("MobileNo");
            try
            {
                StringObjectDictionary filters = new { LocateTime = locateInfo.LocateTime, CallNo = locateInfo.CallNo, OrderByClause = " LocateTime desc" }.ToStringObjectDictionary(false);
                result2.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("OldManLocateInfo_By_CallNo_List", filters);

                OldManLocateInfo oldManLocateInfo = new OldManLocateInfo();
                foreach (var item in result2.rows)
                {
                    oldManLocateInfo.OldManId = Guid.Parse(item["OlderId"].ToString());
                    oldManLocateInfo.HomeLatitudeS = TypeConverter.ChangeString(item["LatitudeS"]);
                    oldManLocateInfo.HomeLongitudeS = TypeConverter.ChangeString(item["LongitudeS"]);
                    oldManLocateInfo.Id = item["Id"] == null ? 0 : Int32.Parse(item["Id"].ToString());
                }
                oldManLocateInfo.LocateTime = DateTime.Parse(locateInfo.LocateTime);
                oldManLocateInfo.LocateLatitudeS = locateInfo.LatitudeS;
                oldManLocateInfo.LocateLongitudeS = locateInfo.LongitudeS;

                if (oldManLocateInfo.Id > 0)
                {
                    result.Success = false;
                }
                else
                {
                    oldManLocateInfo.Id = null;
                    List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                    statements.Add(new IBatisNetBatchStatement { StatementName = oldManLocateInfo.GetCreateMethodName(), ParameterObject = oldManLocateInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                }
            }
            catch(Exception e) {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
        #endregion

        #endregion
    }
}