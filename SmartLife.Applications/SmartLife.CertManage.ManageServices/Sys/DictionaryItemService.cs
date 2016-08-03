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
    public class DictionaryItemService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<DictionaryItem> List()
        {
            CollectionInvokeResult<DictionaryItem> result = new CollectionInvokeResult<DictionaryItem> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<DictionaryItem>();
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
        public CollectionInvokeResult<DictionaryItem> Query(string strParms)
        {
            CollectionInvokeResult<DictionaryItem> result = new CollectionInvokeResult<DictionaryItem> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<DictionaryItem>(dictionary);
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
        public JqgridCollectionInvokeResult<DictionaryItem> ListForJqgrid(JqgridCollectionParam<DictionaryItem> param)
        {
            JqgridCollectionInvokeResult<DictionaryItem> result = new JqgridCollectionInvokeResult<DictionaryItem> { Success = true };
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

                gridCollectionPager.JqgridDoPage<DictionaryItem>(BuilderFactory.DefaultBulder().List<DictionaryItem>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<DictionaryItem> ListForEasyUIgrid(EasyUIgridCollectionParam<DictionaryItem> param)
        {
            EasyUIgridCollectionInvokeResult<DictionaryItem> result = new EasyUIgridCollectionInvokeResult<DictionaryItem> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<DictionaryItem>(BuilderFactory.DefaultBulder().List<DictionaryItem>(filters), param, ref result);
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
        public ModelInvokeResult<DictionaryItemPK> Create(DictionaryItem dictionaryItem)
        {
            ModelInvokeResult<DictionaryItemPK> result = new ModelInvokeResult<DictionaryItemPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (dictionaryItem.ItemId == "自动生成")
                {
                    dictionaryItem.ItemId = GlobalManager.getPK2(dictionaryItem.GetMappingTableName(), "ItemId", "UD");//user define
                }
                if (dictionaryItem.ParentId == "_")
                {
                    dictionaryItem.ParentId = null;
                }
                statements.Add(new IBatisNetBatchStatement { StatementName = dictionaryItem.GetCreateMethodName(), ParameterObject = dictionaryItem.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/ 
                StringObjectDictionary dictionary = new
                {
                    DictionaryId = dictionaryItem.DictionaryId,
                    ItemId = dictionaryItem.ItemId,
                    Action = SqlExecuteType.INSERT.ToString().ToLower(),
                    OldParentId = System.DBNull.Value
                }.ToStringObjectDictionary();
                dictionary.Add("ErrorCode", null);
                dictionary.Add("ErrorMessage", null);

                statements.Add(new IBatisNetBatchStatement { StatementName = "SP_Sys_AdjustDictionaryItem", ParameterObject = dictionary, Type = SqlExecuteType.UPDATE });
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new DictionaryItemPK { DictionaryId = dictionaryItem.DictionaryId, ItemId = dictionaryItem.ItemId };
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
        [WebInvoke(UriTemplate = "{strDictionaryId},{strItemId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<DictionaryItemPK> Update(string strDictionaryId, string strItemId, DictionaryItem dictionaryItem)
        {
            ModelInvokeResult<DictionaryItemPK> result = new ModelInvokeResult<DictionaryItemPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _DictionaryId = strDictionaryId;
                dictionaryItem.DictionaryId = _DictionaryId;
                string _ItemId = strItemId;
                dictionaryItem.ItemId = _ItemId;
                if (dictionaryItem.ParentId == "_")
                {
                    dictionaryItem.ParentId = null;
                }
                statements.Add(new IBatisNetBatchStatement { StatementName = dictionaryItem.GetUpdateMethodName(), ParameterObject = dictionaryItem.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                var dictionaryItemOld = BuilderFactory.DefaultBulder().Load<DictionaryItem, DictionaryItemPK>(new DictionaryItemPK { DictionaryId = strDictionaryId, ItemId = strItemId });
                StringObjectDictionary dictionary = new
                {
                    DictionaryId = dictionaryItem.DictionaryId,
                    ItemId = dictionaryItem.ItemId,
                    Action = SqlExecuteType.UPDATE.ToString().ToLower(),
                    OldParentId = dictionaryItemOld.ParentId
                }.ToStringObjectDictionary();
                dictionary.Add("ErrorCode", null);
                dictionary.Add("ErrorMessage", null);

                statements.Add(new IBatisNetBatchStatement { StatementName = "SP_Sys_AdjustDictionaryItem", ParameterObject = dictionary, Type = SqlExecuteType.UPDATE });
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new DictionaryItemPK { DictionaryId = _DictionaryId, ItemId = _ItemId };

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
        [WebInvoke(UriTemplate = "{strDictionaryId},{strItemId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<DictionaryItemPK> Delete(string strDictionaryId, string strItemId)
        {
            ModelInvokeResult<DictionaryItemPK> result = new ModelInvokeResult<DictionaryItemPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _DictionaryId = strDictionaryId;
                string _ItemId = strItemId;
                DictionaryItemPK pk = new DictionaryItemPK { DictionaryId = _DictionaryId, ItemId = _ItemId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new DictionaryItem().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strDictionaryId},{strItemIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strDictionaryId, string strItemIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrItemIds = strItemIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrItemIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new DictionaryItem().GetUpdateMethodName(); //改删除为作废
                foreach (string strItemId in arrItemIds)
                {
                    DictionaryItemPK pk = new DictionaryItemPK { DictionaryId = strDictionaryId, ItemId = strItemId };
                    DeleteCascade(statements, pk);
                    //改删除为作废
                    //statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pk, Type = SqlExecuteType.DELETE });
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = new DictionaryItem { DictionaryId = pk.DictionaryId, ItemId = pk.ItemId, Status = 0 }.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });

                    /***********************begin 自定义代码*******************/
                    var dictionaryItem = BuilderFactory.DefaultBulder().Load<DictionaryItem, DictionaryItemPK>(pk);
                    StringObjectDictionary dictionary = new
                    {
                        DictionaryId = pk.DictionaryId,
                        ItemId = pk.ItemId,
                        Action = SqlExecuteType.DELETE.ToString().ToLower(),
                        OldParentId = dictionaryItem.ParentId
                    }.ToStringObjectDictionary();
                    dictionary.Add("ErrorCode", null);
                    dictionary.Add("ErrorMessage", null);

                    statements.Add(new IBatisNetBatchStatement { StatementName = "SP_Sys_AdjustDictionaryItem", ParameterObject = dictionary, Type = SqlExecuteType.UPDATE });
                    /***********************end 自定义代码*********************/
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

        #region 删除所选 DeleteSelected2
        [WebInvoke(UriTemplate = "DeleteSelected2/{strDictionaryIdItemIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected2(string strDictionaryIdItemIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrDictionaryIdItemIds = strDictionaryIdItemIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrDictionaryIdItemIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new DictionaryItem().GetDeleteMethodName();
                foreach (string strDictionaryIdItemId in arrDictionaryIdItemIds)
                {
                    string[] arrDictionaryIdItemId = strDictionaryIdItemId.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    DictionaryItemPK pk = new DictionaryItemPK { DictionaryId = arrDictionaryIdItemId[0], ItemId = arrDictionaryIdItemId[1] };
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, DictionaryItemPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strDictionaryId},{strItemId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<DictionaryItem> Load(string strDictionaryId, string strItemId)
        {
            ModelInvokeResult<DictionaryItem> result = new ModelInvokeResult<DictionaryItem> { Success = true };
            try
            {
                string _DictionaryId = strDictionaryId;
                string _ItemId = strItemId;
                var dictionaryItem = BuilderFactory.DefaultBulder().Load<DictionaryItem, DictionaryItemPK>(new DictionaryItemPK { DictionaryId = _DictionaryId, ItemId = _ItemId });
                if (dictionaryItem.ParentId == null)
                {
                    dictionaryItem.ParentId = "_";
                }
                result.instance = dictionaryItem;
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
        //[WebInvoke(UriTemplate = "OutputDictionarySQL", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [WebGet(UriTemplate = "OutputSQL/{strDictionaryId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<string> OutputSQL(string strDictionaryId)
        {
            CollectionInvokeResult<string> result = new CollectionInvokeResult<string> { Success = true };
            try
            {

                string[] dictionaryIds = strDictionaryId.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string tableName = dictionaryIds[0];
                string tableNameItem = dictionaryIds[1];
                string tableNameItemExtend = dictionaryIds[2];
                string dictionaryIdAlls = dictionaryIds[3];
                string dictionaryIdAll = "";
                string dictionaryIdAllItemExtend = "";
                if (dictionaryIds.Length >3)
                {
                    //for (int i = 3; i < dictionaryIds.Length; i++)
                    //{
                    //    dictionaryIds[i] = "'" + dictionaryIds[i] + "'";
                    //    dictionaryIdAlls += dictionaryIds[i] + ",";
                    //}
                    dictionaryIdAll = "where Status='1' and DictionaryId in (" + dictionaryIdAlls + ")";
                    dictionaryIdAllItemExtend = "where DictionaryId in  (" + dictionaryIdAlls+ ")";
                }
                else
                {
                    dictionaryIdAll = "";
                    dictionaryIdAllItemExtend = "";
                }
                List<string> _rows = new List<string>();
                _rows.AddRange(BuilderFactory.DefaultBulder().ExecuteSPForQuery<StringObjectDictionary>("SP_Tol_UspOutputDataEx", new { TableName = tableName, WhereClause = dictionaryIdAll }).Select(item => item["targetscript"].ToString()).ToList());
                _rows.AddRange(BuilderFactory.DefaultBulder().ExecuteSPForQuery<StringObjectDictionary>("SP_Tol_UspOutputDataEx", new { TableName = tableNameItem, WhereClause = dictionaryIdAll }).Select(item => item["targetscript"].ToString()).ToList());
                _rows.AddRange(BuilderFactory.DefaultBulder().ExecuteSPForQuery<StringObjectDictionary>("SP_Tol_UspOutputDataEx", new { TableName = tableNameItemExtend, WhereClause = dictionaryIdAllItemExtend }).Select(item => item["targetscript"].ToString()).ToList());
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

        #region 评估字典参数
        [WebInvoke(UriTemplate = "EvaluationDictionaryItemWithExtendInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public IList<DictItemExtendInfo> GetDictionaryItemWithExtendInfo2(EvaluationDictionary evaluationDictionary)
        {
            string sql = "select ItemId,ItemCode,ItemName {0} from {1} where DictionaryId='{2}' and SUBSTRING(ItemCode,1,2)='{3}' ";
            if (evaluationDictionary.Cols.Count > 0)
            {
                List<string> extendStrs = new List<string>();

                foreach (var col in evaluationDictionary.Cols)
                {
                    switch (col.ExtendColType)
                    {
                        case "s":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemStringVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                        case "d":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemDateTimeVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                        case "b":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemBitVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                        case "i":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemIntVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                        case "f":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemFloatVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                        default:
                            break;

                    }
                }
                sql = string.Format(sql, "," + string.Join(",", extendStrs.ToArray()), new DictionaryItem().GetMappingTableName(), evaluationDictionary.DictionaryId, evaluationDictionary.ItemCode);
            }
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).ExecuteNativeSqlForQuery(sql).Select(item => new DictItemExtendInfo { ItemId = item["ItemId"].ToString(), ItemCode = item["ItemCode"].ToString(), ItemName = item["ItemName"].ToString(), ExtendInfos = BuildDictionaryItemExtendInfos(evaluationDictionary.Cols, item) }).ToList();

        }
        private List<DictionaryItemExtendInfo> BuildDictionaryItemExtendInfos(IList<DictionaryItemExtendConfig> cols, StringObjectDictionary item)
        {
            List<DictionaryItemExtendInfo> extendInfos = null;

            if (cols.Count > 0)
            {
                extendInfos = new List<DictionaryItemExtendInfo>();
                foreach (var col in cols)
                {
                    extendInfos.Add(new DictionaryItemExtendInfo { ExtendCol = col.ExtendCol, ExtendValue = TypeConverter.ChangeString(item[col.ExtendCol]) });
                }
            }
            return extendInfos;
        }
        #endregion
        #endregion
    }

    #region 参数
    public class EvaluationDictionary {
        public string DictionaryId { get; set; }
        public string ItemCode { get; set; }
        public IList<DictionaryItemExtendConfig> Cols { get; set; }
    }
    #endregion
}