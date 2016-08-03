
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-06-14 $
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

namespace SmartLife.Share.Models.Inc
{
    /// <summary>
    /// SyncOut_ServiceWorkOrder_120701模型
    /// </summary>
    public class SyncOut_ServiceWorkOrder_120701 : BaseModel
    {
        #region 属性
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 同步日期
        /// </summary>
        public DateTime? SyncOn { get; set; }
        /// <summary>
        /// 同步标识
        /// </summary>
        public byte? SyncFlag { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 工单编号
        /// </summary>
        public string WorkOrderNo { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Service { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string LinkPhone { get; set; }
        /// <summary>
        /// 服务描述
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 接单时间
        /// </summary>
        public DateTime? AcceptTime { get; set; }
        /// <summary>
        /// 派单时间
        /// </summary>
        public DateTime? PieTime { get; set; }
        /// <summary>
        /// 服务完成时间
        /// </summary>
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// 预约服务时间
        /// </summary>
        public DateTime? ServieTime { get; set; }
        /// <summary>
        /// 回访描述
        /// </summary>
        public string BackNote { get; set; }
        /// <summary>
        /// 工单状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 满意度
        /// </summary>
        public string Satisficing { get; set; }
        /// <summary>
        /// 老人身份证
        /// </summary>
        public string OldManUnique { get; set; }
        /// <summary>
        /// 发生时的地址经度
        /// </summary>
        public string LongitudeS { get; set; }
        /// <summary>
        /// 发生时的地址纬度
        /// </summary>
        public string LatitudeS { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "SyncOut_ServiceWorkOrder_120701";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Inc_SyncOut_ServiceWorkOrder_120701";
        }
        #endregion
    }

    /// <summary>
    /// SyncOut_ServiceWorkOrder_120701主键
    /// </summary>
    public class SyncOut_ServiceWorkOrder_120701PK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}