using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using e0571.web.core.Utils;
using e0571.web.core.TypeInherited;
using SmartLife.Share.Behaviors;
using e0571.web.core.Model;
using e0571.web.core.DataAccess;
using SmartLife.Share.Models.Oca;
using SmartLife.Share.Models.Pub;
using e0571.web.core.Wcf;
using e0571.web.core.TypeExtension;
using System.Web.SessionState;

namespace SmartLife.CertManage.ManageServices.Oca
{
    /// <summary>
    /// _OldCareOldManBaseDataUploadify 的摘要说明
    /// </summary>
    public class _OldCareOldManBaseDataUploadify : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            HttpPostedFile file = context.Request.Files["Filedata"];

            if (file != null)
            {
                string AreaId = context.Request.Form["AreaId"];
                string retFailInfo = "";
                int relationOfMan = 99999;
                int count = 0;

                IList<StringObjectDictionary> datas = NPOIManager.GetSheetData(file.InputStream, 0, true);
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                List<IBatisNetBatchStatement> oldmanconfig_statements = new List<IBatisNetBatchStatement>();
                List<IBatisNetBatchStatement> familymember_statements = new List<IBatisNetBatchStatement>();
                List<IBatisNetBatchStatement> oldmanfamily_statements = new List<IBatisNetBatchStatement>();

                //获取图片
                //NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file.InputStream);
                //NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                //Dictionary<string, Byte[]> picDataDictionary = getSheetPictrues03((NPOI.HSSF.UserModel.HSSFSheet)sheet);
                //获取所有老人基本信息
                IList<OldManBaseInfo> oldManBaseInfoList = RetExistsIdNo(AreaId);
                //获取所有老人配置终端信息
                IList<OldManConfigInfo> oldManConfigInfoList = RetAllConfigCallNo();
                //获取所有老人的亲属资料
                IList<StringObjectDictionary> familyMemberList = RetAllFamilyMember();
                //获取所有街道社区信息
                IList<Area> AreaList = RetAllStreetCommunity();
                //保存图片地址
                //基本信息
                IList<StringObjectDictionary> basedatas = RetEffectiveDatas(datas, ref retFailInfo);
                OldManBaseInfo baseInfo = new OldManBaseInfo() { DataSource = GlobalManager.DIKey_00012_UploadFile, OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now, AreaId = AreaId };
                OldManConfigInfo oldmanconfig = new OldManConfigInfo();
                FamilyMember familymember = new FamilyMember() { Gender = "M", IDNo = "", Status = 1 };
                OldManFamilyInfo oldmanfamily = new OldManFamilyInfo();

                foreach (var data in basedatas)
                {
                    //老人基本资料
                    baseInfo.OldManId = Guid.NewGuid();
                    count = oldManBaseInfoList.Count(s => s.IDNo == data["IDNo"].ToString());
                    if (count > 0)
                    {
                        baseInfo.OldManId = oldManBaseInfoList.First(s => s.IDNo == data["IDNo"].ToString()).OldManId;
                    }
                    baseInfo.Status = 1;
                    baseInfo.HealthInsuranceFlag = Convert.ToByte(data["HealthInsuranceNumber"] != null);
                    baseInfo.SocialInsuranceFlag = Convert.ToByte(data["SocialInsuranceNumber"] != null);
                    StringObjectDictionary sod = baseInfo.ToStringObjectDictionary(false);
                    IDictionary<string, object> dataItem = sod.MixInObject(data, false, e0571.web.core.Other.CaseSensitive.NORMAL);

                    var tmpArea = AreaList.FirstOrDefault(s => s.AreaName.IndexOf(dataItem["AreaId2"].ToString()) > -1);
                    if (dataItem.ContainsKey("AreaId2") && dataItem["AreaId2"] != null && tmpArea != null )
                    {
                        dataItem["AreaId2"] = tmpArea.AreaId != null ? tmpArea.AreaId.ToString().ToUpper() : "";
                    }
                    else
                    {
                        dataItem.Remove("AreaId2");
                    }
                    tmpArea = AreaList.FirstOrDefault(s => s.AreaName.IndexOf(dataItem["AreaId3"].ToString()) > -1);
                    if (dataItem.ContainsKey("AreaId3") && dataItem["AreaId3"] != null && tmpArea != null )
                    {
                        dataItem["AreaId3"] = tmpArea.AreaId != null ? tmpArea.AreaId.ToString().ToUpper() : "";
                    }
                    else
                    {
                        dataItem.Remove("AreaId3");
                    }

                    if (count > 0)
                    {
                        statements.Add(new IBatisNetBatchStatement { StatementName = baseInfo.GetUpdateMethodName(), ParameterObject = dataItem, Type = SqlExecuteType.UPDATE });
                    }
                    else
                    {
                        statements.Add(new IBatisNetBatchStatement { StatementName = baseInfo.GetCreateMethodName(), ParameterObject = dataItem, Type = SqlExecuteType.INSERT });
                    }
                    //老人呼叫设备资料,存在则跳过
                    if (dataItem.ContainsKey("CallNo") && dataItem["CallNo"] != null)
                    {
                        if (oldManConfigInfoList.Count(s => s.CallNo == dataItem["CallNo"].ToString()) == 0)
                        {
                            var configinfoitem = oldManConfigInfoList.FirstOrDefault(s => s.OldManId.ToString() == dataItem["OldManId"].ToString());
                            if (configinfoitem != null && configinfoitem.OldManId != null)
                            {
                                oldmanconfig_statements.Add(new IBatisNetBatchStatement { StatementName = oldmanconfig.GetUpdateMethodName(), ParameterObject = dataItem, Type = SqlExecuteType.UPDATE });
                            }
                            else
                            {
                                oldmanconfig_statements.Add(new IBatisNetBatchStatement { StatementName = oldmanconfig.GetCreateMethodName(), ParameterObject = dataItem, Type = SqlExecuteType.INSERT });
                            }
                        }
                    }

                    //获取亲人信息
                    StringObjectDictionary sod2 = new StringObjectDictionary();
                    for (int i = 0; i < 3; i++)
                    {
                        string familyName = "FamilyName" + (i + 1).ToString();
                        string callNo = "CallNo" + (i + 1).ToString();
                        if (!data.ContainsKey(familyName) || data[familyName] == null) continue;
                        if (!data.ContainsKey(callNo) || data[callNo] == null) continue;

                        oldmanfamily.RelationIdOfFamily = (relationOfMan - i).ToString();
                        oldmanfamily.RelationIdOfOldMan = (relationOfMan - i).ToString();

                        string callNoData = data[callNo].ToString();
                        familymember.FamilyMemberId = Guid.NewGuid();
                        familymember.FamilyMemberName = data[familyName].ToString();
                        //匹配手机号
                        familymember.Mobile = System.Text.RegularExpressions.Regex.Match(callNoData, @"0*[1][358]\d{9}").Value;
                        if (familymember.Mobile.Length > 0) callNoData = callNoData.Replace(familymember.Mobile, "");
                        //匹配座机
                        familymember.Tel = System.Text.RegularExpressions.Regex.Match(callNoData, @"((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))").Value;

                        if (count > 0)
                        {
                            sod2 = familyMemberList.FirstOrDefault(s => s["OldManId"].ToString() == baseInfo.OldManId.ToString() && s["RelationIdOfFamily"].ToString() == oldmanfamily.RelationIdOfFamily);
                            if (sod2 != null && sod2.Count > 0)
                            {
                                if (familyMemberList.Count(s => s["FamilyMemberName"].ToString() == familymember.FamilyMemberName && s["Mobile"].ToString() == familymember.Mobile && s["Tel"].ToString() == familymember.Tel) > 0) continue;
                                familymember.FamilyMemberId = sod2["FamilyMemberId"].ToString().ToGuid();
                            }
                        }
                        sod = baseInfo.ToStringObjectDictionary(false);
                        dataItem = sod.MixInObject(familymember, false, e0571.web.core.Other.CaseSensitive.NORMAL);
                        if (sod2 != null && sod2.Count > 0)
                        {
                            familymember_statements.Add(new IBatisNetBatchStatement { StatementName = familymember.GetUpdateMethodName(), ParameterObject = dataItem, Type = SqlExecuteType.UPDATE });
                        }
                        else
                        {
                            familymember_statements.Add(new IBatisNetBatchStatement { StatementName = familymember.GetCreateMethodName(), ParameterObject = dataItem, Type = SqlExecuteType.INSERT });
                            dataItem = sod.MixInObject(oldmanfamily, false, e0571.web.core.Other.CaseSensitive.NORMAL);
                            oldmanfamily_statements.Add(new IBatisNetBatchStatement { StatementName = oldmanfamily.GetCreateMethodName(), ParameterObject = dataItem, Type = SqlExecuteType.INSERT });
                        }

                    }

                }

                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(oldmanconfig_statements);
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(familymember_statements);
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(oldmanfamily_statements);


                /*string saveName = @context.Request["saveName"];
                string fileType = file.FileName.Substring(file.FileName.LastIndexOf("."));
                string uploadPath = HttpContext.Current.Server.MapPath(@context.Request["folder"]);
                FileAdapter.EnsurePath(uploadPath);
                if (string.IsNullOrEmpty(saveName))
                {
                    file.SaveAs(uploadPath + file.FileName);
                }
                else
                {
                    file.SaveAs(uploadPath + saveName + fileType);
                }
                */
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                context.Response.Write("1");
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

        /// <summary>
        /// 校对身份证及信息
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="failinfo"></param>
        /// <returns></returns>
        private IList<StringObjectDictionary> RetEffectiveDatas(IList<StringObjectDictionary> datas, ref string failinfo)
        {
            IList<StringObjectDictionary> ret = new List<StringObjectDictionary>();

            if (datas.Count <= 0) return new List<StringObjectDictionary>();
            
            string idNo = "";
            int count = 0;
            string callNo = "";
            string address = "";
            foreach (var item in datas)
            {
                if (item["IDNo"] == null || item["Address"] == null) continue;
                idNo = item["IDNo"].ToString();
                if (string.IsNullOrEmpty(idNo)) continue;
                address = item["Address"].ToString();
                if (string.IsNullOrEmpty(address)) continue;
                //if (!CheckIDCard(idNo))
                //{
                //    failinfo += "身份证: " + idNo + " 不合法！";
                //    continue;
                //}
                count = ret.Count(s => s["IDNo"].ToString() == idNo);
                if (count > 0)
                {
                    failinfo += "身份证: " + idNo + " 重复！";
                    continue;
                }
                if (item.ContainsKey("CallNo") && item["CallNo"] != null)
                {
                    callNo = item["CallNo"].ToString();
                    count = ret.Count(s => s["CallNo"] != null && s["CallNo"].ToString() == callNo);
                    if (count > 0)
                    {
                        failinfo += "呼叫号码: " + callNo + " 重复！";
                        item["CallNo"] = "";
                    }
                }
                
                ret.Add(item);
            }

            return ret;
        }

        /// <summary>
        /// 获取已存在的老人的身份证号码
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        private IList<OldManBaseInfo> RetExistsIdNo(string AreaId)
        {
            IList<OldManBaseInfo> baseListInfo;
            try
            {
                baseListInfo = BuilderFactory.DefaultBulder().List<OldManBaseInfo>(new OldManBaseInfo() { AreaId = AreaId });
            }
            catch (Exception ex)
            {
                baseListInfo = new List<OldManBaseInfo>();
            }
            return baseListInfo ?? new List<OldManBaseInfo>();
        }

        /// <summary>
        /// 取出当前库下的所有街道社区
        /// </summary>
        /// <returns></returns>
        private IList<Area> RetAllStreetCommunity()
        {
            IList<Area> areaListInfo;
            try
            {
                areaListInfo = BuilderFactory.DefaultBulder().List<Area>();
            }
            catch (Exception ex)
            {
                areaListInfo = new List<Area>();
            }
            return areaListInfo ?? new List<Area>();
        }

        /// <summary>
        /// 获取所有呼叫号码
        /// </summary>
        /// <returns></returns>
        private IList<OldManConfigInfo> RetAllConfigCallNo()
        {
            IList<OldManConfigInfo> configListInfo;
            try
            {
                configListInfo = BuilderFactory.DefaultBulder().List<OldManConfigInfo>();
            }
            catch (Exception ex)
            {
                configListInfo = new List<OldManConfigInfo>();
            }
            return configListInfo ?? new List<OldManConfigInfo>();
        }

        /// <summary>
        /// 获取所有老人的亲属资料
        /// </summary>
        /// <returns></returns>
        private IList<StringObjectDictionary> RetAllFamilyMember()
        {
            IList<StringObjectDictionary> result = new List<StringObjectDictionary>();
            try
            {
                string sqlstatment= "select a.OldManId,a.RelationIdOfFamily,a.RelationIdOfOldMan,b.FamilyMemberId,b.FamilyMemberName,b.Mobile,b.Tel"+
                                    " from Oca_OldManFamilyInfo a left join Oca_FamilyMember b on a.FamilyMemberId=b.FamilyMemberId ";
                result = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sqlstatment);
            }
            catch (Exception ex)
            {
                result = new List<StringObjectDictionary>();
            }
            return result;
        }

