
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
    public class MsdbJob4Update 
    {
        #region 属性
        public Guid? job_id { get; set; }  

        /*job update field*/
        public string new_name { get; set; }
        public byte? enabled { get; set; }
        public string description { get; set; }

        /*jobschedule update field*/
        public string schedule_name { get; set; }
        public int? freq_type { get; set; }
        public int? freq_interval { get; set; }
        public int? freq_subday_type { get; set; }
        public int? freq_subday_interval { get; set; }
        #endregion

       
    } 
}
