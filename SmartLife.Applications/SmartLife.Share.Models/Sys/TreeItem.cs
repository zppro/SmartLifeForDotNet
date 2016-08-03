
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2012-11-01 $
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
    /// TreeItem模型
    /// </summary>
    public class TreeItem : BaseModel
    {
        #region 属性
        /// <summary>
        /// 树编号
        /// </summary>
        public string TreeId { get; set; }
        /// <summary>
        /// 树项目编号
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 树项目编码
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 树项目名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 生成方式
        /// </summary>
        public string GenerateMode { get; set; }
        /// <summary>
        /// 生成内容
        /// </summary>
        public string GenerateContent { get; set; }
        /// <summary>
        /// 前缀编号
        /// </summary>
        public string PrefixId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int? OrderNo { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "TreeItem";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Sys_TreeItem";
        }
        #endregion
    }

    /// <summary>
    /// TreeItem主键
    /// </summary>
    public class TreeItemPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 树编号
        /// </summary>
        public string TreeId { get; set; }
        /// <summary>
        /// 树项目编号
        /// </summary>
        public string ItemId { get; set; }
        #endregion
    }
}