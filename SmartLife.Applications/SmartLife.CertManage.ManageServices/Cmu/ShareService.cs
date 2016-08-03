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
using SmartLife.Share.Models.Bas;

namespace SmartLife.CertManage.ManageServices.Cmu
{  
    [ServiceValidateSession]
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]

    public class ShareService : AppBaseWcfService
    {
        [WebGet(UriTemplate = "GetStreetsInArea?param={areaId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<Area_V> GetStreetsInArea(string areaId)
        {
            //__AjaxDataService ajaxDateService = new __AjaxDataService();
            //IList<Area_V> user_areas2 = ajaxDateService.GetAreasAsParent();//取sysdictionaryItem所有区域
            //IList<Area> user_areas4 = ajaxDateService.GetArea();//取pub_area所有区域
            //IList<DictItem> user_areas5 = ajaxDateService.GetCityAndAreaInProvince("00012");//取sysdictionaryItem相关区域
            //IList<string> user_areas = ajaxDateService.GetUserArea(NormalSession.UserId);
            //IList<DictItemEx> user_areas3 = ajaxDateService.GetStreetAndCommunityInArea(user_areas[0]);//取pub_area相关区域
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Area_V>(new Area_V { ParentId = areaId, Status = 1, Levels = 4 });
            //return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Area_V>(new {ParentId = areaId, Status = 1, Levels=4, OrderByClause = "Levels asc,OrderNo asc" }.ToStringObjectDictionary());
        }

        [WebGet(UriTemplate = "GetCommunitiesInStreet?param={areaId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<Area_V> GetCommunitiesInStreet(string areaId)
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Area_V>(new Area_V { ParentId = areaId, Status = 1, Levels = 5 });
            //return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Area_V>(new {ParentId = areaId, Status = 1, Levels=4, OrderByClause = "Levels asc,OrderNo asc" }.ToStringObjectDictionary());
        }

        [WebGet(UriTemplate = "GetCommunityInfo?param={areaId}", ResponseFormat = WebMessageFormat.Json)]
        public Area_V GetCommunityInfo(string areaId)
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).Load<Area_V, Area_VPK>(new Area_VPK { AreaId = areaId });
        }

        [WebGet(UriTemplate = "GetTerminalCount?param={areaId}", ResponseFormat = WebMessageFormat.Json)]
        public int GetTerminalCount(string areaId)//只能查区县,街道,社区的
        {
            return this._GetTerminalCount(areaId);
        }
        
        //[WebInvoke(UriTemplate = "GetStreetInArea2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //public IList<Area_V> GetStreetInArea2(Area_V param)
        //{
            //__AjaxDataService ajaxDateService = new __AjaxDataService();

            //IList<Area_V> user_areas2 = ajaxDateService.GetAreasAsParent();//取sysdictionaryItem所有区域
            //IList<Area> user_areas4 = ajaxDateService.GetArea();//取pub_area所有区域
            //IList<DictItem> user_areas5 = ajaxDateService.GetCityAndAreaInProvince("00012");//取sysdictionaryItem相关区域
            //IList<string> user_areas = ajaxDateService.GetUserArea(NormalSession.UserId);
            //IList<DictItemEx> user_areas3 = ajaxDateService.GetStreetAndCommunityInArea(user_areas[0]);//取pub_area相关区域

            //IList<Area_V> streetData = BuilderFactory.DefaultBulder().List<Area_V>(new Area_V { ParentId = AreaId ,Status=1,Levels=4});
            //IList<Area_V> result = new List<Area_V> { };
            //try
            //{
            //    StringObjectDictionary filters = new StringObjectDictionary();
            //    if (param.instance != null)
            //    {
            //        filters = param.instance.ToStringObjectDictionary(false);
            //    }

            //    gridCollectionPager.EasyUIgridDoPage<Area_V>(BuilderFactory.DefaultBulder().List<Area_V>(filters), param, ref result);
            //}
            //catch (Exception ex)
            //{
            //    result.Success = false;
            //    result.ErrorMessage = ex.Message;
            //}
            //return result;
        //}

        #region --私有方法--
        private int _GetTerminalCount(string areaId)
        {
            int count=0;
            string str_sql = "select CallNo from oca_oldmanbaseinfo a inner join oca_oldmanconfiginfo b on a.OldManId=b.OldManId and callno is not null";
            Area_V areaObj=BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).Load<Area_V, Area_VPK>(new Area_VPK { AreaId = areaId });
            if (areaObj.Levels == 3)//区县
                str_sql += string.Format(" where AreaId='{0}'", areaId);
            else if (areaObj.Levels == 4)//街道
                str_sql += string.Format(" where AreaId2='{0}'", areaId);
            else if (areaObj.Levels == 5)//社区
                str_sql += string.Format(" where AreaId3='{0}'", areaId);
            else
                return count;//根据oca_oldmanbaseinfo只能查这三级
            count = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteNativeSqlForCount(str_sql);
            return count;
        }

        #endregion
    }
}