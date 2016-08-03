
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-09-22 $
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
    /// WXMessageTemplate模型
    /// </summary>
    public class WXMessageTemplate : BaseModel
    {
        #region 属性
        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateId { get; set; }
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
        /// 业务类型
        /// </summary>
        public string BizType { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string TemplateContent { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "WXMessageTemplate";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_WXMessageTemplate";
        }
        #endregion
    }

    /// <summary>
    /// WXMessageTemplate主键
    /// </summary>
    public class WXMessageTemplatePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateId { get; set; }
        #endregion
    }
}