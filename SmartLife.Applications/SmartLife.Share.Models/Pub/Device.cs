
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-08-19 $
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
    /// Device模型
    /// </summary>
    public class Device : BaseModel
    {
        #region 属性
        /// <summary>
        /// 设备编号
        /// </summary>
        public Guid? DeviceId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string DeviceCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 激活标识
        /// </summary>
        public byte? ActiveFlag { get; set; }
        /// <summary>
        /// 监控服务器
        /// </summary>
        public string Monitor { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Device";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_Device";
        }
        #endregion
    }

    /// <summary>
    /// Device主键
    /// </summary>
    public class DevicePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 设备编号
        /// </summary>
        public Guid? DeviceId { get; set; }
        #endregion
    }
}