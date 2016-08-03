
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-09-03 $
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
    /// MerchantOpenArea模型
    /// </summary>
    public class MerchantOpenArea : BaseModel
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
        /// 商家服务网点Id
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// 服务区域
        /// </summary>
        public string AreaId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "MerchantOpenArea";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_MerchantOpenArea";
        }
        #endregion
    }

    /// <summary>
    /// MerchantOpenArea主键
    /// </summary>
    public class MerchantOpenAreaPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 商家服务网点Id
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// 服务区域
        /// </summary>
        public string AreaId { get; set; }
        #endregion
    }
}