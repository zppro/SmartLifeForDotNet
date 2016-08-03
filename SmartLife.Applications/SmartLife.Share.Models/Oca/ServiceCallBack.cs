
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-10-11 $
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
    /// ServiceCallBack模型
    /// </summary>
    public class ServiceCallBack : BaseModel
    {
        #region 属性
        /// <summary>
        /// 服务回访记录Id
        /// </summary>
        public Guid? CallBackId { get; set; }
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
        /// 回访操作人
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// 回访操作日期
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 回访服务内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 回访处理状态
        /// </summary>
        public byte? DoStatus { get; set; }
        /// <summary>
        /// 回访的评估等级
        /// </summary>
        public string EvaluatedLevel { get; set; }
        /// <summary>
        /// 呼叫服务Id
        /// </summary>
        public Guid? CallServiceId { get; set; }
        /// <summary>
        /// 老人Id
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// 来源标识
        /// </summary>
        public byte? FromFlag { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ServiceCallBack";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_ServiceCallBack";
        }
        #endregion
    }

    /// <summary>
    /// ServiceCallBack主键
    /// </summary>
    public class ServiceCallBackPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 服务回访记录Id
        /// </summary>
        public Guid? CallBackId { get; set; }
        #endregion
    }
}