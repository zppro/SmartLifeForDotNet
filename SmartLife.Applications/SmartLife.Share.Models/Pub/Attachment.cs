
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-12-16 $
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
    /// <summary>
    /// Attachment模型
    /// </summary>
    public class Attachment : BaseModel
    {
        #region 属性
        /// <summary>
        /// 附件编号
        /// </summary>
        public Guid? AttachmentId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 来源表
        /// </summary>
        public string SourceTable { get; set; }
        /// <summary>
        /// 来源编号
        /// </summary>
        public string SourceId { get; set; }
        /// <summary>
        /// 附件标签
        /// </summary>
        public string AttachmentTag { get; set; }
        /// <summary>
        /// 附件大小
        /// </summary>
        public long? AttachmentSize { get; set; }
        /// <summary>
        /// 后缀名
        /// </summary>
        public string Suffix { get; set; }
        /// <summary>
        /// 原始文件名
        /// </summary>
        public string OriginName { get; set; }
        /// <summary>
        /// 上传后文件名
        /// </summary>
        public string SaveName { get; set; }
        /// <summary>
        /// 略缩图文件名
        /// </summary>
        public string SaveThumbName { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Attachment";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_Attachment";
        }
        #endregion
    }

    /// <summary>
    /// Attachment主键
    /// </summary>
    public class AttachmentPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 附件编号
        /// </summary>
        public Guid? AttachmentId { get; set; }
        #endregion
    }
}