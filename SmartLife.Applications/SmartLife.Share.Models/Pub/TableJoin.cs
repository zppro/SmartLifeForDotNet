
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-06-18 $
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
    /// TableJoin模型
    /// </summary>
    public class TableJoin : BaseModel
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
        /// 表名1
        /// </summary>
        public string TableName1 { get; set; }
        /// <summary>
        /// 表别名1
        /// </summary>
        public string TableAliasName1 { get; set; }
        /// <summary>
        /// 列名1
        /// </summary>
        public string ColumnName1 { get; set; }
        /// <summary>
        /// 表名2
        /// </summary>
        public string TableName2 { get; set; }
        /// <summary>
        /// 表别名2
        /// </summary>
        public string TableAliasName2 { get; set; }
        /// <summary>
        /// 列名2
        /// </summary>
        public string ColumnName2 { get; set; }
        /// <summary>
        /// 关联方式
        /// </summary>
        public string JoinType { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        public string Condition { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int? OrderNo { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "TableJoin";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_TableJoin";
        }
        #endregion
    }

    /// <summary>
    /// TableJoin主键
    /// </summary>
    public class TableJoinPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}
