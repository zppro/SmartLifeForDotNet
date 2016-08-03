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
using SmartLife.Share.Models.Bas;

namespace SmartLife.CertManage.ManageServices.Oca
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class OldManBaseInfoService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<OldManBaseInfo> List()
        {
            CollectionInvokeResult<OldManBaseInfo> result = new CollectionInvokeResult<OldManBaseInfo> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<OldManBaseInfo>();
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
        public JqgridCollectionInvokeResult<OldManBaseInfo> ListForJqgrid(JqgridCollectionParam<OldManBaseInfo> param)
        {
            JqgridCollectionInvokeResult<OldManBaseInfo> result = new JqgridCollectionInvokeResult<OldManBaseInfo> { Success = true };
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

                gridCollectionPager.JqgridDoPage<OldManBaseInfo>(BuilderFactory.DefaultBulder().List<OldManBaseInfo>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<OldManBaseInfo> ListForEasyUIgrid(EasyUIgridCollectionParam<OldManBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<OldManBaseInfo> result = new EasyUIgridCollectionInvokeResult<OldManBaseInfo> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<OldManBaseInfo>(BuilderFactory.DefaultBulder().List<OldManBaseInfo>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region EasyUIgrid数据格式的列表 ListForEasyUIgridPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<OldManBaseInfo> ListForEasyUIgridPage(EasyUIgridCollectionParam<OldManBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<OldManBaseInfo> result = new EasyUIgridCollectionInvokeResult<OldManBaseInfo> { Success = true };
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
                    DateTime parseTime = new DateTime();
                    foreach (var field in param.filterFields)
                    {
                        if (field.key.Equals("OperatedOn_Start") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format(" {0} >= '{1}' ", field.key.Substring(0, field.key.IndexOf('_')), parseTime.ToString("yyyy-MM-dd HH:mm:ss")));
                        }
                        else if (field.key.Contains("AreaId2_Start") && field.value != "")
                        {
                            filters["AreaId2"] = field.value;
                        }
                        else if (field.key.Contains("AreaId3_Start") && field.value != "")
                        {
                            filters["AreaId3"] = field.value;
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

                        if (field.key.IndexOf("AreaId") > -1 && field.value != "")
                        {
                            whereClause.Add(string.Format("(AreaId3 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", field.key, field.value));
                        }
                        else if (field.key.IndexOf("AreaId") > -1 && field.value == "")
                        {

                        }
                        else
                        {
                            fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
                    }
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP) ? " AND " : " " + param.fuzzyFieldOP + " "), fuzzys.ToArray()) + ")");
                    }
                }
                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                string sql = PermissionsCategoryView();
                if (sql == "user")
                {
                    whereClause.Add("'0'='1'");
                }
                else if (sql != "admin")
                {
                    whereClause.Add(sql);
                }
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4Model<OldManBaseInfo>(filters, param, ref result);

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
        public ModelInvokeResult<OldManBaseInfoPK> Create(OldManBaseInfo oldManBaseInfo)
        {
            ModelInvokeResult<OldManBaseInfoPK> result = new ModelInvokeResult<OldManBaseInfoPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (oldManBaseInfo.OldManId == GlobalManager.GuidAsAutoGenerate)
                {
                    oldManBaseInfo.OldManId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                oldManBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                oldManBaseInfo.OperatedOn = DateTime.Now;
                oldManBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                oldManBaseInfo.HealthInsuranceFlag = Convert.ToByte(string.IsNullOrEmpty(oldManBaseInfo.HealthInsuranceNumber) ? 0 : 1);
                oldManBaseInfo.SocialInsuranceFlag = Convert.ToByte(string.IsNullOrEmpty(oldManBaseInfo.SocialInsuranceNumber) ? 0 : 1);
                var param = oldManBaseInfo.ToStringObjectDictionary(false);
                if (string.IsNullOrEmpty(oldManBaseInfo.AreaId2))
                {
                    param["AreaId2"] = DBNull.Value;
                }
                if (string.IsNullOrEmpty(oldManBaseInfo.AreaId3))
                {
                    param["AreaId3"] = DBNull.Value;
                }
                /***********************end 自定义代码*********************/

                statements.Add(new IBatisNetBatchStatement { StatementName = oldManBaseInfo.GetCreateMethodName(), ParameterObject = param, Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManBaseInfoPK { OldManId = oldManBaseInfo.OldManId };
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
        public ModelInvokeResult<OldManBaseInfoPK> Update(string strOldManId, OldManBaseInfo oldManBaseInfo)
        {
            ModelInvokeResult<OldManBaseInfoPK> result = new ModelInvokeResult<OldManBaseInfoPK> { Success = true };
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
                oldManBaseInfo.OldManId = _OldManId;
                /***********************begin 自定义代码*******************/
                oldManBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                oldManBaseInfo.OperatedOn = DateTime.Now;
                oldManBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                var param = oldManBaseInfo.ToStringObjectDictionary(false);
                if (string.IsNullOrEmpty(oldManBaseInfo.AreaId2))
                {
                    param["AreaId2"] = DBNull.Value;
                }
                if (string.IsNullOrEmpty(oldManBaseInfo.AreaId3))
                {
                    param["AreaId3"] = DBNull.Value;
                }
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = oldManBaseInfo.GetUpdateMethodName(), ParameterObject = param, Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManBaseInfoPK { OldManId = _OldManId };

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
        public ModelInvokeResult<OldManBaseInfoPK> Delete(string strOldManId)
        {
            ModelInvokeResult<OldManBaseInfoPK> result = new ModelInvokeResult<OldManBaseInfoPK> { Success = true };
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
                OldManBaseInfoPK pk = new OldManBaseInfoPK { OldManId = _OldManId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new OldManBaseInfo().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
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
                string statementName = new OldManBaseInfo().GetDeleteMethodName();
                foreach (string strOldManId in arrOldManIds)
                {
                    OldManBaseInfoPK pk = new OldManBaseInfoPK { OldManId = strOldManId.ToGuid() };
                    DeleteCascade(statements, pk, "delete");
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
        public ModelInvokeResult<OldManBaseInfoPK> Nullify(string strOldManId)
        {
            ModelInvokeResult<OldManBaseInfoPK> result = new ModelInvokeResult<OldManBaseInfoPK> { Success = true };
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
                OldManBaseInfo oldManBaseInfo = new OldManBaseInfo { OldManId = _OldManId, Status = 0 };
                /***********************begin 自定义代码*******************/
                oldManBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                oldManBaseInfo.OperatedOn = DateTime.Now;
                oldManBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/

                statements.Add(new IBatisNetBatchStatement { StatementName = oldManBaseInfo.GetUpdateMethodName(), ParameterObject = oldManBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new OldManBaseInfoPK { OldManId = _OldManId };
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
                string statementName = new OldManBaseInfo().GetUpdateMethodName();
                foreach (string strOldManId in arrOldManIds)
                {
                    OldManBaseInfo oldManBaseInfo = new OldManBaseInfo { OldManId = strOldManId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    OldManBaseInfoPK pk = new OldManBaseInfoPK { OldManId = strOldManId.ToGuid() };
                    DeleteCascade(statements, pk, "update");
                    oldManBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
                    oldManBaseInfo.OperatedOn = DateTime.Now;
                    oldManBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = oldManBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });

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



        //[WebInvoke(UriTemplate = "NullifySelected?OldManIds={strOldManIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //public InvokeResult NullifySelected(string strOldManIds)
        //{
        //    InvokeResult result = new InvokeResult { Success = true };
        //    try
        //    {
        //        List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
        //        string[] arrOldManIds = strOldManIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        //        if (arrOldManIds.Length == 0)
        //        {
        //            result.Success = false;
        //            result.ErrorCode = 59996;
        //            return result;
        //        }
        //        //string statementName = new OldManBaseInfo().GetUpdateMethodName();
        //        foreach (string strOldManId in arrOldManIds)
        //        {
        //            //OldManBaseInfo oldManBaseInfo = new OldManBaseInfo { Id = Int32.Parse(strOldManId), Status = 0 };
        //            OldManBaseInfo oldManBaseInfo = new OldManBaseInfo();
        //            oldManBaseInfo.Id = Int32.Parse(strOldManId);
        //            /***********************begin 自定义代码*******************/
        //            oldManBaseInfo.OperatedBy = NormalSession.UserId.ToGuid();
        //            oldManBaseInfo.OperatedOn = DateTime.Now;
        //            oldManBaseInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
        //            /***********************end 自定义代码*********************/
        //            BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery("update Oca_OldManBaseInfo set Status=0,OperatedBy='" + oldManBaseInfo.OperatedBy + "',OperatedOn='" + oldManBaseInfo.OperatedOn + "',DataSource='" + oldManBaseInfo.DataSource + "' where Id='" + oldManBaseInfo.Id + "'");
        //            //statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = oldManBaseInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
        //        }
        //        //BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.ErrorMessage = ex.Message;
        //    }
        //    return result;
        //}
        #endregion

        #region 级联 作废、删除扩展接口 DeleteCascade
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, OldManBaseInfoPK pk, string type)
        {
            if ("delete".Equals(type))
            {
                string statementName = new OldManConfigInfo().GetDeleteMethodName();
                statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pk, Type = SqlExecuteType.DELETE });
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
            }
            if ("update".Equals(type))
            {
                string statementName = new OldManConfigInfo().GetUpdateMethodName();
                // ModelInvokeResult < OldManConfigInfo > oldManConfigInfo =  new OldManConfigInfoService().Load(pk.OldManId.ToString());
                OldManConfigInfo oldManConfigInfo = new OldManConfigInfo { OldManId = pk.OldManId };
                oldManConfigInfo.OperatedBy = NormalSession.UserId.ToGuid();
                oldManConfigInfo.OperatedOn = DateTime.Now;
                oldManConfigInfo.CallNo = "";
                oldManConfigInfo.CallNo2 = "";
                oldManConfigInfo.CallNo3 = "";
                statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = oldManConfigInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
            }
            BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
        }
        #endregion

        #region 级联删除扩展接口 DeleteCascade
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, OldManBaseInfoPK pk)
        {
            //此处增加级联删除代码

        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strOldManId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<OldManBaseInfo> Load(string strOldManId)
        {
            ModelInvokeResult<OldManBaseInfo> result = new ModelInvokeResult<OldManBaseInfo> { Success = true };
            try
            {
                Guid? _OldManId = strOldManId.ToGuid();
                if (_OldManId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var oldManBaseInfo = BuilderFactory.DefaultBulder().Load<OldManBaseInfo, OldManBaseInfoPK>(new OldManBaseInfoPK { OldManId = _OldManId });
                result.instance = oldManBaseInfo;
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

        #region 自定义
        [WebInvoke(UriTemplate = "MatchUnknownOldManList", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<OldManBaseInfo> MatchUnknownOldManList(EasyUIgridCollectionParam<OldManBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<OldManBaseInfo> result = new EasyUIgridCollectionInvokeResult<OldManBaseInfo> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                /**********************************************************/
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        filters[field.key] = field.value;
                    }
                }
                List<string> whereClause = new List<string>();
                /**********************************************************/
                /***********************begin 模糊查询*********************/
                //string whereclause = "select distinct a.OldManId from Oca_CallService a inner join Oca_CallRecord  b "+
                //                    " on a.CallServiceId = b.CallServiceId "+
                //                    " where b.Caller = (select Caller from  Oca_CallRecord where CallServiceId='{0}' )"+
                //                    " union select distinct a.OldManId from Oca_OldManBaseInfo  a "+
                //                    " where Mobile = (select  Caller from  Oca_CallRecord where CallServiceId='{0}' ) " +
                //                    " or Tel = (select Caller from  Oca_CallRecord where CallServiceId='{0}' ) ";
                string whereclause = "select distinct OldManId from (select  x.OldManId from Oca_CallService as x, " +
                                     " Oca_CallService as Y where x.CallServiceId!=y.CallServiceId and x.Caller=y.Caller " +
                                     " and Y.CallServiceId='{0}' union " +
                                     " select a.OldManId from Oca_OldManBaseInfo a inner join Oca_CallService b " +
                                     " on a.Tel=b.Caller or a.Mobile=b.Caller where b.CallServiceId='{0}' " +
                                     " ) c where OldManId<>'A9999999-0000-0000-0000-000000000000' ";

                if (param.fuzzyFields != null)
                {
                    foreach (var field in param.fuzzyFields)
                    {
                        if (field.key == "CallServiceId")
                        {
                            whereClause.Add(string.Format(" OldManId in(" + whereclause + ")", field.value));
                        }
                        else
                        {
                            whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
                    }
                }
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
                gridCollectionPager.EasyUIgridDoPage<OldManBaseInfo>(BuilderFactory.DefaultBulder().List<OldManBaseInfo>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        #endregion


        #region 优化街道社区过滤  EasyUIgrid数据格式的列表 ListForEasyUIgridPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<OldManBaseInfo> ListForEasyUIgridPage2(EasyUIgridCollectionParam<OldManBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<OldManBaseInfo> result = new EasyUIgridCollectionInvokeResult<OldManBaseInfo> { Success = true };
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
                        if (field.key.Contains("AreaId2_Start") && field.value != "")
                        {
                            filters["AreaId2"] = field.value;
                        }
                        else if (field.key.Contains("AreaId3_Start") && field.value != "")
                        {
                            filters["AreaId3"] = field.value;
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

                        if (field.key.IndexOf("AreaId") > -1 && field.value != "")
                        {
                            whereClause.Add(string.Format("(AreaId3 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", field.key, field.value));
                        }
                        else if (field.key.IndexOf("AreaId") > -1 && field.value == "")
                        {

                        }
                        else
                        {
                            fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
                    }
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP) ? " AND " : " " + param.fuzzyFieldOP + " "), fuzzys.ToArray()) + ")");
                    }
                }
                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                string sql = PermissionsCategoryView();
                if (sql == "user")
                {
                    whereClause.Add("'0'='1'");
                }
                else if (sql != "admin")
                {
                    whereClause.Add(sql);
                }
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4Model<OldManBaseInfo>(filters, param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 居民基本信息EasyUIgrid数据格式的列表
        [WebInvoke(UriTemplate = "ResidentBaseInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ResidentBaseInfo(EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                //BuilderFactory.DefaultBulder("SmartLife-1203").List<StringObjectDictionary>("",filters)();
                gridCollectionPager.EasyUIgridDoPage<StringObjectDictionary>(BuilderFactory.DefaultBulder("SmartLife-1203").ListStringObjectDictionary("select * from Bas_ResidentBaseInfo", filters), param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 同步评估信息
        [WebInvoke(UriTemplate = "ImportOldManBaseInfoFromEva", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult ImportOldManBaseInfoFromEva()
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                SPParam spParam = new { }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_DBA_ImportOldManBaseInfoFromEvaluated", spParam);
                if (spParam.ErrorCode != 0)
                {
                    result.ErrorCode = spParam.ErrorCode;
                    result.ErrorMessage = spParam.ErrorMessage;
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


        #region 权限查看不同内容
        public string PermissionsCategoryView()
        {
            User currentUser = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };
            string sql = "user";
            if (!currentUser.isSuperAdmin())
            {
                IList<UserArea> userAreas = BuilderFactory.DefaultBulder().List<UserArea>(new UserArea { UserId = NormalSession.UserId.ToGuid() });
                if (userAreas.Count > 0)
                {
                    var areaIdsOfStreet = string.Join("','", userAreas.Where(item => item.AreaId.ToString().Substring(14, 4) == "0000").Select(item => item.AreaId.ToString()));
                    var areaIdsOfCommunity = string.Join("','", userAreas.Where(item => item.AreaId.ToString().Substring(14, 4) != "0000").Select(item => item.AreaId.ToString()));

                    if (areaIdsOfStreet == "")
                    {
                        sql = string.Format("( AreaId3 in ('{0}') or  (ISNULL(AreaId2,'')='' AND ISNULL(AreaId3,'')=''))", areaIdsOfCommunity);
                    }
                    else if (areaIdsOfCommunity == "")
                    {
                        sql = string.Format("(AreaId2 in ('{0}') or  (ISNULL(AreaId2,'')='' AND ISNULL(AreaId3,'')=''))", areaIdsOfStreet);
                    }
                    else
                    {
                        sql = string.Format("(AreaId2 in ('{0}') or AreaId3 in ('{1}') or (ISNULL(AreaId2,'')='' AND ISNULL(AreaId3,'')=''))", areaIdsOfStreet, areaIdsOfCommunity);
                    }
                }
            }
            else
            {
                sql = "admin";
            }
            return sql;
        }
        #endregion


        #region 政府监管平台
        [WebInvoke(UriTemplate = "OldManBaseInfo_ForMonitor_V_ListByPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<OldManBaseInfo_ForMonitor_V> OldManBaseInfo_ForMonitor_V_ListByPage(EasyUIgridCollectionParam<OldManBaseInfo_ForMonitor_V> param)
        {
            EasyUIgridCollectionInvokeResult<OldManBaseInfo_ForMonitor_V> result = new EasyUIgridCollectionInvokeResult<OldManBaseInfo_ForMonitor_V> { Success = true };
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
                        DateTime parseTime = new DateTime();
                        if (field.key.Equals("OperatedOn_Start") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format("OperatedOn >= '{0}' ", field.value));
                        }
                        else if (field.key.Equals("OperatedOn_End") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format("OperatedOn <= '{0}' ", field.value));
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
                        if (field.key.Contains("Tel") || field.key.Contains("Mobile") || field.key.Contains("CallNo") || field.key.Contains("CallNo2") || field.key.Contains("CallNo") || field.key.Contains("InputCode1") || field.key.Contains("InputCode2") || field.key.Contains("OldManName"))
                        {
                            fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
                        else
                        {
                            whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
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
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4Model<OldManBaseInfo_ForMonitor_V>(filters, param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 服务平台下服务人员分配老人

        #region  加载老人
        [WebInvoke(UriTemplate = "ListForEasyUIgrid_OldManBaseInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<OldManBaseInfo> ListForEasyUIgrid_OldManBaseInfo(EasyUIgridCollectionParam<OldManBaseInfo> param)
        {
            EasyUIgridCollectionInvokeResult<OldManBaseInfo> result = new EasyUIgridCollectionInvokeResult<OldManBaseInfo> { Success = true };
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
                        if (field.key.Contains("ServeManIdCheck"))
                        {
                            whereClause.Add(string.Format("(OldManId in (select OldManId from dbo.Oca_MerchantServeManMappingOldMan where ServeManId in ({0})))", field.value));
                        }
                        else if (field.key.Contains("ServeManIdUnCheck"))
                        {
                            whereClause.Add(string.Format("(OldManId not in (select OldManId from dbo.Oca_MerchantServeManMappingOldMan where ServeManId in ({0})))", field.value));
                        }
                        else if (field.key.Contains("StationId"))
                        {
                            whereClause.Add(string.Format("(AreaId3 in (select ServeArea from Oca_MerchantArea where StationId='{0}') or AreaId2 in (select ServeArea from Oca_MerchantArea where StationId='{0}'))", field.value));
                        }
                        else if (field.key.Contains("GovTurnkeyFlag"))
                        {
                            whereClause.Add("OldManId in ( select OldManId from Oca_OldManConfigInfo where GovTurnkeyFlag=1)"); 
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
                        if (field.key.IndexOf("AreaId") > -1 && field.value != "")
                        {
                            whereClause.Add(string.Format("(AreaId3 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", field.key, field.value));
                        }
                        else if (field.key.IndexOf("AreaId") > -1 && field.value == "")
                        {

                        }
                        else
                        {
                            fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
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
                gridCollectionPager.EasyUIgridDoPage4Model<OldManBaseInfo>(filters, param, ref result);
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


        #region 老人家庭地址地位
        [WebInvoke(UriTemplate = "LocationOldMan", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> LocationOldMan(LocationOldMan locationOldMan)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                
                StringObjectDictionary filters = new StringObjectDictionary();
                if (locationOldMan.AreaId2 != "") 
                {
                    filters["AreaId2"] = locationOldMan.AreaId2;
                }
                if (locationOldMan.AreaId3 != "")
                {
                    filters["AreaId3"] = locationOldMan.AreaId3;
                }
                if (locationOldMan.WhereClause != "")
                {
                    filters["WhereClause"] = locationOldMan.WhereClause;
                } 
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("OldManBaseInfoForName",filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion 
         
        #region  养老机构

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridPage3
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage3", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridPage3(EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                        if (field.key == "isMapping")
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

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("GetListOldManByPage2", filters, param, ref result);

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
        [WebInvoke(UriTemplate = "ListForEasyUIgridPageByStationId2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridPageByStationId2(EasyUIgridCollectionParam<StringObjectDictionary> param)
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

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ListForRoomOldManByPage2", filters, param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 查看入住的所有老人
        [WebInvoke(UriTemplate = "ListOldManLivingInRoom2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListOldManLivingInRoom2(EasyUIgridCollectionParam<StringObjectDictionary> param)
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

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("GetListOldManLivingInRoom2", filters, param, ref result);

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

    public class LocationOldMan
    {
        public string AreaId2 { get; set; }
        public string AreaId3 { get; set; }
        public string WhereClause { get; set; }  
    }
}