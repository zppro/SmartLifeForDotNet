
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-12-19 $
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

namespace SmartLife.Share.Models.Bas
{
    /// <summary>
    /// ResidentIDNoRequisition模型
    /// </summary>
    public class ResidentIDNoRequisition : BaseModel
    {
        #region 属性
        /// <summary>
        /// 申请表Id
        /// </summary>
        public Guid? RequisitionId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 记录时间
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
        /// 提交人
        /// </summary>
        public Guid? SubmitBy { get; set; }
        /// <summary>
        /// 提交日期
        /// </summary>
        public DateTime? SubmitOn { get; set; }
        /// <summary>
        /// 确认人
        /// </summary>
        public Guid? ConfirmBy { get; set; }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime? ConfirmOn { get; set; }
        /// <summary>
        /// 居民Id
        /// </summary>
        public Guid? ResidentId { get; set; }
        /// <summary>
        /// 旧身份证号
        /// </summary>
        public string IDNoOld { get; set; }
        /// <summary>
        /// 新身份证号
        /// </summary>
        public string IDNoNew { get; set; }
        /// <summary>
        /// 变更原因
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 申请状态
        /// </summary>
        public byte? DoStatus { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ResidentIDNoRequisition";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Bas_ResidentIDNoRequisition";
        }
        #endregion
    }

    /// <summary>
    /// ResidentIDNoRequisition主键
    /// </summary>
    public class ResidentIDNoRequisitionPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 申请表Id
        /// </summary>
        public Guid? RequisitionId { get; set; }
        #endregion
    }
}