using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// 产品开团时间表
    /// </summary>
    [Table("product_date")]
    public class product_date
    {

        /// <summary>
        /// product_date_id
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 product_date_id { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public Int32 product_id { get; set; }

        /// <summary>
        /// 当天是否开团
        /// </summary>
        public String is_opening { get; set; }

        /// <summary>
        /// 开团日期
        /// </summary>
        public DateTime open_date { get; set; }

        /// <summary>
        /// 成团人数
        /// </summary>
        public Int32? team_people_num { get; set; }

        /// <summary>
        /// 团数
        /// </summary>
        public Int32? team_num { get; set; }

    }
}