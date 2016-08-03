using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLife.WeiXin.Model
{
    public class Search
    {
        public string name { get; set; }
        public string time { get; set; }
    }

    public class SearchList {
        public List<Search> rows { get; set; }
        public int sum { get; set; }
    }
}