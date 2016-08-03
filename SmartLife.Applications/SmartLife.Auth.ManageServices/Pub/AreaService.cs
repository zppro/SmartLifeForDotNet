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

namespace SmartLife.Auth.ManageServices.Pub
{ 
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class AreaService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Area> List()
        {
            CollectionInvokeResult<Area> result = new CollectionInvokeResult<Area> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<Area>();
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
        public CollectionInvokeResult<Area> Query(string strParms)
        {
            CollectionInvokeResult<Area> result = new CollectionInvokeResult<Area> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<Area>(dictionary);
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
        public JqgridCollectionInvokeResult<Area> ListForJqgrid(JqgridCollectionParam<Area> param)
        {
            JqgridCollectionInvokeResult<Area> result = new JqgridCollectionInvokeResult<Area> { Success = true };
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

                gridCollectionPager.JqgridDoPage<Area>(BuilderFactory.DefaultBulder().List<Area>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<Area> ListForEasyUIgrid(EasyUIgridCollectionParam<Area> param)
        {
            EasyUIgridCollectionInvokeResult<Area> result = new EasyUIgridCollectionInvokeResult<Area> { Success = true };
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
                        whereClause.Add(string.Format("{0} like '{1}%'", field.key, field.value));
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

                gridCollectionPager.EasyUIgridDoPage<Area>(BuilderFactory.DefaultBulder().List<Area>(filters), param, ref result);
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
        public ModelInvokeResult<AreaPK> Create(Area area)
        {
            ModelInvokeResult<AreaPK> result = new ModelInvokeResult<AreaPK> { Success = true };
            try
            { 
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (area.AreaId == GlobalManager.GuidAsAutoGenerate)
                {
                    string calcId = GetNextAreaId(area.ParentId);
                    area.AreaId = calcId.ToGuid();
                }
                statements.Add(new IBatisNetBatchStatement { StatementName = area.GetCreateMethodName(), ParameterObject = area.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                StringObjectDictionary dictionary = new
                {
                    AreaId = area.AreaId.ToString(),
                    Action = SqlExecuteType.INSERT.ToString().ToLower(),
                    OldParentId = area.ParentId
                }.ToStringObjectDictionary();
                dictionary.Add("ErrorCode", null);
                dictionary.Add("ErrorMessage", null);

                statements.Add(new IBatisNetBatchStatement { StatementName = "SP_Pub_AdjustArea", ParameterObject = dictionary, Type = SqlExecuteType.UPDATE });
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new AreaPK { AreaId = area.AreaId };
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
        [WebInvoke(UriTemplate = "{strAreaId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<AreaPK> Update(string strAreaId, Area area)
        {
            ModelInvokeResult<AreaPK> result = new ModelInvokeResult<AreaPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _AreaId = strAreaId.ToGuid();
                if (_AreaId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                area.AreaId = _AreaId;
                statements.Add(new IBatisNetBatchStatement { StatementName = area.GetUpdateMethodName(), ParameterObject = area.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                var areaOld = BuilderFactory.DefaultBulder().Load<Area, AreaPK>(new AreaPK { AreaId = _AreaId });
                StringObjectDictionary dictionary = new
                {
                    AreaId = area.AreaId.ToString(),
                    Action = SqlExecuteType.UPDATE.ToString().ToLower(),
                    OldParentId = areaOld.ParentId
                }.ToStringObjectDictionary();
                dictionary.Add("ErrorCode", null);
                dictionary.Add("ErrorMessage", null);

                statements.Add(new IBatisNetBatchStatement { StatementName = "SP_Pub_AdjustArea", ParameterObject = dictionary, Type = SqlExecuteType.UPDATE });
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new AreaPK { AreaId = _AreaId };

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
        [WebInvoke(UriTemplate = "{strAreaId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<AreaPK> Delete(string strAreaId)
        {
            ModelInvokeResult<AreaPK> result = new ModelInvokeResult<AreaPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _AreaId = strAreaId.ToGuid();
                if (_AreaId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                AreaPK pk = new AreaPK { AreaId = _AreaId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new Area().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strAreaIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strAreaIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrAreaIds = strAreaIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrAreaIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Area().GetDeleteMethodName();
                foreach (string strAreaId in arrAreaIds)
                {
                    AreaPK pk = new AreaPK { AreaId = strAreaId.ToGuid() };
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, AreaPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strAreaId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Area> Load(string strAreaId)
        {
            ModelInvokeResult<Area> result = new ModelInvokeResult<Area> { Success = true };
            try
            {
                Guid? _AreaId = strAreaId.ToGuid();
                if (_AreaId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var area = BuilderFactory.DefaultBulder().Load<Area, AreaPK>(new AreaPK { AreaId = _AreaId });
                result.instance = area;
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

        #region 计算Area编号
        private string GetNextAreaId(string parentId)
        {
            if (parentId.Length == 5)
            {
                //街道
                return BuilderFactory.DefaultBulder().ExecuteScalar("GetNextAreaIdAsStreet", new { ParentId = parentId }).ToString();
            }
            else
            {
                return BuilderFactory.DefaultBulder().ExecuteScalar("GetNextAreaIdAsCommunity", new { ParentId = parentId }).ToString();
            }
        }
        #endregion
    }
}