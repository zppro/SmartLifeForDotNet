
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-09-12 $
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
    /// Location模型
    /// </summary>
    public class Location : BaseModel
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 来源表
        /// </summary>
        public string SourceTable { get; set; }
        /// <summary>
        /// 来源键值
        /// </summary>
        public string SourceId { get; set; }
        /// <summary>
        /// 定位时间
        /// </summary>
        public DateTime? LocateTime { get; set; }
        /// <summary>
        /// 定位经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 定位纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 定位类别
        /// </summary>
        public string LocateType { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Location";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_Location";
        }
        #endregion
    }

    /// <summary>
    /// Location主键
    /// </summary>
    public class LocationPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}