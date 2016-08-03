using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
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
using System.IO;
using Newtonsoft.Json;

namespace SmartLife.Share.Behaviors
{
    /// <summary>
    /// 此类专门处理单一的数据请求（针对Sys和Pub数据）
    /// </summary>
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class __AjaxDataService: BaseWcfService
    {
        #region 获取应用别名
        [WebGet(UriTemplate = "ApplicationAliasName/{applicationId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<StringValueRet> GetApplicationAliasName(string applicationId)
        {
            InvokeResult<StringValueRet> result = new InvokeResult<StringValueRet>() { Success = true, ret = new StringValueRet() };
            result.ret.Value = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).Load<Application, ApplicationPK>(new ApplicationPK { ApplicationId = applicationId }).AliasName;
            return result;
        }
        #endregion

        #region 获取所有平台数据
        [WebGet(UriTemplate = "PlatformApplications", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<PlatformApplicationRet> GetPlatformApplications()
        {
            CollectionInvokeResult<PlatformApplicationRet> result = new CollectionInvokeResult<PlatformApplicationRet> { Success = true, rows = new List<PlatformApplicationRet>() };
            try
            { 
                var platforms = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Platform>(new { Status = 1, OrderByClause = "OrderNo asc" }.ToStringObjectDictionary());
                var platformApplications = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<PlatformApplication>();
                var applications = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Application>(new { Status = 1, OrderByClause = "OrderNo asc" }.ToStringObjectDictionary());
                foreach (var platform in platforms)
                {
                    PlatformApplicationRet ret = new PlatformApplicationRet { TheOne = platform };
                    IList<string> appsIdInPlatform = platformApplications.Where(item => item.PlatformId == platform.PlatformId).Select(item=>item.ApplicationId).ToList();
                    ret.Applications = applications.Where(item => appsIdInPlatform.Contains(item.ApplicationId)).ToList();

                    result.rows.Add(ret);
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

        #region 获取所有应用数据数据
        [WebGet(UriTemplate = "Application", ResponseFormat = WebMessageFormat.Json)]
        public IList<Application> GetApplication()
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Application>(new { Status = 1, OrderByClause = "OrderNo asc" }.ToStringObjectDictionary());
        }
        #endregion

        #region 获取所有服务器数据
        [WebGet(UriTemplate = "servers", ResponseFormat = WebMessageFormat.Json)]
        public IList<Server> GetServers()
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Server>(new { Status = 1, OrderByClause = "ServerId asc" }.ToStringObjectDictionary());
        }
        #endregion

        #region 获取应用菜单 (各个应用分开部署)
        [WebGet(UriTemplate = "AppMenu/{applicationId},{parentId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Menu> GetAppMenu(string applicationId, string parentId)
        {
            CollectionInvokeResult<Menu> result = new CollectionInvokeResult<Menu> { Success = true };
            try
            {
                if (parentId.ToLower() == "null")
                {
                    parentId = "*";
                }
                result.rows = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Menu>(new { ApplicationId = applicationId, ParentId = parentId, Status = 1, OrderByClause = "OrderNo asc" }.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取某级菜单 (所有应用一起部署)
        [OperationValidateSession]
        [WebGet(UriTemplate = "Menu/{parentId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Menu> GetMenu(string parentId)
        {
            CollectionInvokeResult<Menu> result = new CollectionInvokeResult<Menu> { Success = true };
            try
            {
                if (parentId.ToLower() == "null")
                {
                    parentId = "*";
                }
                User user = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };
                var parms = new { ParentId = parentId, Status = 1, OrderByClause = "OrderNo asc" }.ToStringObjectDictionary();
                if (user.isSuperAdmin())
                {
                    //超级管理员
                    result.rows = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Menu>(parms);
                }
                else
                {
                    //检测权限
                    parms["UserId"] = NormalSession.UserId.ToGuid();
                    result.rows = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Menu>("Menu_ListByPermisson", parms);
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

        #region 获取所有上级菜单 (所有应用一起部署)
        [WebGet(UriTemplate = "MenuAsCTE/{menuId},{ancestorFlag},{inCludeSelfFlag},{permitFlag}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Menu> GetMenuAsCTE(string menuId, string ancestorFlag, string inCludeSelfFlag, string permitFlag)
        {
            CollectionInvokeResult<Menu> result = new CollectionInvokeResult<Menu> { Success = true };
            try
            {
                if (menuId.ToLower() == "null")
                {
                    menuId = "*";
                }
                User user = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };
                var parms = new { UserId = user.UserId, MenuId = menuId, InCludeSelfFlag = Convert.ToInt32(inCludeSelfFlag) }.ToStringObjectDictionary();

                if (!user.isSuperAdmin())
                {
                    if (permitFlag == "1")
                    {
                        parms["PermitFlag"] = 1;
                    }
                    
                } 
                string statementName = ancestorFlag == "1" ? "Menu_CTE_ListAncestor" : "Menu_CTE_ListDescendant";
                result.rows = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Menu>(statementName, parms);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取非末级菜单
        [WebGet(UriTemplate = "MenusAsParent", ResponseFormat = WebMessageFormat.Json)]
        public IList<Menu> GetMenusAsParent()
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Menu>(new { EndFlag = 0, Status = 1, OrderByClause = "Levels asc,OrderNo asc" }.ToStringObjectDictionary());
        }
        #endregion

        #region 获取菜单信息 
        [WebGet(UriTemplate = "GetMenuByCode/{menuCode}", ResponseFormat = WebMessageFormat.Json)]
        public Menu GetMenuByCode(string menuCode)
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Menu>(new { MenuCode = menuCode, Status = 1, OrderByClause = "Levels asc,OrderNo asc" }.ToStringObjectDictionary()).FirstOrDefault();
        }
        #endregion

        #region 获取字典数据
        [WebGet(UriTemplate = "GetDictionaryItem/{dictionaryId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictItem> GetDictionaryItem(string dictionaryId)
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).List<DictionaryItem>(
                    new DictionaryItem
                    {
                        DictionaryId = dictionaryId,
                        Status = 1
                    }).Select(item => new DictItem { ItemId = item.ItemId, ItemCode = item.ItemCode, ItemName = item.ItemName }).ToList();
        }
        #endregion

        #region 关键字获取字典数据
        [WebGet(UriTemplate = "GetDictionaryItemByKeyword/{dictionaryId},{keyword}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictItem> GetDictionaryItemByKeyword(string dictionaryId, string keyword)
        {
            var param = new DictionaryItem
                    {
                        DictionaryId = dictionaryId,
                        Status = 1
                    }.ToStringObjectDictionary(false); 
            if (keyword.Trim() != "")
            {
                param["WhereClause"] = string.Format("(InputCode1 like '{0}%' or ItemCode like '{0}%' or ItemName like '{0}%' )", keyword);
            }
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<DictionaryItem>(param).Select(item => new DictItem { ItemId = item.ItemId, ItemCode = item.ItemCode, ItemName = item.ItemName }).Take(50).ToList();
        }
        #endregion

        #region 获取字典数据扩展数据
        [WebGet(UriTemplate = "GetDictionaryItemWithExtendInfo/{dictionaryId},{extendColNames}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictItemExtendInfo> GetDictionaryItemWithExtendInfo(string dictionaryId, string extendColNames)
        {
            IList<DictionaryItemExtendConfig> cols = JsonConvert.DeserializeObject<IList<DictionaryItemExtendConfig>>(extendColNames);
            string sql = "select ItemId,ItemCode,ItemName {0} from {1} where DictionaryId='{2}' ";
            if (cols.Count > 0)
            {
                List<string> extendStrs = new List<string>();
                 
                foreach (var col in cols)
                {
                    switch(col.ExtendColType){
                        case "s":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemStringVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                            case "d":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemDateTimeVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                            case "b":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemBitVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                            case "i":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemIntVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                             case "f":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemFloatVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                        default:
                            break;

                    } 
                }
                sql = string.Format(sql,","+ string.Join(",",extendStrs.ToArray()) , new DictionaryItem().GetMappingTableName(), dictionaryId);
            }
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteNativeSqlForQuery(sql).Select(item => new DictItemExtendInfo { ItemId = item["ItemId"].ToString(), ItemCode = item["ItemCode"].ToString(), ItemName = item["ItemName"].ToString(), ExtendInfos = BuildDictionaryItemExtendInfos(cols, item) }).ToList();

        }
        [WebInvoke(UriTemplate = "GetDictionaryItemWithExtendInfo2/{dictionaryId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public IList<DictItemExtendInfo> GetDictionaryItemWithExtendInfo2(string dictionaryId, IList<DictionaryItemExtendConfig> cols)
        {
            string sql = "select ItemId,ItemCode,ItemName {0} from {1} where DictionaryId='{2}' ";
            if (cols.Count > 0)
            {
                List<string> extendStrs = new List<string>();

                foreach (var col in cols)
                {
                    switch (col.ExtendColType)
                    {
                        case "s":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemStringVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                        case "d":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemDateTimeVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                        case "b":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemBitVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                        case "i":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemIntVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                        case "f":
                            extendStrs.Add("dbo.FUNC_Tol_GetDictionaryItemFloatVal(DictionaryId,ItemId,'" + col.ExtendCol + "') as " + col.ExtendCol);
                            break;
                        default:
                            break;

                    }
                }
                sql = string.Format(sql, "," + string.Join(",", extendStrs.ToArray()), new DictionaryItem().GetMappingTableName(), dictionaryId);
            }
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteNativeSqlForQuery(sql).Select(item => new DictItemExtendInfo { ItemId = item["ItemId"].ToString(), ItemCode = item["ItemCode"].ToString(), ItemName = item["ItemName"].ToString(), ExtendInfos = BuildDictionaryItemExtendInfos(cols, item) }).ToList();

        }
        private List<DictionaryItemExtendInfo> BuildDictionaryItemExtendInfos(IList<DictionaryItemExtendConfig> cols, StringObjectDictionary item)
        {
            List<DictionaryItemExtendInfo> extendInfos =null;

            if (cols.Count > 0)
            {
                extendInfos = new List<DictionaryItemExtendInfo>();
                foreach (var col in cols)
                {
                    extendInfos.Add(new DictionaryItemExtendInfo { ExtendCol = col.ExtendCol, ExtendValue = TypeConverter.ChangeString(item[col.ExtendCol]) });
                }
            }
            return extendInfos;
        }
        #endregion

        #region 获取字典数据按扩展数据过滤
        [WebGet(UriTemplate = "GetDictionaryItemByExtendCol/{dictionaryId},{extendColName},{extendColType},{extendColValue}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictItem> GetDictionaryItemByExtendCol(string dictionaryId, string extendColName, string extendColType, string extendColValue)
        {
            string extendValueFilterSubQueryStr = "";
            switch (extendColType)
            {
                case "s":
                    extendValueFilterSubQueryStr = string.Format(" select ItemId from Sys_DictionaryItemExtend where DictionaryId='{0}' and ExtendCol='{1}' and V00001='{2}' ", dictionaryId, extendColName, extendColValue);
                    break;
                case "d":
                    extendValueFilterSubQueryStr = string.Format(" select ItemId from Sys_DictionaryItemExtend where DictionaryId='{0}' and ExtendCol='{1}' and V00005={2} ", dictionaryId, extendColName, extendColValue);
                    break;
                case "b":
                    extendValueFilterSubQueryStr = string.Format(" select ItemId from Sys_DictionaryItemExtend where DictionaryId='{0}' and ExtendCol='{1}' and V00003={2} ", dictionaryId, extendColName, extendColValue);
                    break;
                case "i":
                    extendValueFilterSubQueryStr = string.Format(" select ItemId from Sys_DictionaryItemExtend where DictionaryId='{0}' and ExtendCol='{1}' and V00004={2} ", dictionaryId, extendColName, extendColValue);
                    break;
                case "f":
                    extendValueFilterSubQueryStr = string.Format(" select ItemId from Sys_DictionaryItemExtend where DictionaryId='{0}' and ExtendCol='{1}' and DateDiff(d,V00003,'{2}')=0 ", dictionaryId, extendColName, extendColValue);
                    break;
                default:
                    break;
            }
            string sql = string.Format("select ItemId,ItemCode,ItemName  from {0} where DictionaryId='{1}' and ItemId in ({2})", new DictionaryItem().GetMappingTableName(), dictionaryId, extendValueFilterSubQueryStr);
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteNativeSqlForQuery(sql).Select(item => new DictItem { ItemId = item["ItemId"].ToString(), ItemCode = item["ItemCode"].ToString(), ItemName = item["ItemName"].ToString() }).ToList();
        }
        #endregion

        #region 获取非末级字典项目
        [WebGet(UriTemplate = "DictionaryItemsAsParent/{dictionaryId},{parentId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictionaryItem> DictionaryItemsAsParent(string dictionaryId, string parentId)
        {
            var parms = new { DictionaryId = dictionaryId, EndFlag = 0, Status = 1, OrderByClause = "Levels asc,OrderNo asc" }.ToStringObjectDictionary();
            if (parentId.ToLower() == "null")
            {
                parms["WhereClause"] = " ParentId is null ";
            }
            var results = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<DictionaryItem>(parms);
            return results;
        }
        #endregion

        #region 取行为数据
        [WebGet(UriTemplate = "Behavior", ResponseFormat = WebMessageFormat.Json)]
        public IList<Behavior> GetBehavior()
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Behavior>(new { Status = 1, OrderByClause = "BehaviorCode asc" }.ToStringObjectDictionary());
        }
        #endregion

        #region 取行为数据
        [WebGet(UriTemplate = "BehaviorByMenu/{menuId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<StringObjectDictionary> GetBehaviorByMenu(string menuId)
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ListStringObjectDictionary("Behavior_ListByMenu", new { MenuId = menuId, Status = 1, OrderByClause = "BehaviorCode asc" }.ToStringObjectDictionary());
        }
        #endregion

        #region 获取菜单行为
        [WebGet(UriTemplate = "Behavior/{menuId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<StringObjectDictionary> GetBehaviorPermit(string menuId)
        {
            IList<StringObjectDictionary> rows = null;
            try
            {
                User user = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };
                var parms = new { MenuId = menuId, Status = 1, OrderByClause = "a.BehaviorCode asc" }.ToStringObjectDictionary(false, e0571.web.core.Other.CaseSensitive.NORMAL);
                if (user.isSuperAdmin())
                {
                    //超级管理员
                    rows = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ListStringObjectDictionary("BehaviorForMenu_List", parms);
                }
                else
                {
                    //检测权限
                    parms["UserId"] = NormalSession.UserId.ToGuid();
                    rows = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ListStringObjectDictionary("BehaviorForMenu_ListByPermisson", parms);
                }
            }
            catch (Exception ex)
            {

            }
            return rows;
        }
        #endregion

        #region 保存选择菜单行为
        [WebInvoke(UriTemplate = "SaveBehaviorByMenu/{menuId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SaveBehaviorByMenu(string menuId, IList<string> behaviorCodes)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                MenuBehavior mb = new MenuBehavior();
                mb.MenuId = menuId;
                statements.Add(new IBatisNetBatchStatement { StatementName = mb.GetDeleteMethodName(), ParameterObject = mb.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });

                foreach (var behaviorCode in behaviorCodes)
                {
                    mb.BehaviorCode = behaviorCode;
                    statements.Add(new IBatisNetBatchStatement { StatementName = mb.GetCreateMethodName(), ParameterObject = mb.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }

                BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteNativeSqlNoneQuery(statements);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 保存所有菜单行为
        [WebInvoke(UriTemplate = "MenuBehavior", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UpdateMenuBehavior(IList<string> behaviorCodes)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<string> statements = new List<string>();

                MenuBehavior mb = new MenuBehavior();
                statements.Add("delete from " + mb.GetMappingTableName());

                foreach (var behaviorCode in behaviorCodes)
                {
                    string sql = string.Format("insert into {0} select MenuId,'{1}' from sys_menu where status = 1 and endFlag = 1 ", mb.GetMappingTableName(), behaviorCode);
                    statements.Add(sql);
                } 

                BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteNativeSqlNoneQuery(statements);
              
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 行为菜单绑定
        [WebInvoke(UriTemplate = "BehaviorBindMenu/{behaviorCode}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult BehaviorBindMenu(string behaviorCode, IList<string> menuIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                MenuBehavior mb = new MenuBehavior();
                mb.BehaviorCode = behaviorCode;
                statements.Add(new IBatisNetBatchStatement { StatementName = mb.GetDeleteMethodName(), ParameterObject = mb.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });

                foreach (var menuId in menuIds)
                {
                    mb.MenuId = menuId;
                    statements.Add(new IBatisNetBatchStatement { StatementName = mb.GetCreateMethodName(), ParameterObject = mb.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }

                BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteNativeSqlNoneQuery(statements);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取用户组
        [WebGet(UriTemplate = "Group", ResponseFormat = WebMessageFormat.Json)]
        public IList<Group> Group()
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Group>(new { Status = 1, OrderByClause = "GroupCode asc" }.ToStringObjectDictionary());
        }

        #region 获取【呼叫中心直通队列】下的用户组
        [WebGet(UriTemplate = "GetGroup", ResponseFormat = WebMessageFormat.Json)]
        public IList<Group> GetGroup()
        {

            IDictionary<string, object> parm = GetRequesParams().MixInObject(new { Status = 1, OrderByClause = "GroupCode asc" });
            if (parm.ContainsKey("GroupType") && TypeConverter.ChangeString(parm["GroupType"]).Contains(","))
            {
                parm["WhereClause"] = "GroupType In('" + string.Join("','", parm["GroupType"].ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)) + "')";
                parm.Remove("GroupType");
            }
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).List<Group>(parm);
        }
        #endregion

        #region 获取所在的用户组
        [WebGet(UriTemplate = "GetGroupsByMember/{userId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<string> GetGroupsByMember(string userId)
        {
            return BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).List<GroupMember>(new GroupMember { UserId = Guid.Parse(userId) }).Select(item => item.GroupId.ToString()).ToList();
        }
        #endregion

        #region 获取【呼叫中心队列】
        [WebGet(UriTemplate = "GetCCGroup/{groupType}", ResponseFormat = WebMessageFormat.Json)]
        public IList<StringObjectDictionary> GetCCGroup(string groupType)
        {
            StringObjectDictionary parm  = new StringObjectDictionary();
            parm["WhereClause"] = "GroupType ='" + groupType + "'";
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).ListStringObjectDictionary("CC_Group_List2", parm);
        }
        #endregion

        #endregion

        #region 获取服务机构
        [WebGet(UriTemplate = "GetServiceStation", ResponseFormat = WebMessageFormat.Json)]
        public IList<ServiceStation> GetServiceStation()
        {

            IDictionary<string, object> parm = GetRequesParams().MixInObject(new { Status = 1, OrderByClause = "StationCode asc" });
            if (parm.ContainsKey("StationType") && TypeConverter.ChangeString(parm["StationType"]).Contains(","))
            {
                parm["WhereClause"] = "StationType In('" + string.Join("','", parm["StationType"].ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)) + "')";
                parm.Remove("StationType");
            }
            if (parm.ContainsKey("StationType2") && TypeConverter.ChangeString(parm["StationType2"]).Contains(","))
            {
                parm["WhereClause"] = "StationType2 In('" + string.Join("','", parm["StationType2"].ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)) + "')";
                parm.Remove("StationType2");
            }
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<ServiceStation>(parm);
        } 

        #endregion

        #region 获取呼叫中心
        [WebGet(UriTemplate = "GetCallCenter", ResponseFormat = WebMessageFormat.Json)]
        public CallCenter GetCallCenter()
        {
            IDictionary<string, object> parm = GetRequesParams().MixInObject(new { OrderByClause = "StationId asc" });
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<CallCenter>().First();
        } 
        #endregion

        #region 保存菜单行为授权
        [WebInvoke(UriTemplate = "GroupPermission/{groupId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SaveGroupPermission(string groupId, IList<string> resourceIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                GroupPermit gp = new GroupPermit();
                gp.GroupId = groupId.ToGuid(); 
                statements.Add(new IBatisNetBatchStatement { StatementName = gp.GetDeleteMethodName(), ParameterObject = gp.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                gp.OperatedOn = DateTime.Now;
                gp.OperatedBy = NormalSession.UserId.ToGuid();
                foreach (var resourceId in resourceIds)
                {
                    gp.ResourceId = resourceId;
                    statements.Add(new IBatisNetBatchStatement { StatementName = gp.GetCreateMethodName(), ParameterObject = gp.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }

                BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).ExecuteNativeSqlNoneQuery(statements);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 分级获取辖区Light
        [WebGet(UriTemplate = "GetAreaLight/{levels}", ResponseFormat = WebMessageFormat.Json)]
        public Stream GetAreaLight(string levels)
        {
            var rows = BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).List<Area>(new { Status = 1, Levels = levels }.ToStringObjectDictionary(false)).Select(item => new { AreaId = item.AreaId.ToString().ToUpper(), AreaName = item.AreaName });
            string result = JsonConvert.SerializeObject(rows);

            byte[] resultBytes = Encoding.UTF8.GetBytes(result);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(resultBytes);
        }
        #endregion

        #region 获取辖区
        [WebGet(UriTemplate = "GetArea", ResponseFormat = WebMessageFormat.Json)]
        public IList<Area> GetArea()
        {
            string q = GetRequestQuery("q");
            string whereClause = null;
            StringObjectDictionary filters = new StringObjectDictionary();
            List<string> whereClauses = new List<string>();
            whereClauses.Add(string.Format(" (AreaCode like '%{0}%' or AreaName like '%{0}%' or InputCode1 like '%{0}%') ", q));
            if (whereClauses.Count > 0)
            {
                whereClause = string.Join(" AND ", whereClauses.ToArray());
            }

            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).List<Area>(new { Status = 1, WhereClause = whereClause, OrderByClause = "AreaCode asc" }.ToStringObjectDictionary(false));
        }
        #endregion

        #region 关键字获取辖区数据
        [WebGet(UriTemplate = "GetAreaVByKeyWord/{keyword}", ResponseFormat = WebMessageFormat.Json)]
        public IList<Area_V> GetAreaVByKeyWord(string keyword)
        {
            string whereClause = null;
            StringObjectDictionary filters = new StringObjectDictionary();
            List<string> whereClauses = new List<string>();
            whereClauses.Add(string.Format(" ( InputCode1 like '{0}%' or AreaCode like '{0}%' or AreaName like '{0}%' ) ", keyword));
            if (whereClauses.Count > 0)
            {
                whereClause = string.Join(" AND ", whereClauses.ToArray());
            }

            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Area_V>(new { Status = 1, WhereClause = whereClause, OrderByClause = "AreaCode asc" }.ToStringObjectDictionary(false)).Take(50).ToList();
        }
        #endregion

        #region 获取非末级辖区
        [WebGet(UriTemplate = "AreasAsParent", ResponseFormat = WebMessageFormat.Json)]
        public IList<Area_V> GetAreasAsParent()
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Area_V>(new { Status = 1, WhereClause = "Levels > 1 ", OrderByClause = "Levels asc,OrderNo asc" }.ToStringObjectDictionary());
        }
        #endregion

        #region 获取用户辖区
        [WebGet(UriTemplate = "GetUserArea/{userId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<string> GetUserArea(string userId)
        {
            return BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).List<UserArea>(new UserArea { UserId = Guid.Parse(userId) }).Select(item => item.AreaId.ToString()).ToList();
        }
        #endregion

        #region 获取部署的节点
        [WebGet(UriTemplate = "GetDeployNode/{parentId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictItemEx2> GetDeployNode(string parentId)
        {
            IList<DictItemEx2> rows = null;
            try
            {
                rows = BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).List<DictItemEx2>("CTE_DeployNode_List",
                        new
                        {
                            DictionaryId = "00005",
                            ParentId = parentId
                        }.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                
            }
            return rows;
        }
        #endregion

        #region 获取某省下的[市+区]字典项目
        [WebGet(UriTemplate = "GetCityAndAreaInProvince/{parentId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictItem> GetCityAndAreaInProvince(string parentId)
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).List<DictItem>("CTE_CityAndArea_List",
                    new 
                    {
                        DictionaryId = "00005",
                        ParentId = parentId
                    }.ToStringObjectDictionary());
        }
        #endregion

        #region 获取某区下的[街道+社区]字典项目
        [WebGet(UriTemplate = "GetStreetAndCommunityInArea/{parentId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictItemEx> GetStreetAndCommunityInArea(string parentId)
        {
            IList<DictItemEx> results = null;
            try
            {
                results = BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).List<DictItemEx>("CTE_StreetAndCommunitiy_List",
                    new
                    {
                        ParentId = parentId
                    }.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return results;
        }
        #endregion

        #region 获取已部署访问点（包含上级）
        [WebGet(UriTemplate = "GetDeployAccessPoint", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<DeployAccessPoint> GetDeployAccessPoint()
        {
            CollectionInvokeResult<DeployAccessPoint> result = new CollectionInvokeResult<DeployAccessPoint> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ListStringObjectDictionary("DeployAccessPoint_CTE_ListAncestor", null).Select(item => new DeployAccessPoint { AccessPoint = TypeConverter.ChangeString(item["AccessPoint"]), Levels = Convert.ToInt32(item["Levels"]), OrderNo = Convert.ToInt32(item["OrderNo"]), ItemName = TypeConverter.ChangeString(item["ItemName"]) }).ToList();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取多个值合并
        [WebGet(UriTemplate = "GetNameString", ResponseFormat = WebMessageFormat.Json)]
        public string GetNameString()
        {
            IDictionary<string, object> parm = GetRequesParams();
            return TypeConverter.ChangeString(BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteScalar("GetJoinedString", parm));
        }
        #endregion

        #region 获取分机
        [WebGet(UriTemplate = "GetExt", ResponseFormat = WebMessageFormat.Json)]
        public IList<ListItemExt> GetExt()
        {
            IDictionary<string, object> parm = GetRequesParams().MixInObject(new { Status = 1, OrderByClause = "ExtCode asc" });

            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<CC_Ext>(parm).Select(item => new ListItemExt { Text = item.ExtCode, Value = item.ExtId + "|" + item.IPCCDial + "|" + item.IPCCRelayType + "|" + item.IPCCRelayPrefix }).ToList();
        }
        #endregion

        #region 获取区域名称
        [WebGet(UriTemplate = "GetAreaName/{areaId}", ResponseFormat = WebMessageFormat.Json)]
        public string GetAreaName(string areaId)
        {
            Area_V area = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).Load<Area_V, Area_VPK>(new Area_VPK { AreaId = areaId });
            if (area.AreaId != null)
            {
                return area.AreaName;
            }
            return "";
        }
        #endregion

        #region 获取区域全名
        [WebGet(UriTemplate = "GetAreaFullName/{areaId}", ResponseFormat = WebMessageFormat.Json)]
        public string GetAreaFullName(string areaId)
        {
            return TypeConverter.ChangeString(BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteScalar("GetAreaFullName", new { AreaId = areaId }));
        }
        #endregion

        #region 获取服务大类对应的商家
        [WebGet(UriTemplate = "GetMerhantsByServeItemA/{openId},{serveItemA},{keyword}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceStation_Light> GetMerhantsByServeItemA(string openId, string serveItemA, string keyword)
        {
            CollectionInvokeResult<ServiceStation_Light> result = new CollectionInvokeResult<ServiceStation_Light> { Success = true };
            var param = new { OpenId = openId, ServeItemA = serveItemA }.ToStringObjectDictionary();
            if (keyword != "null")
            {
                param.Add("Keyword", keyword);
            }
            result.rows = BuilderFactory.DefaultBulder().List<ServiceStation_Light>("ServiceStation_Light_ListAsMerchantByServeItemA", param);
            return result;
        }
        #endregion
    }

    public class PlatformApplicationRet
    {
        public Platform TheOne { get; set; }

        public IList<Application> Applications { get; set; }
    }
     

    public class DictItemExtendInfo
    {
        public string ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        public List<DictionaryItemExtendInfo> ExtendInfos { get; set; }
    }

    public class DeployAccessPoint
    {
        public string AccessPoint { get; set; }
        public int OrderNo { get; set; }
        public int Levels { get; set; }
        public string ItemName { get; set; }
    }

    public class ListItemExt
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class DictionaryItemExtendConfig
    {
        public string ExtendCol { get; set; }
        public string ExtendColType { get; set; }//s,d,b,i,f
    }

    public class DictionaryItemExtendInfo
    {
        public string ExtendCol { get; set; }
        public string ExtendValue { get; set; }
    }
}
