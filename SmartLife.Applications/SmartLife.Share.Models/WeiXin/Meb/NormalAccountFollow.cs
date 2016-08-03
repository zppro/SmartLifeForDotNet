
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-03-14 $
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
    /// NormalAccountFollow模型
    /// </summary>
    public class NormalAccountFollow : BaseModel
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// null
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// OpenId
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string FollowToIDNo { get; set; }
        /// <summary>
        /// 被关注者身份证号
        /// </summary>
        public string FollowToName { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "NormalAccountFollow";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Meb_NormalAccountFollow";
        }
        #endregion
    }

    /// <summary>
    /// NormalAccountFollow主键
    /// </summary>
    public class NormalAccountFollowPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}