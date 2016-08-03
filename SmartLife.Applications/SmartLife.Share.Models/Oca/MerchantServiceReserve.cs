
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-09-15 $
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
    /// MerchantServiceReserve模型
    /// </summary>
    public class MerchantServiceReserve : BaseModel
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
        /// 服务网点Id
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// 服务对象Id
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime? ReserveDate { get; set; }
        /// <summary>
        /// 预约From
        /// </summary>
        public string ReserveFrom { get; set; }
        /// <summary>
        /// 预约To
        /// </summary>
        public string ReserveTo { get; set; }
        /// <summary>
        /// 服务人员Id
        /// </summary>
        public Guid? ServeManId { get; set; }
        /// <summary>
        /// 服务人员姓名
        /// </summary>
        public string ServeManName { get; set; }
        /// <summary>
        /// 预约服务项目B
        /// </summary>
        public string ServeItemB { get; set; }
        /// <summary>
        /// 预约服务时长
        /// </summary>
        public string ServeDuration { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 更改标志
        /// </summary>
        public byte? ChangeFlag { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "MerchantServiceReserve";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_MerchantServiceReserve";
        }
        #endregion
    }

    /// <summary>
    /// MerchantServiceReserve主键
    /// </summary>
    public class MerchantServiceReservePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}