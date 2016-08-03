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

namespace SmartLife.BaseData.ManageServices.Pub
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
                    if (param.instance.isNormalUser(groupIds))
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
                user.PasswordHash = e0571.web.core.Security.MD5Provider.Generate("123");//
                user.CreatedBy = NormalSession.UserId.ToGuid();
                user.CreatedOn = DateTime.Now;
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/

                /***********************begin 自定义代码*******************/
                List<string> groupIds = new List<string>();
                /**新增**/
                groupIds.Add("00000000-0000-0000-0000-000000000999");
                user.isNormalUser(groupIds);
                groupIds.Remove("undefined");
                statements.Add(new IBatisNetBatchStatement { StatementName = user.GetCreateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                user.setRoles(statements, groupIds);
                /**新增**/

                //if (user.isNormalUser(groupIds))
                //{
                //    statements.Add(new IBatisNetBatchStatement { StatementName = user.GetCreateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                //    user.setRoles(statements, groupIds);
                //}
                //else
                //{
                //    statements.Add(new IBatisNetBatchStatement { StatementName = user.GetCreateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                //}

                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new UserPK { UserId = user.UserId };
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
        [WebInvoke(UriTemplate = "{strUserId}",Method="PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
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
                /***********************begin 自定义代码*******************/
                List<string> groupIds = new List<string>();
                user.isNormalUser(groupIds);
                statements.Add(new IBatisNetBatchStatement { StatementName = user.GetUpdateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                if (groupIds.Count == 0 || groupIds[0] == "undefined")
                {
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery("delete from Pub_GroupMember WHERE UserId='" + user.UserId + "'");                      
                    groupIds.Add("00000000-0000-0000-0000-000000000999");
                    groupIds.Remove("undefined");
                }
                user.setRoles(statements, groupIds);
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
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
                statements.Add(new IBatisNetBatchStatement { StatementName = user.GetUpdateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
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

        #region 扩展接口

        


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
                    PasswordHash = e0571.web.core.Security.MD5Provider.Generate("123")
                };
                statements.Add(new IBatisNetBatchStatement { StatementName = user.GetUpdateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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