using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.ServiceModel.Web;
using System.Net;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using System.Dynamic;

using Newtonsoft.Json;
using e0571.web.core.DataAccess;
using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.TypeInherited;
using e0571.web.core.TypeExtension;
using e0571.web.core.Wcf;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub;
using SmartLife.Share.Models.Cer;
using SmartLife.Share.Models.WeiXin.Pub;
using SmartLife.Share.Models.WeiXin.Meb;

using win.core.controls.crypto;

namespace SmartLife.Share.Behaviors
{
    public static class GlobalManager
    {
        #region 日志
        public static log4net.ILog inputLogger = log4net.LogManager.GetLogger("Input");
        public static log4net.ILog outputLogger = log4net.LogManager.GetLogger("Output");
        public static StringStringDictionary errorCodeMessages = new StringStringDictionary();
        public static StringStringDictionary settingsForPhysicalDeploy = new StringStringDictionary();
        #endregion

        #region 共享变量
        public static string AT_INTERFACE = "I";
        public static string AT_BROWSER = "B";
        public static string AT_MOBILE = "M";
        public static List<string> ApplicationAccessible = new List<string>();
        public static List<DeployNode> DeployNodes = new List<DeployNode>();
        public static DeployNode CurrentDeployNode = null;
        public static string SALT_MOBILE = "SmartLife.Auth.Mobile.Services";
        public static string SALT_MERCHANT = "SmartLife.Auth.Merchant.Services";
        public static string SALT_SMS = "SmartLife.Auth.Sms.Services";
        public static string SALT_DATA_EXCHANGE = "SmartLife.DataExchange.Services";
        internal static bool _Initilized = false;
        public static string CurrentApplicationId { get; private set; }
        public static Guid? CurrentIPCC_CallCenterId { get; set; }
        public static string LogFlag { get; set; }

        public static string MachineKey { get; set; }
        public static RegInfoToValidate RegInfo = null;
        public static RegInfoValidated verifyInfo;
        
        #endregion

        #region 共享键 
        public const string ApplicationIdKey = "ApplicationId";
        public const string ConnectIdKey = "ConnectId";
        public const string AreaIdKey = "AreaId";
        public const string Job_Host_Database = "msdb";
        public const string Job_Database_User = "job";
        public const string Job_Database_Password = "leblue@2013";
        public const string IbatisConfigFileRefer = "embedded";
        public const string IbatisConfigFileValue_Job = "SQLServer.Share.config,SmartLife.Share.Models";
        #endregion

        #region 全局错误码
        public const int ERR_RefreshAccessTokenFaild = -56669;//刷新AccessToken失败
        #endregion

        #region 公共设置可选项
        public const string SETTINGS_ITEM_OPEN_DEVICE = "OPEN_DEVICE";
        public const string SETTINGS_ITEM_ENV = "ENV";
        public const string SETTINGS_VALUE_ENV_INTRANET = "INTRANET";
        public const string SETTINGS_VALUE_ENV_INTERNET = "INTERNET";
        public const string SETTINGS_VALUE_OPEN_DEVICE_PC = "PC";
        public const string SETTINGS_VALUE_OPEN_DEVICE_IOS = "IOS";
        public const string SETTINGS_VALUE_OPEN_DEVICE_ANDROID = "ANDROID";
        #endregion

        #region 共享预定义GUID
        public static Guid GuidAsAutoGenerate = Guid.Parse("A0000000-0000-0000-0000-000000000000");
        public static Guid GuidAsGroup_SysAdmin = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public static Guid GuidAsGroup_SysOper = Guid.Parse("00000000-0000-0000-0000-000000000002");
        public static Guid  GuidAsGroup_GovernmentAdmin = Guid.Parse("00001000-0001-0000-0000-000000000001");
        public static Guid GuidAsGroup_GovernmentOper = Guid.Parse("00001000-0001-0000-0000-000000000002");
        public static Guid GuidAsGroup_MerchantAdmin = Guid.Parse("00001000-0002-0000-0000-000000000001");
        public static Guid GuidAsGroup_MerchantOper = Guid.Parse("00001000-0002-0000-0000-000000000002");
        public static Guid GuidAsGroup_MerchantServeMan = Guid.Parse("00001000-0002-0000-0000-000000000003");
        public static Guid GuidAsGroup_TeamAdmin = Guid.Parse("00001000-0003-0000-0000-000000000001");
        public static Guid GuidAsGroup_TeamOper = Guid.Parse("00001000-0003-0000-0000-000000000002");
        public static Guid GuidAsGroup_OrganizationAdmin = Guid.Parse("00001000-0004-0000-0000-000000000001");
        public static Guid GuidAsGroup_OrganizationOper = Guid.Parse("00001000-0004-0000-0000-000000000002");
        public static Guid GuidAsGroup_CommunityAdmin = Guid.Parse("00001000-0005-0000-0000-000000000001");
        public static Guid GuidAsGroup_CommunityOper = Guid.Parse("00001000-0005-0000-0000-000000000002");
        #endregion

