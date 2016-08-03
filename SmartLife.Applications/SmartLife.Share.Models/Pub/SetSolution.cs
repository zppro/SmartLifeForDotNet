﻿
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-06-24 $
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
    /// SetSolution模型
    /// </summary>
    public class SetSolution : BaseModel
    {
        #region 属性
        /// <summary>
        /// 方案编号
        /// </summary>
        public string SolutionId { get; set; }
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
        /// 方案名称
        /// </summary>
        public string SolutionName { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int? OrderNo { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 默认集合
        /// </summary>
        public string DefaultSetId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "SetSolution";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_SetSolution";
        }
        #endregion
    }

    /// <summary>
    /// SetSolution主键
    /// </summary>
    public class SetSolutionPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 方案编号
        /// </summary>
        public string SolutionId { get; set; }
        #endregion
    }
}
