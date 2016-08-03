
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-04-16 $
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
    /// OldManConfigInfo模型
    /// </summary>
    public class OldManConfigInfo : BaseModel
    {
        #region 属性
        /// <summary>
        /// 老人Id
        /// </summary>
        public Guid? OldManId { get; set; }
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
        /// 呼叫号码
        /// </summary>
        public string CallNo { get; set; }
        /// <summary>
        /// 定位开关
        /// </summary>
        public byte? LocateFlag { get; set; }
        /// <summary>
        /// 围栏半径
        /// </summary>
        public int? FenceRadius { get; set; }
        /// <summary>
        /// 政府统包开关
        /// </summary>
        public byte? GovTurnkeyFlag { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string CallNo2 { get; set; }
        /// <summary>
        /// 呼叫号码3
        /// </summary>
        public string CallNo3 { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "OldManConfigInfo";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_OldManConfigInfo";
        }
        #endregion
    }

    /// <summary>
    /// OldManConfigInfo主键
    /// </summary>
    public class OldManConfigInfoPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 老人Id
        /// </summary>
        public Guid? OldManId { get; set; }
        #endregion
    }
}