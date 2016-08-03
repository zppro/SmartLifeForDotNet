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
using SmartLife.Share.Behaviors.Services;
using e0571.web.core.Security;
using SmartLife.Share.Behaviors;
using e0571.web.core.DataAccess;
using SmartLife.Share.Models.Pam; 

namespace SmartLife.DataExchange.Services
{
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class PamService : BaseWcfService
    {
        #region 获取机构每日工作提醒记录
        [WebInvoke(UriTemplate = "LoadRemindersWorkExecute", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CollectionInvokeResult<StringObjectDictionary> LoadRemindersWorkExecute()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            InvokeResult result1 = new InvokeResult { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("DailyWorkExecuteReminder");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        #endregion


        #region 更新机构每日工作提醒
        [WebInvoke(UriTemplate = "UpdateWorkExecute", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UpdateWorkExecute(WorkExecuteParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param != null && param.Ids.Count > 0)
                {
                    filters.Add("Ids", string.Join("','", param.Ids.Where(s => !string.IsNullOrEmpty(s)).ToArray()));
                    BuilderFactory.DefaultBulder().ExecuteScalar("DailyReminder_UpdateWorkExecute", filters);
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


        #region 日照机构共同部分



        #region 桔豆机顶盒

        #region 绑定/解绑
        [WebInvoke(UriTemplate = "PADeviceBindingForJuDou", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult PADeviceBindingForJuDou(PADeviceBindingParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
               
                SPParam theSPParam = param.ToSPParam();
                theSPParam["DeviceType"] = GlobalManager.DIKey_00010_桔豆盒子;
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_DeviceBinding", theSPParam);

                if (theSPParam.ErrorCode != 0)
                {
                    result.Success = false;
                    result.ErrorCode = theSPParam.ErrorCode;
                    result.ErrorMessage = theSPParam.ErrorMessage;
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

        #region 自助服务终端
         
        #region 绑定/解绑
        [WebInvoke(UriTemplate = "PADeviceBindingForSelfServiceMachine", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult PADeviceBindingForSelfServiceMachine(PADeviceBindingParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {

                SPParam theSPParam = param.ToSPParam();
                theSPParam["DeviceType"] = GlobalManager.DIKey_00010_自助服务机;
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_DeviceBinding", theSPParam);

                if (theSPParam.ErrorCode != 0)
                {
                    result.Success = false;
                    result.ErrorCode = theSPParam.ErrorCode;
                    result.ErrorMessage = theSPParam.ErrorMessage;
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

        #region 跟老人相关

        #region 获取机构信息
        [WebGet(UriTemplate = "GetPAStationInfo", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<StringObjectDictionary> GetPAStationInfo()
        {
            InvokeResult<StringObjectDictionary> result = new InvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                filters["StationCode"] = GetHttpHeader("PACode");
                result.ret = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetPAStationInfo", filters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        #endregion

        #region 根据机构编码获取老人
        [WebGet(UriTemplate = "GetOldManInfoForSelfServiceMachine", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetOldManInfoForSelfServiceMachine()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                filters["StationCode"] = GetHttpHeader("PACode");
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetOldManInfoForStationByStationCode", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 为老人分配IC卡
        [WebInvoke(UriTemplate = "PAMakeCard", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult PAMakeCard(PAMakeCardParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            { 
                SPParam theSPParam = param.ToSPParam();
                theSPParam["PACode"] = GetHttpHeader("PACode");
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_MakeCard", theSPParam);

                if (theSPParam.ErrorCode != 0)
                {
                    result.Success = false;
                    result.ErrorCode = theSPParam.ErrorCode;
                    result.ErrorMessage = theSPParam.ErrorMessage;
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

        #region 获取老人当天的配餐信息
        [WebGet(UriTemplate = "GetOldManBookMealForToday", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetOldManBookMealForToday()
        { 
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary(); 
                filters["StationCode"] = GetHttpHeader("PACode");
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetOldManBookMealForTodayByOldManId", filters);                 
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        #endregion

        #region 领取配餐
        [WebInvoke(UriTemplate = "PAFetchBookMeal", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult PAFetchBookMeal(PAFetchBookMealParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                SPParam theSPParam = param.ToSPParam();
                theSPParam["PACode"] = GetHttpHeader("PACode");
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_FetchBookMeal", theSPParam);

                if (theSPParam.ErrorCode != 0)
                {
                    result.Success = false;
                    result.ErrorCode = theSPParam.ErrorCode;
                    result.ErrorMessage = theSPParam.ErrorMessage;
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

        #region 更改配餐未领取
        [WebInvoke(UriTemplate = "PAReuseBookMeal", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult PAReuseBookMeal(PAFetchBookMealParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                SPParam theSPParam = param.ToSPParam();
                theSPParam["PACode"] = GetHttpHeader("PACode");
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_ReuseBookMeal", theSPParam);

                if (theSPParam.ErrorCode != 0)
                {
                    result.Success = false;
                    result.ErrorCode = theSPParam.ErrorCode;
                    result.ErrorMessage = theSPParam.ErrorMessage;
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
         
        #region 获取日照课程信息
        [WebGet(UriTemplate = "LoadStationCourseList", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadStationCourseList()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };

            try
            {
                string strStationCode = GetHttpHeader("PACode");
                if (!string.IsNullOrEmpty(strStationCode))
                {
                    StringObjectDictionary filters = new { StationCode = strStationCode }.ToStringObjectDictionary();
                    result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("CourseListByStationCode", filters);
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "机构编码不存在!";
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


       

        #endregion
    }


    public class WorkExecuteParam {
        public IList<string> Ids { get; set; }
    }

    public class PADeviceBindingParam
    {
        public string PACode { get; set; }
        public string MachineKey { get; set; }
        public string Action { get; set; }
    }

    public class PAMakeCardParam
    { 
        public Guid? OldManId { get; set; }
        public Guid? OperatedBy { get; set; }
        public string ICNo { get; set; }
    }

    public class PAFetchBookMealParam
    {
        public Guid? OldManId { get; set; }
        public string MealType { get; set; }
        public Guid? SetMealId { get; set; }
        public Guid? OperatedBy { get; set; }
    }
}