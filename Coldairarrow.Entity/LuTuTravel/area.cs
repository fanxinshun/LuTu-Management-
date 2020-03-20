using Coldairarrow.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// 区域表
    /// </summary>
    [Table("area")]
    public class area
    {

        /// <summary>
        /// 编码
        /// </summary>
        [Key, Column(Order = 1)]
        public String code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// 上级编码
        /// </summary>
        public String parent_code { get; set; }

    }
}