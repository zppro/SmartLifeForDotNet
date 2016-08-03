
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-06-06 $
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

namespace SmartLife.Share.Models.Oca
{
    /// <summary>
    /// EPay_AssessPackage模型
    /// </summary>
    public class EPay_AssessPackage : BaseModel
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
        /// 套餐Id
        /// </summary>
        public Guid? PackageId { get; set; }
        /// <summary>
        /// 评估等级
        /// </summary>
        public string AssessLevel { get; set; }
        /// <summary>
        /// 评估等级名称
        /// </summary>
        public string AssessLevelName { get; set; }
        /// <summary>
        /// 服务开始日期
        /// </summary>
        public string PackageBeginDate { get; set; }
        /// <summary>
        /// 服务结束日期
        /// </summary>
        public string PackageEndDate { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "EPay_AssessPackage";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_EPay_AssessPackage";
        }
        #endregion
    }

    /// <summary>
    /// EPay_AssessPackage主键
    /// </summary>
    public class EPay_AssessPackagePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}