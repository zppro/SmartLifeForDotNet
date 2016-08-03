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
using SmartLife.Share.Models.Auth;

namespace SmartLife.Auth.ManageServices.Auth
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class LisenceUserService : AppBaseWcfService
    {
        #region CMS标准接口

        #region EasyUIgrid数据格式的列表 ListForEasyUIgrid
        [WebInvoke(UriTemplate = "ListForEasyUIgrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<LisenceUser> ListForEasyUIgrid(EasyUIgridCollectionParam<LisenceUser> param)
        {
            EasyUIgridCollectionInvokeResult<LisenceUser> result = new EasyUIgridCollectionInvokeResult<LisenceUser> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    if (!string.IsNullOrEmpty(param.instance.Area1))
                    {

                        var rows = BuilderFactory.DefaultBulder().List<DictItemEx>("CTE_AreaCityProvice_List",
                         new
                         {
                             ItemId = param.instance.Area1
                         }.ToStringObjectDictionary());

                        var city = rows.Where(item => item.Levels == 2).FirstOrDefault();
                        if (city != null)
                        {
                            param.instance.CityId = city.ItemId;
                        }
                    }

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
                //if (!string.IsNullOrEmpty(param.sort))
                //{
                //    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                //}
                /***********************end 排序***************************/

                gridCollectionPager.EasyUIgridDoPage4Model<LisenceUser>(filters, param, ref result);
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
        public ModelInvokeResult<LisenceUserPK> Create(LisenceUser lisenceUser)
        {
            ModelInvokeResult<LisenceUserPK> result = new ModelInvokeResult<LisenceUserPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements0 = new List<IBatisNetBatchStatement>();
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (lisenceUser.UserId == GlobalManager.GuidAsAutoGenerate)
                {
                    lisenceUser.UserId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                lisenceUser.OperatedBy = NormalSession.UserId.ToGuid();
                lisenceUser.OperatedOn = DateTime.Now; 
                /***********************end 自定义代码*********************/
               
                /***********************begin 自定义代码*******************/
                //解析
                List<string> groupIds = new List<string>();
                List<string> areaIds = new List<string>();
                string areaId = null;
                string[] parts = lisenceUser.UserType.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (parts[0] == GlobalManager.DIKey_01001_NormalUser)
                {
                    lisenceUser.UserType = parts[0];
                    groupIds.AddRange(parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                    areaIds.AddRange(parts[2].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                    areaId = parts[3];
                }
                //绑定用户角色
                if (groupIds.Count > 0)
                {
                    GroupMember gm = new GroupMember() { UserId = lisenceUser.UserId };
                    statements.Add(new IBatisNetBatchStatement { StatementName = gm.GetDeleteMethodName(), ParameterObject = gm.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                    foreach (string groupId in groupIds)
                    {
                        gm.GroupId = Guid.Parse(groupId);
                        gm.UserId = lisenceUser.UserId;
                        gm.OperatedBy = NormalSession.UserId.ToGuid();
                        gm.OperatedOn = DateTime.Now;
                        statements.Add(new IBatisNetBatchStatement { StatementName = gm.GetCreateMethodName(), ParameterObject = gm.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }  
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

                User user = new User()
                {
                    UserId = lisenceUser.UserId,
                    UserCode = lisenceUser.UserCode,
                    UserName = lisenceUser.UserName,
                    UserType = lisenceUser.UserType,
                    PasswordHash = lisenceUser.PasswordHash,
                    Gender = "N",
                    CreatedBy = NormalSession.UserId.ToGuid(),
                    CreatedOn = DateTime.Now,
                    OperatedBy = NormalSession.UserId.ToGuid(),
                    OperatedOn = DateTime.Now,
                    Area1 = areaId
                };
                statements.Add(new IBatisNetBatchStatement { StatementName = user.GetCreateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************end 自定义代码*********************/
                
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);

                var param0 = lisenceUser.ToStringObjectDictionary(false);
                if (!string.IsNullOrEmpty(areaId))
                {
                    param0["Area1"] = areaId;
                    var rows = BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).List<DictItemEx>("CTE_AreaCityProvice_List",
                     new
                     {
                         ItemId = areaId
                     }.ToStringObjectDictionary());
                    var city = rows.Where(item => item.Levels == 2).FirstOrDefault();
                    if (city != null)
                    {
                        param0["CityId"] = city.ItemId;
                    }
                }
               
                statements0.Add(new IBatisNetBatchStatement { StatementName = lisenceUser.GetCreateMethodName(), ParameterObject = param0, Type = SqlExecuteType.INSERT });
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements0);

                result.instance = new LisenceUserPK { UserId = lisenceUser.UserId };
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
        public ModelInvokeResult<LisenceUserPK> Update(string strUserId, LisenceUser lisenceUser)
        {
            ModelInvokeResult<LisenceUserPK> result = new ModelInvokeResult<LisenceUserPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements0 = new List<IBatisNetBatchStatement>();
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _UserId = strUserId.ToGuid();
                if (_UserId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                lisenceUser.UserId = _UserId;
                /***********************begin 自定义代码*******************/
                lisenceUser.OperatedBy = NormalSession.UserId.ToGuid();
                lisenceUser.OperatedOn = DateTime.Now; 
                /***********************end 自定义代码*********************/
                
                /***********************begin 自定义代码*******************/
                //解析
                List<string> groupIds = new List<string>();
                List<string> areaIds = new List<string>();
                string areaId = null;
                string[] parts = lisenceUser.UserType.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (parts[0] == GlobalManager.DIKey_01001_NormalUser)
                {
                    lisenceUser.UserType = parts[0];
                    groupIds.AddRange(parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                    areaIds.AddRange(parts[2].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                    areaId = parts[3];
                }
                //绑定用户角色
                if (groupIds.Count > 0)
                {
                    GroupMember gm = new GroupMember() { UserId = lisenceUser.UserId };
                    statements.Add(new IBatisNetBatchStatement { StatementName = gm.GetDeleteMethodName(), ParameterObject = gm.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                    foreach (string groupId in groupIds)
                    {
                        gm.GroupId = Guid.Parse(groupId);
                        gm.UserId = lisenceUser.UserId;
                        gm.OperatedBy = NormalSession.UserId.ToGuid();
                        gm.OperatedOn = DateTime.Now;
                        statements.Add(new IBatisNetBatchStatement { StatementName = gm.GetCreateMethodName(), ParameterObject = gm.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }
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

                User user = new User()
                {
                    UserId = lisenceUser.UserId,
                    UserCode = lisenceUser.UserCode,
                    UserName = lisenceUser.UserName,
                    UserType = lisenceUser.UserType,
                    PasswordHash = lisenceUser.PasswordHash,
                    Gender = "N",
                    CreatedBy = NormalSession.UserId.ToGuid(),
                    CreatedOn = DateTime.Now,
                    OperatedBy = NormalSession.UserId.ToGuid(),
                    OperatedOn = DateTime.Now,
                    Area1 = areaId
                };
                statements.Add(new IBatisNetBatchStatement { StatementName = user.GetUpdateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);

                var param0 = lisenceUser.ToStringObjectDictionary(false);
                if (!string.IsNullOrEmpty(areaId))
                {
                    param0["Area1"] = areaId;
                    var rows = BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).List<DictItemEx>("CTE_AreaCityProvice_List",
                     new
                     {
                         ItemId = areaId
                     }.ToStringObjectDictionary());

                    var city = rows.Where(item => item.Levels == 2).FirstOrDefault();
                    if (city != null)
                    {
                        param0["CityId"] = city.ItemId;
                    }
                }

                statements0.Add(new IBatisNetBatchStatement { StatementName = lisenceUser.GetUpdateMethodName(), ParameterObject = param0, Type = SqlExecuteType.UPDATE });
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements0);

                
                result.instance = new LisenceUserPK { UserId = _UserId };

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
        public ModelInvokeResult<LisenceUserPK> Delete(string strUserId)
        {
            ModelInvokeResult<LisenceUserPK> result = new ModelInvokeResult<LisenceUserPK> { Success = true };
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
                LisenceUserPK pk = new LisenceUserPK { UserId = _UserId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new LisenceUser().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new LisenceUserPK { UserId = _UserId };
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
                string statementName = new LisenceUser().GetDeleteMethodName();
                foreach (string strUserId in arrUserIds)
                {
                    LisenceUserPK pk = new LisenceUserPK { UserId = strUserId.ToGuid() };
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
        public ModelInvokeResult<LisenceUserPK> Nullify(string strUserId)
        {
            ModelInvokeResult<LisenceUserPK> result = new ModelInvokeResult<LisenceUserPK> { Success = true };
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
                LisenceUser lisenceUser = new LisenceUser { UserId = _UserId, Status = 0 };
                /***********************begin 自定义代码*******************/
                lisenceUser.OperatedBy = NormalSession.UserId.ToGuid();
                lisenceUser.OperatedOn = DateTime.Now; 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = lisenceUser.GetUpdateMethodName(), ParameterObject = lisenceUser.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new LisenceUserPK { UserId = _UserId };
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
                List<IBatisNetBatchStatement> statements0 = new List<IBatisNetBatchStatement>();
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrUserIds = strUserIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrUserIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                LisenceUser lisenceUser = new LisenceUser { Status = 0, OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now };
                User user = new User { Status = 0, OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now };
                string statementName0 = lisenceUser.GetUpdateMethodName();
                string statementName = user.GetUpdateMethodName();
                foreach (string strUserId in arrUserIds)
                {
                    lisenceUser.UserId = strUserId.ToGuid();
                    statements0.Add(new IBatisNetBatchStatement { StatementName = statementName0, ParameterObject = lisenceUser.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });

                    user.UserId = strUserId.ToGuid();
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements0);
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, LisenceUserPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strUserId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<LisenceUser> Load(string strUserId)
        {
            ModelInvokeResult<LisenceUser> result = new ModelInvokeResult<LisenceUser> { Success = true };
            try
            {
                Guid? _UserId = strUserId.ToGuid();
                if (_UserId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var lisenceUser = BuilderFactory.DefaultBulder().Load<LisenceUser, LisenceUserPK>(new LisenceUserPK { UserId = _UserId });
                result.instance = lisenceUser;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 停用所选 StopSelected
        [WebInvoke(UriTemplate = "StopSelected/{strUserIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult StopSelected(string strUserIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements0 = new List<IBatisNetBatchStatement>();
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrUserIds = strUserIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrUserIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                LisenceUser lisenceUser = new LisenceUser { StopFlag = 1, OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now };
                User user = new User { StopFlag = 1, OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now };
                string statementName0 = lisenceUser.GetUpdateMethodName();
                string statementName = user.GetUpdateMethodName();
                foreach (string strUserId in arrUserIds)
                {
                    lisenceUser.UserId = strUserId.ToGuid();
                    statements0.Add(new IBatisNetBatchStatement { StatementName = statementName0, ParameterObject = lisenceUser.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });

                    user.UserId = strUserId.ToGuid();
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements0);
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
                List<IBatisNetBatchStatement> statements0 = new List<IBatisNetBatchStatement>();
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrUserIds = strUserIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrUserIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                LisenceUser lisenceUser = new LisenceUser { StopFlag = 0, OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now };
                User user = new User { StopFlag = 0, OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now };
                string statementName0 = lisenceUser.GetUpdateMethodName();
                string statementName = user.GetUpdateMethodName();
                foreach (string strUserId in arrUserIds)
                {
                    lisenceUser.UserId = strUserId.ToGuid();
                    statements0.Add(new IBatisNetBatchStatement { StatementName = statementName0, ParameterObject = lisenceUser.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });

                    user.UserId = strUserId.ToGuid();
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements0);
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

        #region 修改用户密码
        [WebInvoke(UriTemplate = "ChangePassword/{strUserId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult ChangePassword(string strUserId, LisenceUser lisenceUser)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements0 = new List<IBatisNetBatchStatement>();
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                User user = new User { UserId = strUserId.ToGuid(), OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now };

                lisenceUser.UserId = strUserId.ToGuid();
                lisenceUser.OperatedBy = NormalSession.UserId.ToGuid();
                lisenceUser.OperatedOn = DateTime.Now;

                if (!string.IsNullOrEmpty(lisenceUser.PasswordHash))
                {
                    user.PasswordHash = e0571.web.core.Security.MD5Provider.Generate(lisenceUser.PasswordHash); ;
                    lisenceUser.PasswordHash = e0571.web.core.Security.MD5Provider.Generate(lisenceUser.PasswordHash); ;
                }
                else
                {
                    user.PasswordHash = "";
                    lisenceUser.PasswordHash = "";
                }
                
                /***********************begin 自定义代码*******************/
                statements.Add(new IBatisNetBatchStatement { StatementName = user.GetUpdateMethodName(), ParameterObject = user.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                 
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);

                statements0.Add(new IBatisNetBatchStatement { StatementName = lisenceUser.GetUpdateMethodName(), ParameterObject = lisenceUser.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements0);

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