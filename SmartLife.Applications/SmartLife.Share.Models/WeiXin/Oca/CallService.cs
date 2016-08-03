
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-05-04 $
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

namespace SmartLife.Share.Models.WeiXin.Oca
{
    /// <summary>
    /// CallService模型
    /// </summary>
    public class CallService : BaseModel
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public Guid? CallServiceId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public int? CallSeconds { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public byte? DoStatus { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServiceQueueNo { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "CallService";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_CallService";
        }
        #endregion
    }

    /// <summary>
    /// CallService主键
    /// </summary>
    public class CallServicePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public Guid? CallServiceId { get; set; }
        #endregion
    }
}