
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2016-01-14 $
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
    /// FlowDefine模型
    /// </summary>
    public class FlowDefine : BaseModel
    {
        #region 属性
        /// <summary>
        /// 流转定义ID
        /// </summary>
        public Guid? FlowDefineId { get; set; }
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
        /// 服务类别
        /// </summary>
        public string ServiceType { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 流程当前状态
        /// </summary>
        public int? CurrentState { get; set; }
        /// <summary>
        /// 处理动作名称
        /// </summary>
        public string ProcessActionName { get; set; }
        /// <summary>
        /// 处理动作
        /// </summary>
        public int? ProcessAction { get; set; }
        /// <summary>
        /// 流转到
        /// </summary>
        public int? FlowTo { get; set; }
        /// <summary>
        /// 处理人抬头
        /// </summary>
        public string ProcessorTitle { get; set; }
        /// <summary>
        /// 流转名称
        /// </summary>
        public string FlowName { get; set; }
        /// <summary>
        /// 来源列
        /// </summary>
        public string TableColumn { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string FlowCName { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "FlowDefine";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_FlowDefine";
        }
        #endregion
    }

    /// <summary>
    /// FlowDefine主键
    /// </summary>
    public class FlowDefinePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 流转定义ID
        /// </summary>
        public Guid? FlowDefineId { get; set; }
        #endregion
    }
}
