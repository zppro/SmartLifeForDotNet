using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.IO;
using System.Text;

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;

using win.core.controls.crypto;

using SmartLife.Share.Behaviors;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;



namespace SmartLife.Share.Behaviors
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class __RegistrarService : BaseWcfService
    {
        #region 获取机器码
        [WebGet(UriTemplate = "GetMachineKey", BodyStyle = WebMessageBodyStyle.Bare)]
        public Stream GetMachineKey()
        {
            byte[] resultBytes = Encoding.UTF8.GetBytes(GlobalManager.MachineKey);
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";
            return new MemoryStream(resultBytes);
        }
        #endregion

        #region 获取认证信息 
        [WebGet(UriTemplate = "GetRegInfoValidated", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<RegInfoValidated> GetRegInfoValidated()
        {
            InvokeResult<RegInfoValidated> result = new InvokeResult<RegInfoValidated>() { Success = true };
            result.ret = GlobalManager.verifyInfo ?? new RegInfoValidated { IsValid = false, IsExpire = false, ED = DateTime.Now.AddDays(-1), Msg = "", MK = GlobalManager.MachineKey };
            return result;
        }
        #endregion

        #region 激活
        [WebInvoke(UriTemplate = "Activiate", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<RegInfoValidated> Activiate()
        {
            InvokeResult<RegInfoValidated> result = new InvokeResult<RegInfoValidated>() { Success = true };
            GlobalManager.Activiate();
            result.ret = GlobalManager.verifyInfo;
            return result;
        }
        #endregion
    }

    
}
