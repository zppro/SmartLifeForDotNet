
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-09-20 $
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
    /// OldManLocateInfo模型
    /// </summary>
    public class OldManLocateInfo : BaseModel
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
        /// 老人Id
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// 家庭地址经度
        /// </summary>
        public string HomeLongitudeS { get; set; }
        /// <summary>
        /// 家庭地址纬度
        /// </summary>
        public string HomeLatitudeS { get; set; }
        /// <summary>
        /// 定位时间
        /// </summary>
        public DateTime? LocateTime { get; set; }
        /// <summary>
        /// 定位经度
        /// </summary>
        public string LocateLongitudeS { get; set; }
        /// <summary>
        /// 定位纬度
        /// </summary>
        public string LocateLatitudeS { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "OldManLocateInfo";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_OldManLocateInfo";
        }
        #endregion
    }

    /// <summary>
    /// OldManLocateInfo主键
    /// </summary>
    public class OldManLocateInfoPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}