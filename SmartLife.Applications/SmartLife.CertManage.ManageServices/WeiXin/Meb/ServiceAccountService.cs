﻿using System;
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
using SmartLife.Share.Models.WeiXin.Meb;

namespace SmartLife.CertManage.ManageServices.WeiXin.Meb
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServiceAccountService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceAccount> List()
        {
            CollectionInvokeResult<ServiceAccount> result = new CollectionInvokeResult<ServiceAccount> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<ServiceAccount>();
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
        public CollectionInvokeResult<ServiceAccount> Query(string strParms)
        {
            CollectionInvokeResult<ServiceAccount> result = new CollectionInvokeResult<ServiceAccount> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<ServiceAccount>(dictionary);
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
        public JqgridCollectionInvokeResult<ServiceAccount> ListForJqgrid(JqgridCollectionParam<ServiceAccount> param)
        {
            JqgridCollectionInvokeResult<ServiceAccount> result = new JqgridCollectionInvokeResult<ServiceAccount> { Success = true };
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

                gridCollectionPager.JqgridDoPage<ServiceAccount>(BuilderFactory.DefaultBulder().List<ServiceAccount>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<ServiceAccount> ListForEasyUIgrid(EasyUIgridCollectionParam<ServiceAccount> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceAccount> result = new EasyUIgridCollectionInvokeResult<ServiceAccount> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<ServiceAccount>(BuilderFactory.DefaultBulder().List<ServiceAccount>(filters), param, ref result);
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
        public ModelInvokeResult<ServiceAccountPK> Create(ServiceAccount serviceAccount)
        {
            ModelInvokeResult<ServiceAccountPK> result = new ModelInvokeResult<ServiceAccountPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                statements.Add(new IBatisNetBatchStatement { StatementName = serviceAccount.GetCreateMethodName(), ParameterObject = serviceAccount.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceAccountPK { AccountCode = serviceAccount.AccountCode };
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
        [WebInvoke(UriTemplate = "{strAccountCode}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceAccountPK> Update(string strAccountCode, ServiceAccount serviceAccount)
        {
            ModelInvokeResult<ServiceAccountPK> result = new ModelInvokeResult<ServiceAccountPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _AccountCode = strAccountCode;
                serviceAccount.AccountCode = strAccountCode;
                /***********************begin 自定义代码*******************/
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serviceAccount.GetUpdateMethodName(), ParameterObject = serviceAccount.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceAccountPK { AccountCode = _AccountCode };

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
        [WebInvoke(UriTemplate = "{strAccountCode}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceAccountPK> Delete(string strAccountCode)
        {
            ModelInvokeResult<ServiceAccountPK> result = new ModelInvokeResult<ServiceAccountPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _AccountCode = strAccountCode;
                ServiceAccountPK pk = new ServiceAccountPK { AccountCode = _AccountCode };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new ServiceAccount().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceAccountPK { AccountCode = _AccountCode };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strAccountCodes}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strAccountCodes)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrAccountCodes = strAccountCodes.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrAccountCodes.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServiceAccount().GetDeleteMethodName();
                foreach (string strAccountCode in arrAccountCodes)
                {
                    ServiceAccountPK pk = new ServiceAccountPK { AccountCode = strAccountCode };
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
        [WebInvoke(UriTemplate = "Nullify/{strAccountCode}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceAccountPK> Nullify(string strAccountCode)
        {
            ModelInvokeResult<ServiceAccountPK> result = new ModelInvokeResult<ServiceAccountPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _AccountCode = strAccountCode;
                ServiceAccount serviceAccount = new ServiceAccount { AccountCode = _AccountCode, Status = 0 };

                statements.Add(new IBatisNetBatchStatement { StatementName = serviceAccount.GetUpdateMethodName(), ParameterObject = serviceAccount.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceAccountPK { AccountCode = _AccountCode };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strAccountCodes}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strAccountCodes)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrAccountCodes = strAccountCodes.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrAccountCodes.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServiceAccount().GetUpdateMethodName();
                foreach (string strAccountCode in arrAccountCodes)
                {
                    ServiceAccount serviceAccount = new ServiceAccount { AccountCode = strAccountCode, Status = 0 };
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = serviceAccount.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ServiceAccountPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strAccountCode}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ServiceAccount> Load(string strAccountCode)
        {
            ModelInvokeResult<ServiceAccount> result = new ModelInvokeResult<ServiceAccount> { Success = true };
            try
            {
                string _AccountCode = strAccountCode;
                var serviceAccount = BuilderFactory.DefaultBulder().Load<ServiceAccount, ServiceAccountPK>(new ServiceAccountPK { AccountCode = _AccountCode });
                result.instance = serviceAccount;
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