using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e0571.web.core.Model;

namespace SmartLife.Share.Models.WeiXin.Oca
{
    /// <summary>
    /// OldMan模型
    /// </summary>
    public class OldMan : BaseModel
    {
        #region 属性
        /// <summary>
        /// 老人id
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// 老人姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 老人身份证
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 老人性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 老人头像路径
        /// </summary>
        public string UrlHead { get; set; }
        /// <summary>
        /// 数据进入时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 状态:1有效,0无效
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 老人联系号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "OldMan";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_OldMan";
        }
        #endregion
    }

    public class OldManPK : IPrimaryKeys {
        #region 属性
        /// <summary>
        /// OldManId
        /// </summary>
        public Guid? OldManId { get; set; }
        #endregion
    }
}
