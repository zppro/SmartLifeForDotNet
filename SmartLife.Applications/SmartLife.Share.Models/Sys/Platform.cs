
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2012-09-27 $
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
    /// Platform模型
    /// </summary>
    public class Platform : BaseModel
    {
        #region 属性
        /// <summary>
        /// 平台编号
        /// </summary>
        public string PlatformId { get; set; }
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
        public string PlatformCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string PlatformName { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName { get; set; }
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
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Platform";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Sys_Platform";
        }
        #endregion
    }

    /// <summary>
    /// Platform主键
    /// </summary>
    public class PlatformPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 平台编号
        /// </summary>
        public string PlatformId { get; set; }
        #endregion
    }
}