
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-10-13 $
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

namespace SmartLife.Share.Models.Oca
{
    /// <summary>
    /// CallCenter模型
    /// </summary>
    public class CallCenter : BaseModel
    {
        #region 属性
        /// <summary>
        /// 服务网点Id
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int? Port { get; set; }
        /// <summary>
        /// 网关内网IP
        /// </summary>
        public string IPInner { get; set; }
        /// <summary>
        /// 网关内网端口
        /// </summary>
        public int? PortInner { get; set; }
        /// <summary>
        /// 小机IP
        /// </summary>
        public string IPProxy { get; set; }
        /// <summary>
        /// 小机端口
        /// </summary>
        public int? PortProxy { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "CallCenter";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_CallCenter";
        }
        #endregion
    }

    /// <summary>
    /// CallCenter主键
    /// </summary>
    public class CallCenterPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 服务网点Id
        /// </summary>
        public Guid? StationId { get; set; }
        #endregion
    }
}