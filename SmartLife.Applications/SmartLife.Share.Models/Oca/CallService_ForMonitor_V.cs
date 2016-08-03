
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-09-29 $
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
    /// CallService_ForMonitor_V模型
    /// </summary>
    public class CallService_ForMonitor_V : BaseModel
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public Guid? CallServiceId { get; set; }
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
        public byte? Status { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string OperatedBy { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string AreaId2 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string AreaId3 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string LongitudeS { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string LatitudeS { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public int? CallSeconds { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string DoStatus { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string DoResults { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string OldManName { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string CallNo { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? ServiceQueueId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServiceQueueNo { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? ServiceExtId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServiceExtNo { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServiceCatalog { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public byte? AbandonFlag { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public int? DialBackCount { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? AbandonOn { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? LastDialBackOn { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? ServiceQueueIdOld { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServiceQueueNoOld { get; set; }
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
        public string UuidOfIPCC { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServiceArchive { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string PopFlag { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "CallService_ForMonitor_V";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_CallService_ForMonitor_V";
        }
        #endregion
    }

    /// <summary>
    /// CallService_ForMonitor_V主键
    /// </summary>
    public class CallService_ForMonitor_VPK : IPrimaryKeys
    {
        #region 属性
        #endregion
    }
}
