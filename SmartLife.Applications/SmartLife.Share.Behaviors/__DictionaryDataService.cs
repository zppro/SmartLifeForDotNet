using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Net;

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;
using e0571.web.core.MVC;


using SmartLife.Share.Behaviors;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub;

namespace SmartLife.Share.Behaviors
{

    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class __DictionaryDataService : AppBaseWcfService
    {
        [WebGet(UriTemplate = "getDictionaryItem/{strDictionaryId},{strPId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictionaryItem> getDictionaryItem(string strDictionaryId, string strPId)
        {
            try
            {
                DictionaryItem param = new DictionaryItem { DictionaryId = strDictionaryId };
                if (strPId != "ALL")
                {
                    param.ParentId = strPId;
                }
                return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<DictionaryItem>(param);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [WebGet(UriTemplate = "getDictionaryItemAsSelect/{strDictionaryId},{strPId},{strTextFieldFormat},{strValueFieldFormat}", ResponseFormat = WebMessageFormat.Json)]
        public IList<ListItem> getDictionaryItemAsSelect(string strDictionaryId, string strPId, string strTextFieldFormat, string strValueFieldFormat)
        {
            try
            {
                DictionaryItem param = new DictionaryItem { DictionaryId = strDictionaryId };
                if (strPId != "ALL")
                {
                    param.ParentId = strPId;
                }
                var items = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<DictionaryItem>(param).Select(item => new ListItem { Text = item.ToStringObjectDictionary().GetFormatValueString(strTextFieldFormat, @"\{([A-Za-z0-9]+)\}"), Value = item.ToStringObjectDictionary().GetFormatValueString(strValueFieldFormat, @"\{([A-Za-z0-9]+)\}") }).ToList();
                return items;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


    }
}
