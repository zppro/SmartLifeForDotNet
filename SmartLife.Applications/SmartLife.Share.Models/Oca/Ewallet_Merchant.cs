
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
    /// Ewallet_Merchant模型
    /// </summary>
    public class Ewallet_Merchant : BaseModel
    {
        #region 属性
        /// <summary>
        /// 商家Id
        /// </summary>
        public Guid? StationId { get; set; }
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
        /// 商家自身账户余额
        /// </summary>
        public decimal? BalanceOfSelf { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Ewallet_Merchant";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_Ewallet_Merchant";
        }
        #endregion
    }

    /// <summary>
    /// Ewallet_Merchant主键
    /// </summary>
    public class Ewallet_MerchantPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 商家Id
        /// </summary>
        public Guid? StationId { get; set; }
        #endregion
    }
}