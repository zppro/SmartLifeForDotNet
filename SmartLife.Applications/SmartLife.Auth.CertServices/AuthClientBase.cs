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
using e0571.web.core.Security;
using e0571.web.core.Service;
using SmartLife.Share.Behaviors;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub;
using SmartLife.Share.Models.Cer;
using SmartLife.Share.Models.Auth; 

namespace SmartLife.Auth.CertServices
{
    public class AuthClientBase : AppBaseWcfService
    {
        #region 用户认证实现
        protected void AuthenticateUser<T, V>(T param, InvokeResult<V> result)
            where T : AuthenticateClientParam
            where V : AuthenticateClientRet
        {
            string applicationId = GetHttpHeader(GlobalManager.ApplicationIdKey);

            try
            {
                string connectstring_Or_Dbname = Global.oldConnectString;
                //GlobalManager.outputLogger.Info(connectstring_Or_Dbname);
                SPParam theSPParam = new { ApplicationId = applicationId, ObjectId = param.ObjectId, UserCode = param.UserCode, PasswordHash = param.PasswordHash }.ToSPParam();
                
                if (applicationId == GlobalManager.SmartLife_Client_Gov)
                {
                    #region 政府监管客户端 
                    BuilderFactory.DefaultBulder(connectstring_Or_Dbname).ExecuteSPNoneQuery("SP_Auth_Gov", theSPParam);
                    if (theSPParam.ErrorCode == 0)
                    {
                        if (TypeConverter.ChangeString(theSPParam["UserId"]) != "")
                        {
                            result.ret.UserId = TypeConverter.ChangeString(theSPParam["UserId"]).ToGuid();
                        }
                        result.ret.UserName = TypeConverter.ChangeString(theSPParam["UserName"]);
                        result.ret.Area1 = TypeConverter.ChangeString(theSPParam["Area1"]);
                        result.ret.AreaCode1 = TypeConverter.ChangeString(theSPParam["AreaCode1"]);
                        result.ret.AreaName1 = TypeConverter.ChangeString(theSPParam["AreaName1"]);
                        if (TypeConverter.ChangeString(theSPParam["Area2"]) != "")
                        {
                            result.ret.Area2 = TypeConverter.ChangeString(theSPParam["Area2"]).ToGuid();
                            result.ret.AreaCode2 = TypeConverter.ChangeString(theSPParam["AreaCode2"]);
                            result.ret.AreaName2 = TypeConverter.ChangeString(theSPParam["AreaName2"]);
                        }
                        if (TypeConverter.ChangeString(theSPParam["Area3"]) != "")
                        {
                            result.ret.Area3 = TypeConverter.ChangeString(theSPParam["Area3"]).ToGuid();
                            result.ret.AreaCode3 = TypeConverter.ChangeString(theSPParam["AreaCode3"]);
                            result.ret.AreaName3 = TypeConverter.ChangeString(theSPParam["AreaName3"]);
                        }
                        result.ret.CityId = TypeConverter.ChangeString(theSPParam["CityId"]);
                        result.ret.CityCode = TypeConverter.ChangeString(theSPParam["CityCode"]);
                        result.ret.CityName = TypeConverter.ChangeString(theSPParam["CityName"]); 
                        string areaId = null;
                        string areaCode = null;
                        string proviceCode = null;
                        if (result.ret.Area1 != "")
                        {
                            areaId = result.ret.Area1;
                            areaCode = result.ret.AreaCode1;
                            proviceCode = areaCode.Substring(0, 2);
                        }
                        else
                        {
                            if (result.ret.CityId != "")
                            {
                                areaId = result.ret.CityId;
                                areaCode = result.ret.CityCode;
                                proviceCode = areaCode.Substring(0, 2);
                            }
                        }
                        if (!string.IsNullOrEmpty(areaId))
                        {
                            var accessNode = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_CertManage_ManageCMS, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                            if (accessNode != null)
                            {
                                result.ret.AccessPoint = accessNode.AccessPoint;
                            }
                            else
                            {
                                accessNode = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = GlobalManager.SmartLife_CertManage_ManageCMS, ApplicationIdTo = GlobalManager.SmartLife_CertManage_ManageServices, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                                result.ret.AccessPoint = accessNode.AccessPoint + "/../404.htm";
                            }

                            var dataNodeOfManage = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_CertManage_ManageServices, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                            if (dataNodeOfManage != null)
                            {
                                result.ret.DataPointOfManage = dataNodeOfManage.AccessPoint;
                            }

                            var accessNodeOfProvince = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = proviceCode, ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_Auth_ManageCMS, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                            if (accessNodeOfProvince != null)
                            {
                                result.ret.AccessOfProvice = accessNodeOfProvince.AccessPoint;
                            }
                            if (result.ret.Area3 != null)
                            {
                                SPParam area3Param = new { UserId = result.ret.UserId, AreaId = result.ret.Area3, ApplicationId = applicationId }.ToSPParam();
                                result.ret.AuthorizedModules = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPForQuery<Module>("SP_Auth_GetAuthorizedModules", area3Param);
                            }
                            else if (result.ret.Area2 != null)
                            {
                                SPParam area2Param = new { UserId = result.ret.UserId, AreaId = result.ret.Area2, ApplicationId = applicationId }.ToSPParam();
                                result.ret.AuthorizedModules = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPForQuery<Module>("SP_Auth_GetAuthorizedModules", area2Param);
                            }
                            else
                            {
                                SPParam area1Param = new { UserId = result.ret.UserId, AreaId = result.ret.Area1, ApplicationId = applicationId }.ToSPParam();
                                result.ret.AuthorizedModules = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPForQuery<Module>("SP_Auth_GetAuthorizedModules", area1Param);
                            }
                        }
                    }
                    else
                    {
                        //GlobalManager.outputLogger.Info(theSPParam.ErrorCode);
                        result.Success = false;
                        result.ErrorCode = theSPParam.ErrorCode;
                    }
                    #endregion
                }
                else if (applicationId == GlobalManager.SmartLife_Client_Merchant)
                {
                    #region 商家服务客户端
                    BuilderFactory.DefaultBulder(connectstring_Or_Dbname).ExecuteSPNoneQuery("SP_Auth_Mer", theSPParam);
                    if (theSPParam.ErrorCode == 0)
                    {
                        if (TypeConverter.ChangeString(theSPParam["UserId"]) != "")
                        {
                            result.ret.UserId = TypeConverter.ChangeString(theSPParam["UserId"]).ToGuid();
                        }
                        result.ret.UserName = TypeConverter.ChangeString(theSPParam["UserName"]);
                        result.ret.Area1 = TypeConverter.ChangeString(theSPParam["Area1"]);
                        result.ret.AreaCode1 = TypeConverter.ChangeString(theSPParam["AreaCode1"]);
                        result.ret.AreaName1 = TypeConverter.ChangeString(theSPParam["AreaName1"]);
                        if (TypeConverter.ChangeString(theSPParam["Area2"]) != "")
                        {
                            result.ret.Area2 = TypeConverter.ChangeString(theSPParam["Area2"]).ToGuid();
                            result.ret.AreaCode2 = TypeConverter.ChangeString(theSPParam["AreaCode2"]);
                            result.ret.AreaName2 = TypeConverter.ChangeString(theSPParam["AreaName2"]);
                        }
                        if (TypeConverter.ChangeString(theSPParam["Area3"]) != "")
                        {
                            result.ret.Area3 = TypeConverter.ChangeString(theSPParam["Area3"]).ToGuid();
                            result.ret.AreaCode3 = TypeConverter.ChangeString(theSPParam["AreaCode3"]);
                            result.ret.AreaName3 = TypeConverter.ChangeString(theSPParam["AreaName3"]);
                        }
                        result.ret.CityId = TypeConverter.ChangeString(theSPParam["CityId"]);
                        result.ret.CityCode = TypeConverter.ChangeString(theSPParam["CityCode"]);
                        result.ret.CityName = TypeConverter.ChangeString(theSPParam["CityName"]);
                        string areaId = null;
                        string areaCode = null;
                        string proviceCode = null;
                        if (result.ret.Area1 != "")
                        {
                            areaId = result.ret.Area1;
                            areaCode = result.ret.AreaCode1;
                            proviceCode = areaCode.Substring(0, 2);
                        }
                        else
                        {
                            if (result.ret.CityId != "")
                            {
                                areaId = result.ret.CityId;
                                areaCode = result.ret.CityCode;
                                proviceCode = areaCode.Substring(0, 2);
                            }
                        }
                        if (!string.IsNullOrEmpty(areaId))
                        {
                            var accessNode = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_CertManage_ManageCMS, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                            if (accessNode != null)
                            {
                                result.ret.AccessPoint = accessNode.AccessPoint;
                            }
                            else
                            {
                                accessNode = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = GlobalManager.SmartLife_CertManage_ManageCMS, ApplicationIdTo = GlobalManager.SmartLife_CertManage_ManageServices, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                                result.ret.AccessPoint = accessNode.AccessPoint + "/../404.htm";
                            }

                            var dataNodeOfManage = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_CertManage_ManageServices, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                            if (dataNodeOfManage != null)
                            {
                                result.ret.DataPointOfManage = dataNodeOfManage.AccessPoint;
                            }

                            //商家服务访问点
                            var dataNodeOfMerchant = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_CertManage_MerchantServices, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                            if (dataNodeOfMerchant != null)
                            {
                                (result.ret as AuthenticateClientMerRet).DataPointOfMerchant = dataNodeOfMerchant.AccessPoint;
                            }

                            var accessNodeOfProvince = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = proviceCode, ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_Auth_ManageCMS, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                            if (accessNodeOfProvince != null)
                            {
                                result.ret.AccessOfProvice = accessNodeOfProvince.AccessPoint;
                            }

                            if (result.ret.Area3 != null)
                            {
                                SPParam area3Param = new { UserId = result.ret.UserId, AreaId = result.ret.Area3, ApplicationId = applicationId }.ToSPParam();
                                result.ret.AuthorizedModules = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPForQuery<Module>("SP_Auth_GetAuthorizedModules", area3Param);
                            }
                            else if (result.ret.Area2 != null)
                            {
                                SPParam area2Param = new { UserId = result.ret.UserId, AreaId = result.ret.Area2, ApplicationId = applicationId }.ToSPParam();
                                result.ret.AuthorizedModules = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPForQuery<Module>("SP_Auth_GetAuthorizedModules", area2Param);
                            }
                            else
                            {
                                SPParam area1Param = new { UserId = result.ret.UserId, AreaId = result.ret.Area1, ApplicationId = applicationId }.ToSPParam();
                                result.ret.AuthorizedModules = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPForQuery<Module>("SP_Auth_GetAuthorizedModules", area1Param);
                            }

                            //result.ret.AuthorizedModules = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<Module>(new { ApplicationId = applicationId, WhereClause = "ModuleId in (Select ModuleId from Auth_AreaModule Where AreaId='" + result.ret.Area1 + "')", OrderByClause = "OrderNo asc" }.ToStringObjectDictionary());
                        }
                    }
                    else
                    {
                        //GlobalManager.outputLogger.Info(theSPParam.ErrorCode);
                        result.Success = false;
                        result.ErrorCode = theSPParam.ErrorCode;
                    }
                    #endregion
                }
                else if (applicationId == GlobalManager.SmartLife_Client_PensionAgency)
                {
                    #region 养老机构客户端 
                    BuilderFactory.DefaultBulder(connectstring_Or_Dbname).ExecuteSPNoneQuery("SP_Auth_PensionAgency", theSPParam);
                    if (theSPParam.ErrorCode == 0)
                    {
                        if (TypeConverter.ChangeString(theSPParam["UserId"]) != "")
                        {
                            result.ret.UserId = TypeConverter.ChangeString(theSPParam["UserId"]).ToGuid();
                        }
                        result.ret.UserName = TypeConverter.ChangeString(theSPParam["UserName"]);
                        result.ret.Area1 = TypeConverter.ChangeString(theSPParam["Area1"]);
                        result.ret.AreaCode1 = TypeConverter.ChangeString(theSPParam["AreaCode1"]);
                        result.ret.AreaName1 = TypeConverter.ChangeString(theSPParam["AreaName1"]);
                        if (TypeConverter.ChangeString(theSPParam["Area2"]) != "")
                        {
                            result.ret.Area2 = TypeConverter.ChangeString(theSPParam["Area2"]).ToGuid();
                            result.ret.AreaCode2 = TypeConverter.ChangeString(theSPParam["AreaCode2"]);
                            result.ret.AreaName2 = TypeConverter.ChangeString(theSPParam["AreaName2"]);
                        }
                        if (TypeConverter.ChangeString(theSPParam["Area3"]) != "")
                        {
                            result.ret.Area3 = TypeConverter.ChangeString(theSPParam["Area3"]).ToGuid();
                            result.ret.AreaCode3 = TypeConverter.ChangeString(theSPParam["AreaCode3"]);
                            result.ret.AreaName3 = TypeConverter.ChangeString(theSPParam["AreaName3"]);
                        }
                        result.ret.CityId = TypeConverter.ChangeString(theSPParam["CityId"]);
                        result.ret.CityCode = TypeConverter.ChangeString(theSPParam["CityCode"]);
                        result.ret.CityName = TypeConverter.ChangeString(theSPParam["CityName"]); 
                        string areaId = null;
                        string areaCode = null;
                        string proviceCode = null;
                        if (result.ret.Area1 != "")
                        {
                            areaId = result.ret.Area1;
                            areaCode = result.ret.AreaCode1;
                            proviceCode = areaCode.Substring(0, 2);
                        }
                        else
                        {
                            if (result.ret.CityId != "")
                            {
                                areaId = result.ret.CityId;
                                areaCode = result.ret.CityCode;
                                proviceCode = areaCode.Substring(0, 2);
                            }
                        }
                        if (!string.IsNullOrEmpty(areaId))
                        {
                            var accessNode = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_CertManage_ManageCMS, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                            if (accessNode != null)
                            {
                                result.ret.AccessPoint = accessNode.AccessPoint;
                            }
                            else
                            {
                                accessNode = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = GlobalManager.SmartLife_CertManage_ManageCMS, ApplicationIdTo = GlobalManager.SmartLife_CertManage_ManageServices, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                                result.ret.AccessPoint = accessNode.AccessPoint + "/../404.htm";
                            }

                            var dataNodeOfManage = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_CertManage_ManageServices, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                            if (dataNodeOfManage != null)
                            {
                                result.ret.DataPointOfManage = dataNodeOfManage.AccessPoint;
                            }

                            var accessNodeOfProvince = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = proviceCode, ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_Auth_ManageCMS, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                            if (accessNodeOfProvince != null)
                            {
                                result.ret.AccessOfProvice = accessNodeOfProvince.AccessPoint;
                            }
                            if (result.ret.Area3 != null)
                            {
                                SPParam area3Param = new { UserId = result.ret.UserId, AreaId = result.ret.Area3, ApplicationId = applicationId }.ToSPParam();
                                result.ret.AuthorizedModules = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPForQuery<Module>("SP_Auth_GetAuthorizedModules", area3Param);
                            }
                            else if (result.ret.Area2 != null)
                            {
                                SPParam area2Param = new { UserId = result.ret.UserId, AreaId = result.ret.Area2, ApplicationId = applicationId }.ToSPParam();
                                result.ret.AuthorizedModules = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPForQuery<Module>("SP_Auth_GetAuthorizedModules", area2Param);
                            }
                            else
                            {
                                SPParam area1Param = new { UserId = result.ret.UserId, AreaId = result.ret.Area1, ApplicationId = applicationId }.ToSPParam();
                                result.ret.AuthorizedModules = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPForQuery<Module>("SP_Auth_GetAuthorizedModules", area1Param);
                            }
                        }
                    }
                    else
                    {
                        //GlobalManager.outputLogger.Info(theSPParam.ErrorCode);
                        result.Success = false;
                        result.ErrorCode = theSPParam.ErrorCode;
                    }
                    #endregion
                }
                else if (applicationId == GlobalManager.SmartLife_Client_PensionAgency_Order)
                {
                    #region 养老机构客户端
                    BuilderFactory.DefaultBulder(connectstring_Or_Dbname).ExecuteSPNoneQuery("SP_Auth_PensionAgency", theSPParam);
                    if (theSPParam.ErrorCode == 0)
                    {
                        if (TypeConverter.ChangeString(theSPParam["UserId"]) != "")
                        {
                            result.ret.UserId = TypeConverter.ChangeString(theSPParam["UserId"]).ToGuid();
                        }
                        result.ret.UserName = TypeConverter.ChangeString(theSPParam["UserName"]);
                        result.ret.Area1 = TypeConverter.ChangeString(theSPParam["Area1"]);
                        result.ret.AreaCode1 = TypeConverter.ChangeString(theSPParam["AreaCode1"]);
                        result.ret.AreaName1 = TypeConverter.ChangeString(theSPParam["AreaName1"]);
                        if (TypeConverter.ChangeString(theSPParam["Area2"]) != "")
                        {
                            result.ret.Area2 = TypeConverter.ChangeString(theSPParam["Area2"]).ToGuid();
                            result.ret.AreaCode2 = TypeConverter.ChangeString(theSPParam["AreaCode2"]);
                            result.ret.AreaName2 = TypeConverter.ChangeString(theSPParam["AreaName2"]);
                        }
                        if (TypeConverter.ChangeString(theSPParam["Area3"]) != "")
                        {
                            result.ret.Area3 = TypeConverter.ChangeString(theSPParam["Area3"]).ToGuid();
                            result.ret.AreaCode3 = TypeConverter.ChangeString(theSPParam["AreaCode3"]);
                            result.ret.AreaName3 = TypeConverter.ChangeString(theSPParam["AreaName3"]);
                        }
                        result.ret.CityId = TypeConverter.ChangeString(theSPParam["CityId"]);
                        result.ret.CityCode = TypeConverter.ChangeString(theSPParam["CityCode"]);
                        result.ret.CityName = TypeConverter.ChangeString(theSPParam["CityName"]);
                        string areaId = null;
                        string areaCode = null;
                        string proviceCode = null;
                        if (result.ret.Area1 != "")
                        {
                            areaId = result.ret.Area1;
                            areaCode = result.ret.AreaCode1;
                            proviceCode = areaCode.Substring(0, 2);
                        }
                        else
                        {
                            if (result.ret.CityId != "")
                            {
                                areaId = result.ret.CityId;
                                areaCode = result.ret.CityCode;
                                proviceCode = areaCode.Substring(0, 2);
                            }
                        }
                        if (!string.IsNullOrEmpty(areaId))
                        {
                            var accessNode = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_DataExchange_Services, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                            if (accessNode != null)
                            {
                                result.ret.AccessPoint = accessNode.AccessPoint;
                            } 
                        }
                    }
                    else
                    {
                        //GlobalManager.outputLogger.Info(theSPParam.ErrorCode);
                        result.Success = false;
                        result.ErrorCode = theSPParam.ErrorCode;
                    }
                    #endregion
                }
                
            }
            catch (Exception ex)
            {
                GlobalManager.outputLogger.Info(ex.ToString());
            }
        }
        #endregion 
          
    }



