
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-03-21 $
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
    /// DatabaseConnect模型
    /// </summary>
    public class DatabaseConnect : BaseModel
    {
        #region 属性
        /// <summary>
        /// 连接编号
        /// </summary>
        public string ConnectId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 连接名称
        /// </summary>
        public string ConnectName { get; set; }
        /// <summary>
        /// 提供者
        /// </summary>
        public string Provider { get; set; }
        /// <summary>
        /// 服务器名
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 用户代码
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 数据库
        /// </summary>
        public string DatabaseName { get; set; }
        /// <summary>
        /// IBATIS映射ID
        /// </summary>
        public string IBatisMapId { get; set; }
        /// <summary>
        /// IBATIS配置文件引用
        /// </summary>
        public string IbatisConfigFileRefer { get; set; }
        /// <summary>
        /// IBATIS配置文件位置
        /// </summary>
        public string IbatisConfigFileValue { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "DatabaseConnect";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Sys_DatabaseConnect";
        }
        #endregion
    }

    /// <summary>
    /// DatabaseConnect主键
    /// </summary>
    public class DatabaseConnectPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 连接编号
        /// </summary>
        public string ConnectId { get; set; }
        #endregion
    }
}