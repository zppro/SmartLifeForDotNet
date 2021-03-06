﻿
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
    /// Parameter模型
    /// </summary>
    public class Parameter : BaseModel
    {
        #region 属性
        /// <summary>
        /// 参数编号
        /// </summary>
        public string ParameterId { get; set; }
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
        /// 名称
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string ParameterValue { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Parameter";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Sys_Parameter";
        }
        #endregion
    }

    /// <summary>
    /// Parameter主键
    /// </summary>
    public class ParameterPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 参数编号
        /// </summary>
        public string ParameterId { get; set; }
        #endregion
    }
}