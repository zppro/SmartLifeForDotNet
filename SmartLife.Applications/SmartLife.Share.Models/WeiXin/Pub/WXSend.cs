
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-09-29 $
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

namespace SmartLife.Share.Models.WeiXin.Pub
{
    /// <summary>
    /// WXSend模型
    /// </summary>
    public class WXSend : BaseModel
    {
        #region 属性
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 排队时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 预定发送时间
        /// </summary>
        public DateTime? ScheduleTime { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string toUserName { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string fromUserName { get; set; }
        /// <summary>
        /// 发送批号
        /// </summary>
        public string BatchNum { get; set; }
        /// <summary>
        /// 发送内容
        /// </summary>
        public string SendContent { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SendTime { get; set; }
        /// <summary>
        /// 发送结果
        /// </summary>
        public int? SendResult { get; set; }
        /// <summary>
        /// 来源类别
        /// </summary>
        public string SourceCatalog { get; set; }
        /// <summary>
        /// 来源表
        /// </summary>
        public string SourceTable { get; set; }
        /// <summary>
        /// 来源编号
        /// </summary>
        public string SourceId { get; set; }
        /// <summary>
        /// 发送类别
        /// </summary>
        public string SendCatalog { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "WXSend";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_WXSend";
        }
        #endregion
    }

    /// <summary>
    /// WXSend主键
    /// </summary>
    public class WXSendPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}