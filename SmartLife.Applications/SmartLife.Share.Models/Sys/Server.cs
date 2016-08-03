
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-04-28 $
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
    /// Server模型
    /// </summary>
    public class Server : BaseModel
    {
        #region 属性
        /// <summary>
        /// 服务器编号
        /// </summary>
        public string ServerId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 服务器编码
        /// </summary>
        public string ServerCode { get; set; }
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 网络地址
        /// </summary>
        public string IpAddress { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Server";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Sys_Server";
        }
        #endregion
    }

    /// <summary>
    /// Server主键
    /// </summary>
    public class ServerPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 服务器编号
        /// </summary>
        public string ServerId { get; set; }
        #endregion
    }
}