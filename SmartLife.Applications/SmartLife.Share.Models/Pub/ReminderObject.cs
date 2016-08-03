
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-10-28 $
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
    /// ReminderObject模型
    /// </summary>
    public class ReminderObject : BaseModel
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
        /// 对象类别
        /// </summary>
        public string ObjectType { get; set; }
        /// <summary>
        /// 对象键值
        /// </summary>
        public string ObjectKey { get; set; }
        /// <summary>
        /// 提醒编号
        /// </summary>
        public int? ReminderId { get; set; }
        /// <summary>
        /// 响应应用类型
        /// </summary>
        public string ResponseAppType { get; set; }
        /// <summary>
        /// 响应标识
        /// </summary>
        public byte? ResponseFlag { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ReminderObject";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_ReminderObject";
        }
        #endregion
    }

    /// <summary>
    /// ReminderObject主键
    /// </summary>
    public class ReminderObjectPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}