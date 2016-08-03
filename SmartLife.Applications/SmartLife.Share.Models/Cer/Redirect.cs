
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-04-22 $
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
    /// Redirect模型
    /// </summary>
    public class Redirect : BaseModel
    {
        #region 属性
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
        public string ApplicationIdFrom { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ApplicationIdTo { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string ObjectId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Redirect";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Cer_Redirect";
        }
        #endregion
    }

    /// <summary>
    /// Redirect主键
    /// </summary>
    public class RedirectPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// null
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}