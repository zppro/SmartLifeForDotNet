
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-07-08 $
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
    /// Member模型
    /// </summary>
    public class Member : BaseModel
    {
        #region 属性
        /// <summary>
        /// 会员Id
        /// </summary>
        public Guid? MemberId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 密码哈希
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int? CheckInCount { get; set; }
        /// <summary>
        /// 上次登录
        /// </summary>
        public DateTime? LastCheckIn { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Member";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Auth_Member";
        }
        #endregion
    }

    /// <summary>
    /// Member主键
    /// </summary>
    public class MemberPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 会员Id
        /// </summary>
        public Guid? MemberId { get; set; }
        #endregion
    }
}