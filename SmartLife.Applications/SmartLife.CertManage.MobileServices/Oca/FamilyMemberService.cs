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
using SmartLife.Share.Models.Oca;

namespace SmartLife.CertManage.MobileServices.Oca
{
    [MobileServicesValidate(ApplicationIdFrom = "MM101", ApplicationIdTo = "IP004")]
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class FamilyMemberService4IPhone : FamilyMemberServiceInner
    {
        #region 分页获取紧急服务接口
        [WebGet(UriTemplate = "GetRelationNamesWithOldMan/{oldManId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<RelationNamesWithOldMan> GetRelationNamesWithOldMan(string oldManId)
        {
            return base.GetRelationNamesWithOldManInner(oldManId);
        }
        #endregion 
    }
    [MobileServicesValidate(ApplicationIdFrom = "MM301", ApplicationIdTo = "IP004")]
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class FamilyMemberService4Android : FamilyMemberServiceInner
    {
        #region 分页获取紧急服务接口
        [WebGet(UriTemplate = "GetRelationNamesWithOldMan/{oldManId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<RelationNamesWithOldMan> GetRelationNamesWithOldMan(string oldManId)
        {
            return base.GetRelationNamesWithOldManInner(oldManId);
        }
        #endregion
    }
    public class FamilyMemberServiceInner : BaseWcfService
    {
        #region 分页获取紧急服务接口实现
        public InvokeResult<RelationNamesWithOldMan> GetRelationNamesWithOldManInner(string oldManId)
        {
            InvokeResult<RelationNamesWithOldMan> result = new InvokeResult<RelationNamesWithOldMan> { Success = true };
            try
            { 
                StringObjectDictionary filters = new { OldManId = Guid.Parse(oldManId), FamilyMemberId = Guid.Parse(GetHttpHeader("FamilyMemberId")) }.ToStringObjectDictionary();
                result.ret = BuilderFactory.DefaultBulder().ListStringObjectDictionary("OldManFamilyInfo_GetNames", filters).Select(item => new RelationNamesWithOldMan { RelationNameOfOldMan = TypeConverter.ChangeString(item["RelationNameOfOldMan"]), RelationNameOfFamily = TypeConverter.ChangeString(item["RelationNameOfFamily"]) }).FirstOrDefault();

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

    public class RelationNamesWithOldMan
    {
        public string RelationNameOfOldMan { get; set; }
        public string RelationNameOfFamily { get; set; }
    }
     
}