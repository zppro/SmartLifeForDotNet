
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
    /// Application模型
    /// </summary>
    public class Application : BaseModel
    {
        #region 属性
        /// <summary>
        /// 应用编号
        /// </summary>
        public string ApplicationId { get; set; }
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
        public string ApplicationCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }
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
        /// 下载地址
        /// </summary>
        public string DownloadUrl { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Application";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Sys_Application";
        }
        #endregion
    }

    /// <summary>
    /// Application主键
    /// </summary>
    public class ApplicationPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 应用编号
        /// </summary>
        public string ApplicationId { get; set; }
        #endregion
    }
}