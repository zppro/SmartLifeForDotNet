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
using SmartLife.Share.Models.WeiXin.Pub;

namespace SmartLife.CertManage.ManageServices.WeiXin.Pub
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class WXMessageTemplateService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<WXMessageTemplate> List()
        {
            CollectionInvokeResult<WXMessageTemplate> result = new CollectionInvokeResult<WXMessageTemplate> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<WXMessageTemplate>();
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
        public CollectionInvokeResult<WXMessageTemplate> Query(string strParms)
        {
            CollectionInvokeResult<WXMessageTemplate> result = new CollectionInvokeResult<WXMessageTemplate> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<WXMessageTemplate>(dictionary);
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
        public JqgridCollectionInvokeResult<WXMessageTemplate> ListForJqgrid(JqgridCollectionParam<WXMessageTemplate> param)
        {
            JqgridCollectionInvokeResult<WXMessageTemplate> result = new JqgridCollectionInvokeResult<WXMessageTemplate> { Success = true };
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

                gridCollectionPager.JqgridDoPage<WXMessageTemplate>(BuilderFactory.DefaultBulder().List<WXMessageTemplate>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<WXMessageTemplate> ListForEasyUIgrid(EasyUIgridCollectionParam<WXMessageTemplate> param)
        {
            EasyUIgridCollectionInvokeResult<WXMessageTemplate> result = new EasyUIgridCollectionInvokeResult<WXMessageTemplate> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<WXMessageTemplate>(BuilderFactory.DefaultBulder().List<WXMessageTemplate>(filters), param, ref result);
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
        public ModelInvokeResult<WXMessageTemplatePK> Create(WXMessageTemplate wXMessageTemplate)
        {
            ModelInvokeResult<WXMessageTemplatePK> result = new ModelInvokeResult<WXMessageTemplatePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = wXMessageTemplate.GetCreateMethodName(), ParameterObject = wXMessageTemplate.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WXMessageTemplatePK { TemplateId = wXMessageTemplate.TemplateId };
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
        [WebInvoke(UriTemplate = "{strTemplateId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<WXMessageTemplatePK> Update(string strTemplateId, WXMessageTemplate wXMessageTemplate)
        {
            ModelInvokeResult<WXMessageTemplatePK> result = new ModelInvokeResult<WXMessageTemplatePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _TemplateId = strTemplateId;
                wXMessageTemplate.TemplateId = _TemplateId;
                /***********************begin 自定义代码*******************/
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = wXMessageTemplate.GetUpdateMethodName(), ParameterObject = wXMessageTemplate.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WXMessageTemplatePK { TemplateId = _TemplateId };

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
        [WebInvoke(UriTemplate = "{strTemplateId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<WXMessageTemplatePK> Delete(string strTemplateId)
        {
            ModelInvokeResult<WXMessageTemplatePK> result = new ModelInvokeResult<WXMessageTemplatePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _TemplateId = strTemplateId;
                WXMessageTemplatePK pk = new WXMessageTemplatePK { TemplateId = _TemplateId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new WXMessageTemplate().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WXMessageTemplatePK { TemplateId = _TemplateId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strTemplateIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strTemplateIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrTemplateIds = strTemplateIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrTemplateIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new WXMessageTemplate().GetDeleteMethodName();
                foreach (string strTemplateId in arrTemplateIds)
                {
                    WXMessageTemplatePK pk = new WXMessageTemplatePK { TemplateId = strTemplateId };
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
        [WebInvoke(UriTemplate = "Nullify/{strTemplateId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<WXMessageTemplatePK> Nullify(string strTemplateId)
        {
            ModelInvokeResult<WXMessageTemplatePK> result = new ModelInvokeResult<WXMessageTemplatePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _TemplateId = strTemplateId;
                WXMessageTemplate wXMessageTemplate = new WXMessageTemplate { TemplateId = _TemplateId, Status = 0 };
                /***********************begin 自定义代码*******************/
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = wXMessageTemplate.GetUpdateMethodName(), ParameterObject = wXMessageTemplate.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WXMessageTemplatePK { TemplateId = _TemplateId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strTemplateIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strTemplateIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrTemplateIds = strTemplateIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrTemplateIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new WXMessageTemplate().GetUpdateMethodName();
                foreach (string strTemplateId in arrTemplateIds)
                {
                    WXMessageTemplate wXMessageTemplate = new WXMessageTemplate { TemplateId = strTemplateId, Status = 0 };
                    /***********************begin 自定义代码*******************/
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = wXMessageTemplate.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, WXMessageTemplatePK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strTemplateId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<WXMessageTemplate> Load(string strTemplateId)
        {
            ModelInvokeResult<WXMessageTemplate> result = new ModelInvokeResult<WXMessageTemplate> { Success = true };
            try
            {
                string _TemplateId = strTemplateId;
                var wXMessageTemplate = BuilderFactory.DefaultBulder().Load<WXMessageTemplate, WXMessageTemplatePK>(new WXMessageTemplatePK { TemplateId = _TemplateId });
                result.instance = wXMessageTemplate;
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