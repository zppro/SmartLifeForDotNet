
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-04-09 $
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
    /// Merchant模型
    /// </summary>
    public class Merchant : BaseModel
    {
        #region 属性
        /// <summary>
        /// 服务网点Id
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
        /// 接单方式
        /// </summary>
        public string AcceptType { get; set; }
        /// <summary>
        /// 结算周期
        /// </summary>
        public string SettlementPeriod { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlementMode { get; set; }
        /// <summary>
        /// 工作日
        /// </summary>
        public string WorkDay { get; set; }
        /// <summary>
        /// 每日服务时间开始
        /// </summary>
        public string ServeTimeBeginOfDay { get; set; }
        /// <summary>
        /// 每日服务时间结束
        /// </summary>
        public string ServeTimeEndOfDay { get; set; }
        /// <summary>
        /// 服务时间描述
        /// </summary>
        public string ServeTimeOfDayDescription { get; set; }
        /// <summary>
        /// 服务额外说明
        /// </summary>
        public string ServeExtraComment { get; set; }
        /// <summary>
        /// 预置服务项目
        /// </summary>
        public string PresetServeItem { get; set; }
        /// <summary>
        /// 服务流程标志
        /// </summary>
        public byte? ServeFlowFlag { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Merchant";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_Merchant";
        }
        #endregion
    }

    /// <summary>
    /// Merchant主键
    /// </summary>
    public class MerchantPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 服务网点Id
        /// </summary>
        public Guid? StationId { get; set; }
        #endregion
    }
}
