using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// product_marketing
    /// </summary>
    [Table("product_marketing")]
    public class product_marketing
    {

        /// <summary>
        /// 全民营销表ID
        /// </summary>
        [Key, Column(Order = 1)]
        public String product_marketing_id { get; set; }

        /// <summary>
        /// product_id
        /// </summary>
        public Int32? product_id { get; set; }

        /// <summary>
        /// 营销开始时间
        /// </summary>
        public DateTime marketing_starttime { get; set; }

        /// <summary>
        /// 营销结束时间
        /// </summary>
        public DateTime marketing_endtime { get; set; }

        /// <summary>
        /// 市场价/建议零售价
        /// </summary>
        public Decimal? price { get; set; }

        /// <summary>
        /// 团购价
        /// </summary>
        public Decimal? team_price { get; set; }

        /// <summary>
        /// 团长返佣金额
        /// </summary>
        public Decimal? team_commission { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public String create_by { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_time { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public String update_by { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? update_time { get; set; }

    }
}