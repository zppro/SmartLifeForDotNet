
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-06-06 $
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

namespace SmartLife.Share.Models.WeiXin.Meb
{
    /// <summary>
    /// ServiceAccount模型
    /// </summary>
    public class ServiceAccount : BaseModel
    {
        #region 属性
        /// <summary>
        /// 编码
        /// </summary>
        public string AccountCode { get; set; }
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
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部署地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 第三方用户唯一凭证
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 第三方用户唯一凭证密钥
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 地图接口
        /// </summary>
        public string MapAPI { get; set; }
        /// <summary>
        /// 全局唯一票据
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 原始Id
        /// </summary>
        public string RawId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ServiceAccount";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Meb_ServiceAccount";
        }
        #endregion
    }

    /// <summary>
    /// ServiceAccount主键
    /// </summary>
    public class ServiceAccountPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 编码
        /// </summary>
        public string AccountCode { get; set; }
        #endregion
    }
}