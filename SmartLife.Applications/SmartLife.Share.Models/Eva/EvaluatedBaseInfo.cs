
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-05-20 $
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

namespace SmartLife.Share.Models.Eva
{
    /// <summary>
    /// EvaluatedBaseInfo模型
    /// </summary>
    public class EvaluatedBaseInfo : BaseModel
    {
        #region 属性
        /// <summary>
        /// 居民Id
        /// </summary>
        public Guid? ResidentId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 记录状态
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
        public string ResidentName { get; set; }
        /// <summary>
        /// 居民状态
        /// </summary>
        public byte? ResidentStatus { get; set; }
        /// <summary>
        /// 居民业务身份
        /// </summary>
        public string ResidentBizId { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string NativePlace { get; set; }
        /// <summary>
        /// 户籍
        /// </summary>
        public string HouseholdRegister { get; set; }
        /// <summary>
        /// 文化程度
        /// </summary>
        public string EducationLevel { get; set; }
        /// <summary>
        /// 婚姻情况
        /// </summary>
        public string Marriage { get; set; }
        /// <summary>
        /// 居住环境
        /// </summary>
        public string LivingStatus { get; set; }
        /// <summary>
        /// 住房情况
        /// </summary>
        public string HousingStatus { get; set; }
        /// <summary>
        /// 收入情况
        /// </summary>
        public string IncomeStatus { get; set; }
        /// <summary>
        /// 居住所属辖区
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 居住所属街道
        /// </summary>
        public string AreaId2 { get; set; }
        /// <summary>
        /// 居住所属社区
        /// </summary>
        public string AreaId3 { get; set; }
        /// <summary>
        /// 居住地址
        /// </summary>
        public string ResidentialAddress { get; set; }
        /// <summary>
        /// 户籍所在地
        /// </summary>
        public string PlaceOfHouseholdRegister { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 住宅电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 手机号码
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
        /// 居住地址所在籍贯
        /// </summary>
        public string ResidentialOfHometown { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }
        /// <summary>
        /// 户口类别
        /// </summary>
        public string AccountType { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "EvaluatedBaseInfo";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Eva_EvaluatedBaseInfo";
        }
        #endregion
    }

    /// <summary>
    /// EvaluatedBaseInfo主键
    /// </summary>
    public class EvaluatedBaseInfoPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 居民Id
        /// </summary>
        public Guid? ResidentId { get; set; }
        #endregion
    }
}