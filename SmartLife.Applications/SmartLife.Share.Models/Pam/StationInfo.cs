﻿
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-10-27 $
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

namespace SmartLife.Share.Models.Pam
{
    /// <summary>
    /// StationInfo模型
    /// </summary>
    public class StationInfo : BaseModel
    {
        #region 属性
        /// <summary>
        /// 服务网点Id
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
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
        /// 自助服务限额
        /// </summary>
        public byte? SelfServiceLimit { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "StationInfo";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pam_StationInfo";
        }
        #endregion
    }

    /// <summary>
    /// StationInfo主键
    /// </summary>
    public class StationInfoPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 服务网点Id
        /// </summary>
        public Guid? StationId { get; set; }
        #endregion
    }
}
