
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-12-29 $
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
    /// CallService模型
    /// </summary>
    public class CallService : BaseModel
    {
        #region 属性
        /// <summary>
        /// 呼叫服务Id
        /// </summary>
        public Guid? CallServiceId { get; set; }
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
        /// null
        /// </summary>
        public string AreaId2 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string AreaId3 { get; set; }
        /// <summary>
        /// 呼叫服务内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 呼叫地址经度
        /// </summary>
        public string LongitudeS { get; set; }
        /// <summary>
        /// 呼叫地址纬度
        /// </summary>
        public string LatitudeS { get; set; }
        /// <summary>
        /// 呼叫秒数
        /// </summary>
        public int? CallSeconds { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public byte? DoStatus { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>
        public string DoResults { get; set; }
        /// <summary>
        /// 老人Id
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// 服务队列Id
        /// </summary>
        public Guid? ServiceQueueId { get; set; }
        /// <summary>
        /// 服务队列编号
        /// </summary>
        public string ServiceQueueNo { get; set; }
        /// <summary>
        /// 接收分机Id
        /// </summary>
        public Guid? ServiceExtId { get; set; }
        /// <summary>
        /// 接收分机编号
        /// </summary>
        public string ServiceExtNo { get; set; }
        /// <summary>
        /// 服务类别
        /// </summary>
        public string ServiceCatalog { get; set; }
        /// <summary>
        /// 放弃标识
        /// </summary>
        public byte? AbandonFlag { get; set; }
        /// <summary>
        /// 回拨次数
        /// </summary>
        public int? DialBackCount { get; set; }
        /// <summary>
        /// 放弃日期
        /// </summary>
        public DateTime? AbandonOn { get; set; }
        /// <summary>
        /// 最后回拨时间
        /// </summary>
        public DateTime? LastDialBackOn { get; set; }
        /// <summary>
        /// 切换前服务队列Id
        /// </summary>
        public Guid? ServiceQueueIdOld { get; set; }
        /// <summary>
        /// 切换前服务队列编号
        /// </summary>
        public string ServiceQueueNoOld { get; set; }
        /// <summary>
        /// 主叫号码
        /// </summary>
        public string Caller { get; set; }
        /// <summary>
        /// 被叫号码
        /// </summary>
        public string Callee { get; set; }
        /// <summary>
        /// 呼叫中心记录唯一值
        /// </summary>
        public string UuidOfIPCC { get; set; }
        /// <summary>
        /// 服务归档
        /// </summary>
        public string ServiceArchive { get; set; }
        /// <summary>
        /// 弹屏标志
        /// </summary>
        public byte? PopFlag { get; set; }
        /// <summary>
        /// 呼叫回访标志
        /// </summary>
        public int? CallBackFlag { get; set; }
        /// <summary>
        /// 呼入类型
        /// </summary>
        public byte? CallInType { get; set; }
        /// <summary>
        /// 呼入转出标志
        /// </summary>
        public byte? RolledOutFlag { get; set; }
        /// <summary>
        /// 评价老人
        /// </summary>
        public string EvaluateToOldMan { get; set; }
        /// <summary>
        /// 评价老人时间
        /// </summary>
        public DateTime? EvaluateTimeToOldMan { get; set; }
        /// <summary>
        /// 评价人员
        /// </summary>
        public Guid? EvaluatedBy { get; set; }
        /// <summary>
        /// 评价座席
        /// </summary>
        public string EvaluateToSeat { get; set; }
        /// <summary>
        /// 评价座席时间
        /// </summary>
        public DateTime? EvaluateTimeToSeat { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "CallService";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_CallService";
        }
        #endregion
    }

    /// <summary>
    /// CallService主键
    /// </summary>
    public class CallServicePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 呼叫服务Id
        /// </summary>
        public Guid? CallServiceId { get; set; }
        #endregion
    }
}
