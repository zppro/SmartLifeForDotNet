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
using SmartLife.Share.Models.Pam;

namespace SmartLife.CertManage.ManageServices.Pam
{
    [ServiceLogging]
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServeManService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServeMan> List()
        {
            CollectionInvokeResult<ServeMan> result = new CollectionInvokeResult<ServeMan> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<ServeMan>();
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
        public CollectionInvokeResult<ServeMan> Query(string strParms)
        {
            CollectionInvokeResult<ServeMan> result = new CollectionInvokeResult<ServeMan> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<ServeMan>(dictionary);
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
        public JqgridCollectionInvokeResult<ServeMan> ListForJqgrid(JqgridCollectionParam<ServeMan> param)
        {
            JqgridCollectionInvokeResult<ServeMan> result = new JqgridCollectionInvokeResult<ServeMan> { Success = true };
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

                gridCollectionPager.JqgridDoPage<ServeMan>(BuilderFactory.DefaultBulder().List<ServeMan>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<ServeMan> ListForEasyUIgrid(EasyUIgridCollectionParam<ServeMan> param)
        {
            EasyUIgridCollectionInvokeResult<ServeMan> result = new EasyUIgridCollectionInvokeResult<ServeMan> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<ServeMan>(BuilderFactory.DefaultBulder().List<ServeMan>(filters), param, ref result);
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
        public ModelInvokeResult<ServeManPK> Create(ServeMan serveMan)
        {
            ModelInvokeResult<ServeManPK> result = new ModelInvokeResult<ServeManPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (serveMan.UserId == GlobalManager.GuidAsAutoGenerate)
                {
                    serveMan.UserId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                serveMan.OperatedBy = NormalSession.UserId.ToGuid();
                serveMan.OperatedOn = DateTime.Now; 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serveMan.GetCreateMethodName(), ParameterObject = serveMan.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServeManPK { UserId = serveMan.UserId };
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
        [WebInvoke(UriTemplate = "{strUserId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServeManPK> Update(string strUserId, ServeMan serveMan)
        {
            ModelInvokeResult<ServeManPK> result = new ModelInvokeResult<ServeManPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _UserId = strUserId.ToGuid();
                if (_UserId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                serveMan.UserId = _UserId;
                /***********************begin 自定义代码*******************/
                serveMan.OperatedBy = NormalSession.UserId.ToGuid();
                serveMan.OperatedOn = DateTime.Now; 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serveMan.GetUpdateMethodName(), ParameterObject = serveMan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServeManPK { UserId = _UserId };

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
        [WebInvoke(UriTemplate = "{strUserId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServeManPK> Delete(string strUserId)
        {
            ModelInvokeResult<ServeManPK> result = new ModelInvokeResult<ServeManPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _UserId = strUserId.ToGuid();
                if (_UserId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ServeManPK pk = new ServeManPK { UserId = _UserId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new ServeMan().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServeManPK { UserId = _UserId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strUserIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strUserIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrUserIds = strUserIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrUserIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServeMan().GetDeleteMethodName();
                foreach (string strUserId in arrUserIds)
                {
                    ServeManPK pk = new ServeManPK { UserId = strUserId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strUserId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServeManPK> Nullify(string strUserId)
        {
            ModelInvokeResult<ServeManPK> result = new ModelInvokeResult<ServeManPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _UserId = strUserId.ToGuid();
                if (_UserId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ServeMan serveMan = new ServeMan { UserId = _UserId };
                /***********************begin 自定义代码*******************/
                serveMan.OperatedBy = NormalSession.UserId.ToGuid();
                serveMan.OperatedOn = DateTime.Now; 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = serveMan.GetUpdateMethodName(), ParameterObject = serveMan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServeManPK { UserId = _UserId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strUserIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strUserIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrUserIds = strUserIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrUserIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ServeMan().GetUpdateMethodName();
                foreach (string strUserId in arrUserIds)
                {
                    ServeMan serveMan = new ServeMan { UserId = strUserId.ToGuid()  };
                    /***********************begin 自定义代码*******************/
                    serveMan.OperatedBy = NormalSession.UserId.ToGuid();
                    serveMan.OperatedOn = DateTime.Now; 
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = serveMan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ServeManPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strUserId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ServeMan> Load(string strUserId)
        {
            ModelInvokeResult<ServeMan> result = new ModelInvokeResult<ServeMan> { Success = true };
            try
            {
                Guid? _UserId = strUserId.ToGuid();
                if (_UserId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var serveMan = BuilderFactory.DefaultBulder().Load<ServeMan, ServeManPK>(new ServeManPK { UserId = _UserId });
                result.instance = serveMan;
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
        #region  设置呼叫器号码
        [WebInvoke(UriTemplate = "SetServeManCallNo/{strUserId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServeManPK> SetServeManCallNo(string strUserId, ServeMan serveMan)
        {
            ModelInvokeResult<ServeManPK> result = new ModelInvokeResult<ServeManPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _UserId = strUserId.ToGuid();
                if (_UserId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                var count = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(" select * from Pam_ServeMan where UserId='" + _UserId + "'");
                serveMan.UserId = _UserId;
                /***********************begin 自定义代码*******************/
                serveMan.OperatedBy = NormalSession.UserId.ToGuid();
                serveMan.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                if (count > 0)
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = serveMan.GetUpdateMethodName(), ParameterObject = serveMan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                else
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = serveMan.GetCreateMethodName(), ParameterObject = serveMan.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServeManPK { UserId = _UserId };

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region   ServiceManListForEasyUIgridByStationId 根据机构Id查询服务人员，并且把已经和某个老人关联的排在前面
        [WebInvoke(UriTemplate = "ServiceManListForEasyUIgridByStationId", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ServiceManListForEasyUIgridByStationId(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        if (field.key == "OldManId" || field.key == "StationId")
                        {
                            if (field.value.ToGuid() == null)
                            {
                                result.Success = false;
                                result.ErrorMessage = "未选择机构或者老人";
                                return result;
                            }
                            else
                            {
                                filters[field.key] = field.value;
                            }
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
                /**********************************************************/
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("ServiceManListForMapping", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

         
        #region 新增老人和服务人员的关系 
        [WebInvoke(UriTemplate = "OldManMappingServeManAdd?parms={strParms}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<OldManMappingServeManPK> OldManMappingServeManAdd(string strParms)
        {
            ModelInvokeResult<OldManMappingServeManPK> result = new ModelInvokeResult<OldManMappingServeManPK> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                string oldManId = dictionary["OldManId"].ToString();
                string userIds = dictionary["UserIds"].ToString();
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                /***********************end 自定义代码*********************/
                string[] arruserIds = userIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);  
                 
                if (arruserIds.Length > 0)
                {
                    foreach (string arruserId in arruserIds)
                    {
                        OldManMappingServeMan oldManMappingServeManAdd = new OldManMappingServeMan();
                        oldManMappingServeManAdd.OldManId = oldManId.ToGuid();
                        oldManMappingServeManAdd.OperatedBy = NormalSession.UserId.ToGuid();
                        oldManMappingServeManAdd.OperatedOn = DateTime.Now;
                        oldManMappingServeManAdd.BeginTime = DateTime.Now;
                        oldManMappingServeManAdd.EndTime = Convert.ToDateTime("2999-12-31 23:59:59");
                        oldManMappingServeManAdd.UserId = arruserId.ToGuid();
                        statements.Add(new IBatisNetBatchStatement { StatementName = oldManMappingServeManAdd.GetCreateMethodName(), ParameterObject = oldManMappingServeManAdd.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    } 
                    if (statements.Count > 0)
                    {
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                    }
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

        #region 解除老人和服务人员的关系
        [WebInvoke(UriTemplate = "OldManMappingServeManRemove/{strOldManId},{strUserId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<OldManMappingServeManPK> OldManMappingServeManRemove(string strOldManId, string strUserId)
        {
            ModelInvokeResult<OldManMappingServeManPK> result = new ModelInvokeResult<OldManMappingServeManPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _OldManId = strOldManId.ToGuid();
                Guid? _UserId = strUserId.ToGuid();
                if (_UserId == null || _OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                //作废工作计划是否成功
                if (NullifyWorkPlan(strOldManId, strUserId))
                {
                    OldManMappingServeMan oldManMappingServeMan = new OldManMappingServeMan();
                    /***********************begin 自定义代码*******************/
                    oldManMappingServeMan.EndTime = DateTime.Now;
                    oldManMappingServeMan.OperatedBy = NormalSession.UserId.ToGuid();
                    oldManMappingServeMan.OperatedOn = DateTime.Now;
                    oldManMappingServeMan.OldManId = _OldManId;
                    oldManMappingServeMan.UserId = _UserId;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = "OldManMappingServeMan_Remove", ParameterObject = oldManMappingServeMan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                    /***********************begin 自定义代码*******************/
                    /***********************此处添加自定义代码*****************/
                    /***********************end 自定义代码*********************/
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);

                    SPParam spParam = new { }.ToSPParam();
                    spParam["OldManId"] = strOldManId.ToGuid();
                    spParam["BatchFlag"] = 0;
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "工作计划作废失败";
                    return result;
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


        #region 变更老人和服务人员的关系
        [WebInvoke(UriTemplate = "OldManMappingServeManChange/{strOldManId},{strUserId},{strUserIdNew}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<OldManMappingServeManPK> OldManMappingServeManChange(string strOldManId, string strUserId, string strUserIdNew)
        {
            ModelInvokeResult<OldManMappingServeManPK> result = new ModelInvokeResult<OldManMappingServeManPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _OldManId = strOldManId.ToGuid();
                Guid? _UserId = strUserId.ToGuid();
                Guid? _UserIdNew = strUserIdNew.ToGuid();
                if (_OldManId == null || _UserId == null || _UserIdNew == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                //变更工作计划是否成功
                if (ChangeWorkPlan(strOldManId, strUserId, strUserIdNew))
                {
                    OldManMappingServeMan oldManMappingServeManRemove = new OldManMappingServeMan();
                    /***********************作废原来的关系*******************/
                    oldManMappingServeManRemove.EndTime = DateTime.Now;
                    oldManMappingServeManRemove.OperatedBy = NormalSession.UserId.ToGuid();
                    oldManMappingServeManRemove.OperatedOn = DateTime.Now;
                    oldManMappingServeManRemove.OldManId = _OldManId;
                    oldManMappingServeManRemove.UserId = _UserId;
                    statements.Add(new IBatisNetBatchStatement { StatementName = "OldManMappingServeMan_Remove", ParameterObject = oldManMappingServeManRemove.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                    /***********************作废原来的关系*******************/

                    /***********************新增新的关系*****************/
                    OldManMappingServeMan oldManMappingServeManAdd = new OldManMappingServeMan();
                    oldManMappingServeManAdd.OldManId = _OldManId;
                    oldManMappingServeManAdd.OperatedBy = NormalSession.UserId.ToGuid();
                    oldManMappingServeManAdd.OperatedOn = DateTime.Now;
                    oldManMappingServeManAdd.BeginTime = DateTime.Now;
                    oldManMappingServeManAdd.EndTime = Convert.ToDateTime("2999-12-31 23:59:59");
                    oldManMappingServeManAdd.UserId = _UserIdNew;
                    statements.Add(new IBatisNetBatchStatement { StatementName = oldManMappingServeManAdd.GetCreateMethodName(), ParameterObject = oldManMappingServeManAdd.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });

                    /***********************end 新增新的关系*********************/
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);

                    SPParam spParam = new { }.ToSPParam();
                    spParam["OldManId"] = strOldManId.ToGuid();
                    spParam["BatchFlag"] = 0;
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "工作计划作废失败";
                    return result;
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


        #region 根据OldManID和UserID作废工作计划 
        public bool NullifyWorkPlan(string strOldManId, string strUserId)
        {
            bool result = false;
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _OldManId = strOldManId.ToGuid();
                Guid? _UserId = strUserId.ToGuid();
                if (_UserId == null || _OldManId == null)
                {
                    result= false; 
                    return result;
                }
                WorkPlan workPlan = new WorkPlan();
                /***********************begin 自定义代码*******************/
                workPlan.Status = 0;
                workPlan.OperatedBy = NormalSession.UserId.ToGuid();
                workPlan.OperatedOn = DateTime.Now;
                workPlan.UserId = _UserId;
                workPlan.OldManId =_OldManId;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = "NullifyWorkPlan_Update", ParameterObject = workPlan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });

                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result = true;
                /***********************end 自定义代码*********************/
            }
            catch (Exception ex)
            {
                result = false; 
            }
            return result;
        }
        #endregion

        #region 根据OldManID、UserID和UserIDNew变更工作计划
        public bool ChangeWorkPlan(string strOldManId, string strUserId, string strUserIdNew)
        {
            bool result = false;
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _OldManId = strOldManId.ToGuid();
                Guid? _UserId = strUserId.ToGuid();
                Guid? _UserIdNew = strUserIdNew.ToGuid();
                if (_OldManId == null || _UserId == null || _UserIdNew == null)
                {
                    result = false;
                    return result;
                }
                StringObjectDictionary workPlan = new StringObjectDictionary();
                /***********************begin 自定义代码*******************/
                workPlan["StatusW"] = 1;
                workPlan["OperatedBy"] = NormalSession.UserId.ToGuid();
                workPlan["OperatedOn"] = DateTime.Now;
                workPlan["UserId"] = _UserId;
                workPlan["OldManId"] = _OldManId;
                workPlan["UserIdNew"] = _UserIdNew;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = "NullifyWorkPlan_Update", ParameterObject = workPlan, Type = SqlExecuteType.UPDATE });

                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result = true;
                /***********************end 自定义代码*********************/
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region  获取服务人员信息以及配置号码
        [WebGet(UriTemplate = "QueryServeManConfigInfo?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> QueryServeManConfigInfo(string strParms)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetServiceManConfigInfo", dictionary);
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