        /// <summary>
        /// 18位或者15位身份验证符合
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        private bool CheckIDCard(string Id)
        {
            //数字长度验证
            bool bfind  = System.Text.RegularExpressions.Regex.IsMatch(Id, "(\\d{17}[\\d|X])|(\\d{15})");
            if (!bfind) return false;
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            //省份验证
            if (address.IndexOf(Id.Remove(2)) == -1) return false;

            //生日验证或者符合15位身份证标准
            string birth = string.Empty;
            DateTime time = new DateTime();
            if (Id.Length == 18)
            {
                birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            }
            else if (Id.Length == 15)
            {
                birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            }
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;
            }

            //18位校验码验证
            if (Id.Length == 18)
            {
                //校验码验证
                string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                char[] Ai = Id.Remove(17).ToCharArray();
                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
                }
                int y = -1;
                Math.DivRem(sum, 11, out y);
                if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 读取excel2003的图片
        /// </summary>
        /// <param name="sheet">当前sheet对象</param>
        /// <returns></returns>
        private Dictionary<string, Byte[]> getSheetPictrues03(NPOI.HSSF.UserModel.HSSFSheet sheet)
        {
            Dictionary<string, Byte[]> sheetIndexPicMap = new Dictionary<string, Byte[]>();
            System.Collections.IList pictures = sheet.Workbook.GetAllPictures();
            if (pictures.Count > 0)
            {
                NPOI.HSSF.UserModel.HSSFPatriarch hp = (NPOI.HSSF.UserModel.HSSFPatriarch)sheet.DrawingPatriarch;
                foreach (NPOI.HSSF.UserModel.HSSFShape hs in hp.Children)
                {
                    NPOI.HSSF.UserModel.HSSFClientAnchor anchor = (NPOI.HSSF.UserModel.HSSFClientAnchor)hs.Anchor;
                    NPOI.HSSF.UserModel.HSSFPicture pic = (NPOI.HSSF.UserModel.HSSFPicture)hs;
                    NPOI.HSSF.UserModel.HSSFPictureData picData = (NPOI.HSSF.UserModel.HSSFPictureData)pic.PictureData;

                    string pciname = anchor.Row1.ToString() + "." + picData.SuggestFileExtension();
                    sheetIndexPicMap.Add(pciname, picData.Data);
                }
            }
            return sheetIndexPicMap;
        }

