
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2016-01-12 $
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
    /// EvaluationInfo模型
    /// </summary>
    public class EvaluationInfo : BaseModel
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
        /// 申请表Id
        /// </summary>
        public Guid? RequisitionId { get; set; }
        /// <summary>
        /// 评估报告Id
        /// </summary>
        public Guid? ReportId { get; set; }
        /// <summary>
        /// 生活自理能力
        /// </summary>
        public string LifeSkills { get; set; }
        /// <summary>
        /// 生活自理得分
        /// </summary>
        public int? LifeSkillsResult { get; set; }
        /// <summary>
        /// 经济条件
        /// </summary>
        public string EcoCondition { get; set; }
        /// <summary>
        /// 经济条件得分
        /// </summary>
        public int? EcoConditionResult { get; set; }
        /// <summary>
        /// 居住环境
        /// </summary>
        public string LiveEnvironment { get; set; }
        /// <summary>
        /// 居住环境得分
        /// </summary>
        public int? LiveEnvironmentResult { get; set; }
        /// <summary>
        /// 年龄情况
        /// </summary>
        public string AgeCase { get; set; }
        /// <summary>
        /// 年龄情况得分
        /// </summary>
        public int? AgeCaseResult { get; set; }
        /// <summary>
        /// 特殊贡献
        /// </summary>
        public string SContribution { get; set; }
        /// <summary>
        /// 特殊贡献得分
        /// </summary>
        public int? SContributionResult { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string RecommendedserviceType { get; set; }
        /// <summary>
        /// 认知能力
        /// </summary>
        public string CognitiveSkills { get; set; }
        /// <summary>
        /// 认知能力得分
        /// </summary>
        public int? CognitiveSkillsResult { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "EvaluationInfo";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Eva_EvaluationInfo";
        }
        #endregion
    }

    /// <summary>
    /// EvaluationInfo主键
    /// </summary>
    public class EvaluationInfoPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}