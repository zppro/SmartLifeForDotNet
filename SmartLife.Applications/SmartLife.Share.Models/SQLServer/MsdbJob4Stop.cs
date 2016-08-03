
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
    public class MsdbJob4Stop
    {
        #region 属性
        public string job_name { get; set; }
        public Guid? job_id { get; set; }
        public string originating_server { get; set; }
        public string server_name { get; set; } 
        #endregion 
    } 
}
