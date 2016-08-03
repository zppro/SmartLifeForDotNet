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
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Models.Cer;
using e0571.web.core.Security;
using SmartLife.Share.Behaviors;


namespace SmartLife.DataExchange.Services.P12.C03
{
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class A04 : BaseWcfService
    {

        [WebInvoke(UriTemplate = "Resident.JSON", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<A04ParamRet> SyncInResidentJSON(A04Param param)
        {
            InvokeResult<A04ParamRet> result = new InvokeResult<A04ParamRet>() { Success = true };
            string token2 = TokenProvider.GenTokenDynamic("东软社区管理系统", GlobalManager.SmartLife_DataExchange_Services);
            if (token2.EndsWith("="))
            {
                token2 = token2.Substring(0, token2.Length - 1);
            }
            if (param.token != token2)
            {
                result.Success = false;
                result.ErrorCode = 50001;
                result.ErrorMessage = "token被篡改";
            }
            else
            {
                if (param.Total > 0)
                {
                    result.ret = new A04ParamRet { failedIDNo = new List<string>() };

                    try
                    {
                        foreach (var row in param.Rows)
                        { 
                            SPParam theSPParam = row.ToSPParam();
                            theSPParam["AreaId"] = Global.AreaId;
                            theSPParam["DataSourceToken"] = param.token;
                            theSPParam["DataSourceOn"] = param.time;
                            BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Inc_SyncInResident", theSPParam);
                            if (theSPParam.ErrorCode == 0)
                            {
                                //成功
                            }
                            else
                            {
                                //失败
                                result.Success = false;
                                result.ErrorCode = Convert.ToInt32(theSPParam["ErrorCode"]);
                                result.ret.failedIDNo.Add(row.idcardnum);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.ErrorMessage = ex.Message;
                    }
                }
            }
            return result;
        }

        [WebGet(UriTemplate = "GetToken", ResponseFormat = WebMessageFormat.Json)]
        public string GetToken()
        {
            return TokenProvider.GenTokenDynamic("东软社区管理系统", GlobalManager.SmartLife_DataExchange_Services);
        }
    }

    #region 入参类
    
     

    public class A04Param
    {
        public string token { get; set; }
        public int Total { get; set; }
        public IList<A04_Resident> Rows { get; set; }
        public string time { get; set; }
    }

    public class A04_Resident
    {
        public string Area1 { get; set; }
        public string Area2 { get; set; }
        public string Area3 { get; set; }
        public string name { get; set; }//ResidentName
        public string sex { get; set; }//GenderName
        public string birthday { get; set; }//Birthday
        public string idcardnum { get; set; }//IDNo
        public string liveaddress { get; set; }//Address
        public string PostCode { get; set; }//PostCode
        public string familyphone { get; set; }//Tel
        public string mobilephone { get; set; }//Mobile
        public string Remark { get; set; }//Remark
        public string relation { get; set; }//RelationWithFamilyHead
        public string familyid { get; set; }//FamilyAccountId
    }
    #endregion


    #region 返回结果类

    public class A04ParamRet
    {
        public IList<string> failedIDNo { get; set; }
    }

    #endregion
}