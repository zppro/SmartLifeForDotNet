using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using e0571.web.core.Utils;
using e0571.web.core.DataAccess;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;

using SmartLife.Share.Behaviors;
using e0571.web.core.Model;
using SmartLife.Share.Models.Pub;
using System.Drawing;
using System.Text;

namespace AirCenter.Manage.CmsServices
{
    /// <summary>
    /// Uploadify 
    /// </summary>
    public class uploadify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Charset = "utf-8";

            //HttpPostedFile file = context.Request.Files["Filedata"];

            //if (file != null)
            //{
            //    string saveName = @context.Request["saveName"];
            //    string fileType = file.FileName.Substring(file.FileName.LastIndexOf("."));
            //    //string uploadPath = HttpContext.Current.Server.MapPath(@context.Request["folder"]);

            //    //创建文件目录
            //    string virpath = context.Request.Form["folderPath"];
            //    if (string.IsNullOrEmpty(virpath)) {
            //        virpath = @context.Request["folder"];
            //    }
            //    //设置虚拟目录URL
            //    string virDirectorySite = "../../ContentServices";
            //    //获取站点文件路径
            //    string rootpath = HttpContext.Current.Server.MapPath(virDirectorySite);
            //    string uploadPath = HttpContext.Current.Server.MapPath(virDirectorySite + virpath);
            //    if (!string.IsNullOrEmpty(uploadPath)) {
            //        uploadPath += @"\" + DateTime.Now.ToString("yyyy-MM");
            //    }
            //    FileAdapter.EnsurePath(uploadPath);

            //    //是否生成缩略图
            //    bool bThumb = false;
            //    if (!string.IsNullOrEmpty(context.Request.Form["bThumb"])) {
            //        bThumb = true;
            //    }
            //    //文件名
            //    string filename = file.FileName;
            //    string extname = filename.Substring(filename.LastIndexOf(".") + 1);

            //    string strSaveName = filename.Substring(0, filename.LastIndexOf(".")) + "_" + context.Request.Form["ResidentName"] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            //    string strSaveThumbName = strSaveName + "_mini";
            //    string filepath = "", thumbfilepath = "";
            //    if (string.IsNullOrEmpty(saveName))
            //    {
            //        filepath = uploadPath + "\\" + strSaveName + fileType;
            //        if (bThumb)
            //        {
            //            thumbfilepath = uploadPath + "\\" + strSaveThumbName + fileType;
            //        }
            //    }
            //    else
            //    {
            //        filepath = uploadPath + "\\" + saveName + fileType;
            //    }
            //    //保存原图
            //    file.SaveAs(filepath);
            //    string retError = "";   //判断是否成功生成缩略图
            //    if (!string.IsNullOrEmpty(thumbfilepath))
            //    {
            //        retError = MakeThumbnail(filepath, thumbfilepath, 80, 80, "cut", extname);    //保存缩略图
            //    }

            //    if (string.IsNullOrEmpty(retError)) {
            //        Attachment attachment = new Attachment();
            //        attachment.AttachmentId = Guid.NewGuid();
            //        attachment.OriginName = filename;
            //        attachment.SourceTable = context.Request.Form["SourceTable"];
            //        attachment.SourceId = context.Request.Form["ResidentId"];
            //        attachment.AttachmentTag = context.Request.Form["AttachmentTag"];
            //        attachment.Suffix = extname;
            //        attachment.AttachmentSize = file.ContentLength;
            //        attachment.SaveName = filepath.Replace(rootpath, "").Replace("\\", "/");
            //        attachment.SaveThumbName = thumbfilepath.Replace(rootpath, "").Replace("\\", "/");
            //        if (SaveImgInfo(attachment))
            //        {
            //            //context.Response.Write(attachment.ToJson());
            //        }
            //        else {
            //            context.Response.Write("0");
            //        }
            //    }
            //    //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
            //    context.Response.Write("1");
            //}
            //else
            //{
            //    context.Response.Write("0");
            //}  

            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            HttpPostedFile file = context.Request.Files["Filedata"];

