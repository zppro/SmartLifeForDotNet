
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-01-29 $
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
    /// FlowDefineMapping模型
    /// </summary>
    public class FlowDefineMapping : BaseModel
    {
        #region 属性
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 录入时间
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
        /// 映射类型
        /// </summary>
        public string MappingType { get; set; }
        /// <summary>
        /// 映射类型Id
        /// </summary>
        public string MappingId { get; set; }
        /// <summary>
        /// 流转定义名称
        /// </summary>
        public string FlowName { get; set; }
        /// <summary>
        /// 映射列名
        /// </summary>
        public string MappingColumn { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "FlowDefineMapping";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_FlowDefineMapping";
        }
        #endregion
    }

    /// <summary>
    /// FlowDefineMapping主键
    /// </summary>
    public class FlowDefineMappingPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}