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
using SmartLife.Share.Models.PO;
using SmartLife.Share.Models.Pam;

namespace SmartLife.CertManage.ManageServices.Pub
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class UserService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<User> List()
        {
            CollectionInvokeResult<User> result = new CollectionInvokeResult<User> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<User>();
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
        public CollectionInvokeResult<User> Query(string strParms)
        {
            CollectionInvokeResult<User> result = new CollectionInvokeResult<User> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<User>(dictionary);
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
        public JqgridCollectionInvokeResult<User> ListForJqgrid(JqgridCollectionParam<User> param)
        {
            JqgridCollectionInvokeResult<User> result = new JqgridCollectionInvokeResult<User> { Success = true };
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

                gridCollectionPager.JqgridDoPage<User>(BuilderFactory.DefaultBulder().List<User>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<User> ListForEasyUIgrid(EasyUIgridCollectionParam<User> param)
        {
            EasyUIgridCollectionInvokeResult<User> result = new EasyUIgridCollectionInvokeResult<User> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();

                if (param.instance != null)
                {

                    List<string> groupIds = new List<string>();
                    if (param.instance.isNormalUser(groupIds) || param.instance.isMerchantUser(groupIds))
                    {

                        if (groupIds.Count > 0)
                        {
                            whereClause.Add(" UserId in (select UserId from  Pub_GroupMember a where a.GroupId in ('" + string.Join("','", groupIds.ToArray()) + "'))");
                        }
                    }

                    filters = param.instance.ToStringObjectDictionary(false);
                }

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

                gridCollectionPager.EasyUIgridDoPage<User>(BuilderFactory.DefaultBulder().List<User>(filters), param, ref result);

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
        public ModelInvokeResult<UserPK> Create(User user)
        {
            ModelInvokeResult<UserPK> result = new ModelInvokeResult<UserPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (user.UserId == GlobalManager.GuidAsAutoGenerate)
                {
                    user.UserId = Guid.NewGuid();
                }
                user.PasswordHash = e0571.web.core.Security.MD5Provider.Generate("88976666");//
                user.OperatedBy = NormalSession.UserId.ToGuid();
                user.OperatedOn = DateTime.Now;
                user.CreatedBy = NormalSession.UserId.ToGuid();
                user.CreatedOn = DateTime.Now;
                user.InputCode1 = GetInputCode(user.UserName, "py");
                user.InputCode2 = GetInputCode(user.UserName, "wb");
                /*****************  whereClause条件  ************/
                string whereClause = "Status in (0,1)";
                if (!string.IsNullOrEmpty(user.UserType))
                {
                    string[] parts = user.UserType.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Count() > 0)
                    {
                        var userType = parts[0];
                        whereClause = whereClause + " and UserType=" + userType;
                    }
                }
                whereClause = whereClause + " and UserId<>'" + user.UserId + "'";
                /*****************  whereClause条件  ************/
                /***********************begin 自定义代码*******************/
                bool isExist = user.IsExistValue("Pub_User", "UserCode", user.UserCode, whereClause, GetHttpHeader("ConnectId"));
                if (!isExist)
                {
                    /***********************begin 自定义代码*******************/
                    /***********************此处添加自定义代码*****************/
                    /***********************end 自定义代码*********************/

                    /***********************begin 自定义代码*******************/
                    List<string> groupIds = new List<string>();
                    if (user.isNormalUser(groupIds) || user.isMerchantUser(groupIds) || user.isAgencyUser(groupIds))
                    {
                        statements.Add(new IBatisNetBatchStatement { StatementName = user.GetCreateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                        user.setRoles(statements, groupIds);
                    }
                    else
                    {
                        statements.Add(new IBatisNetBatchStatement { StatementName = user.GetCreateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }
                    /***********************end 自定义代码*********************/
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                    SPParam spParam = new { }.ToSPParam();
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceUser", spParam);
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceGroupMember", spParam);
                    result.instance = new UserPK { UserId = user.UserId };
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "工号已被使用";
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
        [WebInvoke(UriTemplate = "{strUserId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<UserPK> Update(string strUserId, User user)
        {
            ModelInvokeResult<UserPK> result = new ModelInvokeResult<UserPK> { Success = true };
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
                user.UserId = _UserId;
                user.OperatedBy = NormalSession.UserId.ToGuid();
                user.OperatedOn = DateTime.Now;
                user.InputCode1 = GetInputCode(user.UserName, "py");
                user.InputCode2 = GetInputCode(user.UserName, "wb");

                /*****************  whereClause条件  ************/
                string whereClause = "Status in (0,1)";
                if (!string.IsNullOrEmpty(user.UserType))
                {
                    string[] parts = user.UserType.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Count() > 0)
                    {
                        var userType = parts[0];
                        whereClause = whereClause + " and UserType=" + userType;
                    }
                }
                whereClause = whereClause + " and UserId<>'" + user.UserId + "'";
                /*****************  whereClause条件  ************/
                /***********************begin 自定义代码*******************/
                bool isExist = user.IsExistValue("Pub_User", "UserCode", user.UserCode, whereClause, GetHttpHeader("ConnectId"));
                if (!isExist)
                {
                    List<string> groupIds = new List<string>();
                    if (user.isNormalUser(groupIds) || user.isMerchantUser(groupIds) || user.isAgencyUser(groupIds))
                    {
                        statements.Add(new IBatisNetBatchStatement { StatementName = user.GetUpdateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                        user.setRoles(statements, groupIds);
                    }
                    else
                    {
                        statements.Add(new IBatisNetBatchStatement { StatementName = user.GetUpdateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                    }
                    /***********************end 自定义代码*********************/
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                    SPParam spParam = new { }.ToSPParam();
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceUser", spParam);
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceGroupMember", spParam);
                    result.instance = new UserPK { UserId = _UserId };
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "工号已被使用";
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
        [WebInvoke(UriTemplate = "{strUserId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<UserPK> Delete(string strUserId)
        {
            ModelInvokeResult<UserPK> result = new ModelInvokeResult<UserPK> { Success = true };
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
                UserPK pk = new UserPK { UserId = _UserId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new User().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = pk;
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
                string statementName = new User().GetDeleteMethodName();
                foreach (string strUserId in arrUserIds)
                {
                    UserPK pk = new UserPK { UserId = strUserId.ToGuid() };
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
        public ModelInvokeResult<UserPK> Nullify(string strUserId)
        {
            ModelInvokeResult<UserPK> result = new ModelInvokeResult<UserPK> { Success = true };
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
                User user = new User { UserId = _UserId, Status = 0 };
                user.OperatedBy = NormalSession.UserId.ToGuid();
                user.OperatedOn = DateTime.Now;
                statements.Add(new IBatisNetBatchStatement { StatementName = user.GetUpdateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                SPParam spParam = new { }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceUser", spParam);
                result.instance = new UserPK { UserId = _UserId };
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
                string statementName = new User().GetUpdateMethodName();
                foreach (string strUserId in arrUserIds)
                {
                    User user = new User { UserId = strUserId.ToGuid(), Status = 0 };
                    user.OperatedBy = NormalSession.UserId.ToGuid();
                    user.OperatedOn = DateTime.Now;
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                SPParam spParam = new { }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceUser", spParam);
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, UserPK pk)
        {
            //此处增加级联删除代码

            //删除组成员关系
            GroupMember gm = new GroupMember { UserId = pk.UserId };
            statements.Add(new IBatisNetBatchStatement { StatementName = gm.GetDeleteMethodName(), ParameterObject = gm.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strUserId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<User> Load(string strUserId)
        {
            ModelInvokeResult<User> result = new ModelInvokeResult<User> { Success = true };
            try
            {
                Guid? _UserId = strUserId.ToGuid();
                if (_UserId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var user = BuilderFactory.DefaultBulder().Load<User, UserPK>(new UserPK { UserId = _UserId });
                result.instance = user;
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


        #region 取得拼音码和五笔码
        public string GetInputCode(string nameStr, string tpye)
        {
            string result = null;
            try
            {
                string param = "";
                if (tpye == "py")
                {
                    param = "select dbo.func_tol_getpy('" + nameStr + "') as InputCode";
                }
                else
                {
                    param = "select dbo.func_tol_getwb('" + nameStr + "') as InputCode";
                }
                result = BuilderFactory.DefaultBulder().ExecuteScalar(param).ToString();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion


        #region 扩展接口

        #region 设置所属组
        [WebInvoke(UriTemplate = "SetGroup/{userId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<UserPK> SetGroup(string userId, User user)
        {
            InvokeResult<UserPK> result = new InvokeResult<UserPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _UserId = userId.ToGuid();
                if (_UserId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                List<string> groupIds = new List<string>();

                if (user.isNormalUser(groupIds) || user.isMerchantUser(groupIds) || user.isAgencyUser(groupIds))
                {
                    user.setRoles(statements, groupIds);
                }
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                /**************************设置所属组之后，同步组与组成员*******************************************/
                SPParam spParam = new { }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceGroup", spParam);
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceGroupMember", spParam);
                /**************************设置所属组之后，同步组与组成员*******************************************/
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 设置用户名与商家的关系
        [WebInvoke(UriTemplate = "SetUserForSeller/{userId},{stationId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SetUserForSeller(string userId, string stationId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                MerchantUser ua = new MerchantUser { UserId = userId.ToGuid() };
                statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetDeleteMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });

                ua.StationId = stationId.ToGuid();
                statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetCreateMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });

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

        #region 设置辖区
        [WebInvoke(UriTemplate = "SetArea/{userId},{areaId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SetArea(string userId, string areaId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                UserArea ua = new UserArea { UserId = userId.ToGuid() };
                statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetDeleteMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });

                ua.AreaId = areaId.ToGuid();
                statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetCreateMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });

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

        #region (评估系统独立部署)设置辖区
        [WebInvoke(UriTemplate = "SetAreaEx", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<UserPK> SetAreaEx(LisenceUser lisenceUser)
        {
            ModelInvokeResult<UserPK> result = new ModelInvokeResult<UserPK> { Success = true };
            try
            { 
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                 
                /***********************begin 自定义代码*******************/
                //解析 
                List<string> areaIds = new List<string>(); 
                string[] parts = lisenceUser.UserType.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (parts[0] == GlobalManager.DIKey_01001_NormalUser)
                {
                    lisenceUser.UserType = parts[0];
                    areaIds.AddRange(parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));  
                } 
                //绑定用户辖区
                if (areaIds.Count > 0)
                {
                    UserArea ua = new UserArea() { UserId = lisenceUser.UserId };
                    statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetDeleteMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                    foreach (string itemId in areaIds)
                    {
                        ua.AreaId = itemId.ToGuid();
                        ua.UserId = lisenceUser.UserId;
                        ua.OperatedBy = NormalSession.UserId.ToGuid();
                        ua.OperatedOn = DateTime.Now;
                        statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetCreateMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }
                }
                 
                /***********************end 自定义代码*********************/

                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);

                result.instance = new UserPK { UserId = lisenceUser.UserId };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion
        #region 获取用户辖区
        [WebGet(UriTemplate = "GetArea/{userId}", ResponseFormat = WebMessageFormat.Json)]
        public string GetArea(string userId)
        {
            return TypeConverter.ChangeString(BuilderFactory.DefaultBulder().ExecuteScalar("GetAreaByUser", new { UserId = userId }));
        }
        #endregion

        #region 停用所选 StopSelected
        [WebInvoke(UriTemplate = "StopSelected/{strUserIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult StopSelected(string strUserIds)
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
                string statementName = new User().GetUpdateMethodName();
                foreach (string strUserId in arrUserIds)
                {
                    User user = new User { UserId = strUserId.ToGuid(), StopFlag = 1 };
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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

        #region 重新启用所选 RestartSelected
        [WebInvoke(UriTemplate = "RestartSelected/{strUserIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult RestartSelectedSelected(string strUserIds)
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
                string statementName = new User().GetUpdateMethodName();
                foreach (string strUserId in arrUserIds)
                {
                    User user = new User { UserId = strUserId.ToGuid(), StopFlag = 0 };
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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

        #region 获取所在的用户组
        [WebGet(UriTemplate = "GetGroup/{userId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<string> GetGroup(string userId)
        {
            return BuilderFactory.DefaultBulder().List<GroupMember>(new GroupMember { UserId = Guid.Parse(userId) }).Select(item => item.GroupId.ToString()).ToList();
        }

        [WebGet(UriTemplate = "GetGroupTwo/{userId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<string> GetGroupTwo(string userId)
        {
            return BuilderFactory.DefaultBulder().List<GroupMember>(new GroupMember { UserId = Guid.Parse(userId) }).Select(item => (item.GroupId.ToString() == "00000000-0000-0000-0000-000000000999" ? null : item.GroupId.ToString())).ToList();
        }
        #endregion

        #region 获取坐席对应队列及被叫号码
        [WebGet(UriTemplate = "GetQueueNoAndCallee/{userId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<QueueNoAndCallee> GetQueueNoAndCallee(string userId)
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ListStringObjectDictionary("GetQueueNoAndCalleeByUser", new { UserId = Guid.Parse(userId) }).Select(item => new QueueNoAndCallee { QueueNo = item["QueueNo"].ToString(), Callee = item["Callee"].ToString() }).ToList();
        }
        #endregion

        #region 修改 无验证用户信息 UpdateValidatedUser
        [WebInvoke(UriTemplate = "UpdateValidatedUser/{strUserId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<UserPK> UpdateValidatedUser(string strUserId, User user)
        {
            ModelInvokeResult<UserPK> result = new ModelInvokeResult<UserPK> { Success = true };
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
                user.UserId = _UserId;
                user.OperatedBy = NormalSession.UserId.ToGuid();
                user.OperatedOn = DateTime.Now;

                if (!string.IsNullOrEmpty(user.PasswordHash))
                {
                    user.PasswordHash = e0571.web.core.Security.MD5Provider.Generate(user.PasswordHash);
                }
                /***********************begin 自定义代码*******************/
                statements.Add(new IBatisNetBatchStatement { StatementName = user.GetUpdateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                SPParam spParam = new { }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceUser", spParam);
                result.instance = new UserPK { UserId = _UserId };

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 密码验证 VerifyPassword
        [WebGet(UriTemplate = "VerifyPassword/{strUserId},{strPassWord}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<User> VerifyPassword(string strUserId, string strPassWord)
        {
            ModelInvokeResult<User> result = new ModelInvokeResult<User> { Success = true };
            try
            {
                Guid? _UserId = strUserId.ToGuid();
                if (_UserId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                string newPassWord = e0571.web.core.Security.MD5Provider.Generate(strPassWord);
                var user = BuilderFactory.DefaultBulder().Load<User, UserPK>(new UserPK { UserId = _UserId });
                if (user.PasswordHash.Equals(newPassWord))
                {
                    result.instance = user;
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

        #region 重置密码
        [WebInvoke(UriTemplate = "RePwd/{userId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult RePwd(string userId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                User user = new User
                {
                    UserId = userId.ToGuid(),
                    //PasswordHash=System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("123","MD5")
                    PasswordHash = e0571.web.core.Security.MD5Provider.Generate("88976666"),
                    OperatedBy = NormalSession.UserId.ToGuid(),
                    OperatedOn = DateTime.Now
                };
                statements.Add(new IBatisNetBatchStatement { StatementName = user.GetUpdateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                SPParam spParam = new { }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceUser", spParam);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 判断是否是座席
        [WebGet(UriTemplate = "CheckCurrentUserIsSeat", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<CheckCurrentUserIsSeatRet> CheckCurrentUserIsSeat()
        {
            InvokeResult<CheckCurrentUserIsSeatRet> result = new InvokeResult<CheckCurrentUserIsSeatRet> { Success = true, ret = new CheckCurrentUserIsSeatRet { } };
            User currentUser = new User { UserId = NormalSession.UserId.ToGuid() };
            result.ret.isCCSeat = currentUser.isCCSeat();
            return result;
        }
        #endregion

        #region 判断用户类别
        [WebGet(UriTemplate = "CheckCurrentUserCatalog", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<CurrentUserCatalogRet> CheckCurrentUserCatalog()
        {
            InvokeResult<CurrentUserCatalogRet> result = new InvokeResult<CurrentUserCatalogRet> { Success = true, ret = new CurrentUserCatalogRet { } };
            User currentUser = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };
            result.ret.isSuperAdmin = currentUser.isSuperAdmin();
            result.ret.isMerchantUser = currentUser.isMerchantUser();
            result.ret.isAgencyUser = currentUser.isAgencyUser();
            result.ret.UserId = NormalSession.UserId.ToGuid();
            result.ret.UserCode = NormalSession.UserCode;
            result.ret.UserName = NormalSession.UserName;
            return result;
        }
        #endregion

        #region 获取关联商家
        [WebGet(UriTemplate = "GetMappingMerhants", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceStation_Light> GetMappingMerhants()
        {
            CollectionInvokeResult<ServiceStation_Light> result = new CollectionInvokeResult<ServiceStation_Light> { Success = true };
            if (GetHttpHeader("isSuperAdmin") == "true")
            {
                result.rows = BuilderFactory.DefaultBulder().List<ServiceStation_Light>("ServiceStation_Light_ListAll", new { StationType = GlobalManager.DIKey_01002_Merchant }.ToStringObjectDictionary());
            }
            else
            {
                result.rows = BuilderFactory.DefaultBulder().List<ServiceStation_Light>("ServiceStation_Light_ListAsMerchantByUser", new { UserId = NormalSession.UserId.ToGuid() }.ToStringObjectDictionary());
            }
            return result;
        }
        #endregion

        #region 获取关联机构
        [WebGet(UriTemplate = "GetMappingAgencys", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceStation_Light> GetMappingAgencys()
        {
            CollectionInvokeResult<ServiceStation_Light> result = new CollectionInvokeResult<ServiceStation_Light> { Success = true };
            if (GetHttpHeader("isSuperAdmin") == "true")
            {
                result.rows = BuilderFactory.DefaultBulder().List<ServiceStation_Light>("ServiceStation_Light_ListAll", new { StationType = GlobalManager.DIKey_01002_SocialOrganization }.ToStringObjectDictionary());
            }
            else
            {
                result.rows = BuilderFactory.DefaultBulder().List<ServiceStation_Light>("ServiceStation_Light_ListAsAgencyByUser", new { UserId = NormalSession.UserId.ToGuid() }.ToStringObjectDictionary());
            }
            return result;
        }
        #endregion

        #region 服务人员设置微信号
        [WebInvoke(UriTemplate = "SetMerchantServeWeixin/{bizType},{userId},{openIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SetMerchantServeWeixin(string bizType, string userId, string openIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrOpenIds = openIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrOpenIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                UserMappingWeiXin userMappingWeiXin = new UserMappingWeiXin();
                foreach (string strOpenId in arrOpenIds)
                {
                    userMappingWeiXin.UserId = userId.ToGuid();
                    userMappingWeiXin.BizType = bizType;
                    userMappingWeiXin.OpenId = strOpenId;
                    userMappingWeiXin.OperatedBy = NormalSession.UserId.ToGuid();
                    userMappingWeiXin.OperatedOn = DateTime.Now;
                    statements.Add(new IBatisNetBatchStatement { StatementName = userMappingWeiXin.GetCreateMethodName(), ParameterObject = userMappingWeiXin.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 服务人员已绑定的微信号
        [WebGet(UriTemplate = "BindedWeixin/{userId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> BindedWeixin(string userId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary>() { Success = true };

            try
            {
                var param = "";
                param = "select * from Meb_NormalAccount where OpenId in (select OpenId from Pub_UserMappingWeiXin where UserId='" + userId + "')";
                result.rows = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;
            }

            return result;
        }
        #endregion

        #region 服务人员微信号解除绑定
        [WebInvoke(UriTemplate = "RemoveBindedWeixin/{userId},{openId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<UserMappingWeiXinPK> RemoveBindedWeixin(string userId, string openId)
        {
            ModelInvokeResult<UserMappingWeiXinPK> result = new ModelInvokeResult<UserMappingWeiXinPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _UserId = userId.ToGuid();
                if (_UserId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string _OpenId =openId ;
                UserMappingWeiXin userMappingWeiXin = new UserMappingWeiXin();
                userMappingWeiXin.UserId = _UserId;
                userMappingWeiXin.OpenId = _OpenId;
                statements.Add(new IBatisNetBatchStatement { StatementName = userMappingWeiXin.GetDeleteMethodName(), ParameterObject = userMappingWeiXin.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
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


        #region 获取某区下有权限的[街道+社区]字典项目
        [WebGet(UriTemplate = "GetStreetAndCommunityInAreaAuthority/{parentId},{userId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictItemEx> GetStreetAndCommunityInArea(string parentId, string userId)
        {
            IList<DictItemEx> results = null;
            try
            {var parentId_All=0;
                if (parentId.Length > 5)
                {
                    var sql = "select * from Pub_UserArea where AreaId = '" + parentId + "' and UserId='" + userId + "'";
                    parentId_All = BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).ExecuteNativeSqlForCount(sql);
                }
                User currentUser = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };
                if (currentUser.isSuperAdmin() || parentId_All >0)
                {
                    results = BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).List<DictItemEx>("CTE_StreetAndCommunitiy_List", new { ParentId = parentId }.ToStringObjectDictionary());
                }
                else
                {
                    results = BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).List<DictItemEx>("CTE_StreetAndCommunitiyAuthority_List", new { ParentId = parentId, UserId = userId }.ToStringObjectDictionary());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return results;
        }
        #endregion


        #region 获取所有坐席人员
        [WebInvoke(UriTemplate = "GetSeats", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<User> GetSeats(EasyUIgridCollectionParam<User> param)
        {
            EasyUIgridCollectionInvokeResult<User> result = new EasyUIgridCollectionInvokeResult<User> { Success = true };
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
                whereClause.Add("UserId in (select b.UserId from Pub_Group a right join Pub_GroupMember b  on a.GroupId=b.GroupId where a.GroupType='10002' and a.Status=1 )");
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/
                /**********************************************************/
                /**********************************************************/
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                /***********************end 排序***************************/

                gridCollectionPager.EasyUIgridDoPage<User>(BuilderFactory.DefaultBulder().List<User>(filters), param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 养老机构

        #region 设置用户名与养老机构的关系
        [WebInvoke(UriTemplate = "SetUserForPensionAgency/{userId},{stationId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SetUserForPensionAgency(string userId, string stationId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                PensionAgencyUser ua = new PensionAgencyUser { UserId = userId.ToGuid() };
                statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetDeleteMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });

                ua.StationId = stationId.ToGuid();
                statements.Add(new IBatisNetBatchStatement { StatementName = ua.GetCreateMethodName(), ParameterObject = ua.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });

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

        #region 作废机构服务人员，同时作废此服务人员相关的工作计划
        [WebInvoke(UriTemplate = "NullifyEx/{strUserId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<UserPK> NullifyEx(string strUserId)
        {
            ModelInvokeResult<UserPK> result = new ModelInvokeResult<UserPK> { Success = true };
            try
            {
                string isOK= NullifyServiceManAbout(strUserId);//作废所有与服务人员有关的事项 
                if (isOK == "true")
                {
                    List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                    Guid? _UserId = strUserId.ToGuid();
                    if (_UserId == null)
                    {
                        result.Success = false;
                        result.ErrorCode = 59996;
                        return result;
                    }
                    User user = new User { UserId = _UserId, Status = 0 };
                    user.OperatedBy = NormalSession.UserId.ToGuid();
                    user.OperatedOn = DateTime.Now;
                    statements.Add(new IBatisNetBatchStatement { StatementName = user.GetUpdateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                    /***********************begin 自定义代码*******************/
                    /***********************此处添加自定义代码*****************/
                    /***********************end 自定义代码*********************/
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                    SPParam spParam = new { }.ToSPParam();
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_SyncAuth_LisenceUser", spParam);
                    result.instance = new UserPK { UserId = _UserId };
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = isOK;
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

        #region  作废所有与服务人员有关的事项 
        private string NullifyServiceManAbout(string userId)
        {
            string result = "";
            try
            {
                /********************************作废工作计划项**************************************/
                WorkPlan workPlan = new WorkPlan();
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                workPlan.UserId = userId.ToGuid();
                workPlan.Status = 0;
                workPlan.OperatedBy = NormalSession.UserId.ToGuid();
                workPlan.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = "NullifyWorkPlan_Update", ParameterObject = workPlan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                /********************************作废工作计划项**************************************/

                /********************************作废老人和服务人员**************************************/
                OldManMappingServeMan oldManMappingServeMan = new OldManMappingServeMan();
                List<IBatisNetBatchStatement> statements_ = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                oldManMappingServeMan.EndTime = DateTime.Now;
                oldManMappingServeMan.OperatedBy = NormalSession.UserId.ToGuid();
                oldManMappingServeMan.OperatedOn = DateTime.Now;
                oldManMappingServeMan.UserId = userId.ToGuid(); 
                statements_.Add(new IBatisNetBatchStatement { StatementName = "OldManMappingServeMan_Remove", ParameterObject = oldManMappingServeMan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements_);
                /********************************作废老人和服务人员**************************************/

                /********************************作废工作计划执行表**************************************/
                string sql = "delete Pam_WorkExecute where UserId='" + userId + "' and ServeManArriveTime is null and RemindTime>GETDATE()";
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql);
                /********************************作废工作计划执行表**************************************/
                result = "true";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion
        #endregion
        #endregion
    }

    public class CheckCurrentUserIsSeatRet
    {
        public bool isCCSeat { get; set; }
    }
    public class CurrentUserCatalogRet
    {
        public Guid? UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public bool isSuperAdmin { get; set; }
        public bool isMerchantUser { get; set; }
        public bool isAgencyUser { get; set; }
    }

    public class LisenceUser
    {  
        public string Area1 { get; set; }
        public Guid? Area2 { get; set; }
        public Guid? Area3 { get; set; }
        public DateTime? CheckInTime { get; set; }
        public string CityId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? Id { get; set; }
        public Guid? OperatedBy { get; set; }
        public DateTime? OperatedOn { get; set; }
        public string PasswordHash { get; set; }
        public byte? Status { get; set; }
        public byte? StopFlag { get; set; }
        public byte? SystemFlag { get; set; }
        public string UserCode { get; set; }
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
          
    }

}