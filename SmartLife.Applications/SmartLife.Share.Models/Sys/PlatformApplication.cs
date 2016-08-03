
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
    /// PlatformApplication模型
    /// </summary>
    public class PlatformApplication : BaseModel
    {
        #region 属性
        /// <summary>
        /// 平台编号
        /// </summary>
        public string PlatformId { get; set; }
        /// <summary>
        /// 应用编号
        /// </summary>
        public string ApplicationId { get; set; }
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
            return "PlatformApplication";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Sys_PlatformApplication";
        }
        #endregion
    }

    /// <summary>
    /// PlatformApplication主键
    /// </summary>
    public class PlatformApplicationPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 平台编号
        /// </summary>
        public string PlatformId { get; set; }
        /// <summary>
        /// 应用编号
        /// </summary>
        public string ApplicationId { get; set; }
        #endregion
    }
}