using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.IO;
using e0571.web.core.Utils;
using e0571.web.core.TypeInherited;
using SmartLife.Share.Behaviors;
using e0571.web.core.Model;
using e0571.web.core.DataAccess;
using SmartLife.Share.Models.Bas;
using e0571.web.core.Wcf;
using e0571.web.core.TypeExtension;
using System.Web.SessionState;
using System.Drawing;

namespace SmartLife.CertManage.ManageServices.Pub
{
    /// <summary>
    /// _ArticleUploadify 的摘要说明
    /// </summary>
    
    public class _ArticleUploadify : IHttpHandler
    {
        private HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            //文件保存目录路径
            String savePath = "/CMU/Article";

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2,pdf");

            //最大文件大小
            int maxSize = 1000000;
            this.context = context;

            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            if (imgFile == null)
            {
                showError("请选择文件。");
            }

            String dirPath = context.Server.MapPath(savePath);

            String dirName = context.Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                showError("目录名不正确。");
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

            //创建文件夹

            //设置虚拟目录URL
            string virDirectorySite = "../../ContentServices";
            //获取站点文件路径
            string rootpath = HttpContext.Current.Server.MapPath(virDirectorySite);
            string uploadPath = HttpContext.Current.Server.MapPath(virDirectorySite + savePath);
            if (!string.IsNullOrEmpty(uploadPath))
            {
                uploadPath += @"\" + DateTime.Now.ToString("yyyy-MM");
            }
            FileAdapter.EnsurePath(uploadPath);

            //文件名
            string strSaveName = fileName.Substring(0, fileName.LastIndexOf(".")) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;

            string filepath = uploadPath + "\\" + strSaveName;

            //保存原图
            imgFile.SaveAs(filepath);

            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = "../" +virDirectorySite+ savePath + "/" + DateTime.Now.ToString("yyyy-MM") + "/" + strSaveName;

            string aa = "gif,jpg,jpeg,png,bmp";
            if (Array.IndexOf(aa.Split(','), fileExt.Substring(1).ToLower()) != -1)
            {
                Image imggg = Image.FromFile(filepath);
                if (imggg.Width > 500)
                {
                    imggg.Dispose();
                    MakeThumbnail(filepath, filepath, 500, 300, "W", fileExt);
                }

                if (imgFile.InputStream == null || imgFile.InputStream.Length > 500 * 1024)
                {
                    MakeThumbnail(filepath, filepath, 500, 300, "W", fileExt);
                }



            }   
      

            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(hash.ToJson());
            context.Response.End();
        }

        // <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径</param>
        /// <param name="thumbnailPath">缩略图路径</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式:HW指定高宽缩放(可能变形);W指定宽，高按比例 H指定高，宽按比例 Cut指定高宽裁减(不变形)</param>　　
        /// <param name="mode">要缩略图保存的格式(gif,jpg,bmp,png) 为空或未知类型都视为jpg</param>　　
        public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, string imageType)
        {
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
                    case ".gif":
                        originalImage.Dispose();
                        File.Delete(originalImagePath);
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".jpg":
                        originalImage.Dispose();
                        File.Delete(originalImagePath);
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".bmp":
                        originalImage.Dispose();
                        File.Delete(originalImagePath);
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".png":
                        originalImage.Dispose();
                        File.Delete(originalImagePath);
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    default:
                        originalImage.Dispose();
                        File.Delete(originalImagePath);
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                }


            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                bitmap.Dispose();
                g.Dispose();
            }
        }

        private void showError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(hash.ToJson());
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}