
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-02-16 $
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
    /// EPay_OldManAccount模型
    /// </summary>
    public class EPay_OldManAccount : BaseModel
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
        /// 老人Id
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// 老人名称
        /// </summary>
        public string OldManName { get; set; }
        /// <summary>
        /// 账户性质
        /// </summary>
        public string AccountNature { get; set; }
        /// <summary>
        /// 服务项目B
        /// </summary>
        public string ServeItemB { get; set; }
        /// <summary>
        /// 服务项目B名称
        /// </summary>
        public string ServeItemBName { get; set; }
        /// <summary>
        /// 服务计费类型
        /// </summary>
        public string FeeType { get; set; }
        /// <summary>
        /// 计费余额数
        /// </summary>
        public decimal? Remain { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "EPay_OldManAccount";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_EPay_OldManAccount";
        }
        #endregion
    }

    /// <summary>
    /// EPay_OldManAccount主键
    /// </summary>
    public class EPay_OldManAccountPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}