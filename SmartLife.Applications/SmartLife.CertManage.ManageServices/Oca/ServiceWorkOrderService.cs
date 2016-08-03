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
    public class ServiceWorkOrderService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceWorkOrder> List()
        {
            CollectionInvokeResult<ServiceWorkOrder> result = new CollectionInvokeResult<ServiceWorkOrder> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<ServiceWorkOrder>();
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
        public CollectionInvokeResult<ServiceWorkOrder> Query(string strParms)
        {
            CollectionInvokeResult<ServiceWorkOrder> result = new CollectionInvokeResult<ServiceWorkOrder> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<ServiceWorkOrder>(dictionary);
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
        public JqgridCollectionInvokeResult<ServiceWorkOrder> ListForJqgrid(JqgridCollectionParam<ServiceWorkOrder> param)
        {
            JqgridCollectionInvokeResult<ServiceWorkOrder> result = new JqgridCollectionInvokeResult<ServiceWorkOrder> { Success = true };
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

                gridCollectionPager.JqgridDoPage<ServiceWorkOrder>(BuilderFactory.DefaultBulder().List<ServiceWorkOrder>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<ServiceWorkOrder> ListForEasyUIgrid(EasyUIgridCollectionParam<ServiceWorkOrder> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceWorkOrder> result = new EasyUIgridCollectionInvokeResult<ServiceWorkOrder> { Success = true };
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
                /*
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }*/
                /***********************end 排序***************************/

                gridCollectionPager.EasyUIgridDoPage4Model<ServiceWorkOrder>(filters, param, ref result);
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
        public ModelInvokeResult<ServiceWorkOrderPK> Create(ServiceWorkOrder serviceWorkOrder)
        {
            ModelInvokeResult<ServiceWorkOrderPK> result = new ModelInvokeResult<ServiceWorkOrderPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (serviceWorkOrder.WorkOrderId == GlobalManager.GuidAsAutoGenerate)
                {
                    serviceWorkOrder.WorkOrderId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                serviceWorkOrder.OperatedBy = NormalSession.UserId.ToGuid();
                serviceWorkOrder.OperatedOn = DateTime.Now;
                serviceWorkOrder.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serviceWorkOrder.GetCreateMethodName(), ParameterObject = serviceWorkOrder.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceWorkOrderPK { WorkOrderId = serviceWorkOrder.WorkOrderId };
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
        [WebInvoke(UriTemplate = "{strWorkOrderId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceWorkOrderPK> Update(string strWorkOrderId, ServiceWorkOrder serviceWorkOrder)
        {
            ModelInvokeResult<ServiceWorkOrderPK> result = new ModelInvokeResult<ServiceWorkOrderPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _WorkOrderId = strWorkOrderId.ToGuid();
                if (_WorkOrderId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                serviceWorkOrder.WorkOrderId = _WorkOrderId;
                /***********************begin 自定义代码*******************/
                serviceWorkOrder.OperatedBy = NormalSession.UserId.ToGuid();
                serviceWorkOrder.OperatedOn = DateTime.Now;
                serviceWorkOrder.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serviceWorkOrder.GetUpdateMethodName(), ParameterObject = serviceWorkOrder.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceWorkOrderPK { WorkOrderId = _WorkOrderId };

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
        [WebInvoke(UriTemplate = "{strWorkOrderId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceWorkOrderPK> Delete(string strWorkOrderId)
        {
            ModelInvokeResult<ServiceWorkOrderPK> result = new ModelInvokeResult<ServiceWorkOrderPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _WorkOrderId = strWorkOrderId.ToGuid();
                if (_WorkOrderId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ServiceWorkOrderPK pk = new ServiceWorkOrderPK { WorkOrderId = _WorkOrderId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new ServiceWorkOrder().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceWorkOrderPK { WorkOrderId = _WorkOrderId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strWorkOrderIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strWorkOrderIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrWorkOrderIds = strWorkOrderIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrWorkOrderIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServiceWorkOrder().GetDeleteMethodName();
                foreach (string strWorkOrderId in arrWorkOrderIds)
                {
                    ServiceWorkOrderPK pk = new ServiceWorkOrderPK { WorkOrderId = strWorkOrderId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strWorkOrderId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServiceWorkOrderPK> Nullify(string strWorkOrderId)
        {
            ModelInvokeResult<ServiceWorkOrderPK> result = new ModelInvokeResult<ServiceWorkOrderPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _WorkOrderId = strWorkOrderId.ToGuid();
                if (_WorkOrderId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ServiceWorkOrder serviceWorkOrder = new ServiceWorkOrder { WorkOrderId = _WorkOrderId, Status = 0 };
                /***********************begin 自定义代码*******************/
                serviceWorkOrder.OperatedBy = NormalSession.UserId.ToGuid();
                serviceWorkOrder.OperatedOn = DateTime.Now;
                serviceWorkOrder.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serviceWorkOrder.GetUpdateMethodName(), ParameterObject = serviceWorkOrder.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServiceWorkOrderPK { WorkOrderId = _WorkOrderId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strWorkOrderIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strWorkOrderIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrWorkOrderIds = strWorkOrderIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrWorkOrderIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServiceWorkOrder().GetUpdateMethodName();
                foreach (string strWorkOrderId in arrWorkOrderIds)
                {
                    ServiceWorkOrder serviceWorkOrder = new ServiceWorkOrder { WorkOrderId = strWorkOrderId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    serviceWorkOrder.OperatedBy = NormalSession.UserId.ToGuid();
                    serviceWorkOrder.OperatedOn = DateTime.Now;
                    serviceWorkOrder.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = serviceWorkOrder.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ServiceWorkOrderPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strWorkOrderId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ServiceWorkOrder> Load(string strWorkOrderId)
        {
            ModelInvokeResult<ServiceWorkOrder> result = new ModelInvokeResult<ServiceWorkOrder> { Success = true };
            try
            {
                Guid? _WorkOrderId = strWorkOrderId.ToGuid();
                if (_WorkOrderId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var serviceWorkOrder = BuilderFactory.DefaultBulder().Load<ServiceWorkOrder, ServiceWorkOrderPK>(new ServiceWorkOrderPK { WorkOrderId = _WorkOrderId });
                result.instance = serviceWorkOrder;
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

        #region 获取整张工单上的信息
        [WebGet(UriTemplate = "GetWorkOrderInfo/{workOrderId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<StringObjectDictionary> GetWorkOrderInfo(string workOrderId)
        {
            InvokeResult<StringObjectDictionary> result = new InvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                result.ret = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderInfo", new { WorkOrderId = workOrderId }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取服务工单接单记录
        [WebGet(UriTemplate = "GetDispatchedServiceReceive/{workOrderId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetDispatchedServiceReceive(string workOrderId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetDispatchedServiceReceive", new { WorkOrderId = Guid.Parse(workOrderId) });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 指派最终的服务商家
        [WebInvoke(UriTemplate = "AssignTheFinalStation", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult AssignTheFinalStation(AssignTheFinalStationParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var spParam = param.ToSPParam(); 
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_WorkOrderAssignTheFinalStation", spParam);
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

        #region 将工单转督办
        [WebInvoke(UriTemplate = "UpToSupervise", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UpToSupervise(UpToSuperviseParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var spParam = param.ToSPParam();
                spParam["StationId"] = GlobalManager.CurrentIPCC_CallCenterId;
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_WorkOrderUpToSupervise", spParam);
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
         

        #region 关闭工单
        [WebInvoke(UriTemplate = "CloseWorkOrder", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CloseWorkOrder(CloseWorkOrderParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var spParam = param.ToSPParam();
                spParam["StationId"] = GlobalManager.CurrentIPCC_CallCenterId;
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_CloseWorkOrder", spParam);
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

        #region 坐席督办完成政府购买服务类工单
        [WebInvoke(UriTemplate = "SuperviseWorkOrderOfVouchBySeat", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SuperviseWorkOrderOfVouchBySeat(SuperviseWorkOrderOfVouchParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var spParam = param.ToSPParam();
                spParam["StationId"] = GlobalManager.CurrentIPCC_CallCenterId;
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_SuperviseWorkOrderOfVouchBySeat", spParam);
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

        #region 政府监管平台 查看商家服务的老人工单信息 EasyUIgrid数据格式的列表 ListForEasyUIgrid
        [WebInvoke(UriTemplate = "ListForEasyUIgridByStation", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ServiceWorkOrder> ListForEasyUIgridByStation(EasyUIgridCollectionParam<ServiceWorkOrder> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceWorkOrder> result = new EasyUIgridCollectionInvokeResult<ServiceWorkOrder> { Success = true };
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
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        if (field.key.Contains("OldManName") || field.key.Contains("Tel") || field.key.Contains("Mobile") || field.key.Contains("WorkOrderContent") || field.key.Contains("ServeManName"))
                        {
                            fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
                        else
                        {
                            whereClause.Add(string.Format("{0} >= '{1}'", field.key, field.value));
                        }
                    }
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP) ? " AND " : " " + param.fuzzyFieldOP + " "), fuzzys.ToArray()) + ")");
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

                gridCollectionPager.EasyUIgridDoPage4Model<ServiceWorkOrder>(filters, param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 每日服务清单
        [WebInvoke(UriTemplate = "GetWorkOrderForReport", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> GetWorkOrderForReport(EasyUIgridCollectionParam param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                List<string> whereClause = new List<string>();
                StringObjectDictionary filters = new StringObjectDictionary();
                string workOrderDoStatus = "";
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        if (field.key == "workOrderDoStatus")
                        {
                            workOrderDoStatus = field.value;
                        }
                        else if (field.key == "CheckInTime")
                        {
                            whereClause.Add("DATEDIFF(DAY,'" + field.value + "',a.CheckInTime)=0");
                            //filters["CheckInTime"] = Convert.ToDateTime(field.value);
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
                    foreach (var field in param.fuzzyFields)
                    {
                        whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                    }
                }
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                List<StringObjectDictionary> _rows = new List<StringObjectDictionary>();
                String[] sq = workOrderDoStatus.Split(',');
                for (var i = 0; i < sq.Length; i++)
                {
                    if (sq[i] == "1")
                    {
                        _rows.AddRange(BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderToResponse", filters));
                    }
                    if (sq[i] == "2")
                    {
                        _rows.AddRange(BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderProcess", filters));
                    }
                    if (sq[i] == "3")
                    {
                        _rows.AddRange(BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderProcessedServicing", filters));
                    }
                    if (sq[i] == "4")
                    {
                        _rows.AddRange(BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderProcessedServiced", filters));
                    }
                    if (sq[i] == "5")
                    {
                        _rows.AddRange(BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderCallBackFinished", filters));
                    }
                }
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


        #region 每日服务
        [WebInvoke(UriTemplate = "GetWorkOrderForReport2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ServiceListDaily_All_V> GetWorkOrderForReport2(EasyUIgridCollectionParam<ServiceListDaily_All_V> param)
        {
            EasyUIgridCollectionInvokeResult<ServiceListDaily_All_V> result = new EasyUIgridCollectionInvokeResult<ServiceListDaily_All_V> { Success = true };
            try
            {
                List<string> whereClause = new List<string>();
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                         if (field.key == "CheckInTime")
                        {
                            whereClause.Add("DATEDIFF(DAY,'" + field.value + "',CheckInTime)=0");
                            //filters["CheckInTime"] = Convert.ToDateTime(field.value);
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
                    foreach (var field in param.fuzzyFields)
                    {
                        if (field.key.Contains("DoStatus"))
                        {

                            whereClause.Add(string.Format("{0} in ({1})", field.key, "'"+field.value.Replace(",","','")+"'"));
                        }
                        else
                        {
                            whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
                    }
                }
                //String[] sq = workOrderDoStatus.Split(',');
                //List<string> fuzzys = new List<string>();
                //for (var i = 0; i < sq.Length; i++)
                //{
                //    fuzzys.Add(string.Format("DoStatus = '{0}'", sq[i]));
                //}
                //if (fuzzys.Count > 0)
                //{
                //    whereClause.Add("(" + string.Join(" or ", fuzzys.ToArray()) + ")");
                //}
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                gridCollectionPager.EasyUIgridDoPage4Model<ServiceListDaily_All_V>(filters, param, ref result);
                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取商家当日所有服务状态
        [WebInvoke(UriTemplate = "QueryTodayService", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CollectionInvokeResult<ServiceListDaily_All_V> QueryTodayService(ServiceListDaily_All_V serviceListDaily_All_V)
        {
            CollectionInvokeResult<ServiceListDaily_All_V> result = new CollectionInvokeResult<ServiceListDaily_All_V> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                filters = serviceListDaily_All_V.ToStringObjectDictionary(false);

                List<string> whereClause = new List<string>();
                if (!string.IsNullOrEmpty(serviceListDaily_All_V.DoStatus)) {
                    string[] doStatus = serviceListDaily_All_V.DoStatus.Split(',');
                    foreach (var item in doStatus) {
                        whereClause.Add(string.Format(" '{0} '", item.ToString()));
                    }
                }

                filters.Add("WhereClause", " DATEDIFF(D,CheckInTime,GETDATE())=0 ");
                if (whereClause.Count > 0)
                {
                    filters.Remove("DoStatus");
                    filters["WhereClause"] += " and DoStatus in(" + string.Join(" , ", whereClause.ToArray()) + ")";
                }
                filters.Add("OrderByClause", " CheckInTime desc ");
                result.rows = BuilderFactory.DefaultBulder().List<ServiceListDaily_All_V>(filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 大屏幕调用商家每日服务信息
        [WebGet(UriTemplate = "LoadMerchantServicesForBigScreen/{serveStationId},{scrollDate},{id},{timeSpanOfHour}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadServicesForBigScreenStreet(string serveStationId, string scrollDate, string id, string timeSpanOfHour)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { ServeStationId = serveStationId, ScrollDate = scrollDate, TimeSpanOfHour = Convert.ToInt32(timeSpanOfHour) }.ToStringObjectDictionary();
                int Id;
                if (int.TryParse(id, out Id))
                {
                    filters.Add("Id", Id);
                }
                else
                {
                    filters.Add("Id", 0);
                }
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetMerchantServeForBigScreen", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        } 
        #endregion
         
        #region 大屏幕调用商家每日服务信息统计数据
        [WebGet(UriTemplate = "LoadMerchantServicesCountForBigScreen/{serveStationId},{scrollDate}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadMerchantServicesCountForBigScreen(string serveStationId, string scrollDate)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { ServeStationId = serveStationId, ScrollDate = scrollDate }.ToStringObjectDictionary();

                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetMerchantServeCountForBigScreen", filters);
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

    public class AssignTheFinalStationParam
    { 
        public Guid? WorkOrderId { get; set; }
        public Guid? StationId { get; set; } 
    }

    public class UpToSuperviseParam
    { 
        public Guid? WorkOrderId { get; set; }
    } 

    public class CloseWorkOrderParam
    { 
        public Guid? WorkOrderId { get; set; }
        public string ServeManArriveTime { get; set; }
        public string ServeManLeaveTime { get; set; }
        public string FeedbackToServiceStation { get; set; }
        public string FeedbackRemarkToServiceStation { get; set; }
        public string ServeFinalResult { get; set; }
        public string ServeFinalResultRemark { get; set; }
    }

    public class SuperviseWorkOrderOfVouchParam
    {
        public Guid? WorkOrderId { get; set; }
        public string ServeManName { get; set; }
        public string ServeEndTime { get; set; }
        public string ServeItemB { get; set; }
        public string ServeFinalResult { get; set; }
        public string ServeFinalResultRemark { get; set; }
    }
}