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
using SmartLife.Share.Models.Bas;
using SmartLife.Share.Models.Eva;
using SmartLife.Share.Models.Pub;

namespace SmartLife.CertManage.ManageServices.Eva
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EvaluatedRequisitionService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<EvaluatedRequisition_V> List()
        {
            CollectionInvokeResult<EvaluatedRequisition_V> result = new CollectionInvokeResult<EvaluatedRequisition_V> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<EvaluatedRequisition_V>();
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
        public CollectionInvokeResult<EvaluatedRequisition_V> Query(string strParms)
        {
            CollectionInvokeResult<EvaluatedRequisition_V> result = new CollectionInvokeResult<EvaluatedRequisition_V> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<EvaluatedRequisition_V>(dictionary);
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
        public JqgridCollectionInvokeResult<EvaluatedRequisition_V> ListForJqgrid(JqgridCollectionParam<EvaluatedRequisition_V> param)
        {
            JqgridCollectionInvokeResult<EvaluatedRequisition_V> result = new JqgridCollectionInvokeResult<EvaluatedRequisition_V> { Success = true };
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

                gridCollectionPager.JqgridDoPage<EvaluatedRequisition_V>(BuilderFactory.DefaultBulder().List<EvaluatedRequisition_V>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<EvaluatedRequisition_V> ListForEasyUIgrid(EasyUIgridCollectionParam<EvaluatedRequisition_V> param)
        {
            EasyUIgridCollectionInvokeResult<EvaluatedRequisition_V> result = new EasyUIgridCollectionInvokeResult<EvaluatedRequisition_V> { Success = true };
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
                /***********************begin 排序*************************/
                /**********************************************************/
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                /***********************end 排序***************************/

                gridCollectionPager.EasyUIgridDoPage<EvaluatedRequisition_V>(BuilderFactory.DefaultBulder().List<EvaluatedRequisition_V>(filters), param, ref result);
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
        public ModelInvokeResult<EvaluatedRequisitionPK> Create(EvaluatedRequisition evaluatedRequisition)
        {
            ModelInvokeResult<EvaluatedRequisitionPK> result = new ModelInvokeResult<EvaluatedRequisitionPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (evaluatedRequisition.RequisitionId == GlobalManager.GuidAsAutoGenerate)
                {
                    evaluatedRequisition.RequisitionId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                evaluatedRequisition.OperatedBy = NormalSession.UserId.ToGuid();
                evaluatedRequisition.OperatedOn = DateTime.Now;
                evaluatedRequisition.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = evaluatedRequisition.GetCreateMethodName(), ParameterObject = evaluatedRequisition.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EvaluatedRequisitionPK { RequisitionId = evaluatedRequisition.RequisitionId };
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
        [WebInvoke(UriTemplate = "{strRequisitionId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EvaluatedRequisitionPK> Update(string strRequisitionId, EvaluatedRequisition evaluatedRequisition)
        {
            ModelInvokeResult<EvaluatedRequisitionPK> result = new ModelInvokeResult<EvaluatedRequisitionPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _RequisitionId = strRequisitionId.ToGuid();
                if (_RequisitionId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                evaluatedRequisition.RequisitionId = _RequisitionId;
                /***********************begin 自定义代码*******************/
                evaluatedRequisition.OperatedBy = NormalSession.UserId.ToGuid();
                evaluatedRequisition.OperatedOn = DateTime.Now;
                evaluatedRequisition.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = evaluatedRequisition.GetUpdateMethodName(), ParameterObject = evaluatedRequisition.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EvaluatedRequisitionPK { RequisitionId = _RequisitionId };

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
        [WebInvoke(UriTemplate = "{strRequisitionId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EvaluatedRequisitionPK> Delete(string strRequisitionId)
        {
            ModelInvokeResult<EvaluatedRequisitionPK> result = new ModelInvokeResult<EvaluatedRequisitionPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _RequisitionId = strRequisitionId.ToGuid();
                if (_RequisitionId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                EvaluatedRequisitionPK pk = new EvaluatedRequisitionPK { RequisitionId = _RequisitionId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new EvaluatedRequisition().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EvaluatedRequisitionPK { RequisitionId = _RequisitionId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strRequisitionIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strRequisitionIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrRequisitionIds = strRequisitionIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrRequisitionIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new EvaluatedRequisition().GetDeleteMethodName();
                foreach (string strRequisitionId in arrRequisitionIds)
                {
                    EvaluatedRequisitionPK pk = new EvaluatedRequisitionPK { RequisitionId = strRequisitionId.ToGuid() };
                    DeleteCascade(statements, pk);
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pk, Type = SqlExecuteType.DELETE });
                }
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
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
        [WebInvoke(UriTemplate = "Nullify/{strRequisitionId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EvaluatedRequisitionPK> Nullify(string strRequisitionId)
        {
            ModelInvokeResult<EvaluatedRequisitionPK> result = new ModelInvokeResult<EvaluatedRequisitionPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _RequisitionId = strRequisitionId.ToGuid();
                if (_RequisitionId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                EvaluatedRequisition evaluatedRequisition = new EvaluatedRequisition { RequisitionId = _RequisitionId, Status = 0 };
                /***********************begin 自定义代码*******************/
                evaluatedRequisition.OperatedBy = NormalSession.UserId.ToGuid();
                evaluatedRequisition.OperatedOn = DateTime.Now;
                evaluatedRequisition.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = evaluatedRequisition.GetUpdateMethodName(), ParameterObject = evaluatedRequisition.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EvaluatedRequisitionPK { RequisitionId = _RequisitionId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strRequisitionIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strRequisitionIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrRequisitionIds = strRequisitionIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrRequisitionIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new EvaluatedRequisition().GetUpdateMethodName();
                foreach (string strRequisitionId in arrRequisitionIds)
                {
                    EvaluatedRequisition evaluatedRequisition = new EvaluatedRequisition { RequisitionId = strRequisitionId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    evaluatedRequisition.OperatedBy = NormalSession.UserId.ToGuid();
                    evaluatedRequisition.OperatedOn = DateTime.Now;
                    evaluatedRequisition.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = evaluatedRequisition.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, EvaluatedRequisitionPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strRequisitionId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<EvaluatedRequisition> Load(string strRequisitionId)
        {
            ModelInvokeResult<EvaluatedRequisition> result = new ModelInvokeResult<EvaluatedRequisition> { Success = true };
            try
            {
                Guid? _RequisitionId = strRequisitionId.ToGuid();
                if (_RequisitionId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var evaluatedRequisition = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<EvaluatedRequisition, EvaluatedRequisitionPK>(new EvaluatedRequisitionPK { RequisitionId = _RequisitionId });
                result.instance = evaluatedRequisition;
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

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<EvaluatedRequisition_V> ListForEasyUIgridPage(EasyUIgridCollectionParam<EvaluatedRequisition_V> param)
        {
            EasyUIgridCollectionInvokeResult<EvaluatedRequisition_V> result = new EasyUIgridCollectionInvokeResult<EvaluatedRequisition_V> { Success = true };
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
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        DateTime parseTime = new DateTime();
                        if (field.key.Equals("OperatedOn_Start") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format("OperatedOn >= '{0}' ", field.value));
                        }
                        else if (field.key.Equals("OperatedOn_End") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format("OperatedOn <= '{0}' ", field.value));
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
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4Model<EvaluatedRequisition_V>(filters, param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 评估申请信息 数据格式的列表 ListForEasyUIgridPage
        [WebInvoke(UriTemplate = "EvaluatedRequisitionInfo/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<EvaluatedRequisition_V> EvaluatedRequisitionInfo(string ConnectId, EasyUIgridCollectionParam<EvaluatedRequisition_V> param)
        {
            EasyUIgridCollectionInvokeResult<EvaluatedRequisition_V> result = new EasyUIgridCollectionInvokeResult<EvaluatedRequisition_V> { Success = true };
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
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        DateTime parseTime = new DateTime();
                        if (field.key.Equals("OperatedOn_Start") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format("OperatedOn >= '{0}' ", field.value));
                        }
                        else if (field.key.Equals("OperatedOn_End") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format("OperatedOn <= '{0}' ", field.value));
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

                        fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                    }
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP) ? " AND " : " " + param.fuzzyFieldOP + " "), fuzzys.ToArray()) + ")");
                    }
                }
                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                string sql = PermissionsCategoryView();
                if (sql == "user")
                {
                    whereClause.Add("'0'='1'");
                }
                else if (sql != "admin")
                {
                    whereClause.Add(sql);
                }
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4Model<EvaluatedRequisition_V>(filters, param, ref result, ConnectId);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 权限查看不同内容
        private string PermissionsCategoryView()
        {
            User currentUser = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };
            string sql = "user";
            if (!currentUser.isSuperAdmin())
            {
                IList<UserArea> userAreas = BuilderFactory.DefaultBulder().List<UserArea>(new UserArea { UserId = NormalSession.UserId.ToGuid() });
                if (userAreas.Count > 0)
                {
                    var areaIdsOfStreet = string.Join("','", userAreas.Where(item => item.AreaId.ToString().Substring(14, 4) == "0000").Select(item => item.AreaId.ToString()));
                    var areaIdsOfCommunity = string.Join("','", userAreas.Where(item => item.AreaId.ToString().Substring(14, 4) != "0000").Select(item => item.AreaId.ToString()));

                    if (areaIdsOfStreet == "")
                    {
                        sql = string.Format("( AreaId3 in ('{0}') or  (ISNULL(AreaId2,'')='' AND ISNULL(AreaId3,'')=''))", areaIdsOfCommunity);
                    }
                    else if (areaIdsOfCommunity == "")
                    {
                        sql = string.Format("(AreaId2 in ('{0}') or  (ISNULL(AreaId2,'')='' AND ISNULL(AreaId3,'')=''))", areaIdsOfStreet);
                    }
                    else
                    {
                        sql = string.Format("(AreaId2 in ('{0}') or AreaId3 in ('{1}') or (ISNULL(AreaId2,'')='' AND ISNULL(AreaId3,'')=''))", areaIdsOfStreet, areaIdsOfCommunity);
                    }
                }
            }
            else
            {
                sql = "admin";
            }
            return sql;
        }
        #endregion

        #region 评估申请信息 数据格式的列表 ListForEasyUIgridPage
        [WebInvoke(UriTemplate = "EvaluatedRequisitionInfo2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<EvaluatedRequisition_V> EvaluatedRequisitionInfo2( EasyUIgridCollectionParam<EvaluatedRequisition_V> param)
        {
            EasyUIgridCollectionInvokeResult<EvaluatedRequisition_V> result = new EasyUIgridCollectionInvokeResult<EvaluatedRequisition_V> { Success = true };
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
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        DateTime parseTime = new DateTime();
                        if (field.key.Equals("OperatedOn_Start") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format("OperatedOn >= '{0}' ", field.value));
                        }
                        else if (field.key.Equals("OperatedOn_End") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format("OperatedOn <= '{0}' ", field.value));
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
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4Model<EvaluatedRequisition_V>(filters, param, ref result, null);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 养老服务补贴发放对象明细表(象山专用)
        //[WebGet(UriTemplate = "GetRequisitionResult", ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(UriTemplate = "GetRequisitionResult", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CollectionInvokeResult<EvaluatedRequisition_V> GetRequisitionResult(RequisitionParams requisitionParams)
        {
            CollectionInvokeResult<EvaluatedRequisition_V> result = new CollectionInvokeResult<EvaluatedRequisition_V>() { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();
                var dictionary = requisitionParams.ToStringObjectDictionary();
                foreach (var field in dictionary)
                {
                    if (field.Value != null && field.Value.ToString() != "null" && field.Value.ToString() != "")
                    {
                        if (field.Key == "ResidentName" || field.Key == "IDNo")
                        {
                            whereClause.Add(string.Format("{0} like '%{1}%'", field.Key, field.Value));
                        }
                        else
                        {
                            filters[field.Key] = field.Value;
                        }
                    }
                }
                //特殊-只统计低保
                filters["IncomeStatus"] = "00001";
                //权限
                string sql = PermissionsCategoryView();
                if (sql == "user")
                {
                    whereClause.Add("'0'='1'");
                }
                else if (sql != "admin")
                {
                    whereClause.Add(sql);
                }
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<EvaluatedRequisition_V>("GetEvaluatedRequisition_List", filters);
                //result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ListStringObjectDictionary("SP_Pam_GetWorkItemForServeMan", evaluatedRequisition_V);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion 

        #region 养老机构日常运营补贴发放明细表(象山专用)
        [WebInvoke(UriTemplate = "GetRequisitionResult2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CollectionInvokeResult<StringObjectDictionary> GetRequisitionResult2(RequisitionParams requisitionParams)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary>() { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();
                var dictionary = requisitionParams.ToStringObjectDictionary();
                foreach (var field in dictionary)
                {
                    if (field.Value != null && field.Value.ToString() != "null" && field.Value.ToString() != "")
                    {
                        if (field.Key == "ResidentName" || field.Key == "IDNo")
                        {
                            whereClause.Add(string.Format("{0} like '%{1}%'", field.Key, field.Value));
                        }
                        else
                        {
                            filters[field.Key] = field.Value;
                        }
                    }
                }
                //特殊-只统计有机构运营补贴
                filters["ServiceSubsidies"] = 0;
                //权限
                string sql = PermissionsCategoryView();
                if (sql == "user")
                {
                    whereClause.Add("'0'='1'");
                }
                else if (sql != "admin")
                {
                    whereClause.Add(sql);
                }
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ListStringObjectDictionary("GetEvaluatedRequisition_List2", filters);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 更新评估最终结果
        [WebInvoke(UriTemplate = "EvaluationFinalResults", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult EvaluationFinalResults(EvaluatedRequisition evaluatedRequisition)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                evaluatedRequisition.OperatedBy = NormalSession.UserId.ToGuid();
                evaluatedRequisition.OperatedOn = DateTime.Now;
                evaluatedRequisition.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                statements.Add(new IBatisNetBatchStatement { StatementName = evaluatedRequisition.GetUpdateMethodName(), ParameterObject = evaluatedRequisition.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                //基础信息库
                ResidentBaseInfo residentBaseInfo = new ResidentBaseInfo();
                residentBaseInfo.ResidentId = evaluatedRequisition.ResidentId;
                residentBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                residentBaseInfo.OperatedOn = DateTime.Now;
                residentBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                residentBaseInfo.StationId = evaluatedRequisition.StationId;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = residentBaseInfo.GetUpdateMethodName(), ParameterObject = residentBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
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


    public class RequisitionParams
    {
        public string ResidentName { get; set; }
        public string IDNo { get; set; }
        public string AreaId { get; set; }
        public string AreaId2 { get; set; }
        public string AreaId3 { get; set; }
        public string CheckInTimeFrom { get; set; }
        public string CheckInTimeTo { get; set; }
        public string OrderByClause { get; set; }
        public string ServeItemType { get; set; }
    }

}