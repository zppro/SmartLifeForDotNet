
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-08-09 $
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
    /// DictionaryItem模型
    /// </summary>
    public class DictionaryItem : BaseModel
    {
        #region 属性
        /// <summary>
        /// 字典编号
        /// </summary>
        public string DictionaryId { get; set; }
        /// <summary>
        /// 字典项目编号
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
        /// 编码
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 上级Id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 级次
        /// </summary>
        public byte? Levels { get; set; }
        /// <summary>
        /// 末级
        /// </summary>
        public byte? EndFlag { get; set; }
        /// <summary>
        /// 此代码是系统代码
        /// </summary>
        public byte? SystemFlag { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int? OrderNo { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string InputCode1 { get; set; }
        /// <summary>
        /// 五笔码
        /// </summary>
        public string InputCode2 { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "DictionaryItem";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Sys_DictionaryItem";
        }
        #endregion
    }

    /// <summary>
    /// DictionaryItem主键
    /// </summary>
    public class DictionaryItemPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 字典编号
        /// </summary>
        public string DictionaryId { get; set; }
        /// <summary>
        /// 字典项目编号
        /// </summary>
        public string ItemId { get; set; }
        #endregion
    }
}