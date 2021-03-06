﻿using System;
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
using SmartLife.Share.Models.Auth.MerchantServices;
using e0571.web.core.Security;
using SmartLife.Share.Behaviors;

namespace SmartLife.Auth.Mobile.Services
{
    [MerchantServicesValidate(ApplicationIdFrom = "MM101", ApplicationIdTo = "IG003")]
    //[ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class V1MerchantService4IPhone : V1MerchantServiceInner
    {

        #region 会员认证
        [WebInvoke(UriTemplate = "AuthenticateMerchant", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<AuthMerchantRet> AuthenticateMerchant(AuthMerchantParam param)
        {
            return base.AuthenticateMerchantInner(param);
        }
        #endregion

    }

    [MerchantServicesValidate(ApplicationIdFrom = "MM301", ApplicationIdTo = "IG003")]
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class V1MerchantService4Android : V1MerchantServiceInner
    {

        #region 会员认证
        [WebInvoke(UriTemplate = "AuthenticateMerchant", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<AuthMerchantRet> AuthenticateMerchant(AuthMerchantParam param)
        {
            return base.AuthenticateMerchantInner(param);
        }
        #endregion

    }

    public class V1MerchantServiceInner : BaseWcfService
    {

        #region 商家认证内部
        
        public InvokeResult<AuthMerchantRet> AuthenticateMerchantInner(AuthMerchantParam param)
        {
            InvokeResult<AuthMerchantRet> result = new InvokeResult<AuthMerchantRet>() { Success = true, ret = new AuthMerchantRet() };
            try
            {
                string applicationIdFrom = GetHttpHeader("ApplicationId");
                SPParam theSPParam = new { StationCode = param.StationCode, PasswordHash = param.PasswordHash, ApplicationIdFrom = applicationIdFrom, ApplicationIdTo = GlobalManager.SmartLife_CertManage_MerchantServices }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Auth_Merchant", theSPParam);
                if (theSPParam.ErrorCode == 0)
                {
                    result.ret.Token = TokenProvider.GenTokenDynamic(param.StationCode, GlobalManager.SALT_MERCHANT);
                    result.ret.StationName = theSPParam["StationName"].ToString();
                    string strNodeInfos = TypeConverter.ChangeString(theSPParam["NodeInfos"]);

                    if (strNodeInfos != "")
                    {
                        result.ret.AuthNodeInfos = new List<AuthNodeInfo> { };
                        string[] arrNodeInfos = strNodeInfos.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        foreach (string strNodeInfo in arrNodeInfos)
                        {
                            string[] arrNodeInfo = strNodeInfo.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            AuthNodeInfo nodeInfo = new AuthNodeInfo { StationId = Guid.Parse(arrNodeInfo[0]), AccessPoint = arrNodeInfo[1] };
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