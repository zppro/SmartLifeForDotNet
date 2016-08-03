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
    public class MerchantServeManMappingOldManService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<MerchantServeManMappingOldMan> List()
        {
            CollectionInvokeResult<MerchantServeManMappingOldMan> result = new CollectionInvokeResult<MerchantServeManMappingOldMan> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<MerchantServeManMappingOldMan>();
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
        public CollectionInvokeResult<MerchantServeManMappingOldMan> Query(string strParms)
        {
            CollectionInvokeResult<MerchantServeManMappingOldMan> result = new CollectionInvokeResult<MerchantServeManMappingOldMan> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<MerchantServeManMappingOldMan>(dictionary);
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
        public JqgridCollectionInvokeResult<MerchantServeManMappingOldMan> ListForJqgrid(JqgridCollectionParam<MerchantServeManMappingOldMan> param)
        {
            JqgridCollectionInvokeResult<MerchantServeManMappingOldMan> result = new JqgridCollectionInvokeResult<MerchantServeManMappingOldMan> { Success = true };
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

                gridCollectionPager.JqgridDoPage<MerchantServeManMappingOldMan>(BuilderFactory.DefaultBulder().List<MerchantServeManMappingOldMan>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<MerchantServeManMappingOldMan> ListForEasyUIgrid(EasyUIgridCollectionParam<MerchantServeManMappingOldMan> param)
        {
            EasyUIgridCollectionInvokeResult<MerchantServeManMappingOldMan> result = new EasyUIgridCollectionInvokeResult<MerchantServeManMappingOldMan> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<MerchantServeManMappingOldMan>(BuilderFactory.DefaultBulder().List<MerchantServeManMappingOldMan>(filters), param, ref result);
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
        public ModelInvokeResult<MerchantServeManMappingOldManPK> Create(MerchantServeManMappingOldMan merchantServeManMappingOldMan)
        {
            ModelInvokeResult<MerchantServeManMappingOldManPK> result = new ModelInvokeResult<MerchantServeManMappingOldManPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>(); 
                /***********************begin 自定义代码*******************/
                merchantServeManMappingOldMan.OperatedBy = NormalSession.UserId.ToGuid();
                merchantServeManMappingOldMan.OperatedOn = DateTime.Now;
                merchantServeManMappingOldMan.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = merchantServeManMappingOldMan.GetCreateMethodName(), ParameterObject = merchantServeManMappingOldMan.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MerchantServeManMappingOldManPK { Id = merchantServeManMappingOldMan.Id };
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
        public ModelInvokeResult<MerchantServeManMappingOldManPK> Update(string strId, MerchantServeManMappingOldMan merchantServeManMappingOldMan)
        {
            ModelInvokeResult<MerchantServeManMappingOldManPK> result = new ModelInvokeResult<MerchantServeManMappingOldManPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                merchantServeManMappingOldMan.OperatedBy = NormalSession.UserId.ToGuid();
                merchantServeManMappingOldMan.OperatedOn = DateTime.Now;
                merchantServeManMappingOldMan.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = merchantServeManMappingOldMan.GetUpdateMethodName(), ParameterObject = merchantServeManMappingOldMan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MerchantServeManMappingOldManPK { Id =int.Parse( strId) };

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
        public ModelInvokeResult<MerchantServeManMappingOldManPK> Delete(string strId)
        {
            ModelInvokeResult<MerchantServeManMappingOldManPK> result = new ModelInvokeResult<MerchantServeManMappingOldManPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                MerchantServeManMappingOldManPK pk = new MerchantServeManMappingOldManPK { Id = int.Parse(strId) };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new MerchantServeManMappingOldMan().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MerchantServeManMappingOldManPK { Id = int.Parse(strId) };
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
                string statementName = new MerchantServeManMappingOldMan().GetDeleteMethodName();
                foreach (string strId in arrIds)
                {
                    MerchantServeManMappingOldManPK pk = new MerchantServeManMappingOldManPK { Id = int.Parse(strId) };
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
        public ModelInvokeResult<MerchantServeManMappingOldManPK> Nullify(string strId)
        {
            ModelInvokeResult<MerchantServeManMappingOldManPK> result = new ModelInvokeResult<MerchantServeManMappingOldManPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                MerchantServeManMappingOldMan merchantServeManMappingOldMan = new MerchantServeManMappingOldMan { Id = int.Parse(strId)  };
                /***********************begin 自定义代码*******************/
                merchantServeManMappingOldMan.OperatedBy = NormalSession.UserId.ToGuid();
                merchantServeManMappingOldMan.OperatedOn = DateTime.Now;
                merchantServeManMappingOldMan.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = merchantServeManMappingOldMan.GetUpdateMethodName(), ParameterObject = merchantServeManMappingOldMan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MerchantServeManMappingOldManPK { Id = int.Parse(strId) };
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
                string statementName = new MerchantServeManMappingOldMan().GetUpdateMethodName();
                foreach (string strId in arrIds)
                {
                    MerchantServeManMappingOldMan merchantServeManMappingOldMan = new MerchantServeManMappingOldMan { Id = int.Parse(strId) };
                    /***********************begin 自定义代码*******************/
                    merchantServeManMappingOldMan.OperatedBy = NormalSession.UserId.ToGuid();
                    merchantServeManMappingOldMan.OperatedOn = DateTime.Now;
                    merchantServeManMappingOldMan.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = merchantServeManMappingOldMan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, MerchantServeManMappingOldManPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<MerchantServeManMappingOldMan> Load(string strId)
        {
            ModelInvokeResult<MerchantServeManMappingOldMan> result = new ModelInvokeResult<MerchantServeManMappingOldMan> { Success = true };
            try
            {
                var merchantServeManMappingOldMan = BuilderFactory.DefaultBulder().Load<MerchantServeManMappingOldMan, MerchantServeManMappingOldManPK>(new MerchantServeManMappingOldManPK { Id = int.Parse(strId) });
                result.instance = merchantServeManMappingOldMan;
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

        #region  自定义
        
        #region 批量新增
        [WebInvoke(UriTemplate = "SaveServeManOldMan", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SaveServeManOldMan(IList<MerchantServeManMappingOldMan> merchantServeManMappingOldMans)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                if (merchantServeManMappingOldMans == null || merchantServeManMappingOldMans.Count == 0)
                {
                    //配置老人不存在
                    result.Success = false;
                    result.ErrorMessage = "服务人员或老人为空";
                    return result; 
                } 
                foreach (MerchantServeManMappingOldMan item in merchantServeManMappingOldMans)
                {
                    if ((item.ServeManId != null || item.ServeManId != "") && (item.OldManId != null || item.OldManId != "") && (item.AreaId != null || item.AreaId != ""))
                    {
                        item.OperatedBy = NormalSession.UserId.ToGuid();
                        item.OperatedOn = DateTime.Now;
                        item.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                        statements.Add(new IBatisNetBatchStatement { StatementName = item.GetCreateMethodName(), ParameterObject = item.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }
                    else
                    {
                        //配置老人不存在
                        result.Success = false;
                        result.ErrorMessage = "服务人员或老人或社区为空";
                        return result;                     
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
          
        #region 批量删除
        [WebInvoke(UriTemplate = "RemoveServeManOldMan", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult RemoveServeManOldMan(IList<MerchantServeManMappingOldMan> merchantServeManMappingOldMans)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                if (merchantServeManMappingOldMans == null || merchantServeManMappingOldMans.Count == 0)
                {
                    //配置老人不存在
                    result.Success = false;
                    result.ErrorMessage = "服务人员或老人为空";
                    return result;
                }
                foreach (MerchantServeManMappingOldMan item in merchantServeManMappingOldMans)
                {
                    if ((item.ServeManId != null || item.ServeManId != "") && (item.OldManId != null || item.OldManId != "") && (item.AreaId != null || item.AreaId != ""))
                    { 
                        statements.Add(new IBatisNetBatchStatement { StatementName = item.GetDeleteMethodName(), ParameterObject = item.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                    }
                    else
                    {
                        //配置老人不存在
                        result.Success = false;
                        result.ErrorMessage = "服务人员或老人或社区为空";
                        return result;
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
}

