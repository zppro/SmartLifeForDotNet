using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using win.core.utils;
using System.Dynamic;
using System.Xml.Linq;
using System.Collections;

namespace SmartLife.Client.Gov
{
    class DictionaryItem
    {
        #region 属性
        public  string ItemCode { get; set; }
        public  string ItemId { get; set; }
        public  string ItemName { get; set; } 
        public  string Levels { get; set; }
        public  string OrderNo { get; set; }
        public  string ParentId { get; set; } 
        #endregion
         

        public static dynamic getDictionaryItemList(string dictionaryItem)
        {
            string url = GovVar.DataPointOfManage + "/Share/AjaxData/GetDictionaryItem/" + dictionaryItem;
            dynamic dyDictionaryItem = null;
            HttpAdapter.getSyncTo(url, (ret, res) =>
            {
                dyDictionaryItem = ret;
            });
            return dyDictionaryItem;
        }

        //public static ArrayList getStreetAndCommunityInArea(string parentId, int levels)
        //{
        //    string url = GovVar.DataPointOfManage + "/Share/AjaxData/GetStreetAndCommunityInArea/" + parentId;
             
        //    ArrayList arrList=new ArrayList();
        //    HttpAdapter.getSyncTo(url, (ret, res) =>
        //    { 
        //        foreach (var row in ret) {
        //            if (row.Levels.Value == levels) {
        //                arrList.Add(row);
        //            }
        //        }
        //    });
        //    return arrList;
        //}


        public static dynamic getStreetAndCommunity(string parentId)
        {
            string url = GovVar.DataPointOfManage + "/Share/AjaxData/GetStreetAndCommunityInArea/" + parentId;

            dynamic dyStreetAndCommunity = null;
            HttpAdapter.getSyncTo(url, (ret, res) =>
            {
                dyStreetAndCommunity = ret;
            });
            return dyStreetAndCommunity;
        }
    }
}
