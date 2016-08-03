
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-02-14 $
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
    /// Accounting_Period模型
    /// </summary>
    public class Accounting_Period : BaseModel
    {
        #region 属性
        /// <summary>
        /// 会计类型
        /// </summary>
        public string AccountingType { get; set; }
        /// <summary>
        /// 会计类型对象
        /// </summary>
        public string AccountingTypeObject { get; set; }
        /// <summary>
        /// 期间
        /// </summary>
        public string PeriodId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 锁定标识
        /// </summary>
        public byte? LockFlag { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Accounting_Period";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_Accounting_Period";
        }
        #endregion
    }

    /// <summary>
    /// Accounting_Period主键
    /// </summary>
    public class Accounting_PeriodPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}