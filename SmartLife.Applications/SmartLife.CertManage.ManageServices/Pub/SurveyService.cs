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
using SmartLife.Share.Models.Bas;

namespace SmartLife.CertManage.ManageServices.Pub
{  
    [ServiceValidateSession]
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]

    public class SurveyService : AppBaseWcfService
    {
        #region --Survey--
        //GET用来获取资源，POST用来新建资源（也可以用于更新资源），PUT用来更新资源，DELETE用来删除资源。
        [WebGet(UriTemplate = "{str_id}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Survey> List_Single(string str_id)
        {
            ModelInvokeResult<Survey> result = new ModelInvokeResult<Survey> { Success = true };
            try
            {
                result.instance = BuilderFactory.DefaultBulder().Load<Survey, SurveyPK>(new SurveyPK { SurveyId = Guid.Parse(str_id) });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "QuerySurvey?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Survey> List(string strParms)
        {
            CollectionInvokeResult<Survey> result = new CollectionInvokeResult<Survey> { Success = true };
            try
            {
                var dictionary_param = new StringObjectDictionary().MixInJson(strParms);
                if (dictionary_param.Count < 1) return result;//dictionary_param.Add("WhereClause", "1<>1");如条件不存在直接返回无需加此条件
                result.rows = BuilderFactory.DefaultBulder().List<Survey>(dictionary_param);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<Survey> ListForEasyUIgridPage(EasyUIgridCollectionParam<Survey> param)
        {
            EasyUIgridCollectionInvokeResult<Survey> result = new EasyUIgridCollectionInvokeResult<Survey> { Success = true };
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
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4Model<Survey>(filters, param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        [WebInvoke(UriTemplate = "QuerySurveyForGrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<Survey> ListForGrid(EasyUIgridCollectionParam<Survey> param)
        {
            EasyUIgridCollectionInvokeResult<Survey> result = new EasyUIgridCollectionInvokeResult<Survey> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }

                //List<string> whereClause = new List<string>();
                //if (param.filterFields != null)
                //{
                //    foreach (var field in param.filterFields)
                //    {
                //        filters[field.key] = field.value;
                //    }
                //}
                gridCollectionPager.EasyUIgridDoPage4Model<Survey>(filters, param,ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        #region 创建 Create
        [WebInvoke(UriTemplate = "", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SurveyPK> CreateSurvey(Survey surveyObj)
        {
            ModelInvokeResult<SurveyPK> result = new ModelInvokeResult<SurveyPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (surveyObj.SurveyId == GlobalManager.GuidAsAutoGenerate)
                {
                    surveyObj.SurveyId = Guid.NewGuid();
                }
                surveyObj.OperatedBy = NormalSession.UserId.ToGuid();
                surveyObj.OperatedOn = DateTime.Now;

                statements.Add(new IBatisNetBatchStatement { StatementName = surveyObj.GetCreateMethodName(), ParameterObject = surveyObj.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                //BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SurveyPK { SurveyId = surveyObj.SurveyId };
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
        [WebInvoke(UriTemplate = "{strSurveyId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SurveyPK> UpdateSurvey(string strSurveyId, Survey surveyObj)
        {
            ModelInvokeResult<SurveyPK> result = new ModelInvokeResult<SurveyPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _SurveyId = strSurveyId.ToGuid();
                if (_SurveyId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                surveyObj.SurveyId = _SurveyId;
                surveyObj.OperatedBy = NormalSession.UserId.ToGuid();
                surveyObj.OperatedOn = DateTime.Now;
                statements.Add(new IBatisNetBatchStatement { StatementName = surveyObj.GetUpdateMethodName(), ParameterObject = surveyObj.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SurveyPK { SurveyId = surveyObj.SurveyId };

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
        [WebInvoke(UriTemplate = "{strSurveyId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SurveyPK> DeleteSurvey(string strSurveyId)
        {
            ModelInvokeResult<SurveyPK> result = new ModelInvokeResult<SurveyPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _SurveyId = strSurveyId.ToGuid();
                if (_SurveyId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                SurveyPK pk = new SurveyPK { SurveyId = _SurveyId };
                //DeleteCascade(statements, pk);//删除级联数据
                statements.Add(new IBatisNetBatchStatement { StatementName = new Survey().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
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

        #region 作废 Nullify
        [WebInvoke(UriTemplate = "Nullify/{strSurveyId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SurveyPK> NullifySurvey(string strSurveyId)
        {
            ModelInvokeResult<SurveyPK> result = new ModelInvokeResult<SurveyPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _SurveyId = strSurveyId.ToGuid();
                if (_SurveyId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                Survey surveyObj = new Survey { SurveyId = _SurveyId, Status = 0 };
                /***********************begin 自定义代码*******************/
                surveyObj.OperatedBy = NormalSession.UserId.ToGuid();
                surveyObj.OperatedOn = DateTime.Now;

                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = surveyObj.GetUpdateMethodName(), ParameterObject = surveyObj.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SurveyPK { SurveyId = _SurveyId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strSurveyIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strSurveyIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrSurveyIds = strSurveyIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrSurveyIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Survey().GetUpdateMethodName();
                foreach (string strSurveyId in arrSurveyIds)
                {
                    Survey surveyObj = new Survey { SurveyId = strSurveyId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    surveyObj.OperatedBy = NormalSession.UserId.ToGuid();
                    surveyObj.OperatedOn = DateTime.Now;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = surveyObj.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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

        #endregion

        #region --SurveyItem--
        //GET用来获取资源，POST用来新建资源（也可以用于更新资源），PUT用来更新资源，DELETE用来删除资源。
        [WebGet(UriTemplate = "QuerySurveyItem?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<SurveyItem> List_Item(string strParms)
        {
            CollectionInvokeResult<SurveyItem> result = new CollectionInvokeResult<SurveyItem> { Success = true };
            try
            {
                var dictionary_param = new StringObjectDictionary().MixInJson(strParms);
                if (dictionary_param.Count < 1) return result;
                result.rows = BuilderFactory.DefaultBulder().List<SurveyItem>(dictionary_param);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebInvoke(UriTemplate = "QuerySurveyItemForGrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<SurveyItem> List_ItemForGrid(EasyUIgridCollectionParam<SurveyItem> param)
        {
            EasyUIgridCollectionInvokeResult<SurveyItem> result = new EasyUIgridCollectionInvokeResult<SurveyItem> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }

                //List<string> whereClause = new List<string>();
                //if (param.filterFields != null)
                //{
                //    foreach (var field in param.filterFields)
                //    {
                //        filters[field.key] = field.value;
                //    }
                //}
                gridCollectionPager.EasyUIgridDoPage4Model<SurveyItem>(filters, param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region --SurveyBlock--
        [WebGet(UriTemplate = "QuerySurveyBlock?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<SurveyBlock> List_Block(string strParms)
        {
            CollectionInvokeResult<SurveyBlock> result = new CollectionInvokeResult<SurveyBlock> { Success = true };
            try
            {
                var dictionary_param = new StringObjectDictionary().MixInJson(strParms);
                if (dictionary_param.Count < 1) return result;
                result.rows = BuilderFactory.DefaultBulder().List<SurveyBlock>(dictionary_param);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "QuerySurveyBlockMix?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> List_BlockMix(string strParms)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                var dictionary_param = new StringObjectDictionary().MixInJson(strParms);
                if (dictionary_param.Count < 1) return result;
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("SurveyBlock_ListMix", dictionary_param);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region --SurveyResult--


        #endregion

        #region --私有方法--
        #region 批量创建项目
        [WebInvoke(UriTemplate = "BatchCreateServeItem/{surveyId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult BatchCreateServeItem(string surveyId, IList<SurveyItem> surveyItems)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                //statements.Add(new IBatisNetBatchStatement { StatementName = new SurveyItem().GetDeleteMethodName(), ParameterObject = new { SurveyId = surveyId.ToGuid() }.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                foreach (var item in surveyItems)
                {
                    item.OperatedBy = NormalSession.UserId.ToGuid();
                    item.OperatedOn = DateTime.Now;
                    item.SurveyId = surveyId.ToGuid();
                    
                    if (item.SurveyItemId.ToString() == "")
                    {
                        item.SurveyItemId = Guid.NewGuid();
                        statements.Add(new IBatisNetBatchStatement { StatementName = item.GetCreateMethodName(), ParameterObject = item.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }
                    else {
                        item.Id = null;
                        statements.Add(new IBatisNetBatchStatement { StatementName = item.GetUpdateMethodName(), ParameterObject = item.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                    }
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

        #endregion
    }
}