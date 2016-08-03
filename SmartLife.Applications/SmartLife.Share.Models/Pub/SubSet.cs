
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-06-23 $
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
    /// SubSet模型
    /// </summary>
    public class SubSet : BaseModel
    {
        #region 属性
        /// <summary>
        /// 子集编号
        /// </summary>
        public string SubSetId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 子集名称
        /// </summary>
        public string SubSetName { get; set; }
        /// <summary>
        /// 集合编号
        /// </summary>
        public string SetId { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int? OrderNo { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 小部件编号
        /// </summary>
        public string WidgetId { get; set; }
        /// <summary>
        /// 小部件设置
        /// </summary>
        public string WidgetOption { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "SubSet";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_SubSet";
        }
        #endregion
    }

    /// <summary>
    /// SubSet主键
    /// </summary>
    public class SubSetPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 子集编号
        /// </summary>
        public string SubSetId { get; set; }
        #endregion
    }
}
