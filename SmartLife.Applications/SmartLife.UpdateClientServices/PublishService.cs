using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.IO;
using System.Text;
using System.Reflection;

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Models.Cer;
using SmartLife.Share.Models.Sys;
using e0571.web.core.Security;
using SmartLife.Share.Behaviors;
using win.core.update;
using Newtonsoft.Json;

namespace SmartLife.UpdateClientServices
{
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class PublishService : BaseWcfService
    {
        [WebGet(UriTemplate = "CheckNew/{applicationId}", ResponseFormat = WebMessageFormat.Json)]
        public Stream CheckNew(string applicationId)
        {
            Application app = BuilderFactory.DefaultBulder().Load<Application, ApplicationPK>(new ApplicationPK { ApplicationId = applicationId });
            if (app.ApplicationId == null)
            {
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";
                return new MemoryStream(Encoding.UTF8.GetBytes("无效的AppId"));
            }
            //检查主程序
            string exeFileName = app.ApplicationCode;
            string exeDir = HttpContext.Current.Server.MapPath("~/" + applicationId);
            string exeFilePath = exeDir + @"\" + exeFileName;
            if (FileAdapter.Exists(exeFilePath))
            {
                
                //Assembly asm = Assembly.LoadFile(exeFilePath);会锁定
                Assembly asm = Assembly.Load(System.IO.File.ReadAllBytes(exeFilePath));
                string version = asm.GetName().Version.ToString();
                Product product = new Product { ApplicationId = applicationId, ApplicationName = app.ApplicationName, Version = version, Description = "更新说明" };
                string appRoot = GetAppRootPath();
                if (appRoot.EndsWith("/"))
                {
                    appRoot = appRoot.Substring(0, appRoot.Length - 1);
                }
                product.FileList = new UpdateFilesInfo { SourcePath = appRoot + "/" + applicationId + "/" };
                
                string[] files = Directory.GetFiles(exeDir, "*", SearchOption.AllDirectories); 
                product.FileList.FileItems = new List<FileItem>();
                foreach (string file in files)
                {
                    string itemName = file.Replace(exeDir + @"\", "").Replace(@"\", "/");
                    product.FileList.FileItems.Add(new FileItem { Name = itemName });
                }
                 
                product.FileList.Count = product.FileList.FileItems.Count.ToString();
                byte[] resultBytes = Encoding.UTF8.GetBytes(XmlSerializeAdapter.Serialize<Product>(product));
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
                return new MemoryStream(resultBytes); 
            }

            return null;
        }

        [WebGet(UriTemplate = "CheckNew2/{applicationId}", ResponseFormat = WebMessageFormat.Json)]
        public Stream CheckNew2(string applicationId)
        {
            dynamic apps = win.core.utils.FileAdapter.GetJSONFromFile(HttpContext.Current.Server.MapPath("~/CheckNew2.json"));
            dynamic app = null;

            foreach (dynamic theOne in apps)
            {
                if (theOne.ApplicationId == applicationId)
                {
                    app = theOne;
                    break;
                }
            }

            if (app == null)
            {
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";
                return new MemoryStream(Encoding.UTF8.GetBytes("无效的AppId"));
            } 

            //检查主程序
            string exeFileName = app.ApplicationCode;
            string exeDir = HttpContext.Current.Server.MapPath("~/" + applicationId);
            string exeFilePath = exeDir + @"\" + exeFileName;
            
            if (FileAdapter.Exists(exeFilePath))
            {

                //Assembly asm = Assembly.LoadFile(exeFilePath);会锁定
                Assembly asm = Assembly.Load(System.IO.File.ReadAllBytes(exeFilePath));
                string version = asm.GetName().Version.ToString();
                Product product = new Product { ApplicationId = applicationId, ApplicationName = app.ApplicationName, Version = version, Description = "更新说明" };
                string appRoot = GetAppRootPath();
                if (appRoot.EndsWith("/"))
                {
                    appRoot = appRoot.Substring(0, appRoot.Length - 1);
                }
                product.FileList = new UpdateFilesInfo { SourcePath = appRoot + "/" + applicationId + "/" };

                string[] files = Directory.GetFiles(exeDir, "*", SearchOption.AllDirectories);
                product.FileList.FileItems = new List<FileItem>();
                foreach (string file in files)
                {
                    string itemName = file.Replace(exeDir + @"\", "").Replace(@"\", "/");
                    product.FileList.FileItems.Add(new FileItem { Name = itemName });
                }

                product.FileList.Count = product.FileList.FileItems.Count.ToString();
                byte[] resultBytes = Encoding.UTF8.GetBytes(XmlSerializeAdapter.Serialize<Product>(product));
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
                return new MemoryStream(resultBytes);
            }

            return null;
        }

        [WebGet(UriTemplate = "CheckNewOrigin/{applicationId}", ResponseFormat = WebMessageFormat.Json)]
        public Stream CheckNewOrigin(string applicationId)
        {
            string result = "";
            if (applicationId == "ipcc" || applicationId == "smartlife")
            {
                if (Global.invalid)
                {
                    result = "";
                }
                else
                {
                    result = e0571.web.core.Utils.DateTimeAdapter.ConvertDateTimeUnix(DateTime.Now).ToString();
                }
            }

            byte[] resultBytes = Encoding.UTF8.GetBytes(result);
            return new MemoryStream(resultBytes);
        }

        [WebGet(UriTemplate = "setInvalid/{invalid}", ResponseFormat = WebMessageFormat.Json)]
        public Stream setInvalid(string invalid)
        {
            Global.invalid = bool.Parse(invalid);
            byte[] resultBytes = Encoding.UTF8.GetBytes("ok");
            return new MemoryStream(resultBytes);
        }
         
        [WebGet(UriTemplate = "Output/{code}", ResponseFormat = WebMessageFormat.Json)]
        public Stream Output(string code)
        {
            string result = "";
            if (code == "apppath")
            {
                result = GetAppRootPath();
            }
            else if (code == "apppath2")
            {
                string appRoot = GetAppRootPath();
                if (appRoot.EndsWith("/"))
                {
                    appRoot = appRoot.Substring(0, appRoot.Length - 1);
                }
                result = appRoot;
            }
            else if (code == "CS002")
            {
                string exeFileName = "SmartLife.Client.Seat.exe";
                string exeFilePath = HttpContext.Current.Server.MapPath("~/" + code) + "/" + exeFileName;
                if (FileAdapter.Exists(exeFilePath))
                {
                    Assembly asm = Assembly.LoadFile(exeFilePath);
                    result = asm.GetName().Version.ToString(); 
                }
            }
            else if (code == "compare")
            {
                string _newsVersion = "0.1.5311.24874";
                int ret = _newsVersion.CompareTo("0.1.5311.16842");
                result = ret.ToString(); 
            }
            byte[] resultBytes = Encoding.UTF8.GetBytes(result);
            return new MemoryStream(resultBytes);
        }
    } 

    
}