
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-03-17 $
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
    /// WXButton模型
    /// </summary>
    public class WXButton : BaseModel
    {
        #region 属性
        /// <summary>
        /// 按钮编号
        /// </summary>
        public string ButtonId { get; set; }
        /// <summary>
        /// 按钮编码
        /// </summary>
        public string ButtonCode { get; set; }
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
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ButtonName { get; set; }
        /// <summary>
        /// 类型对应值
        /// </summary>
        public string KeyOrUrl { get; set; }
        /// <summary>
        /// 上级Id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 级次
        /// </summary>
        public byte? Levels { get; set; }
        /// <summary>
        /// 末级标识
        /// </summary>
        public byte? EndFlag { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int? OrderNo { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "WXButton";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_WXButton";
        }
        #endregion
    }

    /// <summary>
    /// WXButton主键
    /// </summary>
    public class WXButtonPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 按钮编号
        /// </summary>
        public string ButtonId { get; set; }
        #endregion
    }
}