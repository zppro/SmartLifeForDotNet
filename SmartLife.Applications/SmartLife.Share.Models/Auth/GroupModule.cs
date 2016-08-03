
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-12-17 $
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

namespace SmartLife.Share.Models.Auth
{
    /// <summary>
    /// GroupModule模型
    /// </summary>
    public class GroupModule : BaseModel
    {
        #region 属性
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 模块编号
        /// </summary>
        public Guid? ModuleId { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 组编号
        /// </summary>
        public Guid? GroupId { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "GroupModule";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Auth_GroupModule";
        }
        #endregion
    }

    /// <summary>
    /// GroupModule主键
    /// </summary>
    public class GroupModulePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}