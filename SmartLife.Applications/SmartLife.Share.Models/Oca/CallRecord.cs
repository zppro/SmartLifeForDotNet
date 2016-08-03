
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-01-18 $
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
    /// CallRecord模型
    /// </summary>
    public class CallRecord : BaseModel
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Caller { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Callee { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 呼叫服务Id
        /// </summary>
        public Guid? CallServiceId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "CallRecord";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_CallRecord";
        }
        #endregion
    }

    /// <summary>
    /// CallRecord主键
    /// </summary>
    public class CallRecordPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}
