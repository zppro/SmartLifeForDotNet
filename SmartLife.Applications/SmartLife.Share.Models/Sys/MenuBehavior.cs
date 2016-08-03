
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2012-11-08 $
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
    /// MenuBehavior模型
    /// </summary>
    public class MenuBehavior : BaseModel
    {
        #region 属性
        /// <summary>
        /// 菜单编号
        /// </summary>
        public string MenuId { get; set; }
        /// <summary>
        /// 行为编码
        /// </summary>
        public string BehaviorCode { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "MenuBehavior";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Sys_MenuBehavior";
        }
        #endregion
    }

    /// <summary>
    /// MenuBehavior主键
    /// </summary>
    public class MenuBehaviorPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 菜单编号
        /// </summary>
        public string MenuId { get; set; }
        /// <summary>
        /// 行为编码
        /// </summary>
        public string BehaviorCode { get; set; }
        #endregion
    }
}