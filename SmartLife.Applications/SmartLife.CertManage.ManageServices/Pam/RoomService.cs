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
    public class RoomService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Room> List()
        {
            CollectionInvokeResult<Room> result = new CollectionInvokeResult<Room> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<Room>();
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
        public CollectionInvokeResult<Room> Query(string strParms)
        {
            CollectionInvokeResult<Room> result = new CollectionInvokeResult<Room> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<Room>(dictionary);
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
        public JqgridCollectionInvokeResult<Room> ListForJqgrid(JqgridCollectionParam<Room> param)
        {
            JqgridCollectionInvokeResult<Room> result = new JqgridCollectionInvokeResult<Room> { Success = true };
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

                gridCollectionPager.JqgridDoPage<Room>(BuilderFactory.DefaultBulder().List<Room>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<Room> ListForEasyUIgrid(EasyUIgridCollectionParam<Room> param)
        {
            EasyUIgridCollectionInvokeResult<Room> result = new EasyUIgridCollectionInvokeResult<Room> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<Room>(BuilderFactory.DefaultBulder().List<Room>(filters), param, ref result);
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
        public ModelInvokeResult<RoomPK> Create(Room room)
        {
            ModelInvokeResult<RoomPK> result = new ModelInvokeResult<RoomPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (room.RoomId == GlobalManager.GuidAsAutoGenerate)
                {
                    room.RoomId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                room.OperatedBy = NormalSession.UserId.ToGuid();
                room.OperatedOn = DateTime.Now;
                room.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                int isExist = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount("select * from Pam_Room where RoomNo='" + room.RoomNo + "' and StationId='" + room.StationId + "'");
                if (isExist <= 0)
                {
                    int isExistExtNo = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount("select * from Pam_Room where ExtNo='" + room.ExtNo + "' and StationId='" + room.StationId + "' and ExtNo<>'' and ExtNo is not null");
                    if (isExistExtNo <= 0)
                    {/***********************end 自定义代码*********************/
                        statements.Add(new IBatisNetBatchStatement { StatementName = room.GetCreateMethodName(), ParameterObject = room.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                        /***********************begin 自定义代码*******************/
                        /***********************此处添加自定义代码*****************/
                        /***********************end 自定义代码*********************/
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                        result.instance = new RoomPK { RoomId = room.RoomId };
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = room.ExtNo + "分机号  已被使用";
                    }
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = room.RoomNo + "房间  已经存在";
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
        [WebInvoke(UriTemplate = "{strRoomId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<RoomPK> Update(string strRoomId, Room room)
        {
            ModelInvokeResult<RoomPK> result = new ModelInvokeResult<RoomPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _RoomId = strRoomId.ToGuid();
                if (_RoomId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                int isExist = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount("select * from Pam_Room where RoomNo='" + room.RoomNo + "' and StationId='" + room.StationId + "' and RoomId<>'" + strRoomId + "'");
                if (isExist <= 0)
                {
                    int isExistExtNo = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount("select * from Pam_Room where ExtNo='" + room.ExtNo + "' and StationId='" + room.StationId + "' and RoomId<>'" + strRoomId + "' and ExtNo<>'' and ExtNo is not null");
                    if (isExistExtNo <= 0)
                    {
                        room.RoomId = _RoomId;
                        /***********************begin 自定义代码*******************/
                        room.OperatedBy = NormalSession.UserId.ToGuid();
                        room.OperatedOn = DateTime.Now;
                        room.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                        /***********************end 自定义代码*********************/
                        statements.Add(new IBatisNetBatchStatement { StatementName = room.GetUpdateMethodName(), ParameterObject = room.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                        /***********************begin 自定义代码*******************/
                        /***********************此处添加自定义代码*****************/
                        /***********************end 自定义代码*********************/
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                        result.instance = new RoomPK { RoomId = _RoomId };
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = room.ExtNo + "分机号  已被使用";
                    }
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = room.RoomNo + "房间  已经存在";
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
        [WebInvoke(UriTemplate = "{strRoomId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<RoomPK> Delete(string strRoomId)
        {
            ModelInvokeResult<RoomPK> result = new ModelInvokeResult<RoomPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _RoomId = strRoomId.ToGuid();
                if (_RoomId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                RoomPK pk = new RoomPK { RoomId = _RoomId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new Room().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new RoomPK { RoomId = _RoomId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strRoomIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strRoomIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrRoomIds = strRoomIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrRoomIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Room().GetDeleteMethodName();
                foreach (string strRoomId in arrRoomIds)
                {
                    RoomPK pk = new RoomPK { RoomId = strRoomId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strRoomId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<RoomPK> Nullify(string strRoomId)
        {
            ModelInvokeResult<RoomPK> result = new ModelInvokeResult<RoomPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _RoomId = strRoomId.ToGuid();
                if (_RoomId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                Room room = new Room { RoomId = _RoomId, Status = 0 };
                /***********************begin 自定义代码*******************/
                room.OperatedBy = NormalSession.UserId.ToGuid();
                room.OperatedOn = DateTime.Now;
                room.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = room.GetUpdateMethodName(), ParameterObject = room.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new RoomPK { RoomId = _RoomId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strRoomIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strRoomIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrRoomIds = strRoomIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrRoomIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Room().GetUpdateMethodName();
                foreach (string strRoomId in arrRoomIds)
                {
                    string sql = "select * from Pam_RoomOldMan where EndDate>GETDATE() and RoomId='" + strRoomId + "'";
                    int count = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(sql);
                    if (count < 1)
                    {
                        Room room = new Room { RoomId = strRoomId.ToGuid(), Status = 0 };
                        /***********************begin 自定义代码*******************/
                        room.OperatedBy = NormalSession.UserId.ToGuid();
                        room.OperatedOn = DateTime.Now;
                        room.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                        /***********************end 自定义代码*********************/
                        statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = room.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                    }
                    else
                    {
                        sql = "select RoomNo from Pam_Room where RoomId='" + strRoomId + "'";
                        var roomNo = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sql);
                        result.Success = false;
                        result.ErrorMessage = "“" + roomNo[0]["RoomNo"] + "”房间内有人入住，不允许作废";
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

        #region 级联删除扩展接口 DeleteCascade
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, RoomPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strRoomId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Room> Load(string strRoomId)
        {
            ModelInvokeResult<Room> result = new ModelInvokeResult<Room> { Success = true };
            try
            {
                Guid? _RoomId = strRoomId.ToGuid();
                if (_RoomId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var room = BuilderFactory.DefaultBulder().Load<Room, RoomPK>(new RoomPK { RoomId = _RoomId });
                result.instance = room;
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

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<Room> ListForEasyUIgridPage(EasyUIgridCollectionParam<Room> param)
        {
            EasyUIgridCollectionInvokeResult<Room> result = new EasyUIgridCollectionInvokeResult<Room> { Success = true };
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
                        filters[field.key] = field.value;
                    }
                }
                /**********************************************************/
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
                gridCollectionPager.EasyUIgridDoPage4Model<Room>(filters, param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 给用户授权房间
        [WebInvoke(UriTemplate = "UserPermissionRoom/{userId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SaveUserPermissionRoom(string userId, IList<string> roomIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                UserMappingRoom ur = new UserMappingRoom();
                ur.UserId = userId.ToGuid();
                statements.Add(new IBatisNetBatchStatement { StatementName = ur.GetDeleteMethodName(), ParameterObject = ur.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                ur.OperatedOn = DateTime.Now;
                ur.OperatedBy = NormalSession.UserId.ToGuid();
                foreach (var roomId in roomIds)
                {
                    if (roomId.ToGuid() != null)
                    {
                        ur.RoomId = roomId.ToGuid();
                        statements.Add(new IBatisNetBatchStatement { StatementName = ur.GetCreateMethodName(), ParameterObject = ur.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }
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


        #region 查询房间内现住的老人
        [WebGet(UriTemplate = "RoomOldManNow/{roomId},{stationId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> RoomOldManNow(string roomId, string stationId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary>() { Success = true };

            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                filters["RoomId"] = roomId;
                filters["StationId"] = stationId;
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetRoomOldManNow", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;
            }

            return result;
        }
        #endregion

        #region 老人入住房间
        [WebInvoke(UriTemplate = "DistributeRoom", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<RoomOldManPK> DistributeRoom(RoomOldMan roomOldMan)
        {
            ModelInvokeResult<RoomOldManPK> result = new ModelInvokeResult<RoomOldManPK> { Success = true };
            try
            {
                var ret = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery("select  case when BedNo>OccupancyNumber then 1 else 0 end as IsFull from Pam_Room where RoomId='" + roomOldMan.RoomId + "'");

                if (ret[0]["IsFull"].ToString() == "1")
                {
                    /***********************判断老人是否在房间内*******************/
                    string sql_isInRoom = "select * from Pam_RoomOldMan where OldManId='" + roomOldMan.OldManId + "' and EndDate>GETDATE()";
                    var count = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(sql_isInRoom);
                    /***********************判断老人是否在房间内*******************/
                    if (count < 1)
                    {
                        List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                        /***********************房间和老人建立关系*******************/
                        roomOldMan.OperatedBy = NormalSession.UserId.ToGuid();
                        roomOldMan.OperatedOn = DateTime.Now;
                        roomOldMan.BeginDate = DateTime.Now;
                        roomOldMan.EndDate = Convert.ToDateTime("2999-12-31 23:59:59");
                        statements.Add(new IBatisNetBatchStatement { StatementName = roomOldMan.GetCreateMethodName(), ParameterObject = roomOldMan.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                        /***********************房间和老人建立关系*********************/

                        /***********************begin 房间入住人数加一*******************/
                        string sql = "update Pam_Room set OccupancyNumber=OccupancyNumber+1 where RoomId='" + roomOldMan.RoomId + "'";
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql);
                        /***********************end 房间入住人加一*********************/
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "该老人已经入住在房间内，请刷新后重试";
                    }
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "房间已经注满";
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

        #region 老人变更房间
        [WebInvoke(UriTemplate = "ChangeRoom/{roomIdOld}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<RoomOldManPK> ChangeRoom(string roomIdOld, RoomOldMan roomOldMan)
        {
            ModelInvokeResult<RoomOldManPK> result = new ModelInvokeResult<RoomOldManPK> { Success = true };
            try
            {
                var ret = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery("select  case when BedNo>OccupancyNumber then 1 else 0 end as IsFull from Pam_Room where RoomId='" + roomOldMan.RoomId + "'");

                if (ret[0]["IsFull"].ToString() == "1")
                {
                    /***********************判断老人是否在房间内*******************/
                    string sql_isInRoom = "select * from Pam_RoomOldMan where RoomId='" + roomIdOld + "' and OldManId='" + roomOldMan.OldManId + "' and EndDate>GETDATE()";
                    var count = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(sql_isInRoom);
                    /***********************判断老人是否在房间内*******************/
                    if (count > 0)
                    {
                        /***********************作废此老人和房间的关系*******************/
                        string sql_roomOldMan = "update Pam_RoomOldMan set OperatedBy='" + NormalSession.UserId.ToGuid() + "',OperatedOn=GETDATE(),EndDate=GETDATE()" +
                                     "where RoomId='" + roomIdOld + "' and OldManId='" + roomOldMan.OldManId + "' and EndDate>GETDATE()";
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql_roomOldMan);
                        /***********************作废此老人和房间的关系*******************/

                        /***********************begin 房间入住人数减一*******************/
                        string sql_room = "update Pam_Room set OccupancyNumber=OccupancyNumber-1 where RoomId='" + roomIdOld + "'";
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql_room);
                        /***********************end 房间入住人数减一*********************/

                        List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                        /***********************房间和老人建立关系*******************/
                        roomOldMan.OperatedBy = NormalSession.UserId.ToGuid();
                        roomOldMan.OperatedOn = DateTime.Now;
                        roomOldMan.BeginDate = DateTime.Now;
                        roomOldMan.EndDate = Convert.ToDateTime("2999-12-31 23:59:59");
                        statements.Add(new IBatisNetBatchStatement { StatementName = roomOldMan.GetCreateMethodName(), ParameterObject = roomOldMan.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                        /***********************房间和老人建立关系*********************/

                        /***********************begin 房间入住人数加一*******************/
                        string sql = "update Pam_Room set OccupancyNumber=OccupancyNumber+1 where RoomId='" + roomOldMan.RoomId + "'";
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql);
                        /***********************end 房间入住人加一*********************/

                        /***********************begin 更改工作计划的房间ＩＤ*******************/
                        string sql_workPlan = "update Pam_WorkPlan set RoomId='" + roomOldMan.RoomId + "' ,OperatedBy='" + NormalSession.UserId.ToGuid() + "',OperatedOn=GETDATE() where  Status=1 and PlanDate>GETDATE() and OldManId='" + roomOldMan.OldManId + "'";
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql_workPlan);
                        /***********************end 更改工作计划的房间ＩＤ*********************/
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "老人当前不在此房间内，请刷新后重试";
                    }
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "房间已经注满";
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

        #region 老人退出房间（作废老人的工作计划）
        [WebInvoke(UriTemplate = "GetOutRoomAndNullifyWorkPlan/{roomId},{oldManId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<RoomOldManPK> GetOutRoomAndNullifyWorkPlan(string roomId, string oldManId)
        {
            ModelInvokeResult<RoomOldManPK> result = new ModelInvokeResult<RoomOldManPK> { Success = true };
            try
            {
                if (roomId.ToGuid() == null || oldManId.ToGuid() == null)
                {
                    result.Success = false;
                    result.ErrorMessage = "操作对象和房间均不能为空";
                }
                else
                {
                    /***********************判断老人是否在房间内*******************/
                    string sql_isInRoom = "select * from Pam_RoomOldMan where RoomId='" + roomId + "' and OldManId='" + oldManId + "' and EndDate>GETDATE()";
                    var count = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(sql_isInRoom);
                    /***********************判断老人是否在房间内*******************/
                    if (count > 0)
                    {
                        /***********************作废此老人的所有未做的工作计划*********************/
                        string sql_workPlan = "update Pam_WorkPlan set Status=0  ,OperatedBy='" + NormalSession.UserId.ToGuid() + "',OperatedOn=GETDATE() where  Status=1 and PlanDate>GETDATE() and OldManId='" + oldManId + "'";
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql_workPlan); 
                        /***********************作废此老人的所有未做的工作计划*******************/

                        /***********************作废此老人和房间的关系*******************/
                        string sql_roomOldMan = "update Pam_RoomOldMan set OperatedBy='" + NormalSession.UserId.ToGuid() + "',OperatedOn=GETDATE(),EndDate=GETDATE()" +
                                     "where RoomId='" + roomId + "' and OldManId='" + oldManId + "' and EndDate>GETDATE()";
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql_roomOldMan);
                        /***********************作废此老人和房间的关系*******************/ 

                        /***********************begin 房间入住人数减一*******************/
                        string sql_room = "update Pam_Room set OccupancyNumber=OccupancyNumber-1 where RoomId='" + roomId + "'";
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql_room);
                        /***********************end 房间入住人数减一*********************/

                        /***********************作废当天未完成的工作*********************/
                        string sql_workExcute = "delete from Pam_WorkExecute where ServeManArriveTime is null and RemindTime>GETDATE() and OldManId='" + oldManId + "'";
                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql_workExcute);
                        /***********************作废当天未完成的工作*********************/
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "老人当前不在此房间内，请刷新后重试";
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

        #region 获取机构的空余房间
        [WebInvoke(UriTemplate = "RoomEmptyBedList", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CollectionInvokeResult<StringObjectDictionary> RoomEmptyBedList(Room room)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                filters = room.ToStringObjectDictionary(false);
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetRoomEmptyBeds", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 老人曾经住过的房间
        [WebGet(UriTemplate = "LivedRoom/{oldManId},{stationId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LivedRoom(string oldManId, string stationId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary>() { Success = true };

            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                filters["OldManId"] = oldManId;
                filters["StationId"] = stationId;
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetLivedRoom2", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;
            }

            return result;
        }
        #endregion

        #region 机构房间一览
        [WebGet(UriTemplate = "GetRoomInfoWithOldManForStation/{strStationId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetRoomInfoWithOldManForStation(string strStationId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                filters["StationId"] = strStationId.ToGuid();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetRoomListForStation", filters);
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

