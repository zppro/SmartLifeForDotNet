using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLife.WeiXin.Model
{
    public class OAuthUser
    {
        #region 字段属性
        /// <summary>  
        /// 用户的唯一标识  
        /// </summary>  
        public string openid{ set; get; }
        /// <summary>  
        /// 用户昵称   
        /// </summary>  
        public string nickname{ set; get; }
        /// <summary>  
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知   
        /// </summary>  
        public string sex{ set; get; }
        /// <summary>  
        /// 使用语言   
        /// </summary>  
        public string language { set; get; }
        /// <summary>  
        /// 用户个人资料填写的省份  
        /// </summary>  
        public string province{ set; get; }
        /// <summary>  
        /// 普通用户个人资料填写的城市   
        /// </summary>  
        public string city{ set; get;}
        /// <summary>  
        /// 国家，如中国为CN   
        /// </summary>  
        public string country{ set; get; }
        /// <summary>  
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空  
        /// </summary>  
        public string headimgurl{ set; get; }
        /// <summary>  
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
        /// </summary>  
        public string privilege{ set; get; }
        #endregion  
    }
}