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

namespace SmartLife.CertManage.MerchantServices.Oca
{
    //[MerchantTokenValidate()]
    [MerchantServicesValidate(ApplicationIdFrom = "BP001", ApplicationIdTo = "IP005")]
    //[ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MerchantV2Service : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Merchant> List()
        {
            CollectionInvokeResult<Merchant> result = new CollectionInvokeResult<Merchant> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<Merchant>();
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
        public CollectionInvokeResult<Merchant> Query(string strParms)
        {
            CollectionInvokeResult<Merchant> result = new CollectionInvokeResult<Merchant> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<Merchant>(dictionary);
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
        public JqgridCollectionInvokeResult<Merchant> ListForJqgrid(JqgridCollectionParam<Merchant> param)
        {
            JqgridCollectionInvokeResult<Merchant> result = new JqgridCollectionInvokeResult<Merchant> { Success = true };
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

                gridCollectionPager.JqgridDoPage<Merchant>(BuilderFactory.DefaultBulder().List<Merchant>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<Merchant> ListForEasyUIgrid(EasyUIgridCollectionParam<Merchant> param)
        {
            EasyUIgridCollectionInvokeResult<Merchant> result = new EasyUIgridCollectionInvokeResult<Merchant> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<Merchant>(BuilderFactory.DefaultBulder().List<Merchant>(filters), param, ref result);
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
        public ModelInvokeResult<MerchantPK> Create(Merchant merchant)
        {
            ModelInvokeResult<MerchantPK> result = new ModelInvokeResult<MerchantPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (merchant.StationId == GlobalManager.GuidAsAutoGenerate)
                {
                    merchant.StationId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                merchant.OperatedBy = NormalSession.UserId.ToGuid();
                merchant.OperatedOn = DateTime.Now;
                merchant.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = merchant.GetCreateMethodName(), ParameterObject = merchant.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MerchantPK { StationId = merchant.StationId };
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
        public ModelInvokeResult<MerchantPK> Update(string strStationId, Merchant merchant)
        {
            ModelInvokeResult<MerchantPK> result = new ModelInvokeResult<MerchantPK> { Success = true };
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
                merchant.StationId = _StationId;
                /***********************begin 自定义代码*******************/
                merchant.OperatedBy = NormalSession.UserId.ToGuid();
                merchant.OperatedOn = DateTime.Now;
                merchant.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = merchant.GetUpdateMethodName(), ParameterObject = merchant.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MerchantPK { StationId = _StationId };

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
        public ModelInvokeResult<MerchantPK> Delete(string strStationId)
        {
            ModelInvokeResult<MerchantPK> result = new ModelInvokeResult<MerchantPK> { Success = true };
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
                MerchantPK pk = new MerchantPK { StationId = _StationId };
                //DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new Merchant().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MerchantPK { StationId = _StationId };
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
                string statementName = new Merchant().GetDeleteMethodName();
                foreach (string strStationId in arrStationIds)
                {
                    MerchantPK pk = new MerchantPK { StationId = strStationId.ToGuid() };
                    //DeleteCascade(statements, pk);
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

        #region 载入 Load
        [WebGet(UriTemplate = "{strStationId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Merchant> Load(string strStationId)
        {
            ModelInvokeResult<Merchant> result = new ModelInvokeResult<Merchant> { Success = true };
            try
            {
                Guid? _StationId = strStationId.ToGuid();
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var merchant = BuilderFactory.DefaultBulder().Load<Merchant, MerchantPK>(new MerchantPK { StationId = _StationId });
                result.instance = merchant;
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
        public InvokeResult SetServeItemsAndServeModes(string stationId, SetServeItemsAndServeModesV2Param param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                MerchantServeMode xMerchantServeMode = new MerchantServeMode { StationId = stationId.ToGuid(), DataSource = GlobalManager.DIKey_00012_ManualEdit };
                statements.Add(new IBatisNetBatchStatement { StatementName = xMerchantServeMode.GetDeleteMethodName(), ParameterObject = xMerchantServeMode.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL), Type = SqlExecuteType.DELETE });

                if (param.ServeMode != null && param.ServeMode.Count > 0)
                {
                    xMerchantServeMode.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    foreach (var serveMode in param.ServeMode)
                    {
                        xMerchantServeMode.ServeMode = serveMode;
                        statements.Add(new IBatisNetBatchStatement { StatementName = xMerchantServeMode.GetCreateMethodName(), ParameterObject = xMerchantServeMode.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL), Type = SqlExecuteType.INSERT });
                    }
                }

                MerchantServeItem xMerchantServeItem = new MerchantServeItem { StationId = stationId.ToGuid(), Status = param.Status };
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

        #endregion
    }

    public class SetServeItemsAndServeModesV2Param
    {
        public IList<string> ServeItemB { get; set; }//逗号隔开的多值
        public IList<string> ServeFee { get; set; }//逗号隔开的多值
        public byte Status { get; set; }//逗号隔开的多值
        public IList<string> ServeMode { get; set; }//逗号隔开的多值
    }
}

