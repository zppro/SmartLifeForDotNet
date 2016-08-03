
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-06-11 $
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

namespace SmartLife.Share.Models.Auth
{
    /// <summary>
    /// GroupModuleSetting模型
    /// </summary>
    public class GroupModuleSetting : BaseModel
    {
        #region 属性
        /// <summary>
        /// 模块编号
        /// </summary>
        public Guid? ModuleId { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public Guid? GroupId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 坐标
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 前景色
        /// </summary>
        public string ForeColor { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public string BackColor { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "GroupModuleSetting";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Auth_GroupModuleSetting";
        }
        #endregion
    }

    /// <summary>
    /// GroupModuleSetting主键
    /// </summary>
    public class GroupModuleSettingPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 模块编号
        /// </summary>
        public Guid? ModuleId { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public Guid? GroupId { get; set; }
        #endregion
    }
}