
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2015-04-07 $
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

namespace SmartLife.Share.Models.Pam
{
    /// <summary>
    /// ServeMan模型
    /// </summary>
    public class ServeMan : BaseModel
    {
        #region 属性
        /// <summary>
        /// 服务人员编号
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 呼叫器号码
        /// </summary>
        public string CallNo { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ServeMan";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pam_ServeMan";
        }
        #endregion
    }

    /// <summary>
    /// ServeMan主键
    /// </summary>
    public class ServeManPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 服务人员编号
        /// </summary>
        public Guid? UserId { get; set; }
        #endregion
    }
}
