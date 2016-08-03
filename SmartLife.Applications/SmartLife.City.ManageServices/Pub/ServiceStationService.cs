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

namespace SmartLife.City.ManageServices.Pub
{ 
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServiceStationService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceStation> List()
        {
            CollectionInvokeResult<ServiceStation> result = new CollectionInvokeResult<ServiceStation> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<ServiceStation>();
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
        public CollectionInvokeResult<ServiceStation> Query(string strParms)
        {
            CollectionInvokeResult<ServiceStation> result = new CollectionInvokeResult<ServiceStation> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<ServiceStation>(dictionary);
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
        public JqgridCollectionInvokeResult<ServiceStation> ListForJqgrid(JqgridCollectionParam<ServiceStation> param)
        {
            JqgridCollectionInvokeResult<ServiceStation> result = new JqgridCollectionInvokeResult<ServiceStation> { Success = true };
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

                gridCollectionPager.JqgridDoPage<ServiceStation>(BuilderFactory.DefaultBulder().List<ServiceStation>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<ServiceStation> ListForEasyUIgrid(EasyUIgridCollectionParam<ServiceStation> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceStation> result = new EasyUIgridCollectionInvokeResult<ServiceStation> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<ServiceStation>(BuilderFactory.DefaultBulder().List<ServiceStation>(filters), param, ref result);
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
        public ModelInvokeResult<ServiceStationPK> Create(ServiceStation serviceStation)
        {
            ModelInvokeResult<ServiceStationPK> result = new ModelInvokeResult<ServiceStationPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (serviceStation.StationId == GlobalManager.GuidAsAutoGenerate)
                {
                    serviceStation.StationId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                serviceStation.OperatedBy = NormalSession.UserId.ToGuid();
                serviceStation.OperatedOn = DateTime.Now;
                serviceStation.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                var param = serviceStation.ToStringObjectDictionary(false);
                if (string.IsNullOrEmpty(serviceStation.AreaId2))
                {
                    param["AreaId2"] = DBNull.Value;
                }
                if (string.IsNullOrEmpty(serviceStation.AreaId3))
                {
                    param["AreaId3"] = DBNull.Value;
                }
                /***********************end 自定义代码*********************/

                statements.Add(new IBatisNetBatchStatement { StatementName = serviceStation.GetCreateMethodName(), ParameterObject = param, Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceStationPK { StationId = serviceStation.StationId };
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
        [WebInvoke(UriTemplate = "{strStationId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceStationPK> Update(string strStationId, ServiceStation serviceStation)
        {
            ModelInvokeResult<ServiceStationPK> result = new ModelInvokeResult<ServiceStationPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _StationId = strStationId.ToGuid();
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                serviceStation.StationId = _StationId;
                /***********************begin 自定义代码*******************/
                serviceStation.OperatedBy = NormalSession.UserId.ToGuid();
                serviceStation.OperatedOn = DateTime.Now;
                serviceStation.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                var param = serviceStation.ToStringObjectDictionary(false);
                if (string.IsNullOrEmpty(serviceStation.AreaId2))
                {
                    param["AreaId2"] = DBNull.Value;
                }
                if (string.IsNullOrEmpty(serviceStation.AreaId3))
                {
                    param["AreaId3"] = DBNull.Value;
                }
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serviceStation.GetUpdateMethodName(), ParameterObject = param, Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceStationPK { StationId = _StationId };

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
        [WebInvoke(UriTemplate = "{strStationId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceStationPK> Delete(string strStationId)
        {
            ModelInvokeResult<ServiceStationPK> result = new ModelInvokeResult<ServiceStationPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _StationId = strStationId.ToGuid();
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ServiceStationPK pk = new ServiceStationPK { StationId = _StationId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new ServiceStation().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceStationPK { StationId = _StationId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strStationIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strStationIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrStationIds = strStationIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrStationIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServiceStation().GetDeleteMethodName();
                foreach (string strStationId in arrStationIds)
                {
                    ServiceStationPK pk = new ServiceStationPK { StationId = strStationId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strStationId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceStationPK> Nullify(string strStationId)
        {
            ModelInvokeResult<ServiceStationPK> result = new ModelInvokeResult<ServiceStationPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _StationId = strStationId.ToGuid();
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ServiceStation serviceStation = new ServiceStation { StationId = _StationId, Status = 0 };
                /***********************begin 自定义代码*******************/
                serviceStation.OperatedBy = NormalSession.UserId.ToGuid();
                serviceStation.OperatedOn = DateTime.Now;
                serviceStation.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serviceStation.GetUpdateMethodName(), ParameterObject = serviceStation.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceStationPK { StationId = _StationId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strStationIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strStationIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrStationIds = strStationIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrStationIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServiceStation().GetUpdateMethodName();
                foreach (string strStationId in arrStationIds)
                {
                    ServiceStation serviceStation = new ServiceStation { StationId = strStationId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    serviceStation.OperatedBy = NormalSession.UserId.ToGuid();
                    serviceStation.OperatedOn = DateTime.Now;
                    serviceStation.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = serviceStation.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ServiceStationPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strStationId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ServiceStation> Load(string strStationId)
        {
            ModelInvokeResult<ServiceStation> result = new ModelInvokeResult<ServiceStation> { Success = true };
            try
            {
                Guid? _StationId = strStationId.ToGuid();
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var serviceStation = BuilderFactory.DefaultBulder().Load<ServiceStation, ServiceStationPK>(new ServiceStationPK { StationId = _StationId });
                result.instance = serviceStation;
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

        #region 获取服务站类型2
        [WebGet(UriTemplate = "GetStationType2/{stationType}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictionaryItem> GetStationType2(string stationType)
        {
            return BuilderFactory.DefaultBulder().List<DictionaryItem>("GetStationType2", new { StationType = stationType }.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL));
        }
        #endregion

        #region 获取商家对应的服务项目
        [WebGet(UriTemplate = "GetServeItems/{stationId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<MerchantServeItem> GetServeItems(string stationId)
        {
            return BuilderFactory.DefaultBulder().List<MerchantServeItem>(new MerchantServeItem { StationId = stationId.ToGuid() });
        }
        #endregion

        
        #region 获取商家对应的服务方式
        [WebGet(UriTemplate = "GetServeModes/{stationId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<MerchantServeMode> GetServeModes(string stationId)
        {
            return BuilderFactory.DefaultBulder().List<MerchantServeMode>(new MerchantServeMode { StationId = stationId.ToGuid() });
        }
        #endregion

        #region 设置商家对应的服务项目和服务方式
        [WebInvoke(UriTemplate = "SetServeItemsAndServeModes/{stationId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SetServeItemsAndServeModes(string stationId, SetServeItemsAndServeModesParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                MerchantServeMode xMerchantServeMode = new MerchantServeMode { StationId = stationId.ToGuid(), DataSource =  GlobalManager.DIKey_00012_ManualEdit};
                statements.Add(new IBatisNetBatchStatement { StatementName = xMerchantServeMode.GetDeleteMethodName(), ParameterObject = xMerchantServeMode.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL), Type = SqlExecuteType.DELETE});

                if (param.ServeMode != null && param.ServeMode.Count > 0)
                {
                    xMerchantServeMode.DataSource = GlobalManager.DIKey_00012_ManualEdit; 
                    foreach (var serveMode in param.ServeMode)
                    {
                        xMerchantServeMode.ServeMode = serveMode;
                        statements.Add(new IBatisNetBatchStatement { StatementName = xMerchantServeMode.GetCreateMethodName(), ParameterObject = xMerchantServeMode.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL), Type = SqlExecuteType.INSERT });
                    }
                }

                MerchantServeItem xMerchantServeItem = new MerchantServeItem { StationId = stationId.ToGuid() };
                statements.Add(new IBatisNetBatchStatement { StatementName = xMerchantServeItem.GetDeleteMethodName(), ParameterObject = xMerchantServeItem.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL), Type = SqlExecuteType.DELETE });

                if (param.ServeItemB != null && param.ServeItemB.Count > 0)
                {
                    xMerchantServeItem.DataSource = GlobalManager.DIKey_00012_ManualEdit; 
                    //两者一样的数量
                    for (int i = 0; i < param.ServeItemB.Count; i++)
                    {
                        xMerchantServeItem.ServeItemB = param.ServeItemB[i];
                        xMerchantServeItem.ServeFee = decimal.Parse(param.ServeFee[i]);
                        statements.Add(new IBatisNetBatchStatement { StatementName = xMerchantServeItem.GetCreateMethodName(), ParameterObject = xMerchantServeItem.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL), Type = SqlExecuteType.INSERT });
                    }
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

        #region 获取商家服务期间  
        [WebInvoke(UriTemplate = "GetServePeriodsForEasyUIgrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> GetServePeriodsForEasyUIgrid(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            { 
                gridCollectionPager.EasyUIgridDoPage<StringObjectDictionary>(BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetMerchantServePeriods"), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        } 

        #endregion

        #region 添加政府购买商家服务期间
        [WebInvoke(UriTemplate = "AddMerchantServePeriod", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult AddMerchantServePeriod(AddMerchantServePeriodParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var spParam = param.ToSPParam(); 
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_AddMerchantServePeriod", spParam);
                if (spParam.ErrorCode != 0)
                {
                    result.ErrorCode = spParam.ErrorCode;
                    result.ErrorMessage = spParam.ErrorMessage;
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

        #endregion

    }

    public class SetServeItemsAndServeModesParam
    {
        public IList<string> ServeItemB { get; set; }//逗号隔开的多值
        public IList<string> ServeFee { get; set; }//逗号隔开的多值
        public IList<string> ServeMode { get; set; }//逗号隔开的多值
    }

    public class AddMerchantServePeriodParam
    {
        public Guid? StationId { get; set; }
        public string EndDate { get; set; }
    }
}