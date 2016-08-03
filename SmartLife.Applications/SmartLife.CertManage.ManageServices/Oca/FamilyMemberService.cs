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
    public class FamilyMemberService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<FamilyMember> List()
        {
            CollectionInvokeResult<FamilyMember> result = new CollectionInvokeResult<FamilyMember> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<FamilyMember>();
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
        public CollectionInvokeResult<FamilyMember> Query(string strParms)
        {
            CollectionInvokeResult<FamilyMember> result = new CollectionInvokeResult<FamilyMember> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<FamilyMember>(dictionary);
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
        public JqgridCollectionInvokeResult<FamilyMember> ListForJqgrid(JqgridCollectionParam<FamilyMember> param)
        {
            JqgridCollectionInvokeResult<FamilyMember> result = new JqgridCollectionInvokeResult<FamilyMember> { Success = true };
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

                gridCollectionPager.JqgridDoPage<FamilyMember>(BuilderFactory.DefaultBulder().List<FamilyMember>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<FamilyMember> ListForEasyUIgrid(EasyUIgridCollectionParam<FamilyMember> param)
        {
            EasyUIgridCollectionInvokeResult<FamilyMember> result = new EasyUIgridCollectionInvokeResult<FamilyMember> { Success = true };
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
                gridCollectionPager.EasyUIgridDoPage<FamilyMember>(BuilderFactory.DefaultBulder().List<FamilyMember>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<FamilyMember> ListForEasyUIgridPage(EasyUIgridCollectionParam<FamilyMember> param)
        {
            EasyUIgridCollectionInvokeResult<FamilyMember> result = new EasyUIgridCollectionInvokeResult<FamilyMember> { Success = true };
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
                gridCollectionPager.EasyUIgridDoPage4Model<FamilyMember>(filters, param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIComboGrid数据格式的列表 ListForComboGrid
        [WebGet(UriTemplate = "ListForComboGrid", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<FamilyMember> ListForComboGrid()
        {
            CollectionInvokeResult<FamilyMember> result = new CollectionInvokeResult<FamilyMember> { Success = true };
            try
            {
                var param = GetRequesParams();
                result.rows = BuilderFactory.DefaultBulder().List<FamilyMember>(param);
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
        public ModelInvokeResult<FamilyMemberPK> Create(FamilyMember familyMember)
        {
            ModelInvokeResult<FamilyMemberPK> result = new ModelInvokeResult<FamilyMemberPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (familyMember.FamilyMemberId == GlobalManager.GuidAsAutoGenerate)
                {
                    familyMember.FamilyMemberId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                familyMember.OperatedBy = NormalSession.UserId.ToGuid();
                familyMember.OperatedOn = DateTime.Now;
                familyMember.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                familyMember.InputCode1 = GetInputCode(familyMember.FamilyMemberName, "py");
                familyMember.InputCode2 = GetInputCode(familyMember.FamilyMemberName, "wb");
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = familyMember.GetCreateMethodName(), ParameterObject = familyMember.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new FamilyMemberPK { FamilyMemberId = familyMember.FamilyMemberId };
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
        [WebInvoke(UriTemplate = "{strFamilyMemberId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<FamilyMemberPK> Update(string strFamilyMemberId, FamilyMember familyMember)
        {
            ModelInvokeResult<FamilyMemberPK> result = new ModelInvokeResult<FamilyMemberPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _FamilyMemberId = strFamilyMemberId.ToGuid();
                if (_FamilyMemberId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                familyMember.FamilyMemberId = _FamilyMemberId;
                /***********************begin 自定义代码*******************/
                familyMember.OperatedBy = NormalSession.UserId.ToGuid();
                familyMember.OperatedOn = DateTime.Now;
                familyMember.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                familyMember.InputCode1 = GetInputCode(familyMember.FamilyMemberName, "py");
                familyMember.InputCode2 = GetInputCode(familyMember.FamilyMemberName, "wb");
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = familyMember.GetUpdateMethodName(), ParameterObject = familyMember.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new FamilyMemberPK { FamilyMemberId = _FamilyMemberId };

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
        [WebInvoke(UriTemplate = "{strFamilyMemberId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<FamilyMemberPK> Delete(string strFamilyMemberId)
        {
            ModelInvokeResult<FamilyMemberPK> result = new ModelInvokeResult<FamilyMemberPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _FamilyMemberId = strFamilyMemberId.ToGuid();
                if (_FamilyMemberId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                FamilyMemberPK pk = new FamilyMemberPK { FamilyMemberId = _FamilyMemberId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new FamilyMember().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strFamilyMemberIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strFamilyMemberIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrFamilyMemberIds = strFamilyMemberIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrFamilyMemberIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new FamilyMember().GetDeleteMethodName();
                foreach (string strFamilyMemberId in arrFamilyMemberIds)
                {
                    FamilyMemberPK pk = new FamilyMemberPK { FamilyMemberId = strFamilyMemberId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strFamilyMemberId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<FamilyMemberPK> Nullify(string strFamilyMemberId)
        {
            ModelInvokeResult<FamilyMemberPK> result = new ModelInvokeResult<FamilyMemberPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _FamilyMemberId = strFamilyMemberId.ToGuid();
                if (_FamilyMemberId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                } 
                FamilyMember familyMember = new FamilyMember { FamilyMemberId = _FamilyMemberId, Status = 0 };
                /***********************begin 自定义代码*******************/
                familyMember.OperatedBy = NormalSession.UserId.ToGuid();
                familyMember.OperatedOn = DateTime.Now;
                familyMember.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/ 
                
                statements.Add(new IBatisNetBatchStatement { StatementName = familyMember.GetUpdateMethodName(), ParameterObject = familyMember.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE }); 

                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new FamilyMemberPK { FamilyMemberId = _FamilyMemberId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strFamilyMemberIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strFamilyMemberIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrFamilyMemberIds = strFamilyMemberIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrFamilyMemberIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                string statementName = new FamilyMember().GetUpdateMethodName();
                foreach (string strFamilyMemberId in arrFamilyMemberIds)
                {
                   
                    FamilyMember familyMember = new FamilyMember { FamilyMemberId = strFamilyMemberId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    familyMember.OperatedBy = NormalSession.UserId.ToGuid();
                    familyMember.OperatedOn = DateTime.Now;
                    familyMember.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = familyMember.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, FamilyMemberPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strFamilyMemberId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<FamilyMember> Load(string strFamilyMemberId)
        {
            ModelInvokeResult<FamilyMember> result = new ModelInvokeResult<FamilyMember> { Success = true };
            try
            {
                Guid? _FamilyMemberId = strFamilyMemberId.ToGuid();
                if (_FamilyMemberId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var familyMember = BuilderFactory.DefaultBulder().Load<FamilyMember, FamilyMemberPK>(new FamilyMemberPK { FamilyMemberId = _FamilyMemberId });
                result.instance = familyMember;
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


        #region 取得拼音码和五笔码
        public string GetInputCode(string nameStr, string tpye)
        {
            string result = null;
            try
            {
                string param = "";
                if (tpye == "py")
                {
                    param = "select dbo.func_tol_getpy('" + nameStr + "') as InputCode";
                }
                else
                {
                    param = "select dbo.func_tol_getwb('" + nameStr + "') as InputCode";
                }
                result = BuilderFactory.DefaultBulder().ExecuteScalar(param).ToString();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion


        #region  自定义

        #region EasyUIComboGrid数据格式的列表 EasyUIComboGrid 居民信息
        [WebInvoke(UriTemplate = "ListForEasyUIgridPageForFamilyMenber", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<FamilyMember> ListForEasyUIgridPageForFamilyMenber(EasyUIgridCollectionParam<FamilyMember> param)
        {
            EasyUIgridCollectionInvokeResult<FamilyMember> result = new EasyUIgridCollectionInvokeResult<FamilyMember> { Success = true };
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
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                gridCollectionPager.EasyUIgridDoPage4Model<FamilyMember>(filters, param, ref result);
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