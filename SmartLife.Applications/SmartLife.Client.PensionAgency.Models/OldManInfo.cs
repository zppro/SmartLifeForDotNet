using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client.PensionAgency.Models
{
    public class OldManInfo
    {
        #region 属性
        /// <summary>
        /// 老人id
        /// </summary>
        public string OldManId { get; set; }
        /// <summary>
        /// 老人姓名
        /// </summary>
        public string OldManName { get; set; }
        /// <summary>
        /// 老人身份证
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 老人性别
        /// </summary>
        public string GenderName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        public string InputCode1 { get; set; }

        /// <summary>
        /// IC卡号
        /// </summary>
        public string ICNo { get; set; }

        #endregion
    } 
}