    #region 入参类

    public class AuthenticateClientParam
    {
        public byte RunMode { get; set; }
        public string UserCode { get; set; }
        public string PasswordHash { get; set; }
        public string ObjectId { get; set; }//区
    }

    public class AuthenticateClientGovParam : AuthenticateClientParam { }
    public class AuthenticateClientMerParam : AuthenticateClientParam { }
    public class AuthenticateClientPensionAgencyParam : AuthenticateClientParam { }
    #endregion


    #region 返回结果类

    public class AuthenticateClientRet{
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string CityId { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string Area1 { get; set; }
        public string AreaCode1 { get; set; }
        public string AreaName1 { get; set; }
        public Guid? Area2 { get; set; }
        public string AreaCode2 { get; set; }
        public string AreaName2 { get; set; }
        public Guid? Area3 { get; set; }
        public string AreaCode3 { get; set; }
        public string AreaName3 { get; set; }
        public string AccessPoint { get; set; }
        public string AccessOfProvice { get; set; }
        public string DataPointOfManage { get; set; }
        public IList<Module> AuthorizedModules { get; set; }
    }

    public class AuthenticateClientGovRet : AuthenticateClientRet { }
    public class AuthenticateClientMerRet : AuthenticateClientRet {
        public string DataPointOfMerchant { get; set; }
    }
    public class AuthenticateClientPensionAgencyRet : AuthenticateClientRet { }
    
    #endregion
}