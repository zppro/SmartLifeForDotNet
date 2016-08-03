
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-07-18 $
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
    /// ArticleColumnPermit模型
    /// </summary>
    public class ArticleColumnPermit : BaseModel
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public Guid? ColumnId { get; set; }
        /// <summary>
        /// 栏目ID
        /// </summary>
        public byte? Category { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public Guid? OBJ_ID { get; set; }
        /// <summary>
        /// 关联ID
        /// </summary>
        public byte? PermitType { get; set; }
        /// <summary>
        /// 权限类别
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ArticleColumnPermit";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_ArticleColumnPermit";
        }
        #endregion
    }

    /// <summary>
    /// ArticleColumnPermit主键
    /// </summary>
    public class ArticleColumnPermitPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}
