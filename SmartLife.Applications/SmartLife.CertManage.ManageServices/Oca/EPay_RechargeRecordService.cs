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
    [ServiceLogging]
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EPay_RechargeRecordService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<EPay_RechargeRecord> List()
        {
            CollectionInvokeResult<EPay_RechargeRecord> result = new CollectionInvokeResult<EPay_RechargeRecord> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<EPay_RechargeRecord>();
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
        public CollectionInvokeResult<EPay_RechargeRecord> Query(string strParms)
        {
            CollectionInvokeResult<EPay_RechargeRecord> result = new CollectionInvokeResult<EPay_RechargeRecord> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<EPay_RechargeRecord>(dictionary);
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
        public JqgridCollectionInvokeResult<EPay_RechargeRecord> ListForJqgrid(JqgridCollectionParam<EPay_RechargeRecord> param)
        {
            JqgridCollectionInvokeResult<EPay_RechargeRecord> result = new JqgridCollectionInvokeResult<EPay_RechargeRecord> { Success = true };
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

                gridCollectionPager.JqgridDoPage<EPay_RechargeRecord>(BuilderFactory.DefaultBulder().List<EPay_RechargeRecord>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<EPay_RechargeRecord> ListForEasyUIgrid(EasyUIgridCollectionParam<EPay_RechargeRecord> param)
        {
            EasyUIgridCollectionInvokeResult<EPay_RechargeRecord> result = new EasyUIgridCollectionInvokeResult<EPay_RechargeRecord> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<EPay_RechargeRecord>(BuilderFactory.DefaultBulder().List<EPay_RechargeRecord>(filters), param, ref result);
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
        public ModelInvokeResult<EPay_RechargeRecordPK> Create(EPay_RechargeRecord ePay)
        {
            ModelInvokeResult<EPay_RechargeRecordPK> result = new ModelInvokeResult<EPay_RechargeRecordPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                /***********************begin 自定义代码*******************/
                ePay.OperatedBy = NormalSession.UserId.ToGuid();
                ePay.OperatedOn = DateTime.Now;
                ePay.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = ePay.GetCreateMethodName(), ParameterObject = ePay.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EPay_RechargeRecordPK { Id = ePay.Id };
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
        public ModelInvokeResult<EPay_RechargeRecordPK> Update(string strId, EPay_RechargeRecord ePay)
        {
            ModelInvokeResult<EPay_RechargeRecordPK> result = new ModelInvokeResult<EPay_RechargeRecordPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                ePay.OperatedBy = NormalSession.UserId.ToGuid();
                ePay.OperatedOn = DateTime.Now;
                ePay.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = ePay.GetUpdateMethodName(), ParameterObject = ePay.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EPay_RechargeRecordPK { Id = Convert.ToInt32(strId) };

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
        public ModelInvokeResult<EPay_RechargeRecordPK> Delete(string strId)
        {
            ModelInvokeResult<EPay_RechargeRecordPK> result = new ModelInvokeResult<EPay_RechargeRecordPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                EPay_RechargeRecordPK pk = new EPay_RechargeRecordPK { Id = Convert.ToInt32(strId) };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new EPay_RechargeRecord().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EPay_RechargeRecordPK { Id = Convert.ToInt32(strId) };
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
                string statementName = new EPay_RechargeRecord().GetDeleteMethodName();
                foreach (string strId in arrIds)
                {
                    EPay_RechargeRecordPK pk = new EPay_RechargeRecordPK { Id = Convert.ToInt32(strId) };
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
        public ModelInvokeResult<EPay_RechargeRecordPK> Nullify(string strId)
        {
            ModelInvokeResult<EPay_RechargeRecordPK> result = new ModelInvokeResult<EPay_RechargeRecordPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                EPay_RechargeRecord ePay = new EPay_RechargeRecord { Id = Convert.ToInt32(strId) };
                /***********************begin 自定义代码*******************/
                ePay.OperatedBy = NormalSession.UserId.ToGuid();
                ePay.OperatedOn = DateTime.Now;
                ePay.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = ePay.GetUpdateMethodName(), ParameterObject = ePay.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EPay_RechargeRecordPK { Id = Convert.ToInt32(strId) };
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
                string statementName = new EPay_RechargeRecord().GetUpdateMethodName();
                foreach (string strId in arrIds)
                {
                    EPay_RechargeRecord ePay = new EPay_RechargeRecord { Id = Convert.ToInt32(strId) };
                    /***********************begin 自定义代码*******************/
                    ePay.OperatedBy = NormalSession.UserId.ToGuid();
                    ePay.OperatedOn = DateTime.Now;
                    ePay.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = ePay.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, EPay_RechargeRecordPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<EPay_RechargeRecord> Load(string strId)
        {
            ModelInvokeResult<EPay_RechargeRecord> result = new ModelInvokeResult<EPay_RechargeRecord> { Success = true };
            try
            {
                var ePay = BuilderFactory.DefaultBulder().Load<EPay_RechargeRecord, EPay_RechargeRecordPK>(new EPay_RechargeRecordPK { Id = Convert.ToInt32(strId) });
                result.instance = ePay;
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

        #region 获取政府账户指定区间未分配对象 ListRechargeObjectAsCanAssign
        [WebInvoke(UriTemplate = "ListRechargeObjectAsCanAssign", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListRechargeObjectAsCanAssign(EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        filters[field.key] = field.value;
                    }
                }
                /**********************************************************/
                /***********************begin 模糊查询*********************/

                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Format("OldManName like '%{0}%' or InputCode1 like '{0}%' ", filters["k"]));
                }
                /**********************************************************/
                /***********************begin 排序*************************/
                /**********************************************************/
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                /***********************end 排序***************************/
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("ListRechargeObjectAsCanAssign", filters);
                result.total = result.rows.Count;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取个人账户指定区间未分配对象 ListRechargeObjectAsCanAssign2
        [WebInvoke(UriTemplate = "ListRechargeObjectAsCanAssign2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListRechargeObjectAsCanAssign2(EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        filters[field.key] = field.value;
                    }
                }
                /**********************************************************/
                /***********************begin 模糊查询*********************/

                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Format("OldManName like '%{0}%' or InputCode1 like '{0}%' ", filters["k"]));
                }
                /**********************************************************/
                /***********************begin 排序*************************/
                /**********************************************************/
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                /***********************end 排序***************************/
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("ListRechargeObjectAsCanAssign2", filters);
                result.total = result.rows.Count;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取个人账户指定区间已分配对象 ListRechargeObjectAsCanAssign3
        [WebInvoke(UriTemplate = "ListRechargeObjectAsCanAssign3", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListRechargeObjectAsCanAssign3(EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        filters[field.key] = field.value;
                    }
                }
                /**********************************************************/
                /***********************begin 模糊查询*********************/

                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Format("OldManName like '%{0}%' or InputCode1 like '{0}%' ", filters["k"]));
                }
                /**********************************************************/
                /***********************begin 排序*************************/
                /**********************************************************/
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                /***********************end 排序***************************/
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("ListRechargeObjectAsCanAssign3", filters);
                result.total = result.rows.Count;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 充值
        [WebInvoke(UriTemplate = "Recharge/{packageId},{accountNature},{periodId1},{periodId2}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult Recharge(string packageId, string periodId1, string accountNature, string periodId2, IList<ObjectPlain> rechargeObjects)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (rechargeObjects == null || rechargeObjects.Count == 0)
                {
                    //充值对象不存在
                    result.Success = false;
                    result.ErrorMessage = "请选择充值对象";
                    return result;
                }

                EPay_Package package = BuilderFactory.DefaultBulder().Load<EPay_Package, EPay_PackagePK>(new EPay_PackagePK { PackageId = packageId.ToGuid() });
                IList<EPay_PackageItem> packageItems = BuilderFactory.DefaultBulder().List<EPay_PackageItem>(new EPay_PackageItem { PackageId = packageId.ToGuid() });
                IList<EPay_OldManAccount> oldmanAccounts = BuilderFactory.DefaultBulder().List<EPay_OldManAccount>();

                if (packageItems.Count > 0)
                {
                    EPay_RechargeRecord rechargeRecord = new EPay_RechargeRecord
                    {
                        OperatedBy = NormalSession.UserId.ToGuid(),
                        OperatedOn = DateTime.Now,
                        DataSource = GlobalManager.DIKey_00012_ManualEdit,
                        RechargeType = "00001",
                        RechargeTypeId = packageId,
                        RechargeTypeName = package.PackageName,
                        AccountNature = accountNature,
                        PeriodId1 = periodId1,
                        PeriodId2 = periodId2
                    };//套餐
                    string period = "";
                    if (package.PeriodType == GlobalManager.DIKey_00014_Year || package.PeriodType == GlobalManager.DIKey_00014_HalfYear || package.PeriodType == GlobalManager.DIKey_00014_Quarter)
                    {
                        period = periodId1 + "-" + periodId2;
                    }
                    else if (package.PeriodType == GlobalManager.DIKey_00014_Month)
                    {
                        period = periodId1;
                    }
                    DictionaryItem accountNatureItem = BuilderFactory.DefaultBulder().Load<DictionaryItem, DictionaryItemPK>(new DictionaryItemPK { DictionaryId = "11024", ItemId = accountNature });

                    foreach (EPay_PackageItem packageItem in packageItems)
                    {
                        rechargeRecord.ServeItemB = packageItem.ServeItemB;
                        rechargeRecord.ServeItemBName = packageItem.ServeItemBName;
                        rechargeRecord.FeeType = packageItem.FeeType;
                        rechargeRecord.ChargeNum = packageItem.ChargeNum;

                        foreach (ObjectPlain rechargeObject in rechargeObjects)
                        {
                            rechargeRecord.ObjectId = rechargeObject.ObjectId;
                            rechargeRecord.ObjectName = rechargeObject.ObjectName;
                            rechargeRecord.RechargeTitle = "【" + package.PackageName + "】(" + period + ")" + rechargeObject.ObjectName + "  " + packageItem.ServeItemBName + " " + accountNatureItem.ItemName + "充值记录";
                            statements.Add(new IBatisNetBatchStatement { StatementName = rechargeRecord.GetCreateMethodName(), ParameterObject = rechargeRecord.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });

                            EPay_OldManAccount oldManAccount = oldmanAccounts.SingleOrDefault(item => item.OldManId == rechargeObject.ObjectId.ToGuid() && item.ServeItemB == packageItem.ServeItemB);
                            if (oldManAccount != null)
                            {
                                //该项目的账户存在
                                statements.Add(new IBatisNetBatchStatement { StatementName = oldManAccount.GetUpdateMethodName(), ParameterObject = new { Id = oldManAccount.Id, Remain = oldManAccount.Remain + packageItem.ChargeNum }.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                            }
                            else
                            {
                                //该项目的账户不存在
                                oldManAccount = new EPay_OldManAccount
                                {
                                    OperatedBy = NormalSession.UserId.ToGuid(),
                                    OperatedOn = DateTime.Now,
                                    DataSource = GlobalManager.DIKey_00012_ManualEdit,
                                    ServeItemB = packageItem.ServeItemB,
                                    ServeItemBName = packageItem.ServeItemBName,
                                    FeeType = packageItem.FeeType,
                                    Remain = packageItem.ChargeNum,
                                    OldManId = rechargeObject.ObjectId.ToGuid(),
                                    OldManName = rechargeObject.ObjectName,
                                    AccountNature = accountNature
                                };//老人账户

                                statements.Add(new IBatisNetBatchStatement { StatementName = oldManAccount.GetCreateMethodName(), ParameterObject = oldManAccount.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                            }
                        }
                    }
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

        #endregion

        #region 扩展接口
        #region 获取指定区间未分配对象 EPaySpendingList
        [WebInvoke(UriTemplate = "EPaySpendingList", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> EPaySpendingList(EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                    string item = "";
                    foreach (var field in param.fuzzyFields)
                    {
                        item = string.Format("{0} like '%{1}%'", field.key, field.value);
                        if (field.key == "ServeItemA")
                        {
                            if (field.value == "00001") { item = string.Format(" a.{0} = '{1}'", field.key, field.value); }
                            else if (field.value != "00001") { item = string.Format(" a.{0} <> '00001'", field.key, field.value); }
                        }
                        whereClause.Add(item);
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
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("EPaySpendingList", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取指定区间未分配对象 EPaySpendingListPage
        [WebInvoke(UriTemplate = "EPaySpendingListPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> EPaySpendingListPage(EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                    string item = "";
                    foreach (var field in param.filterFields)
                    {
                        if (field.key.Contains("AreaId2_Start") && field.value != "")
                        {
                            whereClause.Add(string.Format("a.AreaId2 = '{0}'", field.value));
                            //filters["AreaId2"] = field.value;
                        }
                        else if (field.key.Contains("a.AreaId3_Start") && field.value != "")
                        {
                            whereClause.Add(string.Format("AreaId3 = '{0}'", field.value));
                            //filters["AreaId3"] = field.value;
                        }
                        else if (field.key == "ServeItemA")
                        {
                            if (field.value == "00001") { item = string.Format(" a.{0} = '{1}'", field.key, field.value); }
                            else if (field.value != "00001") { item = string.Format(" a.{0} <> '00001'", field.key, field.value); }
                        }
                        else
                        {
                            filters[field.key] = field.value;
                        }
                        if (item != "")
                        {
                            whereClause.Add(item);
                        }
                    }
                }

                /***********************begin 模糊查询*********************/
                if (param.fuzzyFields != null)
                {
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
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

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("EPaySpending_ListByPager", filters, param, ref result);
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
        public EasyUIgridCollectionInvokeResult<EPay_RechargeRecord> ListForEasyUIgridPage(EasyUIgridCollectionParam<EPay_RechargeRecord> param)
        {
            EasyUIgridCollectionInvokeResult<EPay_RechargeRecord> result = new EasyUIgridCollectionInvokeResult<EPay_RechargeRecord> { Success = true };
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
                        DateTime parseTime = new DateTime();
                        if (field.key.Equals("OperatedOn_Start") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format("OperatedOn >= '{0}' ", field.value));
                        }
                        else if (field.key.Equals("OperatedOn_End") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format("OperatedOn <= '{0}' ", field.value));
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

                        fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
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
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4Model<EPay_RechargeRecord>(filters, param, ref result);

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