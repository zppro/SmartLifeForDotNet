
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-11-14 $
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
    /// ResidentMigrateLog模型
    /// </summary>
    public class ResidentMigrateLog : BaseModel
    {
        #region 属性
        /// <summary>
        /// 居民动态迁移ID
        /// </summary>
        public Guid? ResidentDynamicMoveId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 事件发生时间
        /// </summary>
        public DateTime? EventTime { get; set; }
        /// <summary>
        /// 居民Id
        /// </summary>
        public Guid? ResidentId { get; set; }
        /// <summary>
        /// 迁出的辖区
        /// </summary>
        public string AreaIdFrom { get; set; }
        /// <summary>
        /// 迁出的街道
        /// </summary>
        public string AreaId2From { get; set; }
        /// <summary>
        /// 迁出的社区
        /// </summary>
        public string AreaId3From { get; set; }
        /// <summary>
        /// 迁入的辖区
        /// </summary>
        public string AreaIdTo { get; set; }
        /// <summary>
        /// 迁入的街道
        /// </summary>
        public string AreaId2To { get; set; }
        /// <summary>
        /// 迁入的社区
        /// </summary>
        public string AreaId3To { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public int? MigrateResultFlag { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Remark { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
                return "ResidentMigrateLog";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
                return "Bas_ResidentMigrateLog";
        }
        #endregion

    }

    /// <summary>
    /// ResidentMigrateLog主键
    /// </summary>
    public class ResidentMigrateLogPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 居民动态迁移ID
        /// </summary>
        public Guid? ResidentDynamicMoveId { get; set; }
        #endregion
    }
}
