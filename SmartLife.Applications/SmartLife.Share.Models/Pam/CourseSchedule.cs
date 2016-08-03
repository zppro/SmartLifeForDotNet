
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-10-29 $
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
    /// CourseSchedule模型
    /// </summary>
    public class CourseSchedule : BaseModel
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
        /// 日照中心编号
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public Guid? DeviceId { get; set; }
        /// <summary>
        /// 课程编号
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 课程开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 课程时长
        /// </summary>
        public int? CourseDuration { get; set; }
        /// <summary>
        /// 课程标志
        /// </summary>
        public byte? CourseFlag { get; set; }
        /// <summary>
        /// 课程信息
        /// </summary>
        public string CourseInfo { get; set; }
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
            return "CourseSchedule";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pam_CourseSchedule";
        }
        #endregion
    }

    /// <summary>
    /// CourseSchedule主键
    /// </summary>
    public class CourseSchedulePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}