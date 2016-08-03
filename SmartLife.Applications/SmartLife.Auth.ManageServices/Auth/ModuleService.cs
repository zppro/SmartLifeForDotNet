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
using SmartLife.Share.Models.Auth; 

namespace SmartLife.Auth.ManageServices.Auth
{ 
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ModuleService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Module> List()
        {
            CollectionInvokeResult<Module> result = new CollectionInvokeResult<Module> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<Module>();
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
        public CollectionInvokeResult<Module> Query(string strParms)
        {
            CollectionInvokeResult<Module> result = new CollectionInvokeResult<Module> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<Module>(dictionary);
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
        public JqgridCollectionInvokeResult<Module> ListForJqgrid(JqgridCollectionParam<Module> param)
        {
            JqgridCollectionInvokeResult<Module> result = new JqgridCollectionInvokeResult<Module> { Success = true };
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

                gridCollectionPager.JqgridDoPage<Module>(BuilderFactory.DefaultBulder().List<Module>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<Module> ListForEasyUIgrid(EasyUIgridCollectionParam<Module> param)
        {
            EasyUIgridCollectionInvokeResult<Module> result = new EasyUIgridCollectionInvokeResult<Module> { Success = true };
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
                 
                /***********************end 排序***************************/
                gridCollectionPager.EasyUIgridDoPage4Model<Module>(filters, param, ref result);
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
        public ModelInvokeResult<ModulePK> Create(Module module)
        {
            ModelInvokeResult<ModulePK> result = new ModelInvokeResult<ModulePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (module.ModuleId == GlobalManager.GuidAsAutoGenerate)
                {
                    module.ModuleId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/ 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = module.GetCreateMethodName(), ParameterObject = module.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ModulePK { ModuleId = module.ModuleId };
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
        [WebInvoke(UriTemplate = "{strModuleId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ModulePK> Update(string strModuleId, Module module)
        {
            ModelInvokeResult<ModulePK> result = new ModelInvokeResult<ModulePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ModuleId = strModuleId.ToGuid();
                if (_ModuleId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                module.ModuleId = _ModuleId;
                /***********************begin 自定义代码*******************/ 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = module.GetUpdateMethodName(), ParameterObject = module.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ModulePK { ModuleId = _ModuleId };

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
        [WebInvoke(UriTemplate = "{strModuleId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ModulePK> Delete(string strModuleId)
        {
            ModelInvokeResult<ModulePK> result = new ModelInvokeResult<ModulePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ModuleId = strModuleId.ToGuid();
                if (_ModuleId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ModulePK pk = new ModulePK { ModuleId = _ModuleId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new Module().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ModulePK { ModuleId = _ModuleId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strModuleIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strModuleIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrModuleIds = strModuleIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrModuleIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Module().GetDeleteMethodName();
                foreach (string strModuleId in arrModuleIds)
                {
                    ModulePK pk = new ModulePK { ModuleId = strModuleId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strModuleId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ModulePK> Nullify(string strModuleId)
        {
            ModelInvokeResult<ModulePK> result = new ModelInvokeResult<ModulePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ModuleId = strModuleId.ToGuid();
                if (_ModuleId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                Module module = new Module { ModuleId = _ModuleId, Status = 0 };
                /***********************begin 自定义代码*******************/ 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = module.GetUpdateMethodName(), ParameterObject = module.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ModulePK { ModuleId = _ModuleId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strModuleIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strModuleIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrModuleIds = strModuleIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrModuleIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Module().GetUpdateMethodName();
                foreach (string strModuleId in arrModuleIds)
                {
                    Module module = new Module { ModuleId = strModuleId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/ 
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = module.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ModulePK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strModuleId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Module> Load(string strModuleId)
        {
            ModelInvokeResult<Module> result = new ModelInvokeResult<Module> { Success = true };
            try
            {
                Guid? _ModuleId = strModuleId.ToGuid();
                if (_ModuleId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var module = BuilderFactory.DefaultBulder().Load<Module, ModulePK>(new ModulePK { ModuleId = _ModuleId });
                result.instance = module;
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

        #region 扩展接口

        #region 区域

        #region 按区域保存访问授权
        [WebInvoke(UriTemplate = "SaveModule4Area/{areaId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SaveModule4Area(string areaId, List<string> moduleIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                AreaModule areaModule = new AreaModule { AreaId = areaId };
                statements.Add(new IBatisNetBatchStatement { StatementName = areaModule.GetDeleteMethodName(), ParameterObject = areaModule.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                if (moduleIds != null && moduleIds.Count > 0)
                {
                    foreach (string moduleId in moduleIds)
                    {
                        areaModule.ModuleId = moduleId.ToGuid();
                        statements.Add(new IBatisNetBatchStatement { StatementName = areaModule.GetCreateMethodName(), ParameterObject = areaModule.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }
                }
               
                /***********************begin 自定义代码*******************/
                /***********************end 自定义代码*********************/
                
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/

                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);

                //SPParam theSPParam = areaModule.ToSPParam();
                //BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Auth_ImportAreaModuleSetting", theSPParam);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion
         
        #region 按区域获取设置列表
        [WebInvoke(UriTemplate = "GetAreaModuleSettingForEasyUIgrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> GetAreaModuleSettingForEasyUIgrid(EasyUIgridCollectionParam<GetAreaModuleSettingParam> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {  
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetAreaModuleSetting", param.instance.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        } 
        #endregion

        #region 按区域获取设置
        [WebGet(UriTemplate = "LoadAreaModuleSetting/{strAreaId},{strModuleId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<GetModuleSettingRet> LoadAreaModuleSetting(string strAreaId, string strModuleId)
        {
            ModelInvokeResult<GetModuleSettingRet> result = new ModelInvokeResult<GetModuleSettingRet> { Success = true };
            try
            {
                Guid? _ModuleId = strModuleId.ToGuid();
                if (_ModuleId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var module = BuilderFactory.DefaultBulder().Load<Module, ModulePK>(new ModulePK { ModuleId = _ModuleId });
                var areaModuleSetting = BuilderFactory.DefaultBulder().Load<AreaModuleSetting, AreaModuleSettingPK>(new AreaModuleSettingPK { AreaId = strAreaId, ModuleId = _ModuleId });
                if (areaModuleSetting.ModuleId == null)
                {
                    result.instance = new GetModuleSettingRet { ModuleCode = module.ModuleCode, ModuleName = module.ModuleName, ModuleId = module.ModuleId, AliasName = module.AliasName, Position = module.Position, Location = module.Location, Size = module.Size, ForeColor = module.ForeColor, BackColor = module.BackColor, AreaId = strAreaId };
                }
                else
                {

                    result.instance = new GetModuleSettingRet { ModuleCode = module.ModuleCode, ModuleName = module.ModuleName, ModuleId = areaModuleSetting.ModuleId, AliasName = areaModuleSetting.AliasName, Position = areaModuleSetting.Position, Location = areaModuleSetting.Location, Size = areaModuleSetting.Size, ForeColor = areaModuleSetting.ForeColor, BackColor = areaModuleSetting.BackColor, AreaId = areaModuleSetting.AreaId };
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

        #region 按区域保存设置
        [WebInvoke(UriTemplate = "SaveAreaModuleSetting", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SaveAreaModuleSetting(AreaModuleSetting areaModuleSetting)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                statements.Add(new IBatisNetBatchStatement { StatementName = areaModuleSetting.GetDeleteMethodName(), ParameterObject = new { ModuleId = areaModuleSetting.ModuleId, AreaId = areaModuleSetting.AreaId }, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = areaModuleSetting.GetCreateMethodName(), ParameterObject = areaModuleSetting.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
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

        #region 按区域恢复默认设置
        [WebInvoke(UriTemplate = "RestoreDefaultAreaModuleSetting", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult RestoreDefaultAreaModuleSetting(AreaModuleSetting areaModuleSetting)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                
                /***********************begin 自定义代码*******************/
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = areaModuleSetting.GetDeleteMethodName(), ParameterObject = new { ModuleId = areaModuleSetting.ModuleId, AreaId = areaModuleSetting.AreaId }, Type = SqlExecuteType.DELETE });
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

        #region 区域分组

        #region 按区域分组保存访问授权
        [WebInvoke(UriTemplate = "SaveModule4AreaGroup/{areaId},{groupId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SaveModule4AreaGroup(string areaId, string groupId, List<string> moduleIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                GroupModule groupModule = new GroupModule { AreaId = areaId, GroupId = groupId.ToGuid() };
                statements.Add(new IBatisNetBatchStatement { StatementName = groupModule.GetDeleteMethodName(), ParameterObject = groupModule.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
                if (moduleIds != null && moduleIds.Count > 0)
                {
                    foreach (string moduleId in moduleIds)
                    {
                        groupModule.ModuleId = moduleId.ToGuid();
                        statements.Add(new IBatisNetBatchStatement { StatementName = groupModule.GetCreateMethodName(), ParameterObject = groupModule.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }
                }

                /***********************begin 自定义代码*******************/
                /***********************end 自定义代码*********************/

                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/

                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);

                //SPParam theSPParam = groupModule.ToSPParam();
                //BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Auth_ImportGroupModuleSetting", theSPParam);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 按区域分组获取设置列表
        [WebInvoke(UriTemplate = "GetAreaGroupModuleSettingForEasyUIgrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> GetAreaGroupModuleSettingForEasyUIgrid(EasyUIgridCollectionParam<GetAreaGroupModuleSettingParam> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetGroupModuleSetting", param.instance.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 按区域分组获取设置
        [WebGet(UriTemplate = "LoadAreaGroupModuleSetting/{strAreaId},{strGroupId},{strModuleId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<GetModuleSettingRet> LoadAreaGroupModuleSetting(string strAreaId, string strGroupId, string strModuleId)
        {
            ModelInvokeResult<GetModuleSettingRet> result = new ModelInvokeResult<GetModuleSettingRet> { Success = true };
            try
            {
                Guid? _ModuleId = strModuleId.ToGuid();
                if (_ModuleId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var module = BuilderFactory.DefaultBulder().Load<Module, ModulePK>(new ModulePK { ModuleId = _ModuleId });
                var groupModuleSetting = BuilderFactory.DefaultBulder().Load<GroupModuleSetting, GroupModuleSettingPK>(new GroupModuleSettingPK { AreaId = strAreaId, GroupId = strGroupId.ToGuid(), ModuleId = _ModuleId });
                if (groupModuleSetting.ModuleId == null)
                {
                    result.instance = new GetModuleSettingRet { ModuleCode = module.ModuleCode, ModuleName = module.ModuleName, ModuleId = module.ModuleId, AliasName = module.AliasName, Position = module.Position, Location = module.Location, Size = module.Size, ForeColor = module.ForeColor, BackColor = module.BackColor, AreaId = strAreaId, GroupId = strGroupId.ToGuid() };
                }
                else
                {

                    result.instance = new GetModuleSettingRet { ModuleCode = module.ModuleCode, ModuleName = module.ModuleName, ModuleId = groupModuleSetting.ModuleId, AliasName = groupModuleSetting.AliasName, Position = groupModuleSetting.Position, Location = groupModuleSetting.Location, Size = groupModuleSetting.Size, ForeColor = groupModuleSetting.ForeColor, BackColor = groupModuleSetting.BackColor, AreaId = groupModuleSetting.AreaId, GroupId = groupModuleSetting.GroupId };
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

        #region 按区域分组保存设置
        [WebInvoke(UriTemplate = "SaveAreaGroupModuleSetting", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SaveAreaGroupModuleSetting(GroupModuleSetting groupModuleSetting)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                /***********************begin 自定义代码*******************/
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = groupModuleSetting.GetDeleteMethodName(), ParameterObject = new { ModuleId = groupModuleSetting.ModuleId, AreaId = groupModuleSetting.AreaId, GroupId = groupModuleSetting.GroupId }, Type = SqlExecuteType.DELETE });
                statements.Add(new IBatisNetBatchStatement { StatementName = groupModuleSetting.GetCreateMethodName(), ParameterObject = groupModuleSetting.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
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

        #region 按区域分组恢复默认设置
        [WebInvoke(UriTemplate = "RestoreDefaultAreaGroupModuleSetting", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult RestoreDefaultAreaGroupModuleSetting(GroupModuleSetting groupModuleSetting)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                /***********************begin 自定义代码*******************/
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = groupModuleSetting.GetDeleteMethodName(), ParameterObject = new { ModuleId = groupModuleSetting.ModuleId, AreaId = groupModuleSetting.AreaId, GroupId = groupModuleSetting.GroupId }, Type = SqlExecuteType.DELETE });
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

        #endregion
    }

    public class GetAreaModuleSettingParam
    {
        public string AreaId { get; set; }
    }

    public class GetAreaGroupModuleSettingParam
    {
        public string AreaId { get; set; }
        public Guid GroupId { get; set; }
    }

    public class GetModuleSettingRet
    { 
        public string ModuleCode { get; set; } 
        public string ModuleName { get; set; } 
        public Guid? ModuleId { get; set; } 
        public string AreaId { get; set; } 
        public string AliasName { get; set; } 
        public string Position { get; set; } 
        public string Location { get; set; }
        public string Size { get; set; }
        public string ForeColor { get; set; }
        public string BackColor { get; set; }
        public Guid? GroupId { get; set; }
    }
}