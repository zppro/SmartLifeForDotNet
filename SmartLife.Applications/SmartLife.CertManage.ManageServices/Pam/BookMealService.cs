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
    public class BookMealService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<BookMeal> List()
        {
            CollectionInvokeResult<BookMeal> result = new CollectionInvokeResult<BookMeal> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<BookMeal>();
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
        public CollectionInvokeResult<BookMeal> Query(string strParms)
        {
            CollectionInvokeResult<BookMeal> result = new CollectionInvokeResult<BookMeal> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<BookMeal>(dictionary);
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
        public JqgridCollectionInvokeResult<BookMeal> ListForJqgrid(JqgridCollectionParam<BookMeal> param)
        {
            JqgridCollectionInvokeResult<BookMeal> result = new JqgridCollectionInvokeResult<BookMeal> { Success = true };
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

                gridCollectionPager.JqgridDoPage<BookMeal>(BuilderFactory.DefaultBulder().List<BookMeal>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<BookMeal> ListForEasyUIgrid(EasyUIgridCollectionParam<BookMeal> param)
        {
            EasyUIgridCollectionInvokeResult<BookMeal> result = new EasyUIgridCollectionInvokeResult<BookMeal> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<BookMeal>(BuilderFactory.DefaultBulder().List<BookMeal>(filters), param, ref result);
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
        public ModelInvokeResult<BookMealPK> Create(BookMeal bookMeal)
        {
            ModelInvokeResult<BookMealPK> result = new ModelInvokeResult<BookMealPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>(); 
                /***********************begin 自定义代码*******************/
                bookMeal.OperatedBy = NormalSession.UserId.ToGuid();
                bookMeal.OperatedOn = DateTime.Now;
                bookMeal.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = bookMeal.GetCreateMethodName(), ParameterObject = bookMeal.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new BookMealPK { Id = bookMeal.Id };
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
        [WebInvoke(UriTemplate = "{strId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<BookMealPK> Update(string strId, BookMeal bookMeal)
        {
            ModelInvokeResult<BookMealPK> result = new ModelInvokeResult<BookMealPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (strId == "")
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                bookMeal.Id = Int32.Parse(strId);
                /***********************begin 自定义代码*******************/
                bookMeal.OperatedBy = NormalSession.UserId.ToGuid();
                bookMeal.OperatedOn = DateTime.Now;
                bookMeal.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = bookMeal.GetUpdateMethodName(), ParameterObject = bookMeal.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new BookMealPK { Id = Int32.Parse(strId) };

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
        [WebInvoke(UriTemplate = "{strId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<BookMealPK> Delete(string strId)
        {
            ModelInvokeResult<BookMealPK> result = new ModelInvokeResult<BookMealPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                BookMealPK pk = new BookMealPK { Id = Int32.Parse(strId) };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new BookMeal().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new BookMealPK { Id = Int32.Parse(strId) };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrIds = strIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new BookMeal().GetDeleteMethodName();
                foreach (string strId in arrIds)
                {
                    BookMealPK pk = new BookMealPK { Id = Int32.Parse(strId) };
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
        [WebInvoke(UriTemplate = "Nullify/{strId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<BookMealPK> Nullify(string strId)
        {
            ModelInvokeResult<BookMealPK> result = new ModelInvokeResult<BookMealPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                BookMeal bookMeal = new BookMeal { Id = Int32.Parse(strId), Status = 0 };
                /***********************begin 自定义代码*******************/
                bookMeal.OperatedBy = NormalSession.UserId.ToGuid();
                bookMeal.OperatedOn = DateTime.Now;
                bookMeal.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = bookMeal.GetUpdateMethodName(), ParameterObject = bookMeal.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new BookMealPK { Id = Int32.Parse(strId) };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrIds = strIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new BookMeal().GetUpdateMethodName();
                foreach (string strId in arrIds)
                {
                    BookMeal bookMeal = new BookMeal { Id = Int32.Parse(strId), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    bookMeal.OperatedBy = NormalSession.UserId.ToGuid();
                    bookMeal.OperatedOn = DateTime.Now;
                    bookMeal.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = bookMeal.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, BookMealPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<BookMeal> Load(string strId)
        {
            ModelInvokeResult<BookMeal> result = new ModelInvokeResult<BookMeal> { Success = true };
            try
            {
                var bookMeal = BuilderFactory.DefaultBulder().Load<BookMeal, BookMealPK>(new BookMealPK { Id = Int32.Parse(strId) });
                result.instance = bookMeal;
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
        #region 获取配餐信息
        [WebInvoke(UriTemplate = "ListBookMeal", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListBookMeal(EasyUIgridCollectionParam<ListBookMealParam> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { StationId = param.instance.StationId }.ToStringObjectDictionary();
                if (param.instance.RoomId.ToString() != "" && param.instance.RoomId != null)
                {
                    filters.Add("RoomId", param.instance.RoomId.ToString());
                }
                if (param.instance.FloorNo != "" && param.instance.FloorNo != null)
                {
                    filters.Add("FloorNo", param.instance.FloorNo.ToString());
                }
                DateTime beginDate = DateTime.Parse(param.instance.BeginDateStr);
                DateTime endDate = DateTime.Parse(param.instance.EndDateStr);
                List<string> thePivotColumnsForSelect = new List<string>();
                List<string> thePivotColumns = new List<string>();
                TimeSpan ts = endDate - beginDate;
                DateTime dt = beginDate;
                for (int i = 0; i < ts.Days; i++)
                {
                    thePivotColumnsForSelect.Add(string.Format("[{0}] as _{0}", dt.ToString("yyyyMMdd")));
                    thePivotColumns.Add(dt.ToString("yyyyMMdd"));
                    dt = dt.AddDays(1);
                }
                thePivotColumnsForSelect.Add(string.Format("[{0}] as _{0}", dt.ToString("yyyyMMdd")));
                thePivotColumns.Add(dt.ToString("yyyyMMdd"));
                filters.Add("PivotColumnsForSelect", string.Join(",", thePivotColumnsForSelect.ToArray()));
                filters.Add("PivotColumns", "[" + string.Join("],[", thePivotColumns.ToArray()) + "]");
                if (param.instance.OldManName != "" && param.instance.OldManName != null)
                {
                    filters.Add("OldManName", "OldManName like '" + param.instance.OldManName + "%'");
                }
                filters["isRoomFlag"] = 0;
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("BookMealList", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 创建 CreateAll 新增配餐信息
        [WebInvoke(UriTemplate = "CreateAll", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CreateAll(IList<BookMeal> bookMeals)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>(); 
                List<string> arroldManIds = new List<string>();
                foreach (BookMeal bookMeal in bookMeals)
                { 
                    /***********************begin 自定义代码*******************/
                    bookMeal.OperatedBy = NormalSession.UserId.ToGuid();
                    bookMeal.OperatedOn = DateTime.Now;
                    bookMeal.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = "BookMeal_Create_All", ParameterObject = bookMeal.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    /***********************begin 自定义代码*******************/
                    /***********************此处添加自定义代码*****************/
                    /***********************end 自定义代码*********************/
                }
                if (statements.Count > 0)
                {
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements); 
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

        #region 复制配餐信息
        [WebInvoke(UriTemplate = "CopyBookMealByWeek", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CopyBookMealByWeek(CopyBookMeal copyBookMeal)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (copyBookMeal.OldManIds == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string[] arrOldManIds = copyBookMeal.OldManIds.Split('|');
                copyBookMeal.OldManIds = "'" + string.Join("','", arrOldManIds) + "'";

                int days = (DateTime.Parse(copyBookMeal.CopyTo_Start) - DateTime.Parse(copyBookMeal.CopyFrom_Start)).Days;
                copyBookMeal.Days = days;

                copyBookMeal.OperatedBy = NormalSession.UserId.ToGuid();
                StringObjectDictionary filters = copyBookMeal.ToStringObjectDictionary(false);
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = "CopyBookMeal", ParameterObject = copyBookMeal.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
         
        #endregion
    }
    public class ListBookMealParam
    {
        public Guid? StationId { get; set; }
        public Guid? RoomId { get; set; }
        public string FloorNo { get; set; }
        public string BeginDateStr { get; set; }
        public string EndDateStr { get; set; }
        public string OldManName { get; set; }
    }
    public class CopyBookMeal
    {
        public string OldManIds { get; set; }
        public string CopyFrom_Start { get; set; }
        public string CopyFrom_End { get; set; }
        public string CopyTo_Start { get; set; }
        public string CopyTo_End { get; set; }
        public int Days { get; set; }
        public Guid? OperatedBy { get; set; }
    }

}

