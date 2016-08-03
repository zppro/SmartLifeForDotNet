
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-06-20 $
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

namespace SmartLife.Share.Models.Bas
{
    /// <summary>
    /// FamilyBaseInfo模型
    /// </summary>
    public class FamilyBaseInfo : BaseModel
    {
        #region 属性
        /// <summary>
        /// 家庭Id
        /// </summary>
        public Guid? FamilyId { get; set; }
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
        /// 户主姓名
        /// </summary>
        public string HouseHolderName { get; set; }
        /// <summary>
        /// 迁入时间
        /// </summary>
        public DateTime? ImmigrationTime { get; set; }
        /// <summary>
        /// 住户类型
        /// </summary>
        public string HouseHoldType { get; set; }
        /// <summary>
        /// 家庭荣誉
        /// </summary>
        public string FamilyHonor { get; set; }
        /// <summary>
        /// 家庭类型
        /// </summary>
        public string FamilyType { get; set; }
        /// <summary>
        /// 单亲家庭
        /// </summary>
        public byte? SingleParent { get; set; }
        /// <summary>
        /// 户籍城区
        /// </summary>
        public string ResidenceDistrict { get; set; }
        /// <summary>
        /// 房屋Id
        /// </summary>
        public Guid? HouseId { get; set; }
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
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "FamilyBaseInfo";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Bas_FamilyBaseInfo";
        }
        #endregion
    }

    /// <summary>
    /// FamilyBaseInfo主键
    /// </summary>
    public class FamilyBaseInfoPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 家庭Id
        /// </summary>
        public Guid? FamilyId { get; set; }
        #endregion
    }
}