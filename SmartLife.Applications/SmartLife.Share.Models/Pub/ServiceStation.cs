
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-09-21 $
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
    /// ServiceStation模型
    /// </summary>
    public class ServiceStation : BaseModel
    {
        #region 属性
        /// <summary>
        /// 服务站编号
        /// </summary>
        public Guid? StationId { get; set; }
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
        /// 服务站编码
        /// </summary>
        public string StationCode { get; set; }
        /// <summary>
        /// 服务站名称
        /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// 服务站类型
        /// </summary>
        public string StationType { get; set; }
        /// <summary>
        /// 服务站类型2
        /// </summary>
        public string StationType2 { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string DataSource { get; set; }
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
        /// 服务站地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 服务站地址经度
        /// </summary>
        public string LongitudeS { get; set; }
        /// <summary>
        /// 服务站地址纬度
        /// </summary>
        public string LatitudeS { get; set; }
        /// <summary>
        /// 服务热线
        /// </summary>
        public string Hotline { get; set; }
        /// <summary>
        /// 服务站座机
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 服务站传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 服务站电邮
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 服务站邮编
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 服务站联系人
        /// </summary>
        public string LinkMan { get; set; }
        /// <summary>
        /// 服务站联系人手机
        /// </summary>
        public string LinkManMobile { get; set; }
        /// <summary>
        /// 服务站简介
        /// </summary>
        public string Intro { get; set; }
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
            return "ServiceStation";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_ServiceStation";
        }
        #endregion
    }

    /// <summary>
    /// ServiceStation主键
    /// </summary>
    public class ServiceStationPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 服务站编号
        /// </summary>
        public Guid? StationId { get; set; }
        #endregion
    }
}