            if (file != null)
            {
                //创建文件目录
                string virPath = context.Request.Form["virPath"];
                if (string.IsNullOrEmpty(virPath))
                {
                    virPath = "/others";
                }

                string virRoot = "ContentServices";
                string virFullPath = (virPath.StartsWith("/") ? virPath : "/" + virPath);
                if (!virFullPath.EndsWith("/"))
                {
                    virFullPath += "/";
                }
                string dateTimeBlock = DateTime.Now.ToString("yyyy-MM");
                virFullPath += dateTimeBlock;
                string fileSavePath = HttpContext.Current.Server.MapPath("/" + virRoot + virFullPath);
                FileAdapter.EnsurePath(fileSavePath);

                //文件名 
                string originName = file.FileName;
                string extname = originName.Substring(originName.LastIndexOf(".") + 1);

                string fileNamePart = originName.Substring(0, originName.LastIndexOf(".")) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + GetRandomString(4);
                string fileName = fileNamePart + "." + extname;
                string fileThumbName = fileNamePart + "_thumb." + extname;

                string filepath = "", thumbfilepath = "";
                filepath = fileSavePath + @"\" + fileName;
                //是否生成缩略图 
                if (needMakeThumbnail(extname))
                {
                    thumbfilepath = fileSavePath + @"\" + fileThumbName;
                }
                //保存
                file.SaveAs(filepath);
                string retError = "";   //判断是否成功生成缩略图
                if (!string.IsNullOrEmpty(thumbfilepath))
                {
                    retError = MakeThumbnail(filepath, thumbfilepath, 80, 80, "cut", extname);    //保存缩略图
                }

                string retValue = "1";
                try
                {
                    if (string.IsNullOrEmpty(retError))
                    {
                        Attachment attachment = new Attachment();
                        attachment.AttachmentId = Guid.NewGuid();
                        attachment.OriginName = originName;
                        attachment.SourceTable = context.Request.Form["SourceTable"];
                        attachment.SourceId = context.Request.Form["ArticleId"];
                        attachment.AttachmentTag = context.Request.Form["AttachmentTag"];
                        attachment.Suffix = extname;
                        attachment.AttachmentSize = file.ContentLength;
                        attachment.SaveName = virFullPath + "/" + fileName;
                        attachment.SaveThumbName = virFullPath + "/" + fileThumbName;

                        BuilderFactory.DefaultBulder(context.Request.Form["ConnectId"]).Create<Attachment>(attachment);

                        retValue = attachment.AttachmentId.ToString();
                    }
                }
                catch(Exception ex){
                    
                }
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                context.Response.Write(retValue);
            }
            else
            {
                context.Response.Write("0");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private string GetRandomString(int length)
        {
            StringBuilder sb = new StringBuilder();
            Random r = new Random();

            while (length > 0)
            {
                sb.Append(r.Next(9));
                length--;
            }
            return sb.ToString();
        }
        private bool needMakeThumbnail(string ext)
        {
            string[] imageExtNames = new string[] { "png", "bmp", "jpg", "gif" };
            return imageExtNames.Contains(ext.ToLower());
        }
        protected bool SaveImgInfo(Attachment attachment)
        {
            bool bSuccess = true;
            try {

                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (attachment.AttachmentId == GlobalManager.GuidAsAutoGenerate)
                {
                    attachment.AttachmentId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = attachment.GetCreateMethodName(), ParameterObject = attachment.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
            }
            catch(Exception ex) {
                bSuccess = false;
            }
            return bSuccess;
        }

        /// 生成缩略图 
        /// </summary> 
        /// <param name="originalImagePath">源图路径</param> 
        /// <param name="thumbnailPath">缩略图路径</param> 
        /// <param name="width">缩略图宽度</param> 
        /// <param name="height">缩略图高度</param> 
        /// <param name="mode">生成缩略图的方式:HW指定高宽缩放(可能变形);W指定宽，高按比例 H指定高，宽按比例 Cut指定高宽裁减(不变形)</param>　　 
        /// <param name="mode">要缩略图保存的格式(gif,jpg,bmp,png) 为空或未知类型都视为jpg</param>　　 
        protected string MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, string imageType)
        {
            string retError = "";
            Image originalImage = Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）
                    break;
                case "W"://指定宽，高按比例
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例 
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片 
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
               new Rectangle(x, y, ow, oh),
               GraphicsUnit.Pixel);
            try
            {
                //以jpg格式保存缩略图 
                switch (imageType.ToLower())
                {
                    case "gif":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "jpg":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "bmp":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "png":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                }
            }
            catch (Exception ex)
            {
                retError = ex.ToString();
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

            return retError;
        }

    }
}