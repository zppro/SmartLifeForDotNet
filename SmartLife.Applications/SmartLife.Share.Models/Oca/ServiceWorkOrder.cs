
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-09-29 $
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
    /// ServiceWorkOrder模型
    /// </summary>
    public class ServiceWorkOrder : BaseModel
    {
        #region 属性
        /// <summary>
        /// 工单Id
        /// </summary>
        public Guid? WorkOrderId { get; set; }
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
        /// 工单编号
        /// </summary>
        public string WorkOrderNo { get; set; }
        /// <summary>
        /// 工单内容
        /// </summary>
        public string WorkOrderContent { get; set; }
        /// <summary>
        /// 服务项目A
        /// </summary>
        public string ServeItemA { get; set; }
        /// <summary>
        /// 服务项目B
        /// </summary>
        public string ServeItemB { get; set; }
        /// <summary>
        /// 服务方式
        /// </summary>
        public string ServeMode { get; set; }
        /// <summary>
        /// 服务半径
        /// </summary>
        public string ServeRadius { get; set; }
        /// <summary>
        /// 服务关键字
        /// </summary>
        public string ServeKeyword { get; set; }
        /// <summary>
        /// 服务类别
        /// </summary>
        public string ServeType { get; set; }
        /// <summary>
        /// 服务费用
        /// </summary>
        public decimal? ServeFee { get; set; }
        /// <summary>
        /// 政府部分
        /// </summary>
        public decimal? ServeFeeByGov { get; set; }
        /// <summary>
        /// 自费部分
        /// </summary>
        public decimal? ServeFeeBySelf { get; set; }
        /// <summary>
        /// 其他收费项
        /// </summary>
        public string OtherCharges { get; set; }
        /// <summary>
        /// 其他收费项金额
        /// </summary>
        public decimal? OtherChargesFee { get; set; }
        /// <summary>
        /// 要求服务时间
        /// </summary>
        public DateTime? ServiceTimeRequired { get; set; }
        /// <summary>
        /// 要求服务备注
        /// </summary>
        public string RemarkRequired { get; set; }
        /// <summary>
        /// 督办时间
        /// </summary>
        public DateTime? SuperviseTime { get; set; }
        /// <summary>
        /// 督办人
        /// </summary>
        public Guid? SupervisedBy { get; set; }
        /// <summary>
        /// 呼叫服务Id
        /// </summary>
        public Guid? CallServiceId { get; set; }
        /// <summary>
        /// 老人Id
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// 老人姓名
        /// </summary>
        public string OldManName { get; set; }
        /// <summary>
        /// 老人性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 家庭地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 家庭地址经度
        /// </summary>
        public string LongitudeS { get; set; }
        /// <summary>
        /// 家庭地址纬度
        /// </summary>
        public string LatitudeS { get; set; }
        /// <summary>
        /// 老人座机
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 老人手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 指派服务网点Id
        /// </summary>
        public Guid? ServeStationId { get; set; }
        /// <summary>
        /// 服务站名称
        /// </summary>
        public string ServeStationName { get; set; }
        /// <summary>
        /// 服务人姓名
        /// </summary>
        public string ServeManName { get; set; }
        /// <summary>
        /// 商家派人服务时间
        /// </summary>
        public DateTime? ServeManDispatchedTime { get; set; }
        /// <summary>
        /// 实际服务开始时间
        /// </summary>
        public DateTime? ServeBeginTime { get; set; }
        /// <summary>
        /// 实际服务结束时间
        /// </summary>
        public DateTime? ServeEndTime { get; set; }
        /// <summary>
        /// 服务站最后填写时间
        /// </summary>
        public DateTime? InputtedTimeByServiceStation { get; set; }
        /// <summary>
        /// 服务处理状态
        /// </summary>
        public byte? DoStatus { get; set; }
        /// <summary>
        /// 服务结果
        /// </summary>
        public string ServeResult { get; set; }
        /// <summary>
        /// 服务结果备注
        /// </summary>
        public string ServeResultRemark { get; set; }
        /// <summary>
        /// 回访人
        /// </summary>
        public Guid? FeedbackBy { get; set; }
        /// <summary>
        /// 老人对服务网点回访反馈
        /// </summary>
        public string FeedbackToServiceStation { get; set; }
        /// <summary>
        /// 老人对服务网点回访时间
        /// </summary>
        public DateTime? FeedbackTimeToServiceStation { get; set; }
        /// <summary>
        /// 老人对服务网点回访反馈备注
        /// </summary>
        public string FeedbackRemarkToServiceStation { get; set; }
        /// <summary>
        /// 服务网点对老人回访反馈
        /// </summary>
        public string FeedbackToOldMan { get; set; }
        /// <summary>
        /// 服务网点对老人回访时间
        /// </summary>
        public DateTime? FeedbackTimeToOldMan { get; set; }
        /// <summary>
        /// 服务网点对老人回访反馈备注
        /// </summary>
        public string FeedbackRemarkToOldMan { get; set; }
        /// <summary>
        /// 服务人员到达时间
        /// </summary>
        public DateTime? ServeManArriveTime { get; set; }
        /// <summary>
        /// 服务人员离开时间
        /// </summary>
        public DateTime? ServeManLeaveTime { get; set; }
        /// <summary>
        /// 服务最终结果
        /// </summary>
        public string ServeFinalResult { get; set; }
        /// <summary>
        /// 服务最终结果备注
        /// </summary>
        public string ServeFinalResultRemark { get; set; }
        /// <summary>
        /// 特殊处理标识
        /// </summary>
        public byte? SpecialFlag { get; set; }
        /// <summary>
        /// 服务人员
        /// </summary>
        public Guid? ServeManId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ServiceWorkOrder";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_ServiceWorkOrder";
        }
        #endregion
    }

    /// <summary>
    /// ServiceWorkOrder主键
    /// </summary>
    public class ServiceWorkOrderPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 工单Id
        /// </summary>
        public Guid? WorkOrderId { get; set; }
        #endregion
    }
}