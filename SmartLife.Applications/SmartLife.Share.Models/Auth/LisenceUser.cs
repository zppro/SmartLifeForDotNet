
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-04-11 $
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

namespace SmartLife.Share.Models.Auth
{
    /// <summary>
    /// LisenceUser模型
    /// </summary>
    public class LisenceUser : BaseModel
    {
        #region 属性
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid? UserId { get; set; }
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
        /// 操作人
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }
        /// <summary>
        /// 密码哈希
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// 系统标识
        /// </summary>
        public byte? SystemFlag { get; set; }
        /// <summary>
        /// 停用标识
        /// </summary>
        public byte? StopFlag { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid? CreatedBy { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// 所属城市
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public string Area1 { get; set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public Guid? Area2 { get; set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public Guid? Area3 { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "LisenceUser";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Auth_LisenceUser";
        }
        #endregion
    }

    /// <summary>
    /// LisenceUser主键
    /// </summary>
    public class LisenceUserPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid? UserId { get; set; }
        #endregion
    }
}