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
using SmartLife.Share.Models.Eva;

namespace SmartLife.CertManage.ManageServices.Eva
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EvaluatedBaseInfoService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<EvaluatedBaseInfo> List()
        {
            CollectionInvokeResult<EvaluatedBaseInfo> result = new CollectionInvokeResult<EvaluatedBaseInfo> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<EvaluatedBaseInfo>();
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
        public CollectionInvokeResult<EvaluatedBaseInfo> Query(string strParms)
        {
            CollectionInvokeResult<EvaluatedBaseInfo> result = new CollectionInvokeResult<EvaluatedBaseInfo> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<EvaluatedBaseInfo>(dictionary);
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
        public JqgridCollectionInvokeResult<EvaluatedBaseInfo> ListForJqgrid(JqgridCollectionParam<EvaluatedBaseInfo> param)
        {
            JqgridCollectionInvokeResult<EvaluatedBaseInfo> result = new JqgridCollectionInvokeResult<EvaluatedBaseInfo> { Success = true };
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

                gridCollectionPager.JqgridDoPage<EvaluatedBaseInfo>(BuilderFactory.DefaultBulder().List<EvaluatedBaseInfo>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的列表 ListForEasyUIgrid
        [WebInvoke(UriTemplate = "ListForEasyUIgrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<EvaluatedBaseInfo> ListForEasyUIgrid(EasyUIgridCollectionParam<EvaluatedBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<EvaluatedBaseInfo> result = new EasyUIgridCollectionInvokeResult<EvaluatedBaseInfo> { Success = true };
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
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                /***********************end 排序***************************/

                gridCollectionPager.EasyUIgridDoPage<EvaluatedBaseInfo>(BuilderFactory.DefaultBulder().List<EvaluatedBaseInfo>(filters), param, ref result);
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
        public ModelInvokeResult<EvaluatedBaseInfoPK> Create(EvaluatedBaseInfo evaluatedBaseInfo)
        {
            ModelInvokeResult<EvaluatedBaseInfoPK> result = new ModelInvokeResult<EvaluatedBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (evaluatedBaseInfo.ResidentId == GlobalManager.GuidAsAutoGenerate)
                {
                    evaluatedBaseInfo.ResidentId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                evaluatedBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                evaluatedBaseInfo.OperatedOn = DateTime.Now;
                evaluatedBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = evaluatedBaseInfo.GetCreateMethodName(), ParameterObject = evaluatedBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EvaluatedBaseInfoPK { ResidentId = evaluatedBaseInfo.ResidentId };
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
        [WebInvoke(UriTemplate = "{strResidentId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EvaluatedBaseInfoPK> Update(string strResidentId, EvaluatedBaseInfo evaluatedBaseInfo)
        {
            ModelInvokeResult<EvaluatedBaseInfoPK> result = new ModelInvokeResult<EvaluatedBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ResidentId = strResidentId.ToGuid();
                if (_ResidentId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                evaluatedBaseInfo.ResidentId = _ResidentId;
                /***********************begin 自定义代码*******************/
                evaluatedBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                evaluatedBaseInfo.OperatedOn = DateTime.Now;
                evaluatedBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = evaluatedBaseInfo.GetUpdateMethodName(), ParameterObject = evaluatedBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EvaluatedBaseInfoPK { ResidentId = _ResidentId };

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
        [WebInvoke(UriTemplate = "{strResidentId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EvaluatedBaseInfoPK> Delete(string strResidentId)
        {
            ModelInvokeResult<EvaluatedBaseInfoPK> result = new ModelInvokeResult<EvaluatedBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ResidentId = strResidentId.ToGuid();
                if (_ResidentId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                EvaluatedBaseInfoPK pk = new EvaluatedBaseInfoPK { ResidentId = _ResidentId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new EvaluatedBaseInfo().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EvaluatedBaseInfoPK { ResidentId = _ResidentId };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 删除所选 DeleteSelected
        [WebInvoke(UriTemplate = "DeleteSelected/{strResidentIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strResidentIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrResidentIds = strResidentIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrResidentIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new EvaluatedBaseInfo().GetDeleteMethodName();
                foreach (string strResidentId in arrResidentIds)
                {
                    EvaluatedBaseInfoPK pk = new EvaluatedBaseInfoPK { ResidentId = strResidentId.ToGuid() };
                    DeleteCascade(statements, pk);
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pk, Type = SqlExecuteType.DELETE });
                }
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


        #region 作废 Nullify
        [WebInvoke(UriTemplate = "Nullify/{strResidentId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EvaluatedBaseInfoPK> Nullify(string strResidentId)
        {
            ModelInvokeResult<EvaluatedBaseInfoPK> result = new ModelInvokeResult<EvaluatedBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ResidentId = strResidentId.ToGuid();
                if (_ResidentId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                EvaluatedBaseInfo evaluatedBaseInfo = new EvaluatedBaseInfo { ResidentId = _ResidentId, Status = 0 };
                /***********************begin 自定义代码*******************/
                evaluatedBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                evaluatedBaseInfo.OperatedOn = DateTime.Now;
                evaluatedBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = evaluatedBaseInfo.GetUpdateMethodName(), ParameterObject = evaluatedBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EvaluatedBaseInfoPK { ResidentId = _ResidentId };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 作废所选 NullifySelected
        [WebInvoke(UriTemplate = "NullifySelected/{strResidentIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strResidentIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrResidentIds = strResidentIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrResidentIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new EvaluatedBaseInfo().GetUpdateMethodName();
                foreach (string strResidentId in arrResidentIds)
                {
                    EvaluatedBaseInfo evaluatedBaseInfo = new EvaluatedBaseInfo { ResidentId = strResidentId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    evaluatedBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                    evaluatedBaseInfo.OperatedOn = DateTime.Now;
                    evaluatedBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = evaluatedBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
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

        #region 级联删除扩展接口 DeleteCascade
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, EvaluatedBaseInfoPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strResidentId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<EvaluatedBaseInfo> Load(string strResidentId)
        {
            ModelInvokeResult<EvaluatedBaseInfo> result = new ModelInvokeResult<EvaluatedBaseInfo> { Success = true };
            try
            {
                Guid? _ResidentId = strResidentId.ToGuid();
                if (_ResidentId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var evaluatedBaseInfo = BuilderFactory.DefaultBulder().Load<EvaluatedBaseInfo, EvaluatedBaseInfoPK>(new EvaluatedBaseInfoPK { ResidentId = _ResidentId });
                result.instance = evaluatedBaseInfo;
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

        #region 自定义业务接口

        #region EasyUIgrid数据格式的分页列表 ListForEasyUIgridPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<EvaluatedBaseInfo> ListForEasyUIgridPage(EasyUIgridCollectionParam<EvaluatedBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<EvaluatedBaseInfo> result = new EasyUIgridCollectionInvokeResult<EvaluatedBaseInfo> { Success = true };
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
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                    }
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP) ? " AND " : " " + param.fuzzyFieldOP + " "), fuzzys.ToArray()) + ")");
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

                gridCollectionPager.EasyUIgridDoPage4Model<EvaluatedBaseInfo>(filters, param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 更新评估对象
        [WebInvoke(UriTemplate = "SynBaseInfo/{Age},{AreaId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SynBaseInfo(string Age,string Status,string AreaId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                //var spParam = param.ToSPParam();
                var spParam = new EvaluatedBaseInfo().ToSPParam();
                spParam["SourceData"] = "SmartLife-1203";
                spParam["Age"] = Age;
                spParam["AreaId"] = AreaId;
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_ImportEvaluatedBaseInfo", spParam);
                if (spParam.ErrorCode != 0 && spParam.ErrorCode != 51000)
                {
                    result.ErrorCode = spParam.ErrorCode;
                    result.ErrorMessage = spParam.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的分页列表 ListForEasyUIgridPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridPage2(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                //if (param.instance != null)
                //{
                //    filters = param.instance.ToStringObjectDictionary(false);
                //}
                List<string> whereClause = new List<string>();
                /**********************************************************/
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        if (field.key.Contains("Gender_Start"))
                        {
                            if (field.value != "N")
                            {
                                filters["Gender"] = field.value;
                            }
                        }
                        else
                        {
                            filters[field.key] = field.value;
                        }
                    }
                }
                /***********************begin 模糊查询*********************/
                if (param.fuzzyFields != null)
                {
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        if (field.key.IndexOf("AreaCode") > -1 && field.value != "")
                        {
                            whereClause.Add(string.Format("(AreaId3 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", field.key, field.value));
                        }
                        else if (field.key.IndexOf("AreaCode") > -1 && field.value == "")
                        {

                        }
                        else
                        {
                            fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
                    } 
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP) ? " AND " : " " + param.fuzzyFieldOP + " "), fuzzys.ToArray()) + ")");
                    }
                    if (whereClause.Count > 0)
                    {
                        filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                    }
                }

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("EvaluatedBaseInfoAndDictionaryItem_ListByPage", filters, param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region EasyUIgrid数据格式的分页列表(视图) ListForEasyUIgridPage_V
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage_V", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<EvaluatedBaseInfo_V> ListForEasyUIgridPage_V(EasyUIgridCollectionParam<EvaluatedBaseInfo_V> param)
        {
            EasyUIgridCollectionInvokeResult<EvaluatedBaseInfo_V> result = new EasyUIgridCollectionInvokeResult<EvaluatedBaseInfo_V> { Success = true };
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
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                    }
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP) ? " AND " : " " + param.fuzzyFieldOP + " "), fuzzys.ToArray()) + ")");
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

                gridCollectionPager.EasyUIgridDoPage4Model<EvaluatedBaseInfo_V>(filters, param, ref result, GetHttpHeader("ConnectId"));

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
}