        #region 共享字典（将来用缓存实现）
        public const string DIKey_00002_SystemRole = "00001";
        public const string DIKey_00002_UserRole = "00002";
        public const string DIKey_00002_EmergencyAid = "10000";
        public const string DIKey_00002_FamillyCall = "10001";
        public const string DIKey_00002_Infotainment = "10002";
        public const string DIKey_00002_LifeService = "10003";

        public const string DIKey_00012_UploadFile = "00001";
        public const string DIKey_00012_ManualEdit = "00002";
        public const string DIKey_00012_Database = "00003";

        public const string DIKey_00011_User = "00001";
        public const string DIKey_00011_Member = "00002";

        public const string DIKey_00014_Year = "00001";
        public const string DIKey_00014_HalfYear = "00002";
        public const string DIKey_00014_Quarter = "00003";
        public const string DIKey_00014_Month = "00004";

        public const string DIKey_00015_SqlServer2005 = "00001";
        public const string DIKey_00015_Oracle10 = "00002";
        public const string DIKey_00015_MySql = "00003";
        public const string DIKey_00015_SQLite3 = "00004";

        public const string DIKey_01001_SuperAdmin = "00001";
        public const string DIKey_01001_DTS = "00002";
        public const string DIKey_01001_DayCareUser = "00005";
        public const string DIKey_01001_AgencyUser = "00004";
        public const string DIKey_01001_MerchantUser = "00003";
        public const string DIKey_01001_NormalUser = "00099";

        public const string DIKey_01002_Government = "00001";
        public const string DIKey_01002_Community = "00002";
        public const string DIKey_01002_Merchant = "00003";
        public const string DIKey_01002_CallCenter = "00004";
        public const string DIKey_01002_Team = "00005";
        public const string DIKey_01002_SocialOrganization = "00006";

        public const string DIKey_01004_Client = "00001";
        public const string DIKey_01004_Mobile = "00002";
        public const string DIKey_01004_Sms = "00003";
        public const string DIKey_01004_Web = "00004";
        public const string DIKey_01004_WeiXin = "00005";

        public const string DIKey_00010_ipad = "00001";
        public const string DIKey_00010_ipod = "00002";
        public const string DIKey_00010_iphone = "00003";
        public const string DIKey_00010_模拟器 = "00004";
        public const string DIKey_00010_打印机 = "00005";
        public const string DIKey_00010_IPCC = "00006";
        public const string DIKey_00010_语音网关 = "00007";
        public const string DIKey_00010_数据库服务器 = "00008";
        public const string DIKey_00010_摄像头 = "00009";
        public const string DIKey_00010_红外设备 = "00010";
        public const string DIKey_00010_自助服务机 = "00011";
        public const string DIKey_00010_桔豆盒子 = "00012";
        #endregion

        #region 参数Id键
        public const string PIKey_Sys_RunMode = "Sys_RunMode";
        public const string PIKey_Sys_LogFlag = "Sys_LogFlag";
        public const string PIKey_Sys_Registrar = "Sys_Registrar";
        #endregion

        #region 应用值 
        public const string SmartLife_CertManage_CertServices = "IP001";
        public const string SmartLife_CertManage_ManageServices = "IP002";
        public const string SmartLife_CertManage_ContentServices = "IP003";
        public const string SmartLife_CertManage_MobileServices = "IP004";
        public const string SmartLife_CertManage_MerchantServices = "IP005";
        public const string SmartLife_CertManage_SmsServices = "IP006";

        public const string SmartLife_CertManage_ManageCMS = "BP001";

        public const string SmartLife_OldCare_ManageServices = "IG001";
        public const string SmartLife_OldCare_ContentServices = "IG002";
        public const string SmartLife_OldCare_ManageCMS = "BG001";

