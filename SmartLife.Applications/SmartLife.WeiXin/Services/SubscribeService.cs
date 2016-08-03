using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e0571.web.core.Model;
using SmartLife.WeiXin.Pub;
using System.Data.SqlClient;

namespace SmartLife.WeiXin.Services
{
    public class SubscribeService
    {
        /// <summary>
        /// 用户关注公共号
        /// </summary>
        /// <param name="openid">用户标识</param>
        public void subscribe(string openid)
        {
            var param = "";
            if (exist(openid))
            {
                param = "update Meb_NormalAccount set Status='1' where OpenId='" + openid + "'";
            }
            else
            {
                param = "insert into Meb_NormalAccount(Status,OpenId,SubscribeTime) values('1','" + openid + "','" + DateTime.Now + "')";
            }
            BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(param);
        }
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="openid">用户标识</param>
        public void unsubscribe(string openid) 
        {
            var param = "update Meb_NormalAccount set Status='0' where OpenId='" + openid + "'";
            BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(param); 
        }
        /// <summary>
        ///  查询 用户在 数据库中是否已经有记录
        /// </summary>
        /// <param name="openid">用户标识</param>
        public bool exist(string openid) 
        {
            var param = "select * from dbo.Meb_NormalAccount where OpenId='" + openid + "'";
            int count = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(param);
            return count >= 1 ? true : false;
        }

    }
}