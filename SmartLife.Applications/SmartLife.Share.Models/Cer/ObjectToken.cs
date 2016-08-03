
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-05-09 $
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

namespace SmartLife.Share.Models.Cer
{
    /// <summary>
    /// ObjectToken模型
    /// </summary>
    public class ObjectToken : BaseModel
    {
        #region 属性
        /// <summary>
        /// 标记
        /// </summary>
        public Guid? Token { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 应用编号从
        /// </summary>
        public string ApplicationIdFrom { get; set; }
        /// <summary>
        /// 应用编号到
        /// </summary>
        public string ApplicationIdTo { get; set; }
        /// <summary>
        /// 对象类别
        /// </summary>
        public string ObjectType { get; set; }
        /// <summary>
        /// 对象Id
        /// </summary>
        public string ObjectId { get; set; }
        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime? ExpireOn { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ObjectToken";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Cer_ObjectToken";
        }
        #endregion
    }

    /// <summary>
    /// ObjectToken主键
    /// </summary>
    public class ObjectTokenPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 标记
        /// </summary>
        public Guid? Token { get; set; }
        #endregion
    }
}