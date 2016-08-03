
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-05-04 $
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

namespace SmartLife.Share.Models.WeiXin.Oca
{
    /// <summary>
    /// ServiceWorkOrder模型
    /// </summary>
    public class ServiceWorkOrder : BaseModel
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public Guid? WorkOrderId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string WorkOrderContent { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServeItemA { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServeItemB { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? CallServiceId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? ServeStationId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ServeManName { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? ServeBeginTime { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ServiceWorkOrder";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_ServiceWorkOrder";
        }
        #endregion
    }

    /// <summary>
    /// ServiceWorkOrder主键
    /// </summary>
    public class ServiceWorkOrderPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public Guid? WorkOrderId { get; set; }
        #endregion
    }
}