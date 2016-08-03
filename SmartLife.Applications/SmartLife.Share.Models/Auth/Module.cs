
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-11-03 $
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
    /// Module模型
    /// </summary>
    public class Module : BaseModel
    {
        #region 属性
        /// <summary>
        /// 模块编号
        /// </summary>
        public Guid? ModuleId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string ModuleCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 留空
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 前景色
        /// </summary>
        public string ForeColor { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public string BackColor { get; set; }
        /// <summary>
        /// 字体
        /// </summary>
        public string Font { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 位置排序序号
        /// </summary>
        public int? OrderNo { get; set; }
        /// <summary>
        /// 访问地址
        /// </summary>
        public string AccessUrl { get; set; }
        /// <summary>
        /// 打开目标
        /// </summary>
        public string OpenTarget { get; set; }
        /// <summary>
        /// 模块版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 应用编号
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        public string ControlType { get; set; }
        /// <summary>
        /// 控件选项
        /// </summary>
        public string ControlOption { get; set; }
        /// <summary>
        /// 助手栏标志
        /// </summary>
        public int? AssistToolbarFlag { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Module";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Auth_Module";
        }
        #endregion
    }

    /// <summary>
    /// Module主键
    /// </summary>
    public class ModulePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 模块编号
        /// </summary>
        public Guid? ModuleId { get; set; }
        #endregion
    }
}