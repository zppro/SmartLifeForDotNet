
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-11-02 $
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
    /// BookMeal模型
    /// </summary>
    public class BookMeal : BaseModel
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
        /// 数据来源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 日照中心编号
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// 老人Id
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// 配餐类别
        /// </summary>
        public string MealType { get; set; }
        /// <summary>
        /// 套餐编号
        /// </summary>
        public Guid? SetMealId { get; set; }
        /// <summary>
        /// 配餐日期
        /// </summary>
        public DateTime? BookDate { get; set; }
        /// <summary>
        /// 配取标志
        /// </summary>
        public byte? FetchFlag { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 配取人
        /// </summary>
        public Guid? FetchOperateBy { get; set; }
        /// <summary>
        /// 领取日期
        /// </summary>
        public DateTime? FetchTime { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "BookMeal";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pam_BookMeal";
        }
        #endregion
    }

    /// <summary>
    /// BookMeal主键
    /// </summary>
    public class BookMealPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}