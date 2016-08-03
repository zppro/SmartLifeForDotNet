using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using SmartLife.Share.Models.Pub;

using Newtonsoft.Json;

namespace SmartLife.Share.Behaviors
{
    //公共 流程处理
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class __CirculationService : AppBaseWcfService
    {
        #region 流转业务

        #region 获取最新一条记录 Query
        [WebGet(UriTemplate = "QueryLatest?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Flow> QueryLatest(string strParms)
        {
            ModelInvokeResult<Flow> result = new ModelInvokeResult<Flow> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.instance = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<Flow>(dictionary).FirstOrDefault();
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
        public CollectionInvokeResult<Flow> Query(string strParms)
        {
            CollectionInvokeResult<Flow> result = new CollectionInvokeResult<Flow> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<Flow>(dictionary);
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
        [WebGet(UriTemplate = "QueryFlow?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> QueryFlow(string strParms)
        {

            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);

                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ListStringObjectDictionary("GetFlow_List2", dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 加载
        [WebInvoke(UriTemplate = "{flowId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<Flow> Load(string flowId)
        {
            ModelInvokeResult<Flow> result = new ModelInvokeResult<Flow> { Success = true };
            try
            {
                Guid? _FlowId = flowId.ToGuid();
                if (_FlowId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var flw = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<Flow, FlowPK>(new FlowPK { FlowId = _FlowId });
                result.instance = flw;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 数据提交 不通过
        [WebInvoke(UriTemplate = "PrevFlow/{bIZ_IDArr},{processComment},{tableName},{primaryKey},{flowName}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult PrevFlow(string bIZ_IDArr, string processComment, string tableName, string primaryKey, string flowName)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<string> nativeSqlStatements = new List<string>();
                string strConnectId = GetHttpHeader("ConnectId");

                string[] businessIdArr = bIZ_IDArr.Split('|');
                for (int i = 0; i < businessIdArr.Length; i++)
                {
                    if (string.IsNullOrEmpty(businessIdArr[i])) continue;
                    string sql = "insert into PUB_Flow (FlowId,OperatedBy,OperatedOn,BIZ_ID,TableName,FlowFrom,FlowTo, " +
                              " ProcessResult,ProcessComment2,ProcessTitle )" +
                              " select '{0}','{1}','{2}','{3}',TableName,CurrentState,FlowTo,ProcessAction,'{4}',ProcessorTitle From   " +
                              " PUB_FlowDefine where Status =1 and FlowName='{6}' and CurrentState=(select top 1 FlowTo+2 From PUB_Flow where  " +
                              " TableName='{5}' and BIZ_ID='{3}' order by OperatedOn desc) and TableName='{5}' and ProcessAction= '2'";

                    string statements = string.Format(sql, Guid.NewGuid().ToString(),
                                        NormalSession.UserId,
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                        businessIdArr[i],
                                        processComment,
                                        tableName,
                                        flowName);
                    nativeSqlStatements.Add(statements);
                }
                BuilderFactory.DefaultBulder(strConnectId).ExecuteNativeSqlNoneQuery(nativeSqlStatements);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 数据提交 通过
        [WebInvoke(UriTemplate = "NextFlow/{bIZ_IDArr},{processComment},{tableName},{primaryKey},{flowName}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult NextFlow(string bIZ_IDArr, string processComment, string tableName, string primaryKey, string flowName)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<string> nativeSqlStatements = new List<string>();
                string strConnectId = GetHttpHeader("ConnectId");
                string[] businessIdArr = bIZ_IDArr.Split('|');
                bool isAync = false;//标志是否需要同步
                for (int i = 0; i < businessIdArr.Length; i++)
                {
                    if (string.IsNullOrEmpty(businessIdArr[i])) continue;

                    string sql = "insert into PUB_Flow (FlowId,OperatedBy,OperatedOn,BIZ_ID,TableName,FlowFrom,FlowTo, " +
                                " ProcessResult,ProcessComment,ProcessTitle )" +
                                " select '{0}','{1}','{2}','{3}',TableName,CurrentState,FlowTo,ProcessAction,'{4}',ProcessorTitle From  " +
                                " PUB_FlowDefine  where  Status =1 and FlowName='{8}' and CurrentState={6} and TableName='{5}' and ProcessAction= {7}";

                    //查最新流程记录
                    object obj_flowDefine = BuilderFactory.DefaultBulder(strConnectId).ExecuteScalar(string.Format("select top 1 FlowTo From " +
                                  " PUB_Flow where  TableName='{0}' and BIZ_ID='{1}' order by OperatedOn desc", tableName, businessIdArr[i]));
                    int? _currentState, _processAction = 1;
                    if (obj_flowDefine == null || obj_flowDefine == DBNull.Value)
                    {
                        _currentState = 10;
                        _processAction = 0;
                    }
                    else
                    {
                        _currentState = Convert.ToInt32(obj_flowDefine) + 1;
                    }
                    string statements = string.Format(sql, Guid.NewGuid().ToString(),
                                        NormalSession.UserId,
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                        businessIdArr[i],
                                        processComment,
                                        tableName,
                                        _currentState,
                                        _processAction,
                                        flowName);
                    nativeSqlStatements.Add(statements);

                    /************************判断此步操作是否是最后一步，并改变Status的值和同步数据*************************/
                    int flowStep = int.Parse(flowName.Substring(flowName.Length - 2));
                    //取出这个流转名称下的最高FlowTo 
                    var ret = BuilderFactory.DefaultBulder(strConnectId).ExecuteScalar(string.Format(" select FlowTo From  " +
                            " PUB_FlowDefine  where  Status =1 and FlowName='{0}' and CurrentState={1} and TableName='{2}' and ProcessAction= {3}",
                            flowName, _currentState, tableName, _processAction)).ToString();
                    int flowTo = int.Parse(ret) - 10;//取出这此操作后的FlowTo
                    if (flowTo == flowStep)//判断此步操作是否是最后一步
                    {
                        isAync = FlowStepIsFinish(strConnectId, businessIdArr[i], primaryKey, tableName);
                    }
                    /************************判断此步操作是否是最后一步，并改变Status的值和同步数据*************************/
                }
                BuilderFactory.DefaultBulder(strConnectId).ExecuteNativeSqlNoneQuery(nativeSqlStatements);
                if (isAync)
                {
                    /*************************同步有效数据*************************/
                    SPParam spParam = new { }.ToSPParam();
                    spParam["Phase"] = 2;
                    BuilderFactory.DefaultBulder(strConnectId).ExecuteSPNoneQuery("SP_DBA_Aync_Oca_OldManBaseInfo", spParam);
                    /*************************同步有效数据*************************/
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

        #region 改变Status的值和同步数据
        private bool FlowStepIsFinish(string strConnectId, string bIZ_ID, string primaryKey, string tableName)
        {
            bool result = false;
            try
            {
                //改变Status的值
                string sql_update = string.Format("update {0} set Status=1 ,OperatedBy='{1}',OperatedOn='{2}' where {3}='{4}'",
                                   tableName,
                                   NormalSession.UserId,
                                   DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                   primaryKey,
                                   bIZ_ID);
                BuilderFactory.DefaultBulder(strConnectId).ExecuteScalar(sql_update);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region 数据审批后再修改(currentState为流程最高节点)
        [WebInvoke(UriTemplate = "ReFlow/{bIZ_IDArr},{primaryKey},{tableName},{flowName}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult ReFlow(string bIZ_IDArr, string primaryKey, string tableName, string flowName)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<string> nativeSqlStatements = new List<string>();
                string strConnectId = GetHttpHeader("ConnectId");
                string[] businessIdArr = bIZ_IDArr.Split('|');
                for (int i = 0; i < businessIdArr.Length; i++)
                {
                    if (string.IsNullOrEmpty(businessIdArr[i])) continue;
                    string sql = "insert into PUB_Flow (FlowId,OperatedBy,OperatedOn,BIZ_ID,TableName,FlowFrom,FlowTo, " +
                                  " ProcessResult,ProcessTitle )" +
                                  " select '{0}','{1}','{2}','{3}',TableName,CurrentState,FlowTo,ProcessAction,ProcessorTitle From " +
                                  " PUB_FlowDefine where  Status =1 and FlowName='{6}' and CurrentState=10 and TableName='{5}' and ProcessAction= 0 ";
                    //+ " and (select top 1 b.Status from Pub_Flow a left join {5} b on a.BIZ_ID=b.{4} where BIZ_ID='{3}' and TableName='{5}' order by a.OperatedOn desc)=1";

                    string statements = string.Format(sql, Guid.NewGuid().ToString(),
                                        NormalSession.UserId,
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                        businessIdArr[i],
                                        primaryKey,
                                        tableName,
                                        flowName);
                    nativeSqlStatements.Add(statements);
                }
                BuilderFactory.DefaultBulder(strConnectId).ExecuteNativeSqlNoneQuery(nativeSqlStatements);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 撤销流程
        [WebInvoke(UriTemplate = "CancleFlow/{bIZ_IDArr},{tableName},{flowName}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult CancleFlow(string bIZ_IDArr, string tableName, string flowName)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<string> nativeSqlStatements = new List<string>();
                string strConnectId = GetHttpHeader("ConnectId");
                string[] businessIdArr = bIZ_IDArr.Split('|');
                for (int i = 0; i < businessIdArr.Length; i++)
                {
                    if (string.IsNullOrEmpty(businessIdArr[i])) continue;

                    string sql = "insert into PUB_Flow (FlowId,OperatedBy,OperatedOn,BIZ_ID,TableName,FlowFrom,FlowTo, " +
                                 " ProcessResult,ProcessTitle )" +
                                 " select '{0}','{1}','{2}','{3}',TableName,CurrentState,FlowTo,ProcessAction,ProcessorTitle From " +
                                 " PUB_FlowDefine where  Status =1 and FlowName='{5}' and CurrentState=(select top 1  FlowTo From PUB_Flow where TableName='{4}' and BIZ_ID='{3}' order by OperatedOn desc)" +
                                  "  and TableName='{4}' and ProcessAction= 0";

                    string statements = string.Format(sql, Guid.NewGuid().ToString(),
                                         NormalSession.UserId,
                                         DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                         businessIdArr[i],
                                         tableName,
                                         flowName);
                    nativeSqlStatements.Add(statements);
                }
                BuilderFactory.DefaultBulder(strConnectId).ExecuteNativeSqlNoneQuery(nativeSqlStatements);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 撤销流程_删除
        [WebInvoke(UriTemplate = "DeleteFlow/{bIZ_IDArr},{currentState},{flowTo},{tableName}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult DeleteFlow(string bIZ_IDArr, string currentState, string flowTo, string tableName)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<string> nativeSqlStatements = new List<string>();
                string strConnectId = GetHttpHeader("ConnectId");
                string[] businessIdArr = bIZ_IDArr.Split('|');
                for (int i = 0; i < businessIdArr.Length; i++)
                {
                    if (string.IsNullOrEmpty(businessIdArr[i])) continue;

                    string sql = "delete from Pub_Flow where FlowId=(select top 1 FlowId from Pub_Flow where BIZ_ID='{0}' " +
                                 " and  TableName='{1}' and FlowFrom='{2}' and  FlowTo='{3}' and ProcessResult='{4}' order by OperatedOn desc)" +
                                 " and  FlowId = (select top 1 FlowId from Pub_Flow where BIZ_ID='{0}' " +
                                 " and  TableName='{1}' order by OperatedOn desc)";

                    string statements = string.Format(sql, businessIdArr[i],
                                        tableName,
                                        currentState,
                                        flowTo,
                                        0);
                    nativeSqlStatements.Add(statements);
                }
                BuilderFactory.DefaultBulder(strConnectId).ExecuteNativeSqlNoneQuery(nativeSqlStatements);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region  流 下一步(包括正常流和撤销流)
        [WebInvoke(UriTemplate = "NextFlowTo", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult NextFlowTo(FlowAction flowAction)
        {
            InvokeResult result = new InvokeResult { Success = true };

            string strConnectId = GetHttpHeader("ConnectId");
            flowAction.FlowType = 0;

            result = ExecFlowAction(flowAction, strConnectId);
            return result;
        }
        #endregion

        #region  回流 非撤销 正向
        [WebInvoke(UriTemplate = "ReFlowTo", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult ReFlowTo(FlowAction flowAction)
        {
            InvokeResult result = new InvokeResult { Success = true };

            string strConnectId = GetHttpHeader("ConnectId");
            flowAction.FlowType = 1;

            result = ExecFlowAction(flowAction, strConnectId);
            return result;
        }
        #endregion

        #region 流转
        private InvokeResult ExecFlowAction(FlowAction flowAction, string strConnectId)
        {
            InvokeResult result = new InvokeResult { Success = true };

            string[] bIds = flowAction.BIZ_IDs.Split('|');
            //流转状态  0-正流，1-逆流  默认正流，正流（当前状态加上当前操作）；逆流（当前状态）
            int iAction = 0;
            if (flowAction.FlowType == 0)
            {
                //默认正向流转
                iAction += flowAction.ProcessAction;
            }
             
            try
            {
                FlowDefineMapping flowDefineMapping = BuilderFactory.DefaultBulder(strConnectId).List<FlowDefineMapping>(new { MappingType = flowAction.MappingType, MappingColumn = flowAction.MappingColumn, MappingId = flowAction.MappingId }.ToStringObjectDictionary()).FirstOrDefault();
                if (flowDefineMapping != null && !string.IsNullOrEmpty(flowDefineMapping.FlowName))
                {
                    flowAction.FlowName = flowDefineMapping.FlowName;
                }

                //找出当前流程的最高去处和默认去处
                IList<FlowDefine> flowDefineList = BuilderFactory.DefaultBulder(strConnectId).List<FlowDefine>(new { FlowName = flowAction.FlowName, TableName = flowAction.TableName, TableColumn = flowAction.TableColumn, Status = 1, OrderByClause = " FlowTo ,CurrentState " }.ToStringObjectDictionary());

                //如果是流程到了最顶级的话或者未进入流程或者切换流程则跳回当前流程默认的最低级别,以达到重新开始流转效果
                string sql = "insert into PUB_Flow (FlowId,OperatedBy,OperatedOn,BIZ_ID,TableName,FlowDefineId," +
                                " FlowFrom,FlowTo,ProcessResult,ProcessComment,ProcessTitle )";
                sql += "select NEWID(),'" + NormalSession.UserId + "',GETDATE(),t.{0},k.TableName,k.FlowDefineId,k.CurrentState," +
                " k.FlowTo,k.ProcessAction,'" + flowAction.ProcessComment + "',k.ProcessorTitle From  (select x.{0}," +
                " (case when isnull(z.FlowName,'') <>'{2}' or y.FlowTo is null or y.FlowTo={4} then {3} else y.FlowTo end)  as prevState, " +
                " (case when  isnull(z.FlowName,'') <>'{2}' then '{2}' else z.FlowName end) FlowName ," +
                " isnull(z.TableColumn,'{0}') TableColumn, isnull(z.TableName,'{1}') TableName From " +
                " (select a.{0},MAX(b.Id) as FId From {1} a left join Pub_Flow b  on a.{0}=b.BIZ_ID ";
                if (bIds.Length > 0 && bIds[0]!="")
                {
                    //去掉空值，并给每个值加上'',以逗号隔开(支持部分批量)
                    sql += " where a.{0} in( '" + string.Join("','", bIds.Where(s => !string.IsNullOrEmpty(s)).ToArray()) + "' )";
                }
                else
                {
                    //按区域进行过滤  以到达批量的效果
                    sql += " where a." + flowAction.MappingColumn + "= '" + flowAction.MappingId + "' ";
                    if (!string.IsNullOrEmpty(flowAction.WhereClause)) {
                        sql += " and " + flowAction.WhereClause;
                    }
                }
                sql += " group by a.{0} ) x left join Pub_Flow y on x.FId=y.Id left join Pub_FlowDefine z on " +
                " y.FlowDefineId=z.FlowDefineId ) t left join Pub_FlowDefine k on k.CurrentState=t.prevState+{5}  " +
                " and k.ProcessAction={5} and k.FlowName=t.FlowName and k.TableName=t.TableName and k.TableColumn=t.TableColumn" +
                " where k.CurrentState={6} and k.ProcessAction={5} and k.FlowName='{2}' and k.TableName='{1}' and k.TableColumn='{0}'";

                string statements = string.Format(sql, flowAction.TableColumn, flowAction.TableName, flowAction.FlowName,
                    flowDefineList.First().FlowTo, flowDefineList.Last().FlowTo, iAction, flowAction.ProcessState + iAction);

                BuilderFactory.DefaultBulder(strConnectId).ExecuteNativeSqlNoneQuery(statements);

                //是否流到最顶级。是的话返回true
                FlowDefine flowdefine = flowDefineList.FirstOrDefault(s => (s.CurrentState == flowAction.ProcessState + iAction) && (s.ProcessAction == flowAction.ProcessAction));
                if (flowdefine != null && flowDefineList.Last() != null && flowdefine.FlowTo == flowDefineList.Last().FlowTo)
                {
                    result.ErrorMessage = "true";
                }
            }
            catch (Exception ex) {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        #endregion


        #region  流 下一步(包括正常流和撤销流)(改造)
        [WebInvoke(UriTemplate = "NextFlowTo2", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult NextFlowTo2(ExecFlowAction execFlowAction)
        {
            InvokeResult result = new InvokeResult { Success = true };
            string strConnectId = GetHttpHeader("ConnectId"); 
            try
            {
                var spParam = execFlowAction.ToSPParam();
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                BuilderFactory.DefaultBulder(strConnectId).ExecuteSPNoneQuery("SP_Pub_ExecFlowAction", spParam);
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

    public class FlowAction
    {
        public string FlowName { get; set; }
        public string TableName { get; set; }
        public string TableColumn { get; set; }
        public string BIZ_IDs { get; set; }
        public string MappingType { get; set; }
        public string MappingColumn { get; set; }
        public string MappingId { get; set; }
        public string WhereClause { get; set; }
        public int ProcessState { get; set; }
        public int ProcessAction { get; set; }
        public int FlowType { get; set; }
        public string ProcessComment { get; set; }
    }
    public class ExecFlowAction
    {
        public string BIZ_IDs { get; set; } 
        public string TableName { get; set; }
        public string TableColumn { get; set; }
        public string StatusColumn { get; set; }
        public string StatusValuse { get; set; }
        public string ServiceType { get; set; }
        public string MappingType { get; set; } 
        public string MappingId { get; set; }
        public string WhereClause { get; set; }
        public int ProcessState { get; set; }
        public int ProcessAction { get; set; } 
        public string ProcessComment { get; set; }
        public string ProcessComment2 { get; set; }
    }
}
