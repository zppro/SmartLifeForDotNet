
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-07-10 $
 * 
 * iBATIS.NET For DotNet4.0 Models
 * Copyright (C) 2009 - CreateCode v0.5
 * 
 * $Remark: $
 * 
 * 
 * 
 ********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e0571.web.core.Model;

namespace SmartLife.Share.Models.Bas
{
    /// <summary>
    /// ResidentActivityLog模型
    /// </summary>
    public class ResidentActivityLog : BaseModel
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 居民Id
        /// </summary>
        public Guid? ResidentId { get; set; }
        /// <summary>
        /// 活动时间
        /// </summary>
        public DateTime? ActivityTime { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public string ActivityType { get; set; }
        /// <summary>
        /// 活动数据
        /// </summary>
        public string ActivityData { get; set; }
        /// <summary>
        /// 活动说明
        /// </summary>
        public string ActivityDescription { get; set; }
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
            return "ResidentActivityLog";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Bas_ResidentActivityLog";
        }
        #endregion
    }

    /// <summary>
    /// ResidentActivityLog主键
    /// </summary>
    public class ResidentActivityLogPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}
