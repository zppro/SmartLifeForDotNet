
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-01-23 $
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
    /// InstitutionsCareCenter模型
    /// </summary>
    public class InstitutionsCareCenter : BaseModel
    {
        #region 属性
        /// <summary>
        /// 服务机构ID
        /// </summary>
        public Guid? StationId { get; set; }
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
        /// 操作日期
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 总床位数
        /// </summary>
        public int? OwnBeds { get; set; }
        /// <summary>
        /// 日托床位
        /// </summary>
        public int? DayCareBeds { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? BuildingArea { get; set; }
        /// <summary>
        /// 成立时间
        /// </summary>
        public DateTime? Established { get; set; }
        /// <summary>
        /// 平均日服务人数
        /// </summary>
        public decimal? AveDailyServices { get; set; }
        /// <summary>
        /// 登记性质
        /// </summary>
        public byte? RegistrationType { get; set; }
        /// <summary>
        /// 运营方式
        /// </summary>
        public byte? OperationMode { get; set; }
        /// <summary>
        /// 服务区域类型
        /// </summary>
        public byte? ServicesType { get; set; }        
        /// <summary>
        /// 入住数量
        /// </summary>
        public int? StayingNums { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "InstitutionsCareCenter";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_InstitutionsCareCenter";
        }
        #endregion
    }

    /// <summary>
    /// InstitutionsCareCenter主键
    /// </summary>
    public class InstitutionsCareCenterPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 服务机构ID
        /// </summary>
        public Guid? StationId { get; set; }
        #endregion
    }
}
