
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-08-05 $
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
    /// WorkExecute模型
    /// </summary>
    public class WorkExecute : BaseModel
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
        /// 服务人员
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// 房间ID
        /// </summary>
        public Guid? RoomId { get; set; }
        /// <summary>
        /// 老人Id
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// 工作项
        /// </summary>
        public string WorkItem { get; set; }
        /// <summary>
        /// 服务人员到达时间
        /// </summary>
        public DateTime? ServeManArriveTime { get; set; }
        /// <summary>
        /// 服务人员离开时间
        /// </summary>
        public DateTime? ServeManLeaveTime { get; set; }
        /// <summary>
        /// 首次提醒时间
        /// </summary>
        public DateTime? RemindTime { get; set; }
        /// <summary>
        /// 预计结束时间
        /// </summary>
        public DateTime? PlanEndTime { get; set; }
        /// <summary>
        /// 最后扫描时间
        /// </summary>
        public DateTime? LastScanTime { get; set; }
        /// <summary>
        /// 已提醒次数
        /// </summary>
        public int? Reminded { get; set; }
        /// <summary>
        /// 最大提醒次数
        /// </summary>
        public int? RemindMax { get; set; }
        /// <summary>
        /// 播放重复次数
        /// </summary>
        public int? PlayRepeats { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "WorkExecute";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pam_WorkExecute";
        }
        #endregion
    }

    /// <summary>
    /// WorkExecute主键
    /// </summary>
    public class WorkExecutePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}