/*******************针对主键是Id的*************************/
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
using e0571.web.core.MVC;

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
    public class OldManFamilyInfoService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<OldManFamilyInfo> List()
        {
            CollectionInvokeResult<OldManFamilyInfo> result = new CollectionInvokeResult<OldManFamilyInfo> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<OldManFamilyInfo>();
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
        public CollectionInvokeResult<OldManFamilyInfo> Query(string strParms)
        {
            CollectionInvokeResult<OldManFamilyInfo> result = new CollectionInvokeResult<OldManFamilyInfo> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<OldManFamilyInfo>(dictionary);
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
        public JqgridCollectionInvokeResult<OldManFamilyInfo> ListForJqgrid(JqgridCollectionParam<OldManFamilyInfo> param)
        {
            JqgridCollectionInvokeResult<OldManFamilyInfo> result = new JqgridCollectionInvokeResult<OldManFamilyInfo> { Success = true };
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

                gridCollectionPager.JqgridDoPage<OldManFamilyInfo>(BuilderFactory.DefaultBulder().List<OldManFamilyInfo>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgrid(EasyUIgridCollectionParam<StringObjectDictionary> param)
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

                gridCollectionPager.EasyUIgridDoPage<StringObjectDictionary>(BuilderFactory.DefaultBulder().ListStringObjectDictionary("OldManFamilyInfo_By_OldMan_List", filters), param, ref result);
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
        public ModelInvokeResult<OldManFamilyInfoPK> Create(OldManFamilyInfo oldManFamilyInfo)
        {
            ModelInvokeResult<OldManFamilyInfoPK> result = new ModelInvokeResult<OldManFamilyInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (oldManFamilyInfo.Id == -1)
                {
                    oldManFamilyInfo.Id = null;
                }
                oldManFamilyInfo.OperatedBy = NormalSession.UserId.ToGuid();
                oldManFamilyInfo.OperatedOn = DateTime.Now;
                oldManFamilyInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                statements.Add(new IBatisNetBatchStatement { StatementName = oldManFamilyInfo.GetCreateMethodName(), ParameterObject = oldManFamilyInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManFamilyInfoPK { Id = oldManFamilyInfo.Id };
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
        [WebInvoke(UriTemplate = "{strId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<OldManFamilyInfoPK> Update(string strId, OldManFamilyInfo oldManFamilyInfo)
        {
            ModelInvokeResult<OldManFamilyInfoPK> result = new ModelInvokeResult<OldManFamilyInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                statements.Add(new IBatisNetBatchStatement { StatementName = oldManFamilyInfo.GetUpdateMethodName(), ParameterObject = oldManFamilyInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManFamilyInfoPK { Id = _Id };

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
        [WebInvoke(UriTemplate = "{strId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<OldManFamilyInfoPK> Delete(string strId)
        {
            ModelInvokeResult<OldManFamilyInfoPK> result = new ModelInvokeResult<OldManFamilyInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                OldManFamilyInfoPK pk = new OldManFamilyInfoPK { Id = _Id };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new OldManFamilyInfo().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManFamilyInfoPK { Id = _Id };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrIds = strIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new OldManFamilyInfo().GetDeleteMethodName();
                foreach (string strId in arrIds)
                {
                    OldManFamilyInfoPK pk = new OldManFamilyInfoPK { Id = Convert.ToInt32(strId) };
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
        [WebInvoke(UriTemplate = "Nullify/{strId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<OldManFamilyInfoPK> Nullify(string strId)
        {
            ModelInvokeResult<OldManFamilyInfoPK> result = new ModelInvokeResult<OldManFamilyInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                OldManFamilyInfo oldManFamilyInfo = new OldManFamilyInfo { Id = _Id, Status = 0 };
                statements.Add(new IBatisNetBatchStatement { StatementName = oldManFamilyInfo.GetUpdateMethodName(), ParameterObject = oldManFamilyInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManFamilyInfoPK { Id = _Id };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrIds = strIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new OldManFamilyInfo().GetUpdateMethodName();
                foreach (string strId in arrIds)
                {
                    OldManFamilyInfo oldManFamilyInfo = new OldManFamilyInfo { Id = Convert.ToInt32(strId), Status = 0 };
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = oldManFamilyInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, OldManFamilyInfoPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<OldManFamilyInfo> Load(string strId)
        {
            ModelInvokeResult<OldManFamilyInfo> result = new ModelInvokeResult<OldManFamilyInfo> { Success = true };
            try
            {
                var oldManFamilyInfo = BuilderFactory.DefaultBulder().Load<OldManFamilyInfo, OldManFamilyInfoPK>(new OldManFamilyInfoPK { Id = Convert.ToInt32(strId) });
                result.instance = oldManFamilyInfo;
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
        [WebInvoke(UriTemplate = "ListByOldMan/{oldManId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListByOldMan(string oldManId, EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();
                /**********************************************************/
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        filters[field.key] = field.value;
                    }
                }

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

                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("OldManFamilyInfo_By_OldMan_List", filters);
                result.total = result.rows.Count;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebInvoke(UriTemplate = "SaveByOldMan/{oldManId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SaveAll(string oldManId, IEnumerable<OldManFamilyInfo> oldManFamilyInfos)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            { 

                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                statements.Add(new IBatisNetBatchStatement { StatementName = new OldManFamilyInfo().GetDeleteMethodName(), ParameterObject = new { OldManId = oldManId.ToGuid() }, Type = SqlExecuteType.DELETE });

                foreach (OldManFamilyInfo oldManFamilyInfo in oldManFamilyInfos)
                {
                    oldManFamilyInfo.OldManId = oldManId.ToGuid();
                    oldManFamilyInfo.OperatedBy = NormalSession.UserId.ToGuid();
                    oldManFamilyInfo.OperatedOn = DateTime.Now;
                    oldManFamilyInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    statements.Add(new IBatisNetBatchStatement { StatementName = oldManFamilyInfo.GetCreateMethodName(), ParameterObject = oldManFamilyInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }
                 
                
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                if (statements.Count > 0)
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

        [WebGet(UriTemplate = "ListByOldMan2/{oldManId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ListItem> ListByOldMan2(string oldManId)
        {
            CollectionInvokeResult<ListItem> result = new CollectionInvokeResult<ListItem> { Success = true };
            
            try
            {
                StringObjectDictionary parms = new { OldManId = Guid.Parse(oldManId), FamilyMemberStatus = 1 }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("OldManFamilyInfo_By_OldMan_List2", parms).Select(item => new ListItem { Text = TypeConverter.ChangeString(item["RelationNameOfOldMan"]), Value = TypeConverter.ChangeString(item["FamilyMemberName"]) + "|" + TypeConverter.ChangeString(item["FamilyMemberContactPhone"]) + "|" + TypeConverter.ChangeString(item["FamilyMemberId"]) }).ToList();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            } 
            return result;
        }


        #region 获取老人成员所有信息
        [WebGet(UriTemplate = "ListByFamilyMember?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> ListByFamilyMember(string strParms)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("OldManFamilyMember_Info_List2", dictionary.ToStringObjectDictionary(false));
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 老人绑定亲人（不允许重复绑定）
        [WebInvoke(UriTemplate = "UnRepeatCreate", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<OldManFamilyInfoPK> UnRepeatCreate(OldManFamilyInfo oldManFamilyInfo)
        {
            ModelInvokeResult<OldManFamilyInfoPK> result = new ModelInvokeResult<OldManFamilyInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (oldManFamilyInfo.Id == -1)
                {
                    oldManFamilyInfo.Id = null;
                }
                /**************************查看是否已经绑定过此人*******************************/
                CollectionInvokeResult<OldManFamilyInfo> ret = new CollectionInvokeResult<OldManFamilyInfo> { Success = true };
                ret.rows = BuilderFactory.DefaultBulder().List<OldManFamilyInfo>(oldManFamilyInfo);
                /**************************查看是否已经绑定过此人*******************************/

                if (ret.rows.Count < 1)
                {
                    oldManFamilyInfo.OperatedBy = NormalSession.UserId.ToGuid();
                    oldManFamilyInfo.OperatedOn = DateTime.Now;
                    oldManFamilyInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    statements.Add(new IBatisNetBatchStatement { StatementName = oldManFamilyInfo.GetCreateMethodName(), ParameterObject = oldManFamilyInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    /***********************begin 自定义代码*******************/
                    /***********************此处添加自定义代码*****************/
                    /***********************end 自定义代码*********************/
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                    result.instance = new OldManFamilyInfoPK { Id = oldManFamilyInfo.Id };
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "此人已经绑定过，不能重复绑定";
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


        #region  删除老人单条亲人
        [WebInvoke(UriTemplate = "DeleteFamily", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<OldManFamilyInfoPK> DeleteFamily(OldManFamilyInfo oldManFamilyInfo)
        {
            ModelInvokeResult<OldManFamilyInfoPK> result = new ModelInvokeResult<OldManFamilyInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                statements.Add(new IBatisNetBatchStatement { StatementName = new OldManFamilyInfo().GetDeleteMethodName(), ParameterObject = oldManFamilyInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
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

        #endregion
    }
}