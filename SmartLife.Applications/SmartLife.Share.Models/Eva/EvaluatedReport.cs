
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2016-06-12 $
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

namespace SmartLife.Share.Models.Eva
{
    /// <summary>
    /// EvaluatedReport模型
    /// </summary>
    public class EvaluatedReport : BaseModel
    {
        #region 属性
        /// <summary>
        /// 评估报告Id
        /// </summary>
        public Guid? ReportId { get; set; }
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
        /// 申请表Id
        /// </summary>
        public Guid? RequisitionId { get; set; }
        /// <summary>
        /// 评估对象姓名
        /// </summary>
        public string EvaluatedName { get; set; }
        /// <summary>
        /// 居住所属辖区
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 居住所属街道
        /// </summary>
        public string AreaId2 { get; set; }
        /// <summary>
        /// 居住所属社区
        /// </summary>
        public string AreaId3 { get; set; }
        /// <summary>
        /// 评估报告编号
        /// </summary>
        public string ReportCode { get; set; }
        /// <summary>
        /// 评估状态
        /// </summary>
        public byte? DoStatus { get; set; }
        /// <summary>
        /// 上一次评估报告Id
        /// </summary>
        public Guid? LastReportId { get; set; }
        /// <summary>
        /// 评估类别
        /// </summary>
        public string AssessType { get; set; }
        /// <summary>
        /// 评估员姓名
        /// </summary>
        public string Assessor { get; set; }
        /// <summary>
        /// 评估单位
        /// </summary>
        public string AssessmentUnit { get; set; }
        /// <summary>
        /// 评估单位电话
        /// </summary>
        public string AssessmentCall { get; set; }
        /// <summary>
        /// 评估分值
        /// </summary>
        public int? AssessmentScores { get; set; }
        /// <summary>
        /// 建议服务类型
        /// </summary>
        public string ServeItemType { get; set; }
        /// <summary>
        /// 社区意见
        /// </summary>
        public string CommunityRemark { get; set; }
        /// <summary>
        /// 社区提交意见时间
        /// </summary>
        public DateTime? CommunityTime { get; set; }
        /// <summary>
        /// 街道审查
        /// </summary>
        public byte? StreetExamine { get; set; }
        /// <summary>
        /// 街道审查时间
        /// </summary>
        public DateTime? StreetExamineTime { get; set; }
        /// <summary>
        /// 市民政局审核
        /// </summary>
        public byte? CityAudit { get; set; }
        /// <summary>
        /// 市民政局审核时间
        /// </summary>
        public DateTime? CityAuditTime { get; set; }
        /// <summary>
        /// 评估对象等级
        /// </summary>
        public string AssessLevel { get; set; }
        /// <summary>
        /// 服务起始时间
        /// </summary>
        public DateTime? ServiceBeginTime { get; set; }
        /// <summary>
        /// 服务截止时间
        /// </summary>
        public DateTime? ServiceEndTime { get; set; }
        /// <summary>
        /// 社区核查
        /// </summary>
        public byte? CommunityCheck { get; set; }
        /// <summary>
        /// 社区核查人
        /// </summary>
        public Guid? CommunityChecker { get; set; }
        /// <summary>
        /// 街道审查人
        /// </summary>
        public Guid? StreetExaminer { get; set; }
        /// <summary>
        /// 市民政局审核人
        /// </summary>
        public Guid? CityApproval { get; set; }
        /// <summary>
        /// 打印次数
        /// </summary>
        public int? PrintNo { get; set; }
        /// <summary>
        /// 居家养老结果
        /// </summary>
        public string OcaResult { get; set; }
        /// <summary>
        /// 机构养老结果
        /// </summary>
        public string PamResult { get; set; }
        /// <summary>
        /// 日照中心结果
        /// </summary>
        public string DccResult { get; set; }
        /// <summary>
        /// 评估结果
        /// </summary>
        public string AssessmentResult { get; set; }
        /// <summary>
        /// 服务商补助
        /// </summary>
        public decimal? ServiceSubsidies { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "EvaluatedReport";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Eva_EvaluatedReport";
        }
        #endregion
    }

    /// <summary>
    /// EvaluatedReport主键
    /// </summary>
    public class EvaluatedReportPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 评估报告Id
        /// </summary>
        public Guid? ReportId { get; set; }
        #endregion
    }
}