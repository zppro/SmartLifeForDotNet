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
using SmartLife.Share.Models.WeiXin.Pub;

namespace SmartLife.CertManage.ManageServices.Oca
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MerchantServiceReserveService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<MerchantServiceReserve> List()
        {
            CollectionInvokeResult<MerchantServiceReserve> result = new CollectionInvokeResult<MerchantServiceReserve> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<MerchantServiceReserve>();
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
        public CollectionInvokeResult<MerchantServiceReserve> Query(string strParms)
        {
            CollectionInvokeResult<MerchantServiceReserve> result = new CollectionInvokeResult<MerchantServiceReserve> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<MerchantServiceReserve>(dictionary);
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
        public JqgridCollectionInvokeResult<MerchantServiceReserve> ListForJqgrid(JqgridCollectionParam<MerchantServiceReserve> param)
        {
            JqgridCollectionInvokeResult<MerchantServiceReserve> result = new JqgridCollectionInvokeResult<MerchantServiceReserve> { Success = true };
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

                gridCollectionPager.JqgridDoPage<MerchantServiceReserve>(BuilderFactory.DefaultBulder().List<MerchantServiceReserve>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<MerchantServiceReserve> ListForEasyUIgrid(EasyUIgridCollectionParam<MerchantServiceReserve> param)
        {
            EasyUIgridCollectionInvokeResult<MerchantServiceReserve> result = new EasyUIgridCollectionInvokeResult<MerchantServiceReserve> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<MerchantServiceReserve>(BuilderFactory.DefaultBulder().List<MerchantServiceReserve>(filters), param, ref result);
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
        public ModelInvokeResult<MerchantServiceReservePK> Create(MerchantServiceReserve merchantServiceReserve)
        {
            ModelInvokeResult<MerchantServiceReservePK> result = new ModelInvokeResult<MerchantServiceReservePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (merchantServiceReserve.Id == -1)
                {
                    merchantServiceReserve.Id = null;
                }
                /***********************begin 自定义代码*******************/
                merchantServiceReserve.OperatedBy = NormalSession.UserId.ToGuid();
                merchantServiceReserve.OperatedOn = DateTime.Now;
                merchantServiceReserve.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = merchantServiceReserve.GetCreateMethodName(), ParameterObject = merchantServiceReserve.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);

                result.instance = new MerchantServiceReservePK { Id = merchantServiceReserve.Id };
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
        public ModelInvokeResult<MerchantServiceReservePK> Update(string strId, MerchantServiceReserve merchantServiceReserve)
        {
            ModelInvokeResult<MerchantServiceReservePK> result = new ModelInvokeResult<MerchantServiceReservePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                merchantServiceReserve.OperatedBy = NormalSession.UserId.ToGuid();
                merchantServiceReserve.OperatedOn = DateTime.Now;
                merchantServiceReserve.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = merchantServiceReserve.GetUpdateMethodName(), ParameterObject = merchantServiceReserve.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MerchantServiceReservePK { Id = int.Parse(strId) };

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
        public ModelInvokeResult<MerchantServiceReservePK> Delete(string strId)
        {
            ModelInvokeResult<MerchantServiceReservePK> result = new ModelInvokeResult<MerchantServiceReservePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                MerchantServiceReservePK pk = new MerchantServiceReservePK { Id = int.Parse(strId) };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new MerchantServiceReserve().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MerchantServiceReservePK { Id = int.Parse(strId) };
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
                string statementName = new MerchantServiceReserve().GetDeleteMethodName();
                foreach (string strId in arrIds)
                {
                    MerchantServiceReservePK pk = new MerchantServiceReservePK { Id = int.Parse(strId) };
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, MerchantServiceReservePK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<MerchantServiceReserve> Load(string strId)
        {
            ModelInvokeResult<MerchantServiceReserve> result = new ModelInvokeResult<MerchantServiceReserve> { Success = true };
            try
            {
                var merchantServiceReserve = BuilderFactory.DefaultBulder().Load<MerchantServiceReserve, MerchantServiceReservePK>(new MerchantServiceReservePK { Id = int.Parse(strId) });
                result.instance = merchantServiceReserve;
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
        #region 创建 CreateOne
        [WebInvoke(UriTemplate = "CreateOne", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<MerchantServiceReservePK> CreateOne(MerchantServiceReserve merchantServiceReserve)
        {
            ModelInvokeResult<MerchantServiceReservePK> result = new ModelInvokeResult<MerchantServiceReservePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (merchantServiceReserve.Id == -1)
                {
                    merchantServiceReserve.Id = null;
                }
                /***********************begin 自定义代码*******************/
                merchantServiceReserve.OperatedBy = NormalSession.UserId.ToGuid();
                merchantServiceReserve.OperatedOn = DateTime.Now;
                merchantServiceReserve.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = merchantServiceReserve.GetCreateMethodName(), ParameterObject = merchantServiceReserve.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/

                int PK = int.Parse(BuilderFactory.DefaultBulder().Create("MerchantServiceReserve_CreateReturnId", merchantServiceReserve).ToString());

                result.instance = new MerchantServiceReservePK { Id = PK };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 创建 CreateAll
        [WebInvoke(UriTemplate = "CreateAll", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CreateAll(IList<MerchantServiceReserve> merchantServiceReserves)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                foreach (MerchantServiceReserve merchantServiceReserve in merchantServiceReserves)
                {
                    if (merchantServiceReserve.Id == -1)
                    {
                        merchantServiceReserve.Id = null;
                    }
                    /***********************begin 自定义代码*******************/
                    merchantServiceReserve.OperatedBy = NormalSession.UserId.ToGuid();
                    merchantServiceReserve.OperatedOn = DateTime.Now;
                    merchantServiceReserve.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = merchantServiceReserve.GetCreateMethodName(), ParameterObject = merchantServiceReserve.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    /***********************begin 自定义代码*******************/
                    /***********************此处添加自定义代码*****************/
                    /***********************end 自定义代码*********************/
                }
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
        #endregion

        #region 发送微信提醒
        [WebInvoke(UriTemplate = "RemindReserveInfo/{serveInfoItemId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult RemindReserveInfo(string serveInfoItemId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var spParam = new { ReserveInfoItemId = int.Parse(serveInfoItemId) }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_RemindServiceReserveInfo", spParam);
                if (spParam.ErrorCode != 0)
                {
                    result.ErrorCode = spParam.ErrorCode;
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

        #region 派单人员手工生成预约服务工单
        [WebInvoke(UriTemplate = "GenReserveWorkOrder", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult GenReserveWorkOrder(GenReserveWorkOrderV2Param param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var spParam = param.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_GenReserveWorkOrder", spParam);
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

    public class GenReserveWorkOrderV2Param
    {
        public Guid? StationId { get; set; }
        public Guid? OldManId { get; set; }
        public string AreaId3 { get; set; }
        public byte? GovTurnkeyFlag { get; set; }

    }
}