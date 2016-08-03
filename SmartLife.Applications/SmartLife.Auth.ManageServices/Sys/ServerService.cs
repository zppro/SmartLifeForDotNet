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

namespace SmartLife.Auth.ManageServices.Sys
{
    [ServiceLogging]
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServerService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Server> List()
        {
            CollectionInvokeResult<Server> result = new CollectionInvokeResult<Server> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<Server>();
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
        public CollectionInvokeResult<Server> Query(string strParms)
        {
            CollectionInvokeResult<Server> result = new CollectionInvokeResult<Server> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<Server>(dictionary);
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
        public JqgridCollectionInvokeResult<Server> ListForJqgrid(JqgridCollectionParam<Server> param)
        {
            JqgridCollectionInvokeResult<Server> result = new JqgridCollectionInvokeResult<Server> { Success = true };
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

                gridCollectionPager.JqgridDoPage<Server>(BuilderFactory.DefaultBulder().List<Server>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<Server> ListForEasyUIgrid(EasyUIgridCollectionParam<Server> param)
        {
            EasyUIgridCollectionInvokeResult<Server> result = new EasyUIgridCollectionInvokeResult<Server> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<Server>(BuilderFactory.DefaultBulder().List<Server>(filters), param, ref result);
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
        public ModelInvokeResult<ServerPK> Create(Server server)
        {
            ModelInvokeResult<ServerPK> result = new ModelInvokeResult<ServerPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (server.ServerId == "自动生成")
                {
                    server.ServerId = GlobalManager.getPK(server.GetMappingTableName(), "ServerId");
                }
                /***********************begin 自定义代码*******************/ 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = server.GetCreateMethodName(), ParameterObject = server.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                bool needDatabaseConnect = server.ServerId.StartsWith("Job");
                if (needDatabaseConnect)
                {
                    /***********************begin 自定义代码*******************/
                    DatabaseConnect databaseConnect = new DatabaseConnect
                    {
                        ConnectId = "Job-" + server.ServerId,
                        ConnectName = "作业-" + server.ServerName,
                        Provider = GlobalManager.DIKey_00015_SqlServer2005,
                        ServerName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(server.IpAddress),
                        DatabaseName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(GlobalManager.Job_Host_Database),
                        UserCode = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(GlobalManager.Job_Database_User),
                        UserPassword = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(GlobalManager.Job_Database_Password),
                        IBatisMapId = "SqlMap-Job-" + server.ServerId,
                        IbatisConfigFileRefer = GlobalManager.IbatisConfigFileRefer,
                        IbatisConfigFileValue = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(GlobalManager.IbatisConfigFileValue_Job)
                    };
                    statements.Add(new IBatisNetBatchStatement { StatementName = databaseConnect.GetCreateMethodName(), ParameterObject = databaseConnect.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    /***********************end 自定义代码*********************/
                }
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServerPK { ServerId = server.ServerId };
                if (needDatabaseConnect)
                {
                    //注册数据连接ase
                    GlobalManager.RegisterDatabaseConnections();
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

        #region 修改 Update
        [WebInvoke(UriTemplate = "{strServerId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServerPK> Update(string strServerId, Server server)
        {
            ModelInvokeResult<ServerPK> result = new ModelInvokeResult<ServerPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                var parameterObject = server.ToStringObjectDictionary(false);
                parameterObject["OldServerId"] = strServerId;
                /***********************begin 自定义代码*******************/ 
                /***********************end 自定义代码*********************/
                string sql = BuilderFactory.DefaultBulder().GetSql("Server_Update2", parameterObject);
                statements.Add(new IBatisNetBatchStatement { StatementName = "Server_Update2", ParameterObject = parameterObject, Type = SqlExecuteType.UPDATE });
                bool needDatabaseConnect = server.ServerId.StartsWith("Job");
                if (needDatabaseConnect)
                {
                    /***********************begin 自定义代码*******************/
                    DatabaseConnect databaseConnect = new DatabaseConnect
                    {
                        ConnectId = "Job-" + server.ServerId,
                        ConnectName = "作业-" + server.ServerName,
                        Provider = GlobalManager.DIKey_00015_SqlServer2005,
                        ServerName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(server.IpAddress),
                        DatabaseName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(GlobalManager.Job_Host_Database),
                        UserCode = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(GlobalManager.Job_Database_User),
                        UserPassword = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(GlobalManager.Job_Database_Password),
                        IBatisMapId = "SqlMap-Job-" + server.ServerId,
                        IbatisConfigFileRefer = GlobalManager.IbatisConfigFileRefer,
                        IbatisConfigFileValue = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(GlobalManager.IbatisConfigFileValue_Job)
                    };

                    var parameterObject2 = databaseConnect.ToStringObjectDictionary(false);
                    parameterObject2["OldConnectId"] = "Job-" + strServerId;

                    statements.Add(new IBatisNetBatchStatement { StatementName = "DatabaseConnect_Update2", ParameterObject = parameterObject2, Type = SqlExecuteType.UPDATE });
                    /***********************end 自定义代码*********************/
                }
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServerPK { ServerId = strServerId };
                if (needDatabaseConnect)
                {
                    //注册数据连接ase
                    GlobalManager.RegisterDatabaseConnections();
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

        #region 删除 Delete
        [WebInvoke(UriTemplate = "{strServerId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServerPK> Delete(string strServerId)
        {
            ModelInvokeResult<ServerPK> result = new ModelInvokeResult<ServerPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _ServerId = strServerId;
                ServerPK pk = new ServerPK { ServerId = _ServerId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new Server().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServerPK { ServerId = _ServerId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strServerIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strServerIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrServerIds = strServerIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrServerIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Server().GetDeleteMethodName();
                foreach (string strServerId in arrServerIds)
                {
                    ServerPK pk = new ServerPK { ServerId = strServerId };
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
        [WebInvoke(UriTemplate = "Nullify/{strServerId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ServerPK> Nullify(string strServerId)
        {
            ModelInvokeResult<ServerPK> result = new ModelInvokeResult<ServerPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _ServerId = strServerId;
                Server server = new Server { ServerId = _ServerId, Status = 0 };
                /***********************begin 自定义代码*******************/ 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = server.GetUpdateMethodName(), ParameterObject = server.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ServerPK { ServerId = _ServerId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strServerIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strServerIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrServerIds = strServerIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrServerIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Server().GetUpdateMethodName();
                foreach (string strServerId in arrServerIds)
                {
                    Server server = new Server { ServerId = strServerId, Status = 0 };
                    /***********************begin 自定义代码*******************/ 
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = server.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ServerPK pk)
        {
            //此处增加级联删除代码
            DatabaseConnectPK param = new DatabaseConnectPK { ConnectId = "Job-" + pk.ServerId };
            statements.Add(new IBatisNetBatchStatement { StatementName = new DatabaseConnect().GetDeleteMethodName(), ParameterObject = param, Type = SqlExecuteType.DELETE });
             
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strServerId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Server> Load(string strServerId)
        {
            ModelInvokeResult<Server> result = new ModelInvokeResult<Server> { Success = true };
            try
            {
                string _ServerId = strServerId;
                var server = BuilderFactory.DefaultBulder().Load<Server, ServerPK>(new ServerPK { ServerId = _ServerId });
                result.instance = server;
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
        [WebInvoke(UriTemplate = "TestServerConnection/{serverId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<string> TestServerConnection(string serverId)
        {
            InvokeResult<string> result = new InvokeResult<string> { Success = true };
            try
            {

                result.ret = TypeConverter.ChangeString(BuilderFactory.DefaultBulder(serverId).ExecuteScalar("select db_name()"));

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = "连接失败：" + ex.Message;
            }
            return result;
        }
        #endregion
    }
}