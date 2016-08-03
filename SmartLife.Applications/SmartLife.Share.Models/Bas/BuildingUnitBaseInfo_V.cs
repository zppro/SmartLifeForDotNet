
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-07-03 $
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

namespace SmartLife.Share.Models.Bas
{
    /// <summary>
    /// BuildingUnitBaseInfo_V模型
    /// </summary>
    public class BuildingUnitBaseInfo_V : BaseModel
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public Guid? UnitId { get; set; }
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
        public Guid? OperatedBy { get; set; }
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
        public string UnitName { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? BuildingId { get; set; }
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
        public Guid? RoadId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "BuildingUnitBaseInfo_V";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Bas_BuildingUnitBaseInfo_V";
        }
        #endregion
    }

    /// <summary>
    /// BuildingUnitBaseInfo_V主键
    /// </summary>
    public class BuildingUnitBaseInfo_VPK : IPrimaryKeys
    {
        #region 属性
        #endregion
    }
}