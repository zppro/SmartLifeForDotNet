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
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub;

namespace SmartLife.City.ManageServices.AppShare
{
    //[ServiceAuthorization(Salt = "[codans]", ValidateKey = "AirMenu1.0")]
    [ServiceLogging]
    [ServiceValidateSession]
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CommonService : BaseWcfService
    {
        [WebGet(UriTemplate = "getSession", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<SessionInfo> getSession()
        {
            InvokeResult<SessionInfo> result = new InvokeResult<SessionInfo> { Success = true, ret = new SessionInfo() };
            result.ret.UserName = NormalSession.UserName;
            result.ret.CompanyId = NormalSession.UserId;
            return result;
        }

        [WebInvoke(UriTemplate = "ClearUploadedFiles", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<ClearUploadedFilesRet> ClearUploadedFiles(ClearUploadedFilesParam param)
        {
            InvokeResult<ClearUploadedFilesRet> result = new InvokeResult<ClearUploadedFilesRet>() { Success = true, ret = new ClearUploadedFilesRet() };
            List<string> successFilePaths = new List<string>();
            try
            {
                foreach (var filePath in param.FilePaths)
                {
                    string uploadPathDecode = HttpContext.Current.Server.UrlDecode(filePath);
                    string uploadPath = HttpContext.Current.Server.MapPath(uploadPathDecode);
                    FileAdapter.Delete(uploadPath);
                    successFilePaths.Add(uploadPathDecode);
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            result.ret.SuccessFilePaths = successFilePaths;
            return result;
        }

        #region 坐席分机 添加绑定
        [WebInvoke(UriTemplate = "AddSeatExtBinding", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult AddSeatExtBinding(AddSeatExtBindingParam param)
        {
            InvokeResult result = new InvokeResult() { Success = true };
            try
            {
                SPParam theSPParam = new { UserId = Guid.Parse(NormalSession.UserId), ExtId = param.ExtId }.ToSPParam();
                BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPNoneQuery("SP_Pub_AddSeatExtBinding", theSPParam);

                if (theSPParam.ErrorCode != 0)
                {
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

        #region 坐席分机 删除绑定
        [WebInvoke(UriTemplate = "RemoveSeatExtBinding", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult RemoveSeatExtBinding()
        {
            InvokeResult result = new InvokeResult() { Success = true };
            try
            {
                SPParam theSPParam = new { UserId = Guid.Parse(NormalSession.UserId) }.ToSPParam();
                BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPNoneQuery("SP_Pub_RemoveSeatExtBinding", theSPParam);

                if (theSPParam.ErrorCode != 0)
                {
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
    }

    #region 入参类
    public class ClearUploadedFilesParam
    {
        public IList<string> FilePaths { get; set; }
    }
    public class AddSeatExtBindingParam
    { 
        public Guid? ExtId { get; set; }
    }
     
    #endregion

    #region 返回结果类

    #region CMS管理身份
    public class SessionInfo
    {
        public string CompanyId { get; set; }
        public string UserName { get; set; }
    }
    #endregion

    public class ClearUploadedFilesRet
    {
        public IList<string> SuccessFilePaths { get; set; }
    }

    #endregion
}