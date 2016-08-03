using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using e0571.web.core.DataAccess;
using e0571.web.core.Model;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Cer;
using SmartLife.Share.Behaviors;
using System.Reflection; 
using System.Dynamic;


namespace SmartLife.CertManage.ManageServices
{
    public class Global : HttpApplication
    {
        //public static string gApplicationId = "IP002";//SmartLife.CertManage.ManageServices
        private bool bClearRegisterRoute = false; //是否已经清除,已清除，则禁止重启定时器，否则继续

        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
            IBatisNetManager.Init();//数据初始化

            GlobalManager.Init(GlobalManager.SmartLife_CertManage_ManageServices);
            GlobalManager.ReadParams();
            //GlobalManager.ReadRegInfo();//读取注册信息
            Assembly assem = Assembly.GetExecutingAssembly();
            StreamReader srForErrorMessage = new StreamReader(assem.GetManifestResourceStream("SmartLife.CertManage.ManageServices.ErrorCode.txt"));
            string[] lines = srForErrorMessage.ReadToEnd().Split('\n');
            srForErrorMessage.Close();
            foreach (var line in lines)
            {
                if (line.Trim() != "")
                {
                    var keyValues = line.Split("=".ToCharArray());
                    if (!GlobalManager.errorCodeMessages.ContainsKey(keyValues[0]))
                    {
                        GlobalManager.errorCodeMessages.Add(keyValues[0], keyValues[1].Replace("\r", ""));
                    }
                }
            }

            //初始化引用访问性
            GlobalManager.ApplicationAccessible.AddRange(BuilderFactory.DefaultBulder().List<ApplicationAccessibility>(new ApplicationAccessibility { InterfaceApplicationId = GlobalManager.SmartLife_CertManage_ManageServices }).Select(item => item.AccessApplicationId).ToList());
            GlobalManager.DeployNodes.AddRange(BuilderFactory.DefaultBulder().List<DeployNode>(new DeployNode { ApplicationIdTo = GlobalManager.SmartLife_CertManage_ManageServices }));
            CacheManager.Init();
            GlobalManager.CurrentIPCC_CallCenterId = BuilderFactory.DefaultBulder().List<SmartLife.Share.Models.Oca.CallCenter>().First().StationId;

