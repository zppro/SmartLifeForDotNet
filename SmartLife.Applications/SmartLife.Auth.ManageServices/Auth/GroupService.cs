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

namespace SmartLife.Auth.ManageServices.Auth
{ 
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class GroupService : AppBaseWcfService
    {
        #region CMS标准接口 

        #region EasyUIgrid数据格式的列表 ListForEasyUIgrid
        [WebInvoke(UriTemplate = "ListForEasyUIgrid/{connectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<Group> ListForEasyUIgrid(string connectId, EasyUIgridCollectionParam<Group> param)
        {
            EasyUIgridCollectionInvokeResult<Group> result = new EasyUIgridCollectionInvokeResult<Group> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<Group>(BuilderFactory.DefaultBulder(connectId).List<Group>(filters), param, ref result);
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
        public ModelInvokeResult<GroupPK> Create(Group group)
        {
            ModelInvokeResult<GroupPK> result = new ModelInvokeResult<GroupPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (group.GroupId == GlobalManager.GuidAsAutoGenerate)
                {
                    group.GroupId = Guid.NewGuid();
                }
                group.OperatedBy = NormalSession.UserId.ToGuid();
                group.OperatedOn = DateTime.Now;
                group.AreaId = GetHttpHeader(GlobalManager.AreaIdKey);
                statements.Add(new IBatisNetBatchStatement { StatementName = group.GetCreateMethodName(), ParameterObject = group.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new GroupPK { GroupId = group.GroupId };
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
        [WebInvoke(UriTemplate = "{strGroupId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<GroupPK> Update(string strGroupId, Group group)
        {
            ModelInvokeResult<GroupPK> result = new ModelInvokeResult<GroupPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _GroupId = strGroupId.ToGuid();
                if (_GroupId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                group.GroupId = _GroupId;
                group.OperatedBy = NormalSession.UserId.ToGuid();
                group.OperatedOn = DateTime.Now;
                statements.Add(new IBatisNetBatchStatement { StatementName = group.GetUpdateMethodName(), ParameterObject = group.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new GroupPK { GroupId = _GroupId };

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
        [WebInvoke(UriTemplate = "{strGroupId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<GroupPK> Delete(string strGroupId)
        {
            ModelInvokeResult<GroupPK> result = new ModelInvokeResult<GroupPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _GroupId = strGroupId.ToGuid();
                if (_GroupId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                GroupPK pk = new GroupPK { GroupId = _GroupId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new Group().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strGroupIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strGroupIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrGroupIds = strGroupIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrGroupIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Group().GetDeleteMethodName();
                foreach (string strGroupId in arrGroupIds)
                {
                    GroupPK pk = new GroupPK { GroupId = strGroupId.ToGuid() };
                    DeleteCascade(statements, pk);
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pk, Type = SqlExecuteType.DELETE });
                }
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);
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
        [WebInvoke(UriTemplate = "Nullify/{strGroupId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<GroupPK> Nullify(string strGroupId)
        {
            ModelInvokeResult<GroupPK> result = new ModelInvokeResult<GroupPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _GroupId = strGroupId.ToGuid();
                if (_GroupId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                Group group = new Group { GroupId = _GroupId, Status = 0 };
                /***********************begin 自定义代码*******************/
                group.OperatedBy = NormalSession.UserId.ToGuid();
                group.OperatedOn = DateTime.Now; 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = group.GetUpdateMethodName(), ParameterObject = group.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new GroupPK { GroupId = _GroupId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strGroupIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strGroupIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrGroupIds = strGroupIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrGroupIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Group().GetUpdateMethodName();
                foreach (string strGroupId in arrGroupIds)
                {
                    Group group = new Group { GroupId = strGroupId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    group.OperatedBy = NormalSession.UserId.ToGuid();
                    group.OperatedOn = DateTime.Now; 
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = group.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, GroupPK pk)
        {
            //此处增加级联删除代码
            //删除组成员关系
            GroupMember gm = new GroupMember { GroupId = pk.GroupId };
            statements.Add(new IBatisNetBatchStatement { StatementName = gm.GetDeleteMethodName(), ParameterObject = gm.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });

            //删除组权限
            GroupPermit gp = new GroupPermit { GroupId = pk.GroupId };
            statements.Add(new IBatisNetBatchStatement { StatementName = gp.GetDeleteMethodName(), ParameterObject = gp.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strGroupId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Group> Load(string strGroupId)
        {
            ModelInvokeResult<Group> result = new ModelInvokeResult<Group> { Success = true };
            try
            {
                Guid? _GroupId = strGroupId.ToGuid();
                if (_GroupId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var group = BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).Load<Group, GroupPK>(new GroupPK { GroupId = _GroupId });
                result.instance = group;
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