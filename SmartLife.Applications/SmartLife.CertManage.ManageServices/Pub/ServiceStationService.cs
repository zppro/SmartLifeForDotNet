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
using SmartLife.Share.Models.Oca;
using System.Collections;


namespace SmartLife.CertManage.ManageServices.Pub
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServiceStationService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceStation> List()
        {
            CollectionInvokeResult<ServiceStation> result = new CollectionInvokeResult<ServiceStation> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ServiceStation>();
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
        public CollectionInvokeResult<ServiceStation> Query(string strParms)
        {
            CollectionInvokeResult<ServiceStation> result = new CollectionInvokeResult<ServiceStation> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ServiceStation>(dictionary);
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
        public JqgridCollectionInvokeResult<ServiceStation> ListForJqgrid(JqgridCollectionParam<ServiceStation> param)
        {
            JqgridCollectionInvokeResult<ServiceStation> result = new JqgridCollectionInvokeResult<ServiceStation> { Success = true };
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

                gridCollectionPager.JqgridDoPage<ServiceStation>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ServiceStation>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<ServiceStation> ListForEasyUIgrid(EasyUIgridCollectionParam<ServiceStation> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceStation> result = new EasyUIgridCollectionInvokeResult<ServiceStation> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<ServiceStation>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ServiceStation>(filters), param, ref result);
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
        public ModelInvokeResult<ServiceStationPK> Create(ServiceStation serviceStation)
        {
            ModelInvokeResult<ServiceStationPK> result = new ModelInvokeResult<ServiceStationPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (serviceStation.StationId == GlobalManager.GuidAsAutoGenerate)
                {
                    serviceStation.StationId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                serviceStation.OperatedBy = NormalSession.UserId.ToGuid();
                serviceStation.OperatedOn = DateTime.Now;
                serviceStation.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                serviceStation.InputCode1 = GetInputCode(serviceStation.StationName, "py");
                serviceStation.InputCode2 = GetInputCode(serviceStation.StationName, "wb");
                var param = serviceStation.ToStringObjectDictionary(false);
                if (string.IsNullOrEmpty(serviceStation.AreaId2))
                {
                    param["AreaId2"] = DBNull.Value;
                }
                if (string.IsNullOrEmpty(serviceStation.AreaId3))
                {
                    param["AreaId3"] = DBNull.Value;
                }
                /***********************end 自定义代码*********************/
                /*****************  whereClause条件  ************/
                string whereClause = "Status in (0,1,2)";
                if (!string.IsNullOrEmpty(serviceStation.StationType))
                {
                    whereClause = whereClause + " and StationType=" + serviceStation.StationType;
                }
                if (!string.IsNullOrEmpty(serviceStation.StationType2))
                {
                    whereClause = whereClause + " and StationType2=" + serviceStation.StationType2;
                }
                whereClause = whereClause + " and StationId<>'" + serviceStation.StationId + "'";
                /*****************  whereClause条件  ************/
                bool isExist = serviceStation.IsExistValue("Pub_ServiceStation", "StationCode", serviceStation.StationCode, whereClause, GetHttpHeader("ConnectId"));
                if (!isExist)
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = serviceStation.GetCreateMethodName(), ParameterObject = param, Type = SqlExecuteType.INSERT });
                    /***********************begin 自定义代码*******************/

                    /***********************end 自定义代码*********************/
                    BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                    result.instance = new ServiceStationPK { StationId = serviceStation.StationId };
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "机构编码已被使用";
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

        #region 修改 Update
        [WebInvoke(UriTemplate = "{strStationId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceStationPK> Update(string strStationId, ServiceStation serviceStation)
        {
            ModelInvokeResult<ServiceStationPK> result = new ModelInvokeResult<ServiceStationPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _StationId = strStationId.ToGuid();
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                serviceStation.StationId = _StationId;
                /***********************begin 自定义代码*******************/
                serviceStation.OperatedBy = NormalSession.UserId.ToGuid();
                serviceStation.OperatedOn = DateTime.Now;
                serviceStation.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                serviceStation.InputCode1 = GetInputCode(serviceStation.StationName, "py");
                serviceStation.InputCode2 = GetInputCode(serviceStation.StationName, "wb");
                var param = serviceStation.ToStringObjectDictionary(false);
                if (string.IsNullOrEmpty(serviceStation.AreaId2))
                {
                    param["AreaId2"] = DBNull.Value;
                }
                if (string.IsNullOrEmpty(serviceStation.AreaId3))
                {
                    param["AreaId3"] = DBNull.Value;
                }
                /*****************  whereClause条件  ************/
                string whereClause = "Status in (1,2)";
                if (!string.IsNullOrEmpty(serviceStation.StationType))
                {
                    whereClause = whereClause + " and StationType=" + serviceStation.StationType;
                }
                if (!string.IsNullOrEmpty(serviceStation.StationType2))
                {
                    whereClause = whereClause + " and StationType2=" + serviceStation.StationType2;
                }
                whereClause = whereClause + " and StationId<>'" + serviceStation.StationId + "'";
                /*****************  whereClause条件  ************/
                bool isExist = serviceStation.IsExistValue("Pub_ServiceStation", "StationCode", serviceStation.StationCode, whereClause, GetHttpHeader("ConnectId"));
                if (!isExist)
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = serviceStation.GetUpdateMethodName(), ParameterObject = param, Type = SqlExecuteType.UPDATE });
                    /***********************begin 自定义代码*******************/
                    /***********************此处添加自定义代码*****************/
                    /***********************end 自定义代码*********************/
                    BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                    result.instance = new ServiceStationPK { StationId = _StationId };
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "机构编码已被使用";
                }
                /***********************end 自定义代码*********************/

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region BatchUpdate 批量修改状态 (用于审批后更新状态)
        [WebInvoke(UriTemplate = "BatchUpdate/{ids}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult BatchUpdate(string ids)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrStationIds = ids.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrStationIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServiceStation().GetUpdateMethodName();
                foreach (string strStationId in arrStationIds)
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = new { StationId = strStationId.ToGuid(), Status = 1 }, Type = SqlExecuteType.UPDATE });
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

        #region 删除 Delete
        [WebInvoke(UriTemplate = "{strStationId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceStationPK> Delete(string strStationId)
        {
            ModelInvokeResult<ServiceStationPK> result = new ModelInvokeResult<ServiceStationPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _StationId = strStationId.ToGuid();
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ServiceStationPK pk = new ServiceStationPK { StationId = _StationId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new ServiceStation().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceStationPK { StationId = _StationId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strStationIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strStationIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrStationIds = strStationIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrStationIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServiceStation().GetDeleteMethodName();
                foreach (string strStationId in arrStationIds)
                {
                    ServiceStationPK pk = new ServiceStationPK { StationId = strStationId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strStationId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceStationPK> Nullify(string strStationId)
        {
            ModelInvokeResult<ServiceStationPK> result = new ModelInvokeResult<ServiceStationPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _StationId = strStationId.ToGuid();
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ServiceStation serviceStation = new ServiceStation { StationId = _StationId, Status = 0 };
                /***********************begin 自定义代码*******************/
                serviceStation.OperatedBy = NormalSession.UserId.ToGuid();
                serviceStation.OperatedOn = DateTime.Now;
                serviceStation.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serviceStation.GetUpdateMethodName(), ParameterObject = serviceStation.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceStationPK { StationId = _StationId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strStationIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strStationIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrStationIds = strStationIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrStationIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServiceStation().GetUpdateMethodName();
                foreach (string strStationId in arrStationIds)
                {
                    ServiceStation serviceStation = new ServiceStation { StationId = strStationId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    serviceStation.OperatedBy = NormalSession.UserId.ToGuid();
                    serviceStation.OperatedOn = DateTime.Now;
                    serviceStation.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = serviceStation.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                if (!string.IsNullOrEmpty(GetHttpHeader("ConnectId")))
                {
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
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

        #region 级联删除扩展接口 DeleteCascade
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ServiceStationPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strStationId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ServiceStation> Load(string strStationId)
        {
            ModelInvokeResult<ServiceStation> result = new ModelInvokeResult<ServiceStation> { Success = true };
            try
            {
                Guid? _StationId = strStationId.ToGuid();
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var serviceStation = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<ServiceStation, ServiceStationPK>(new ServiceStationPK { StationId = _StationId });
                result.instance = serviceStation;
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

        #region 取得拼音码和五笔码
        public string GetInputCode(string nameStr, string tpye)
        {
            string result = null;
            try
            {
                string param = "";
                if (tpye == "py")
                {
                    param = "select dbo.func_tol_getpy('" + nameStr + "') as InputCode";
                }
                else
                {
                    param = "select dbo.func_tol_getwb('" + nameStr + "') as InputCode";
                }
                result = BuilderFactory.DefaultBulder().ExecuteScalar(param).ToString();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion

        #region 业务接口

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridByConnectId  带有ConnectId
        [WebInvoke(UriTemplate = "ListForEasyUIgridPageByConnectId/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ServiceStation> ListForEasyUIgridPageByConnectId(string ConnectId, EasyUIgridCollectionParam<ServiceStation> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceStation> result = new EasyUIgridCollectionInvokeResult<ServiceStation> { Success = true };
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
                        if (field.key.Contains("AreaId2_Start") && field.value != "")
                        {
                            filters["AreaId2"] = field.value;
                        }
                        else if (field.key.Contains("AreaId3_Start") && field.value != "")
                        {
                            filters["AreaId3"] = field.value;
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
                        if (field.key.IndexOf("AreaId") > -1 && field.value != "")
                        {
                            whereClause.Add(string.Format("(AreaId3 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", field.key, field.value));
                        }
                        else if (field.key.IndexOf("AreaId") > -1 && field.value == "")
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
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }

                gridCollectionPager.EasyUIgridDoPage4Model<ServiceStation>(filters, param, ref result, ConnectId);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的审批流程列表 FlowListForEasyUIgrid
        [WebInvoke(UriTemplate = "FlowListForEasyUIgrid/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> FlowListForEasyUIgrid(string ConnectId, EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                List<string> whereClause2 = new List<string>();

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
                            else
                            {

                            }
                        }
                        else if (field.key != "FlowTo_Start")
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
                                whereClause.Add("Status ='1'");
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
                if (param.fuzzyFields != null)
                {
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        if (field.key.IndexOf("AreaId") > -1 && field.value != "")
                        {
                            whereClause.Add(string.Format("(AreaId3 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", field.key, field.value));
                        }
                        else if (field.key.IndexOf("AreaId") > -1 && field.value == "")
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
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                if (whereClause2.Count > 0)
                {
                    filters.Add("WhereClause2", string.Join(" AND ", whereClause2.ToArray()));
                }
                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ServiceStationBaseInfoAndDictionaryItem_ListByPage", filters, param, ref result, ConnectId);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 同步社区信息
        [WebInvoke(UriTemplate = "SynBaseInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SynBaseInfo()
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                SPParam spParam = new { }.ToSPParam();
                //spParam["Phase"] = Phase;
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_Aync_Pub_ServiceStationBaseInfo", spParam);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 同步商家基本信息
        [WebInvoke(UriTemplate = "SynSellerBaseInfo/{ids}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SynSellerBaseInfo(string ids)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrStationId = ids.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var stationIds = "'" + string.Join("','", arrStationId.ToArray()) + "'";
                SPParam spParam = new { }.ToSPParam();
                spParam["StationIds"] = stationIds;
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_Push_Pub_ServiceStation", spParam);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 新增商家基本信息
        [WebInvoke(UriTemplate = "PullSellerBaseInfo/{ids}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult PullSellerBaseInfo(string ids)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var connectId = ids.Substring(ids.LastIndexOf('|') + 1);//取跨库链接字符串
                ids = ids.Remove(ids.LastIndexOf('|'));
                var areaid = ids.Substring(ids.LastIndexOf('|') + 1);//取当前操作所在的区
                ids = ids.Remove(ids.LastIndexOf('|'));
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrStationId = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ServiceStation>(new { Status = 1, StationType = "00003" }.ToStringObjectDictionary());
                List<string> stationIds = new List<string>();
                for (var i = 0; i < arrStationId.Length; i++)
                {
                    int icount = rows.Count(s => Convert.ToString(s.StationId) == arrStationId[i]);
                    if (icount <= 0)
                    {
                        stationIds.Add("'" + arrStationId[i] + "'");//区级库是否已经存在选择的商家
                    }
                }
                if (stationIds.Count > 0)
                {
                    //如果区级库不存在此商家
                    SPParam spParam = new { }.ToSPParam();
                    spParam["StationIds"] = string.Join(",", stationIds.ToArray());
                    BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_Pull_Pub_ServiceStation", spParam);//同步到区级库

                    User user = new User();
                    for (var j = 0; j < stationIds.Count; j++)
                    {
                        var s1 = stationIds[j].Remove(0, 1);
                        var stationId = s1.Remove(s1.Length - 1, 1);
                        var queryUserOfStation=QueryUserOfStation(stationId);//此商家下原来是否有停用的用户
                        if (queryUserOfStation == "")
                        {
                            //新增商家   增加默认管理员账号
                            var dictionary = new { StationId = stationId }.ToStringObjectDictionary();
                            CollectionInvokeResult<ServiceStation> station_result = new CollectionInvokeResult<ServiceStation> { Success = true };
                            station_result.rows = BuilderFactory.DefaultBulder(connectId).List<ServiceStation>(dictionary);
                            user.UserId = Guid.NewGuid();
                            user.UserName = station_result.rows[0].StationName;
                            user.UserCode = station_result.rows[0].StationCode + "-admin";
                            user.Gender = "N";
                            user.UserType = "00003|00001000-0002-0000-0000-000000000001";
                            user.SystemFlag = 1;
                            user.Area1 = areaid;
                            user.CreatedBy = NormalSession.UserId.ToGuid();
                            user.CreatedOn = DateTime.Now;
                            user.InputCode1 = GetInputCode(station_result.rows[0].StationName, "py");
                            user.InputCode2 = GetInputCode(station_result.rows[0].StationName, "wb");
                            if (!CreateStationUser(user).Success)
                            {
                                result.Success = false;
                                result.ErrorMessage = "商家管理员帐号注册失败";
                            }
                            else
                            {
                                SetUserForStation(user.UserId.ToString(), stationId);
                            }
                        }
                        else if (queryUserOfStation == "账号启用失败")
                        {
                            result.Success = false;
                            result.ErrorMessage = "原有的账号恢复出现异常";
                        }
                    }
                }
                else
                {
                    ///已存在的商家，如果没有系统默认的账号   就新建账号
                    EasyUIgridCollectionInvokeResult<User> ret = new EasyUIgridCollectionInvokeResult<User> { Success = true };
                    User user = new User();
                    for (var i = 0; i < arrStationId.Length; i++)
                    {
                        bool flag = false;//判断是否已经存在SystemFlag=1的账号
                        var stationId = arrStationId[i];
                        string whereClause = "UserId in (select UserId from Pub_MerchantUser where StationId='" + arrStationId[i] + "')";
                        ret.rows = BuilderFactory.DefaultBulder().List<User>("User_ListAsServeManForInput", new { WhereClause = whereClause }.ToStringObjectDictionary());
                        if (ret.rows.Count > 0)
                        {
                            //商家下有账号时，判断是否已经存在SystemFlag=1的账号
                            int icount = ret.rows.Count(s => Convert.ToString(s.SystemFlag) == "1");
                            if (icount <= 0)
                            {
                                flag = true;
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            //商家下没有账号时
                            flag = true;
                        }

                        if (flag) {
                            //新增商家   增加默认管理员账号
                            var dictionary = new { StationId = stationId }.ToStringObjectDictionary();
                            CollectionInvokeResult<ServiceStation> station_result = new CollectionInvokeResult<ServiceStation> { Success = true };
                            station_result.rows = BuilderFactory.DefaultBulder(connectId).List<ServiceStation>(dictionary);
                            user.UserId = Guid.NewGuid();
                            user.UserName = station_result.rows[0].StationName;
                            user.UserCode = station_result.rows[0].StationCode + "-admin";
                            user.Gender = "N";
                            user.UserType = "00003|00001000-0002-0000-0000-000000000001";
                            user.SystemFlag = 1;
                            user.Area1 = areaid;
                            user.CreatedBy = NormalSession.UserId.ToGuid();
                            user.CreatedOn = DateTime.Now;
                            user.InputCode1 = GetInputCode(station_result.rows[0].StationName, "py");
                            user.InputCode2 = GetInputCode(station_result.rows[0].StationName, "wb");
                            if (!CreateStationUser(user).Success)
                            {
                                result.Success = false;
                                result.ErrorMessage = "商家管理员帐号注册失败";
                            }
                            else
                            {
                                SetUserForStation(user.UserId.ToString(), stationId);
                            }                        
                        }

                    }
                    result.Success = false;
                    result.ErrorMessage = "所选商家以全部存在，请用初始账号登陆";
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


        #region 查看商家是否已经有用户存在  QueryUserOfStation
        private string QueryUserOfStation(string stationId)
        {
            string result = "";
            try
            {
                CollectionInvokeResult<MerchantUser> ret = new CollectionInvokeResult<MerchantUser> { Success = true };
                var param = new { StationId = stationId }.ToStringObjectDictionary();
                ret.rows = BuilderFactory.DefaultBulder().List<MerchantUser>(param);//查出这个商家下的所有用户
                var count=ret.rows.Count ;
                if (count > 0)
                {
                    var flog = true;
                    for (var i = 0; i < count; i++)
                    {
                        //把所有用户都重新启用，如果启用失败   则返回false
                        if (!UpdateUsersOfStationId(ret.rows[i].UserId.ToString(), "restart").Success) 
                        {
                            flog = false;
                        };
                    }
                    if (flog)
                    {
                        result = "账号启用成功";
                    }
                    else 
                    {
                        result = "账号启用失败";
                    }
                }
                else {
                    result = "";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion

        #region 如果商家下没有用户，则新建一个默认的用户
        private ModelInvokeResult<UserPK> CreateStationUser(User user)
        {
            ModelInvokeResult<UserPK> result = new ModelInvokeResult<UserPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (user.UserId == GlobalManager.GuidAsAutoGenerate)
                {
                    user.UserId = Guid.NewGuid();
                }
                user.PasswordHash = e0571.web.core.Security.MD5Provider.Generate("88976666");//
                user.CreatedBy = NormalSession.UserId.ToGuid();
                user.CreatedOn = DateTime.Now;
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/

                /***********************begin 自定义代码*******************/
                List<string> groupIds = new List<string>();
                if (user.isNormalUser(groupIds) || user.isMerchantUser(groupIds) || user.isAgencyUser(groupIds) || user.isDayCareUser(groupIds))
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = user.GetCreateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    user.setRoles(statements, groupIds);
                }
                else
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = user.GetCreateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                SPParam spParam = new { }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceUser", spParam);
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceGroupMember", spParam);
                result.instance = new UserPK { UserId = user.UserId };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 新增的用户名与商家建立关系
        private InvokeResult SetUserForStation(string userId, string stationId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                MerchantUser ua = new MerchantUser { UserId = userId.ToGuid() };
                statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetDeleteMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });

                ua.StationId = stationId.ToGuid();
                statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetCreateMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });

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

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridByStationId 根据商家Id查询用户
        [WebInvoke(UriTemplate = "ListForEasyUIgridByStationId/{stationId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridByStationId(string stationId, EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();
                User user = new User();
                if (param.filterFields != null)
                {

                    foreach (var field in param.filterFields)
                    {
                        if (field.key == "UserType")
                        {
                            user.UserType = field.value;
                        }
                        else
                        {
                            filters[field.key] = field.value;
                        }
                    }
                    List<string> groupIds = new List<string>();
                    if (user.isNormalUser(groupIds) || user.isMerchantUser(groupIds) )
                    {
                        if (groupIds.Count > 0)
                        {
                            whereClause.Add(" a.UserId in (select UserId from Pub_GroupMember where GroupId in ('" + string.Join("','", groupIds.ToArray()) + "') and a.UserId in (select UserId from Pub_MerchantUser where StationId='" + stationId + "'))");
                        }
                    } 
                }

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

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ListForEasyUIgridByStationId", filters, param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 停用/启用商家时修改该商家下所以用户的状态 Update
        [WebInvoke(UriTemplate = "UpdateUsersOfStationId/{strStationId},{stopOrrestart}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<UserPK> UpdateUsersOfStationId(string strStationId, string stopOrrestart)
        {
            ModelInvokeResult<UserPK> result = new ModelInvokeResult<UserPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _StationId = strStationId.ToGuid();//商家的默认管理员UserId是商家的StationId
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                /***********************取商家下的所有用户*******************/
                var param = new { StationId = _StationId, StopFlag = (stopOrrestart == "stop" ? 1 : 0), Status = (stopOrrestart == "stop" ? 0 : 1) }.ToStringObjectDictionary();
                statements.Add(new IBatisNetBatchStatement { StatementName = "UpdateUserForMerchantBatch", ParameterObject = param, Type = SqlExecuteType.UPDATE });
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

        #region UpdateServiceStations 部分批量修改状态 (用于审批后更新已选择的状态)
        [WebInvoke(UriTemplate = "UpdateServiceStations/{_status},{ids}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UpdateServiceStations(string _status, string ids)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrStationId = ids.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrStationId.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServiceStation().GetUpdateMethodName();
                foreach (string strStationId in arrStationId)
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = new { StationId = strStationId.ToGuid(), Status = _status, OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now }, Type = SqlExecuteType.UPDATE });
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

        #region UpdateBaseResidents 全部批量修改状态 (用于审批后更新符合条件的所以状态)
        [WebInvoke(UriTemplate = "UpdateServiceStationsAll", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UpdateBaseResidentsAll(FlowAction flowAction)
        {
            InvokeResult result = new InvokeResult { Success = true };
            //流转状态  0-正流，1-逆流  默认正流，正流（当前状态加上当前操作）；逆流（当前状态）
            int iAction = 0;
            if (flowAction.FlowType == 0)
            {
                //默认正向流转
                iAction += flowAction.ProcessAction;
            }

            try
            {
                FlowDefineMapping flowDefineMapping = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<FlowDefineMapping>(new { MappingType = flowAction.MappingType, MappingColumn = flowAction.MappingColumn, MappingId = flowAction.MappingId }.ToStringObjectDictionary()).First();
                if (flowDefineMapping != null && !string.IsNullOrEmpty(flowDefineMapping.FlowName))
                {
                    flowAction.FlowName = flowDefineMapping.FlowName;
                }

                //找出当前流程的最高去处和默认去处
                IList<FlowDefine> flowDefineList = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<FlowDefine>(new { FlowName = flowAction.FlowName, TableName = flowAction.TableName, TableColumn = flowAction.TableColumn, Status = 1, OrderByClause = " FlowTo ,CurrentState " }.ToStringObjectDictionary());

                //如果是流程到了最顶级的话或者未进入流程或者切换流程则跳回当前流程默认的最低级别,以达到重新开始流转效果
                string sql = "update {1} set Status=1 where {0} in (";

                sql += "select x.{0} From  (" +
                    "select a.{0},MAX(b.Id) as FId From {1} a left join Pub_Flow b  on a.{0}=b.BIZ_ID " +
                    "where a.{4}= '{5}'  ";

                if (!string.IsNullOrEmpty(flowAction.WhereClause)) {
                        sql += " and " + flowAction.WhereClause;
                    }

                sql += "group by a.{0} ) x left join Pub_Flow y on x.FId=y.Id  left join Pub_FlowDefine z on  y.FlowDefineId=z.FlowDefineId " +
                    "where z.FlowName='{2}' and z.TableName='{1}' and z.TableColumn='{0}' and y.FlowTo = {3}";

                sql += ")";

                string statements = string.Format(sql, flowAction.TableColumn, flowAction.TableName, flowAction.FlowName,
                    flowDefineList.Last().FlowTo, flowAction.MappingColumn, flowAction.MappingId);

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


        #region 验证机构编码是否已被使用
        [WebGet(UriTemplate = "StationCodeDuplicates?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> StationCodeDuplicates(string strParms)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                var param = "";
                param = "select * from Pub_ServiceStation where Status >" + dictionary["Status"] + " and StationCode='" + dictionary["StationCode"] + "'";
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlForQuery(param);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 获取服务站类型2
        [WebGet(UriTemplate = "GetStationType2/{stationType}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictionaryItem> GetStationType2(string stationType)
        {
            return BuilderFactory.DefaultBulder().List<DictionaryItem>("GetStationType2", new { StationType = stationType }.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL));
        }
        #endregion

        #region 获取商家对应的服务项目
        [WebGet(UriTemplate = "GetServeItems/{stationId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<MerchantServeItem> GetServeItems(string stationId)
        {
            return BuilderFactory.DefaultBulder().List<MerchantServeItem>(new MerchantServeItem { StationId = stationId.ToGuid() });
        }
        #endregion


        #region 获取商家对应的服务方式
        [WebGet(UriTemplate = "GetServeModes/{stationId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<MerchantServeMode> GetServeModes(string stationId)
        {
            return BuilderFactory.DefaultBulder().List<MerchantServeMode>(new MerchantServeMode { StationId = stationId.ToGuid() });
        }
        #endregion

        #region 设置商家对应的服务项目和服务方式
        [WebInvoke(UriTemplate = "SetServeItemsAndServeModes/{stationId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SetServeItemsAndServeModes(string stationId, SetServeItemsAndServeModesParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                MerchantServeMode xMerchantServeMode = new MerchantServeMode { StationId = stationId.ToGuid(), DataSource = GlobalManager.DIKey_00012_ManualEdit };
                statements.Add(new IBatisNetBatchStatement { StatementName = xMerchantServeMode.GetDeleteMethodName(), ParameterObject = xMerchantServeMode.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL), Type = SqlExecuteType.DELETE });

                if (param.ServeMode != null && param.ServeMode.Count > 0)
                {
                    xMerchantServeMode.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    foreach (var serveMode in param.ServeMode)
                    {
                        xMerchantServeMode.ServeMode = serveMode;
                        statements.Add(new IBatisNetBatchStatement { StatementName = xMerchantServeMode.GetCreateMethodName(), ParameterObject = xMerchantServeMode.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL), Type = SqlExecuteType.INSERT });
                    }
                }

                MerchantServeItem xMerchantServeItem = new MerchantServeItem { StationId = stationId.ToGuid() };
                statements.Add(new IBatisNetBatchStatement { StatementName = xMerchantServeItem.GetDeleteMethodName(), ParameterObject = xMerchantServeItem.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL), Type = SqlExecuteType.DELETE });

                if (param.ServeItemB != null && param.ServeItemB.Count > 0)
                {
                    xMerchantServeItem.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    //两者一样的数量
                    for (int i = 0; i < param.ServeItemB.Count; i++)
                    {
                        xMerchantServeItem.ServeItemB = param.ServeItemB[i];
                        xMerchantServeItem.ServeFee = decimal.Parse(param.ServeFee[i]);
                        xMerchantServeItem.ServeFeeRemarks = param.ServeFeeRemarks[i];
                        statements.Add(new IBatisNetBatchStatement { StatementName = xMerchantServeItem.GetCreateMethodName(), ParameterObject = xMerchantServeItem.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL), Type = SqlExecuteType.INSERT });
                    }
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

        #region 设置商家对应的服务区域
        [WebInvoke(UriTemplate = "SetMerchantServeArea/{stationId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SetMerchantServeArea(string stationId, IList<string> AreaId3s)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                MerchantArea xMerchantArea = new MerchantArea { StationId = stationId.ToGuid(), DataSource = GlobalManager.DIKey_00012_ManualEdit };
                statements.Add(new IBatisNetBatchStatement { StatementName = xMerchantArea.GetDeleteMethodName(), ParameterObject = xMerchantArea.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL), Type = SqlExecuteType.DELETE });

                if (AreaId3s != null && AreaId3s.Count > 0)
                {
                    foreach (var areaId in AreaId3s)
                    {
                        xMerchantArea.ServeArea = areaId;
                        statements.Add(new IBatisNetBatchStatement { StatementName = xMerchantArea.GetCreateMethodName(), ParameterObject = xMerchantArea.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL), Type = SqlExecuteType.INSERT });
                    }
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

        #region 获取商家服务期间
        [WebInvoke(UriTemplate = "GetServePeriodsForEasyUIgrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> GetServePeriodsForEasyUIgrid(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                gridCollectionPager.EasyUIgridDoPage<StringObjectDictionary>(BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetMerchantServePeriods"), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        #endregion

        #region 添加政府购买商家服务期间
        [WebInvoke(UriTemplate = "AddMerchantServePeriod", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult AddMerchantServePeriod(AddMerchantServePeriodParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var spParam = param.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_AddMerchantServePeriod", spParam);
                if (spParam.ErrorCode != 0)
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

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ServiceStation> ListForEasyUIgridPage(EasyUIgridCollectionParam<ServiceStation> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceStation> result = new EasyUIgridCollectionInvokeResult<ServiceStation> { Success = true };
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
                        if (field.key.Contains("AreaId2_Start") && field.value != "")
                        {
                            filters["AreaId2"] = field.value;
                        }
                        else if (field.key.Contains("AreaId3_Start") && field.value != "")
                        {
                            filters["AreaId3"] = field.value;
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
                /**********************************************************/
                gridCollectionPager.EasyUIgridDoPage4Model<ServiceStation>(filters, param, ref result, GetHttpHeader("ConnectId"));

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region EasyUIgrid数据格式的列表 ListForEasyUIgridPageForPullSeller
        [WebInvoke(UriTemplate = "ListForEasyUIgridPageForPullSeller/{strConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ServiceStation> ListForEasyUIgridPageForPullSeller(string strConnectId, EasyUIgridCollectionParam<ServiceStation> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceStation> result = new EasyUIgridCollectionInvokeResult<ServiceStation> { Success = true };
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
                gridCollectionPager.EasyUIgridDoPage4Model<ServiceStation>(filters, param, ref result, strConnectId);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region EasyUIgrid数据格式的列表根据街道社区树过滤商家  ListForEasyUIgridPageByTree
        [WebInvoke(UriTemplate = "ListForEasyUIgridPageByTree", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ServiceStation> ListForEasyUIgridPageByTree(EasyUIgridCollectionParam<ServiceStation> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceStation> result = new EasyUIgridCollectionInvokeResult<ServiceStation> { Success = true };
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
                }
                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
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
                /**********************************************************/
                /***********************begin 排序*************************/
                /**********************************************************/
                gridCollectionPager.EasyUIgridDoPage4Model<ServiceStation>(filters, param, ref result, GetHttpHeader("ConnectId"));
                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 市级商家业务操作
        #region 删除MerchantOpenArea表的记录，下次同步时，不再同步这个区的信息了
        [WebInvoke(UriTemplate = "UpdateCityMerchantArea/{strStationId},{strAreaId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<MerchantOpenAreaPK> UpdateCityMerchantArea(string strStationId, string strAreaId)
        {
            ModelInvokeResult<MerchantOpenAreaPK> result = new ModelInvokeResult<MerchantOpenAreaPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _StationId = strStationId.ToGuid();
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string _AreaId = strAreaId;
                MerchantOpenAreaPK pk = new MerchantOpenAreaPK { StationId = _StationId, AreaId = _AreaId };
                statements.Add(new IBatisNetBatchStatement { StatementName = new MerchantOpenArea().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MerchantOpenAreaPK { StationId = _StationId, AreaId = _AreaId };
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


        #region 服务平台

        #region 获取服务人员
        [WebGet(UriTemplate = "ListServeMan/{stationId},{userType},{groupId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<User> ListServeMan(string stationId, string userType, string groupId)
        {
            CollectionInvokeResult<User> result = new CollectionInvokeResult<User> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<User>("User_ListAsServeMan", new { StationId = stationId, UserType = userType, GroupId = groupId }.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 键入即时获取服务人员
        [WebInvoke(UriTemplate = "ListServeManForInput/{groupId},{stationId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<User> ListServeManForInput(string groupId, string stationId, EasyUIgridCollectionParam<User> param)
        {
            EasyUIgridCollectionInvokeResult<User> result = new EasyUIgridCollectionInvokeResult<User> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }
                string whereClause = "UserType='" + filters["UserType"] + "' and  UserId in (select a.UserId from Pub_MerchantUser a inner join Pub_GroupMember b on a.UserId=b.UserId where a.StationId='" + stationId + "' and b.GroupId='" + groupId + "') and ( UserName like '" + filters["UserName"] + "%' or UserCode like '" + filters["UserName"] + "%')";
                result.rows = BuilderFactory.DefaultBulder().List<User>("User_ListAsServeManForInput", new { WhereClause = whereClause }.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取排班 
        [WebInvoke(UriTemplate = "ListSchedule", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListSchedule(EasyUIgridCollectionParam<ListScheduleParam> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { StationId = param.instance.StationId, AreaId = param.instance.AreaId, ServeManId = param.instance.ServeManId }.ToStringObjectDictionary();
                DateTime beginDate = DateTime.Parse(param.instance.BeginDateStr);
                DateTime endDate = DateTime.Parse(param.instance.EndDateStr);
                List<string> thePivotColumnsForSelect = new List<string>();
                List<string> thePivotColumns = new List<string>();
                TimeSpan ts = endDate - beginDate;
                DateTime dt = beginDate;
                for (int i = 0; i < ts.Days; i++)
                {
                    thePivotColumnsForSelect.Add(string.Format("[{0}] as _{0}",dt.ToString("yyyyMMdd")));
                    thePivotColumns.Add(dt.ToString("yyyyMMdd"));
                    dt = dt.AddDays(1); 
                }
                thePivotColumnsForSelect.Add(string.Format("[{0}] as _{0}", dt.ToString("yyyyMMdd")));
                thePivotColumns.Add(dt.ToString("yyyyMMdd"));
                filters.Add("PivotColumnsForSelect", string.Join(",", thePivotColumnsForSelect.ToArray()));
                filters.Add("PivotColumns", "[" + string.Join("],[", thePivotColumns.ToArray()) + "]");
                if (param.instance.OnlyShowGovTurnkey)
                {
                    filters.Add("GovTurnkeyFlag", 1);
                }
                if (param.instance.OldManName!="" && param.instance.OldManName!=null) 
                {
                    filters.Add("OldManName","OldManName like '" +param.instance.OldManName+"%'");
                }

                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("ScheduleListByMerchant", filters); 
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取签到情况
        [WebInvoke(UriTemplate = "ListCheckIn", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListCheckIn(EasyUIgridCollectionParam<ListCheckInParam> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { StationId = param.instance.StationId }.ToStringObjectDictionary();
                DateTime beginDate = DateTime.Parse(param.instance.BeginDateStr);
                DateTime endDate = DateTime.Parse(param.instance.EndDateStr);
                List<string> thePivotColumnsForSelect = new List<string>();
                List<string> thePivotColumns = new List<string>();
                TimeSpan ts = endDate - beginDate;
                DateTime dt = beginDate;
                for (int i = 0; i < ts.Days; i++)
                {
                    thePivotColumnsForSelect.Add(string.Format("[{0}] as _{0}", dt.ToString("yyyyMMdd")));
                    thePivotColumns.Add(dt.ToString("yyyyMMdd"));
                    dt = dt.AddDays(1);
                }
                thePivotColumnsForSelect.Add(string.Format("[{0}] as _{0}", dt.ToString("yyyyMMdd")));
                thePivotColumns.Add(dt.ToString("yyyyMMdd"));
                filters.Add("PivotColumnsForSelect", string.Join(",", thePivotColumnsForSelect.ToArray()));
                filters.Add("PivotColumns", "[" + string.Join("],[", thePivotColumns.ToArray()) + "]");
                //string sql = BuilderFactory.DefaultBulder().GetSql("GetMerchantUserCheckInInfo", filters);

                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetMerchantUserCheckInInfo", filters);
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


        #region 政府监管平台
        [WebInvoke(UriTemplate = "Pub_ServiceStation_ForMonitor_V", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ServiceStation_ForMonitor_V> OldManBaseInfo_ForMonitor_V_ListByPage(EasyUIgridCollectionParam<ServiceStation_ForMonitor_V> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceStation_ForMonitor_V> result = new EasyUIgridCollectionInvokeResult<ServiceStation_ForMonitor_V> { Success = true };
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
                        if (field.key.Contains("Tel") || field.key.Contains("LinkManMobile") || field.key.Contains("StationName")  || field.key.Contains("InputCode1") || field.key.Contains("InputCode2"))
                        {
                            fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
                        else
                        {
                            whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
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

                gridCollectionPager.EasyUIgridDoPage4Model<ServiceStation_ForMonitor_V>(filters, param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region   机构养老

        #region 新建养老机构
        [WebInvoke(UriTemplate = "AddAgencyBaseInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult AddAgencyBaseInfo(IList<ServiceStation> serviceStations)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (serviceStations == null || serviceStations.Count == 0)
                {
                    //配置老人不存在
                    result.Success = false;
                    result.ErrorMessage = "请选择配置老人";
                    return result;
                }
                 
                foreach (ServiceStation item in serviceStations)
                { 
                    item.Status = 1;
                    item.OperatedBy = NormalSession.UserId.ToGuid();
                    item.OperatedOn = DateTime.Now; 
                    statements.Add(new IBatisNetBatchStatement { StatementName = item.GetUpdateMethodName(), ParameterObject = item.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }

                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
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

        #region 作废社会组织（变更机构类型）
        [WebInvoke(UriTemplate = "UnAddAgencyBaseInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UnAddAgencyBaseInfo(IList<ServiceStation> serviceStations)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (serviceStations == null || serviceStations.Count == 0)
                {
                    //配置老人不存在
                    result.Success = false;
                    result.ErrorMessage = "请选择社会组织";
                    return result;
                }

                var strStationIds = string.Join("','", serviceStations.Select(item => item.StationId.ToString()));
                string residentStatus = "";
                foreach (var o in serviceStations) { residentStatus = o.StationType2 == "00001" ? "7" : "9"; }
                string sql = "select * from Oca_OldManBaseInfo where Status=3 and ResidentStatus=" + residentStatus + " and StationId  in ('" + strStationIds + "')";
                int count = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(sql);
                 
                if (count < 1)
                {
                    foreach (ServiceStation item in serviceStations)
                    {
                        item.OperatedBy = NormalSession.UserId.ToGuid();
                        item.OperatedOn = DateTime.Now;
                        statements.Add(new IBatisNetBatchStatement { StatementName = item.GetUpdateMethodName(), ParameterObject = item.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                        UpdateUsersOfAgency(item.StationId.ToString(), "stop");
                    }
                    BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements); 
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "选择的社会组织内还有老人存在，不允许此操作";
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

        #region 新建社会组织管理员账号
        [WebInvoke(UriTemplate = "PullAgencyBaseInfo/{ids}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult PullAgencyBaseInfo(string ids,User user)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var connectId = ids.Substring(ids.LastIndexOf('|') + 1);//取跨库链接字符串
                ids = ids.Remove(ids.LastIndexOf('|'));
                var areaid = ids.Substring(ids.LastIndexOf('|') + 1);//取当前操作所在的区
                ids = ids.Remove(ids.LastIndexOf('|'));
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrStationId = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ServiceStation>(new { Status = 1, StationType = "00006" }.ToStringObjectDictionary());
                List<string> stationIds = new List<string>();
                for (var i = 0; i < arrStationId.Length; i++)
                {
                    int icount = rows.Count(s => Convert.ToString(s.StationId) == arrStationId[i]);
                    if (icount <= 0)
                    {
                        stationIds.Add("'" + arrStationId[i] + "'");//区级库是否已经存在选择的社会组织
                    }
                }
                if (stationIds.Count > 0)
                {
                    //如果区级库不存在此社会组织
                    SPParam spParam = new { }.ToSPParam();
                    spParam["StationIds"] = string.Join(",", stationIds.ToArray());
                    BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_Pull_Pub_ServiceStation", spParam);//同步到区级库
                     
                    for (var j = 0; j < stationIds.Count; j++)
                    {
                        var s1 = stationIds[j].Remove(0, 1);
                        var stationId = s1.Remove(s1.Length - 1, 1);
                        var queryUserOfAgency = QueryUserOfAgency(stationId);//此社会组织下原来是否有停用的用户
                        if (queryUserOfAgency == "")
                        {
                            //新增社会组织   增加默认管理员账号
                            var dictionary = new { StationId = stationId }.ToStringObjectDictionary();
                            CollectionInvokeResult<ServiceStation> station_result = new CollectionInvokeResult<ServiceStation> { Success = true };
                            station_result.rows = BuilderFactory.DefaultBulder(connectId).List<ServiceStation>(dictionary);
                            user.UserId = Guid.NewGuid();
                            user.UserName = station_result.rows[0].StationName;
                            user.UserCode = GetInputCode(station_result.rows[0].StationName, "py").ToLower();
                            user.Gender = "N"; 
                            user.SystemFlag = 1;
                            user.Area1 = areaid;
                            user.CreatedBy = NormalSession.UserId.ToGuid();
                            user.CreatedOn = DateTime.Now;
                            user.InputCode1 = GetInputCode(station_result.rows[0].StationName, "py");
                            user.InputCode2 = GetInputCode(station_result.rows[0].StationName, "wb");
                            if (!CreateStationUser(user).Success)
                            {
                                result.Success = false;
                                result.ErrorMessage = "社会组织管理员帐号注册失败";
                            }
                            else
                            {
                                SetUserForAgency(user.UserId.ToString(), stationId);
                            }
                        }
                        else if (queryUserOfAgency == "账号启用失败")
                        {
                            result.Success = false;
                            result.ErrorMessage = "原有的账号恢复出现异常";
                        }
                    }
                }
                else
                {
                    ///已存在的社会组织，如果没有系统默认的账号   就新建账号
                    EasyUIgridCollectionInvokeResult<User> ret = new EasyUIgridCollectionInvokeResult<User> { Success = true }; 
                    for (var i = 0; i < arrStationId.Length; i++)
                    {
                        bool flag = false;//判断是否已经存在SystemFlag=1的账号
                        var stationId = arrStationId[i];
                        QueryUserOfAgency(stationId);//此社会组织下原来是否有停用的用户 
                        string strParms = "{\"WhereClause\":\"UserId in (select UserId from Pub_PensionAgencyUser where StationId='" + arrStationId[i] + "')\"}";
                       
                        ret.rows = BuilderFactory.DefaultBulder().List<User>(new StringObjectDictionary().MixInJson(strParms));

                        //string whereClause = "UserId in (select UserId from Pub_PensionAgencyUser where StationId='" + arrStationId[i] + "')";
                        //ret.rows = BuilderFactory.DefaultBulder().List<User>("User_ListAsServeManForInput", new { WhereClause = whereClause }.ToStringObjectDictionary());
                        if (ret.rows.Count > 0)
                        {
                            //社会组织下有账号时，判断是否已经存在SystemFlag=1的账号
                            int icount = ret.rows.Count(s => Convert.ToString(s.SystemFlag) == "1");
                            if (icount <= 0)
                            {
                                flag = true;
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            //社会组织下没有账号时
                            flag = true;
                        }
                        if (flag)
                        {
                            //新增社会组织   增加默认管理员账号
                            var dictionary = new { StationId = stationId }.ToStringObjectDictionary();
                            CollectionInvokeResult<ServiceStation> station_result = new CollectionInvokeResult<ServiceStation> { Success = true };
                            station_result.rows = BuilderFactory.DefaultBulder(connectId).List<ServiceStation>(dictionary);
                            user.UserId = Guid.NewGuid();
                            user.UserName = station_result.rows[0].StationName;
                            user.UserCode = GetInputCode(station_result.rows[0].StationName, "py").ToLower(); ;
                            user.Gender = "N"; 
                            user.SystemFlag = 1;
                            user.Area1 = areaid;
                            user.CreatedBy = NormalSession.UserId.ToGuid();
                            user.CreatedOn = DateTime.Now;
                            user.InputCode1 = GetInputCode(station_result.rows[0].StationName, "py");
                            user.InputCode2 = GetInputCode(station_result.rows[0].StationName, "wb");
                            if (!CreateStationUser(user).Success)
                            {
                                result.Success = false;
                                result.ErrorMessage = "社会组织管理员帐号注册失败";
                            }
                            else
                            {
                                SetUserForAgency(user.UserId.ToString(), stationId);
                            }
                        }

                    } 
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

        #region 新增的用户名与社会组织建立关系
        private InvokeResult SetUserForAgency(string userId, string stationId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                PensionAgencyUser ua = new PensionAgencyUser { UserId = userId.ToGuid() };
                statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetDeleteMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });

                ua.StationId = stationId.ToGuid();
                statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetCreateMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });

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


        #region 作废社会组织
        [WebInvoke(UriTemplate = "NullifyAgency/{strStationId},{strStationType2}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceStationPK> NullifyAgency(string strStationId, string strStationType2)
        {
            ModelInvokeResult<ServiceStationPK> result = new ModelInvokeResult<ServiceStationPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _StationId = strStationId.ToGuid();
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string residentStatus = strStationType2 == "00001" ? "7" : "9";
                string sql = "select * from Oca_OldManBaseInfo where Status=3 and ResidentStatus=" + residentStatus + " and StationId='" + strStationId + "'";
                int count = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(sql);
                 
                if (count < 1)
                {
                    ServiceStation serviceStation = new ServiceStation { StationId = _StationId, Status = 0 };
                    /***********************begin 自定义代码*******************/
                    serviceStation.OperatedBy = NormalSession.UserId.ToGuid();
                    serviceStation.OperatedOn = DateTime.Now;
                    serviceStation.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = serviceStation.GetUpdateMethodName(), ParameterObject = serviceStation.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                    /***********************begin 自定义代码*******************/
                    BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                    result.instance = new ServiceStationPK { StationId = _StationId };
                    UpdateUsersOfAgency(strStationId, "stop");
                    
                    /***********************解除社会组织下的用户关系*****************/
                    PensionAgencyUser ua = new PensionAgencyUser { StationId = _StationId };
                    statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetDeleteMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);

                    /***********************end 自定义代码*********************/
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "该社会组织内还有老人存在,不允许作废";
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


        #region 停用/启用社会组织时修改该社会组织下所以用户的状态 Update
        [WebInvoke(UriTemplate = "UpdateUsersOfAgency/{strStationId},{stopOrrestart}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<UserPK> UpdateUsersOfAgency(string strStationId, string stopOrrestart)
        {
            ModelInvokeResult<UserPK> result = new ModelInvokeResult<UserPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _StationId = strStationId.ToGuid();//商家的默认管理员UserId是商家的StationId
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                /***********************取社会组织下的所有用户*******************/
                var param = new { StationId = _StationId, StopFlag = (stopOrrestart == "stop" ? 1 : 0), Status = (stopOrrestart == "stop" ? 0 : 1), OperatedBy = NormalSession.UserId.ToGuid() }.ToStringObjectDictionary();
                statements.Add(new IBatisNetBatchStatement { StatementName = "UpdateUserForAgencyBatch", ParameterObject = param, Type = SqlExecuteType.UPDATE });
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                SPParam spParam = new { }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceUser", spParam);
                 
                /***********************社会组织是什么类型*******************/
                var ret = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery("select StationType,StationType2 from Pub_ServiceStation where  StationId='" + _StationId + "' ").ToArray();

                /***********************作废社会组织下的所有工作计划项和删除未完成的工作执行项*******************/
                if (stopOrrestart == "stop" && ret[0]["StationType2"].ToString()=="00001")
                {
                    param = new { StationId = _StationId}.ToStringObjectDictionary();
                    statements.Add(new IBatisNetBatchStatement { StatementName = "DeleteUndoWorkPlan", ParameterObject = param, Type = SqlExecuteType.DELETE });
                    statements.Add(new IBatisNetBatchStatement { StatementName = "DeleteUndoWorkExecute", ParameterObject = param, Type = SqlExecuteType.DELETE });
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);                 
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

        #region 查看社会组织是否已经有用户存在  QueryUserOfAgency
        private string QueryUserOfAgency(string stationId)
        {
            string result = "";
            try
            {
                CollectionInvokeResult<PensionAgencyUser> ret = new CollectionInvokeResult<PensionAgencyUser> { Success = true };
                var param = new { StationId = stationId }.ToStringObjectDictionary();
                ret.rows = BuilderFactory.DefaultBulder().List<PensionAgencyUser>(param);//查出这个社会组织下的所有用户
                var count = ret.rows.Count;
                if (count > 0)
                {
                    var flog = true;
                    //把所有用户都重新启用，如果启用失败   则返回false
                    if (!UpdateUsersOfAgency(stationId, "restart").Success)
                    {
                        flog = false;
                    };
                    result = flog == true ? "账号启用成功" : "账号启用失败";
                }
                else
                {
                    result = "";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的列表 PensionAgencyListForEasyUIgridByStationId 根据社会组织Id查询用户
        [WebInvoke(UriTemplate = "PensionAgencyListForEasyUIgridByStationId/{stationId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> PensionAgencyListForEasyUIgridByStationId(string stationId, EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();
                User user = new User();
                if (param.filterFields != null)
                { 
                    foreach (var field in param.filterFields)
                    {
                        if (field.key == "UserType")
                        {
                            user.UserType = field.value;
                        }
                        else
                        {
                            filters[field.key] = field.value;
                        }
                    }
                    List<string> groupIds = new List<string>();
                    if (user.isNormalUser(groupIds) || user.isAgencyUser(groupIds))
                    {
                        if (groupIds.Count > 0)
                        {
                            whereClause.Add(" a.UserId in (select UserId from Pub_GroupMember where GroupId in ('" + string.Join("','", groupIds.ToArray()) + "') and a.UserId in (select UserId from Pub_PensionAgencyUser where StationId='" + stationId + "'))");
                        }
                    } 
                }

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

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("PensionAgencyListForEasyUIgridByStationId", filters, param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 授权查询机构下的所有用户
        [WebGet(UriTemplate = "GetAllUsersOfPensionAgency/{stationId},{areaId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetAllUsersOfPensionAgency(string stationId, string areaId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                filters["StationId"] = stationId;
                filters["AreaId"] = areaId;
                filters["OrderByClause"] = " g.GroupName asc ";
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetAllUsers_PensionAgency", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取服务人员
        [WebGet(UriTemplate = "ListServeManForAgency/{stationId},{userType},{groupId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<User> ListServeManForAgency(string stationId, string userType, string groupId)
        {
            CollectionInvokeResult<User> result = new CollectionInvokeResult<User> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<User>("User_ListAsServeManForAgency", new { StationId = stationId, UserType = userType, GroupId = groupId }.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 键入即时获取服务人员
        [WebInvoke(UriTemplate = "ListServeManForInput_Agencys/{groupId},{stationId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<User> ListServeManForInput_Agencys(string groupId, string stationId, EasyUIgridCollectionParam<User> param)
        {
            EasyUIgridCollectionInvokeResult<User> result = new EasyUIgridCollectionInvokeResult<User> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }
                string whereClause = "UserType='" + filters["UserType"] + "' and  UserId in (select a.UserId from Pub_PensionAgencyUser a inner join Pub_GroupMember b on a.UserId=b.UserId where a.StationId='" + stationId + "' and b.GroupId='" + groupId + "') and ( UserName like '" + filters["UserName"] + "%' or UserCode like '" + filters["UserName"] + "%')";
                result.rows = BuilderFactory.DefaultBulder().List<User>("User_ListAsServeManForInput", new { WhereClause = whereClause }.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取工作计划
        [WebInvoke(UriTemplate = "ListWorkPlan", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListWorkPlan(EasyUIgridCollectionParam<ListWorkPlanParam> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { StationId = param.instance.StationId}.ToStringObjectDictionary();
                if (param.instance.RoomId.ToString() != "" && param.instance.RoomId != null)
                {
                    filters.Add("RoomId", param.instance.RoomId.ToString());
                }
                if (param.instance.FloorNo != "" && param.instance.FloorNo != null)
                {
                    filters.Add("FloorNo", param.instance.FloorNo.ToString());
                }
                DateTime beginDate = DateTime.Parse(param.instance.BeginDateStr);
                DateTime endDate = DateTime.Parse(param.instance.EndDateStr);
                List<string> thePivotColumnsForSelect = new List<string>();
                List<string> thePivotColumns = new List<string>();
                TimeSpan ts = endDate - beginDate;
                DateTime dt = beginDate;
                for (int i = 0; i < ts.Days; i++)
                {
                    thePivotColumnsForSelect.Add(string.Format("[{0}] as _{0}", dt.ToString("yyyyMMdd")));
                    thePivotColumns.Add(dt.ToString("yyyyMMdd"));
                    dt = dt.AddDays(1);
                }
                thePivotColumnsForSelect.Add(string.Format("[{0}] as _{0}", dt.ToString("yyyyMMdd")));
                thePivotColumns.Add(dt.ToString("yyyyMMdd"));
                filters.Add("PivotColumnsForSelect", string.Join(",", thePivotColumnsForSelect.ToArray()));
                filters.Add("PivotColumns", "[" + string.Join("],[", thePivotColumns.ToArray()) + "]"); 
                if (param.instance.OldManName != "" && param.instance.OldManName != null)
                {
                    filters.Add("OldManName", "OldManName like '" + param.instance.OldManName + "%'");
                }

                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("WorkPlanListByAgency", filters);
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



    

    public class SetServeItemsAndServeModesParam
    {
        public IList<string> ServeItemB { get; set; }//逗号隔开的多值
        public IList<string> ServeFee { get; set; }//逗号隔开的多值
        public IList<string> ServeFeeRemarks { get; set; }//逗号隔开的多值
        public IList<string> ServeMode { get; set; }//逗号隔开的多值
    }

    public class AddMerchantServePeriodParam
    {
        public Guid? StationId { get; set; }
        public string EndDate { get; set; }
    }

    public class ListScheduleParam
    {
        public Guid? StationId { get; set; }
        public Guid? AreaId { get; set; }
        public Guid? ServeManId { get; set; }
        public string BeginDateStr { get; set; }
        public string EndDateStr { get; set; }
        public bool OnlyShowGovTurnkey { get; set; }
        public string OldManName { get; set; }
    }

    public class ListCheckInParam
    {
        public Guid? StationId { get; set; } 
        public string BeginDateStr { get; set; }
        public string EndDateStr { get; set; } 
    }
    public class ListWorkPlanParam
    {
        public Guid? StationId { get; set; }
        public Guid? RoomId { get; set; }
        public string FloorNo { get; set; }
        public string BeginDateStr { get; set; }
        public string EndDateStr { get; set; } 
        public string OldManName { get; set; }
    }

}