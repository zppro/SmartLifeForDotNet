
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-04-08 $
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

namespace SmartLife.Share.Models.Dcc
{
    /// <summary>
    /// DayCareObjectBaseInfo模型
    /// </summary>
    public class DayCareObjectBaseInfo : BaseModel
    {
        #region 属性
        /// <summary>
        /// 老人Id
        /// </summary>
        public Guid? OldManId { get; set; }
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
        public string OldManName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 有无医保
        /// </summary>
        public byte? HealthInsuranceFlag { get; set; }
        /// <summary>
        /// 医保号码
        /// </summary>
        public string HealthInsuranceNumber { get; set; }
        /// <summary>
        /// 有无社保
        /// </summary>
        public byte? SocialInsuranceFlag { get; set; }
        /// <summary>
        /// 社保号码
        /// </summary>
        public string SocialInsuranceNumber { get; set; }
        /// <summary>
        /// 居住情况
        /// </summary>
        public string LivingState { get; set; }
        /// <summary>
        /// 身份情况
        /// </summary>
        public string OldManIdentity { get; set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public string AreaId2 { get; set; }
        /// <summary>
        /// 所属社区
        /// </summary>
        public string AreaId3 { get; set; }
        /// <summary>
        /// 家庭地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 家庭地址经度
        /// </summary>
        public string LongitudeS { get; set; }
        /// <summary>
        /// 家庭地址纬度
        /// </summary>
        public string LatitudeS { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 座机
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string InputCode1 { get; set; }
        /// <summary>
        /// 五笔码
        /// </summary>
        public string InputCode2 { get; set; }
        /// <summary>
        /// 居民状态
        /// </summary>
        public byte? ResidentStatus { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "DayCareObjectBaseInfo";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Dcc_DayCareObjectBaseInfo";
        }
        #endregion
    }

    /// <summary>
    /// DayCareObjectBaseInfo主键
    /// </summary>
    public class DayCareObjectBaseInfoPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 老人Id
        /// </summary>
        public Guid? OldManId { get; set; }
        #endregion
    }
}