        public const string SmartLife_Auth_Mobile_Services = "IG003";
        public const string SmartLife_Auth_Merchant_Services = "IB001";
        public const string SmartLife_Auth_Sms_Services = "IS001";

        public const string SmartLife_Mobile_Member_IPhone = "MM101";

        public const string SmartLife_DataExchange_Services = "IC001";
        public const string SmartLife_DayCareCenter_AIO = "SC001";

        public const string SmartLife_WeiXin = "SP001";

        public const string SmartLife_BaseData_ManageServices = "IP007"; 
        public const string SmartLife_BaseData_ManageCMS = "BP002";

        public const string SmartLife_Auth_ManageServices = "IP008";
        public const string SmartLife_Auth_ManageCMS = "BP003";
        public const string SmartLife_Auth_CertServices = "IP009";

        public const string SmartLife_City_ManageServices = "IP012";
        public const string SmartLife_City_ManageCMS = "BP004";

        public const string SmartLife_Officer_ManageServices = "IG011";
        public const string SmartLife_Officer_CertServices = "IG012";
        public const string SmartLife_Officer_ManageCMS = "BG011";

        public const string SmartLife_Client_Merchant = "CB001";
        public const string SmartLife_Client_Gov = "CG001";
        public const string SmartLife_Client_PensionAgency = "CC001";
        public const string SmartLife_Client_PensionAgency_SelfService = "CC002";
        public const string SmartLife_Client_PensionAgency_Order = "CC003";

        public const string SmartLife_WeiXinCloud_OldCare = "IP011";
        public const string SmartLife_WeiXinCloud_Schedule = "CP001";
        public const string SmartLife_Client_Schedule = "CP002";

        #endregion

        #region 共享接口
        public static void Init(string applicationId)
        {
            if (!_Initilized)
            {
                Assembly assem = Assembly.GetExecutingAssembly();
                log4net.Config.XmlConfigurator.Configure(assem.GetManifestResourceStream("SmartLife.Share.Behaviors.log4net.config"));

                if (applicationId == SmartLife_CertManage_CertServices || applicationId == SmartLife_Auth_Mobile_Services
                    || applicationId == SmartLife_CertManage_MobileServices || applicationId == SmartLife_DataExchange_Services
                    || applicationId == SmartLife_WeiXin || applicationId == SmartLife_Officer_CertServices || applicationId == SmartLife_Auth_CertServices
                     || applicationId == SmartLife_CertManage_MerchantServices
                    )
                {
                    _Initilized = true;
                }
                CurrentApplicationId = applicationId;

                StreamReader srForErrorMessage = new StreamReader(assem.GetManifestResourceStream("SmartLife.Share.Behaviors.ErrorCodeMessage.txt"));
                string[] lines = srForErrorMessage.ReadToEnd().Split('\n');
                srForErrorMessage.Close();
                foreach (var line in lines)
                {
                    if (line.Trim() != "")
                    {
                        var keyValues = line.Split("=".ToCharArray());
                        if (!errorCodeMessages.ContainsKey(keyValues[0]))
                        {
                            errorCodeMessages.Add(keyValues[0], keyValues[1].Replace("\r", ""));
                        }
                    }
                }
            }
        }

        public static void InitIndependent()
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            log4net.Config.XmlConfigurator.Configure(assem.GetManifestResourceStream("SmartLife.Share.Behaviors.log4net.config"));
            StreamReader srForErrorMessage = new StreamReader(assem.GetManifestResourceStream("SmartLife.Share.Behaviors.ErrorCodeMessage.txt"));
            string[] lines = srForErrorMessage.ReadToEnd().Split('\n');
            srForErrorMessage.Close();
            foreach (var line in lines)
            {
                if (line.Trim() != "")
                {
                    var keyValues = line.Split("=".ToCharArray());
                    if (!errorCodeMessages.ContainsKey(keyValues[0]))
                    {
                        errorCodeMessages.Add(keyValues[0], keyValues[1].Replace("\r", ""));
                    }
                }
            }
        }

        internal static void InitByWcfService()
        {
            _Initilized = true;
        }

