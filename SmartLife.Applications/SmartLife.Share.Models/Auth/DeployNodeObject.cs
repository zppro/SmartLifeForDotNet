using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Share.Models.Auth
{
    public class DeployNodeObject
    {
        public string ObjectId { get; set; }
        public string ObjectItemId { get; set; }
        public string ObjectItemName { get; set; }
        public string ObjectParentId { get; set; }
        public byte? ObjectLevels { get; set; }
        public int? ObjectOrderNo { get; set; }
    }
}
