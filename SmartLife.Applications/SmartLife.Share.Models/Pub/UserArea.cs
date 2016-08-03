
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2012-11-15 $
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
    /// UserArea模型
    /// </summary>
    public class UserArea : BaseModel
    {
        #region 属性
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// 辖区编号
        /// </summary>
        public Guid? AreaId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
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
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "UserArea";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_UserArea";
        }
        #endregion
    }

    /// <summary>
    /// UserArea主键
    /// </summary>
    public class UserAreaPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// 辖区编号
        /// </summary>
        public Guid? AreaId { get; set; }
        #endregion
    }
}