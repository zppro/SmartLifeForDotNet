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

namespace SmartLife.CertManage.ManageServices.Oca
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServiceCallBackService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceCallBack> List()
        {
            CollectionInvokeResult<ServiceCallBack> result = new CollectionInvokeResult<ServiceCallBack> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<ServiceCallBack>();
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
        public CollectionInvokeResult<ServiceCallBack> Query(string strParms)
        {
            CollectionInvokeResult<ServiceCallBack> result = new CollectionInvokeResult<ServiceCallBack> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<ServiceCallBack>(dictionary);
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
        public JqgridCollectionInvokeResult<ServiceCallBack> ListForJqgrid(JqgridCollectionParam<ServiceCallBack> param)
        {
            JqgridCollectionInvokeResult<ServiceCallBack> result = new JqgridCollectionInvokeResult<ServiceCallBack> { Success = true };
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

                gridCollectionPager.JqgridDoPage<ServiceCallBack>(BuilderFactory.DefaultBulder().List<ServiceCallBack>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<ServiceCallBack> ListForEasyUIgrid(EasyUIgridCollectionParam<ServiceCallBack> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceCallBack> result = new EasyUIgridCollectionInvokeResult<ServiceCallBack> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<ServiceCallBack>(BuilderFactory.DefaultBulder().List<ServiceCallBack>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region EasyUIgrid数据格式的列表 ListForEasyUIgridByPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridByPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ServiceCallBack> ListForEasyUIgridByPage(EasyUIgridCollectionParam<ServiceCallBack> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceCallBack> result = new EasyUIgridCollectionInvokeResult<ServiceCallBack> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }
                List<string> whereClause = new List<string>();
                whereClause.Add(string.Format("(OperatedBy is null or OperatedBy='{0}')", NormalSession.UserId.ToGuid()));
                /**********************************************************/
                if (param.filterFields != null)
                {
                    DateTime parseTime = new DateTime();
                    foreach (var field in param.filterFields)
                    {
                        if (field.key.IndexOf('_') > -1 && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format(" {0} >= '{1}' ", field.key.Substring(0, field.key.IndexOf('_')), parseTime.ToString("yyyy-MM-dd HH:mm:ss")));
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
                
                gridCollectionPager.EasyUIgridDoPage4Model<ServiceCallBack>(filters, param, ref result); 
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridByPage2
        [WebInvoke(UriTemplate = "ListForEasyUIgridByPage2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridByPage2(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }
                List<string> whereClause = new List<string>();
                whereClause.Add(string.Format("(a.OperatedBy is null or a.OperatedBy='{0}')", NormalSession.UserId.ToGuid()));
                /**********************************************************/
                if (param.filterFields != null)
                {
                    DateTime parseTime = new DateTime();
                    foreach (var field in param.filterFields)
                    {
                        if (field.key.IndexOf('_') > -1 && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format(" b.{0} >= '{1}' ", field.key.Substring(0, field.key.IndexOf('_')), parseTime.ToString("yyyy-MM-dd HH:mm:ss")));
                        }
                        else if (field.key == "DoStatus_Start") {
                            whereClause.Add(string.Format(" a.{0} = '{1}' ", field.key.Substring(0, field.key.IndexOf('_')), field.value));
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
                        fuzzys.Add(string.Format("b.{0} like '%{1}%'", field.key, field.value));
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

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ServiceCallBack_ListByPage2", filters, param, ref result);
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
        public ModelInvokeResult<ServiceCallBackPK> Create(ServiceCallBack serviceCallBack)
        {
            ModelInvokeResult<ServiceCallBackPK> result = new ModelInvokeResult<ServiceCallBackPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (serviceCallBack.CallBackId == GlobalManager.GuidAsAutoGenerate)
                {
                    serviceCallBack.CallBackId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                serviceCallBack.OperatedBy = NormalSession.UserId.ToGuid();
                serviceCallBack.OperatedOn = DateTime.Now;
                serviceCallBack.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serviceCallBack.GetCreateMethodName(), ParameterObject = serviceCallBack.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceCallBackPK { CallBackId = serviceCallBack.CallBackId };
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
        [WebInvoke(UriTemplate = "{strCallBackId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceCallBackPK> Update(string strCallBackId, ServiceCallBack serviceCallBack)
        {
            ModelInvokeResult<ServiceCallBackPK> result = new ModelInvokeResult<ServiceCallBackPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _CallBackId = strCallBackId.ToGuid();
                if (_CallBackId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                serviceCallBack.CallBackId = _CallBackId;
                /***********************begin 自定义代码*******************/
                serviceCallBack.OperatedBy = NormalSession.UserId.ToGuid();
                serviceCallBack.OperatedOn = DateTime.Now;
                serviceCallBack.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serviceCallBack.GetUpdateMethodName(), ParameterObject = serviceCallBack.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceCallBackPK { CallBackId = _CallBackId };

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
        [WebInvoke(UriTemplate = "{strCallBackId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceCallBackPK> Delete(string strCallBackId)
        {
            ModelInvokeResult<ServiceCallBackPK> result = new ModelInvokeResult<ServiceCallBackPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _CallBackId = strCallBackId.ToGuid();
                if (_CallBackId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ServiceCallBackPK pk = new ServiceCallBackPK { CallBackId = _CallBackId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new ServiceCallBack().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceCallBackPK { CallBackId = _CallBackId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strCallBackIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strCallBackIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrCallBackIds = strCallBackIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrCallBackIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServiceCallBack().GetDeleteMethodName();
                foreach (string strCallBackId in arrCallBackIds)
                {
                    ServiceCallBackPK pk = new ServiceCallBackPK { CallBackId = strCallBackId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strCallBackId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceCallBackPK> Nullify(string strCallBackId)
        {
            ModelInvokeResult<ServiceCallBackPK> result = new ModelInvokeResult<ServiceCallBackPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _CallBackId = strCallBackId.ToGuid();
                if (_CallBackId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ServiceCallBack serviceCallBack = new ServiceCallBack { CallBackId = _CallBackId, Status = 0 };
                /***********************begin 自定义代码*******************/
                serviceCallBack.OperatedBy = NormalSession.UserId.ToGuid();
                serviceCallBack.OperatedOn = DateTime.Now;
                serviceCallBack.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serviceCallBack.GetUpdateMethodName(), ParameterObject = serviceCallBack.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceCallBackPK { CallBackId = _CallBackId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strCallBackIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strCallBackIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrCallBackIds = strCallBackIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrCallBackIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServiceCallBack().GetUpdateMethodName();
                foreach (string strCallBackId in arrCallBackIds)
                {
                    ServiceCallBack serviceCallBack = new ServiceCallBack { CallBackId = strCallBackId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    serviceCallBack.OperatedBy = NormalSession.UserId.ToGuid();
                    serviceCallBack.OperatedOn = DateTime.Now;
                    serviceCallBack.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = serviceCallBack.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ServiceCallBackPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strCallBackId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ServiceCallBack> Load(string strCallBackId)
        {
            ModelInvokeResult<ServiceCallBack> result = new ModelInvokeResult<ServiceCallBack> { Success = true };
            try
            {
                Guid? _CallBackId = strCallBackId.ToGuid();
                if (_CallBackId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var serviceCallBack = BuilderFactory.DefaultBulder().Load<ServiceCallBack, ServiceCallBackPK>(new ServiceCallBackPK { CallBackId = _CallBackId });
                result.instance = serviceCallBack;
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

        #region 创建 CreateCallBackByNoOperator
        [WebInvoke(UriTemplate = "CreateByNoOperator", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceCallBackPK> CreateByNoOperator(ServiceCallBack serviceCallBack)
        {
            ModelInvokeResult<ServiceCallBackPK> result = new ModelInvokeResult<ServiceCallBackPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (serviceCallBack.CallBackId == GlobalManager.GuidAsAutoGenerate)
                {
                    serviceCallBack.CallBackId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                //serviceCallBack.OperatedBy = NormalSession.UserId.ToGuid();
                //serviceCallBack.OperatedOn = DateTime.Now;
                serviceCallBack.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serviceCallBack.GetCreateMethodName(), ParameterObject = serviceCallBack.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceCallBackPK { CallBackId = serviceCallBack.CallBackId };
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