
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2012-10-31 $
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
    /// Tree模型
    /// </summary>
    public class Tree : BaseModel
    {
        #region 属性
        /// <summary>
        /// 树编号
        /// </summary>
        public string TreeId { get; set; }
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
        /// 树类别
        /// </summary>
        public string TreeType { get; set; }
        /// <summary>
        /// 加载方式
        /// </summary>
        public string LoadMode { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string TreeCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string TreeName { get; set; }
        /// <summary>
        /// 树参数格式
        /// </summary>
        public string TreeParamFormat { get; set; }
        /// <summary>
        /// 根生成方式
        /// </summary>
        public byte? RootNodeGenerateMode { get; set; }
        /// <summary>
        /// 根内容
        /// </summary>
        public string RootNodeContent { get; set; }
        /// <summary>
        /// 根显示格式
        /// </summary>
        public string RootNodeFormat { get; set; }
        /// <summary>
        /// 根显示图标
        /// </summary>
        public string RootNodeIconCls { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Tree";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Sys_Tree";
        }
        #endregion
    }

    /// <summary>
    /// Tree主键
    /// </summary>
    public class TreePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 树编号
        /// </summary>
        public string TreeId { get; set; }
        #endregion
    }
}