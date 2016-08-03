
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-04-03 $
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
    /// PensionAgencyUser模型
    /// </summary>
    public class PensionAgencyUser : BaseModel
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
        /// 机构Id
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// 账户Id
        /// </summary>
        public Guid? UserId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "PensionAgencyUser";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_PensionAgencyUser";
        }
        #endregion
    }

    /// <summary>
    /// PensionAgencyUser主键
    /// </summary>
    public class PensionAgencyUserPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 机构Id
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// 账户Id
        /// </summary>
        public Guid? UserId { get; set; }
        #endregion
    }
}
