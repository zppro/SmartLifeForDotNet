using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using e0571.web.core.Wcf;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.Model;
using SmartLife.Share.Models.Oca;
using SmartLife.Share.Models.Eva;
using SmartLife.Share.Behaviors;
using Newtonsoft.Json;
using SmartLife.Share.Models.Pub;
using System.IO;
using System.Text;

namespace SmartLife.CertManage.ManageServices.AppShare
{
    /// <summary>
    /// ExportExcel 的摘要说明
    /// </summary>
    public class ExportExcel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;

            string strExportType = context.Request.Form["ExportType"].ToString();
            dynamic param = JsonConvert.DeserializeObject(context.Request.Form["ExportData"]);

            switch (strExportType)
            {
                case "ExportFamilyMember":
                    ExportFamilyMember(context, param);
                    break;
                case "ExportOldManBaseInfo":
                    ExportOldManBaseInfo(context, param);
                    break;
                case "ExportAssessmentNotice":
                    ExportAssessmentNotice(context, param);
                    break;
                default:
                    context.Response.Write("");
                    context.Response.End();
                    break;
            }

            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        private void ExportFamilyMember(HttpContext context, dynamic param)
        {
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    foreach (var field in param.instance)
                    {
                        filters.Add(field.Name, (string)field.Value);
                    }
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
                /**********************************************************/
                if (!string.IsNullOrEmpty((string)param.sort))
                {
                    filters.Add("OrderByClause", (string)param.sort + " " + (string)param.order ?? "ASC");
                }
                /***********************end 排序***************************/
                IList<FamilyMember> familyMemberList = BuilderFactory.DefaultBulder().List<FamilyMember>(filters);
                //System.Data.DataTable dt = BuilderFactory.DefaultBulder().QueryForDataTable("FamilyMember_List", filters);

                IList<string> colNames = new List<string>();
                colNames.Add("姓名");
                colNames.Add("身份证");
                colNames.Add("性别");
                colNames.Add("操作时间");
                colNames.Add("座机");
                colNames.Add("手机");
                colNames.Add("地址");
                IList<string> colKeys = new List<string>();
                colKeys.Add("FamilyMemberName");
                colKeys.Add("IDNo");
                colKeys.Add("Gender");
                colKeys.Add("OperatedOn");
                colKeys.Add("Tel");
                colKeys.Add("Mobile");
                colKeys.Add("Address");
                //NPOIManager.RenderToBrowserByDataSet(context, dt.DataSet, 0, "ddss.xls", "你知不知道", colNames, colKeys, false);
                NPOIManager.RenderToBrowserByList<FamilyMember>(context, familyMemberList, 0, "ddss.xls", "", colNames, colKeys, false);
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.alertInfo('Excel导出失败!');</script>");
                context.Response.End();
            }
        }

        private void ExportOldManBaseInfo(HttpContext context, dynamic param)
        {
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    foreach (var field in param.instance)
                    {
                        filters.Add(field.Name, (string)field.Value);
                    }
                }
                List<string> whereClause = new List<string>();
                /**********************************************************/
                if (param.filterFields != null)
                { 
                    foreach (var field in param.filterFields)
                    {
                        if (field.key.Value.Contains("AreaId2_Start") && field.value.Value != "")
                        {
                            filters["AreaId2"] = field.value.Value;
                        }
                        else if (field.key.Value.Contains("AreaId3_Start") && field.value.Value != "")
                        {
                            filters["AreaId3"] = field.value.Value;
                        }
                        else
                        {
                            filters[field.key.Value] = field.value.Value;
                        }
                    }
                }
                /***********************begin 模糊查询*********************/
                if (param.fuzzyFields != null)
                {
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {

                        if (field.key.Value.Equals("AreaId") && field.value.Value != "")
                        {
                            whereClause.Add(string.Format("(AreaId3 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%') or AreaId2 in (select cast(AreaId as varchar (40)) from Pub_Area where {0} like '{1}%'))", field.key.Value, field.value.Value));
                        }
                        else if (field.key.Value.Equals("AreaId")  && field.value.Value == "")
                        {

                        }
                        else
                        {
                            fuzzys.Add(string.Format("{0} like '%{1}%'", field.key.Value, field.value.Value));
                        }
                    }
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP.Value) ? " AND " : " " + param.fuzzyFieldOP.Value + " "), fuzzys.ToArray()) + ")");
                    }
                }
                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                if (filters["isSuperAdmin"].ToString() == "False")
                {
                    string sql = PermissionsCategoryView(filters["UserId"].ToString());
                    whereClause.Add(sql);                
                } 
                /***********************end 自定义代码*********************/
                whereClause.Add(" a.IDNo not like '%*%' ");
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                if (!string.IsNullOrEmpty((string)param.sort.Value))
                {
                    filters.Add("OrderByClause", (string)param.sort.Value + " " + (string)param.order.Value ?? "ASC");
                }
                /***********************end 排序***************************/
                IList<StringObjectDictionary> oldManBaseInfoList = BuilderFactory.DefaultBulder().ListStringObjectDictionary("OldManBaseInfoForName", filters);

                //System.Data.DataTable dt = BuilderFactory.DefaultBulder().QueryForDataTable("FamilyMember_List", filters);

                IList<string> colNames = new List<string>();
                colNames.Add("姓名");
                colNames.Add("性别");
                colNames.Add("年龄");
                colNames.Add("身份证号码");
                colNames.Add("医保标志");
                colNames.Add("医保号码");
                colNames.Add("社保标志");
                colNames.Add("社保号码");
                colNames.Add("居住情况");
                colNames.Add("身份情况");
                colNames.Add("所属辖区");
                colNames.Add("所属街道");
                colNames.Add("所属社区");
                colNames.Add("地址");
                colNames.Add("经度");
                colNames.Add("纬度");
                colNames.Add("邮编");
                colNames.Add("电话");
                colNames.Add("手机");
                colNames.Add("备注");
                colNames.Add("呼叫号码1");
                colNames.Add("呼叫号码2");
                colNames.Add("呼叫号码3");
                colNames.Add("政府购买标志"); 
                IList<string> colKeys = new List<string>();
                colKeys.Add("OldManName");
                colKeys.Add("GenderName");
                colKeys.Add("Age");
                colKeys.Add("IDNo");
                colKeys.Add("HealthInsuranceFlagName");
                colKeys.Add("HealthInsuranceNumber");
                colKeys.Add("SocialInsuranceFlagName");
                colKeys.Add("SocialInsuranceNumber");
                colKeys.Add("LivingStatusName");
                colKeys.Add("OldManIdentityName");
                colKeys.Add("AreaIdName");
                colKeys.Add("AreaId2Name");
                colKeys.Add("AreaId3Name");
                colKeys.Add("Address");
                colKeys.Add("LongitudeS");
                colKeys.Add("LatitudeS");
                colKeys.Add("PostCode");
                colKeys.Add("Tel");
                colKeys.Add("Mobile");
                colKeys.Add("Remark");
                colKeys.Add("CallNo");
                colKeys.Add("CallNo2");
                colKeys.Add("CallNo3");
                colKeys.Add("GovTurnkeyFlagName"); 
                //NPOIManager.RenderToBrowserByDataSet(context, dt.DataSet, 0, "ddss.xls", "你知不知道", colNames, colKeys, false);
                NPOIManager.RenderToBrowserByListStringObjectDictionary(context, oldManBaseInfoList, 0, "老人基本信息" + DateTime.Now.ToString("yyyyMMddhh:mm:ss") + ".xls", "", colNames, colKeys, false);
                //NPOIManager.RenderToBrowserByList<StringObjectDictionary>(context, oldManBaseInfoList, 0, "ddss.xls", "", colNames, colKeys, false);
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.alertInfo('Excel导出失败!');</script>");
                context.Response.End();
            }
        }

        #region 权限查看不同内容
        public string PermissionsCategoryView(string userId)
        { 
            string sql = "";
            IList<UserArea> userAreas = BuilderFactory.DefaultBulder().List<UserArea>(new UserArea { UserId = userId.ToGuid() });
            if (userAreas.Count > 0)
            {
                var areaIdsOfStreet = string.Join("','", userAreas.Where(item => item.AreaId.ToString().Substring(14, 4) == "0000").Select(item => item.AreaId.ToString()));
                var areaIdsOfCommunity = string.Join("','", userAreas.Where(item => item.AreaId.ToString().Substring(14, 4) != "0000").Select(item => item.AreaId.ToString()));

                if (areaIdsOfStreet == "")
                {
                    sql = string.Format("( AreaId3 in ('{0}') or  (ISNULL(AreaId2,'')='' AND ISNULL(AreaId3,'')=''))", areaIdsOfCommunity);
                }
                else if (areaIdsOfCommunity == "")
                {
                    sql = string.Format("(AreaId2 in ('{0}') or  (ISNULL(AreaId2,'')='' AND ISNULL(AreaId3,'')=''))", areaIdsOfStreet);
                }
                else
                {
                    sql = string.Format("(AreaId2 in ('{0}') or AreaId3 in ('{1}') or (ISNULL(AreaId2,'')='' AND ISNULL(AreaId3,'')=''))", areaIdsOfStreet, areaIdsOfCommunity);
                }
            }
            return sql;
        }
        #endregion

        #region 导出评估告知书
        private void ExportAssessmentNotice(HttpContext context, dynamic param)
        {
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();

                List<string> whereClause = new List<string>();
                if (param.ReportIds != null)
                {
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.ReportIds)
                    {
                        fuzzys.Add(string.Format(" '{0}' ", field));
                    }

                    
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("ReportId in(" + string.Join(" , ", fuzzys.ToArray()) + ") ");
                    }
                    if (whereClause.Count > 0) {
                        filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                    }
                }
                
                /**********************************************************/
                /***********************begin 排序*************************/
                /**********************************************************/
                if (!string.IsNullOrEmpty((string)param.sort))
                {
                    filters.Add("OrderByClause", (string)param.sort + " " + (string)param.order ?? "ASC");
                }
                /***********************end 排序***************************/

                string strConnectId = (string)param.ConnectId;
                IList<EvaluatedReport> evaluatedReportList = BuilderFactory.DefaultBulder(strConnectId).List<EvaluatedReport>(filters);

                string xlsName = "告知书-" + DateTime.Now.ToString("yyyy年MM月dd日") + "-" + DateTime.Now.Millisecond.ToString() + ".xls";
                NPOIManager.CreateEvaluateNoticeExcel<EvaluatedReport>(context, evaluatedReportList, xlsName, "养老服务告知书", strConnectId);
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.alertInfo('Excel导出失败!');</script>");
                context.Response.End();
            }
        }
        #endregion
    }
}