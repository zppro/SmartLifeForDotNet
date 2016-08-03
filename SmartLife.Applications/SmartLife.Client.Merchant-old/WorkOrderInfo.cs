using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client.Merchant
{
    public class WorkOrderInfo
    {
        public string WorkOrderId { get; set; }
        public string WorkOrderNo { get; set; }
        public string ServiceTimeRequired { get; set; }
        public string WorkOrderContent { get; set; }
        public string OldManName { get; set; }
        public string Address { get; set; }
        public string ServeManArriveTime { get; set; }
        public string ServeManLeaveTime { get; set; }
        public string ServiceTime { get; set; }
        public string FeedbackToServiceStationName { get; set; }

        public string DoStatusName { get; set; }
        public string AccessPoint { get; set; }
        public Guid? StationId { get; set; }

        public static string getDoStatusName(int doStatus)
        {
            string result = "";
            switch (doStatus)
            {
                case 0:
                    result = "派单中";
                    break;
                case 1:
                    result = "处置中";
                    break;
                case 2:
                    result = "督办中";
                    break;
                case 3:
                    result = "回访中";
                    break;
                case 4:
                    result = "回访完成";
                    break;
                default:
                    break;
            }
            return result;
        }
        public static string getGenderName(string gender)
        {
            string result = "";
            switch (gender)
            {
                case "F":
                    result = "女";
                    break;
                case "M":
                    result = "男";
                    break;
                default:
                    result = "未知";
                    break;
            }
            return result;
        }

    }
}
