
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2012-11-12 $
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
    /// GroupPermit模型
    /// </summary>
    public class GroupPermit : BaseModel
    {
        #region 属性
        /// <summary>
        /// 组编号
        /// </summary>
        public Guid? GroupId { get; set; }
        /// <summary>
        /// 资源编号
        /// </summary>
        public string ResourceId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 录入时间
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
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "GroupPermit";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_GroupPermit";
        }
        #endregion
    }

    /// <summary>
    /// GroupPermit主键
    /// </summary>
    public class GroupPermitPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 组编号
        /// </summary>
        public Guid? GroupId { get; set; }
        /// <summary>
        /// 资源编号
        /// </summary>
        public string ResourceId { get; set; }
        #endregion
    }
}