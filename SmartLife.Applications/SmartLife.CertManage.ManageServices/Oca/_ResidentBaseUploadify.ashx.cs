using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using e0571.web.core.Utils;
using e0571.web.core.TypeInherited;
using SmartLife.Share.Behaviors;
using SmartLife.Share.Models.Tmp;
using e0571.web.core.Model;
using e0571.web.core.DataAccess;
using e0571.web.core.Wcf;
using e0571.web.core.TypeExtension;
using System.Web.SessionState;

namespace SmartLife.CertManage.ManageServices.Oca
{
    /// <summary>
    /// _ResidentBaseUploadify 的摘要说明
    /// </summary>
    public class _ResidentBaseUploadify : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            HttpPostedFile file = context.Request.Files["Filedata"];

            if (file != null)
            {
                string AreaId = context.Request.Form["AreaId"];
                string AreaId2 = context.Request.Form["AreaId2"];
                string AreaId3 = context.Request.Form["AreaId3"];
                string conectId = context.Request.Form["ConnectId"];
                string iResidentStatus = context.Request.Form["ResidentStatus"];
                int iStatus = int.Parse(context.Request.Form["Status"]);

                IList<StringObjectDictionary> datas = NPOIManager.GetSheetData(file.InputStream, 0, true);
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                //数据转换
                List<ExcelError> errors = new List<ExcelError>();
                IList<StringObjectDictionary> basedatas = RetEffectiveDatas(datas, errors);

                XlsOfResidentBaseInfo residentBaseInfo = new XlsOfResidentBaseInfo() { Status = (byte)iStatus, DataSource = GlobalManager.DIKey_00012_UploadFile, OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now, AreaId = AreaId };
                foreach (var data in basedatas)
                {
                    residentBaseInfo.ResidentId = Guid.NewGuid();
                    StringObjectDictionary sod = residentBaseInfo.ToStringObjectDictionary(false);
                    IDictionary<string, object> dataItem = sod.MixInObject(data, false, e0571.web.core.Other.CaseSensitive.NORMAL);
                    dataItem["AreaId2"] = AreaId2;
                    dataItem["AreaId3"] = AreaId3;
                    dataItem["ResidentStatus"] = iResidentStatus;

                    statements.Add(new IBatisNetBatchStatement { StatementName = residentBaseInfo.GetCreateMethodName(), ParameterObject = dataItem, Type = SqlExecuteType.INSERT });
                }
                //先清空临时表
                BuilderFactory.DefaultBulder(conectId).ExecuteNativeSqlNoneQuery("Truncate table Tmp_XlsOfResidentBaseInfo");
                //插入数据
                BuilderFactory.DefaultBulder(conectId).ExecuteNativeSqlNoneQuery(statements);
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                context.Response.Write(errors.ToJson());
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
        private IList<StringObjectDictionary> RetEffectiveDatas(IList<StringObjectDictionary> datas,List<ExcelError> errorList)
        {
            IList<StringObjectDictionary> ret = new List<StringObjectDictionary>();

            if (datas.Count <= 0) return new List<StringObjectDictionary>();
            int count = 0;
            ExcelError er;
            foreach (var item in datas)
            {
                er = new ExcelError();
                er.ResidentName = e0571.web.core.Utils.TypeConverter.ChangeString(item["ResidentName"], "");
                er.IDNo = e0571.web.core.Utils.TypeConverter.ChangeString(item["IDNo"], "");
                er.CallNo = e0571.web.core.Utils.TypeConverter.ChangeString(item["CallNo"], "");
                er.AreaId2 = e0571.web.core.Utils.TypeConverter.ChangeString(item["AreaId2"], "");
                er.AreaId3 = e0571.web.core.Utils.TypeConverter.ChangeString(item["AreaId3"], "");

                if (string.IsNullOrEmpty(er.ResidentName))
                {
                    er.ErrorInfo = "姓名不能为空!";
                    errorList.Add(er);
                    continue;
                }
                if (string.IsNullOrEmpty(er.IDNo))
                {
                    er.ErrorInfo = "身份证不能为空!";
                    errorList.Add(er);
                    continue;
                }
                if (!CheckIDCard(er.IDNo))
                {
                    er.ErrorInfo = "身份证不合法!";
                    errorList.Add(er);
                    continue;
                }

                count = ret.Count(s => s["IDNo"].ToString() == er.IDNo);
                if (count > 0)
                {
                    er.ErrorInfo = "身份证重复!";
                    errorList.Add(er);
                    continue;
                }

                if (!string.IsNullOrEmpty(er.CallNo)) {
                    bool btrue = System.Text.RegularExpressions.Regex.IsMatch(er.CallNo, @"^[0-9]\d*$");
                   if (!btrue) {
                       er.ErrorInfo = "呼叫号码格式不正确!";
                       errorList.Add(er);
                       continue;
                   }

                   count = ret.Count(s => s["CallNo"].ToString() == er.CallNo);
                   if (count > 0)
                   {
                       er.ErrorInfo = "呼叫号码重复!";
                       errorList.Add(er);
                       continue;
                   }
                }
                

                ret.Add(item);
            }

            return ret;
        }

        /// <summary>
        /// 18位或者15位身份验证符合
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        private bool CheckIDCard(string Id)
        {
            //数字长度验证
            bool bfind = System.Text.RegularExpressions.Regex.IsMatch(Id, "(\\d{17}[\\d|X])|(\\d{15})");
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
    }

    public class ExcelError {
        public string ErrorInfo { get; set; }
        public string ResidentName { get; set; }
        public string IDNo { get; set; }
        public string CallNo { get; set; }
        public string AreaId2 { get; set; }
        public string AreaId3 { get; set; }
    }
}