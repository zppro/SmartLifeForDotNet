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
using SmartLife.Share.Models.Bas;

namespace SmartLife.CertManage.ManageServices.Bas
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ResidentIDNoRequisitionService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ResidentIDNoRequisition> List()
        {
            CollectionInvokeResult<ResidentIDNoRequisition> result = new CollectionInvokeResult<ResidentIDNoRequisition> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ResidentIDNoRequisition>();
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
        public CollectionInvokeResult<ResidentIDNoRequisition> Query(string strParms)
        {
            CollectionInvokeResult<ResidentIDNoRequisition> result = new CollectionInvokeResult<ResidentIDNoRequisition> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ResidentIDNoRequisition>(dictionary);
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
        public JqgridCollectionInvokeResult<ResidentIDNoRequisition> ListForJqgrid(JqgridCollectionParam<ResidentIDNoRequisition> param)
        {
            JqgridCollectionInvokeResult<ResidentIDNoRequisition> result = new JqgridCollectionInvokeResult<ResidentIDNoRequisition> { Success = true };
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

                gridCollectionPager.JqgridDoPage<ResidentIDNoRequisition>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ResidentIDNoRequisition>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<ResidentIDNoRequisition> ListForEasyUIgrid(EasyUIgridCollectionParam<ResidentIDNoRequisition> param)
        {
            EasyUIgridCollectionInvokeResult<ResidentIDNoRequisition> result = new EasyUIgridCollectionInvokeResult<ResidentIDNoRequisition> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<ResidentIDNoRequisition>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ResidentIDNoRequisition>(filters), param, ref result);
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
        public ModelInvokeResult<ResidentIDNoRequisitionPK> Create(ResidentIDNoRequisition residentIDNoRequisition)
        {
            ModelInvokeResult<ResidentIDNoRequisitionPK> result = new ModelInvokeResult<ResidentIDNoRequisitionPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (residentIDNoRequisition.RequisitionId == GlobalManager.GuidAsAutoGenerate)
                {
                    residentIDNoRequisition.RequisitionId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                residentIDNoRequisition.OperatedBy = NormalSession.UserId.ToGuid();
                residentIDNoRequisition.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                residentIDNoRequisition.SubmitBy = NormalSession.UserId.ToGuid();
                residentIDNoRequisition.SubmitOn = DateTime.Now;
                statements.Add(new IBatisNetBatchStatement { StatementName = residentIDNoRequisition.GetCreateMethodName(), ParameterObject = residentIDNoRequisition.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentIDNoRequisitionPK { RequisitionId = residentIDNoRequisition.RequisitionId };
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
        public ModelInvokeResult<ResidentIDNoRequisitionPK> Update(string strRequisitionId, ResidentIDNoRequisition residentIDNoRequisition)
        {
            ModelInvokeResult<ResidentIDNoRequisitionPK> result = new ModelInvokeResult<ResidentIDNoRequisitionPK> { Success = true };
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
                residentIDNoRequisition.RequisitionId = _RequisitionId;
                /***********************begin 自定义代码*******************/
                residentIDNoRequisition.OperatedBy = NormalSession.UserId.ToGuid();
                residentIDNoRequisition.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = residentIDNoRequisition.GetUpdateMethodName(), ParameterObject = residentIDNoRequisition.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentIDNoRequisitionPK { RequisitionId = _RequisitionId };

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
        public ModelInvokeResult<ResidentIDNoRequisitionPK> Delete(string strRequisitionId)
        {
            ModelInvokeResult<ResidentIDNoRequisitionPK> result = new ModelInvokeResult<ResidentIDNoRequisitionPK> { Success = true };
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
                ResidentIDNoRequisitionPK pk = new ResidentIDNoRequisitionPK { RequisitionId = _RequisitionId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new ResidentIDNoRequisition().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentIDNoRequisitionPK { RequisitionId = _RequisitionId };
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
                string statementName = new ResidentIDNoRequisition().GetDeleteMethodName();
                foreach (string strRequisitionId in arrRequisitionIds)
                {
                    ResidentIDNoRequisitionPK pk = new ResidentIDNoRequisitionPK { RequisitionId = strRequisitionId.ToGuid() };
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
        public ModelInvokeResult<ResidentIDNoRequisitionPK> Nullify(string strRequisitionId)
        {
            ModelInvokeResult<ResidentIDNoRequisitionPK> result = new ModelInvokeResult<ResidentIDNoRequisitionPK> { Success = true };
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
                ResidentIDNoRequisition residentIDNoRequisition = new ResidentIDNoRequisition { RequisitionId = _RequisitionId };
                /***********************begin 自定义代码*******************/
                residentIDNoRequisition.OperatedBy = NormalSession.UserId.ToGuid();
                residentIDNoRequisition.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = residentIDNoRequisition.GetUpdateMethodName(), ParameterObject = residentIDNoRequisition.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentIDNoRequisitionPK { RequisitionId = _RequisitionId };
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
                string statementName = new ResidentIDNoRequisition().GetUpdateMethodName();
                foreach (string strRequisitionId in arrRequisitionIds)
                {
                    ResidentIDNoRequisition residentIDNoRequisition = new ResidentIDNoRequisition { RequisitionId = strRequisitionId.ToGuid()};
                    /***********************begin 自定义代码*******************/
                    residentIDNoRequisition.OperatedBy = NormalSession.UserId.ToGuid();
                    residentIDNoRequisition.OperatedOn = DateTime.Now;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = residentIDNoRequisition.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ResidentIDNoRequisitionPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strRequisitionId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ResidentIDNoRequisition> Load(string strRequisitionId)
        {
            ModelInvokeResult<ResidentIDNoRequisition> result = new ModelInvokeResult<ResidentIDNoRequisition> { Success = true };
            try
            {
                Guid? _RequisitionId = strRequisitionId.ToGuid();
                if (_RequisitionId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var residentIDNoRequisition = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<ResidentIDNoRequisition, ResidentIDNoRequisitionPK>(new ResidentIDNoRequisitionPK { RequisitionId = _RequisitionId });
                result.instance = residentIDNoRequisition;
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

        #region 自定义

        #region 居民变更身份证号EasyUIgrid数据格式的列表分页查询(权限)
        [WebInvoke(UriTemplate = "RequisitionListForEasyUIgrid/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> RequisitionListForEasyUIgrid(string connectId, EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                        }
                        else if (field.key.Contains("DoStatus_Start"))
                        {
                            filters["DoStatus"] = field.value;
                        }
                        else
                        {
                            filters[field.key] = field.value;
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
                        else if (field.key.Contains("Status"))
                        {
                            whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
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


                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ResidentIDNoRequisitionAndDictionaryItem_ListByPage", filters, param, ref result, connectId);
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

        #region 审批是否通过
        [WebInvoke(UriTemplate = "IsRequisitionPass/{strRequisitionId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ResidentIDNoRequisitionPK> IsRequisitionPass(string strRequisitionId, ResidentIDNoRequisition residentIDNoRequisition)
        {
            ModelInvokeResult<ResidentIDNoRequisitionPK> result = new ModelInvokeResult<ResidentIDNoRequisitionPK> { Success = true };
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
                residentIDNoRequisition.RequisitionId = _RequisitionId;
                /***********************begin 自定义代码*******************/
                residentIDNoRequisition.OperatedBy = NormalSession.UserId.ToGuid();
                residentIDNoRequisition.OperatedOn = DateTime.Now;
                residentIDNoRequisition.ConfirmBy = NormalSession.UserId.ToGuid();
                residentIDNoRequisition.ConfirmOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = residentIDNoRequisition.GetUpdateMethodName(), ParameterObject = residentIDNoRequisition.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                if (residentIDNoRequisition.DoStatus == 1)
                {
                    /***********************begin 更改区级库的身份证号码*******************/
                    List<IBatisNetBatchStatement> statements_oldManBaseInfo = new List<IBatisNetBatchStatement>();
                    OldManBaseInfo oldManBaseInfo = new OldManBaseInfo();
                    oldManBaseInfo.OldManId = residentIDNoRequisition.ResidentId;
                    oldManBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                    oldManBaseInfo.OperatedOn = DateTime.Now;
                    oldManBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    oldManBaseInfo.IDNo = residentIDNoRequisition.IDNoNew;
                    statements_oldManBaseInfo.Add(new IBatisNetBatchStatement { StatementName = oldManBaseInfo.GetUpdateMethodName(), ParameterObject = oldManBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements_oldManBaseInfo);
                    /***********************end 更改区级库的身份证号码*********************/

                    /***********************begin 更改城市库的身份证号码*******************/
                    List<IBatisNetBatchStatement> statements_residentBaseInfo = new List<IBatisNetBatchStatement>();
                    ResidentBaseInfo residentBaseInfo = new ResidentBaseInfo();
                    residentBaseInfo.ResidentId = residentIDNoRequisition.ResidentId;
                    residentBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                    residentBaseInfo.OperatedOn = DateTime.Now;
                    residentBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    residentBaseInfo.IDNo = residentIDNoRequisition.IDNoNew;
                    statements_residentBaseInfo.Add(new IBatisNetBatchStatement { StatementName = residentBaseInfo.GetUpdateMethodName(), ParameterObject = residentBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                    BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements_residentBaseInfo);
                    /***********************end 更改城市库的身份证号码*********************/

                }
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);

                /***********************begin 添加居民活动信息*******************/   
                string sql_str=" insert into Bas_ResidentActivityLog (CheckInTime,ResidentId,ActivityTime,ActivityType,ActivityData,ActivityDescription,ActivityChangeType)"
                               +" select  GETDATE() CheckInTime,a.ResidentId,GETDATE() ActivityTime,'00001' ActivityType"
                               +",'{\"Source\":\"07\",\"from\":\"07\",\"to\":\"07\",\"Operate\":\"变更身份证\",\"NewIDNo\":\"'+CAST( IDNoNew as varchar(20))+'\",\"OldIDNo\":\"'+CAST( IDNoOld as varchar(20))+'\",\"OperatedBy\":\"'+ISNULL(CAST( ConfirmBy as varchar(36)),'')+'\",\"AreaId\":\"'+CAST( b.AreaId as varchar(36))+'\",\"DoStatus\":\"" + residentIDNoRequisition.DoStatus + "\"}' ActivityData"
                               + ",'变更身份证:结果\"" + (residentIDNoRequisition.DoStatus == 1 ? "通过" : "不通过") + "\",原身份证\"'+IDNoOld+'\"变更为\"'+IDNoNew+'\",\"所在辖区\":\"'+ItemName+'\"' ActivityDescription,'00001' ActivityChangeType"
                               +" from Bas_ResidentIDNoRequisition a inner join Bas_ResidentBaseInfo b on a.ResidentId=b.ResidentId   inner join Sys_DictionaryItem c on b.AreaId=c.ItemId"
                               + " where c.DictionaryId='00005' and RequisitionId='" + strRequisitionId + "'";
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(sql_str);
                /***********************end 添加居民活动信息*******************/

                result.instance = new ResidentIDNoRequisitionPK { RequisitionId = _RequisitionId };
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