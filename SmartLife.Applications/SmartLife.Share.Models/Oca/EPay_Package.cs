
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-06-10 $
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
    /// EPay_Package模型
    /// </summary>
    public class EPay_Package : BaseModel
    {
        #region 属性
        /// <summary>
        /// 套餐Id
        /// </summary>
        public Guid? PackageId { get; set; }
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
        /// 套餐名称
        /// </summary>
        public string PackageName { get; set; }
        /// <summary>
        /// 套餐说明
        /// </summary>
        public string PackageComment { get; set; }
        /// <summary>
        /// 套餐资费
        /// </summary>
        public decimal? PackageCharges { get; set; }
        /// <summary>
        /// 充值频率
        /// </summary>
        public string PeriodType { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? PackageBeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? PackageEndDate { get; set; }
        /// <summary>
        /// 套餐备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 套餐类型
        /// </summary>
        public string PackageType { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "EPay_Package";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_EPay_Package";
        }
        #endregion
    }

    /// <summary>
    /// EPay_Package主键
    /// </summary>
    public class EPay_PackagePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 套餐Id
        /// </summary>
        public Guid? PackageId { get; set; }
        #endregion
    }
}