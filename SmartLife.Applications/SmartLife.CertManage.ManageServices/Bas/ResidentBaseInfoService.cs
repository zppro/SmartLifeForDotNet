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
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ResidentBaseInfoService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ResidentBaseInfo> List()
        {
            CollectionInvokeResult<ResidentBaseInfo> result = new CollectionInvokeResult<ResidentBaseInfo> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ResidentBaseInfo>();
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
        public CollectionInvokeResult<ResidentBaseInfo> Query(string strParms)
        {
            CollectionInvokeResult<ResidentBaseInfo> result = new CollectionInvokeResult<ResidentBaseInfo> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ResidentBaseInfo>(dictionary);
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
        public JqgridCollectionInvokeResult<ResidentBaseInfo> ListForJqgrid(JqgridCollectionParam<ResidentBaseInfo> param)
        {
            JqgridCollectionInvokeResult<ResidentBaseInfo> result = new JqgridCollectionInvokeResult<ResidentBaseInfo> { Success = true };
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

                gridCollectionPager.JqgridDoPage<ResidentBaseInfo>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ResidentBaseInfo>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<ResidentBaseInfo> ListForEasyUIgrid(EasyUIgridCollectionParam<ResidentBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<ResidentBaseInfo> result = new EasyUIgridCollectionInvokeResult<ResidentBaseInfo> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<ResidentBaseInfo>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ResidentBaseInfo>(filters), param, ref result);
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
        public ModelInvokeResult<ResidentBaseInfoPK> Create(ResidentBaseInfo residentBaseInfo)
        {
            ModelInvokeResult<ResidentBaseInfoPK> result = new ModelInvokeResult<ResidentBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (residentBaseInfo.ResidentId == GlobalManager.GuidAsAutoGenerate)
                {
                    residentBaseInfo.ResidentId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                residentBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                residentBaseInfo.OperatedOn = DateTime.Now;
                residentBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                residentBaseInfo.InputCode1 = GetInputCode(residentBaseInfo.ResidentName, "py");
                residentBaseInfo.InputCode2 = GetInputCode(residentBaseInfo.ResidentName, "wb");
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = residentBaseInfo.GetCreateMethodName(), ParameterObject = residentBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentBaseInfoPK { ResidentId = residentBaseInfo.ResidentId };
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
        public ModelInvokeResult<ResidentBaseInfoPK> Update(string strResidentId, ResidentBaseInfo residentBaseInfo)
        {
            ModelInvokeResult<ResidentBaseInfoPK> result = new ModelInvokeResult<ResidentBaseInfoPK> { Success = true };
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
                residentBaseInfo.ResidentId = _ResidentId;
                /***********************begin 自定义代码*******************/
                residentBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                residentBaseInfo.OperatedOn = DateTime.Now;
                residentBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                residentBaseInfo.InputCode1 = GetInputCode(residentBaseInfo.ResidentName, "py");
                residentBaseInfo.InputCode2 = GetInputCode(residentBaseInfo.ResidentName, "wb");
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = residentBaseInfo.GetUpdateMethodName(), ParameterObject = residentBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentBaseInfoPK { ResidentId = _ResidentId };

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
        public ModelInvokeResult<ResidentBaseInfoPK> Delete(string strResidentId)
        {
            ModelInvokeResult<ResidentBaseInfoPK> result = new ModelInvokeResult<ResidentBaseInfoPK> { Success = true };
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
                ResidentBaseInfoPK pk = new ResidentBaseInfoPK { ResidentId = _ResidentId };
                //DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new ResidentBaseInfo().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentBaseInfoPK { };
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
                string statementName = new ResidentBaseInfo().GetDeleteMethodName();
                foreach (string strResidentId in arrResidentIds)
                {
                    ResidentBaseInfoPK pk = new ResidentBaseInfoPK { ResidentId = strResidentId.ToGuid() };
                    //DeleteCascade(statements, pk);
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
        [WebInvoke(UriTemplate = "Nullify/{strResidentId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ResidentBaseInfoPK> Nullify(string strResidentId)
        {
            ModelInvokeResult<ResidentBaseInfoPK> result = new ModelInvokeResult<ResidentBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                List<IBatisNetBatchStatement> statements_ = new List<IBatisNetBatchStatement>();
                Guid? _ResidentId = strResidentId.ToGuid();
                if (_ResidentId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                ResidentBaseInfo residentBaseInfo = new ResidentBaseInfo { ResidentId = _ResidentId, Status = 0 };
                /***********************begin 自定义代码*******************/
                residentBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                residentBaseInfo.OperatedOn = DateTime.Now;
                residentBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = residentBaseInfo.GetUpdateMethodName(), ParameterObject = residentBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);


                /*********************************作废区级库数据****************************************/
                OldManBaseInfo oldManBaseInfo = new OldManBaseInfo { OldManId = _ResidentId, Status = 0 };
                /***********************begin 自定义代码*******************/
                oldManBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                oldManBaseInfo.OperatedOn = DateTime.Now;
                oldManBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements_.Add(new IBatisNetBatchStatement { StatementName = oldManBaseInfo.GetUpdateMethodName(), ParameterObject = oldManBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements_);
                /*********************************作废区级库数据****************************************/
                result.instance = new ResidentBaseInfoPK { };
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
                List<IBatisNetBatchStatement> statements_ = new List<IBatisNetBatchStatement>();
                string[] arrResidentIds = strResidentIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrResidentIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ResidentBaseInfo().GetUpdateMethodName();
                string statementName_ = new OldManBaseInfo().GetUpdateMethodName();
                foreach (string strResidentId in arrResidentIds)
                {
                    ResidentBaseInfo residentBaseInfo = new ResidentBaseInfo { ResidentId = strResidentId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    ResidentBaseInfoPK pk = new ResidentBaseInfoPK { ResidentId = strResidentId.ToGuid() };
                    DeleteCascade(statements, pk, "update");
                    residentBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                    residentBaseInfo.OperatedOn = DateTime.Now;
                    residentBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = residentBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                    /*********************************作废区级库数据****************************************/

                    OldManBaseInfo oldManBaseInfo = new OldManBaseInfo { OldManId = strResidentId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    OldManBaseInfoPK pk_ = new OldManBaseInfoPK { OldManId = strResidentId.ToGuid() };
                    DeleteCascade_(statements_, pk_, "update");
                    oldManBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                    oldManBaseInfo.OperatedOn = DateTime.Now;
                    oldManBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements_.Add(new IBatisNetBatchStatement { StatementName = statementName_, ParameterObject = oldManBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                    /*********************************作废区级库数据****************************************/

                }
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements_);
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ResidentBaseInfoPK pk, string type)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region (区级库)级联 作废、删除扩展接口 DeleteCascade_
        private void DeleteCascade_(List<IBatisNetBatchStatement> statements, OldManBaseInfoPK pk, string type)
        {
            if ("delete".Equals(type))
            {
                string statementName = new OldManConfigInfo().GetDeleteMethodName();
                statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pk, Type = SqlExecuteType.DELETE });
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
            }
            if ("update".Equals(type))
            {
                string statementName = new OldManConfigInfo().GetUpdateMethodName();
                // ModelInvokeResult < OldManConfigInfo > oldManConfigInfo =  new OldManConfigInfoService().Load(pk.OldManId.ToString());
                OldManConfigInfo oldManConfigInfo = new OldManConfigInfo { OldManId = pk.OldManId };
                oldManConfigInfo.OperatedBy = NormalSession.UserId.ToGuid();
                oldManConfigInfo.OperatedOn = DateTime.Now;
                oldManConfigInfo.CallNo = "";
                oldManConfigInfo.CallNo2 = "";
                oldManConfigInfo.CallNo3 = "";
                statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = oldManConfigInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
            }
            BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
        }
        #endregion


        #region 载入 Load
        [WebGet(UriTemplate = "{strResidentId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ResidentBaseInfo> Load(string strResidentId)
        {
            ModelInvokeResult<ResidentBaseInfo> result = new ModelInvokeResult<ResidentBaseInfo> { Success = true };
            try
            {
                Guid? _ResidentId = strResidentId.ToGuid();
                if (_ResidentId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var residentBaseInfo = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<ResidentBaseInfo, ResidentBaseInfoPK>(new ResidentBaseInfoPK { ResidentId = _ResidentId });
                result.instance = residentBaseInfo;
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
        public string GetInputCode(string nameStr,string tpye) 
        {
            string result=null;
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

        #region 自定义业务接口

        #region 居民基本信息EasyUIgrid数据格式的列表(非权限)
        [WebInvoke(UriTemplate = "ResidentBaseInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ResidentBaseInfo> ResidentBaseInfo(EasyUIgridCollectionParam<ResidentBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<ResidentBaseInfo> result = new EasyUIgridCollectionInvokeResult<ResidentBaseInfo> { Success = true };
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
                gridCollectionPager.EasyUIgridDoPage<ResidentBaseInfo>(BuilderFactory.DefaultBulder("SmartLife1203").List<ResidentBaseInfo>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 居民基本信息EasyUIgrid数据格式的列表(权限)
        [WebInvoke(UriTemplate = "ResidentBaseInfo2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ResidentBaseInfo> ResidentBaseInfo2(EasyUIgridCollectionParam<ResidentBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<ResidentBaseInfo> result = new EasyUIgridCollectionInvokeResult<ResidentBaseInfo> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }
                List<string> whereClause = new List<string>();

                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        filters[field.key] = field.value;
                    }
                }
                if (param.fuzzyFields != null)
                {
                    foreach (var field in param.fuzzyFields)
                    {
                        whereClause.Add(string.Format("(AreaId3 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", field.key, field.value));
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
                /***********************begin 排序*************************/
                /**********************************************************/
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                /***********************end 排序***************************/
                gridCollectionPager.EasyUIgridDoPage4Model<ResidentBaseInfo>(filters, param, ref result, GetHttpHeader("ConnectId"));
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 居民基本信息判断查询的是否为超级管理员(权限)
        [WebInvoke(UriTemplate = "isSuperAdmin/{UserId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public Boolean isSuperAdmin(string UserId)
        {
            Boolean result = false;
            User currentUser = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };
            if (currentUser.isSuperAdmin())
            {
                result = true;
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

        #region 居民基本信息EasyUIgrid数据格式的列表分页查询(权限)
        [WebInvoke(UriTemplate = "ResidentBaseInfo3/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ResidentBaseInfo3(string connectId, EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                        else if (field.key.Contains ("ResidentStatus_Start"))
                        {
                            filters["ResidentStatus"] = field.value;
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
                                //if (field.value == "020")
                                //{
                                //    whereClause2.Add("FlowFrom between 21 and 29");
                                //}
                                //else if (field.value == "030")
                                //{
                                //    whereClause2.Add("FlowFrom between 31 and 39");
                                //    //修改status
                                //    if (filters.ContainsKey("Status"))
                                //    {
                                //        filters.Remove("Status");
                                //        whereClause.Add("Status in ('1','2')");
                                //    }
                                //}
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

                 
                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ResidentBaseInfoAndDictionaryItem_ListByPage", filters, param, ref result, connectId);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 居民基本信息EasyUIgrid数据格式的列表分页查询(权限)
        [WebInvoke(UriTemplate = "ResidentBaseInfo33", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ResidentBaseInfo33( EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                        else if (field.key.Contains("ResidentStatus_Start"))
                        {
                            filters["ResidentStatus"] = field.value;
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
                                //if (field.value == "020")
                                //{
                                //    whereClause2.Add("FlowFrom between 21 and 29");
                                //}
                                //else if (field.value == "030")
                                //{
                                //    whereClause2.Add("FlowFrom between 31 and 39");
                                //    //修改status
                                //    if (filters.ContainsKey("Status"))
                                //    {
                                //        filters.Remove("Status");
                                //        whereClause.Add("Status in ('1','2')");
                                //    }
                                //}
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


                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ResidentBaseInfoAndDictionaryItem_ListByPage", filters, param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 同步基本信息
        [WebInvoke(UriTemplate = "SynBaseInfo/{Phase}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SynBaseInfo(string Phase)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                SPParam spParam = new { }.ToSPParam();
                spParam["Phase"] = Phase;
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_Aync_Oca_OldManBaseInfo", spParam);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 同步上传基本信息
        [WebInvoke(UriTemplate = "SynUploadBaseInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SynUploadBaseInfo()
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                SPParam spParam = new { }.ToSPParam();
                spParam["bckTableName"] = "Bas_ResidentBaseInfoOld" + DateTime.Now.ToString("yyyyMMddHHmm");
                spParam["Phase"] = 2;
                spParam["iType"] = 0;
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_ImportResidentBaseInfo3", spParam);
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_Aync_Oca_OldManBaseInfo", spParam);
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_ImportResidentExtInfo2", spParam);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region UpdateBaseResidents 部分批量修改状态 (用于审批后更新已选择的状态)
        [WebInvoke(UriTemplate = "UpdateBaseResidents/{_status},{ids}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UpdateBaseResidents(string _status, string ids)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrResidentIds = ids.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrResidentIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ResidentBaseInfo().GetUpdateMethodName();
                foreach (string strResidentId in arrResidentIds)
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = new { ResidentId = strResidentId.ToGuid(), Status = _status, OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now }, Type = SqlExecuteType.UPDATE });
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
        [WebInvoke(UriTemplate = "UpdateBaseResidentsAll", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
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

                if (!string.IsNullOrEmpty(flowAction.WhereClause))
                {
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



        #region 验证身份证号是否有效 
        [WebGet(UriTemplate = "ResidentsIDNoDuplicates?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> ResidentsIDNoDuplicates(string strParms)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms); 
                var param = "";
                param = "select * from Bas_ResidentBaseInfo where Status >" + dictionary["Status"] + " and IDNo='" + dictionary["IDNo"] + "'";
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

        #region 死亡处理 ResidentDestroy
        [WebInvoke(UriTemplate = "ResidentDestroy/{residentId},{areaId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<ResidentBaseInfo> ResidentDestroy(string residentId, string areaId)
        {
            InvokeResult<ResidentBaseInfo> result = new InvokeResult<ResidentBaseInfo> { Success = true, ret = new ResidentBaseInfo() };
            try
            {
                //var spParam = param.ToSPParam();
                SPParam spParam = new { }.ToSPParam();
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                spParam["ResidentId"] = Guid.Parse(residentId);
                spParam["AreaId"] = areaId;
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_ResidentDestroy", spParam);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 迁出处理(可批量操作) ResidentMigrate
        [WebInvoke(UriTemplate = "ResidentMigrate", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<ResidentMigrateLog> ResidentMigrate(ResidentMigrates param)
        {
            InvokeResult<ResidentMigrateLog> result = new InvokeResult<ResidentMigrateLog> { Success = true,ret = new ResidentMigrateLog() };
            try
            { 
                var spParam = param.ToSPParam();
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_ResidentMigrate", spParam); 
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 撤销迁出、拒绝迁入(可批量操作) ResidentMigrateBack
        [WebInvoke(UriTemplate = "ResidentMigrateBack", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<StringObjectDictionary> ResidentMigrateBack(ResidentMigrates param)
        {
            InvokeResult<StringObjectDictionary> result = new InvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                var spParam = param.ToSPParam();
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_ResidentMigrateBack", spParam);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion 

        #region 确认、允许迁入，进入审批流程(可批量操作) ResidentMigrateConfirm
        [WebInvoke(UriTemplate = "ResidentMigrateConfirm", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<ResidentMigrateLog> ResidentMigrateConfirm(ResidentMigrates param)
        {
            InvokeResult<ResidentMigrateLog> result = new InvokeResult<ResidentMigrateLog> { Success = true, ret = new ResidentMigrateLog() };
            try
            {
                var spParam = param.ToSPParam();
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_ResidentMigrateConfirm", spParam);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion 

        #region 已迁出的老人信息查询(权限)
        [WebInvoke(UriTemplate = "ResidentBaseInfoMigrated/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ResidentBaseInfoMigrated(string connectId, EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();
                List<string> whereClause2 = new List<string>();
                bool flog_status = true ;

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
                        else if (field.key.Contains("MigrateResultFlag"))
                        {
                            if (field.value=="11")
                            {
                                whereClause.Add(" MigrateResultFlag is not null ");
                            }
                            else if (field.value == "0")
                            {
                                whereClause.Add(" MigrateResultFlag is null ");
                            }
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
                        if (field.key.IndexOf("AreaId") > -1 && field.value != "" && field.value != null)
                        {
                            if (field.key.IndexOf("From") > -1 && field.value != "" && field.value!=null)
                            {
                                whereClause.Add(string.Format("(AreaId3From in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2From in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", "AreaId", field.value));
                                flog_status = false;
                            }
                            else if (field.key.IndexOf("To") > -1 && field.value != "" && field.value != null)
                            {
                                whereClause.Add(string.Format("(AreaId3To in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2To in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", "AreaId", field.value));
                                flog_status = true;
                            }
                         }
                        else if (field.key.IndexOf("AreaId") > -1 && (field.value == "" || field.value == null))
                        {
                            flog_status = field.key.IndexOf("From") > -1 ? false : true;
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
                string sql = PermissionsCategoryMigratedView(flog_status);
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


                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ResidentBaseInfoAndDictionaryItemMigrated_ListByPage", filters, param, ref result, connectId);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 已迁出的老人信息查询(权限)
        [WebInvoke(UriTemplate = "ResidentBaseInfoMigrated2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ResidentBaseInfoMigrated2( EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();
                List<string> whereClause2 = new List<string>();
                bool flog_status = true;

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
                        else if (field.key.Contains("MigrateResultFlag"))
                        {
                            if (field.value == "11")
                            {
                                whereClause.Add(" MigrateResultFlag is not null ");
                            }
                            else if (field.value == "0")
                            {
                                whereClause.Add(" MigrateResultFlag is null ");
                            }
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
                        if (field.key.IndexOf("AreaId") > -1 && field.value != "" && field.value != null)
                        {
                            if (field.key.IndexOf("From") > -1 && field.value != "" && field.value != null)
                            {
                                whereClause.Add(string.Format("(AreaId3From in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2From in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", "AreaId", field.value));
                                flog_status = false;
                            }
                            else if (field.key.IndexOf("To") > -1 && field.value != "" && field.value != null)
                            {
                                whereClause.Add(string.Format("(AreaId3To in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2To in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", "AreaId", field.value));
                                flog_status = true;
                            }
                        }
                        else if (field.key.IndexOf("AreaId") > -1 && (field.value == "" || field.value == null))
                        {
                            flog_status = field.key.IndexOf("From") > -1 ? false : true;
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
                string sql = PermissionsCategoryMigratedView(flog_status);
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


                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ResidentBaseInfoAndDictionaryItemMigrated_ListByPage", filters, param, ref result, null);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 权限查看不同已迁出的内容
        public string PermissionsCategoryMigratedView(bool flog_status)
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
                    if (!flog_status)
                    {
                        if (areaIdsOfStreet == "")
                        {
                            sql = string.Format("( AreaId3From in ('{0}') or  (ISNULL(AreaId2From,'')='' AND ISNULL(AreaId3From,'')=''))", areaIdsOfCommunity);
                        }
                        else if (areaIdsOfCommunity == "")
                        {
                            sql = string.Format("(AreaId2From in ('{0}') or  (ISNULL(AreaId2From,'')='' AND ISNULL(AreaId3From,'')=''))", areaIdsOfStreet);
                        }
                        else
                        {
                            sql = string.Format("(AreaId2From in ('{0}') or AreaId3From in ('{1}') or (ISNULL(AreaId2From,'')='' AND ISNULL(AreaId3From,'')=''))", areaIdsOfStreet, areaIdsOfCommunity);
                        }
                    }
                    else
                    {
                        if (areaIdsOfStreet == "")
                        {
                            sql = string.Format("( AreaId3To in ('{0}') or  (ISNULL(AreaId2To,'')='' AND ISNULL(AreaId3To,'')=''))", areaIdsOfCommunity);
                        }
                        else if (areaIdsOfCommunity == "")
                        {
                            sql = string.Format("(AreaId2To in ('{0}') or  (ISNULL(AreaId2To,'')='' AND ISNULL(AreaId3To,'')=''))", areaIdsOfStreet);
                        }
                        else
                        {
                            sql = string.Format("(AreaId2To in ('{0}') or AreaId3To in ('{1}') or (ISNULL(AreaId2To,'')='' AND ISNULL(AreaId3To,'')=''))", areaIdsOfStreet, areaIdsOfCommunity);
                        }
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

        #region 机构

        #region 居民基本信息EasyUIgrid数据格式的列表分页查询(权限)
        [WebInvoke(UriTemplate = "AgencyResidentBaseInfoList/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> AgencyResidentBaseInfoList(string connectId, EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        filters[field.key] = field.value;
                    }
                }
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
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /***********************end 自定义代码*******************/


                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("AgencyResidentBaseInfo_ListByPage", filters, param, ref result, connectId);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 编辑 InsertUpdate
        [WebInvoke(UriTemplate = "AgencyResidentBaseInfo/{strResidentId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ResidentBaseInfoPK> AgencyResidentBaseInfo(string strResidentId, ResidentBaseInfo residentBaseInfo)
        {
            ModelInvokeResult<ResidentBaseInfoPK> result = new ModelInvokeResult<ResidentBaseInfoPK> { Success = true };
            try
            {
                bool bUpdate = true;//默认修改

                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ResidentId = strResidentId.ToGuid();
                if (_ResidentId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                //新增，否则修改
                if (_ResidentId == GlobalManager.GuidAsAutoGenerate) {
                    bUpdate = false;
                    _ResidentId = Guid.NewGuid();
                }
                residentBaseInfo.ResidentId = _ResidentId;
                /***********************begin 自定义代码*******************/
                residentBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                residentBaseInfo.OperatedOn = DateTime.Now;
                residentBaseInfo.DataSource = "00006";
                residentBaseInfo.AreaId2 = string.IsNullOrEmpty(residentBaseInfo.AreaId2) ? "AAAAA" : residentBaseInfo.AreaId2;
                residentBaseInfo.InputCode1 = GetInputCode(residentBaseInfo.ResidentName, "py");
                residentBaseInfo.InputCode2 = GetInputCode(residentBaseInfo.ResidentName, "wb");
                /***********************end 自定义代码*********************/
                if (bUpdate)
                {
                    //修改
                    statements.Add(new IBatisNetBatchStatement { StatementName = residentBaseInfo.GetUpdateMethodName(), ParameterObject = residentBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                else {
                    statements.Add(new IBatisNetBatchStatement { StatementName = residentBaseInfo.GetCreateMethodName(), ParameterObject = residentBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentBaseInfoPK { ResidentId = _ResidentId };

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 同步机构居民信息-旧版
        [WebInvoke(UriTemplate = "SynAgencyBaseInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SynAgencyBaseInfo(SynAgencyBase synAgencyBase)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                if (!string.IsNullOrEmpty(synAgencyBase.ResidentIds) && synAgencyBase.StationId != null)
                {
                    SPParam spParam = synAgencyBase.ToSPParam();
                    spParam["OperatedBy"] = NormalSession.UserId.ToGuid();
                    spParam["Delimeter"] = string.IsNullOrEmpty(synAgencyBase.Delimeter) ? "," : synAgencyBase.Delimeter;
                    BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_Aync_Pam_PensionAgencyObjectBaseInfo", spParam);
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

        #region 同步组织居民信息-新版
        [WebInvoke(UriTemplate = "SynOrganizationBaseInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SynOrganizationBaseInfo(SynOrgBaseInfo synOrgBaseInfo)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                if (!string.IsNullOrEmpty(synOrgBaseInfo.ResidentIds) && synOrgBaseInfo.StationId != null)
                {
                    SPParam spParam = synOrgBaseInfo.ToSPParam();
                    spParam["OperatedBy"] = NormalSession.UserId.ToGuid();
                    spParam["Delimeter"] = string.IsNullOrEmpty(synOrgBaseInfo.Delimeter) ? "," : synOrgBaseInfo.Delimeter;
                    BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_Aync_Org_OldManBaseInfo", spParam);
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

        #region 查看所有居民基本信息
        [WebInvoke(UriTemplate = "ResidentBaseInfoForActivityLog/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ResidentBaseInfoForActivityLog(string connectId, EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary(); 
                List<string> whereClause = new List<string>(); 
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        if (field.key.Contains("Gender_Start"))
                        {
                            if (field.value != "N" && field.value != "")
                            {
                                filters["Gender"] = field.value;
                            }
                        }
                        else
                        {
                            if (field.value != "")
                            {
                                filters[field.key] = field.value;
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

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ResidentBaseInfoForActivityLog_ListByPage", filters, param, ref result, connectId);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 居民基本信息变更情况
        [WebInvoke(UriTemplate = "ResidentActivityLog_ForEasyUIgrid/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ResidentActivityLog> ResidentActivityLog_ForEasyUIgrid(string connectId, EasyUIgridCollectionParam<ResidentActivityLog> param)
        {
            EasyUIgridCollectionInvokeResult<ResidentActivityLog> result = new EasyUIgridCollectionInvokeResult<ResidentActivityLog> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<ResidentActivityLog>(BuilderFactory.DefaultBulder(connectId).List<ResidentActivityLog>(filters), param, ref result);
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

        #region 象山客户端专用


        #region 创建 Create
        [WebInvoke(UriTemplate = "CreateInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ResidentBaseInfoPK> CreateInfo(ResidentBaseInfo residentBaseInfo)
        {
            ModelInvokeResult<ResidentBaseInfoPK> result = new ModelInvokeResult<ResidentBaseInfoPK> { Success = true };
            try
            {
                string connectId = GetHttpHeader("ConnectId");
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (residentBaseInfo.ResidentId == GlobalManager.GuidAsAutoGenerate)
                {
                    residentBaseInfo.ResidentId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/ 
                residentBaseInfo.OperatedOn = DateTime.Now; 
                residentBaseInfo.InputCode1 = GetInputCode(residentBaseInfo.ResidentName, "py");
                residentBaseInfo.InputCode2 = GetInputCode(residentBaseInfo.ResidentName, "wb");
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = residentBaseInfo.GetCreateMethodName(), ParameterObject = residentBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                //BuilderFactory.DefaultBulder(connectId).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentBaseInfoPK { ResidentId = residentBaseInfo.ResidentId };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion



        #region 居民基本信息列表分页查询
        [WebInvoke(UriTemplate = "GetResidentBaseInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> GetResidentBaseInfo(InputResidentBaseInfoParam baseInfoParam)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                List<string> whereClause = new List<string>();
                string connectId = GetHttpHeader("ConnectId");
                StringObjectDictionary param_filters = baseInfoParam.ToStringObjectDictionary(false);
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param_filters != null)
                {
                    foreach (var field in param_filters)
                    {
                        if (field.Key == "KeyWord")
                        {
                            whereClause.Add(string.Format("( ResidentName like '%{0}%' or IDNo like  '%{0}%'or Tel like  '%{0}%'or Mobile like '%{0}%' )", field.Value));
                        }
                        else if (field.Key == "AreaId" && field.Value.ToString() != "")
                        {
                            whereClause.Add(string.Format("(AreaId3 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", field.Key, field.Value));
                        }
                        else if (field.Key == "AreaId" && field.Value.ToString() == "")
                        {

                        } 
                    }
                }
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                EasyUIgridCollectionParam<StringObjectDictionary> param=new EasyUIgridCollectionParam<StringObjectDictionary>();
                param.order = baseInfoParam.Order;
                param.page = baseInfoParam.PageNo;
                param.rows = baseInfoParam.PageSize;
                param.sort = baseInfoParam.Sort;


                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ResidentBaseInfoAndDictionaryItem_ListByPage", filters, param, ref result, connectId);
                //result.rows = BuilderFactory.DefaultBulder(connectId).ListStringObjectDictionary("GetResidentBaseInfoForCS", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 验证身份证号是否有效
        [WebGet(UriTemplate = "IDNoDuplicatesForCS", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> IDNoDuplicatesForCS()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            { 
                string connectId = GetHttpHeader("ConnectId");
                string iDNo = GetHttpHeader("IDNo");
                string status = GetHttpHeader("Status");
                var param = "select * from Bas_ResidentBaseInfo where Status >" + status + " and IDNo='" + iDNo + "'";
                result.rows = BuilderFactory.DefaultBulder(connectId).ExecuteNativeSqlForQuery(param);
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

    public class SynOrgBaseInfo {
        public string ResidentIds { get; set; }
        public Guid? StationId { get; set; }
        public string Delimeter { get; set; }
        public int Atype { get; set; }
        public int RStatus { get; set; }
    }

    public class SynAgencyBase {
        public string ResidentIds { get; set; }
        public Guid? StationId { get; set; }
        public string Delimeter { get; set; }
        public int Atype { get; set; }
    }
    public class ResidentMigrates
    {
        #region 属性 
        /// <summary>
        /// 居民Ids
        /// </summary>
        public string ResidentIds { get; set; }
        /// <summary>
        /// 迁出的辖区
        /// </summary>
        public string AreaIdFrom { get; set; }
        /// <summary>
        /// 迁出的街道
        /// </summary>
        public string AreaId2From { get; set; }
        /// <summary>
        /// 迁出的社区
        /// </summary>
        public string AreaId3From { get; set; }
        /// <summary>
        /// 迁入的辖区
        /// </summary>
        public string AreaIdTo { get; set; }
        /// <summary>
        /// 迁入的街道
        /// </summary>
        public string AreaId2To { get; set; }
        /// <summary>
        /// 迁入的社区
        /// </summary>
        public string AreaId3To { get; set; } 
        /// <summary>
        /// null
        /// </summary>
        public string Remark { get; set; }
        #endregion

    }

    public class InputResidentBaseInfoParam {
        public string Order { get; set; }
        public string Sort { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string AreaId { get; set; }
        public string KeyWord { get; set; }
    }
     
}