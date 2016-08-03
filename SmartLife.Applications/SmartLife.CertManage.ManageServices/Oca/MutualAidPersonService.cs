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

namespace SmartLife.CertManage.ManageServices.Oca
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MutualAidPersonService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<MutualAidPerson> List()
        {
            CollectionInvokeResult<MutualAidPerson> result = new CollectionInvokeResult<MutualAidPerson> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<MutualAidPerson>();
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
        public CollectionInvokeResult<MutualAidPerson> Query(string strParms)
        {
            CollectionInvokeResult<MutualAidPerson> result = new CollectionInvokeResult<MutualAidPerson> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<MutualAidPerson>(dictionary);
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
        public JqgridCollectionInvokeResult<MutualAidPerson> ListForJqgrid(JqgridCollectionParam<MutualAidPerson> param)
        {
            JqgridCollectionInvokeResult<MutualAidPerson> result = new JqgridCollectionInvokeResult<MutualAidPerson> { Success = true };
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

                gridCollectionPager.JqgridDoPage<MutualAidPerson>(BuilderFactory.DefaultBulder().List<MutualAidPerson>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<MutualAidPerson> ListForEasyUIgrid(EasyUIgridCollectionParam<MutualAidPerson> param)
        {
            EasyUIgridCollectionInvokeResult<MutualAidPerson> result = new EasyUIgridCollectionInvokeResult<MutualAidPerson> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<MutualAidPerson>(BuilderFactory.DefaultBulder().List<MutualAidPerson>(filters), param, ref result);
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
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<MutualAidPerson> ListForEasyUIgridPage(EasyUIgridCollectionParam<MutualAidPerson> param)
        {
            EasyUIgridCollectionInvokeResult<MutualAidPerson> result = new EasyUIgridCollectionInvokeResult<MutualAidPerson> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage4Model<MutualAidPerson>(filters, param, ref result);
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
        public ModelInvokeResult<MutualAidPersonPK> Create(MutualAidPerson mutualAidPerson)
        {
            ModelInvokeResult<MutualAidPersonPK> result = new ModelInvokeResult<MutualAidPersonPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (mutualAidPerson.MutualAidPersonId == GlobalManager.GuidAsAutoGenerate)
                {
                    mutualAidPerson.MutualAidPersonId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                mutualAidPerson.OperatedBy = NormalSession.UserId.ToGuid();
                mutualAidPerson.OperatedOn = DateTime.Now;
                mutualAidPerson.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = mutualAidPerson.GetCreateMethodName(), ParameterObject = mutualAidPerson.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MutualAidPersonPK { MutualAidPersonId = mutualAidPerson.MutualAidPersonId };
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
        [WebInvoke(UriTemplate = "{strMutualAidPersonId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<MutualAidPersonPK> Update(string strMutualAidPersonId, MutualAidPerson mutualAidPerson)
        {
            ModelInvokeResult<MutualAidPersonPK> result = new ModelInvokeResult<MutualAidPersonPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _MutualAidPersonId = strMutualAidPersonId.ToGuid();
                if (_MutualAidPersonId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                mutualAidPerson.MutualAidPersonId = _MutualAidPersonId;
                /***********************begin 自定义代码*******************/
                mutualAidPerson.OperatedBy = NormalSession.UserId.ToGuid();
                mutualAidPerson.OperatedOn = DateTime.Now;
                mutualAidPerson.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = mutualAidPerson.GetUpdateMethodName(), ParameterObject = mutualAidPerson.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MutualAidPersonPK { MutualAidPersonId = _MutualAidPersonId };

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
        [WebInvoke(UriTemplate = "{strMutualAidPersonId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<MutualAidPersonPK> Delete(string strMutualAidPersonId)
        {
            ModelInvokeResult<MutualAidPersonPK> result = new ModelInvokeResult<MutualAidPersonPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _MutualAidPersonId = strMutualAidPersonId.ToGuid();
                if (_MutualAidPersonId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                MutualAidPersonPK pk = new MutualAidPersonPK { MutualAidPersonId = _MutualAidPersonId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new MutualAidPerson().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MutualAidPersonPK { MutualAidPersonId = _MutualAidPersonId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strMutualAidPersonIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strMutualAidPersonIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrMutualAidPersonIds = strMutualAidPersonIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrMutualAidPersonIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new MutualAidPerson().GetDeleteMethodName();
                foreach (string strMutualAidPersonId in arrMutualAidPersonIds)
                {
                    MutualAidPersonPK pk = new MutualAidPersonPK { MutualAidPersonId = strMutualAidPersonId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strMutualAidPersonId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<MutualAidPersonPK> Nullify(string strMutualAidPersonId)
        {
            ModelInvokeResult<MutualAidPersonPK> result = new ModelInvokeResult<MutualAidPersonPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _MutualAidPersonId = strMutualAidPersonId.ToGuid();
                if (_MutualAidPersonId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                MutualAidPerson mutualAidPerson = new MutualAidPerson { MutualAidPersonId = _MutualAidPersonId, Status = 0 };
                /***********************begin 自定义代码*******************/
                mutualAidPerson.OperatedBy = NormalSession.UserId.ToGuid();
                mutualAidPerson.OperatedOn = DateTime.Now;
                mutualAidPerson.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/

                statements.Add(new IBatisNetBatchStatement { StatementName = mutualAidPerson.GetUpdateMethodName(), ParameterObject = mutualAidPerson.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new MutualAidPersonPK { MutualAidPersonId = _MutualAidPersonId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strMutualAidPersonIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strMutualAidPersonIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrMutualAidPersonIds = strMutualAidPersonIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrMutualAidPersonIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new MutualAidPerson().GetUpdateMethodName();
                foreach (string strMutualAidPersonId in arrMutualAidPersonIds)
                {
                    MutualAidPerson mutualAidPerson = new MutualAidPerson { MutualAidPersonId = strMutualAidPersonId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    mutualAidPerson.OperatedBy = NormalSession.UserId.ToGuid();
                    mutualAidPerson.OperatedOn = DateTime.Now;
                    mutualAidPerson.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = mutualAidPerson.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, MutualAidPersonPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strMutualAidPersonId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<MutualAidPerson> Load(string strMutualAidPersonId)
        {
            ModelInvokeResult<MutualAidPerson> result = new ModelInvokeResult<MutualAidPerson> { Success = true };
            try
            {
                Guid? _MutualAidPersonId = strMutualAidPersonId.ToGuid();
                if (_MutualAidPersonId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var mutualAidPerson = BuilderFactory.DefaultBulder().Load<MutualAidPerson, MutualAidPersonPK>(new MutualAidPersonPK { MutualAidPersonId = _MutualAidPersonId });
                result.instance = mutualAidPerson;
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