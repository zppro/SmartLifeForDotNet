
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-09-21 $
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
    public class ServiceStation_Light : BaseModel
    {
        #region 属性
        /// <summary>
        /// 服务站编号
        /// </summary>
        public Guid? StationId { get; set; }   
        /// <summary>
        /// 服务站编码
        /// </summary>
        public string StationCode { get; set; }
        /// <summary>
        /// 服务站名称
        /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// 服务站类型
        /// </summary>
        public string StationType { get; set; }
        /// <summary>
        /// 服务站类型2
        /// </summary>
        public string StationType2 { get; set; } 
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ServiceStation_Light";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_ServiceStation_Light";
        }
        #endregion
    }
}
