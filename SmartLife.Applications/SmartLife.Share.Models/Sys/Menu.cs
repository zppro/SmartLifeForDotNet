
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-11-12 $
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
    /// Menu模型
    /// </summary>
    public class Menu : BaseModel
    {
        #region 属性
        /// <summary>
        /// 菜单编号
        /// </summary>
        public string MenuId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 代码分类
        /// </summary>
        public string CodeClass { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string MenuCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 页面路径
        /// </summary>
        public string PageUrl { get; set; }
        /// <summary>
        /// 应用
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// 上级菜单
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 级次
        /// </summary>
        public byte? Levels { get; set; }
        /// <summary>
        /// 末级标识
        /// </summary>
        public byte? EndFlag { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int? OrderNo { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 框架内打开
        /// </summary>
        public byte? OpenInFrameFlag { get; set; }
        /// <summary>
        /// 选中刷新
        /// </summary>
        public byte? SelectOnRefreshFlag { get; set; }
        /// <summary>
        /// 可以全屏
        /// </summary>
        public byte? CanFullScreenFlag { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Menu";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Sys_Menu";
        }
        #endregion
    }

    /// <summary>
    /// Menu主键
    /// </summary>
    public class MenuPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 菜单编号
        /// </summary>
        public string MenuId { get; set; }
        #endregion
    }
}