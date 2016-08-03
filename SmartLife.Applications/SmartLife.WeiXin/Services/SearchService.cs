using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using SmartLife.WeiXin.Pub;
using SmartLife.WeiXin.Model;
using e0571.web.core.Wcf;
using SmartLife.Share.Models.WeiXin.Meb;

using e0571.web.core.Model;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using SmartLife.Share.Behaviors;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SmartLife.WeiXin.xml;
using System.ServiceModel.Web;
using Newtonsoft.Json.Linq;
using e0571.web.core.DataAccess;
using e0571.web.core.Utils;
using System.Web.Script.Serialization;
using SmartLife.Share.Models.WeiXin.Oca;



namespace SmartLife.WeiXin.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SearchService : AppBaseWcfService
    {
        #region 服务记录
        [WebGet(UriTemplate = "searchService?params={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string searchService(string strParms)
        {
            JObject _jObject = JObject.Parse(strParms);
            var type = _jObject["type"].ToString();
            var startTime = _jObject["startTime"].ToString();
            var endTime = _jObject["endTime"].ToString();
            var followToIDNo = _jObject["followToIDNo"].ToString();

            var oldManId = "" ;//老人id

             List<Search> searchs = new List<Search>();
             SearchList searchList = new SearchList();

            //查找老人表得到oldmanid
            var dictionaryBound = new { IDNo = followToIDNo }.ToStringObjectDictionary();
            IList<OldMan> oldMan = BuilderFactory.DefaultBulder().List<OldMan>(dictionaryBound);
            if (oldMan.Count == 0)
            {
                searchList.sum = 0;
                return new JavaScriptSerializer().Serialize(searchList);
            }
            else 
            {
                oldManId = oldMan[0].OldManId.ToString();
            }

            //查询 服务记录
            var ret = "";
            // 查询政府服务记录
            if ("Government".Equals(type)) {
                ret = governmentService(oldManId, startTime, endTime, searchList, searchs);
            }

            //查询 老人服务的 记录
            if ("OldMan".Equals(type)) 
            {
               ret = oldManService(oldManId, startTime, endTime, searchList, searchs);
            }
            return ret;
        }
        #endregion

        #region   查询政府服务记录
        public string governmentService(string oldManId, string startTime, string endTime, SearchList searchList, List<Search> searchs) 
        {
            var dictionaryLife = new { OldManId = oldManId, ServeItemA = "00001", OrderByClause = " ServeBeginTime desc" }.ToStringObjectDictionary();
            List<string> whereClauseLife = new List<string>();
            if (!"".Equals(startTime))
            {
                whereClauseLife.Add("ServeBeginTime>='" + startTime + "'");
            }

            if (!"".Equals(endTime))
            {
                whereClauseLife.Add("ServeBeginTime<='" + endTime + "'");
            }
            if (whereClauseLife.Count > 0)
            {
                dictionaryLife.Add("WhereClause", string.Join(" AND ", whereClauseLife.ToArray()));
            }
            IList<ServiceWorkOrder> governmentServiceWorkOrders = BuilderFactory.DefaultBulder().List<ServiceWorkOrder>(dictionaryLife);
            int sum = 0;
            DateTime dateTime;
            foreach (ServiceWorkOrder GServiceWorkOrder in governmentServiceWorkOrders)
            {
                if (GServiceWorkOrder.ServeBeginTime != null)
                {
                    dateTime = (DateTime)GServiceWorkOrder.ServeBeginTime;
                    searchs.Add(new Search() { name = GServiceWorkOrder.WorkOrderContent, time = dateTime.ToString("yyyy-MM-dd") });
                    sum++;
                }
            }
            searchList.sum = sum;
            searchList.rows = searchs;
            return new JavaScriptSerializer().Serialize(searchList);
        }
        #endregion

        #region   查询老人服务记录
        public string oldManService(string oldManId, string startTime, string endTime, SearchList searchList, List<Search> searchs)
        {
            //老人生活服务的记录
            var dictionaryLife = new { OldManId = oldManId, OrderByClause = " ServeBeginTime desc" }.ToStringObjectDictionary();
            List<string> whereClauseLife = new List<string>();
            whereClauseLife.Add("ServeItemA <> 00001");
            if (!"".Equals(startTime))
            {
                whereClauseLife.Add("ServeBeginTime>='" + startTime + "'");
            }

            if (!"".Equals(endTime))
            {
                whereClauseLife.Add("ServeBeginTime<='" + endTime + "'");
            }
            dictionaryLife.Add("WhereClause", string.Join(" AND ", whereClauseLife.ToArray()));

            IList<ServiceWorkOrder> governmentServiceWorkOrders = BuilderFactory.DefaultBulder().List<ServiceWorkOrder>(dictionaryLife);
            int sum = 0;
            DateTime dateTime;
            foreach (ServiceWorkOrder GServiceWorkOrder in governmentServiceWorkOrders)
            {
                if (GServiceWorkOrder.ServeBeginTime != null)
                {
                    dateTime = (DateTime)GServiceWorkOrder.ServeBeginTime;
                    searchs.Add(new Search() { name = GServiceWorkOrder.WorkOrderContent, time = dateTime.ToString("yyyy-MM-dd") });
                    sum++;
                }
            }
            //老人 呼叫的记录
            var dictionaryCall = new { OldManId = oldManId, OrderByClause = " CheckInTime desc" }.ToStringObjectDictionary();
            List<string> whereClause = new List<string>();
            if (!"".Equals(startTime))
            {
                whereClause.Add("CheckInTime>='" + startTime + "'");
            }

            if (!"".Equals(endTime))
            {
                whereClause.Add("CheckInTime<='" + endTime + "'");
            }
            if (whereClause.Count > 0)
            {
                dictionaryCall.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
            }

            IList<CallService> callServices = BuilderFactory.DefaultBulder().List<CallService>(dictionaryCall);
            foreach (CallService callService in callServices)
            {
                //生活服务的 记录上面已经列出 排除
                if ("3" == callService.ServiceQueueNo.Substring(callService.ServiceQueueNo.Length - 1, 1))
                {
                    continue;
                }
                dateTime = (DateTime)callService.CheckInTime;
                searchs.Add(new Search() { name = callService.Content, time = dateTime.ToString("yyyy-MM-dd") });
                sum++;
            }

            searchList.sum = sum;
            searchList.rows = searchs;
            return new JavaScriptSerializer().Serialize(searchList);
        }
        #endregion
    }
}