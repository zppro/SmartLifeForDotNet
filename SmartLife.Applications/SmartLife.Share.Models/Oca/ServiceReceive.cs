
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-09-16 $
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
    /// ServiceReceive模型
    /// </summary>
    public class ServiceReceive : BaseModel
    {
        #region 属性
        /// <summary>
        /// 服务接单Id
        /// </summary>
        public Guid? ServiceReceiveId { get; set; }
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
        /// 接单状态
        /// </summary>
        public byte? ReceiveStatus { get; set; }
        /// <summary>
        /// 服务网点Id
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string DispatchType { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ReceiveType { get; set; }
        /// <summary>
        /// 工单Id
        /// </summary>
        public Guid? WorkOrderId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ServiceReceive";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_ServiceReceive";
        }
        #endregion
    }

    /// <summary>
    /// ServiceReceive主键
    /// </summary>
    public class ServiceReceivePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 服务接单Id
        /// </summary>
        public Guid? ServiceReceiveId { get; set; }
        #endregion
    }
}