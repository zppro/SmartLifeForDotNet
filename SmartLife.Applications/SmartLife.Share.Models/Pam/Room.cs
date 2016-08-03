
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-04-07 $
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

namespace SmartLife.Share.Models.Pam
{
    /// <summary>
    /// Room模型
    /// </summary>
    public class Room : BaseModel
    {
        #region 属性
        /// <summary>
        /// 房间序号
        /// </summary>
        public Guid? RoomId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
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
        /// 操作时间
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomNo { get; set; }
        /// <summary>
        /// 分机号
        /// </summary>
        public string ExtNo { get; set; }
        /// <summary>
        /// 床位数
        /// </summary>
        public int? BedNo { get; set; }
        /// <summary>
        /// 入住人数
        /// </summary>
        public int? OccupancyNumber { get; set; }
        /// <summary>
        /// 机构ID
        /// </summary>
        public Guid? StationId { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string FloorNo { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Room";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pam_Room";
        }
        #endregion
    }

    /// <summary>
    /// Room主键
    /// </summary>
    public class RoomPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 房间序号
        /// </summary>
        public Guid? RoomId { get; set; }
        #endregion
    }
}
