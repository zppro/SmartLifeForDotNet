
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-09-10 $
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

namespace SmartLife.Share.Models.Pub
{
    /// <summary>
    /// ServiceStation_ForMonitor_V模型
    /// </summary>
    public class ServiceStation_ForMonitor_V : BaseModel
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string StationCode { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string StationType { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string StationType2 { get; set; }
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
        public string Address { get; set; }
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
        public string Hotline { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string LinkMan { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string LinkManMobile { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string InputCode1 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string InputCode2 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string serveitemB { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string serveArea { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public int? num { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ServiceStation_ForMonitor_V";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_ServiceStation_ForMonitor_V";
        }
        #endregion
    }

    /// <summary>
    /// ServiceStation_ForMonitor_V主键
    /// </summary>
    public class ServiceStation_ForMonitor_VPK : IPrimaryKeys
    {
        #region 属性
        #endregion
    }
}
