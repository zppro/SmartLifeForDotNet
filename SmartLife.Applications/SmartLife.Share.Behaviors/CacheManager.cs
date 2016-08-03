using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using e0571.web.core.Service;
using e0571.web.core.Cache;
using e0571.web.core.Model;
using e0571.web.core.TypeInherited;
using e0571.web.core.TypeExtension;

using SmartLife.Share.Models.Sys;

namespace SmartLife.Share.Behaviors
{
    public static class CacheManager
    {
        public static ICacheProvider ParmeterCacheProvider { get; private set; }
        public static ICacheProvider WhiteListCacheProvider { get; private set; }

        public static void Init()
        { 
            //系统参数缓存 
            ICacheProvider providerParameter = new OracleCacheProvidercs("Parameter");
            IList<string> parameterIds = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Parameter>().Select(item => item.ParameterId).ToList();
            providerParameter.AddKeys(parameterIds);
            providerParameter.RefreshCacheItem += new RefreshCacheItemDelegate(delegate(object o, RefreshCacheItemEventArgs args)
            {
                Parameter parameter = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).Load<Parameter, ParameterPK>(new ParameterPK { ParameterId = args.Item.Key });
                args.Item.Data = parameter.ParameterValue;
                args.Item.Expired = false;
            });
            CacheService.Instance.RegisterCacheProvider(providerParameter);
            ParmeterCacheProvider = providerParameter;
  
            //白名单缓存
            ICacheProvider providerWhiteList = new OracleCacheProvidercs("WhiteList");
            string sqlWhiteListKey = "select ApplicationId from Sys_WhiteList Where ApplicationId in ('"+string.Join("','",GlobalManager.ApplicationAccessible.ToArray())+"') Group By ApplicationId ";
            IList<string> whiteListKeys = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteNativeSqlForQuery(sqlWhiteListKey).Select(item => item[GlobalManager.ApplicationIdKey].ToString()).ToList();

            providerWhiteList.RefreshCacheItem += new RefreshCacheItemDelegate(delegate(object o, RefreshCacheItemEventArgs args)
            {
                IList<WhiteList> whiteLists = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<WhiteList>(new WhiteList { ApplicationId = args.Item.Key });
                args.Item.Data = whiteLists;
                args.Item.Expired = false;
            });
            CacheService.Instance.RegisterCacheProvider(providerWhiteList);
            WhiteListCacheProvider = providerWhiteList; 

        }

        #region 释放缓存方法

        public static void ExpireWhenParameterValueChanged(string parameterId)
        {
            ParmeterCacheProvider.ExpireKeyEqual(parameterId);
        }
        public static void ExpireAllParameters()
        {
            ParmeterCacheProvider.ExpireAll();
        }

        public static void ExpireWhenWhiteListChanged(string parameterId)
        {
            WhiteListCacheProvider.ExpireKeyEqual(parameterId);
        }
        public static void ExpireAllWhiteLists()
        {
            WhiteListCacheProvider.ExpireAll();
        }
        #endregion
    }
}
