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

namespace SmartLife.CertManage.ManageServices.Sys
{
    [ServiceLogging] 
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DatabaseConnectService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<DatabaseConnect> List()
        {
            CollectionInvokeResult<DatabaseConnect> result = new CollectionInvokeResult<DatabaseConnect> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<DatabaseConnect>();
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
        public CollectionInvokeResult<DatabaseConnect> Query(string strParms)
        {
            CollectionInvokeResult<DatabaseConnect> result = new CollectionInvokeResult<DatabaseConnect> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<DatabaseConnect>(dictionary);
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
        public JqgridCollectionInvokeResult<DatabaseConnect> ListForJqgrid(JqgridCollectionParam<DatabaseConnect> param)
        {
            JqgridCollectionInvokeResult<DatabaseConnect> result = new JqgridCollectionInvokeResult<DatabaseConnect> { Success = true };
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

                gridCollectionPager.JqgridDoPage<DatabaseConnect>(BuilderFactory.DefaultBulder().List<DatabaseConnect>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<DatabaseConnect> ListForEasyUIgrid(EasyUIgridCollectionParam<DatabaseConnect> param)
        {
            EasyUIgridCollectionInvokeResult<DatabaseConnect> result = new EasyUIgridCollectionInvokeResult<DatabaseConnect> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<DatabaseConnect>(BuilderFactory.DefaultBulder().List<DatabaseConnect>(filters), param, ref result);
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
        public ModelInvokeResult<DatabaseConnectPK> Create(DatabaseConnect databaseConnect)
        {
            ModelInvokeResult<DatabaseConnectPK> result = new ModelInvokeResult<DatabaseConnectPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (databaseConnect.ConnectId == "自动生成")
                {
                    databaseConnect.ConnectId = GlobalManager.getPK(databaseConnect.GetMappingTableName(), "ConnectId");
                }
                /***********************begin 自定义代码*******************/
                databaseConnect.ServerName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(databaseConnect.ServerName);
                databaseConnect.DatabaseName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(databaseConnect.DatabaseName);
                databaseConnect.UserCode = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(databaseConnect.UserCode);
                databaseConnect.UserPassword = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(databaseConnect.UserPassword);
                databaseConnect.IbatisConfigFileValue = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(databaseConnect.IbatisConfigFileValue);
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = databaseConnect.GetCreateMethodName(), ParameterObject = databaseConnect.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new DatabaseConnectPK { ConnectId = databaseConnect.ConnectId };

                //注册数据连接ase
                GlobalManager.RegisterDatabaseConnections();
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
        [WebInvoke(UriTemplate = "{strConnectId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<DatabaseConnectPK> Update(string strConnectId, DatabaseConnect databaseConnect)
        {
            ModelInvokeResult<DatabaseConnectPK> result = new ModelInvokeResult<DatabaseConnectPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                databaseConnect.ServerName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(databaseConnect.ServerName);
                databaseConnect.DatabaseName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(databaseConnect.DatabaseName);
                databaseConnect.UserCode = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(databaseConnect.UserCode);
                databaseConnect.UserPassword = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(databaseConnect.UserPassword);
                databaseConnect.IbatisConfigFileValue = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Encrypt(databaseConnect.IbatisConfigFileValue);
                var parameterObject = databaseConnect.ToStringObjectDictionary(false);
                parameterObject["OldConnectId"] = strConnectId;
                /***********************end 自定义代码*********************/

                statements.Add(new IBatisNetBatchStatement { StatementName = "DatabaseConnect_Update2", ParameterObject = parameterObject, Type = SqlExecuteType.UPDATE });

                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new DatabaseConnectPK { ConnectId = databaseConnect.ConnectId };

                //注册数据连接ase
                GlobalManager.RegisterDatabaseConnections();
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
        [WebInvoke(UriTemplate = "{strConnectId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<DatabaseConnectPK> Delete(string strConnectId)
        {
            ModelInvokeResult<DatabaseConnectPK> result = new ModelInvokeResult<DatabaseConnectPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _ConnectId = strConnectId;
                DatabaseConnectPK pk = new DatabaseConnectPK { ConnectId = _ConnectId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new DatabaseConnect().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new DatabaseConnectPK { ConnectId = _ConnectId };

                //注册数据连接ase
                GlobalManager.RegisterDatabaseConnections();
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strConnectIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strConnectIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrConnectIds = strConnectIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrConnectIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new DatabaseConnect().GetDeleteMethodName();
                foreach (string strConnectId in arrConnectIds)
                {
                    DatabaseConnectPK pk = new DatabaseConnectPK { ConnectId = strConnectId };
                    DeleteCascade(statements, pk);
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pk, Type = SqlExecuteType.DELETE });
                }
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);

                //注册数据连接ase
                GlobalManager.RegisterDatabaseConnections();
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, DatabaseConnectPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strConnectId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<DatabaseConnect> Load(string strConnectId)
        {
            ModelInvokeResult<DatabaseConnect> result = new ModelInvokeResult<DatabaseConnect> { Success = true };
            try
            {
                string _ConnectId = strConnectId;
                var databaseConnect = BuilderFactory.DefaultBulder().Load<DatabaseConnect, DatabaseConnectPK>(new DatabaseConnectPK { ConnectId = _ConnectId });

                databaseConnect.ServerName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(databaseConnect.ServerName);
                databaseConnect.DatabaseName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(databaseConnect.DatabaseName);
                databaseConnect.UserCode = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(databaseConnect.UserCode);
                databaseConnect.UserPassword = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(databaseConnect.UserPassword);
                databaseConnect.IbatisConfigFileValue = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(databaseConnect.IbatisConfigFileValue);

                result.instance = databaseConnect;
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
        [WebInvoke(UriTemplate = "TestDatabaseConnection/{connectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<string> TestDatabaseConnection(string connectId)
        {
            InvokeResult<string> result = new InvokeResult<string> { Success = true };
            try
            {
                //var daoManager = IBatisNet.DataAccess.DaoManager.GetInstance(connectId);
                //string s = daoManager.LocalDataSource.ConnectionString;
                result.ret = TypeConverter.ChangeString(BuilderFactory.DefaultBulder(connectId).ExecuteScalar("select db_name()"));
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 象山客户端专用
        #region 根据CityCode查询 ConnectId
        [WebGet(UriTemplate = "GetConnectId/{strCityCode}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<DatabaseConnect> GetConnectId(string strCityCode)
        {
            CollectionInvokeResult<DatabaseConnect> result = new CollectionInvokeResult<DatabaseConnect> { Success = true };
            try
            {
                StringObjectDictionary dictionary = new StringObjectDictionary(); 
                dictionary["WhereClause"] = "ConnectId like '%" + strCityCode + "'";
                result.rows = BuilderFactory.DefaultBulder().List<DatabaseConnect>(dictionary);
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