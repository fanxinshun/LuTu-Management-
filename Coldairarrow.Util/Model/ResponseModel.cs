using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Util.Model
{
    /// <summary>
    /// Http返回数据模型1
    /// </summary>
    public class ResponseModel
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string total { get; set; }
        public object info { get; set; }
        public List<object> list { get; set; }
    }
}
