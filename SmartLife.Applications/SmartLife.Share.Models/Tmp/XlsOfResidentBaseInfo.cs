
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2016-06-14 $
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

namespace SmartLife.Share.Models.Tmp
{
    /// <summary>
    /// XlsOfResidentBaseInfo模型
    /// </summary>
    public class XlsOfResidentBaseInfo : BaseModel
    {
        #region 属性
        public Guid? ResidentId { get; set; }

        public int? Id { get; set; }

        public DateTime? CheckInTime { get; set; }

        public byte? Status { get; set; }

        public Guid? OperatedBy { get; set; }

        public DateTime? OperatedOn { get; set; }

        public string DataSource { get; set; }

        public string ResidentName { get; set; }

        public string Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string IDNo { get; set; }

        public byte? HealthInsuranceFlag { get; set; }

        public string HealthInsuranceNumber { get; set; }

        public byte? SocialInsuranceFlag { get; set; }

        public string SocialInsuranceNumber { get; set; }

        public string LivingState { get; set; }

        public string OldManIdentity { get; set; }

        public string NativePlace { get; set; }

        public string EducationLevel { get; set; }

        public string EducationLevelNodes { get; set; }

        public string Marriage { get; set; }

        public string LivingStatus { get; set; }

        public string HousingStatus { get; set; }

        public string HousingStatusNodes { get; set; }

        public string IncomeStatus { get; set; }

        public string PlaceOfHouseholdRegister { get; set; }

        public string Nation { get; set; }

        public string AccountType { get; set; }

        public string AreaId { get; set; }

        public string AreaId2 { get; set; }

        public string AreaId3 { get; set; }

        public string Address { get; set; }

        public string LongitudeS { get; set; }

        public string LatitudeS { get; set; }

        public string PostCode { get; set; }

        public string Tel { get; set; }

        public string Mobile { get; set; }

        public string CallNo { get; set; }

        public string Remark { get; set; }

        public byte? ResidentStatus { get; set; }

        public string InputCode1 { get; set; }

        public string InputCode2 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string FamilyName1 { get; set; }

        public string CallNo1 { get; set; }

        public string FamilyName2 { get; set; }

        public string CallNo2 { get; set; }

        public string FamilyName3 { get; set; }

        public string CallNo3 { get; set; }
        #endregion

        #region 重写方法
        public override string GetMappingModelName()
        {
            return "XlsOfResidentBaseInfo";
        }

        public override string GetMappingTableName()
        {
            return "Tmp_XlsOfResidentBaseInfo";
        }
        #endregion
    }

    /// <summary>
    /// XlsOfResidentBaseInfo主键
    /// </summary>
    public class XlsOfResidentBaseInfoPK : IPrimaryKeys
    {
        #region 属性
        public Guid? ResidentId { get; set; }
        #endregion
    }
}