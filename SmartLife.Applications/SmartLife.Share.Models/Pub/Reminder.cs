
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-09-21 $
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

namespace SmartLife.Share.Models.Pub
{
    /// <summary>
    /// Reminder模型
    /// </summary>
    public class Reminder : BaseModel
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 来源表
        /// </summary>
        public string SourceTable { get; set; }
        /// <summary>
        /// 来源列
        /// </summary>
        public string SourceColumn { get; set; }
        /// <summary>
        /// 来源键值
        /// </summary>
        public string SourceKey { get; set; }
        /// <summary>
        /// 来源类别
        /// </summary>
        public string SourceType { get; set; }
        /// <summary>
        /// 提醒时间
        /// </summary>
        public DateTime? RemindTime { get; set; }
        /// <summary>
        /// 提醒内容
        /// </summary>
        public string RemindContent { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Reminder";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_Reminder";
        }
        #endregion
    }

    /// <summary>
    /// Reminder主键
    /// </summary>
    public class ReminderPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}