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
using SmartLife.Share.Models.Pam;

namespace SmartLife.CertManage.ManageServices.Pam
{
    [ServiceLogging]
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CourseScheduleService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<CourseSchedule> List()
        {
            CollectionInvokeResult<CourseSchedule> result = new CollectionInvokeResult<CourseSchedule> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<CourseSchedule>();
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
        public CollectionInvokeResult<CourseSchedule> Query(string strParms)
        {
            CollectionInvokeResult<CourseSchedule> result = new CollectionInvokeResult<CourseSchedule> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<CourseSchedule>(dictionary);
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
        public JqgridCollectionInvokeResult<CourseSchedule> ListForJqgrid(JqgridCollectionParam<CourseSchedule> param)
        {
            JqgridCollectionInvokeResult<CourseSchedule> result = new JqgridCollectionInvokeResult<CourseSchedule> { Success = true };
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

                gridCollectionPager.JqgridDoPage<CourseSchedule>(BuilderFactory.DefaultBulder().List<CourseSchedule>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<CourseSchedule> ListForEasyUIgrid(EasyUIgridCollectionParam<CourseSchedule> param)
        {
            EasyUIgridCollectionInvokeResult<CourseSchedule> result = new EasyUIgridCollectionInvokeResult<CourseSchedule> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<CourseSchedule>(BuilderFactory.DefaultBulder().List<CourseSchedule>(filters), param, ref result);
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
        public ModelInvokeResult<CourseSchedulePK> Create(CourseSchedule courseSchedule)
        {
            ModelInvokeResult<CourseSchedulePK> result = new ModelInvokeResult<CourseSchedulePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (courseSchedule.Id == -1)
                {
                    courseSchedule.Id = null;
                }
                /***********************begin 自定义代码*******************/
                courseSchedule.OperatedBy = NormalSession.UserId.ToGuid();
                courseSchedule.OperatedOn = DateTime.Now;
                courseSchedule.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = courseSchedule.GetCreateMethodName(), ParameterObject = courseSchedule.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new CourseSchedulePK { Id = courseSchedule.Id };
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
        public ModelInvokeResult<CourseSchedulePK> Update(string strId, CourseSchedule courseSchedule)
        {
            ModelInvokeResult<CourseSchedulePK> result = new ModelInvokeResult<CourseSchedulePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                /***********************begin 自定义代码*******************/
                courseSchedule.OperatedBy = NormalSession.UserId.ToGuid();
                courseSchedule.OperatedOn = DateTime.Now;
                courseSchedule.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = courseSchedule.GetUpdateMethodName(), ParameterObject = courseSchedule.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new CourseSchedulePK { Id = _Id };

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
        public ModelInvokeResult<CourseSchedulePK> Delete(string strId)
        {
            ModelInvokeResult<CourseSchedulePK> result = new ModelInvokeResult<CourseSchedulePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                CourseSchedulePK pk = new CourseSchedulePK { Id = _Id };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new CourseSchedule().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new CourseSchedulePK { Id = _Id };
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
                string statementName = new CourseSchedule().GetDeleteMethodName();
                foreach (string strId in arrIds)
                {
                    CourseSchedulePK pk = new CourseSchedulePK { Id = Convert.ToInt32(strId) };
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
        public ModelInvokeResult<CourseSchedulePK> Nullify(string strId)
        {
            ModelInvokeResult<CourseSchedulePK> result = new ModelInvokeResult<CourseSchedulePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                CourseSchedule courseSchedule = new CourseSchedule { Id = _Id, Status = 0 };
                /***********************begin 自定义代码*******************/
                courseSchedule.OperatedBy = NormalSession.UserId.ToGuid();
                courseSchedule.OperatedOn = DateTime.Now;
                courseSchedule.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = courseSchedule.GetUpdateMethodName(), ParameterObject = courseSchedule.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new CourseSchedulePK { Id = _Id };
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
                string statementName = new CourseSchedule().GetUpdateMethodName();
                foreach (string strId in arrIds)
                {
                    CourseSchedule courseSchedule = new CourseSchedule { Id = Convert.ToInt32(strId), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    courseSchedule.OperatedBy = NormalSession.UserId.ToGuid();
                    courseSchedule.OperatedOn = DateTime.Now;
                    courseSchedule.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = courseSchedule.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, CourseSchedulePK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<CourseSchedule> Load(string strId)
        {
            ModelInvokeResult<CourseSchedule> result = new ModelInvokeResult<CourseSchedule> { Success = true };
            try
            {
                var courseSchedule = BuilderFactory.DefaultBulder().Load<CourseSchedule, CourseSchedulePK>(new CourseSchedulePK { Id = Convert.ToInt32(strId) });
                result.instance = courseSchedule;
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

        #region 获取周课程信息
        [WebInvoke(UriTemplate = "ListCourseSchedule", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListCourseSchedule(EasyUIgridCollectionParam<ListCourseScheduleParam> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = param.instance.ToStringObjectDictionary(false);
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
                if (!string.IsNullOrEmpty(param.instance.DeviceName))
                {
                    filters.Add("DeviceName", "DeviceName like '" + param.instance.DeviceName + "%'");
                }
                if (!string.IsNullOrEmpty(param.instance.DeviceCode)) {
                    filters.Add("DeviceCode", "DeviceCode like '" + param.instance.DeviceCode + "%'");
                }

                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("CourseScheduleList", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 创建 CreateSchedule
        [WebInvoke(UriTemplate = "CreateSchedule", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CreateSchedule(IList<CourseSchedule> courseSchedules)
        {
            InvokeResult result = new InvokeResult{ Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (courseSchedules != null) {
                    foreach (CourseSchedule cs in courseSchedules) {
                        if (cs.Id == -1)
                        {
                            cs.Id = null;
                        }
                        else {
                            continue;
                        }

                        cs.OperatedBy = NormalSession.UserId.ToGuid();
                        cs.OperatedOn = DateTime.Now;
                        cs.DataSource = GlobalManager.DIKey_00012_ManualEdit;

                        statements.Add(new IBatisNetBatchStatement { StatementName = cs.GetCreateMethodName(), ParameterObject = cs.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }

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

        #region 复制课程信息 CopyCourceSchedule
        [WebInvoke(UriTemplate = "CopyCourceSchedule", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CopyCourceSchedule(CopyCourceParam copyCourceParam)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<string> statements = new List<string>();
                if (copyCourceParam.StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                if (copyCourceParam.DeviceIds == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                string[] deviceIds = copyCourceParam.DeviceIds.Split('|');
                DateTime copyFromStartDate = DateTime.Parse(copyCourceParam.CopyFrom_Start);
                DateTime copyFromEndDate = DateTime.Parse(copyCourceParam.CopyFrom_End);
                DateTime copyToStartDate = DateTime.Parse(copyCourceParam.CopyTo_Start);
                DateTime copyToEndDate = DateTime.Parse(copyCourceParam.CopyTo_End);

                //删除语句
                string strSql = " delete Pam_CourseSchedule where status=1 " + (copyCourceParam.CopyType == null ? "" : " and CourseFlag= " + copyCourceParam.CopyType.ToString()) + " and StationId ='" + copyCourceParam.StationId.ToString() + "' and DeviceId in('" + string.Join("','", deviceIds) + "') and BeginTime between '" + copyToStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + copyToEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                statements.Add(strSql);
                //新增
                //间隔天数
                int iSpaceDays = (copyToStartDate - copyFromStartDate).Days;
                //以任意时间为起点 进行复制 不好算 待定，先以起始段和目标段间隔数相同进行复制
                //if ((copyFromEndDate - copyFromStartDate).Days >= (copyToEndDate - copyToStartDate).Days)
                //{
                //    copyFromEndDate = copyFromStartDate.AddDays((copyToEndDate - copyToStartDate).Days + 1);

                //    strSql = "insert into Pam_CourseSchedule (CheckInTime,Status,OperatedBy,OperatedOn,DataSource,StationId,DeviceId,CourseId,CourseName,BeginTime,CourseDuration,CourseFlag,CourseInfo,Remark) " +
                //        " select GETDATE(),1,'" + NormalSession.UserId + "',GETDATE(),'" + GlobalManager.DIKey_00012_ManualEdit + "',StationId,DeviceId,CourseId,CourseName,dateadd(day," + iSpaceDays.ToString() + ",BeginTime) BeginTime,CourseDuration,CourseFlag,CourseInfo,Remark " +
                //        " From Pam_CourseSchedule where Status=1 and StationId ='" + copySourceParam.StationId.ToString() + "' and BeginTime between '" + copyFromStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + copyFromEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and DeviceId in('" + string.Join("','", deviceIds) + "')";
                //    statements.Add(strSql);
                //}
                //else
                //{
                //    //相差多少天
                //    //需复制天
                //    int iStartDays = (copyFromEndDate - copyFromStartDate).Days+1;
                //    int iEndDays = (copyToEndDate - copyToStartDate).Days + 1;
                //    //重复天
                //    int idays = (iEndDays % iStartDays > 0) ? iEndDays / iStartDays + 1 : iEndDays / iStartDays;
                //    for (int i = 0; i < idays; i++)
                //    {
                //        strSql = "insert into Pam_CourseSchedule (CheckInTime,Status,OperatedBy,OperatedOn,DataSource,StationId,DeviceId,CourseId,CourseName, BeginTime,CourseDuration,CourseFlag,CourseInfo,Remark) " +
                //        " select GETDATE(),1,'" + NormalSession.UserId + "',GETDATE(),'" + GlobalManager.DIKey_00012_ManualEdit + "',StationId,DeviceId,CourseId,CourseName,dateadd(day," + (iSpaceDays + i).ToString() + ",BeginTime) BeginTime,CourseDuration,CourseFlag,CourseInfo,Remark " +
                //        " From Pam_CourseSchedule where Status=1 and StationId ='" + copySourceParam.StationId.ToString() + "' and BeginTime between '" + copyFromStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + copyFromEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and DeviceId in('" + string.Join("','", deviceIds) + "')";
                //        statements.Add(strSql);
                //    }
                    
                //}


                strSql = "insert into Pam_CourseSchedule (CheckInTime,Status,OperatedBy,OperatedOn,DataSource,StationId,DeviceId,CourseId,CourseName,BeginTime,CourseDuration,CourseFlag,CourseInfo,Remark) " +
                       " select GETDATE(),1,'" + NormalSession.UserId + "',GETDATE(),'" + GlobalManager.DIKey_00012_ManualEdit + "',StationId,DeviceId,CourseId,CourseName,dateadd(day," + iSpaceDays.ToString() + ",BeginTime) BeginTime,CourseDuration,CourseFlag,CourseInfo,Remark " +
                       " From Pam_CourseSchedule where Status=1 " + (copyCourceParam.CopyType == null ? "" : " and CourseFlag= " + copyCourceParam.CopyType.ToString()) + " and StationId ='" + copyCourceParam.StationId.ToString() + "' and BeginTime between '" + copyFromStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + copyFromEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and DeviceId in('" + string.Join("','", deviceIds) + "')";
                statements.Add(strSql);

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

        #region 创建 CreateSchedule
        [WebInvoke(UriTemplate = "CreateLiveSchedule", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CreateLiveSchedule(LiveCourseParam liveCourseParam)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<string> statements = new List<string>();

                //循环天数？有待找到更好的解决方案(每条记录起始时间唯一[如有上条记录 开始时间>上条记录结束时间],[如有下条记录 结束时间<下条记录起始时间])
                //思路:查找当前日照下有效的且为直播模式(课程标志为2)以及当前选中时间的临近两条记录的所有记录,
                //以StationId,DeviceID合并为前条记录的起始时间，前条记录的结束时间，后条记录的开始时间，后条记录的结束时间
                //用or合并查询或者Case when 暂时用case when
                if (liveCourseParam != null && liveCourseParam.Id == null && liveCourseParam.BeginTime!=null)
                {
                    int iDayCycle = liveCourseParam.DayCycle ?? 0;  //循环天数
                    DateTime beginTime = liveCourseParam.BeginTime ?? DateTime.Now;
                    DateTime endTime = liveCourseParam.BeginTime == null ? DateTime.Now : beginTime.AddSeconds(liveCourseParam.CourseDuration ?? 0);
                    IList<string> whereClause = new List<string>();
                    if(!string.IsNullOrEmpty(liveCourseParam.AreaId)){
                        whereClause.Add(string.Format(" a.AreaId='{0}' ", liveCourseParam.AreaId));
                    }
                    if(!string.IsNullOrEmpty(liveCourseParam.AreaId2)){
                        whereClause.Add(string.Format(" a.AreaId2='{0}' ", liveCourseParam.AreaId2));
                    }
                    if(!string.IsNullOrEmpty(liveCourseParam.AreaId3)){
                        whereClause.Add(string.Format(" a.AreaId3='{0}' ", liveCourseParam.AreaId3));
                    }

                    string strSql = "";
                    for (int i = 0; i <= iDayCycle; i++)
                    {
                        strSql = "insert into Pam_CourseSchedule (CheckInTime,Status,OperatedBy,OperatedOn,DataSource,StationId,DeviceId,CourseId,CourseName,BeginTime,CourseDuration,CourseFlag,CourseInfo)" +
                            " select GETDATE(),1,'" + NormalSession.UserId + "',GETDATE(),'" + GlobalManager.DIKey_00012_ManualEdit + "',z.StationId,z.DeviceId,'" + liveCourseParam.CourseId +
                            "','" + liveCourseParam.CourseName + "','" + beginTime.AddDays(i).ToString("yyyy-MM-dd HH:mm:ss") + "'," + (liveCourseParam.CourseDuration ?? 0).ToString() + "," + liveCourseParam.CourseFlag.ToString() + ",'" + liveCourseParam.CourseInfo +"' "+
                            " From (select t.StationId,t.DeviceId,MIN(t.BeginTime) LastBeginTime,MIN(t.EndTime) LastEndTime, " +
                            " MAX(t.BeginTime) nextBeginTime,MAX(t.EndTime) nextEndTime From ( " +
                            " select b.StationId,b.DeviceId,d.BeginTime,d.EndTime from Pub_ServiceStation a  " +
                            " inner join Pam_Device b on a.StationId=b.StationId and b.Status=1 " +
                            " inner join Pub_Device c on b.DeviceId=c.DeviceId and c.Status=1 and c.DeviceType='00012' " +
                            " left join ( select y.*,DATEADD(S,x.CourseDuration,x.BeginTime) EndTime From Pam_CourseSchedule x inner join ( " +
                            " select StationId,DeviceId,MIN(BeginTime) BeginTime from Pam_CourseSchedule  where status=1  " +
                            " and CourseFlag=" + liveCourseParam.CourseFlag.ToString() + " and DATEDIFF(D,BeginTime,GETDATE())=" + i.ToString() +
                            " and BeginTime>='" + beginTime.AddDays(i).ToString("yyyy-MM-dd HH:mm:ss") + "' group by StationId,DeviceId " +
                            " union " +
                            " select StationId,DeviceId,max(BeginTime) BeginTime from Pam_CourseSchedule where status=1 " +
                            " and CourseFlag=" + liveCourseParam.CourseFlag.ToString() + " and DATEDIFF(D,BeginTime,GETDATE())=" + i.ToString() +
                            " and BeginTime<='" + beginTime.AddDays(i).ToString("yyyy-MM-dd HH:mm:ss") + "' group by StationId,DeviceId " +
                            " ) y on x.StationId=y.StationId and x.DeviceId=y.DeviceId and x.BeginTime=y.BeginTime " +
                            " ) d on b.StationId=d.StationId and b.DeviceId=d.DeviceId ";
                        if (whereClause.Count > 0)
                        {
                            strSql += " where " + string.Join(" and ", whereClause);
                        }
                        strSql += " ) t group by t.StationId,t.DeviceId " +
                            " ) z where 1=(case when z.LastBeginTime is null and z.nextBeginTime is null then 1 " +
                            " when z.LastBeginTime>'" + endTime.AddDays(i).ToString("yyyy-MM-dd HH:mm:ss") + "' then 1 " +
                            " when nextBeginTime>LastEndTime and LastEndTime<'" + beginTime.AddDays(i).ToString("yyyy-MM-dd HH:mm:ss") + "' and nextBeginTime>'" + endTime.AddDays(i).ToString("yyyy-MM-dd HH:mm:ss") + "' then 1 " +
                            " when z.nextEndTime<'" + beginTime.AddDays(i).ToString("yyyy-MM-dd HH:mm:ss") + "' then 1 else 0 end )";

                        statements.Add(strSql);
                    }

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


    public class ListCourseScheduleParam
    {
        public Guid? StationId { get; set; }
        public Guid? DeviceId { get; set; }
        public int? CourseFlag { get; set; }
        public string BeginDateStr { get; set; }
        public string EndDateStr { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceName { get; set; }
    }

    public class CopyCourceParam
    {
        public Guid? StationId { get; set; }
        public string DeviceIds { get; set; }
        public string CopyFrom_Start { get; set; }
        public string CopyFrom_End { get; set; }
        public string CopyTo_Start { get; set; }
        public string CopyTo_End { get; set; }
        public int? CopyType { get; set; }
    }

    public class LiveCourseParam : CourseSchedule
    {
        public string AreaId { get; set; }
        public string AreaId2 { get; set; }
        public string AreaId3 { get; set; }
        public int? DayCycle { get; set; }
    }
}

