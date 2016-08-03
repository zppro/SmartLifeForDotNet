
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-10-28 $
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

namespace SmartLife.Share.Models.Pam
{
    /// <summary>
    /// SetMeal模型
    /// </summary>
    public class SetMeal : BaseModel
    {
        #region 属性
        /// <summary>
        /// 套餐编号
        /// </summary>
        public Guid? SetMealId { get; set; }
        /// <summary>
        /// Id
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
        /// 套餐名称
        /// </summary>
        public string SetMealName { get; set; }
        /// <summary>
        /// 日照中心编号
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// 套餐内容
        /// </summary>
        public string SetMealContent { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "SetMeal";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pam_SetMeal";
        }
        #endregion
    }

    /// <summary>
    /// SetMeal主键
    /// </summary>
    public class SetMealPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 套餐编号
        /// </summary>
        public Guid? SetMealId { get; set; }
        #endregion
    }
}