            //注册数据连接
            var itemsOfProvider = BuilderFactory.DefaultBulder().List<DictionaryItem>(new DictionaryItem { DictionaryId = "00015" });
            IList<DatabaseConnect> databaseConnects = BuilderFactory.DefaultBulder().List<DatabaseConnect>();
            IList<dynamic> connects = databaseConnects.Select(item => {
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
            
            IBatisNetManager.RegisterIBATIS(connects);

            //GlobalManager.Activiate();

            //定时清除路由
            ClearRegisterRoutes();  
        }

        private void RegisterRoutes()
        {
            // Share
            RouteTable.Routes.Add(new ServiceRoute("Share/Init", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__InitService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/RegistrarService", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__RegistrarService)));

            
            RouteTable.Routes.Add(new ServiceRoute("Share/Login", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__AuthenticationService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/ParameterData", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__ParameterDataService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/AjaxData", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__AjaxDataService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/TreeDataService", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__TreeDataService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/DictionaryDataService", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__DictionaryDataService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/MsSQLScriptService", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__MsSQLScriptService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/IPCC", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__IPCCService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/Common", new WebServiceHostFactory(), typeof(AppShare.CommonService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/Circulation", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__CirculationService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/CrossDomain", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.CrossDomainService)));
            
            
           
            // Sys 
            RouteTable.Routes.Add(new ServiceRoute("Sys/MenuService", new WebServiceHostFactory(), typeof(Sys.MenuService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/WhiteListService", new WebServiceHostFactory(), typeof(Sys.WhiteListService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/ParameterService", new WebServiceHostFactory(), typeof(Sys.ParameterService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/TreeService", new WebServiceHostFactory(), typeof(Sys.TreeService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/TreeItemService", new WebServiceHostFactory(), typeof(Sys.TreeItemService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/DictionaryItemService", new WebServiceHostFactory(), typeof(Sys.DictionaryItemService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/DatabaseConnectService", new WebServiceHostFactory(), typeof(Sys.DatabaseConnectService)));
           
            // Pub
            RouteTable.Routes.Add(new ServiceRoute("Pub/UserService", new WebServiceHostFactory(), typeof(Pub.UserService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/GroupService", new WebServiceHostFactory(), typeof(Pub.GroupService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/AreaService", new WebServiceHostFactory(), typeof(Pub.AreaService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/ServiceStationService", new WebServiceHostFactory(), typeof(Pub.ServiceStationService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/ReportService", new WebServiceHostFactory(), typeof(Pub.ReportService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/ReminderService", new WebServiceHostFactory(), typeof(Pub.ReminderService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/SmsSendService", new WebServiceHostFactory(), typeof(Pub.SmsSendService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/LocationService", new WebServiceHostFactory(), typeof(Pub.LocationService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/CallObjectService", new WebServiceHostFactory(), typeof(Pub.CallObjectService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/SurveyService", new WebServiceHostFactory(), typeof(Pub.SurveyService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/SurveyServiceResult", new WebServiceHostFactory(), typeof(Pub.SurveyServiceResult)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/AttachmentService", new WebServiceHostFactory(), typeof(Pub.AttachmentService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/SetColumnService", new WebServiceHostFactory(), typeof(Pub.SetColumnService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/TableJoinService", new WebServiceHostFactory(), typeof(Pub.TableJoinService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/SetSolutionService", new WebServiceHostFactory(), typeof(Pub.SetSolutionService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/SetService", new WebServiceHostFactory(), typeof(Pub.SetService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/SubSetService", new WebServiceHostFactory(), typeof(Pub.SubSetService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/SubSetColumnService", new WebServiceHostFactory(), typeof(Pub.SubSetColumnService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/SetTableService", new WebServiceHostFactory(), typeof(Pub.SetTableService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/SubsetHelperService", new WebServiceHostFactory(), typeof(Pub.SubsetHelperService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/ArticleColumnService", new WebServiceHostFactory(), typeof(Pub.ArticleColumnService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/ArticleColumnPermitService", new WebServiceHostFactory(), typeof(Pub.ArticleColumnPermitService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/ArticleService", new WebServiceHostFactory(), typeof(Pub.ArticleService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/WageService", new WebServiceHostFactory(), typeof(Pub.WageService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/ArticleColumn_RelationService", new WebServiceHostFactory(), typeof(Pub.ArticleColumn_RelationService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/CmsService", new WebServiceHostFactory(), typeof(Pub.CmsService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/FlowDefineService", new WebServiceHostFactory(), typeof(Pub.FlowDefineService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/FlowDefineMappingService", new WebServiceHostFactory(), typeof(Pub.FlowDefineMappingService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/DeviceService", new WebServiceHostFactory(), typeof(Pub.DeviceService)));

            // Oca 
            RouteTable.Routes.Add(new ServiceRoute("Oca/OldManLocateInfoService", new WebServiceHostFactory(), typeof(Oca.OldManLocateInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/OldManBaseInfoService", new WebServiceHostFactory(), typeof(Oca.OldManBaseInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/OldManConfigInfoService", new WebServiceHostFactory(), typeof(Oca.OldManConfigInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/OldManComplainInfoService", new WebServiceHostFactory(), typeof(Oca.OldManComplainInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/OldManFamilyInfoService", new WebServiceHostFactory(), typeof(Oca.OldManFamilyInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/MutualAidPersonService", new WebServiceHostFactory(), typeof(Oca.MutualAidPersonService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/FamilyCameraService", new WebServiceHostFactory(), typeof(Oca.FamilyCameraService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/FamilyMemberService", new WebServiceHostFactory(), typeof(Oca.FamilyMemberService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/Ewallet_OldManService", new WebServiceHostFactory(), typeof(Oca.Ewallet_OldManService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/Ewallet_Recharge_RecordService", new WebServiceHostFactory(), typeof(Oca.Ewallet_Recharge_RecordService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/CallCenterService", new WebServiceHostFactory(), typeof(Oca.CallCenterService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/CC_GroupService", new WebServiceHostFactory(), typeof(Oca.CC_GroupService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/CC_ExtService", new WebServiceHostFactory(), typeof(Oca.CC_ExtService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/CallService", new WebServiceHostFactory(), typeof(Oca.CallServiceService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/UnSaveCallService", new WebServiceHostFactory(), typeof(Oca.UnSaveCallServiceService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/ServiceCallBackService", new WebServiceHostFactory(), typeof(Oca.ServiceCallBackService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/ServiceTrackLogService", new WebServiceHostFactory(), typeof(Oca.ServiceTrackLogService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/VoiceService", new WebServiceHostFactory(), typeof(Oca.ServiceVoiceService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/WorkOrderService", new WebServiceHostFactory(), typeof(Oca.ServiceWorkOrderService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/CallRecordService", new WebServiceHostFactory(), typeof(Oca.CallRecordService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/EPay_PackageService", new WebServiceHostFactory(), typeof(Oca.EPay_PackageService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/EPay_AssessPackageService", new WebServiceHostFactory(), typeof(Oca.EPay_AssessPackageService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/EPay_PackageItemService", new WebServiceHostFactory(), typeof(Oca.EPay_PackageItemService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/EPay_RechargeRecordService", new WebServiceHostFactory(), typeof(Oca.EPay_RechargeRecordService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/MerchantServiceReserveService", new WebServiceHostFactory(), typeof(Oca.MerchantServiceReserveService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/MerchantServeManMappingOldManService", new WebServiceHostFactory(), typeof(Oca.MerchantServeManMappingOldManService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/MerchantService", new WebServiceHostFactory(), typeof(Oca.MerchantService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/InstitutionsCareCenterService", new WebServiceHostFactory(), typeof(Oca.InstitutionsCareCenterService)));

            //Bas
            RouteTable.Routes.Add(new ServiceRoute("Bas/ResidentBaseInfoService", new WebServiceHostFactory(), typeof(Bas.ResidentBaseInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Bas/ResidentMigrateLogService", new WebServiceHostFactory(), typeof(Bas.ResidentMigrateLogService)));
            RouteTable.Routes.Add(new ServiceRoute("Bas/ResidentIDNoRequisitionService", new WebServiceHostFactory(), typeof(Bas.ResidentIDNoRequisitionService)));
            RouteTable.Routes.Add(new ServiceRoute("Bas/RoadBaseInfoService", new WebServiceHostFactory(), typeof(Bas.RoadBaseInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Bas/BuildingBaseInfoService", new WebServiceHostFactory(), typeof(Bas.BuildingBaseInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Bas/BuildingUnitBaseInfoService", new WebServiceHostFactory(), typeof(Bas.BuildingUnitBaseInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Bas/HouseBaseInfoService", new WebServiceHostFactory(), typeof(Bas.HouseBaseInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Bas/FamilyBaseInfoService", new WebServiceHostFactory(), typeof(Bas.FamilyBaseInfoService)));
            //Eva
            RouteTable.Routes.Add(new ServiceRoute("Eva/EvaluatedBaseInfoService", new WebServiceHostFactory(), typeof(Eva.EvaluatedBaseInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Eva/EvaluatedReportService", new WebServiceHostFactory(), typeof(Eva.EvaluatedReportService)));
            RouteTable.Routes.Add(new ServiceRoute("Eva/EvaluatedRequisitionService", new WebServiceHostFactory(), typeof(Eva.EvaluatedRequisitionService)));
            RouteTable.Routes.Add(new ServiceRoute("Eva/EvaluationInfoService", new WebServiceHostFactory(), typeof(Eva.EvaluationInfoService)));

            // Pam
            RouteTable.Routes.Add(new ServiceRoute("Pam/PensionAgencyObjectBaseInfoService", new WebServiceHostFactory(), typeof(Pam.PensionAgencyObjectBaseInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Pam/RoomService", new WebServiceHostFactory(), typeof(Pam.RoomService)));
            RouteTable.Routes.Add(new ServiceRoute("Pam/ServeManService", new WebServiceHostFactory(), typeof(Pam.ServeManService)));
            RouteTable.Routes.Add(new ServiceRoute("Pam/StationInfoService", new WebServiceHostFactory(), typeof(Pam.StationInfoService)));
            RouteTable.Routes.Add(new ServiceRoute("Pam/WorkExecuteService", new WebServiceHostFactory(), typeof(Pam.WorkExecuteService)));
            RouteTable.Routes.Add(new ServiceRoute("Pam/WorkPlanService", new WebServiceHostFactory(), typeof(Pam.WorkPlanService))); 
            RouteTable.Routes.Add(new ServiceRoute("Pam/AssessmentItemService", new WebServiceHostFactory(), typeof(Pam.AssessmentItemService)));
            RouteTable.Routes.Add(new ServiceRoute("Pam/AssessmentResultService", new WebServiceHostFactory(), typeof(Pam.AssessmentResultService)));
            RouteTable.Routes.Add(new ServiceRoute("Pam/BookMealService", new WebServiceHostFactory(), typeof(Pam.BookMealService)));
            RouteTable.Routes.Add(new ServiceRoute("Pam/SetMealService", new WebServiceHostFactory(), typeof(Pam.SetMealService)));
            RouteTable.Routes.Add(new ServiceRoute("Pam/CourseScheduleService", new WebServiceHostFactory(), typeof(Pam.CourseScheduleService)));
            RouteTable.Routes.Add(new ServiceRoute("Pam/DeviceMsgNoticeService", new WebServiceHostFactory(), typeof(Pam.DeviceMsgNoticeService)));

            //Cmu
            RouteTable.Routes.Add(new ServiceRoute("Cmu/ShareService", new WebServiceHostFactory(), typeof(Cmu.ShareService)));

            //WeiXin.Meb
            RouteTable.Routes.Add(new ServiceRoute("WeiXin/Meb/ServiceAccountService", new WebServiceHostFactory(), typeof(WeiXin.Meb.ServiceAccountService)));
            RouteTable.Routes.Add(new ServiceRoute("WeiXin/Meb/NormalAccountService", new WebServiceHostFactory(), typeof(WeiXin.Meb.NormalAccountService)));

            //WeiXin.Pub
            RouteTable.Routes.Add(new ServiceRoute("WeiXin/Pub/WXMessageTemplateService", new WebServiceHostFactory(), typeof(WeiXin.Pub.WXMessageTemplateService)));
            RouteTable.Routes.Add(new ServiceRoute("WeiXin/Pub/WXStationAuthRequestService", new WebServiceHostFactory(), typeof(WeiXin.Pub.WXStationAuthRequestService)));
        }

        private void ClearRegisterRoutes() {
            System.Timers.Timer clearTimer = new System.Timers.Timer(30000);
            clearTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs e)
            {
                if (e.SignalTime.Hour == 2 && e.SignalTime.Minute <= 5)
                {
                    win.core.utils.HttpAdapter.getSyncTo("http://update.lifeblue.com.cn:392/SmartLife/Publish/CheckNewOrigin/smartlife", (ret, res) =>
                    {
                        string strRet = e0571.web.core.Utils.TypeConverter.ChangeString(ret, "");
                        if (string.IsNullOrEmpty(strRet) && RouteTable.Routes != null && RouteTable.Routes.Count > 0)
                        {
                            RouteTable.Routes.Clear();
                            bClearRegisterRoute = true;
                            clearTimer.Enabled = false;
                            clearTimer = null;
                        }
                    });
                }
            });
            clearTimer.AutoReset = true;
            clearTimer.Enabled = true;
        }

        void Application_End(object sender, EventArgs e)
        {
            if (!bClearRegisterRoute)
            {
                //在应用程序关闭时运行的代码
                System.Threading.Thread.Sleep(1000);

                string url = "http://localhost/SmartLife.CertManage.ManageServices/AppShare/forReStart.ashx";     //测试
                if (GlobalManager.CurrentDeployNode != null && GlobalManager.CurrentDeployNode.RunMode != 1)
                {//正式
                    url = GlobalManager.CurrentDeployNode.AccessPoint + "/AppShare/forReStart.ashx";
                }
                if (GlobalManager.CurrentDeployNode == null && GlobalManager.DeployNodes != null && GlobalManager.DeployNodes.Count > 0)
                {
                    //GlobalManager.CurrentDeployNode 为空,IIS已回收过
                    url = GlobalManager.DeployNodes.FirstOrDefault(s => s.RunMode == 0).AccessPoint + "/AppShare/forReStart.ashx";
                }

                System.Net.HttpWebRequest myHttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                System.Net.HttpWebResponse myHttpWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();
                System.IO.Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流
            }
        }
    }
}
