using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e0571.web.core.Model;

namespace SmartLife.Share.Models.Sys
{
    /// <summary>
    /// DictItemEx2模型
    /// </summary>
    public class DictItemEx2 : BaseModel
    {
        #region 属性 
        /// <summary>
        /// 字典项目编号
        /// </summary>
        public string ItemId { get; set; } 
        /// <summary>
        /// 编码
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 扩展1
        /// </summary>
        public string Extend1 { get; set; }
        /// <summary>
        /// 扩展2
        /// </summary>
        public string Extend2 { get; set; }
        /// <summary>
        /// 扩展3
        /// </summary>
        public string Extend3 { get; set; } 
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

    /// <summary>
    /// DictionaryItem主键
    /// </summary>
    public class DictItemEx2PK : IPrimaryKeys
    {
        #region 属性 
        /// <summary>
        /// 字典项目编号
        /// </summary>
        public string ItemId { get; set; }
        #endregion
    }
}
