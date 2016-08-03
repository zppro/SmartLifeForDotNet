
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-04-18 $
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
    /// CC_Ext模型
    /// </summary>
    public class CC_Ext : BaseModel
    {
        #region 属性
        /// <summary>
        /// 分机Id
        /// </summary>
        public Guid? ExtId { get; set; }
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
        /// 分机号
        /// </summary>
        public string ExtCode { get; set; }
        /// <summary>
        /// IPCC设备号
        /// </summary>
        public string IPCCDial { get; set; }
        /// <summary>
        /// IPCC中继类型
        /// </summary>
        public string IPCCRelayType { get; set; }
        /// <summary>
        /// 服务网点Id
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// IPCC中继前缀
        /// </summary>
        public string IPCCRelayPrefix { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "CC_Ext";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_CC_Ext";
        }
        #endregion
    }

    /// <summary>
    /// CC_Ext主键
    /// </summary>
    public class CC_ExtPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 分机Id
        /// </summary>
        public Guid? ExtId { get; set; }
        #endregion
    }
}
