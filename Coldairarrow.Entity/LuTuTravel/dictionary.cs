using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// 业务字典表
    /// </summary>
    [Table("dictionary")]
    public class dictionary
    {

        /// <summary>
        /// id
        /// </summary>
        [Key, Column(Order = 1)]
        public String id { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public String code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public Int32? sort { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public String value { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public String description { get; set; }

    }
}