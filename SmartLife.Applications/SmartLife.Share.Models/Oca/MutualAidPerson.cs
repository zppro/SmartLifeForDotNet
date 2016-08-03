
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-06-04 $
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
    /// MutualAidPerson模型
    /// </summary>
    public class MutualAidPerson : BaseModel
    {
        #region 属性
        /// <summary>
        /// 互助人员Id
        /// </summary>
        public Guid? MutualAidPersonId { get; set; }
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
        /// 操作日期
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string MutualAidPersonName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 互助项目
        /// </summary>
        public string MutualAidItem { get; set; }
        /// <summary>
        /// 互助性质
        /// </summary>
        public string MutualAidNature { get; set; }
        /// <summary>
        /// 费用
        /// </summary>
        public decimal? Fee { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string InputCode1 { get; set; }
        /// <summary>
        /// 五笔码
        /// </summary>
        public string InputCode2 { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "MutualAidPerson";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_MutualAidPerson";
        }
        #endregion
    }

    /// <summary>
    /// MutualAidPerson主键
    /// </summary>
    public class MutualAidPersonPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 互助人员Id
        /// </summary>
        public Guid? MutualAidPersonId { get; set; }
        #endregion
    }
}