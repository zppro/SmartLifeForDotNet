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
using SmartLife.Share.Models.Oca;

namespace SmartLife.CertManage.ManageServices.Oca
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class UnSaveCallServiceService : BaseWcfService
    {
        #region 业务接口

        #region 即时滚动页调用
        [WebGet(UriTemplate = "LoadAgencyCallServicesForScroll/{timeSpanOfMinute},{id}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadAgencyCallServicesForScroll(string timeSpanOfMinute, string id)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {

                StringObjectDictionary filters = new { timeSpanOfMinute = Convert.ToInt32(timeSpanOfMinute) }.ToStringObjectDictionary();
                int Id;
                if (int.TryParse(id, out Id))
                {
                    filters.Add("Id", Id);
                }
                else
                {
                    filters.Add("Id", 0);
                }
                List<string> whereClause = new List<string>();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetAgencyCallServicesForScroll", filters);
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
    }

    #region 输入输出


    #endregion

}