 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e0571.web.core.Model;

namespace SmartLife.Share.Models.Oca
{
    /// <summary>
    /// ServiceVoice模型
    /// </summary>
    public class ServiceVoiceAddress : BaseModel
    {
        #region 属性 
        public byte? VoiceType { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string IPInner { get; set; }
        public int PortInner { get; set; }
        public string Address { get; set; }  
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "";
        }
        #endregion
    } 
}