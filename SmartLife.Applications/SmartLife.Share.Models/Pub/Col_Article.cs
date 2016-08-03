
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-10-15 $
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

namespace SmartLife.Share.Models.Pub
{
    public class Col_Article : BaseModel
    {
        public Guid? ArticleId { get; set; }
        public DateTime? PublishDate { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public IList<Attachment> Attachments { get; set; }

        public override string GetMappingModelName()
        {
            return string.Empty;
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return string.Empty;
        }
    }
}