
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-09-06 $
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
    /// Ewallet_Recharge_Record模型
    /// </summary>
    public class Ewallet_Recharge_Record : BaseModel
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
        /// 所属辖区
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public string AreaId2 { get; set; }
        /// <summary>
        /// 所属社区
        /// </summary>
        public string AreaId3 { get; set; }
        /// <summary>
        /// 充值对象类型
        /// </summary>
        public string RechargeObjectType { get; set; }
        /// <summary>
        /// 充值对象
        /// </summary>
        public Guid? RechargeObject { get; set; }
        /// <summary>
        /// 充值账户
        /// </summary>
        public string RechargeAccount { get; set; }
        /// <summary>
        /// 充值项目
        /// </summary>
        public string RechargeItem { get; set; }
        /// <summary>
        /// 充值时间
        /// </summary>
        public DateTime? RechargeTime { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal? RechargeAmount { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Ewallet_Recharge_Record";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_Ewallet_Recharge_Record";
        }
        #endregion
    }

    /// <summary>
    /// Ewallet_Recharge_Record主键
    /// </summary>
    public class Ewallet_Recharge_RecordPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}