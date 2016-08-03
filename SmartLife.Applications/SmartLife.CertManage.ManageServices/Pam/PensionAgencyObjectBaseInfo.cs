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
    public class PensionAgencyObjectBaseInfoService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<PensionAgencyObjectBaseInfo> List()
        {
            CollectionInvokeResult<PensionAgencyObjectBaseInfo> result = new CollectionInvokeResult<PensionAgencyObjectBaseInfo> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<PensionAgencyObjectBaseInfo>();
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
        public CollectionInvokeResult<PensionAgencyObjectBaseInfo> Query(string strParms)
        {
            CollectionInvokeResult<PensionAgencyObjectBaseInfo> result = new CollectionInvokeResult<PensionAgencyObjectBaseInfo> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<PensionAgencyObjectBaseInfo>(dictionary);
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
        public JqgridCollectionInvokeResult<PensionAgencyObjectBaseInfo> ListForJqgrid(JqgridCollectionParam<PensionAgencyObjectBaseInfo> param)
        {
            JqgridCollectionInvokeResult<PensionAgencyObjectBaseInfo> result = new JqgridCollectionInvokeResult<PensionAgencyObjectBaseInfo> { Success = true };
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

                gridCollectionPager.JqgridDoPage<PensionAgencyObjectBaseInfo>(BuilderFactory.DefaultBulder().List<PensionAgencyObjectBaseInfo>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<PensionAgencyObjectBaseInfo> ListForEasyUIgrid(EasyUIgridCollectionParam<PensionAgencyObjectBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<PensionAgencyObjectBaseInfo> result = new EasyUIgridCollectionInvokeResult<PensionAgencyObjectBaseInfo> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<PensionAgencyObjectBaseInfo>(BuilderFactory.DefaultBulder().List<PensionAgencyObjectBaseInfo>(filters), param, ref result);
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
        public ModelInvokeResult<PensionAgencyObjectBaseInfoPK> Create(PensionAgencyObjectBaseInfo pensionAgencyObjectBaseInfo)
        {
            ModelInvokeResult<PensionAgencyObjectBaseInfoPK> result = new ModelInvokeResult<PensionAgencyObjectBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (pensionAgencyObjectBaseInfo.OldManId == GlobalManager.GuidAsAutoGenerate)
                {
                    pensionAgencyObjectBaseInfo.OldManId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                pensionAgencyObjectBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                pensionAgencyObjectBaseInfo.OperatedOn = DateTime.Now;
                pensionAgencyObjectBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = pensionAgencyObjectBaseInfo.GetCreateMethodName(), ParameterObject = pensionAgencyObjectBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new PensionAgencyObjectBaseInfoPK { OldManId = pensionAgencyObjectBaseInfo.OldManId };
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
        [WebInvoke(UriTemplate = "{strOldManId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<PensionAgencyObjectBaseInfoPK> Update(string strOldManId, PensionAgencyObjectBaseInfo pensionAgencyObjectBaseInfo)
        {
            ModelInvokeResult<PensionAgencyObjectBaseInfoPK> result = new ModelInvokeResult<PensionAgencyObjectBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _OldManId = strOldManId.ToGuid();
                if (_OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                pensionAgencyObjectBaseInfo.OldManId = _OldManId;
                /***********************begin 自定义代码*******************/
                pensionAgencyObjectBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                pensionAgencyObjectBaseInfo.OperatedOn = DateTime.Now;
                pensionAgencyObjectBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = pensionAgencyObjectBaseInfo.GetUpdateMethodName(), ParameterObject = pensionAgencyObjectBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new PensionAgencyObjectBaseInfoPK { OldManId = _OldManId };

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
        [WebInvoke(UriTemplate = "{strOldManId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<PensionAgencyObjectBaseInfoPK> Delete(string strOldManId)
        {
            ModelInvokeResult<PensionAgencyObjectBaseInfoPK> result = new ModelInvokeResult<PensionAgencyObjectBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _OldManId = strOldManId.ToGuid();
                if (_OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                PensionAgencyObjectBaseInfoPK pk = new PensionAgencyObjectBaseInfoPK { OldManId = _OldManId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new PensionAgencyObjectBaseInfo().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new PensionAgencyObjectBaseInfoPK { OldManId = _OldManId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strOldManIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strOldManIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrOldManIds = strOldManIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrOldManIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new PensionAgencyObjectBaseInfo().GetDeleteMethodName();
                foreach (string strOldManId in arrOldManIds)
                {
                    PensionAgencyObjectBaseInfoPK pk = new PensionAgencyObjectBaseInfoPK { OldManId = strOldManId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strOldManId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<PensionAgencyObjectBaseInfoPK> Nullify(string strOldManId)
        {
            ModelInvokeResult<PensionAgencyObjectBaseInfoPK> result = new ModelInvokeResult<PensionAgencyObjectBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _OldManId = strOldManId.ToGuid();
                if (_OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                PensionAgencyObjectBaseInfo pensionAgencyObjectBaseInfo = new PensionAgencyObjectBaseInfo { OldManId = _OldManId, Status = 0 };
                /***********************begin 自定义代码*******************/
                pensionAgencyObjectBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                pensionAgencyObjectBaseInfo.OperatedOn = DateTime.Now;
                pensionAgencyObjectBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = pensionAgencyObjectBaseInfo.GetUpdateMethodName(), ParameterObject = pensionAgencyObjectBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new PensionAgencyObjectBaseInfoPK { OldManId = _OldManId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strOldManIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strOldManIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrOldManIds = strOldManIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrOldManIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new PensionAgencyObjectBaseInfo().GetUpdateMethodName();
                foreach (string strOldManId in arrOldManIds)
                {
                    PensionAgencyObjectBaseInfo pensionAgencyObjectBaseInfo = new PensionAgencyObjectBaseInfo { OldManId = strOldManId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    pensionAgencyObjectBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                    pensionAgencyObjectBaseInfo.OperatedOn = DateTime.Now;
                    pensionAgencyObjectBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pensionAgencyObjectBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, PensionAgencyObjectBaseInfoPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strOldManId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<PensionAgencyObjectBaseInfo> Load(string strOldManId)
        {
            ModelInvokeResult<PensionAgencyObjectBaseInfo> result = new ModelInvokeResult<PensionAgencyObjectBaseInfo> { Success = true };
            try
            {
                Guid? _OldManId = strOldManId.ToGuid();
                if (_OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var pensionAgencyObjectBaseInfo = BuilderFactory.DefaultBulder().Load<PensionAgencyObjectBaseInfo, PensionAgencyObjectBaseInfoPK>(new PensionAgencyObjectBaseInfoPK { OldManId = _OldManId });
                result.instance = pensionAgencyObjectBaseInfo;
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
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridPage(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
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
                        if (field.key == "isMapping" )
                        {
                            whereClause.Add(" OldManId not in ( select OldManId  from Pam_RoomOldMan a left join Pam_Room b on a.RoomId=b.RoomId where a.EndDate>GETDATE() and b.StationId='" + field.value.ToString() + "')");
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
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("GetListOldManByPage", filters, param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridPageByStationId
        [WebInvoke(UriTemplate = "ListForEasyUIgridPageByStationId", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridPageByStationId(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
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
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ListForRoomOldManByPage", filters, param, ref result);
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
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetLivedRoom", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;
            }

            return result;
        }
        #endregion

        #region 查看入住的所有老人
        [WebInvoke(UriTemplate = "ListOldManLivingInRoom", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListOldManLivingInRoom(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
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
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("GetListOldManLivingInRoom", filters, param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 老人入院-旧版
        [WebInvoke(UriTemplate = "OldManAdmission", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult OldManAdmission(OldManAdmissionParams oldManAdmissionParams)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                IList<string> nativeSqlStatments = new List<string>();
                string strSql = "";

                if (oldManAdmissionParams.OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                //配置呼叫号码--有呼叫号码
                if (!string.IsNullOrEmpty(oldManAdmissionParams.CallNo))
                {
                    strSql = string.Format("select * From Pam_AgencyObjectConfigInfo where OldManId='{0}' or " +
                                            " CallNo='{1}'", oldManAdmissionParams.OldManId, oldManAdmissionParams.CallNo);
                    IList<StringObjectDictionary> agencyObjectConfigInfos = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql);

                    if (agencyObjectConfigInfos.Count == 0)
                    {
                        strSql = string.Format("insert into Pam_AgencyObjectConfigInfo(OldManId,OperatedBy,OperatedOn,DataSource,CallNo) " +
                            " values('{0}','{1}',GetDate(),'{2}','{3}') ", oldManAdmissionParams.OldManId, NormalSession.UserId
                            , GlobalManager.DIKey_00012_ManualEdit, oldManAdmissionParams.CallNo);
                        nativeSqlStatments.Add(strSql);
                    }
                    else if (agencyObjectConfigInfos.Count == 1 && agencyObjectConfigInfos.Count(s => s["OldManId"].ToString() == oldManAdmissionParams.OldManId.ToString()) > 0)
                    {
                        strSql = string.Format("update Pam_AgencyObjectConfigInfo set OperatedBy='{0}',OperatedOn=GetDate(),CallNo='{1}' " +
                            " where OldManId='{2}' ", NormalSession.UserId, oldManAdmissionParams.CallNo, oldManAdmissionParams.OldManId);
                        nativeSqlStatments.Add(strSql);
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorCode = 51001;
                        result.ErrorMessage = "当前号码已经使用，请重新确认老人号码";
                        return result;
                    }

                    //这边有问题 待解决
                    strSql = string.Format("select * From Oca_OldManConfigInfo where OldManId='{0}' or CallNo='{1}' or CallNo2='{1}' or CallNo3='{1}' ", oldManAdmissionParams.OldManId, oldManAdmissionParams.CallNo);
                    IList<StringObjectDictionary> oldManConfigInfos = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql);
                    if (oldManConfigInfos.Count == 0)
                    {
                        strSql = string.Format("insert into Oca_OldManConfigInfo(OldManId,OperatedBy,OperatedOn,DataSource,CallNo3) " +
                            " values('{0}','{1}',GetDate(),'{2}','{3}') ", oldManAdmissionParams.OldManId, NormalSession.UserId
                            , GlobalManager.DIKey_00012_ManualEdit, oldManAdmissionParams.CallNo);
                        nativeSqlStatments.Add(strSql);
                    }
                    else if (oldManConfigInfos.Count == 1 && oldManConfigInfos.Count(s => s["OldManId"].ToString() == oldManAdmissionParams.OldManId.ToString()) > 0)
                    {
                        strSql = string.Format("update Oca_OldManConfigInfo set OperatedBy='{0}',OperatedOn=GetDate(),CallNo3='{1}' " +
                            " where OldManId='{2}' ", NormalSession.UserId, oldManAdmissionParams.CallNo, oldManAdmissionParams.OldManId);
                        nativeSqlStatments.Add(strSql);
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorCode = 51001;
                        result.ErrorMessage = "当前号码已经使用，请重新确认老人号码";
                        return result;
                    }
                }
                else {
                    //清空号码
                    strSql = string.Format("select * From Pam_AgencyObjectConfigInfo where OldManId='{0}'", oldManAdmissionParams.OldManId);
                    IList<StringObjectDictionary> agencyObjectConfigInfos = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql);

                    if (agencyObjectConfigInfos.Count > 0)
                    {
                        strSql = string.Format("update Pam_AgencyObjectConfigInfo set OperatedBy='{0}',OperatedOn=GetDate(),CallNo='' where OldManId='{1}' ", NormalSession.UserId, oldManAdmissionParams.OldManId);
                        nativeSqlStatments.Add(strSql);
                    }

                    strSql = string.Format("select * From Oca_OldManConfigInfo where OldManId='{0}' ", oldManAdmissionParams.OldManId);
                    IList<StringObjectDictionary> oldManConfigInfos = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql);
                    if (oldManConfigInfos.Count > 0)
                    {
                        strSql = string.Format("update Oca_OldManConfigInfo set OperatedBy='{0}',OperatedOn=GetDate(),CallNo3='' where OldManId='{1}' ", NormalSession.UserId, oldManAdmissionParams.OldManId);
                        nativeSqlStatments.Add(strSql);
                    }
                }

                //配置房间和床位号
                if (oldManAdmissionParams.RoomId != null) {
                    //历史
                    strSql = string.Format("update Pam_Room set OperatedBy='{0}',OperatedOn=GETDATE(),OccupancyNumber=(case when OccupancyNumber>0 then OccupancyNumber-1 else 0 end) " +
                             " where Status=1 and RoomId=(select top 1 RoomId from Pam_RoomOldMan where OldManId='{1}' and GETDATE() between BeginDate and EndDate order by CheckInTime desc)"
                             , NormalSession.UserId, oldManAdmissionParams.OldManId);
                    nativeSqlStatments.Add(strSql);

                    strSql = string.Format("update Pam_RoomOldMan set OperatedBy='{0}',OperatedOn=GETDATE(),EndDate=GETDATE() where OldManId='{1}' and GETDATE() between BeginDate and EndDate",
                        NormalSession.UserId, oldManAdmissionParams.OldManId);
                    nativeSqlStatments.Add(strSql);

                    //历史-工作计划
                    strSql = string.Format("update Pam_WorkPlan set OperatedBy='{0}',OperatedOn=GETDATE(),RoomId='{1}' where Status=1 and OldManId='{2}' and PlanDate>GETDATE() and CompleteStatus=0",
                        NormalSession.UserId,oldManAdmissionParams.RoomId, oldManAdmissionParams.OldManId);
                    nativeSqlStatments.Add(strSql);

                    //当前
                    strSql = string.Format("update Pam_Room set OperatedBy='{0}',OperatedOn=GETDATE(),OccupancyNumber=OccupancyNumber+1 where Status=1 and RoomId='{1}'",
                        NormalSession.UserId, oldManAdmissionParams.RoomId);
                    nativeSqlStatments.Add(strSql);

                    strSql = string.Format("insert into Pam_RoomOldMan(OperatedBy,OperatedOn,OldManId,RoomId,BeginDate,EndDate,SickBedNo) values('{0}',GETDATE(),'{1}','{2}',GETDATE(),'{3}','{4}') ",
                        NormalSession.UserId, oldManAdmissionParams.OldManId, oldManAdmissionParams.RoomId, "2999-12-31 23:59:59", oldManAdmissionParams.SickBedNo);
                    nativeSqlStatments.Add(strSql);
                }

                //配置服务项目
                if (oldManAdmissionParams.AssessmentResults != null)
                {
                    //查找历史
                    strSql = string.Format("select * From Pam_AssessmentResult where Status=1 and OldManId='{0}'", oldManAdmissionParams.OldManId);
                    IList<StringObjectDictionary> assessmentResults = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql);

                    //删除多余的  Except不可用
                    foreach (var item in assessmentResults) {
                        if (oldManAdmissionParams.AssessmentResults.Count(s => s.WorkItem == item["WorkItem"].ToString()) > 0) { 
                        }
                        else{
                            strSql = string.Format("Update Pam_AssessmentResult set OperatedBy='{0}',OperatedOn=GETDATE(),Status=0  where status=1 and OldManId='{1}' and WorkItem='{2}'", NormalSession.UserId, oldManAdmissionParams.OldManId, item["WorkItem"]);
                            nativeSqlStatments.Add(strSql);
                        }
                    }
                    
                    //更新历史
                    int icount = assessmentResults.Count;
                    foreach (AssessmentResult ar in oldManAdmissionParams.AssessmentResults)
                    {
                        //存在历史 更新
                        if (icount > 0 && assessmentResults.Count(s => s["WorkItem"].ToString() == ar.WorkItem) > 0)
                        {
                            strSql = string.Format("update Pam_AssessmentResult set OperatedBy='{0}',OperatedOn=getdate(),WorkSchedule='{1}',Workload='{2}',RemindFlag='{3}',RemindRepeats='{4}'," +
                            "PlayRepeats='{5}',CheckFlag='{6}',CheckDescription='{7}' where OldManId='{8}' and Status=1 and WorkItem='{9}'", NormalSession.UserId, ar.WorkSchedule, ar.Workload,
                            ar.RemindFlag, ar.RemindRepeats, ar.PlayRepeats, ar.CheckFlag, ar.CheckDescription, oldManAdmissionParams.OldManId, ar.WorkItem);
                        }
                        else
                        {
                            //新增
                            strSql = string.Format("insert into Pam_AssessmentResult(Status,OperatedBy,OperatedOn,DataSource,OldManId,WorkItem,WorkSchedule,Workload," +
                                "RemindFlag,RemindRepeats,PlayRepeats,CheckFlag,CheckDescription) values(1,'{0}',GETDATE(),'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                                NormalSession.UserId, GlobalManager.DIKey_00012_ManualEdit, oldManAdmissionParams.OldManId,
                                ar.WorkItem, ar.WorkSchedule, ar.Workload, ar.RemindFlag, ar.RemindRepeats, ar.PlayRepeats, ar.CheckFlag, ar.CheckDescription);
                            nativeSqlStatments.Add(strSql);
                        }
                    }

                    //历史-工作计划
                    strSql = string.Format("Update Pam_WorkPlan set OperatedBy='{0}',OperatedOn=GETDATE(),Status=0 where Status=1 and OldManId='{1}' and PlanDate>GETDATE() and WorkItem not in('{2}')",
                        NormalSession.UserId, oldManAdmissionParams.OldManId, string.Join("','", oldManAdmissionParams.AssessmentResults.Select(s => s.WorkItem).ToArray()));
                    nativeSqlStatments.Add(strSql);
                    
                }
                //执行入院
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(nativeSqlStatments);

                //重新生成工作执行项
                SPParam spParam = new { OldManId = oldManAdmissionParams.OldManId, BatchFlag = 0 }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);
            }
            catch (Exception ex) {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        #endregion

        #region 老人出院-旧版
        [WebInvoke(UriTemplate = "OldMansDischarge", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult OldMansDischarge(OldManDischarge oldManDischarge)
        {
            InvokeResult result = new InvokeResult{ Success = true };
            try
            {
                IList<string> nativeSqlStatments = new List<string>();
                if (oldManDischarge.OldManIds != null && oldManDischarge.OldManIds.Count > 0)
                {
                    //删除老人机构关系
                    //批量更新空值或NULL值无效
                    string strSql = "update Pam_PensionAgencyObjectBaseInfo set OperatedBy='{0}',OperatedOn='{1}'," +
                    " DataSource='{2}',Status=3,ResidentStatus=3,DischargeTime='{3}',DischargeNote='{4}',StationId=null  where OldManId in('{5}')";

                    DateTime DischargeTime;
                    if (!DateTime.TryParse(oldManDischarge.DischargeTime, out DischargeTime))
                    {
                        DischargeTime = DateTime.Now;
                    }
                    else {
                        DischargeTime = DischargeTime.AddHours(DateTime.Now.Hour);
                        DischargeTime = DischargeTime.AddMinutes(DateTime.Now.Minute);
                    }
                    string nativeSqlStatment = string.Format(strSql, NormalSession.UserId,
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            GlobalManager.DIKey_00012_ManualEdit,
                            DischargeTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            oldManDischarge.DischargeNote,
                            string.Join("','", oldManDischarge.OldManIds.ToArray()
                            )
                        );
                    nativeSqlStatments.Add(nativeSqlStatment);

                    //释放呼叫号码
                    strSql = "Update Pam_AgencyObjectConfigInfo set OperatedBy='{0}',OperatedOn=GETDATE(),CallNo='' where " +
                            " OldManId in ('{1}')";
                    nativeSqlStatment = string.Format(strSql, NormalSession.UserId, string.Join("','", oldManDischarge.OldManIds.ToArray()));
                    nativeSqlStatments.Add(nativeSqlStatment);
                    
                    //修改房间入住数
                    strSql = "update Pam_Room set  OperatedBy='{0}',OperatedOn='{1}',DataSource='{2}'," +
                            "OccupancyNumber= (case when OccupancyNumber > 0 then OccupancyNumber-1 else 0 end) " +
                            " From Pam_Room a inner join Pam_RoomOldMan b on a.RoomId=b.RoomId where a.Status=1 " +
                            " and a.StationId='{3}' and GETDATE() between b.BeginDate and b.EndDate and b.OldManId in('{4}')";
                    nativeSqlStatment = string.Format(strSql, NormalSession.UserId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            GlobalManager.DIKey_00012_ManualEdit, oldManDischarge.StationId,
                            string.Join("','", oldManDischarge.OldManIds.ToArray())
                        );
                    nativeSqlStatments.Add(nativeSqlStatment);

                    //删除老人房间关系
                    strSql = "update Pam_RoomOldMan set OperatedBy='{0}',OperatedOn='{1}',EndDate='{2}'" +
                        " where OldManId in('{3}') and getdate() between BeginDate and EndDate";
                    nativeSqlStatment = string.Format(strSql, NormalSession.UserId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), string.Join("','", oldManDischarge.OldManIds.ToArray())
                        );
                    nativeSqlStatments.Add(nativeSqlStatment);

                    //作废服务项目
                    strSql = "Update Pam_AssessmentResult set OperatedBy='{0}',OperatedOn=GETDATE(),Status=0  where status=1" +
                            " and OldManId in ('{1}')";
                    nativeSqlStatment = string.Format(strSql, NormalSession.UserId, string.Join("','", oldManDischarge.OldManIds.ToArray()));
                    nativeSqlStatments.Add(nativeSqlStatment);

                    //作废工作计划
                    strSql = "update Pam_WorkPlan set  OperatedBy='{0}',OperatedOn='{1}',Status=0 where OldManId in('{2}') and PlanDate>GETDATE() and CompleteStatus=0";
                    nativeSqlStatment = string.Format(strSql, NormalSession.UserId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),string.Join("','", oldManDischarge.OldManIds.ToArray()));
                    nativeSqlStatments.Add(nativeSqlStatment);

                    //删除大于当前时间老人的预定执行工作项目
                    strSql = "delete Pam_WorkExecute where OldManId in('{0}') and DATEDIFF(D,RemindTime,GETDATE()) = 0 " +
                        " and ServeManArriveTime is Null and RemindTime>GETDATE()";
                    nativeSqlStatment = string.Format(strSql, string.Join("','", oldManDischarge.OldManIds.ToArray()));
                    nativeSqlStatments.Add(nativeSqlStatment);

                    //执行操作
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(nativeSqlStatments);
                }
                else {
                    result.Success = false;
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

        #region 老人入院-新版
        [WebInvoke(UriTemplate = "OrgOldManAdmission", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult OrgOldManAdmission(OldManAdmissionParams oldManAdmissionParams)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                IList<string> nativeSqlStatments = new List<string>();
                string strSql = "";

                if (oldManAdmissionParams.OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                //配置呼叫号码--有呼叫号码
                if (!string.IsNullOrEmpty(oldManAdmissionParams.CallNo))
                {
                    //这边有问题 待解决
                    strSql = string.Format("select * From Oca_OldManConfigInfo where OldManId='{0}' or CallNo='{1}' or CallNo2='{1}' or CallNo3='{1}' ", oldManAdmissionParams.OldManId, oldManAdmissionParams.CallNo);
                    IList<StringObjectDictionary> oldManConfigInfos = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql);
                    if (oldManConfigInfos.Count == 0)
                    {
                        strSql = string.Format("insert into Oca_OldManConfigInfo(OldManId,OperatedBy,OperatedOn,DataSource,CallNo3) " +
                            " values('{0}','{1}',GetDate(),'{2}','{3}') ", oldManAdmissionParams.OldManId, NormalSession.UserId
                            , GlobalManager.DIKey_00012_ManualEdit, oldManAdmissionParams.CallNo);
                        nativeSqlStatments.Add(strSql);
                    }
                    else if (oldManConfigInfos.Count == 1 && oldManConfigInfos.Count(s => s["OldManId"].ToString() == oldManAdmissionParams.OldManId.ToString()) > 0)
                    {
                        strSql = string.Format("update Oca_OldManConfigInfo set OperatedBy='{0}',OperatedOn=GetDate(),CallNo3='{1}' " +
                            " where OldManId='{2}' ", NormalSession.UserId, oldManAdmissionParams.CallNo, oldManAdmissionParams.OldManId);
                        nativeSqlStatments.Add(strSql);
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorCode = 51001;
                        result.ErrorMessage = "当前号码已经使用，请重新确认老人号码";
                        return result;
                    }
                }
                else
                {
                    //清空号码
                    strSql = string.Format("select * From Oca_OldManConfigInfo where OldManId='{0}' ", oldManAdmissionParams.OldManId);
                    IList<StringObjectDictionary> oldManConfigInfos = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql);
                    if (oldManConfigInfos.Count > 0)
                    {
                        strSql = string.Format("update Oca_OldManConfigInfo set OperatedBy='{0}',OperatedOn=GetDate(),CallNo3='' where OldManId='{1}' ", NormalSession.UserId, oldManAdmissionParams.OldManId);
                        nativeSqlStatments.Add(strSql);
                    }
                }

                //配置房间和床位号
                if (oldManAdmissionParams.RoomId != null)
                {
                    //历史
                    strSql = string.Format("update Pam_Room set OperatedBy='{0}',OperatedOn=GETDATE(),OccupancyNumber=(case when OccupancyNumber>0 then OccupancyNumber-1 else 0 end) " +
                             " where Status=1 and RoomId=(select top 1 RoomId from Pam_RoomOldMan where OldManId='{1}' and GETDATE() between BeginDate and EndDate order by CheckInTime desc)"
                             , NormalSession.UserId, oldManAdmissionParams.OldManId);
                    nativeSqlStatments.Add(strSql);

                    strSql = string.Format("update Pam_RoomOldMan set OperatedBy='{0}',OperatedOn=GETDATE(),EndDate=GETDATE() where OldManId='{1}' and GETDATE() between BeginDate and EndDate",
                        NormalSession.UserId, oldManAdmissionParams.OldManId);
                    nativeSqlStatments.Add(strSql);

                    //历史-工作计划
                    strSql = string.Format("update Pam_WorkPlan set OperatedBy='{0}',OperatedOn=GETDATE(),RoomId='{1}' where Status=1 and OldManId='{2}' and PlanDate>GETDATE() and CompleteStatus=0",
                        NormalSession.UserId, oldManAdmissionParams.RoomId, oldManAdmissionParams.OldManId);
                    nativeSqlStatments.Add(strSql);

                    //当前
                    strSql = string.Format("update Pam_Room set OperatedBy='{0}',OperatedOn=GETDATE(),OccupancyNumber=OccupancyNumber+1 where Status=1 and RoomId='{1}'",
                        NormalSession.UserId, oldManAdmissionParams.RoomId);
                    nativeSqlStatments.Add(strSql);

                    strSql = string.Format("insert into Pam_RoomOldMan(OperatedBy,OperatedOn,OldManId,RoomId,BeginDate,EndDate,SickBedNo) values('{0}',GETDATE(),'{1}','{2}',GETDATE(),'{3}','{4}') ",
                        NormalSession.UserId, oldManAdmissionParams.OldManId, oldManAdmissionParams.RoomId, "2999-12-31 23:59:59", oldManAdmissionParams.SickBedNo);
                    nativeSqlStatments.Add(strSql);
                }

                //配置服务项目
                if (oldManAdmissionParams.AssessmentResults != null)
                {
                    //查找历史
                    strSql = string.Format("select * From Pam_AssessmentResult where Status=1 and OldManId='{0}'", oldManAdmissionParams.OldManId);
                    IList<StringObjectDictionary> assessmentResults = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql);

                    //删除多余的  Except不可用
                    foreach (var item in assessmentResults)
                    {
                        if (oldManAdmissionParams.AssessmentResults.Count(s => s.WorkItem == item["WorkItem"].ToString()) > 0)
                        {
                        }
                        else
                        {
                            strSql = string.Format("Update Pam_AssessmentResult set OperatedBy='{0}',OperatedOn=GETDATE(),Status=0  where status=1 and OldManId='{1}' and WorkItem='{2}'", NormalSession.UserId, oldManAdmissionParams.OldManId, item["WorkItem"]);
                            nativeSqlStatments.Add(strSql);
                        }
                    }

                    //更新历史
                    int icount = assessmentResults.Count;
                    foreach (AssessmentResult ar in oldManAdmissionParams.AssessmentResults)
                    {
                        //存在历史 更新
                        if (icount > 0 && assessmentResults.Count(s => s["WorkItem"].ToString() == ar.WorkItem) > 0)
                        {
                            strSql = string.Format("update Pam_AssessmentResult set OperatedBy='{0}',OperatedOn=getdate(),WorkSchedule='{1}',Workload='{2}',RemindFlag='{3}',RemindRepeats='{4}'," +
                            "PlayRepeats='{5}',CheckFlag='{6}',CheckDescription='{7}' where OldManId='{8}' and Status=1 and WorkItem='{9}'", NormalSession.UserId, ar.WorkSchedule, ar.Workload,
                            ar.RemindFlag, ar.RemindRepeats, ar.PlayRepeats, ar.CheckFlag, ar.CheckDescription, oldManAdmissionParams.OldManId, ar.WorkItem);
                        }
                        else
                        {
                            //新增
                            strSql = string.Format("insert into Pam_AssessmentResult(Status,OperatedBy,OperatedOn,DataSource,OldManId,WorkItem,WorkSchedule,Workload," +
                                "RemindFlag,RemindRepeats,PlayRepeats,CheckFlag,CheckDescription) values(1,'{0}',GETDATE(),'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                                NormalSession.UserId, GlobalManager.DIKey_00012_ManualEdit, oldManAdmissionParams.OldManId,
                                ar.WorkItem, ar.WorkSchedule, ar.Workload, ar.RemindFlag, ar.RemindRepeats, ar.PlayRepeats, ar.CheckFlag, ar.CheckDescription);
                            nativeSqlStatments.Add(strSql);
                        }
                    }

                    //历史-工作计划
                    strSql = string.Format("Update Pam_WorkPlan set OperatedBy='{0}',OperatedOn=GETDATE(),Status=0 where Status=1 and OldManId='{1}' and PlanDate>GETDATE() and WorkItem not in('{2}')",
                        NormalSession.UserId, oldManAdmissionParams.OldManId, string.Join("','", oldManAdmissionParams.AssessmentResults.Select(s => s.WorkItem).ToArray()));
                    nativeSqlStatments.Add(strSql);

                }
                //执行入院
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(nativeSqlStatments);

                //重新生成工作执行项
                SPParam spParam = new { OldManId = oldManAdmissionParams.OldManId, BatchFlag = 0 }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        #endregion

        #region 老人出院-新版
        [WebInvoke(UriTemplate = "OrgOldMansDischarge", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult OrgOldMansDischarge(OldManDischarge oldManDischarge)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                IList<string> nativeSqlStatments = new List<string>();
                if (oldManDischarge.OldManIds != null && oldManDischarge.OldManIds.Count > 0)
                {
                    //删除老人机构关系
                    //批量更新空值或NULL值无效
                    string strSql = "update Oca_OldManBaseInfo set OperatedBy='{0}',OperatedOn='{1}'," +
                    " DataSource='{2}',Status=3,ResidentStatus=3,DischargeTime='{3}',DischargeNote='{4}',StationId=null  where OldManId in('{5}')";

                    DateTime DischargeTime;
                    if (!DateTime.TryParse(oldManDischarge.DischargeTime, out DischargeTime))
                    {
                        DischargeTime = DateTime.Now;
                    }
                    else
                    {
                        DischargeTime = DischargeTime.AddHours(DateTime.Now.Hour);
                        DischargeTime = DischargeTime.AddMinutes(DateTime.Now.Minute);
                    }
                    string nativeSqlStatment = string.Format(strSql, NormalSession.UserId,
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            GlobalManager.DIKey_00012_ManualEdit,
                            DischargeTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            oldManDischarge.DischargeNote,
                            string.Join("','", oldManDischarge.OldManIds.ToArray()
                            )
                        );
                    nativeSqlStatments.Add(nativeSqlStatment);

                    //修改房间入住数
                    strSql = "update Pam_Room set  OperatedBy='{0}',OperatedOn='{1}',DataSource='{2}'," +
                            "OccupancyNumber= (case when OccupancyNumber > 0 then OccupancyNumber-1 else 0 end) " +
                            " From Pam_Room a inner join Pam_RoomOldMan b on a.RoomId=b.RoomId where a.Status=1 " +
                            " and a.StationId='{3}' and GETDATE() between b.BeginDate and b.EndDate and b.OldManId in('{4}')";
                    nativeSqlStatment = string.Format(strSql, NormalSession.UserId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            GlobalManager.DIKey_00012_ManualEdit, oldManDischarge.StationId,
                            string.Join("','", oldManDischarge.OldManIds.ToArray())
                        );
                    nativeSqlStatments.Add(nativeSqlStatment);

                    //删除老人房间关系
                    strSql = "update Pam_RoomOldMan set OperatedBy='{0}',OperatedOn='{1}',EndDate='{2}'" +
                        " where OldManId in('{3}') and getdate() between BeginDate and EndDate";
                    nativeSqlStatment = string.Format(strSql, NormalSession.UserId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), string.Join("','", oldManDischarge.OldManIds.ToArray())
                        );
                    nativeSqlStatments.Add(nativeSqlStatment);

                    //作废服务项目
                    strSql = "Update Pam_AssessmentResult set OperatedBy='{0}',OperatedOn=GETDATE(),Status=0  where status=1" +
                            " and OldManId in ('{1}')";
                    nativeSqlStatment = string.Format(strSql, NormalSession.UserId, string.Join("','", oldManDischarge.OldManIds.ToArray()));
                    nativeSqlStatments.Add(nativeSqlStatment);

                    //作废工作计划
                    strSql = "update Pam_WorkPlan set  OperatedBy='{0}',OperatedOn='{1}',Status=0 where OldManId in('{2}') and PlanDate>GETDATE() and CompleteStatus=0";
                    nativeSqlStatment = string.Format(strSql, NormalSession.UserId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), string.Join("','", oldManDischarge.OldManIds.ToArray()));
                    nativeSqlStatments.Add(nativeSqlStatment);

                    //删除大于当前时间老人的预定执行工作项目
                    strSql = "delete Pam_WorkExecute where OldManId in('{0}') and DATEDIFF(D,RemindTime,GETDATE()) = 0 " +
                        " and ServeManArriveTime is Null and RemindTime>GETDATE()";
                    nativeSqlStatment = string.Format(strSql, string.Join("','", oldManDischarge.OldManIds.ToArray()));
                    nativeSqlStatments.Add(nativeSqlStatment);

                    //执行操作
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(nativeSqlStatments);
                }
                else
                {
                    result.Success = false;
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
        #endregion
    }

    public class OldManAdmissionParams
    {
        public Guid? OldManId { get; set; }
        public string CallNo { get; set; }
        public Guid? RoomId { get; set; }
        public int? SickBedNo { get; set; }
        public IList<AssessmentResult> AssessmentResults { get; set; }
    }

    public class OldManDischarge {
        public IList<string> OldManIds { get; set; }
        public string DischargeTime { get; set; }
        public string DischargeNote { get; set; }
        public string StationId { get; set; }
    }

}

