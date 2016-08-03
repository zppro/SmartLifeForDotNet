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
    public class EPay_PackageService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<EPay_Package> List()
        {
            CollectionInvokeResult<EPay_Package> result = new CollectionInvokeResult<EPay_Package> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<EPay_Package>();
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
        public CollectionInvokeResult<EPay_Package> Query(string strParms)
        {
            CollectionInvokeResult<EPay_Package> result = new CollectionInvokeResult<EPay_Package> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<EPay_Package>(dictionary);
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
        public JqgridCollectionInvokeResult<EPay_Package> ListForJqgrid(JqgridCollectionParam<EPay_Package> param)
        {
            JqgridCollectionInvokeResult<EPay_Package> result = new JqgridCollectionInvokeResult<EPay_Package> { Success = true };
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

                gridCollectionPager.JqgridDoPage<EPay_Package>(BuilderFactory.DefaultBulder().List<EPay_Package>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<EPay_Package> ListForEasyUIgrid(EasyUIgridCollectionParam<EPay_Package> param)
        {
            EasyUIgridCollectionInvokeResult<EPay_Package> result = new EasyUIgridCollectionInvokeResult<EPay_Package> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<EPay_Package>(BuilderFactory.DefaultBulder().List<EPay_Package>(filters), param, ref result);
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
        public ModelInvokeResult<EPay_PackagePK> Create(EPay_Package ePay)
        {
            ModelInvokeResult<EPay_PackagePK> result = new ModelInvokeResult<EPay_PackagePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (ePay.PackageId == GlobalManager.GuidAsAutoGenerate)
                {
                    ePay.PackageId = Guid.NewGuid();
                }
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
                result.instance = new EPay_PackagePK { PackageId = ePay.PackageId };
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
        [WebInvoke(UriTemplate = "{strPackageId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EPay_PackagePK> Update(string strPackageId, EPay_Package ePay)
        {
            ModelInvokeResult<EPay_PackagePK> result = new ModelInvokeResult<EPay_PackagePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _PackageId = strPackageId.ToGuid();
                if (_PackageId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ePay.PackageId = _PackageId;
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
                result.instance = new EPay_PackagePK { PackageId = _PackageId };

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
        [WebInvoke(UriTemplate = "{strPackageId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EPay_PackagePK> Delete(string strPackageId)
        {
            ModelInvokeResult<EPay_PackagePK> result = new ModelInvokeResult<EPay_PackagePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _PackageId = strPackageId.ToGuid();
                if (_PackageId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                EPay_PackagePK pk = new EPay_PackagePK { PackageId = _PackageId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new EPay_Package().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new EPay_PackagePK { PackageId = _PackageId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strPackageIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strPackageIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrPackageIds = strPackageIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrPackageIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new EPay_Package().GetDeleteMethodName();
                foreach (string strPackageId in arrPackageIds)
                {
                    EPay_PackagePK pk = new EPay_PackagePK { PackageId = strPackageId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strPackageId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<EPay_PackagePK> Nullify(string strPackageId)
        {
            ModelInvokeResult<EPay_PackagePK> result = new ModelInvokeResult<EPay_PackagePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _PackageId = strPackageId.ToGuid();
                if (_PackageId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                EPay_Package ePay = new EPay_Package { PackageId = _PackageId, Status = 0 };
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
                result.instance = new EPay_PackagePK { PackageId = _PackageId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strPackageIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strPackageIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrPackageIds = strPackageIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrPackageIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new EPay_Package().GetUpdateMethodName();
                foreach (string strPackageId in arrPackageIds)
                {
                    EPay_Package ePay = new EPay_Package { PackageId = strPackageId.ToGuid(), Status = 0 };
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, EPay_PackagePK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strPackageId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<EPay_Package> Load(string strPackageId)
        {
            ModelInvokeResult<EPay_Package> result = new ModelInvokeResult<EPay_Package> { Success = true };
            try
            {
                Guid? _PackageId = strPackageId.ToGuid();
                if (_PackageId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var ePay = BuilderFactory.DefaultBulder().Load<EPay_Package, EPay_PackagePK>(new EPay_PackagePK { PackageId = _PackageId });
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

        #region 扩展方法
        #region EasyUIgrid数据格式的列表 ListForEasyUIgridPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<EPay_Package> ListForEasyUIgridPage(EasyUIgridCollectionParam<EPay_Package> param)
        {
            EasyUIgridCollectionInvokeResult<EPay_Package> result = new EasyUIgridCollectionInvokeResult<EPay_Package> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage4Model<EPay_Package>(filters, param, ref result);

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

        #region 批量创建项目
        [WebInvoke(UriTemplate = "BatchCreateServeItem/{packageId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult BatchCreateServeItem(string packageId,IList<EPay_PackageItem> packageItems)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                statements.Add(new IBatisNetBatchStatement { StatementName = new EPay_PackageItem().GetDeleteMethodName(), ParameterObject = new { PackageId = packageId.ToGuid() }.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                foreach(var item in packageItems){
                    item.OperatedBy = NormalSession.UserId.ToGuid();
                    item.OperatedOn = DateTime.Now;
                    item.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    item.PackageId = packageId.ToGuid();
                    item.Id = null;
                    
                    statements.Add(new IBatisNetBatchStatement { StatementName = item.GetCreateMethodName(), ParameterObject = item.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
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

        #region Obsolete 套餐对象
        /* 
        #region 未分配对象 ListPackageObjectAsCanAssign
        [WebInvoke(UriTemplate = "ListPackageObjectAsCanAssign", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListPackageObjectAsCanAssign(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                
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
            
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Format("OldManName like '%{0}%' or InputCode1 like '{0}%' ", filters["k"]));
                }
               
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                } 

                gridCollectionPager.EasyUIgridDoPage<StringObjectDictionary>(BuilderFactory.DefaultBulder().ListStringObjectDictionary("EPay_PackageObject_ListAsCanAssign", filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 已分配对象 ListPackageObjectAsAssigned
        [WebInvoke(UriTemplate = "ListPackageObjectAsAssigned", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListPackageObjectAsAssigned(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                
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
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Format("OldManName like '%{0}%' or InputCode1 like '{0}%' ", filters["k"]));
                } 
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                } 

                gridCollectionPager.EasyUIgridDoPage<StringObjectDictionary>(BuilderFactory.DefaultBulder().ListStringObjectDictionary("EPay_PackageObject_ListAsAssigned", filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        
        #region 将所有未分配对象分配给指定的套餐
        [WebInvoke(UriTemplate = "AssignAll/{packageId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult AssignAll(string packageId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                string sql = "insert into Oca_EPay_PackageObject(OperatedBy,OperatedOn,DataSource,PackageId,ObjectId,ObjectName) select '" + NormalSession.UserId + "' as OperatedBy,getdate() as OperatedOn,'" + GlobalManager.DIKey_00012_ManualEdit + "' as DataSource, '" + packageId + "' as PackageId,x.OldManId as ObjectId,x.OldManName as ObjectName from Oca_OldManBaseInfo x inner join Oca_OldManConfigInfo y on x.OldManId = y.OldManId left join Oca_EPay_PackageObject z on y.OldManId=z.ObjectId where x.Status= 1 and y.GovTurnkeyFlag=1 and z.ObjectId is null";
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 将选中对象分配给指定套餐
        [WebInvoke(UriTemplate = "AssignSelected/{packageId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult AssignSelected(string packageId, IList<ObjectPlain> theObjectPlains)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                EPay_PackageObject po = new EPay_PackageObject() { PackageId = packageId.ToGuid(), OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now, DataSource = GlobalManager.DIKey_00012_ManualEdit };
                foreach (ObjectPlain o in theObjectPlains)
                {
                    po.ObjectId = o.ObjectId;
                    po.ObjectName = o.ObjectName;
                    statements.Add(new IBatisNetBatchStatement { StatementName = po.GetCreateMethodName(), ParameterObject = po.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
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

        
        #region 将当前套餐下所有的已分配对象设置为未分配
        [WebInvoke(UriTemplate = "UnAssignAll/{packageId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UnAssignAll(string packageId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                EPay_PackageObject po = new EPay_PackageObject() { PackageId = packageId.ToGuid() };
                statements.Add(new IBatisNetBatchStatement { StatementName = po.GetDeleteMethodName(), ParameterObject = po.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });

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

        #region 将当前套餐下选中的已分配对象设置为未分配
        [WebInvoke(UriTemplate = "UnAssignSelected/{packageId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UnAssignSelected(string packageId, IList<string> theObjectIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                EPay_PackageObject po = new EPay_PackageObject() { PackageId = packageId.ToGuid() };
                foreach (string objectId in theObjectIds)
                {
                    po.ObjectId = objectId;
                    statements.Add(new IBatisNetBatchStatement { StatementName = po.GetDeleteMethodName(), ParameterObject = po.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
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
        */
        #endregion

        #region 套餐for绑定
        [WebGet(UriTemplate = "ListForBinding", ResponseFormat = WebMessageFormat.Json)]
        public IList<EPay_Package> ListForBinding()
        {
            return BuilderFactory.DefaultBulder().List<EPay_Package>(new { Status = 1, WhereClause = "GETDATE() between PackageBeginDate and PackageEndDate", OrderByClause = "PackageName asc" }.ToStringObjectDictionary());
        }
        #endregion

        #region 选择套餐for绑定
        [WebGet(UriTemplate = "ListForBinding2/{PackageType}", ResponseFormat = WebMessageFormat.Json)]
        public IList<EPay_Package> ListForBinding2(string PackageType)
        {
            return BuilderFactory.DefaultBulder().List<EPay_Package>(new { Status = 1, WhereClause = "GETDATE() between PackageBeginDate and PackageEndDate and PackageType = " + PackageType, OrderByClause = "PackageName asc" }.ToStringObjectDictionary());
        }
        #endregion

        #region 个人充值选择套餐for绑定
        [WebGet(UriTemplate = "SelfListForBinding", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> SelfListForBinding()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("EPay_Package_SelfList", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        #endregion

        #region 余额信息
        [WebGet(UriTemplate = "GetRemains/{oldManId},{serveItemB}", ResponseFormat = WebMessageFormat.Json)] 
        public InvokeResult<EPay_OldManAccount> GetRemains(string oldManId, string serveItemB)
        {
            InvokeResult<EPay_OldManAccount> result = new InvokeResult<EPay_OldManAccount> { Success = true };
            try
            {
                result.ret = BuilderFactory.DefaultBulder().List<EPay_OldManAccount>(new EPay_OldManAccount { OldManId = oldManId.ToGuid(), ServeItemB = serveItemB }).FirstOrDefault();

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

    public class ObjectPlain
    {
        public string ObjectId { get; set; }
        public string ObjectName { get; set; }
    }
}