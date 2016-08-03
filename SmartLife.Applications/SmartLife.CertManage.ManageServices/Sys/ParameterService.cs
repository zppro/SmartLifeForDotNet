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

namespace SmartLife.CertManage.ManageServices.Sys
{ 
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ParameterService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Parameter> List()
        {
            CollectionInvokeResult<Parameter> result = new CollectionInvokeResult<Parameter> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<Parameter>();
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
        public CollectionInvokeResult<Parameter> Query(string strParms)
        {
            CollectionInvokeResult<Parameter> result = new CollectionInvokeResult<Parameter> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<Parameter>(dictionary);
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
        public JqgridCollectionInvokeResult<Parameter> ListForJqgrid(JqgridCollectionParam<Parameter> param)
        {
            JqgridCollectionInvokeResult<Parameter> result = new JqgridCollectionInvokeResult<Parameter> { Success = true };
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

                gridCollectionPager.JqgridDoPage<Parameter>(BuilderFactory.DefaultBulder().List<Parameter>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<Parameter> ListForEasyUIgrid(EasyUIgridCollectionParam<Parameter> param)
        {
            EasyUIgridCollectionInvokeResult<Parameter> result = new EasyUIgridCollectionInvokeResult<Parameter> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<Parameter>(BuilderFactory.DefaultBulder().List<Parameter>(filters), param, ref result);
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
        public ModelInvokeResult<ParameterPK> Create(Parameter parameter)
        {
            ModelInvokeResult<ParameterPK> result = new ModelInvokeResult<ParameterPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                /*
                if (parameter.ParameterId == "自动生成")
                {
                    parameter.ParameterId = GlobalManager.getPK(parameter.GetMappingTableName(), "ParameterId");
                }
                */
                statements.Add(new IBatisNetBatchStatement { StatementName = parameter.GetCreateMethodName(), ParameterObject = parameter.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ParameterPK { ParameterId = parameter.ParameterId };
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
        [WebInvoke(UriTemplate = "{strParameterId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ParameterPK> Update(string strParameterId, Parameter parameter)
        {
            ModelInvokeResult<ParameterPK> result = new ModelInvokeResult<ParameterPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _ParameterId = strParameterId;
                parameter.ParameterId = _ParameterId;
                statements.Add(new IBatisNetBatchStatement { StatementName = parameter.GetUpdateMethodName(), ParameterObject = parameter.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ParameterPK { ParameterId = _ParameterId };

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
        [WebInvoke(UriTemplate = "{strParameterId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ParameterPK> Delete(string strParameterId)
        {
            ModelInvokeResult<ParameterPK> result = new ModelInvokeResult<ParameterPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _ParameterId = strParameterId;
                ParameterPK pk = new ParameterPK { ParameterId = _ParameterId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new Parameter().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = pk;
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strParameterIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strParameterIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrParameterIds = strParameterIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrParameterIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Parameter().GetDeleteMethodName();
                foreach (string strParameterId in arrParameterIds)
                {
                    ParameterPK pk = new ParameterPK { ParameterId = strParameterId };
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


        #region 级联删除扩展接口 DeleteCascade
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ParameterPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strParameterId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Parameter> Load(string strParameterId)
        {
            ModelInvokeResult<Parameter> result = new ModelInvokeResult<Parameter> { Success = true };
            try
            {
                string _ParameterId = strParameterId;
                var parameter = BuilderFactory.DefaultBulder().Load<Parameter, ParameterPK>(new ParameterPK { ParameterId = _ParameterId });
                result.instance = parameter;
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


        #region 业务接口
        #region 导出SQL
        //[WebInvoke(UriTemplate = "OutputTreeSQL", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [WebGet(UriTemplate = "OutputSQL/{strParameterIds}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<string> OutputSQL(string strParameterIds)
        {
            CollectionInvokeResult<string> result = new CollectionInvokeResult<string> { Success = true };
            try
            {

                string[] parameterIds = strParameterIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string tableName = parameterIds[0];
                string parameterIdAll = "";
                if (parameterIds.Length > 1)
                {
                    for (int i = 1; i < parameterIds.Length; i++)
                    {
                        parameterIds[i] = "'" + parameterIds[i] + "'";
                        parameterIdAll += parameterIds[i] + ",";
                    }
                    parameterIdAll = parameterIdAll.Remove(parameterIdAll.LastIndexOf(","), 1);
                    parameterIdAll = "where Status='1' and ParameterId in (" + parameterIdAll + ")";
                }
                else
                {
                    parameterIdAll = "";
                }
                List<string> _rows = new List<string>();
                _rows.AddRange(BuilderFactory.DefaultBulder().ExecuteSPForQuery<StringObjectDictionary>("SP_Tol_UspOutputDataEx", new { TableName = tableName, WhereClause = parameterIdAll }).Select(item => item["targetscript"].ToString()).ToList());
                result.rows = _rows;
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