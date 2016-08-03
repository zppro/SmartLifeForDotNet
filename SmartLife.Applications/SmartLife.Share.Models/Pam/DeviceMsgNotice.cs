
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-10-28 $
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

namespace SmartLife.Share.Models.Pam
{
    /// <summary>
    /// DeviceMsgNotice模型
    /// </summary>
    public class DeviceMsgNotice : BaseModel
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
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 日照中心编号
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public Guid? DeviceId { get; set; }
        /// <summary>
        /// 通知位置
        /// </summary>
        public string NoticePosition { get; set; }
        /// <summary>
        /// 通知类型
        /// </summary>
        public string NoticeType { get; set; }
        /// <summary>
        /// 通知内容
        /// </summary>
        public string NoticeContent { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "DeviceMsgNotice";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pam_DeviceMsgNotice";
        }
        #endregion
    }

    /// <summary>
    /// DeviceMsgNotice主键
    /// </summary>
    public class DeviceMsgNoticePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}