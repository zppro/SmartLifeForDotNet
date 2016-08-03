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
    [ServiceLogging]
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BuildingUnitBaseInfoService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<BuildingUnitBaseInfo> List()
        {
            CollectionInvokeResult<BuildingUnitBaseInfo> result = new CollectionInvokeResult<BuildingUnitBaseInfo> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<BuildingUnitBaseInfo>();
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
        public CollectionInvokeResult<BuildingUnitBaseInfo> Query(string strParms)
        {
            CollectionInvokeResult<BuildingUnitBaseInfo> result = new CollectionInvokeResult<BuildingUnitBaseInfo> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<BuildingUnitBaseInfo>(dictionary);
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
        public JqgridCollectionInvokeResult<BuildingUnitBaseInfo> ListForJqgrid(JqgridCollectionParam<BuildingUnitBaseInfo> param)
        {
            JqgridCollectionInvokeResult<BuildingUnitBaseInfo> result = new JqgridCollectionInvokeResult<BuildingUnitBaseInfo> { Success = true };
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

                gridCollectionPager.JqgridDoPage<BuildingUnitBaseInfo>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<BuildingUnitBaseInfo>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<BuildingUnitBaseInfo> ListForEasyUIgrid(EasyUIgridCollectionParam<BuildingUnitBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<BuildingUnitBaseInfo> result = new EasyUIgridCollectionInvokeResult<BuildingUnitBaseInfo> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<BuildingUnitBaseInfo>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<BuildingUnitBaseInfo>(filters), param, ref result);
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
        public ModelInvokeResult<BuildingUnitBaseInfoPK> Create(BuildingUnitBaseInfo buildingUnitBaseInfo)
        {
            ModelInvokeResult<BuildingUnitBaseInfoPK> result = new ModelInvokeResult<BuildingUnitBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (buildingUnitBaseInfo.UnitId == GlobalManager.GuidAsAutoGenerate)
                {
                    buildingUnitBaseInfo.UnitId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                buildingUnitBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                buildingUnitBaseInfo.OperatedOn = DateTime.Now;
                buildingUnitBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = buildingUnitBaseInfo.GetCreateMethodName(), ParameterObject = buildingUnitBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new BuildingUnitBaseInfoPK { UnitId = buildingUnitBaseInfo.UnitId };
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
        [WebInvoke(UriTemplate = "{strUnitId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<BuildingUnitBaseInfoPK> Update(string strUnitId, BuildingUnitBaseInfo buildingUnitBaseInfo)
        {
            ModelInvokeResult<BuildingUnitBaseInfoPK> result = new ModelInvokeResult<BuildingUnitBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _UnitId = strUnitId.ToGuid();
                if (_UnitId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                buildingUnitBaseInfo.UnitId = _UnitId;
                /***********************begin 自定义代码*******************/
                buildingUnitBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                buildingUnitBaseInfo.OperatedOn = DateTime.Now;
                buildingUnitBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = buildingUnitBaseInfo.GetUpdateMethodName(), ParameterObject = buildingUnitBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new BuildingUnitBaseInfoPK { UnitId = _UnitId };

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
        [WebInvoke(UriTemplate = "{strUnitId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<BuildingUnitBaseInfoPK> Delete(string strUnitId)
        {
            ModelInvokeResult<BuildingUnitBaseInfoPK> result = new ModelInvokeResult<BuildingUnitBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _UnitId = strUnitId.ToGuid();
                if (_UnitId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                BuildingUnitBaseInfoPK pk = new BuildingUnitBaseInfoPK { UnitId = _UnitId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new BuildingUnitBaseInfo().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new BuildingUnitBaseInfoPK { UnitId = _UnitId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strUnitIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strUnitIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrUnitIds = strUnitIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrUnitIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new BuildingUnitBaseInfo().GetDeleteMethodName();
                foreach (string strUnitId in arrUnitIds)
                {
                    BuildingUnitBaseInfoPK pk = new BuildingUnitBaseInfoPK { UnitId = strUnitId.ToGuid() };
                    DeleteCascade(statements, pk);
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
        [WebInvoke(UriTemplate = "Nullify/{strUnitId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<BuildingUnitBaseInfoPK> Nullify(string strUnitId)
        {
            ModelInvokeResult<BuildingUnitBaseInfoPK> result = new ModelInvokeResult<BuildingUnitBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _UnitId = strUnitId.ToGuid();
                if (_UnitId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                BuildingUnitBaseInfo buildingUnitBaseInfo = new BuildingUnitBaseInfo { UnitId = _UnitId, Status = 0 };
                /***********************begin 自定义代码*******************/
                buildingUnitBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                buildingUnitBaseInfo.OperatedOn = DateTime.Now;
                buildingUnitBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = buildingUnitBaseInfo.GetUpdateMethodName(), ParameterObject = buildingUnitBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new BuildingUnitBaseInfoPK { UnitId = _UnitId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strUnitIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strUnitIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrUnitIds = strUnitIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrUnitIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new BuildingUnitBaseInfo().GetUpdateMethodName();
                foreach (string strUnitId in arrUnitIds)
                {
                    BuildingUnitBaseInfo buildingUnitBaseInfo = new BuildingUnitBaseInfo { UnitId = strUnitId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    buildingUnitBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                    buildingUnitBaseInfo.OperatedOn = DateTime.Now;
                    buildingUnitBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = buildingUnitBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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

        #region 级联删除扩展接口 DeleteCascade
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, BuildingUnitBaseInfoPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strUnitId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<BuildingUnitBaseInfo> Load(string strUnitId)
        {
            ModelInvokeResult<BuildingUnitBaseInfo> result = new ModelInvokeResult<BuildingUnitBaseInfo> { Success = true };
            try
            {
                Guid? _UnitId = strUnitId.ToGuid();
                if (_UnitId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var buildingUnitBaseInfo = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<BuildingUnitBaseInfo, BuildingUnitBaseInfoPK>(new BuildingUnitBaseInfoPK { UnitId = _UnitId });
                result.instance = buildingUnitBaseInfo;
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
                IList<UserArea> userAreas = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<UserArea>(new UserArea { UserId = NormalSession.UserId.ToGuid() });
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

        #region EasyUIgrid数据格式的列表分页查询(权限) ListForEasyUIgrid
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage_ByUsers/{strConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<BuildingUnitBaseInfo_V> ListForEasyUIgridPage_ByUsers(string strConnectId, EasyUIgridCollectionParam<BuildingUnitBaseInfo_V> param)
        {
            EasyUIgridCollectionInvokeResult<BuildingUnitBaseInfo_V> result = new EasyUIgridCollectionInvokeResult<BuildingUnitBaseInfo_V> { Success = true };
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
                /**********************************************************/
                /***********************begin 模糊查询*********************/
                if (param.fuzzyFields != null)
                {
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        DateTime parseTime = new DateTime();
                        {
                            if (DateTime.TryParse(field.value, out parseTime))
                            {
                                fuzzys.Add(string.Format(" {0} >= '{1}' ", field.key, parseTime.ToString("yyyy-MM-dd HH:mm:ss")));
                            }
                            else if (field.key.Contains("AreaCode"))
                            {
                                whereClause.Add(string.Format("(AreaId3 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') )", field.key, field.value));
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
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }

                gridCollectionPager.EasyUIgridDoPage4Model<BuildingUnitBaseInfo_V>(filters, param, ref result, strConnectId);
                //gridCollectionPager.EasyUIgridDoPage<RoadBaseInfo>(BuilderFactory.DefaultBulder().List<RoadBaseInfo>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获得楼宇名称
        [WebInvoke(UriTemplate = "ListForEasyUIgrid2/{strConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<BuildingUnitBaseInfo> ListForEasyUIgrid2(string strConnectId, EasyUIgridCollectionParam<BuildingUnitBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<BuildingUnitBaseInfo> result = new EasyUIgridCollectionInvokeResult<BuildingUnitBaseInfo> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<BuildingUnitBaseInfo>(BuilderFactory.DefaultBulder(strConnectId).List<BuildingUnitBaseInfo>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 更改楼宇表中的单元数量
        [WebInvoke(UriTemplate = "RefreshStatistics/{MasterTable},{SlaveTable},{UpdateColumn},{PrimaryKey},{ForeignKey},{PrimaryKeyValue}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult RefreshStatistics(string MasterTable, string SlaveTable, string UpdateColumn, string PrimaryKey, string ForeignKey, string PrimaryKeyValue)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                SPParam spParam = new { }.ToSPParam();
                spParam["MasterTable"] = MasterTable;
                spParam["SlaveTable"] = SlaveTable;
                spParam["UpdateColumn"] = UpdateColumn;
                spParam["PrimaryKey"] = PrimaryKey;
                spParam["ForeignKey"] = ForeignKey;
                spParam["PrimaryKeyValue"] = PrimaryKeyValue;
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteSPNoneQuery("SP_DBA_RefreshStatistics", spParam);
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