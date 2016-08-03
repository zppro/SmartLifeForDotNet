
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-06-27 $
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
    /// SubSetColumn模型
    /// </summary>
    public class SubSetColumn : BaseModel
    {
        #region 属性
        /// <summary>
        /// 子集编号
        /// </summary>
        public string SubSetId { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 原列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 新列名
        /// </summary>
        public string ColumnNameNew { get; set; }
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
        /// 新中文列名
        /// </summary>
        public string ColumnCNameNew { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int? OrderNo { get; set; }
        /// <summary>
        /// 计算列标志
        /// </summary>
        public byte? ComputeColumnFlag { get; set; }
        /// <summary>
        /// 绑定表达式
        /// </summary>
        public string Expression { get; set; }
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
            return "SubSetColumn";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_SubSetColumn";
        }
        #endregion
    }

    /// <summary>
    /// SubSetColumn主键
    /// </summary>
    public class SubSetColumnPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 子集编号
        /// </summary>
        public string SubSetId { get; set; }
        /// <summary>
        /// 新列名
        /// </summary>
        public string ColumnNameNew { get; set; }
        #endregion
    }
}
