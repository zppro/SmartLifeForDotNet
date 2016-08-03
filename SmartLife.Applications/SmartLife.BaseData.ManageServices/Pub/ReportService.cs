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

namespace SmartLife.BaseData.ManageServices.Pub
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReportService : AppBaseWcfService
    {

        #region 查询地域 AreaQuery
        [WebGet(UriTemplate = "AreaQuery?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string AreaQuery(string strParms)
        {
            var re = "";
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);

                var areaid = "";
                foreach (var item in dictionary)
                {
                    areaid = item.Value.ToString();
                }
                var param = "select * from Pub_Area_V where AreaCode like '" + areaid + "%' order by Levels asc";
                // var dictionary = new StringObjectDictionary().MixInJson(param);
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                re = res.ToJson();
            }
            catch (Exception e)
            {
            }
            return re;
        }
        #endregion
        #region 查询地域 AreaQueryPeople
        [WebGet(UriTemplate = "AreaQueryPeople?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string AreaQueryPeople(string strParms)
        {
            var re = "";
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);

                var areaid = "";
                foreach (var item in dictionary)
                {
                    areaid = item.Value.ToString();
                }
                var param = "select * from Pub_Area_V where AreaCode like '" + areaid + "%' and levels <5 order by Levels asc";
                // var dictionary = new StringObjectDictionary().MixInJson(param);
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                re = res.ToJson();
            }
            catch (Exception e)
            {
            }
            return re;
        }
        #endregion


        [WebGet(UriTemplate = "GetCallTotal?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        private string GetCallTotal(string strParms)
        {
            string strret = string.Empty;
            string[] strParm = strParms.Split(',');

            try
            {
                string strSql = "";
                for (int i = 0; i < strParm.Length; i++)
                {
                    if (strParm[i] != "")
                    {
                        strSql = "SELECT COUNT(*) FROM Oca_CallService where AreaId2='" + strParm[i] + "'";
                        IList<StringObjectDictionary> oldBaseInfoLists = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql);
                        foreach (var item in oldBaseInfoLists.ToList())
                        {
                            foreach (var items in item)
                            {
                                strret += items.Value + ",";
                            }
                        }
                    }

                }
                if (strret.Length > 0)
                {
                    strret.Remove(strret.Length - 1, 1);
                }

                //list.Add(oldBaseInfoLists.);
            }
            catch (Exception ex)
            {
                return "";
            }
            return strret;
        }

        #region 得到街道
        [WebGet(UriTemplate = "Getstreet?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string Getstreet(string strParms)
        {
            var re = "";
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                var param = "";
                var area = "";
                foreach (var item in dictionary)
                {
                    if (item.Key.Equals("area") && item.Value.ToString() != "")
                    {
                        area = item.Value.ToString();
                        param = "select  AreaId,AreaName from Pub_Area where ParentId='" + area + "'";
                    }
                    else
                    {
                        param = "select AreaId,AreaName from Pub_Area where Levels = 4";
                    }
                }


                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);

                if (res.Count() == 0)
                {
                    param = "select  AreaId,AreaName from Pub_Area where AreaId='" + area + "'";
                    res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                }
                re = res.ToJson();
            }
            catch (Exception e)
            {

            }
            return re;
        }
        #endregion
        //------------------------------------------------------------------------------------------------购买服务报表
        #region 得到购买服务列表的y轴
        [WebGet(UriTemplate = "GetType?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string GetType(string strParms)
        {
            var dictionary = new StringObjectDictionary().MixInJson(strParms);
            var re = "";
            var param = "";
            var servetype = "";

            try
            {
                foreach (var item in dictionary)
                {
                    if (item.Key.Equals("servetype") && item.Value.ToString() != "")
                    {
                        servetype = item.Value.ToString();
                    }
                }

                if (servetype.Length > 2)
                {
                    param = "select * from Sys_DictionaryItem where ItemCode ='" + servetype + "' and DictionaryId='11013'  ";

                    var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                    re = res.ToJson();
                }
                else if (servetype.Length == 2)
                {

                    param = "select * from Sys_DictionaryItem where ItemId like '" + servetype + "%' and DictionaryId='11013' ";
                    var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                    re = res.ToJson();
                }
                else
                {

                    param = "select * from Sys_DictionaryItem where DictionaryId='11012'";
                    var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                    re = res.ToJson();

                }

            }
            catch (Exception e)
            {

            }
            return re;
        }

        #endregion

        #region 得到服务大类型
        [WebGet(UriTemplate = "GetBigServeType?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string GetBigServeType(string strParms)
        {
            var re = "";
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                var param = "";

                param = "select ID,ItemId,ItemName,ItemCode from Sys_DictionaryItem where DictionaryId='11012' and status='1'";

                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);


                re = res.ToJson();
            }
            catch (Exception e)
            {

            }
            return re;
        }
        #endregion
        #region 得到服务小类型
        [WebGet(UriTemplate = "GetSmallServeType?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string GetSmallServeType(string strParms)
        {
            var re = "";
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                var param = "";

                param = "select ID,ItemId,ItemName,ItemCode from Sys_DictionaryItem where DictionaryId='11013' and status='1'";

                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);


                re = res.ToJson();
            }
            catch (Exception e)
            {

            }
            return re;
        }
        #endregion

        #region 统计购买类型

        [WebGet(UriTemplate = "BuyServe?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string BuyServe(string strParms)
        {
            var dictionary = new StringObjectDictionary().MixInJson(strParms);
            var method = "";
            var CheckInTimeF = "";
            var CheckInTimeT = "";
            var area = "";
            var typeSelect = "";
            var servetype = "";
            var param = "";
            var re = "";
            var dateParam = "";
            var DictionaryId = "";
            var ServeItem = "";


            foreach (var item in dictionary)
            {
                if (item.Key.Equals("selMethod") && item.Value.ToString() != "")
                {
                    method = item.Value.ToString();
                    if ("day".Equals(method) || "week".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(10),CheckInTime,120) ";
                    }
                    else if ("month".Equals(method) || "year".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(7),CheckInTime,120) ";
                    }
                }
                if (item.Key.Equals("CheckInTimeF") && item.Value.ToString() != "")
                {
                    CheckInTimeF = item.Value.ToString();
                    if ("day".Equals(method) || "week".Equals(method))
                    {
                        CheckInTimeF = Convert.ToDateTime(CheckInTimeF).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        CheckInTimeF = Convert.ToDateTime(CheckInTimeF).ToString("yyyy-MM");
                    }
                }
                if (item.Key.Equals("CheckInTimeT") && item.Value.ToString() != "")
                {
                    CheckInTimeT = item.Value.ToString();
                    DateTime time = Convert.ToDateTime(CheckInTimeT);

                    if ("day".Equals(method) || "week".Equals(method))
                    {
                        time = time.AddDays(1);
                        CheckInTimeT = time.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        time = time.AddMonths(1);
                        CheckInTimeT = time.ToString("yyyy-MM");
                    }

                }
                if (item.Key.Equals("area") && item.Value.ToString() != "")
                {
                    area = item.Value.ToString();
                }
                if (item.Key.Equals("typeSelect") && item.Value.ToString() != "")
                {
                    typeSelect = item.Value.ToString();
                }
                if (item.Key.Equals("servetype") && item.Value.ToString() != "")
                {
                    servetype = item.Value.ToString();
                }


            }

            if (servetype != "")
            {
                DictionaryId = "11013";
                ServeItem = "ServeItemB";
                //DictionaryId = "11012";
                //ServeItem = "ServeItemA"; 
            }
            else
            {
                DictionaryId = "11013";
                ServeItem = "ServeItemB";
                //DictionaryId = "11012";
                //ServeItem = "ServeItemA"; 
            }

            try
            {
                param = "select * from  (select " + dateParam + " as time," + ServeItem + " as maxType,SUM(1) " +
                        "as sums  from Oca_ServiceWorkOrder where Status='1' and DoStatus<>'9'";

                param = param + " and (AreaId2='" + area + "' or AreaId='" + area + "' or AreaId3 ='" + area + "' )";

                if (CheckInTimeF != "")
                {
                    param = param + " and " + dateParam + ">='" + CheckInTimeF + "'";
                }
                if (CheckInTimeF != "")
                {
                    param = param + " and " + dateParam + "<'" + CheckInTimeT + "'";
                }
                param = param + " group by " + ServeItem + "," + dateParam + ") " +
                       "serveType left join Sys_DictionaryItem dic on dic.ItemId =serveType.maxType where  dic.DictionaryId='" + DictionaryId + "'";
                if (servetype.Length == 5)
                {
                    param = param + " and ItemCode='" + servetype + "'";
                }

                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                re = res.ToJson();
            }
            catch (Exception e)
            {

            }
            return re;
        }
        #endregion

        //---------------------------------------------------------------------------------------服务类型处理分析
        #region 服务评价等级
        [WebGet(UriTemplate = "AccessGrade?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string AccessGrade(string strParms)
        {
            var re = "";
            try
            {
                var param = "select * from Sys_DictionaryItem where DictionaryId='11017'";
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                re = res.ToJson();
            }
            catch (Exception e)
            {

            }
            return re;
        }

        #endregion

        #region 服务类型分析 评价
        [WebGet(UriTemplate = "Assess?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string Assess(string strParms)
        {
            var dictionary = new StringObjectDictionary().MixInJson(strParms);
            var method = "";
            var CheckInTimeF = "";
            var CheckInTimeT = "";
            var area = "";
            var typeSelect = "";
            var servetype = "";
            var param = "";
            var re = "";
            var dateParam = "";
            var DictionaryId = "";
            var ServeItem = "";
            var assess = "";
            var faceTo = "";


            foreach (var item in dictionary)
            {
                if (item.Key.Equals("selMethod") && item.Value.ToString() != "")
                {
                    method = item.Value.ToString();
                    if ("day".Equals(method) || "week".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(10),CheckInTime,120) ";
                    }
                    else if ("month".Equals(method) || "year".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(7),CheckInTime,120) ";
                    }
                }
                if (item.Key.Equals("CheckInTimeF") && item.Value.ToString() != "")
                {
                    CheckInTimeF = item.Value.ToString();
                    if ("day".Equals(method) || "week".Equals(method))
                    {
                        CheckInTimeF = Convert.ToDateTime(CheckInTimeF).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        CheckInTimeF = Convert.ToDateTime(CheckInTimeF).ToString("yyyy-MM");
                    }
                }
                if (item.Key.Equals("CheckInTimeT") && item.Value.ToString() != "")
                {
                    CheckInTimeT = item.Value.ToString();
                    DateTime time = Convert.ToDateTime(CheckInTimeT);

                    if ("day".Equals(method) || "week".Equals(method))
                    {
                        time = time.AddDays(1);
                        CheckInTimeT = time.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        time = time.AddMonths(1);
                        CheckInTimeT = time.ToString("yyyy-MM");
                    }

                }
                if (item.Key.Equals("area") && item.Value.ToString() != "")
                {
                    area = item.Value.ToString();
                }
                if (item.Key.Equals("typeSelect") && item.Value.ToString() != "")
                {
                    typeSelect = item.Value.ToString();
                }
                if (item.Key.Equals("servetype") && item.Value.ToString() != "")
                {
                    servetype = item.Value.ToString();
                }
                if (item.Key.Equals("assess") && item.Value.ToString() != "")
                {
                    assess = item.Value.ToString();
                }
                if (item.Key.Equals("faceTo") && item.Value.ToString() != "")
                {
                    faceTo = item.Value.ToString();
                    if (faceTo == "merchant")
                    {
                        faceTo = "FeedbackToServiceStation";
                    }
                    else if (faceTo == "oldMan")
                    {
                        faceTo = "FeedbackToOldMan";
                    }
                }
            }

            if (servetype != "")
            {
                DictionaryId = "11013";
                ServeItem = "ServeItemB";
            }
            else
            {
                DictionaryId = "11013";
                ServeItem = "ServeItemB";
                //DictionaryId = "11012";
                //ServeItem = "ServeItemA";
            }

            try
            {
                param = "select * from (select " + dateParam + " as time," + ServeItem + " as item,sum(1) as sums," + faceTo + " as assess " +
                        "from Oca_ServiceWorkOrder where Status='1' and DoStatus<>'9' and (AreaId2='" + area + "' or AreaId='" + area + "' or AreaId3 ='" + area + "' ) ";
                if (CheckInTimeF != "")
                {
                    param = param + " and " + dateParam + ">='" + CheckInTimeF + "'";
                }
                if (CheckInTimeF != "")
                {
                    param = param + " and " + dateParam + "<'" + CheckInTimeT + "'";
                }

                param = param + "group by " + faceTo + "," + dateParam + "," + ServeItem + ") res left join Sys_DictionaryItem dic " +
                       "on dic.ItemId = res.item where dic.DictionaryId='" + DictionaryId + "'";

                if (servetype.Length == 5)
                {
                    param = param + " and res.ItemCode='" + servetype + "'";
                }
                //if (assess != "null")
                //{
                //    param = param + " and res.assess = '" + assess + "'";
                //}
                //else {
                //    param = param + " and res.assess  is null ";
                //}
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                re = res.ToJson();
            }
            catch (Exception e)
            {

            }
            return re;

        }

        #endregion
        //---------------------------------------------------------------------------------------通话类型分析统计
        #region 通话类型的次数 时长
        [WebGet(UriTemplate = "CallType?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string CallType(string strParms)
        {
            var re = "";

            try
            {

                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                var method = "";
                var CheckInTimeF = "";
                var CheckInTimeT = "";
                var area = "";
                var typeSelect = "";
                var dateParam = "";
                var param = "";

                foreach (var item in dictionary)
                {
                    if (item.Key.Equals("selMethod") && item.Value.ToString() != "")
                    {
                        method = item.Value.ToString();
                        if ("day".Equals(method) || "week".Equals(method))
                        {
                            dateParam = " CONVERT(varchar(10),CheckInTime,120) ";
                        }
                        else if ("month".Equals(method) || "year".Equals(method))
                        {
                            dateParam = " CONVERT(varchar(7),CheckInTime,120) ";
                        }
                    }
                    if (item.Key.Equals("CheckInTimeF") && item.Value.ToString() != "")
                    {
                        CheckInTimeF = item.Value.ToString();

                        if ("day".Equals(method) || "week".Equals(method))
                        {
                            CheckInTimeF = Convert.ToDateTime(CheckInTimeF).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            CheckInTimeF = Convert.ToDateTime(CheckInTimeF).ToString("yyyy-MM");
                        }

                    }
                    if (item.Key.Equals("CheckInTimeT") && item.Value.ToString() != "")
                    {
                        CheckInTimeT = item.Value.ToString();
                        DateTime time = Convert.ToDateTime(CheckInTimeT);

                        if ("day".Equals(method) || "week".Equals(method))
                        {
                            time = time.AddDays(1);
                            CheckInTimeT = time.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            time = time.AddMonths(1);
                            CheckInTimeT = time.ToString("yyyy-MM");
                        }
                    }
                    if (item.Key.Equals("area") && item.Value.ToString() != "")
                    {
                        area = item.Value.ToString();
                    }
                    if (item.Key.Equals("typeSelect") && item.Value.ToString() != "")
                    {
                        typeSelect = item.Value.ToString();
                    }
                }

                param = "select " + dateParam + " as datetime,SUM(1) as times,SUM(case when CallSeconds<=0 then 0 else (CallSeconds/60) end) as time, " +
                    "substring(Content,len(Content)-3,4) as servetype from Oca_CallService where (AreaId='" + area + "' or AreaId2='" + area + "' or AreaId3='" + area + "') ";
                if (typeSelect != "")
                {
                    param = param + " and substring(Content,len(Content)-3,4)='" + typeSelect + "'";
                }
                if (CheckInTimeF != "")
                {
                    param = param + "and " + dateParam + ">'" + CheckInTimeF + "'";
                }
                if (CheckInTimeT != "")
                {
                    param = param + "and " + dateParam + "<'" + CheckInTimeT + "'";
                }

                param = param + " group by substring(Content,len(Content)-3,4)," + dateParam;
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                re = res.ToJson();

            }
            catch (Exception e)
            {

            }

            return re;
        }
        #endregion
        //---------------------------------------------------------------------------------------服务类型统计表
        #region 服务类型统计表
        [WebGet(UriTemplate = "ServiceType?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string ServiceType(string strParms) {
            var dictionary = new StringObjectDictionary().MixInJson(strParms);
            var method = "";
            var CheckInTime = "";
            var area = "";
            var param = "";
            var re = "";
            var dateParam = "";
            string levelName = "";

            foreach (var item in dictionary)
            {
                if (item.Key.Equals("selMethod") && item.Value.ToString() != "")
                {
                    method = item.Value.ToString();
                    if ("month".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(7),CheckInTime,120) ";
                    }
                    else if ("year".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(4),CheckInTime,120) ";
                    }
                }
                if (item.Key.Equals("CheckInTime") && item.Value.ToString() != "")
                {
                    CheckInTime = item.Value.ToString();
                    if ("month".Equals(method))
                    {
                        CheckInTime = Convert.ToDateTime(CheckInTime).ToString("yyyy-MM");
                    }
                }

                if (item.Key.Equals("area") && item.Value.ToString() != "")
                {
                    area = item.Value.ToString();
                    levelName = getStreetOrComm(area);
                }
            }
            if (area.Length <= 10)
            {
                param = "select area.AreaName areaName,form.ServeItemB,form.cost from pub_Area area left join (select distinct " + levelName + " Area,ServeItemB,sum(ServeFeeByGov) cost,COUNT(*) total from Oca_ServiceWorkOrder  where status='1'  and " + dateParam + "='" + CheckInTime + "' group by ServeItemB," + levelName + ") form on area.AreaId = form.Area where (area.ParentId='" + area + "')";
            }
            else
            {
                param = "select area.AreaName areaName,form.ServeItemB,form.cost from pub_Area area left join (select distinct " + levelName + " Area,ServeItemB,sum(ServeFeeByGov) cost,COUNT(*) total from Oca_ServiceWorkOrder  where status='1'  and " + dateParam + "='" + CheckInTime + "' group by ServeItemB," + levelName + ") form on area.AreaId = form.Area where (area.ParentId='" + area + "' or area.AreaId='" + area + "')";
            }
            var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
            re = res.ToJson();
            return re;
        }
        #endregion

        //------------------------------------------------------------------------ 老人费用一览表
        #region 老人费用一览表
        [WebGet(UriTemplate = "staOldCost?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string staOldCost(string strParms)
        {
            var dictionary = new StringObjectDictionary().MixInJson(strParms);
            var method = "";
            var CheckInTime = "";
            var area = "";
            var param = "";
            var re = "";
            var dateParam = "";

            foreach (var item in dictionary)
            {
                if (item.Key.Equals("selMethod") && item.Value.ToString() != "")
                {
                    method = item.Value.ToString();
                    if ("month".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(7),CheckInTime,120) ";
                    }
                    else if ("year".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(4),CheckInTime,120) ";
                    }
                }
                if (item.Key.Equals("CheckInTime") && item.Value.ToString() != "")
                {
                    CheckInTime = item.Value.ToString();
                    if ("month".Equals(method))
                    {
                        CheckInTime = Convert.ToDateTime(CheckInTime).ToString("yyyy-MM");
                    }
                       
                }
                if (item.Key.Equals("area") && item.Value.ToString() != "")
                {
                    area = item.Value.ToString();
                }
                param = "select oldManName,OldManId,ServeItemB,sum(ServeFeeByGov) cost from Oca_ServiceWorkOrder where status='1' and (AreaId2='" + area + "' or AreaId='" + area + "' or AreaId3 ='" + area + "' ) and " + dateParam + "='" + CheckInTime + "' group by ServeItemB,oldManName,OldManId ";
            }
            var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
            re = res.ToJson();
            return re;
        }
         #endregion

        //------------------------------------------------------------------------ 商家费用一览表
        #region 商家费用一览表
        [WebGet(UriTemplate = "staServiceCost?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string staServiceCost(string strParms)
        {
            var dictionary = new StringObjectDictionary().MixInJson(strParms);
            var method = "";
            var CheckInTime = "";
            var area = "";
            var param = "";
            var re = "";
            var dateParam = "";

            foreach (var item in dictionary)
            {
                if (item.Key.Equals("selMethod") && item.Value.ToString() != "")
                {
                    method = item.Value.ToString();
                    if ("month".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(7),CheckInTime,120) ";
                    }
                    else if ("year".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(4),CheckInTime,120) ";
                    }
                }
                if (item.Key.Equals("CheckInTime") && item.Value.ToString() != "")
                {
                    CheckInTime = item.Value.ToString();
                    if ("month".Equals(method))
                    {
                        CheckInTime = Convert.ToDateTime(CheckInTime).ToString("yyyy-MM");
                    }

                }
                if (item.Key.Equals("area") && item.Value.ToString() != "")
                {
                    area = item.Value.ToString();
                }
                param = "select ServeStationName,ServeStationId,ServeItemB,sum(ServeFeeByGov) cost from Oca_ServiceWorkOrder where status='1' and (AreaId2='" + area + "' or AreaId='" + area + "' or AreaId3 ='" + area + "' ) and " + dateParam + "='" + CheckInTime + "' group by ServeItemB,ServeStationName,ServeStationId ";
            }
            var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
            re = res.ToJson();
            return re;
        }
        #endregion

        //------------------------------------------------------------------------ 老人费用明细表
        #region 老人费用明细表
        [WebGet(UriTemplate = "getOldManDetail?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string getOldManDetail(string strParms)
        {
            var dictionary = new StringObjectDictionary().MixInJson(strParms);
            var method = "";
            var CheckInTime = "";
            var area = "";
            var oldManId = "";
            var param = "";
            var re = "";
            var dateParam = "";

            foreach (var item in dictionary)
            {
                if (item.Key.Equals("selMethod") && item.Value.ToString() != "")
                {
                    method = item.Value.ToString();
                    if ("month".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(7),CheckInTime,120) ";
                    }
                    else if ("year".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(4),CheckInTime,120) ";
                    }
                }
                if (item.Key.Equals("CheckInTime") && item.Value.ToString() != "")
                {
                    CheckInTime = item.Value.ToString();
                    if ("month".Equals(method))
                    {
                        CheckInTime = Convert.ToDateTime(CheckInTime).ToString("yyyy-MM");
                    }

                }
                if (item.Key.Equals("area") && item.Value.ToString() != "")
                {
                    area = item.Value.ToString();
                }

                if (item.Key.Equals("oldManId") && item.Value.ToString() != "")
                {
                    oldManId = item.Value.ToString();
                }
                param = "select CONVERT(varchar(10),CheckInTime,120) times,ServeItemB,ServeFeeByGov cost,ServeStationName from  Oca_ServiceWorkOrder where status='1' and   (AreaId2='" + area + "' or AreaId='" + area + "' or AreaId3 ='" + area + "' ) and " + dateParam + "='" + CheckInTime + "' and OldManId='" + oldManId + "'";
            }
            var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
            re = res.ToJson();
            return re;
        }
        #endregion

        //------------------------------------------------------------------------ 商家费用明细表
        #region 商家费用明细表
        [WebGet(UriTemplate = "getShopDetail?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string getShopDetail(string strParms)
        {
            var dictionary = new StringObjectDictionary().MixInJson(strParms);
            var method = "";
            var CheckInTime = "";
            var area = "";
            var ServeStationId = "";
            var param = "";
            var re = "";
            var dateParam = "";

            foreach (var item in dictionary)
            {
                if (item.Key.Equals("selMethod") && item.Value.ToString() != "")
                {
                    method = item.Value.ToString();
                    if ("month".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(7),CheckInTime,120) ";
                    }
                    else if ("year".Equals(method))
                    {
                        dateParam = " CONVERT(varchar(4),CheckInTime,120) ";
                    }
                }
                if (item.Key.Equals("CheckInTime") && item.Value.ToString() != "")
                {
                    CheckInTime = item.Value.ToString();
                    if ("month".Equals(method))
                    {
                        CheckInTime = Convert.ToDateTime(CheckInTime).ToString("yyyy-MM");
                    }

                }
                if (item.Key.Equals("area") && item.Value.ToString() != "")
                {
                    area = item.Value.ToString();
                }

                if (item.Key.Equals("ServeStationId") && item.Value.ToString() != "")
                {
                    ServeStationId = item.Value.ToString();
                }
                param = "select CONVERT(varchar(10),CheckInTime,120) times,ServeItemB,isnull(ServeFeeByGov,0) cost,OldManName from  Oca_ServiceWorkOrder where status='1' and   (AreaId2='" + area + "' or AreaId='" + area + "' or AreaId3 ='" + area + "' ) and " + dateParam + "='" + CheckInTime + "' and ServeStationId='" + ServeStationId + "'";
            }
            var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
            re = res.ToJson();
            return re;
        }
        #endregion

        //------------------------------------------------------------------------获取查询街道还是社区
        public string getStreetOrComm(string area) {
            var leavesql = "select Levels from Pub_Area where ParentId ='" + area + "'";
            var resLeave = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(leavesql);
            string level = "";
            string levelName = "";
            foreach (var item in resLeave)
            {
                level = item["Levels"].ToString();

            }
            if ("4".Equals(level))
            {
                levelName = "AreaId2";
            }
            else {
                levelName = "AreaId3";
            }
            return levelName;
        }
        //----------------------------------------------------------------------------------------
        #region 通话记录查询 CallQuery
        [WebGet(UriTemplate = "CallQuery?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string CallQuery(string strParms)
        {
            var re = "";

            try
            {

                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                var method = "";
                var CheckInTimeF = "";
                var CheckInTimeT = "";
                var area = "";
                var typeSelect = "";

                foreach (var item in dictionary)
                {
                    if (item.Key.Equals("selMethod") && item.Value.ToString() != "")
                    {
                        method = item.Value.ToString();
                    }
                    if (item.Key.Equals("CheckInTimeF") && item.Value.ToString() != "")
                    {
                        CheckInTimeF = item.Value.ToString();

                    }
                    if (item.Key.Equals("CheckInTimeT") && item.Value.ToString() != "")
                    {
                        CheckInTimeT = item.Value.ToString();
                    }
                    if (item.Key.Equals("area") && item.Value.ToString() != "")
                    {
                        area = item.Value.ToString();
                    }
                    if (item.Key.Equals("typeSelect") && item.Value.ToString() != "")
                    {
                        typeSelect = item.Value.ToString();
                    }

                }

                var leavesql = "select Levels from Pub_Area where ParentId ='" + area + "'";
                var resLeave = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(leavesql);
                string level = "";
                foreach (var item in resLeave)
                {
                    level = item["Levels"].ToString();

                }

                var param = "";
                if ("day".Equals(method) || "week".Equals(method))
                {
                    if ("4".Equals(level))
                    {
                        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(10),CheckInTime,120) as time,AreaId2 as address,COUNT(1) as sums from Oca_CallService ";
                        if (typeSelect != "")
                        {
                            param = param + " where Content like '%" + typeSelect + "'";
                        }
                        param = param + " group by CONVERT(varchar(10),CheckInTime,120),AreaId2 ) as cal where pa.AreaId = cal.address";
                    }
                    else if ("5".Equals(level))
                    {
                        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(10),CheckInTime,120) as time,AreaId3 as address,COUNT(1) as sums from Oca_CallService ";
                        if (typeSelect != "")
                        {
                            param = param + " where Content like '%" + typeSelect + "'";
                        }
                        param = param + " group by CONVERT(varchar(10),CheckInTime,120),AreaId3 ) as cal where pa.AreaId = cal.address and pa.ParentId='" + area + "'";
                    }
                    else
                    {
                        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(10),CheckInTime,120) as time,AreaId3 as address,COUNT(1) as sums from Oca_CallService ";
                        if (typeSelect != "")
                        {
                            param = param + " where Content like '%" + typeSelect + "'";
                        }
                        param = param + " group by CONVERT(varchar(10),CheckInTime,120),AreaId3 ) as cal where pa.AreaId = cal.address and pa.AreaId='" + area + "'";
                    }
                }
                else if ("month".Equals(method) || "year".Equals(method))
                {
                    if ("4".Equals(level))
                    {
                        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(7),CheckInTime,120) as time,AreaId2 as address,COUNT(1) as sums from Oca_CallService ";
                        if (typeSelect != "")
                        {
                            param = param + " where Content like '%" + typeSelect + "'";
                        }
                        param = param + " group by CONVERT(varchar(7),CheckInTime,120),AreaId2 ) as cal where pa.AreaId = cal.address";
                    }
                    else if ("5".Equals(level))
                    {
                        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(7),CheckInTime,120) as time,AreaId3 as address,COUNT(1) as sums from Oca_CallService ";
                        if (typeSelect != "")
                        {
                            param = param + " where Content like '%" + typeSelect + "'";
                        }
                        param = param + " group by CONVERT(varchar(7),CheckInTime,120),AreaId3 ) as cal where pa.AreaId = cal.address and pa.ParentId='" + area + "'";
                    }
                    else
                    {
                        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(7),CheckInTime,120) as time,AreaId3 as address,COUNT(1) as sums from Oca_CallService ";
                        if (typeSelect != "")
                        {
                            param = param + " where Content like '%" + typeSelect + "'";
                        }
                        param = param + " group by CONVERT(varchar(7),CheckInTime,120),AreaId3 ) as cal where pa.AreaId = cal.address and pa.AreaId='" + area + "'";
                    }
                }

                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                re = res.ToJson();
            }
            catch (Exception e)
            {

            }

            return re;

        }
        #endregion

        #region 通话时长查询 CallLength
        [WebGet(UriTemplate = "CallLength?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string CallLength(string strParms)
        {
            var re = "";

            try
            {

                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                var method = "";
                var CheckInTimeF = "";
                var CheckInTimeT = "";
                var area = "";
                var typeSelect = "";
                foreach (var item in dictionary)
                {
                    if (item.Key.Equals("selMethod") && item.Value.ToString() != "")
                    {
                        method = item.Value.ToString();
                    }
                    if (item.Key.Equals("CheckInTimeF") && item.Value.ToString() != "")
                    {
                        CheckInTimeF = item.Value.ToString();
                    }
                    if (item.Key.Equals("CheckInTimeT") && item.Value.ToString() != "")
                    {
                        CheckInTimeT = item.Value.ToString();
                    }
                    if (item.Key.Equals("area") && item.Value.ToString() != "")
                    {
                        area = item.Value.ToString();
                    }
                    if (item.Key.Equals("typeSelect") && item.Value.ToString() != "")
                    {
                        typeSelect = item.Value.ToString();
                    }

                }

                var leavesql = "select Levels from Pub_Area where ParentId ='" + area + "'";
                var resLeave = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(leavesql);
                string level = "";
                foreach (var item in resLeave)
                {
                    level = item["Levels"].ToString();

                }

                var param = "";
                if ("day".Equals(method) || "week".Equals(method))
                {
                    if ("4".Equals(level))
                    {
                        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(10),CheckInTime,120) as time,AreaId2 as address,sum(case when (CallSeconds<=0) then 0 else (CallSeconds/60) end) as sums from Oca_CallService ";
                        if (typeSelect != "")
                        {
                            param = param + " where Content like '%" + typeSelect + "'";
                        }
                        param = param + " group by CONVERT(varchar(10),CheckInTime,120),AreaId2 ) as cal where pa.AreaId = cal.address";
                    }
                    else if ("5".Equals(level))
                    {
                        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(10),CheckInTime,120) as time,AreaId3 as address,sum(case when (CallSeconds<=0) then 0 else (CallSeconds/60) end) as sums from Oca_CallService ";
                        if (typeSelect != "")
                        {
                            param = param + " where Content like '%" + typeSelect + "'";

                        }
                        param = param + " group by CONVERT(varchar(10),CheckInTime,120),AreaId3 ) as cal where pa.AreaId = cal.address and pa.ParentId='" + area + "'";
                    }
                    else
                    {
                        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(10),CheckInTime,120) as time,AreaId3 as address,sum(case when (CallSeconds<=0) then 0 else (CallSeconds/60) end) as sums from Oca_CallService ";
                        if (typeSelect != "")
                        {
                            param = param + " where Content like '%" + typeSelect + "'";
                        }
                        param = param + " group by CONVERT(varchar(10),CheckInTime,120),AreaId3 ) as cal where pa.AreaId = cal.address and pa.AreaId='" + area + "'";
                    }
                }
                else if ("month".Equals(method) || "year".Equals(method))
                {
                    if ("4".Equals(level))
                    {
                        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(7),CheckInTime,120) as time,AreaId2 as address,sum(case when (CallSeconds<=0) then 0 else (CallSeconds/60) end) as sums from Oca_CallService ";
                        if (typeSelect != "")
                        {
                            param = param + " where Content like '%" + typeSelect + "'";
                        }
                        param = param + " group by CONVERT(varchar(7),CheckInTime,120),AreaId2 ) as cal where pa.AreaId = cal.address";
                    }
                    else if ("5".Equals(level))
                    {
                        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(7),CheckInTime,120) as time,AreaId3 as address,sum(case when (CallSeconds<=0) then 0 else (CallSeconds/60) end) as sums from Oca_CallService ";
                        if (typeSelect != "")
                        {
                            param = param + " where Content like '%" + typeSelect + "'";
                        }
                        param = param + " group by CONVERT(varchar(7),CheckInTime,120),AreaId3 ) as cal where pa.AreaId = cal.address and pa.ParentId='" + area + "'";

                    }
                    else
                    {
                        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(7),CheckInTime,120) as time,AreaId3 as address,sum(case when (CallSeconds<=0) then 0 else (CallSeconds/60) end) as sums from Oca_CallService ";
                        if (typeSelect != "")
                        {
                            param = param + " where Content like '%" + typeSelect + "'";
                        }
                        param = param + " group by CONVERT(varchar(7),CheckInTime,120),AreaId3 ) as cal where pa.AreaId = cal.address and pa.AreaId='" + area + "'";
                    }
                }
                //else if ("year".Equals(method))
                //{
                //    if ("4".Equals(level))
                //    {
                //        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(4),CheckInTime,120) as time,AreaId2 as address,sum(case when (CallSeconds<=0) then 0 else (CallSeconds/60) end) as sums from Oca_CallService group by CONVERT(varchar(4),CheckInTime,120),AreaId2 ) as cal where pa.AreaId = cal.address";
                //    }
                //    else if ("year".Equals(method) && "5".Equals(level))
                //    {
                //        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(4),CheckInTime,120) as time,AreaId3 as address,sum(case when (CallSeconds<=0) then 0 else (CallSeconds/60) end) as sums from Oca_CallService group by CONVERT(varchar(4),CheckInTime,120),AreaId3 ) as cal where pa.AreaId = cal.address and pa.ParentId='" + area + "'";

                //    }
                //    else
                //    {
                //        param = "select cal.time,pa.AreaName,cal.sums from Pub_Area as pa,(Select CONVERT(varchar(4),CheckInTime,120) as time,AreaId3 as address,sum(case when (CallSeconds<=0) then 0 else (CallSeconds/60) end) as sums from Oca_CallService group by CONVERT(varchar(4),CheckInTime,120),AreaId3 ) as cal where pa.AreaId = cal.address and pa.AreaId='" + area + "'";
                //    }
                //}
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                re = res.ToJson();
            }
            catch (Exception e)
            {

            }

            return re;

        }
        #endregion


        #region 查询列表 sqlQuerystrParms
        [WebGet(UriTemplate = "sqlQuery?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string sqlQuery(string strParms)
        {
            var result = "";

            try
            {

                var param = "SELECT * FROM Oca_OldManBaseInfo  where 1=1";
                var dictionary = new StringObjectDictionary().MixInJson(strParms);

                foreach (var item in dictionary)
                {

                    if (item.Key.Equals("CheckInTimeF") && item.Value.ToString() != "")
                    {
                        param = param + " and CheckInTime > '" + item.Value.ToString() + "'";
                    }
                    if (item.Key.Equals("CheckInTimeT") && item.Value.ToString() != "")
                    {
                        param = param + " and CheckInTime < '" + item.Value.ToString() + "'";
                    }
                    if (item.Key.Equals("OldManN") && item.Value.ToString() != "")
                    {
                        param = param + " and OldManName = '" + item.Value.ToString() + "'";
                    }
                    if (item.Key.Equals("GenderS") && item.Value.ToString() != "")
                    {
                        param = param + " and Gender = '" + item.Value.ToString() + "'";
                    }
                    if (item.Key.Equals("OperatedOnF") && item.Value.ToString() != "")
                    {
                        param = param + " and OperatedOn > '" + item.Value.ToString() + "'";
                    }
                    if (item.Key.Equals("OperatedOnT") && item.Value.ToString() != "")
                    {
                        param = param + " and OperatedOn < '" + item.Value.ToString() + "'";
                    }
                    if (item.Key.Equals("Addres") && item.Value.ToString() != "")
                    {
                        param = param + " and Address like N'%" + item.Value.ToString() + "%'";
                    }
                    if (item.Key.Equals("Telt") && item.Value.ToString() != "")
                    {
                        param = param + " and Tel like N'%" + item.Value.ToString() + "%'";
                    }
                    if (item.Key.Equals("Area") && item.Value.ToString() != "")
                    {
                        param = param + " and (AreaId = '" + item.Value.ToString() + "' or AreaId2 = '" + item.Value.ToString() + "' or AreaId3 = '" + item.Value.ToString() + "')";
                    }
                }

                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                result = res.ToJson();
            }
            catch (Exception ex)
            {
                //result.Success = false;
                //result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 许敢自定义方法

        /// <summary>
        /// 得到每个街道的人数
        /// </summary>
        /// <param name="strParms"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = "GetAreaPeople?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        private string GetAreaPeople(string strParms)
        {
            string strret = string.Empty;
            string[] strParm = strParms.Split(',');

            try
            {
                string strSql = "";
                for (int i = 0; i < strParm.Length - 2; i++)
                {
                    if (strParm[i] != "")
                    {
                        strSql = "SELECT COUNT(*) FROM Oca_OldManBaseInfo where (AreaId2='" + strParm[i] + "' or AreaId3='" + strParm[i] + "')";
                        string aa = strParm[strParm.Length - 2];
                        bool jj = (strParm[strParm.Length - 2] == "");
                        bool jja = (strParm[strParm.Length - 1].Trim() == "");
                        string jjad = strParm[strParm.Length - 1].Trim();
                        bool jjaa = (!"".Equals(strParm[strParm.Length - 2].Trim()));
                        bool jjaaa = (!"".Equals(strParm[strParm.Length - 1].Trim()));
                        if (("" != strParm[strParm.Length - 2].Trim()) && ("" != strParm[strParm.Length - 1].Trim()))
                        {
                            strSql = strSql + " and (OperatedOn>'" + strParm[strParm.Length - 2] + "' and OperatedOn<'" + strParm[strParm.Length - 1] + "')";
                        }
                        IList<StringObjectDictionary> oldBaseInfoLists = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql);
                        foreach (var item in oldBaseInfoLists.ToList())
                        {
                            foreach (var items in item)
                            {
                                strret += items.Value + ",";
                            }
                        }
                    }

                }
                if (strret.Length > 0)
                {
                    strret.Remove(strret.Length - 1, 1);
                }

                //list.Add(oldBaseInfoLists.);
            }
            catch (Exception ex)
            {
                return "";
            }
            return strret;
        }


        /// <summary>
        /// 省
        /// </summary>
        /// <param name="strParms"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = "GetProvince?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        private string GetProvince(string strParms)
        {
            string strret = string.Empty;
            try
            {
                string sqlStatement = " select ItemId,ItemName From Sys_DictionaryItem where Levels=1  and DictionaryId='00005'";
                IList<StringObjectDictionary> oldBaseInfoList = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sqlStatement);
                strret = oldBaseInfoList.ToJson();
            }
            catch (Exception ex)
            {
                return "";
            }
            return strret;
        }


        /// <summary>
        /// 市
        /// </summary>
        /// <param name="strParms"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = "GetCity?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        private string GetCity(string strParms)
        {
            string strret = string.Empty;
            try
            {
                string sqlStatement = " select ItemId,ItemName From Sys_DictionaryItem where   ParentId='" + strParms + "'";
                IList<StringObjectDictionary> oldBaseInfoList = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sqlStatement);
                strret = oldBaseInfoList.ToJson();
            }
            catch (Exception ex)
            {
                return "";
            }
            return strret;
        }

        /// <summary>
        /// 得到区下面有多少街道
        /// </summary>
        /// <param name="strParms"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = "GetBaoBiao?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        private string GetAreaBaoBiao(string strParms)
        {
            string strret = string.Empty;
            try
            {
                string sqlStatement = "select AreaId,AreaName From Pub_Area  where ParentId='" + strParms + "'";
                IList<StringObjectDictionary> Areaid = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sqlStatement);
                strret = Areaid.ToJson();
            }
            catch (Exception ex)
            {
                return "";
            }
            return strret;
        }

        #endregion

        #region 查询列表 Query
        public CollectionInvokeResult<OldManBaseInfo> Query(string strParms)
        {
            CollectionInvokeResult<OldManBaseInfo> result = new CollectionInvokeResult<OldManBaseInfo> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<OldManBaseInfo>(dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 扩展

        #region 老年人花名册统计
        [WebGet(UriTemplate = "OldQuery?params={strParams}", RequestFormat = WebMessageFormat.Json)]
        public string OldQuery(string strParams)
        {
            string result;
            try
            {
                var param = "SELECT OldManName,IDNo,Gender,FLOOR(datediff(DY,Birthday,getdate())/365.25) Age,HealthInsuranceFlag,SocialInsuranceFlag,Tel,Mobile,LivingState,OldManIdentity,Address FROM Oca_OldManBaseInfo  where 1=1 and Status=1";
                var dictionary = new StringObjectDictionary().MixInJson(strParams);

                foreach (var item in dictionary)
                {
                    
                    if (item.Key.Equals("Area") && item.Value.ToString() != "")
                    {
                        param = param + " and (AreaId = '" + item.Value.ToString() + "' or AreaId2 = '" + item.Value.ToString() + "' or AreaId3 = '" + item.Value.ToString() + "')";
                    }
                    if (item.Key.Equals("Gender") && item.Value.ToString() != "")
                    {
                        param = param + " and Gender = '" + item.Value.ToString() + "'";
                    }
                    if (item.Key.Equals("HealthInsuranceFlag") && item.Value.ToString() != "")
                    {
                        param = param + " and HealthInsuranceFlag = '" + item.Value.ToString() + "'";
                    }
                    if (item.Key.Equals("SocialInsuranceFlag") && item.Value.ToString() != "")
                    {
                        param = param + " and SocialInsuranceFlag = '" + item.Value.ToString() + "'";
                    }
                    if (item.Key.Equals("Age") && item.Value.ToString() != "")
                    {
                        string age = item.Value.ToString();
                        if (age == "0") { age = "asc"; }
                        else if (age == "1") { age = "desc"; }
                        param = param + " order by Birthday " + age;
                    }
                }

                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                result = res.ToJson();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion 

        #region 老年人年龄统计
        [WebGet(UriTemplate = "OldAgeQuery?params={strParams}", RequestFormat = WebMessageFormat.Json)]
        public string OldAgeQuery(string strParams)
        {
            string result;
            var area = "";
            string levelName = "";
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParams);
                foreach (var item in dictionary)
                {
                    if (item.Key.Equals("Area") && item.Value.ToString() != "")
                    {
                        area = item.Value.ToString();
                        levelName = getStreetOrComm(area);
                    }
                }
                var param = "with b as(select area.AreaName areaName,Man,Woman,Age1,Age2,Age3,Age4,Age5,Total from pub_Area area,(select distinct " + levelName + " Area,sum(case when Gender='M' then 1 else 0 end)Man,sum(case when Gender='F' then 1 else 0 end)Woman,sum(case when FLOOR(datediff(DY,Birthday,getdate())/365.25) Between 60 and 70 then 1 else 0 end)Age1,sum(case when FLOOR(datediff(DY,Birthday,getdate())/365.25) Between 71 and 80 then 1 else 0 end)Age2,sum(case when FLOOR(datediff(DY,Birthday,getdate())/365.25) Between 81 and 90 then 1 else 0 end)Age3,sum(case when FLOOR(datediff(DY,Birthday,getdate())/365.25) Between 91 and 100 then 1 else 0 end)Age4,sum(case when FLOOR(datediff(DY,Birthday,getdate())/365.25) Between 101 and 200 then 1 else 0 end)Age5,COUNT(*)Total from Oca_OldManBaseInfo where 1=1 and Status=1 and  (AreaId2='" + area + "' or AreaId='" + area + "' or AreaId3 ='" + area + "' ) group by " + levelName + ") a where area.AreaId = a.Area)select * from b union select '合计',sum(b.Man),sum(b.Woman),sum(b.Age1),sum(b.Age2),sum(b.Age3),sum(b.Age4),sum(b.Age5),SUM(b.Total) from b order by areaname desc";

                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                result = res.ToJson();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion

        #region 老年人享受服务统计
        [WebGet(UriTemplate = "OldServiceQuery?params={strParams}", RequestFormat = WebMessageFormat.Json)]
        public string OldServiceQuery(string strParams)
        {
            string result; 
            var area = "";
            string levelName = "";
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParams);
  
                foreach (var item in dictionary)
                {
                    if (item.Key.Equals("Area") && item.Value.ToString() != "")
                    {
                        area = item.Value.ToString();
                        levelName = getStreetOrComm(area);
                    }
                }
                var param = "with b as (select area.AreaName areaName,Home,Knowledge from Pub_Area area, (select distinct " + levelName + " Area,COUNT(*)Home,COUNT(*)Knowledge from Oca_OldManBaseInfo where 1=1 and Status=1 and  (AreaId2='" + area + "' or AreaId='" + area + "' or AreaId3 ='" + area + "' ) group by " + levelName + ")a where area.AreaId = a.Area)select * from b union select '合计',SUM(Home),SUM(Knowledge) from b order by areaname desc";
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                result = res.ToJson();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion

        #region 老人结余信息
        [WebGet(UriTemplate = "OldResult?params={strParams}", RequestFormat = WebMessageFormat.Json)]
        public string OldResult(string strParams)
        {
            string result;
            var area = "";
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParams);

                foreach (var item in dictionary)
                {
                    if (item.Key.Equals("Area") && item.Value.ToString() != "")
                    {
                        area = item.Value.ToString(); 
                    }
                }
                var param = "select a.OldManName as oldmanName,sum(case b.serveitemb when '01001' then b.Remain else 0 end ) as  maid,sum(case b.serveitemb when '01002' then b.Remain else 0 end ) as  maintain,sum(case b.serveitemb when '01003' then b.Remain else 0 end ) as  medical from (select x.oldmanid, x.oldmanname from oca_oldmanbaseinfo x inner join oca_oldmanconfiginfo y on x.oldmanid=y.oldmanid where x.status=1 and y.govturnkeyflag=1 and (x.AreaId2='" + area + "' or x.AreaId='" + area + "' or x.AreaId3 ='" + area + "' ) ) a left join (select  oldmanid,serveitemb,Remain from Oca_EPay_OldManAccount) b on a.oldmanid=b.oldmanid  group by a.oldmanid,a.oldmanname";
                //
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                result = res.ToJson();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion


        #region 老年人居住情况统计
        [WebGet(UriTemplate = "OldLivingQuery?params={strParams}", RequestFormat = WebMessageFormat.Json)]
        public string OldLivingQuery(string strParams)
        {
            string result;
            try
            {
                
                var dictionary = new StringObjectDictionary().MixInJson(strParams);
                var area = "";
            string levelName = "";
                foreach (var item in dictionary)
                {
                    if (item.Key.Equals("Area") && item.Value.ToString() != "")
                    {
                        area = item.Value.ToString();
                        levelName = getStreetOrComm(area);
                    }
                }
                var param = " with b as (select area.AreaName areaName,LivingState1,LivingState2,LivingState3,LivingState4,Total from Pub_Area area, (select distinct " + levelName + " Area,  SUM(case when LivingState='00001' then 1 else 0 end)LivingState1, SUM(case when LivingState='00002' then 1 else 0 end)LivingState2, SUM(case when LivingState='00003' then 1 else 0 end)LivingState3, SUM(case when LivingState='00004' then 1 else 0 end)LivingState4, COUNT(*)Total from Oca_OldManBaseInfo where 1=1 and Status=1 and  (AreaId2='" + area + "' or AreaId='" + area + "' or AreaId3 ='" + area + "' ) group by " + levelName + ")a where area.AreaId = a.Area)select * from b union select '合计',SUM(LivingState1),SUM(LivingState2),SUM(LivingState3),SUM(LivingState4),SUM(Total) from b order by areaname desc";
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                result = res.ToJson();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion

        #region 老年人身份情况统计
        [WebGet(UriTemplate = "OldIdentityQuery?params={strParams}", RequestFormat = WebMessageFormat.Json)]
        public string OldIdentityQuery(string strParams)
        {
            string result;
            try
            {
                
                var dictionary = new StringObjectDictionary().MixInJson(strParams);
                var area = "";
                string levelName = "";
                foreach (var item in dictionary)
                {
                    if (item.Key.Equals("Area") && item.Value.ToString() != "")
                    {
                        area = item.Value.ToString();
                        levelName = getStreetOrComm(area);
                    }
                }
                var param = "with b as (select area.AreaName areaName,Identity1,Identity2,Total from Pub_Area area, (select distinct " + levelName + " Area, SUM(case when OldManIdentity='00001' then 1 else 0 end)Identity1,SUM(case when OldManIdentity='00002' then 1 else 0 end)Identity2,COUNT(*)Total from Oca_OldManBaseInfo where 1=1 and Status=1 and  (AreaId2='" + area + "' or AreaId='" + area + "' or AreaId3 ='" + area + "' ) group by " + levelName + ")a where area.AreaId = a.Area)select * from b union select '合计',SUM(Identity1),SUM(Identity2),SUM(Total) from b order by areaname desc";
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                result = res.ToJson();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion

        #region 呼叫情况统计
        [WebGet(UriTemplate = "CallTypeQuery?params={strParams}", ResponseFormat = WebMessageFormat.Json)]
        public string CallTypeQuery(string strParams)
        {
            string result;

            try
            {

                var dictionary = new StringObjectDictionary().MixInJson(strParams);
                var method = "";
                var checkInTime = "";
                var area = "";
                var dateParam = "";
                var param = "";
                string levelName = "";
                string sql = "";
                foreach (var item in dictionary)
                {
                    if (item.Key.Equals("selMethod") && item.Value.ToString() != "")
                    {
                        method = item.Value.ToString();
                        if ("month".Equals(method))
                        {
                            dateParam = " CONVERT(varchar(7),CheckInTime,120) ";
                        }
                        else if ("year".Equals(method))
                        {
                            dateParam = " CONVERT(varchar(4),CheckInTime,120) ";
                        }
                    }
                    if (item.Key.Equals("checkInTime") && item.Value.ToString() != "")
                    {
                        checkInTime = item.Value.ToString();
                        if (checkInTime != "")
                        {
                            sql = "and  " + dateParam + "='" + checkInTime + "'";
                        }
                    }
                    if (item.Key.Equals("Area") && item.Value.ToString() != "")
                    {
                        area = item.Value.ToString();
                        levelName = getStreetOrComm(area);
                    }
                }
                param = "with b as(select area.AreaName areaName,a.Number,a.Time,a.Servetype from Pub_Area area,(select distinct " + levelName + " Area,SUM(1)Number,SUM(case when CAST(callseconds as float)/60 between 0.01 and 1 then 1 else (CallSeconds/60)+1 end)Time,substring(ServiceQueueNo,len(ServiceQueueNo)-0,1)Servetype from Oca_CallService where Status=1 " + sql + " and (AreaId2='" + area + "' or AreaId='" + area + "' or AreaId3 ='" + area + "' ) group by substring(ServiceQueueNo,len(ServiceQueueNo)-0,1)," + levelName +
                " union select distinct " + levelName + " Area,SUM(1)Number,SUM(case when CAST(callseconds as float)/60 between 0.01 " +
                "and 1 then 1 else (CallSeconds/60)+1 end)Time,4 Servetype from  Oca_CallService where Status=1  and (AreaId2='"+ area + "' or AreaId='" + area + "' or AreaId3 ='" + area + "' )" +
 "and OldManId in (select distinct oldmanid from Oca_OldManConfigInfo where GovTurnkeyFlag=1) "+ "group by "+ levelName + ")a where area.AreaId = a.Area)select * from b order by areaname desc";
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                result = res.ToJson();

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
        #endregion

        #region 饼状图
        [WebGet(UriTemplate = "CallTypeQueryPie?parms={strParams}", ResponseFormat = WebMessageFormat.Json)]
        public string CallTypeQueryPie(string strParams)
        {
            string result;

            try
            {

                var dictionary = new StringObjectDictionary().MixInJson(strParams);
                var checkInTime="";
                var param = "";
                foreach (var item in dictionary)
                {

                    if (item.Key.Equals("CheckInTime") && item.Value.ToString() != "")
                    {
                        checkInTime = item.Value.ToString();
                        checkInTime = Convert.ToDateTime(checkInTime).ToString("yyyy-MM-dd");
                    }
                }
                param = "select item1,item2,item3,item4 from oca_call_service_stat_report where CONVERT(varchar(10),dates,120)='" + checkInTime +"'";
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                result = res.ToJson();

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
        #endregion

        #region 老人总数
        [WebGet(UriTemplate = "oldManCount", ResponseFormat = WebMessageFormat.Json)]
        public string oldManCount()
        {
            string result;

            try
            {

                var param = "";
                param = "select sum(1) count from oca_oldmanbaseinfo";
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                result = res.ToJson();

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
        #endregion 

        #region 1天内每个小时
        [WebGet(UriTemplate = "CallTypeHour?parms={strParams}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<int> CallTypeHour(string strParams)
        {
            CollectionInvokeResult<int> result = new CollectionInvokeResult<int> { Success = true };

            try
            { 
                var dictionary = new StringObjectDictionary().MixInJson(strParams);
                var checkInTime = "";
                var param = "";
                foreach (var item in dictionary)
                {

                    if (item.Key.Equals("CheckInTime") && item.Value.ToString() != "")
                    {
                        checkInTime = item.Value.ToString();
                        checkInTime = Convert.ToDateTime(checkInTime).ToString("yyyy-MM-dd");
                    }
                }
                long  span = MvcHtmlHelperExtension.DateDiff(MvcHtmlHelperExtension.DateInterval.DD, Convert.ToDateTime(checkInTime), DateTime.Now);
                if ( span> 0)
                {
                    param = "select value1,value2,value3,value4,value5,value6,value7,value8,value9,value10,value11,value12,value13,value14,value15,value16,value17,value18,value19,value20,value21,value22,value23,value24 from oca_call_service_stat_report where dates='" + checkInTime + "'";
                    var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param).SingleOrDefault();
                    result.rows = res.Select(item => Convert.ToInt32(item.Value)).ToList();
                }
                else if (span == 0)
                {
                    List<string> fields = new List<string>();
                    int currentHour = DateTime.Now.Hour;
                    int currentMin = DateTime.Now.Minute;
                    int i;
                    for (i = 0; i < currentHour; i++)
                    {
                        fields.Add("value" + (i + 1).ToString());
                    }
                    fields.Add("floor(" + currentMin + ".0/60" + "*value" + (i + 1).ToString() + ") as value" + (i + 1).ToString());
                    i++;
                    for (; i < 24; i++)
                    {
                        fields.Add(" 0 as value" + (i + 1).ToString());
                    }
                    string sql = "select " + String.Join(",", fields.ToArray()) + " from oca_call_service_stat_report where dates='" + checkInTime + "'";
                    var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sql).SingleOrDefault();
                    result.rows = res.Select(item => Convert.ToInt32(item.Value)).ToList();
                }
                else
                {
                    result.rows = new List<int> { };
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        #endregion

        #region 1天内每个小时方法2
        [WebGet(UriTemplate = "CallTypeHourTwo", ResponseFormat = WebMessageFormat.Json)]
        public IList<StatHour> CallTypeHourTwo()
        {
            var sql = "select x.xAlias,x.yAlias from (SELECT  CONVERT(datetime, Convert(varchar(10),dates,120)+' '+substring(xAlias,1,5)+':00',120) as [From] ,Convert(datetime,Convert(varchar(10),dates,120)+' '+substring(xAlias,7,5)+':59',120) as [To],Convert(varchar(10),dates,120)+' '+xAlias as xAlias, yAlias FROM (SELECT dates, value1 as [00:00-00:59],value2 as [01:00-01:59],value3 as [02:00-02:59],value4 as [03:00-03:59],value5 as [04:00-04:59],value6 as [05:00-05:59],value7 as [06:00-06:59],value8  as [07:00-07:59],value9 as [08:00-08:59],value10 as [09:00-09:59],value11 as [10:00-10:59],value12 as [11:00-11:59],value13 as [12:00-12:59],value14 as [13:00-13:59],value15 as [14:00-14:59],value16 as [15:00-15:59],value17 as [16:00-16:59],value18 as [17:00-17:59],value19 as [18:00-18:59],value20 as [19:00-19:59],value21 as [20:00-20:59],value22 as [21:00-21:59],value23 as [22:00-22:59],value24 as [23:00-23:59] FROM oca_call_service_stat_report) p UNPIVOT(yAlias FOR xAlias IN ( [00:00-00:59],[01:00-01:59],[02:00-02:59],[03:00-03:59],[04:00-04:59],[05:00-05:59],[06:00-06:59],[07:00-07:59],[08:00-08:59],[09:00-09:59],[10:00-10:59],[11:00-11:59],[12:00-12:59],[13:00-13:59],[14:00-14:59],[15:00-15:59],[16:00-16:59],[17:00-17:59],[18:00-18:59],[19:00-19:59],[20:00-20:59],[21:00-21:59],[22:00-22:59],[23:00-23:59]))AS unpvt)x where x.[From]>= DateAdd(D,-1,GETDATE()) and  DateAdd(hh,1,GETDATE()) > x.[To]  ";
            return BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sql).Select(item => new StatHour { xAlias = item["xAlias"].ToString(), yAlias = Convert.ToInt32(item["yAlias"]) }).ToList();
        }


        #endregion

        #region 统计一天每小时的呼叫量
        [WebGet(UriTemplate = "GetDayPerHour?parms={strParams}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<int> GetDayPerHour(string strParams)
        {
            CollectionInvokeResult<int> result = new CollectionInvokeResult<int>() { Success=true};
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParams);
                var checkInTime = "";
                var param = "";
                foreach (var item in dictionary)
                {

                    if (item.Key.Equals("CheckInTime") && item.Value.ToString() != "")
                    {
                        checkInTime = item.Value.ToString();
                        checkInTime = Convert.ToDateTime(checkInTime).ToString("yyyy-MM-dd");
                    }
                }
                param = "select SUM(case when DATEPART(hour,CheckInTime)=0 then 1 else 0 end)item0,SUM(case when DATEPART(hour,CheckInTime)=1 then 1 else 0 end)item1,SUM(case when DATEPART(hour,CheckInTime)=2 then 1 else 0 end)item2,SUM(case when DATEPART(hour,CheckInTime)=3 then 1 else 0 end)item3,SUM(case when DATEPART(hour,CheckInTime)=4 then 1 else 0 end)item4,SUM(case when DATEPART(hour,CheckInTime)=5 then 1 else 0 end)item5,SUM(case when DATEPART(hour,CheckInTime)=6 then 1 else 0 end)item6,SUM(case when DATEPART(hour,CheckInTime)=7 then 1 else 0 end)item7,SUM(case when DATEPART(hour,CheckInTime)=8 then 1 else 0 end)item8,SUM(case when DATEPART(hour,CheckInTime)=9 then 1 else 0 end)item9,SUM(case when DATEPART(hour,CheckInTime)=10 then 1 else 0 end)item10,SUM(case when DATEPART(hour,CheckInTime)=11 then 1 else 0 end)item11,SUM(case when DATEPART(hour,CheckInTime)=12 then 1 else 0 end)item12,SUM(case when DATEPART(hour,CheckInTime)=13 then 1 else 0 end)item13,SUM(case when DATEPART(hour,CheckInTime)=14 then 1 else 0 end)item14,SUM(case when DATEPART(hour,CheckInTime)=15 then 1 else 0 end)item15,SUM(case when DATEPART(hour,CheckInTime)=16 then 1 else 0 end)item16,SUM(case when DATEPART(hour,CheckInTime)=17 then 1 else 0 end)item17,SUM(case when DATEPART(hour,CheckInTime)=18 then 1 else 0 end)item18,SUM(case when DATEPART(hour,CheckInTime)=19 then 1 else 0 end)item19,SUM(case when DATEPART(hour,CheckInTime)=20 then 1 else 0 end)item20,SUM(case when DATEPART(hour,CheckInTime)=21 then 1 else 0 end)item21,SUM(case when DATEPART(hour,CheckInTime)=22 then 1 else 0 end)item22,SUM(case when DATEPART(hour,CheckInTime)=23 then 1 else 0 end)item23 from Oca_CallService where CONVERT(varchar(10),CheckInTime,120)='" + checkInTime + "'";
                var res = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param).SingleOrDefault();
                result.rows = res.Select(item => Convert.ToInt32(item.Value)).ToList();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 1个小时每5分钟
        [WebGet(UriTemplate = "CallTypeMinute", ResponseFormat = WebMessageFormat.Json)]
        public IList<StatHour> CallTypeMinute()
        {
                var sql = "exec SP_Stc_CallsForHour";
                return BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sql).Select(item => new StatHour { xAlias = item["items"].ToString(), yAlias = Convert.ToInt32(item["times"]) }).ToList();
        }
        #endregion

        #region 已服务老人
        [WebGet(UriTemplate = "GetServiceOldMan", ResponseFormat = WebMessageFormat.Json)]
        public string GetServiceOldMan()
        {
            string serviceOldMan=null;
            try
            {
                serviceOldMan = BuilderFactory.DefaultBulder().List<OldManBaseInfo>(new OldManBaseInfo { Status = 1 }).ToList().Count.ToString();
            }
            catch (Exception ex)
            {
                serviceOldMan = ex.Message;
            }
            return serviceOldMan;
        }
        #endregion

        #region 已发放终端
        [WebGet(UriTemplate = "GetPaymentTerminal", ResponseFormat = WebMessageFormat.Json)]
        public string GetPaymentTerminal()
        {
            string paymentTerminal = null;
            try
            {
               paymentTerminal = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery("select * from Oca_OldManBaseInfo where Status=1 and OldManId in (select OldManId from Oca_OldManConfigInfo where len(CallNo)>0)").ToList().Count.ToString();
            }
            catch (Exception ex)
            {
                paymentTerminal = ex.Message;
            }
            return paymentTerminal;
        }
        #endregion

        //#region 今日累计呼叫
        //[WebGet(UriTemplate = "GetAccumulatedCall", ResponseFormat = WebMessageFormat.Json)]
        //public string GetAccumulatedCall()
        //{
        //    string accumulatedCall = null;
        //    try
        //    {
        //        accumulatedCall = BuilderFactory.DefaultBulder().List<CallService>(new CallService { Status = 1, CheckInTime = DateTime.Now }).ToList().Count.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        accumulatedCall = ex.Message;
        //    }
        //    return accumulatedCall;
        //}
        //#endregion

        #region 饼状图2
        [WebGet(UriTemplate = "GetServiceTypePie?parms={strParams}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetServiceTypePie(string strParams)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary>() { Success = true };

            try
            {

                var dictionary = new StringObjectDictionary().MixInJson(strParams);
                var checkInTime = "";
                var param = "";
                foreach (var item in dictionary)
                {

                    if (item.Key.Equals("CheckInTime") && item.Value.ToString() != "")
                    {
                        checkInTime = item.Value.ToString();
                        checkInTime = Convert.ToDateTime(checkInTime).ToString("yyyy-MM-dd");
                    }
                }
                param = "select SUBSTRING(CONVERT(varchar(400),[ServiceQueueId]),30,1) serviceType  from Oca_CallService where CONVERT(varchar(10),CheckInTime,120)='" + checkInTime + "'";
                result.rows = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(param);
                

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;
            }

            return result;
        }
        #endregion

        #endregion
    }

    public class StatHour
    {
        public string xAlias { get; set; }
        public int yAlias { get; set; }
    }
}

