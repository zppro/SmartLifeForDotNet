
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-06-15 $
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
    /// Group模型
    /// </summary>
    public class Group : BaseModel
    {
        #region 属性
        /// <summary>
        /// 组编号
        /// </summary>
        public Guid? GroupId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 组编码
        /// </summary>
        public string GroupCode { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 组类型
        /// </summary>
        public string GroupType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 系统标识
        /// </summary>
        public byte? SystemFlag { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string InputCode1 { get; set; }
        /// <summary>
        /// 五笔码
        /// </summary>
        public string InputCode2 { get; set; }
        /// <summary>
        /// 所属省市
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 所属服务站
        /// </summary>
        public Guid? StationId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Group";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_Group";
        }
        #endregion
    }

    /// <summary>
    /// Group主键
    /// </summary>
    public class GroupPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 组编号
        /// </summary>
        public Guid? GroupId { get; set; }
        #endregion
    }
}