        public static string getPK(string tableName, string pkName)
        {
            return getPK(tableName, pkName, null);
        }
        public static string getPK(string tableName, string pkName, string whereClause)
        {
            string result = null;
            StringObjectDictionary dictionary = new
            {
                TableName = tableName,
                PKName = pkName
            }.ToStringObjectDictionary();
            if (string.IsNullOrEmpty(whereClause))
            {
                dictionary.Add("WhereClause", System.DBNull.Value);
            }
            else
            {
                dictionary.Add("WhereClause", whereClause);
            }
            dictionary.Add("ret", null);

            BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPNoneQuery("SP_Tol_AutoGenerateForSysPK", dictionary);
            result = TypeConverter.ChangeString(dictionary["ret"]);
            return result;
        }

        public static string getPK2(string tableName, string pkName, string prefix)
        {
            return getPK2(tableName, pkName, prefix, null);
        }
        public static string getPK2(string tableName, string pkName, string prefix, string whereClause)
        {
            string result = null;
            StringObjectDictionary dictionary = new
            {
                TableName = tableName,
                PKName = pkName,
                Prefix = prefix ?? ""
            }.ToStringObjectDictionary();
            if (string.IsNullOrEmpty(whereClause))
            {
                dictionary.Add("WhereClause", System.DBNull.Value);
            }
            else
            {
                dictionary.Add("WhereClause", whereClause);
            }
            dictionary.Add("ret", null);

            BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPNoneQuery("SP_Tol_AutoGenerateForSysPK2", dictionary);
            result = TypeConverter.ChangeString(dictionary["ret"]);
            return result;
        }

        public static string getConnectString()
        {
            return getConnectString(null);
        }

