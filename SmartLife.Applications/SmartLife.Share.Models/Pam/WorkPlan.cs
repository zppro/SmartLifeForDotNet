
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-08-07 $
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
    /// WorkPlan模型
    /// </summary>
    public class WorkPlan : BaseModel
    {
        #region 属性
        /// <summary>
        /// 工作计划编号
        /// </summary>
        public Guid? PlanId { get; set; }
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
        /// 操作人
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 计划日期
        /// </summary>
        public DateTime? PlanDate { get; set; }
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
        /// 完成标识
        /// </summary>
        public byte? FinishFlag { get; set; }
        /// <summary>
        /// 服务人员
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 完成情况
        /// </summary>
        public byte? CompleteStatus { get; set; }
        /// <summary>
        /// 实际工作量
        /// </summary>
        public int? WorkloadActual { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "WorkPlan";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pam_WorkPlan";
        }
        #endregion
    }

    /// <summary>
    /// WorkPlan主键
    /// </summary>
    public class WorkPlanPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 工作计划编号
        /// </summary>
        public Guid? PlanId { get; set; }
        #endregion
    }
}
