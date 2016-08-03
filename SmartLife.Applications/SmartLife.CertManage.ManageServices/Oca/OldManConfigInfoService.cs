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
    public class OldManConfigInfoService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<OldManConfigInfo> List()
        {
            CollectionInvokeResult<OldManConfigInfo> result = new CollectionInvokeResult<OldManConfigInfo> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<OldManConfigInfo>();
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
        public CollectionInvokeResult<OldManConfigInfo> Query(string strParms)
        {
            CollectionInvokeResult<OldManConfigInfo> result = new CollectionInvokeResult<OldManConfigInfo> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<OldManConfigInfo>(dictionary);
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
        public JqgridCollectionInvokeResult<OldManConfigInfo> ListForJqgrid(JqgridCollectionParam<OldManConfigInfo> param)
        {
            JqgridCollectionInvokeResult<OldManConfigInfo> result = new JqgridCollectionInvokeResult<OldManConfigInfo> { Success = true };
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

                gridCollectionPager.JqgridDoPage<OldManConfigInfo>(BuilderFactory.DefaultBulder().List<OldManConfigInfo>(filters), param, ref result);
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

                gridCollectionPager.EasyUIgridDoPage<StringObjectDictionary>(BuilderFactory.DefaultBulder().ListStringObjectDictionary("OldManConfigInfo_By_OldMan_List", filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridPage(EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                        if (field.key.Contains("AreaId2_Start") && field.value != "")
                        {
                            filters["AreaId2"] = field.value;
                        }
                        else if (field.key.Contains("AreaId3_Start") && field.value != "")
                        {
                            filters["AreaId3"] = field.value;
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
                /***********************end 模糊查询***********************/
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

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("OldManConfigInfo_By_OldMan_ListByPage", filters, param, ref result);
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
        public ModelInvokeResult<OldManConfigInfoPK> Create(OldManConfigInfo oldManConfigInfo)
        {
            ModelInvokeResult<OldManConfigInfoPK> result = new ModelInvokeResult<OldManConfigInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (oldManConfigInfo.OldManId == GlobalManager.GuidAsAutoGenerate)
                {
                    oldManConfigInfo.OldManId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                oldManConfigInfo.OperatedBy = NormalSession.UserId.ToGuid();
                oldManConfigInfo.OperatedOn = DateTime.Now;
                oldManConfigInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = oldManConfigInfo.GetCreateMethodName(), ParameterObject = oldManConfigInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManConfigInfoPK { OldManId = oldManConfigInfo.OldManId };
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
        [WebInvoke(UriTemplate = "{strOldManId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<OldManConfigInfoPK> Update(string strOldManId, OldManConfigInfo oldManConfigInfo)
        {
            ModelInvokeResult<OldManConfigInfoPK> result = new ModelInvokeResult<OldManConfigInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _OldManId = strOldManId.ToGuid();
                if (_OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                oldManConfigInfo.OldManId = _OldManId;
                /***********************begin 自定义代码*******************/
                oldManConfigInfo.OperatedBy = NormalSession.UserId.ToGuid();
                oldManConfigInfo.OperatedOn = DateTime.Now;
                oldManConfigInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;


                string sqlCount = "select a.OldManId,CallNo,CallNo2,CallNo3 from Oca_OldManConfigInfo a left join Oca_OldManBaseInfo b on a.OldManId=b.OldManId where b.Status=1 And a.OldManId <> '" + oldManConfigInfo.OldManId.ToString() + "'";
                List<string> whereClause = new List<string>();
                if (oldManConfigInfo.CallNo != null && oldManConfigInfo.CallNo.Trim() != "")
                {
                    whereClause.Add(" a.CallNo='" + oldManConfigInfo.CallNo + "' ");
                }
                if (oldManConfigInfo.CallNo2 != null && oldManConfigInfo.CallNo2.Trim() != "")
                {
                    whereClause.Add(" a.CallNo2='" + oldManConfigInfo.CallNo2 + "' ");
                }
                if (oldManConfigInfo.CallNo3 != null && oldManConfigInfo.CallNo3.Trim() != "")
                {
                    whereClause.Add(" a.CallNo3='" + oldManConfigInfo.CallNo3 + "' ");
                }
                if (whereClause.Count > 0)
                {
                    sqlCount += " And (" + string.Join("OR", whereClause.ToArray()) + ")";
                    //检测号码是否库内唯一
                    if (BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(sqlCount) > 0)
                    {
                        result.Success = false;
                        result.ErrorCode = 51001;
                        return result;
                    }
                }

                /***********************end 自定义代码*********************/
                if (BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(string.Format("select * from {0} where OldManId='{1}'", oldManConfigInfo.GetMappingTableName(), strOldManId)) == 1)
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = oldManConfigInfo.GetUpdateMethodName(), ParameterObject = oldManConfigInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                else
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = oldManConfigInfo.GetCreateMethodName(), ParameterObject = oldManConfigInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }

                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManConfigInfoPK { OldManId = _OldManId };

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
        [WebInvoke(UriTemplate = "{strOldManId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<OldManConfigInfoPK> Delete(string strOldManId)
        {
            ModelInvokeResult<OldManConfigInfoPK> result = new ModelInvokeResult<OldManConfigInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _OldManId = strOldManId.ToGuid();
                if (_OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                OldManConfigInfoPK pk = new OldManConfigInfoPK { OldManId = _OldManId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new OldManConfigInfo().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManConfigInfoPK { OldManId = _OldManId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strOldManIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strOldManIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrOldManIds = strOldManIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrOldManIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new OldManConfigInfo().GetDeleteMethodName();
                foreach (string strOldManId in arrOldManIds)
                {
                    OldManConfigInfoPK pk = new OldManConfigInfoPK { OldManId = strOldManId.ToGuid() };
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, OldManConfigInfoPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strOldManId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<OldManConfigInfo> Load(string strOldManId)
        {
            ModelInvokeResult<OldManConfigInfo> result = new ModelInvokeResult<OldManConfigInfo> { Success = true };
            try
            {
                Guid? _OldManId = strOldManId.ToGuid();
                if (_OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var oldManConfigInfo = BuilderFactory.DefaultBulder().Load<OldManConfigInfo, OldManConfigInfoPK>(new OldManConfigInfoPK { OldManId = _OldManId });
                result.instance = oldManConfigInfo;
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

        #region 扩展业务

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


        #region wj 新加入修改 NewUpdate
        [WebInvoke(UriTemplate = "NEWUPDATE/{strIDNo}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<OldManConfigInfoPK> NEWUPDATE(string strIDNo, OldManConfigInfo oldManConfigInfo)
        {
            ModelInvokeResult<OldManConfigInfoPK> result = new ModelInvokeResult<OldManConfigInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();


                var oldmanbaseinfo = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery("select OldManId from Oca_OldManBaseInfo where IDNo='" + strIDNo + "'");

                string OldManId = "";
                foreach (var item in oldmanbaseinfo)
                {
                    OldManId = item["OldManId"].ToString();

                }
                // string a = OldManBaseInfo.GetProperty("OldManId").ToString();
                // var oldManBaseInfo = BuilderFactory.DefaultBulder().Load<OldManBaseInfo, OldManBaseInfoPK>(new OldManBaseInfoPK { IDNo = strOldManId });


                Guid? _OldManId = OldManId.ToGuid();
                if (_OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                oldManConfigInfo.OldManId = _OldManId;
                /***********************begin 自定义代码*******************/
                oldManConfigInfo.OperatedBy = NormalSession.UserId.ToGuid();
                oldManConfigInfo.OperatedOn = DateTime.Now;
                oldManConfigInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;


                if (oldManConfigInfo.CallNo != null && oldManConfigInfo.CallNo.Trim() != "")
                {
                    //检测号码是否库内唯一
                    if (BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount("select OldManId,CallNo from Oca_OldManConfigInfo where CallNo='" + oldManConfigInfo.CallNo + "' and OldManId <> '" + oldManConfigInfo.OldManId.ToString() + "'") > 0)
                    {
                        result.Success = false;
                        result.ErrorCode = 51001;
                        return result;
                    }

                }
                /***********************end 自定义代码*********************/
                if (BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(string.Format("select * from {0} where OldManId='{1}'", oldManConfigInfo.GetMappingTableName(), OldManId)) == 1)
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = oldManConfigInfo.GetUpdateMethodName(), ParameterObject = oldManConfigInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                else
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = oldManConfigInfo.GetCreateMethodName(), ParameterObject = oldManConfigInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }

                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManConfigInfoPK { OldManId = _OldManId };

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 批量修改老人的政府通包标志
        [WebInvoke(UriTemplate = "BatchSetOldManGovTurnkey", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult BatchSetOldManGovTurnkey(IList<OldManConfigInfo> OldManConfigInfos)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (OldManConfigInfos == null || OldManConfigInfos.Count == 0)
                {
                    //配置老人不存在
                    result.Success = false;
                    result.ErrorMessage = "请选择配置老人";
                    return result;
                }

                foreach (OldManConfigInfo item in OldManConfigInfos)
                {
                    item.OperatedBy = NormalSession.UserId.ToGuid();
                    item.OperatedOn = DateTime.Now;
                    item.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    statements.Add(new IBatisNetBatchStatement { StatementName = item.GetUpdateMethodName(), ParameterObject = item.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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

        #endregion

        #region  养老机构

        #region 设置呼叫号码 
        [WebInvoke(UriTemplate = "PamConfigInfo/{strOldManId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<OldManConfigInfoPK> PamConfigInfo(string strOldManId, OldManConfigInfo oldManConfigInfo)
        {
            ModelInvokeResult<OldManConfigInfoPK> result = new ModelInvokeResult<OldManConfigInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _OldManId = strOldManId.ToGuid();
                if (_OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                oldManConfigInfo.OldManId = _OldManId;
                /***********************begin 自定义代码*******************/
                oldManConfigInfo.OperatedBy = NormalSession.UserId.ToGuid();
                oldManConfigInfo.OperatedOn = DateTime.Now;
                oldManConfigInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;


                string sqlCount = "select a.OldManId,CallNo,CallNo2,CallNo3 from Oca_OldManConfigInfo a left join Oca_OldManBaseInfo b on a.OldManId=b.OldManId where b.Status=3 And a.OldManId <> '" + oldManConfigInfo.OldManId.ToString() + "'";
                List<string> whereClause = new List<string>();
                if (oldManConfigInfo.CallNo != null && oldManConfigInfo.CallNo.Trim() != "")
                {
                    whereClause.Add(" a.CallNo='" + oldManConfigInfo.CallNo + "' ");
                }
                if (oldManConfigInfo.CallNo2 != null && oldManConfigInfo.CallNo2.Trim() != "")
                {
                    whereClause.Add(" a.CallNo2='" + oldManConfigInfo.CallNo2 + "' ");
                }
                if (oldManConfigInfo.CallNo3 != null && oldManConfigInfo.CallNo3.Trim() != "")
                {
                    whereClause.Add(" a.CallNo3='" + oldManConfigInfo.CallNo3 + "' ");
                }
                if (whereClause.Count > 0)
                {
                    sqlCount += " And (" + string.Join("OR", whereClause.ToArray()) + ")";
                    //检测号码是否库内唯一
                    if (BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(sqlCount) > 0)
                    {
                        result.Success = false;
                        result.ErrorCode = 51001;
                        return result;
                    }
                }

                /***********************end 自定义代码*********************/
                if (BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(string.Format("select * from {0} where OldManId='{1}'", oldManConfigInfo.GetMappingTableName(), strOldManId)) == 1)
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = oldManConfigInfo.GetUpdateMethodName(), ParameterObject = oldManConfigInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                else
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = oldManConfigInfo.GetCreateMethodName(), ParameterObject = oldManConfigInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }

                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManConfigInfoPK { OldManId = _OldManId };
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