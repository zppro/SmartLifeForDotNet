﻿
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-06-11 $
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

namespace SmartLife.Share.Models.Pub
{
    /// <summary>
    /// SurveyResult模型
    /// </summary>
    public class SurveyResult : BaseModel
    {
        #region 属性
        /// <summary>
        /// 问卷结果编号
        /// </summary>
        public Guid? SurveyResultId { get; set; }
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
        /// 操作时间
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 问卷编号
        /// </summary>
        public Guid? SurveyId { get; set; }
        /// <summary>
        /// 调查项编号
        /// </summary>
        public Guid? SurveyItemId { get; set; }
        /// <summary>
        /// 调查项值
        /// </summary>
        public string SurveyItemValue { get; set; }
        /// <summary>
        /// 调查对象
        /// </summary>
        public Guid? SurveyTargetId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "SurveyResult";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_SurveyResult";
        }
        #endregion
    }

    /// <summary>
    /// SurveyResult主键
    /// </summary>
    public class SurveyResultPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 问卷结果编号
        /// </summary>
        public Guid? SurveyResultId { get; set; }
        #endregion
    }
}
