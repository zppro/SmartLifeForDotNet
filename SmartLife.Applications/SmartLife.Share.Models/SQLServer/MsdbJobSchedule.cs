
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-01-09 $
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

namespace SmartLife.Share.Models.SQLServer
{
    public class MsdbJobSchedule : BaseModel
    {
        #region 属性
        public int schedule_id { get; set; }
        public string schedule_name { get; set; } 
        public byte? enabled { get; set; }
        public int freq_type { get; set; }
        public int freq_interval { get; set; }
        public int freq_subday_type { get; set; }
        public int freq_subday_interval { get; set; } 
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "";
        }
        #endregion
    }

    public class MsdbJobSchedulePK : IPrimaryKeys
    {
        #region 属性 
        public int schedule_id { get; set; }
        #endregion
    }
}
