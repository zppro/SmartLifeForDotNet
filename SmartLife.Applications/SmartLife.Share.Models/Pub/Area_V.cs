
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-10-15 $
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
    /// Area_V模型
    /// </summary>
    public class Area_V : BaseModel
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public byte? Levels { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public byte? EndFlag { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public int? OrderNo { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string InputCode1 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string InputCode2 { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Area_V";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_Area_V";
        }
        #endregion
    }

    /// <summary>
    /// Area_V主键
    /// </summary>
    public class Area_VPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public string AreaId { get; set; }
        #endregion
    }
}