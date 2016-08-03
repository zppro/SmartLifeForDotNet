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

using SmartLife.WeiXin.Model;
using System.Net;
using System.Text;
using SmartLife.WeiXin.Pub;
using Newtonsoft.Json.Linq;
using SmartLife.Share.Models.WeiXin.Meb;
using SmartLife.Share.Models.WeiXin.Oca;

namespace SmartLife.WeiXin.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BoundOldmanService : AppBaseWcfService
    {
        #region 查询老人是否已经绑定   查询老人信息是否正确 
        [WebGet(UriTemplate = "getOneOldMan?params={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<OldMan> getOneOldMan(string strParms)
        {
            JObject _jObject = JObject.Parse(strParms);
            var openid = _jObject["openid"].ToString();//用户
            var IDNo = _jObject["IDNo"].ToString();
            var OldIDNo = _jObject["OldIDNo"].ToString();//老人
            var OldName = _jObject["OldName"].ToString();

            //查询老人与用户是否已经绑定
            CollectionInvokeResult<NormalAccountFollow> resultFollow = new CollectionInvokeResult<NormalAccountFollow> { Success = true };
            var dictionaryFollow = new StringObjectDictionary().MixInJson("{'OpenId':'" + openid + "','IDNo':'" + IDNo + "','FollowToIDNo':'" + OldIDNo + "','FollowToName':'" + OldName + "'}");
            resultFollow.rows = BuilderFactory.DefaultBulder().List<NormalAccountFollow>(dictionaryFollow);

            CollectionInvokeResult<OldMan> result = new CollectionInvokeResult<OldMan> { Success = true };
            if (resultFollow.rows.Count > 0) //如果绑定 直接返回
            {
                result.ErrorCode = -1000;
                return result;
            }

            //查询老人信息是否正确   正确绑定
            var dictionary = new StringObjectDictionary().MixInJson("{'IDNo':'" + OldIDNo + "','Name':'" + OldName + "'}");

            result.rows = BuilderFactory.DefaultBulder().List<OldMan>(dictionary);

            foreach (OldMan oldman in result.rows)
            {
                if (!string.IsNullOrEmpty(oldman.IDNo) && !string.IsNullOrEmpty(oldman.Name))
                {
                    if (!insertNormalAccountFollow(openid, IDNo, OldIDNo, OldName))//绑定老人
                    {
                        result.Success = false;
                    }
                    
                    return result;
                }
            }
            return result;
        }
        #endregion

        #region 查询已绑定的老人
        [WebGet(UriTemplate = "findBoundOldMans?params={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<NormalAccountFollow> findBoundOldMans(string strParms)
        {
            JObject _Jobject = JObject.Parse(strParms);
            var openid = _Jobject["openid"].ToString();
            CollectionInvokeResult<NormalAccountFollow> result = new CollectionInvokeResult<NormalAccountFollow> { Success = true };
            var dictionary = new StringObjectDictionary().MixInJson("{'OpenId':'" + openid + "'}");
            result.rows = BuilderFactory.DefaultBulder().List<NormalAccountFollow>(dictionary);

            return result;
        }

        #endregion

        #region 绑定老人
        public bool insertNormalAccountFollow(string openid, string IDNo, string OldIDNo, string OldName)
        {
            NormalAccountFollow accountFollow = new NormalAccountFollow();
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                accountFollow.OpenId = openid;
                accountFollow.IDNo = IDNo;
                accountFollow.FollowToIDNo = OldIDNo;
                accountFollow.FollowToName = OldName;
                statements.Add(new IBatisNetBatchStatement { StatementName = accountFollow.GetCreateMethodName(), ParameterObject = accountFollow.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
            }
            catch (Exception e) {
                Util.WriteTxt("绑定老人错误：" + e.ToString());
                return (false);
            }

            return true;
        }
        #endregion

        #region 删除绑定关系 Delete
        [WebInvoke(UriTemplate = "{oldmanId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<NormalAccountFollowPK> Delete(string oldmanId)
        {
            Util.WriteTxt("删除");
            ModelInvokeResult<NormalAccountFollowPK> result = new ModelInvokeResult<NormalAccountFollowPK> { Success = true };
            try 
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                NormalAccountFollowPK pk = new NormalAccountFollowPK { Id = int.Parse(oldmanId) };
                statements.Add(new IBatisNetBatchStatement { StatementName = new NormalAccountFollow().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = pk;
            }
            catch (Exception ex) 
            {
                Util.WriteTxt(ex.ToString());
            }
            return result;
        }
        #endregion

    }
}