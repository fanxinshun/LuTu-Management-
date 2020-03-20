using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// 团表
    /// </summary>
    [Table("team")]
    public class team
    {

        /// <summary>
        /// 团表id
        /// </summary>
        [Key, Column(Order = 1)]
        public int Id { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public int product_id { get; set; }

        /// <summary>
        /// 1拼团中 2拼团成功 3拼团失败
        /// </summary>
        public Int32 status { get; set; }

        /// <summary>
        /// 开团日期
        /// </summary>
        public DateTime start_time { get; set; }

        /// <summary>
        /// 开团人id
        /// </summary>
        public String member_id { get; set; }

    }
}