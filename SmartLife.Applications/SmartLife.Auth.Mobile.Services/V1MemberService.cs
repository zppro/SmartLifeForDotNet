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
using SmartLife.Share.Models.Auth;
using SmartLife.Share.Models.Auth.MobileServices;
using e0571.web.core.Security;
using SmartLife.Share.Behaviors;

namespace SmartLife.Auth.Mobile.Services
{
    [MobileServicesValidate(ApplicationIdFrom = "MM101", ApplicationIdTo = "IG003")]
    //[ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class V1MemberService4IPhone : V1MemberServiceInner
    {

        #region 会员认证
        [WebInvoke(UriTemplate = "AuthenticateMember", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<AuthMemberRet> AuthenticateMember(AuthMemberParam param)
        {
            return AuthenticateMemberInner(param);
        }
        #endregion

    }

    [MobileServicesValidate(ApplicationIdFrom = "MM301", ApplicationIdTo = "IG003")]
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class V1MemberService4Android : V1MemberServiceInner
    {

        #region 会员认证
        [WebInvoke(UriTemplate = "AuthenticateMember", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<AuthMemberRet> AuthenticateMember(AuthMemberParam param)
        {
            return AuthenticateMemberInner(param);
        }
        #endregion

    }
     
    public class V1MemberServiceInner : BaseWcfService
    { 
        #region 会员认证 内部
        public InvokeResult<AuthMemberRet> AuthenticateMemberInner(AuthMemberParam param)
        {
            InvokeResult<AuthMemberRet> result = new InvokeResult<AuthMemberRet>() { Success = true, ret = new AuthMemberRet() };
            try
            {
                string applicationIdFrom = GetHttpHeader("ApplicationId");
                SPParam theSPParam = new { IDNo = param.IDNo, PasswordHash = param.PasswordHash, ApplicationIdFrom = applicationIdFrom, ApplicationIdTo = GlobalManager.SmartLife_CertManage_MobileServices }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Auth_Member", theSPParam);
                if (theSPParam.ErrorCode == 0)
                {
                    string memberId = theSPParam["MemberId"].ToString();
                    result.ret.Token = TokenProvider.GenTokenDynamic(memberId, GlobalManager.SALT_MOBILE);
                    result.ret.MemberId = Guid.Parse(memberId);
                    result.ret.MemberName = theSPParam["MemberName"].ToString();
                    string strNodeInfos = TypeConverter.ChangeString(theSPParam["NodeInfos"]);
                    
                    if (strNodeInfos != "")
                    {
                        result.ret.AuthNodeInfos = new List<AuthNodeInfo> { };
                        string[] arrNodeInfos = strNodeInfos.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        foreach (string strNodeInfo in arrNodeInfos)
                        {
                            string[] arrNodeInfo = strNodeInfo.Split(",".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
                            AuthNodeInfo nodeInfo = new AuthNodeInfo { FamilyMemberId = Guid.Parse(arrNodeInfo[0]), AccessPoint = arrNodeInfo[1] };
                            result.ret.AuthNodeInfos.Add(nodeInfo);
                        }
                        
                    }
                }
                else
                {
                    result.Success = false;
                    result.ErrorCode = Convert.ToInt32(theSPParam["ErrorCode"]);
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

     

}