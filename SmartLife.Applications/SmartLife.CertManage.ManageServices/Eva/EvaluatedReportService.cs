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
    public class EvaluatedReportService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<EvaluatedReport> List()
        {
            CollectionInvokeResult<EvaluatedReport> result = new CollectionInvokeResult<EvaluatedReport> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<EvaluatedReport>();
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
        public CollectionInvokeResult<EvaluatedReport> Query(string strParms)
        {
            CollectionInvokeResult<EvaluatedReport> result = new CollectionInvokeResult<EvaluatedReport> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<EvaluatedReport>(dictionary);
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
        public JqgridCollectionInvokeResult<EvaluatedReport> ListForJqgrid(JqgridCollectionParam<EvaluatedReport> param)
        {
            JqgridCollectionInvokeResult<EvaluatedReport> result = new JqgridCollectionInvokeResult<EvaluatedReport> { Success = true };
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

                gridCollectionPager.JqgridDoPage<EvaluatedReport>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<EvaluatedReport>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<EvaluatedReport> ListForEasyUIgrid(EasyUIgridCollectionParam<EvaluatedReport> param)
        {
            EasyUIgridCollectionInvokeResult<EvaluatedReport> result = new EasyUIgridCollectionInvokeResult<EvaluatedReport> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<EvaluatedReport>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<EvaluatedReport>(filters), param, ref result);
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
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<EvaluatedReport> ListForEasyUIgridPage(EasyUIgridCollectionParam<EvaluatedReport> param)
        {
            EasyUIgridCollectionInvokeResult<EvaluatedReport> result = new EasyUIgridCollectionInvokeResult<EvaluatedReport> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage4Model<EvaluatedReport>(filters, param, ref result);

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
        public ModelInvokeResult<EvaluatedReportPK> Create(EvaluatedReport evaluatedReport)
        {
            ModelInvokeResult<EvaluatedReportPK> result = new ModelInvokeResult<EvaluatedReportPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (evaluatedReport.ReportId == GlobalManager.GuidAsAutoGenerate)
                {
                    evaluatedReport.ReportId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                evaluatedReport.OperatedBy = NormalSession.UserId.ToGuid();
                evaluatedReport.OperatedOn = DateTime.Now;
                evaluatedReport.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = evaluatedReport.GetCreateMethodName(), ParameterObject = evaluatedReport.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EvaluatedReportPK { ReportId = evaluatedReport.ReportId };
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
        [WebInvoke(UriTemplate = "{strReportId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EvaluatedReportPK> Update(string strReportId, EvaluatedReport evaluatedReport)
        {
            ModelInvokeResult<EvaluatedReportPK> result = new ModelInvokeResult<EvaluatedReportPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ReportId = strReportId.ToGuid();
                if (_ReportId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                evaluatedReport.ReportId = _ReportId;
                /***********************begin 自定义代码*******************/
                //社区审核
                if (evaluatedReport.CommunityChecker == GlobalManager.GuidAsAutoGenerate )
                {
                    evaluatedReport.CommunityChecker = NormalSession.UserId.ToGuid();
                }
                else if (evaluatedReport.StreetExaminer == GlobalManager.GuidAsAutoGenerate)
                {//街道审核
                    evaluatedReport.StreetExaminer = NormalSession.UserId.ToGuid();
                }
                else if (evaluatedReport.CityApproval == GlobalManager.GuidAsAutoGenerate)
                {//市民政审核
                    evaluatedReport.CityApproval = NormalSession.UserId.ToGuid();
                }
                else {
                    evaluatedReport.OperatedBy = NormalSession.UserId.ToGuid();
                    evaluatedReport.OperatedOn = DateTime.Now;
                }
                evaluatedReport.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = evaluatedReport.GetUpdateMethodName(), ParameterObject = evaluatedReport.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EvaluatedReportPK { ReportId = _ReportId };

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
        [WebInvoke(UriTemplate = "{strReportId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EvaluatedReportPK> Delete(string strReportId)
        {
            ModelInvokeResult<EvaluatedReportPK> result = new ModelInvokeResult<EvaluatedReportPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ReportId = strReportId.ToGuid();
                if (_ReportId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                EvaluatedReportPK pk = new EvaluatedReportPK { ReportId = _ReportId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new EvaluatedReport().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EvaluatedReportPK { ReportId = _ReportId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strReportIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strReportIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrReportIds = strReportIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrReportIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new EvaluatedReport().GetDeleteMethodName();
                foreach (string strReportId in arrReportIds)
                {
                    EvaluatedReportPK pk = new EvaluatedReportPK { ReportId = strReportId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strReportId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EvaluatedReportPK> Nullify(string strReportId)
        {
            ModelInvokeResult<EvaluatedReportPK> result = new ModelInvokeResult<EvaluatedReportPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ReportId = strReportId.ToGuid();
                if (_ReportId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                EvaluatedReport evaluatedReport = new EvaluatedReport { ReportId = _ReportId, Status = 0 };
                /***********************begin 自定义代码*******************/
                evaluatedReport.OperatedBy = NormalSession.UserId.ToGuid();
                evaluatedReport.OperatedOn = DateTime.Now;
                evaluatedReport.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = evaluatedReport.GetUpdateMethodName(), ParameterObject = evaluatedReport.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EvaluatedReportPK { ReportId = _ReportId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strReportIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strReportIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrReportIds = strReportIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrReportIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new EvaluatedReport().GetUpdateMethodName();
                foreach (string strReportId in arrReportIds)
                {
                    EvaluatedReport evaluatedReport = new EvaluatedReport { ReportId = strReportId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    evaluatedReport.OperatedBy = NormalSession.UserId.ToGuid();
                    evaluatedReport.OperatedOn = DateTime.Now;
                    evaluatedReport.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = evaluatedReport.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, EvaluatedReportPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strReportId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<EvaluatedReport> Load(string strReportId)
        {
            ModelInvokeResult<EvaluatedReport> result = new ModelInvokeResult<EvaluatedReport> { Success = true };
            try
            {
                Guid? _ReportId = strReportId.ToGuid();
                if (_ReportId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var evaluatedReport = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<EvaluatedReport, EvaluatedReportPK>(new EvaluatedReportPK { ReportId = _ReportId });
                result.instance = evaluatedReport;
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

        #region 业务

        #region EasyUIgrid数据格式的分页列表 ListForEasyUIgridPage 带数据库连接strConnectId
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage2/{strConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<EvaluatedReport> ListForEasyUIgridPage2(string strConnectId, EasyUIgridCollectionParam<EvaluatedReport> param)
        {
            EasyUIgridCollectionInvokeResult<EvaluatedReport> result = new EasyUIgridCollectionInvokeResult<EvaluatedReport> { Success = true };
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
                        filters[field.key] = field.value;
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

                gridCollectionPager.EasyUIgridDoPage4Model<EvaluatedReport>(filters, param, ref result, strConnectId);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的分页列表 ListForEasyUIgridPage 带数据库连接strConnectId
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage22", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<EvaluatedReport> ListForEasyUIgridPage22(  EasyUIgridCollectionParam<EvaluatedReport> param)
        {
            EasyUIgridCollectionInvokeResult<EvaluatedReport> result = new EasyUIgridCollectionInvokeResult<EvaluatedReport> { Success = true };
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
                        filters[field.key] = field.value;
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

                gridCollectionPager.EasyUIgridDoPage4Model<EvaluatedReport>(filters, param, ref result, null);

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
        [WebInvoke(UriTemplate = "EvaluatedReportInfo/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> EvaluatedReportInfo(string ConnectId, EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();
                List<string> whereClause2 = new List<string>();
                /**********************************************************/
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        if (field.key != "FlowTo_Start")
                        {
                            filters[field.key] = field.value;
                        }
                        else
                        {
                             if (field.value.Length == 1)
                            {
                                if (filters.ContainsKey("Status"))
                                {
                                    filters.Remove("Status");
                                }
                                whereClause.Add(" Status ='1' ");
                            }
                            else if (field.value.Length==2)
                            {
                                filters["FlowTo"] = field.value;
                            }
                            else if (field.value.Length == 3)
                            {
                                int flowTo =Int32.Parse( field.value.Substring(1));
                                if (flowTo == 10)
                                {
                                    whereClause2.Add("FlowTo <> 10");
                                }
                                else
                                {
                                    whereClause2.Add("FlowFrom between " + (flowTo + 1).ToString() + " and " + (flowTo + 9).ToString());
                                } 
                            }
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
                if (whereClause2.Count > 0)
                {
                    filters.Add("WhereClause2", string.Join(" AND ", whereClause2.ToArray()));
                }
                /**********************************************************/

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("EvaluatedReportInfo_ListByPage", filters, param, ref result, ConnectId);

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
        [WebInvoke(UriTemplate = "EvaluatedReportInfo2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> EvaluatedReportInfo2( EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();
                List<string> whereClause2 = new List<string>();
                /**********************************************************/
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        if (field.key != "FlowTo_Start")
                        {
                            filters[field.key] = field.value;
                        }
                        else
                        {
                            if (field.value.Length == 1)
                            {
                                if (filters.ContainsKey("Status"))
                                {
                                    filters.Remove("Status");
                                }
                                whereClause.Add(" Status ='1' ");
                            }
                            else if (field.value.Length == 2)
                            {
                                filters["FlowTo"] = field.value;
                            }
                            else if (field.value.Length == 3)
                            {
                                int flowTo = Int32.Parse(field.value.Substring(1));
                                if (flowTo == 10)
                                {
                                    whereClause2.Add("FlowTo <> 10");
                                }
                                else
                                {
                                    whereClause2.Add("FlowFrom between " + (flowTo + 1).ToString() + " and " + (flowTo + 9).ToString());
                                }
                            }
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
                if (whereClause2.Count > 0)
                {
                    filters.Add("WhereClause2", string.Join(" AND ", whereClause2.ToArray()));
                }
                /**********************************************************/

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("EvaluatedReportInfo_ListByPage", filters, param, ref result, null);

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
        public string PermissionsCategoryView()
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

        #region 查询评估记录
        [WebGet(UriTemplate = "QueryReportList?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> QueryReportList(string strParms)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ListStringObjectDictionary("QueryEvaluatedReport_List",dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 生成评估单
        [WebInvoke(UriTemplate = "CreateEvaluatedReport", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CreateEvaluatedReport(EvaluatedReport evaluatedReport)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var spParam = evaluatedReport.ToSPParam();
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                spParam["DataSource"] = GlobalManager.DIKey_00012_ManualEdit;
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_Eva_AddAssessmentReportOrder", spParam);
                if (spParam.ErrorCode != 0)
                {
                    result.ErrorCode = spParam.ErrorCode;
                    result.ErrorMessage = spParam.ErrorMessage;
                }
                else {
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

        #region 创建评估单
        [WebInvoke(UriTemplate = "BuildEvaluatedReport", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EvaluatedReport> BuildEvaluatedReport(EvaluatedReport evaluatedReport)
        {
            ModelInvokeResult<EvaluatedReport> result = new ModelInvokeResult<EvaluatedReport> { Success = true };
            try
            {
                var spParam = evaluatedReport.ToSPParam();
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                spParam["DataSource"] = GlobalManager.DIKey_00012_ManualEdit;
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_Eva_AddAssessmentReportOrder", spParam);
                if (spParam.ErrorCode == 0)
                {
                    Guid? _ReportId = spParam.ErrorMessage.ToGuid();
                    if (_ReportId == null)
                    {
                        result.Success = false;
                        result.ErrorCode = 59996;
                        return result;
                    }
                    result.instance = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<EvaluatedReport, EvaluatedReportPK>(new EvaluatedReportPK { ReportId = _ReportId });
                }
                else
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

        #region 更新打印次数
        [WebInvoke(UriTemplate = "UpdateEvaluatedPrintNo/{strReportIds}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UpdateEvaluatedPrintNo(string strReportIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                
                List<string> sqlParamsList = new List<string>();
                Guid? _ReportId;
                string[] reportIdArr = strReportIds.Split('|');
                for (int i = 0; i < reportIdArr.Length; i++)
                {
                    _ReportId = reportIdArr[i].ToGuid();
                    if (_ReportId != null)
                    {
                        sqlParamsList.Add(string.Format("'{0}'", reportIdArr[i]));
                    }
                }

                var evaluatedReportList = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlForQuery("select ReportId,PrintNo from Eva_EvaluatedReport where ReportId in(" + string.Join(" , ", sqlParamsList.ToArray()) + ")");

                EvaluatedReport evaluatedReport;
                foreach(var item in evaluatedReportList){
                    evaluatedReport = new EvaluatedReport();
                    evaluatedReport.ReportId = item["ReportId"].ToString().ToGuid();
                    evaluatedReport.PrintNo = (item["PrintNo"] != null ? int.Parse(item["PrintNo"].ToString()) : 0) + 1;
                    statements.Add(new IBatisNetBatchStatement { StatementName = evaluatedReport.GetUpdateMethodName(), ParameterObject = evaluatedReport.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        #endregion
    }
}