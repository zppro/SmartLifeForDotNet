
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
    public class MsdbJob : BaseModel
    {
        #region 属性
        public Guid? job_id { get; set; }
        public string originating_server { get; set; }
        public string name { get; set; }
        public byte? enabled { get; set; }
        public string description { get; set; }
        public string owner { get; set; }
        public int last_run_date { get; set; }
        public int last_run_time { get; set; }
        public byte? last_run_outcome { get; set; }
        public int next_run_date { get; set; }
        public int next_run_time { get; set; }
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

    public class MsdbJobPK : IPrimaryKeys
    {
        #region 属性 
        public Guid? job_id { get; set; }
        #endregion
    }
}
