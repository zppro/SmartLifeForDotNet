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
using SmartLife.Share.Models.Dcc;

namespace SmartLife.DataExchange.Services
{
    [DayCareCenterAIOTokenValidate()]
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DayCareCenterService : BaseWcfService
    {
        #region 一体机设备签到
        [WebGet(UriTemplate = "DayCareObject4AIO", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<GetResidentAsJSONRet> GetDayCareObject4AIO()
        {
            InvokeResult<GetResidentAsJSONRet> result = new InvokeResult<GetResidentAsJSONRet>() { Success = true };
            try
            {
                string aioNo = GetHttpHeader("AIONo");//设备Mac地址
                string dccNo = GetHttpHeader("DCCNo");//日照中心编码
                SPParam theSPParam = new { AIONo = aioNo, DCCNo = dccNo }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Dcc_AIOCheckIn", theSPParam);
                if (theSPParam.ErrorCode == 0)
                {
                    result.ret = new GetResidentAsJSONRet();
                    result.ret.rows = BuilderFactory.DefaultBulder().ListUDT<DayCareObject4AIO>("DayCareObject4AIO_ListByDCCNo", new { DCCNo = dccNo });
                }
                else
                {
                    result.Success = false;
                    result.ErrorCode = theSPParam.ErrorCode;
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

        #region 老人注册
        [WebInvoke(UriTemplate = "DayCareObjectRegister", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DayCareObjectRegister(DayCareObjectRegisterParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                SPParam theSPParam = new { DCCNo = GetHttpHeader("DCCNo"), IDNo = param.IDNo }.ToSPParam();

                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Dcc_DayCareObjectRegister", theSPParam);
                if (theSPParam.ErrorCode != 0)
                {
                    result.Success = false;
                    result.ErrorCode = theSPParam.ErrorCode;
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

        #region 老人签到（上报健康数据）
        [WebInvoke(UriTemplate = "DayCareObjectCheckIn", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DayCareObjectCheckIn(DayCareObjectCheckInParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                SPParam theSPParam = param.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Dcc_DayCareObjectCheckIn", theSPParam);
                if (theSPParam.ErrorCode != 0)
                {
                    result.Success = false;
                    result.ErrorCode = theSPParam.ErrorCode;
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

        #region 设备报警
        [WebInvoke(UriTemplate = "DayCareDeviceAlarm/{strIDNo}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DayCareDeviceAlarm(string strIDNo)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                SPParam theSPParam = new { DeviceNo = GetHttpHeader("DCCNo"), IDNo = strIDNo }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Dcc_DeviceAlarm", theSPParam);
                if (theSPParam.ErrorCode != 0)
                {
                    result.Success = false;
                    result.ErrorCode = theSPParam.ErrorCode;
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

        #region 获取老人基本数据
        [WebGet(UriTemplate = "DayCareObjectList", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<DayCareObjectRet> DayCareObjectList()
        {
            CollectionInvokeResult<DayCareObjectRet> result = new CollectionInvokeResult<DayCareObjectRet> { Success = true };
            try
            {
                string strSql = "select b.IDNo,b.OldManName,b.Gender from Pub_Area a inner join " +
                                " Oca_OldManBaseInfo b on CAST(a.AreaId as varchar(40)) = b.AreaId3 and b.Status=1 " +
                                " where a.AreaCode='" +  GetHttpHeader("DCCNo").Substring(1,11) + "' and  a.Status=1";
                result.rows = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql).Select(item => new DayCareObjectRet() { Gender = item["Gender"].ToString(), IDNo = item["IDNo"].ToString(), OldManName = item["OldManName"].ToString() }).ToList();
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

    #region 入参类
    public class DayCareObjectCheckInParam
    {
        public string IDNo { get; set; }
        public int DBP { get; set; }
        public int SBP { get; set; }
        public int MBP { get; set; }
        public int HR { get; set; }
    }
    public class DayCareObjectRegisterParam
    {
        public string IDNo { get; set; } 
    }
    #endregion


    #region 返回结果类

    public class GetResidentAsJSONRet
    {
        public IList<DayCareObject4AIO> rows { get; set; }
    } 

    public class DayCareObjectRet{
        public string IDNo {get;set;}
        public string OldManName {get;set;}
        public string Gender {get;set;}
    }

    #endregion
}