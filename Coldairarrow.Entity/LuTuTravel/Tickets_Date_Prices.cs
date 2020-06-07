using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// Tickets_Date_Prices
    /// </summary>
    [Table("Tickets_Date_Prices")]
    public class Tickets_Date_Prices
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 门票Id
        /// </summary>
        public int Tickets_Id { get; set; }
        /// <summary>
        /// 本日时间
        /// </summary>
        public DateTime? date { get; set; }

        /// <summary>
        /// 本日设置的价格
        /// </summary>
        public Decimal? price { get; set; }

        /// <summary>
        /// 本日设置的库存
        /// </summary>
        public Int32? stock { get; set; }

        /// <summary>
        /// 本日设置的指导价
        /// </summary>
        public Decimal? suggest_price { get; set; }

        /// <summary>
        /// 本日设置的市场价
        /// </summary>
        public Decimal? market_price { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public String create_by { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public String update_by { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime update_time { get; set; }

    }
}