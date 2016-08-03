
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-02-16 $
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
    /// EPay_RechargeRecord模型
    /// </summary>
    public class EPay_RechargeRecord : BaseModel
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
        /// 充值类型
        /// </summary>
        public string RechargeType { get; set; }
        /// <summary>
        /// 充值标题
        /// </summary>
        public string RechargeTitle { get; set; }
        /// <summary>
        /// 充值分类编号
        /// </summary>
        public string RechargeTypeId { get; set; }
        /// <summary>
        /// 充值分类名称
        /// </summary>
        public string RechargeTypeName { get; set; }
        /// <summary>
        /// 充值项目
        /// </summary>
        public string ServeItemB { get; set; }
        /// <summary>
        /// 充值项目名称
        /// </summary>
        public string ServeItemBName { get; set; }
        /// <summary>
        /// 对象Id
        /// </summary>
        public string ObjectId { get; set; }
        /// <summary>
        /// 对象名称
        /// </summary>
        public string ObjectName { get; set; }
        /// <summary>
        /// 充值账户性质
        /// </summary>
        public string AccountNature { get; set; }
        /// <summary>
        /// 期间1
        /// </summary>
        public string PeriodId1 { get; set; }
        /// <summary>
        /// 期间2
        /// </summary>
        public string PeriodId2 { get; set; }
        /// <summary>
        /// 服务计费类型
        /// </summary>
        public string FeeType { get; set; }
        /// <summary>
        /// 计费数
        /// </summary>
        public decimal? ChargeNum { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "EPay_RechargeRecord";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_EPay_RechargeRecord";
        }
        #endregion
    }

    /// <summary>
    /// EPay_RechargeRecord主键
    /// </summary>
    public class EPay_RechargeRecordPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}