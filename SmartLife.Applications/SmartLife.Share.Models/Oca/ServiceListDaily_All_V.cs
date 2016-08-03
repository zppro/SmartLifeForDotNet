
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-11-12 $
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
    /// ServiceListDaily_All_V模型
    /// </summary>
    public class ServiceListDaily_All_V : BaseModel
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServeStationName { get; set; }
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
        public string LongitudeS { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string LatitudeS { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServeManName { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? ServeManArriveTime { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? ServeManLeaveTime { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServeCountTime { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? ReserveDate { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ReserveFrom { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ReserveTo { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServeDuration { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServeItemB { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string DoStatus { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? WorkOrderId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public int? ServiceReserveId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ServiceListDaily_All_V";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_ServiceListDaily_All_V";
        }
        #endregion
    }

    /// <summary>
    /// ServiceListDaily_All_V主键
    /// </summary>
    public class ServiceListDaily_All_VPK : IPrimaryKeys
    {
        #region 属性
        #endregion
    }
}