
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-06-22 $
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
    /// CC_Group模型
    /// </summary>
    public class CC_Group : BaseModel
    {
        #region 属性
        /// <summary>
        /// 组Id
        /// </summary>
        public Guid? GroupId { get; set; }
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
        /// 数据来源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 对应被叫号码
        /// </summary>
        public string Callee { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "CC_Group";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_CC_Group";
        }
        #endregion
    }

    /// <summary>
    /// CC_Group主键
    /// </summary>
    public class CC_GroupPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 组Id
        /// </summary>
        public Guid? GroupId { get; set; }
        #endregion
    }
}