        public static string getConnectString(string connectId)
        {
            string _connectString = null;
            if (TypeConverter.ChangeString(connectId) != "")
            {
                _connectString = connectId;
            }
            else
            {
                if (CurrentDeployNode != null)
                {
                    _connectString = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(CurrentDeployNode.ConnectString));
                }
            }
            return _connectString;
        }

        public static void RegisterDatabaseConnections()
        {
            //注册数据连接ase
            var itemsOfProvider = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<DictionaryItem>(new DictionaryItem { DictionaryId = "00015" });
            IList<DatabaseConnect> databaseConnects = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<DatabaseConnect>();
            IList<dynamic> connects = databaseConnects.Select(item =>
            {
                dynamic connect = new ExpandoObject();
                connect.ConnectId = item.ConnectId;
                connect.Provider = itemsOfProvider.Single(it => it.ItemId == item.Provider).ItemName;
                connect.ServerName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(item.ServerName);
                connect.UserCode = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(item.UserCode);
                connect.UserPassword = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(item.UserPassword);
                connect.DatabaseName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(item.DatabaseName);
                connect.IbatisConfigFileRefer = item.IbatisConfigFileRefer;
                connect.IbatisConfigFileValue = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(item.IbatisConfigFileValue);
                return connect;
            }).ToList();

            RegisterDatabaseConnection(connects);
        }

        public static void RegisterDatabaseConnection(dynamic connection)
        {
            RegisterDatabaseConnection(new List<dynamic> { connection });
        }
        public static void RegisterDatabaseConnection(IList<dynamic> connections)
        {
            IBatisNetManager.RegisterIBATIS(connections);
        }

        public class DeployArea
        {
            public string id ;
            public string code;
        } 

        public static DeployArea GetDeployArea()
        {
            return JsonConvert.DeserializeObject<DeployArea>(TypeConverter.ChangeString(CacheManager.ParmeterCacheProvider.Get("Sys_AppDeployArea"))) ?? new DeployArea { id = "*", code = "*" };
        }

        #endregion 

        #region 公共扩展
        public static bool isSuperAdmin(this User user)
        {
            bool result = false;
            if (user.UserType == DIKey_01001_SuperAdmin)
            {
                result = true;
            }
            return result;
        }

        public static bool isDTS(this User user)
        {
            bool result = false;
            if (user.UserType == DIKey_01001_DTS)
            {
                result = true;
            }
            return result;
        }

        public static bool isCCSeat(this User user)
        {
            bool result = false;
            result = Convert.ToBoolean(BuilderFactory.DefaultBulder(getConnectString()).ExecuteScalar("CheckIsCCSeat", new { UserId = user.UserId.ToString() }));
            return result;
        }

        public static bool IsMonitor(this User user)
        {
            bool result = false;
            result = Convert.ToBoolean(BuilderFactory.DefaultBulder(getConnectString()).ExecuteScalar("CheckIsMonitor", new { UserId = user.UserId.ToString() }));
            return result;
        }

        public static bool isMerchantUser(this User user)
        {
            bool result = false;
            result = Convert.ToBoolean(BuilderFactory.DefaultBulder(getConnectString()).ExecuteScalar("CheckIsMerchantUser", new { UserId = user.UserId.ToString() }));
            return result;
        }

        public static bool isAgencyUser(this User user)
        {
            bool result = false;
            result = Convert.ToBoolean(BuilderFactory.DefaultBulder(getConnectString()).ExecuteScalar("CheckIsAgencyUser", new { UserId = user.UserId.ToString() }));
            return result;
        }

        public static bool isDayCareUser(this User user)
        {
            bool result = false;
            result = Convert.ToBoolean(BuilderFactory.DefaultBulder(getConnectString()).ExecuteScalar("CheckIsDayCareUser", new { UserId = user.UserId.ToString() }));
            return result;
        }

        public static bool isNormalUser(this User user, List<string> groupIds)
        {
            bool result = false;

            string[] parts = user.UserType.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (parts[0] == DIKey_01001_NormalUser)
            {
                user.UserType = parts[0];
                result = true;
                if (parts.Length == 2)
                {
                    groupIds.AddRange(parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                }
            }
            return result;
        }

        public static bool isMerchantUser(this User user, List<string> groupIds)
        {
            bool result = false;

            string[] parts = user.UserType.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (parts[0] == DIKey_01001_MerchantUser)
            {
                user.UserType = parts[0];
                result = true;
                if (parts.Length == 2)
                {
                    groupIds.AddRange(parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                }
            }
            return result;
        }


        public static bool isAgencyUser(this User user, List<string> groupIds)
        {
            bool result = false;

            string[] parts = user.UserType.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (parts[0] == DIKey_01001_AgencyUser)
            {
                user.UserType = parts[0];
                result = true;
                if (parts.Length == 2)
                {
                    groupIds.AddRange(parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                }
            }
            return result;
        }

        public static bool isDayCareUser(this User user, List<string> groupIds)
        {
            bool result = false;

            string[] parts = user.UserType.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (parts[0] == DIKey_01001_DayCareUser)
            {
                user.UserType = parts[0];
                result = true;
                if (parts.Length == 2)
                {
                    groupIds.AddRange(parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                }
            }
            return result;
        }

        /*
        public static bool setSysAdmin(this User user, IList<IBatisNetBatchStatement> statements)
        {
            return setSingleRole(user, statements, GuidAsGroup_SysAdmin);
        }

        public static bool setSysOper(this User user, IList<IBatisNetBatchStatement> statements)
        {
            return setSingleRole(user, statements, GuidAsGroup_SysOper);
        }
        */

        public static bool setRoles(this User user, IList<IBatisNetBatchStatement> statements, IList<string> groupIds)
        {
            bool result = false;
            GroupMember gm = new GroupMember() { UserId = user.UserId };
            statements.Add(new IBatisNetBatchStatement { StatementName = gm.GetDeleteMethodName(), ParameterObject = gm.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });
            foreach (string groupId in groupIds)
            {
                gm.GroupId = Guid.Parse(groupId);
                gm.UserId = user.UserId;
                gm.OperatedBy = NormalSession.UserId.ToGuid();
                gm.OperatedOn = DateTime.Now;
                statements.Add(new IBatisNetBatchStatement { StatementName = gm.GetCreateMethodName(), ParameterObject = gm.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
            }
            result = true; 
            return result;
        }
        
        public static bool IsExistValue(this User user, string tableName, string columnName, string columnValue, string whereClause, string connectId)
        {
            bool result = false;
            SPParam spParam = new { }.ToSPParam();
            spParam["TableName"] = tableName;
            spParam["ColumnName"] = columnName;
            spParam["Value"] = columnValue;
            spParam["WhereClause"] = whereClause;
            var ret = BuilderFactory.DefaultBulder(connectId).ExecuteScalar("SP_Pub_IsExistValue", spParam);
            result = Convert.ToBoolean(ret);
            return result;
        }

        public static bool IsExistValue(this ServiceStation serviceStation, string tableName, string columnName, string columnValue, string whereClause, string connectId)
        {
            bool result = false;
            SPParam spParam = new { }.ToSPParam();
            spParam["TableName"] = tableName;
            spParam["ColumnName"] = columnName;
            spParam["Value"] = columnValue;
            spParam["WhereClause"] = whereClause;
            var ret = BuilderFactory.DefaultBulder(connectId).ExecuteScalar("SP_Pub_IsExistValue", spParam);
            result = Convert.ToBoolean(ret);
            return result;
        }
        #endregion

        #region 共享参数（读取到内存）
        public static void ReadParams()
        {
            //日志开关
            Parameter p = BuilderFactory.DefaultBulder(getConnectString()).Load<Parameter, ParameterPK>(new ParameterPK { ParameterId = PIKey_Sys_LogFlag });
            if (p.ParameterId != null)
            {
                LogFlag = p.ParameterValue;
            }
            else
            {
                LogFlag = "0";
            }
        }
        #endregion

        #region 微信相关
        public enum enumButtonTag
        {
            Arrive,
            Leave,
            Feedback
        }

        #region 微信模板
        public const string TemplateId_MyReserveInfoTitle = "MyReserveInfoTitle";
        public const string TemplateId_MyReserveInfoMoreUrl = "MyReserveInfoMoreUrl";
        public const string TemplateId_MyServeRecordTitle = "MyServeRecordTitle";
        public const string TemplateId_MyServeRecordMoreUrl = "MyServeRecordMoreUrl";
        public const string TemplateId_MyServeDurationStatTitle = "MyServeDurationStatTitle";
        public const string TemplateId_MyServeDurationStatMoreUrl = "MyServeDurationStatMoreUrl";
        public const string TemplateId_Not_As_ServeMan = "Not_As_ServeMan";
        public const string TemplateId_ServeManAuthPass = "ServeManAuthPass";
        public const string TemplateId_ServeManAuthDeny = "ServeManAuthDeny";
        #endregion

        public static ServiceAccount TheServiceAccount;
        public static StringStringDictionary TheMapAPIOfInverseAdressParse = new StringStringDictionary();

        public static StringStringDictionary TheWeiXinAPIOfV1 = new StringStringDictionary();
        public const string APIGETKey_Bas_GetAccessToken = "Bas_GetAccessToken";
        public const string APIPOSTKey_Cmu_CreateCustomMenu = "Cmu_CreateCustomMenu";
        public const string APIGETKey_Cmu_DeleteCustomMenu = "Cmu_DeleteCustomMenu";
        public const string APIGETKey_Meb_GetInfoOfNormalAccount = "Meb_GetInfoOfNormalAccount";
        public const string APIPOSTKey_Msg_SendCustomMessage = "Msg_SendCustomMessage";
        public const string APIGETKey_OAuth2_GetAccessToken = "OAuth2_GetAccessToken";
        public static IWeiXinDebugger TheWeiXinDebugger;
        public static IWeiXinResponseMessage TheWeiXinResponser;
        public static IWeiXinRequestMessage TheWeiXinRequester;
        public static IWeiXinDispatchMessage TheWeiXinDispatcher;

        public static void InitWeiXin(string serviceAccountCode, IWeiXinDebugger debugger, IWeiXinResponseMessage responser, IWeiXinRequestMessage requester, IWeiXinDispatchMessage dispatcher)
        {
            TheServiceAccount = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).Load<ServiceAccount, ServiceAccountPK>(new ServiceAccountPK { AccountCode = serviceAccountCode });
            var items = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ListStringObjectDictionary("GetMapAPIOfInverseAdressParse");
            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    TheMapAPIOfInverseAdressParse.Add(TypeConverter.ChangeString(item["ItemId"]), TypeConverter.ChangeString(item["ItemValue"]));
                }
            }
            TheWeiXinAPIOfV1.Add(APIGETKey_Bas_GetAccessToken, "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}");
            TheWeiXinAPIOfV1.Add(APIPOSTKey_Cmu_CreateCustomMenu, "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}");
            TheWeiXinAPIOfV1.Add(APIGETKey_Cmu_DeleteCustomMenu, "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}");
            TheWeiXinAPIOfV1.Add(APIGETKey_Meb_GetInfoOfNormalAccount, "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN");
            TheWeiXinAPIOfV1.Add(APIPOSTKey_Msg_SendCustomMessage, "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}");
            TheWeiXinAPIOfV1.Add(APIGETKey_OAuth2_GetAccessToken, "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code");
            TheWeiXinDebugger = debugger;
            TheWeiXinResponser = responser;
            TheWeiXinRequester = requester;
            TheWeiXinDispatcher = dispatcher;

        }

        #region 检查OpenId针对某个商家是否合法
        public static bool IsAuthorizedForOpenIdToStation(string openId, Guid? stationId)
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<WXStationAuthRequest>(new WXStationAuthRequest { OpenId = openId, StationId = stationId, DoStatus = 1 }).Count > 0;
        }
        #endregion

        #region 根据OpenId获取服务人员Id
        public static Guid? GetServeManIdByOpenId(string openId, Guid? stationId)
        {
            string strUserId = TypeConverter.ChangeString(BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteScalar(string.Format("select dbo.FUNC_Oca_GetUserIdByOpenId('{0}','{1}')", openId, stationId.ToString())));
            if(strUserId!=""){
                return strUserId.ToGuid();
            }
            return null;
        }
        #endregion

        #endregion

        #region 读取注册信息机器码
        public static void ReadRegInfo()
        {
            MachineKey = ZPCrypto.GetMachineKey(MachineHardwareType.CPU | MachineHardwareType.HARDDISK | MachineHardwareType.MAINBOARD);

            Parameter p = BuilderFactory.DefaultBulder(getConnectString()).Load<Parameter, ParameterPK>(new ParameterPK { ParameterId = PIKey_Sys_Registrar });
            if (p.ParameterId != null)
            {
                RegInfo = JsonConvert.DeserializeObject<RegInfoToValidate>(p.ParameterValue);    
            }
        }
        #endregion
        #region 激活
        public static void Activiate()
        {
            if (GlobalManager.RegInfo == null)
            {
                verifyInfo = new RegInfoValidated { IsExpire = true, IsValid = false, ED = DateTime.Now.AddDays(-1), Msg = "没有注册主体信息", MK = MachineKey };
                return;
            }

            string licx2Directory = HttpContext.Current.Server.MapPath("~") + @"\..\licx2\";
            string licx2Path = Path.Combine(licx2Directory, RegInfo.CP + '-' + RegInfo.PN + ".licx2");
            bool licx2exist = win.core.utils.FileAdapter.Exists(licx2Path);
            if (!licx2exist)
            {
                licx2Path = Path.Combine(licx2Directory, "dev.licx2");
                licx2exist = win.core.utils.FileAdapter.Exists(licx2Path);
            }
            if (!licx2exist)
            {
                licx2Directory = HttpContext.Current.Server.MapPath("~") + @"\..\..\licx2\";
                licx2Path = Path.Combine(licx2Directory, RegInfo.CP + '-' + RegInfo.PN + ".licx2");
            }
            licx2exist = win.core.utils.FileAdapter.Exists(licx2Path);
            if (!licx2exist)
            {
                licx2Path = Path.Combine(licx2Directory, "dev.licx2");
                licx2exist = win.core.utils.FileAdapter.Exists(licx2Path);
            }
            if (!licx2exist)
            {
                verifyInfo = new RegInfoValidated { IsExpire = true, IsValid = false, ED = DateTime.Now.AddDays(-1), Msg = "没有licx2文件", MK = MachineKey };
                return;
            }

            using (StreamReader sr = new StreamReader(licx2Path))
            {
                string cryptoedString = sr.ReadToEnd();

                VerifyStruct vs = ZPCrypto.Activiate(cryptoedString, RegInfo.CP, RegInfo.PN, (MachineHardwareType)RegInfo.MachineType, licx2Directory);

                verifyInfo = new RegInfoValidated { IsExpire = vs.Expired, IsValid = vs.Valid, ED = vs.ED, Msg = "", MK = MachineKey };
                TimeSpan ts = vs.ED.Subtract(DateTime.Now);
                int days = ts.Days;
                if (days < 15)
                {
                    if (days > 0)
                    {
                        verifyInfo.Msg = string.Format("系统即将在{0}天后过期，请联系管理员延长使用效期", days.ToString());
                    }
                    else
                    {
                        verifyInfo.Msg = string.Format("系统即将在{0}小时{1}分后过期，请联系管理员延长使用效期", ts.Hours, ts.Minutes);
                    }
                }

                if (vs.Valid && verifyInfo.Msg != "")
                {
                    verifyInfo.needRemind = true;
                }
            }

        }
        #endregion
    }
}