        ///// <summary>
        ///// 读取excel2007的图片
        ///// </summary>
        ///// <param name="sheet">当前sheet对象</param>
        ///// <returns></returns>
        //private Dictionary<string, Byte[]> getSheetPictrues07(XSSFSheet sheet)
        //{
        //    Dictionary<string, Byte[]> sheetIndexPicMap = new Dictionary<string, byte[]>();
        //    List<NPOI.POIXMLDocumentPart> pictures = sheet.GetRelations();
        //    foreach (NPOI.POIXMLDocumentPart pp in pictures)
        //    {
        //        XSSFDrawing drawing = (XSSFDrawing)pp;
        //        List<NPOI.POIXMLDocumentPart> pdplist = drawing.GetRelations();
        //        foreach (var iitem in pdplist)
        //        {
        //            string rid = iitem.GetPackageRelationship().Id;

        //            CT_TwoCellAnchor cttow = drawing.GetCTDrawing().TwoCellAnchors.Where(s => s.picture.blipFill.blip.embed == rid).FirstOrDefault();
        //            string sname = cttow.picture.nvPicPr.cNvPr.descr;

        //            XSSFPictureData xsd = (XSSFPictureData)iitem;

        //            sheetIndexPicMap.Add(sname, xsd.Data);
        //        }
        //    }

        //    return sheetIndexPicMap;
        //}

    }
}