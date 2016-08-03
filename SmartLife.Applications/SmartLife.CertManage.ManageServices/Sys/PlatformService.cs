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
    public class PlatformService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Platform> List()
        {
            CollectionInvokeResult<Platform> result = new CollectionInvokeResult<Platform> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<Platform>();
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
        public CollectionInvokeResult<Platform> Query(string strParms)
        {
            CollectionInvokeResult<Platform> result = new CollectionInvokeResult<Platform> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<Platform>(dictionary);
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
        public JqgridCollectionInvokeResult<Platform> ListForJqgrid(JqgridCollectionParam<Platform> param)
        {
            JqgridCollectionInvokeResult<Platform> result = new JqgridCollectionInvokeResult<Platform> { Success = true };
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

                gridCollectionPager.JqgridDoPage<Platform>(BuilderFactory.DefaultBulder().List<Platform>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<Platform> ListForEasyUIgrid(EasyUIgridCollectionParam<Platform> param)
        {
            EasyUIgridCollectionInvokeResult<Platform> result = new EasyUIgridCollectionInvokeResult<Platform> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<Platform>(BuilderFactory.DefaultBulder().List<Platform>(filters), param, ref result);
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
        public ModelInvokeResult<PlatformPK> Create(Platform platform)
        {
            ModelInvokeResult<PlatformPK> result = new ModelInvokeResult<PlatformPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (platform.PlatformId == "自动生成")
                {
                    platform.PlatformId = GlobalManager.getPK(platform.GetMappingTableName(), "PlatformId");
                }
                statements.Add(new IBatisNetBatchStatement { StatementName = platform.GetCreateMethodName(), ParameterObject = platform.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new PlatformPK { PlatformId = platform.PlatformId };
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
        [WebInvoke(UriTemplate = "{strPlatformId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<PlatformPK> Update(string strPlatformId, Platform platform)
        {
            ModelInvokeResult<PlatformPK> result = new ModelInvokeResult<PlatformPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _PlatformId = strPlatformId;
                platform.PlatformId = _PlatformId;
                statements.Add(new IBatisNetBatchStatement { StatementName = platform.GetUpdateMethodName(), ParameterObject = platform.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new PlatformPK { PlatformId = _PlatformId };

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
        [WebInvoke(UriTemplate = "{strPlatformId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<PlatformPK> Delete(string strPlatformId)
        {
            ModelInvokeResult<PlatformPK> result = new ModelInvokeResult<PlatformPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _PlatformId = strPlatformId;
                PlatformPK pk = new PlatformPK { PlatformId = _PlatformId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new Platform().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strPlatformIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strPlatformIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrPlatformIds = strPlatformIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrPlatformIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Platform().GetDeleteMethodName();
                foreach (string strPlatformId in arrPlatformIds)
                {
                    PlatformPK pk = new PlatformPK { PlatformId = strPlatformId };
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, PlatformPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strPlatformId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Platform> Load(string strPlatformId)
        {
            ModelInvokeResult<Platform> result = new ModelInvokeResult<Platform> { Success = true };
            try
            {
                string _PlatformId = strPlatformId;
                var platform = BuilderFactory.DefaultBulder().Load<Platform, PlatformPK>(new PlatformPK { PlatformId = _PlatformId });
                result.instance = platform;
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