
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2012-09-25 $
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

namespace SmartLife.Share.Models.Sys
{
    /// <summary>
    /// ApplicationAccessibility模型
    /// </summary>
    public class ApplicationAccessibility : BaseModel
    {
        #region 属性
        /// <summary>
        /// 应用编号
        /// </summary>
        public string InterfaceApplicationId { get; set; }
        /// <summary>
        /// 应用编号
        /// </summary>
        public string AccessApplicationId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public int? Id { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ApplicationAccessibility";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Sys_ApplicationAccessibility";
        }
        #endregion
    }

    /// <summary>
    /// ApplicationAccessibility主键
    /// </summary>
    public class ApplicationAccessibilityPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 应用编号
        /// </summary>
        public string InterfaceApplicationId { get; set; }
        /// <summary>
        /// 应用编号
        /// </summary>
        public string AccessApplicationId { get; set; }
        #endregion
    }
}