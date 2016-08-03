
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-09-09 $
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
    /// OldManBaseInfo_ForMonitor_V模型
    /// </summary>
    public class OldManBaseInfo_ForMonitor_V : BaseModel
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public Guid? OldManId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string OldManName { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public byte? HealthInsuranceFlag { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string HealthInsuranceNumber { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public byte? SocialInsuranceFlag { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string SocialInsuranceNumber { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string LivingState { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string OldManIdentity { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string AreaId2 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string AreaId3 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string LongitudeS { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string LatitudeS { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string InputCode1 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string InputCode2 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string AreaIdForCall { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string CallNo { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string CallNo2 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string CallNo3 { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public int? FenceRadius { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string LocateFlag { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string GovTurnkeyFlag { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string AssessLevel { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string familyinfo { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "OldManBaseInfo_ForMonitor_V";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Oca_OldManBaseInfo_ForMonitor_V";
        }
        #endregion
    }

    /// <summary>
    /// OldManBaseInfo_ForMonitor_V主键
    /// </summary>
    public class OldManBaseInfo_ForMonitor_VPK : IPrimaryKeys
    {
        #region 属性
        #endregion
    }
}
