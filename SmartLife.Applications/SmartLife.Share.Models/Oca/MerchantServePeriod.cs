﻿
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-10-19 $
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
    /// MerchantServePeriod模型
    /// </summary>
    public class MerchantServePeriod : BaseModel
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 服务网点Id
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "MerchantServePeriod";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_MerchantServePeriod";
        }
        #endregion
    }

    /// <summary>
    /// MerchantServePeriod主键
    /// </summary>
    public class MerchantServePeriodPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}