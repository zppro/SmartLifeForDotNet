using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

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
using System.Xml;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SmartLife.CertManage.ManageServices.Pub
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class WageService : AppBaseWcfService
    {

        #region 创建人员 Create
        [WebInvoke(UriTemplate = "CreateEmployee", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SubSetColumnPK> CreateEmployee(SubSetColumn subSetColumn)
        {
            ModelInvokeResult<SubSetColumnPK> result = new ModelInvokeResult<SubSetColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (subSetColumn.ColumnNameNew == "自动生成")
                {
                    subSetColumn.ColumnNameNew = GlobalManager.getPK(subSetColumn.GetMappingTableName(), "ColumnNameNew");
                }
                /***********************begin 自定义代码*******************/
                subSetColumn.OperatedBy = NormalSession.UserId.ToGuid();
                subSetColumn.OperatedOn = DateTime.Now;

                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = subSetColumn.GetCreateMethodName(), ParameterObject = subSetColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SubSetColumnPK { SubSetId = subSetColumn.SubSetId, ColumnNameNew = subSetColumn.ColumnNameNew };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 修改人员 Update
        [WebInvoke(UriTemplate = "UpdateEmployee/{strSubSetId},{strColumnNameNew}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SubSetColumnPK> UpdateEmployee(string strSubSetId, string strColumnNameNew, SubSetColumn subSetColumn)
        {
            ModelInvokeResult<SubSetColumnPK> result = new ModelInvokeResult<SubSetColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _SubSetId = strSubSetId;
                subSetColumn.SubSetId = _SubSetId;

                string _ColumnNameNew = subSetColumn.ColumnNameNew;//双主键中的一个可更新时
                StringObjectDictionary dictionary = subSetColumn.ToStringObjectDictionary(false);
                dictionary.Add("Old_ColumnNameNew", strColumnNameNew);

                subSetColumn.OperatedBy = NormalSession.UserId.ToGuid();
                subSetColumn.OperatedOn = DateTime.Now;
                statements.Add(new IBatisNetBatchStatement { StatementName = "SubSetColumn_Update2", ParameterObject = dictionary, Type = SqlExecuteType.UPDATE });

                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SubSetColumnPK { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew };

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 载入人员 Load
        [WebGet(UriTemplate = "LoadEmployee/{strSubSetId},{strColumnNameNew}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<SubSetColumn> LoadEmployee(string strSubSetId, string strColumnNameNew)
        {
            ModelInvokeResult<SubSetColumn> result = new ModelInvokeResult<SubSetColumn> { Success = true };
            try
            {
                string _SubSetId = strSubSetId;
                string _ColumnNameNew = strColumnNameNew;
                var subSetColumn = BuilderFactory.DefaultBulder().Load<SubSetColumn, SubSetColumnPK>(new SubSetColumnPK { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew });
                result.instance = subSetColumn;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 查询列表人员 Query
        [WebGet(UriTemplate = "QueryEmployee?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<SubSetColumn> QueryEmployee(string strParms)
        {
            CollectionInvokeResult<SubSetColumn> result = new CollectionInvokeResult<SubSetColumn> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<SubSetColumn>(dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 创建人员学历 Create
        [WebInvoke(UriTemplate = "CreateEmpEducation", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SubSetColumnPK> CreateEmpEducation(SubSetColumn subSetColumn)
        {
            ModelInvokeResult<SubSetColumnPK> result = new ModelInvokeResult<SubSetColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (subSetColumn.ColumnNameNew == "自动生成")
                {
                    subSetColumn.ColumnNameNew = GlobalManager.getPK(subSetColumn.GetMappingTableName(), "ColumnNameNew");
                }
                /***********************begin 自定义代码*******************/
                subSetColumn.OperatedBy = NormalSession.UserId.ToGuid();
                subSetColumn.OperatedOn = DateTime.Now;

                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = subSetColumn.GetCreateMethodName(), ParameterObject = subSetColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SubSetColumnPK { SubSetId = subSetColumn.SubSetId, ColumnNameNew = subSetColumn.ColumnNameNew };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 修改人员学历 Update
        [WebInvoke(UriTemplate = "UpdateEmpEducation/{strSubSetId},{strColumnNameNew}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SubSetColumnPK> UpdateEmpEducation(string strSubSetId, string strColumnNameNew, SubSetColumn subSetColumn)
        {
            ModelInvokeResult<SubSetColumnPK> result = new ModelInvokeResult<SubSetColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _SubSetId = strSubSetId;
                subSetColumn.SubSetId = _SubSetId;

                string _ColumnNameNew = subSetColumn.ColumnNameNew;//双主键中的一个可更新时
                StringObjectDictionary dictionary = subSetColumn.ToStringObjectDictionary(false);
                dictionary.Add("Old_ColumnNameNew", strColumnNameNew);

                subSetColumn.OperatedBy = NormalSession.UserId.ToGuid();
                subSetColumn.OperatedOn = DateTime.Now;
                statements.Add(new IBatisNetBatchStatement { StatementName = "SubSetColumn_Update2", ParameterObject = dictionary, Type = SqlExecuteType.UPDATE });

                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SubSetColumnPK { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew };

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 载入人员学历 Load
        [WebGet(UriTemplate = "LoadEmpEducation/{strSubSetId},{strColumnNameNew}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<SubSetColumn> LoadEmpEducation(string strSubSetId, string strColumnNameNew)
        {
            ModelInvokeResult<SubSetColumn> result = new ModelInvokeResult<SubSetColumn> { Success = true };
            try
            {
                string _SubSetId = strSubSetId;
                string _ColumnNameNew = strColumnNameNew;
                var subSetColumn = BuilderFactory.DefaultBulder().Load<SubSetColumn, SubSetColumnPK>(new SubSetColumnPK { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew });
                result.instance = subSetColumn;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 查询列表人员学历 Query
        [WebGet(UriTemplate = "QueryEmpEducation?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<SubSetColumn> QueryEmpEducation(string strParms)
        {
            CollectionInvokeResult<SubSetColumn> result = new CollectionInvokeResult<SubSetColumn> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<SubSetColumn>(dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 创建工作履历 Create
        [WebInvoke(UriTemplate = "CreateWorkHis", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SubSetColumnPK> CreateWorkHis(SubSetColumn subSetColumn)
        {
            ModelInvokeResult<SubSetColumnPK> result = new ModelInvokeResult<SubSetColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (subSetColumn.ColumnNameNew == "自动生成")
                {
                    subSetColumn.ColumnNameNew = GlobalManager.getPK(subSetColumn.GetMappingTableName(), "ColumnNameNew");
                }
                /***********************begin 自定义代码*******************/
                subSetColumn.OperatedBy = NormalSession.UserId.ToGuid();
                subSetColumn.OperatedOn = DateTime.Now;

                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = subSetColumn.GetCreateMethodName(), ParameterObject = subSetColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SubSetColumnPK { SubSetId = subSetColumn.SubSetId, ColumnNameNew = subSetColumn.ColumnNameNew };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion     

        #region 修改人员工作履历 Update
        [WebInvoke(UriTemplate = "UpdateEmpWorkHis/{strSubSetId},{strColumnNameNew}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SubSetColumnPK> UpdateEmpWorkHis(string strSubSetId, string strColumnNameNew, SubSetColumn subSetColumn)
        {
            ModelInvokeResult<SubSetColumnPK> result = new ModelInvokeResult<SubSetColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _SubSetId = strSubSetId;
                subSetColumn.SubSetId = _SubSetId;

                string _ColumnNameNew = subSetColumn.ColumnNameNew;//双主键中的一个可更新时
                StringObjectDictionary dictionary = subSetColumn.ToStringObjectDictionary(false);
                dictionary.Add("Old_ColumnNameNew", strColumnNameNew);

                subSetColumn.OperatedBy = NormalSession.UserId.ToGuid();
                subSetColumn.OperatedOn = DateTime.Now;
                statements.Add(new IBatisNetBatchStatement { StatementName = "SubSetColumn_Update2", ParameterObject = dictionary, Type = SqlExecuteType.UPDATE });

                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SubSetColumnPK { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew };

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion      

        #region 载入人员工作履历 Load
        [WebGet(UriTemplate = "LoadWorkHis/{strSubSetId},{strColumnNameNew}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<SubSetColumn> LoadWorkHis(string strSubSetId, string strColumnNameNew)
        {
            ModelInvokeResult<SubSetColumn> result = new ModelInvokeResult<SubSetColumn> { Success = true };
            try
            {
                string _SubSetId = strSubSetId;
                string _ColumnNameNew = strColumnNameNew;
                var subSetColumn = BuilderFactory.DefaultBulder().Load<SubSetColumn, SubSetColumnPK>(new SubSetColumnPK { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew });
                result.instance = subSetColumn;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion   

        #region 查询列表人员工作履历 Query
        [WebGet(UriTemplate = "QueryEmpWorkHis?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<SubSetColumn> QueryEmpWorkHis(string strParms)
        {
            CollectionInvokeResult<SubSetColumn> result = new CollectionInvokeResult<SubSetColumn> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<SubSetColumn>(dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion
    }
}

