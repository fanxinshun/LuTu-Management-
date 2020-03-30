using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// 支付表
    /// </summary>
    [Table("pay")]
    public class pay
    {

        /// <summary>
        /// 支付表id
        /// </summary>
        [Key, Column(Order = 1)]
        public String id { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        public String order_id { get; set; }

        /// <summary>
        /// 支付类型 1微信 2支付宝
        /// </summary>
        public Int32 type { get; set; }

        /// <summary>
        /// 支付流水号
        /// </summary>
        public String serial_number { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public Decimal money { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? pay_time { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 支付状态 0支付中 1支付成功 2支付失败 3退款成功 4退款失败
        /// </summary>
        public Int32 status { get; set; }

        /// <summary>
        /// 退款流水号
        /// </summary>
        public String return_number { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public Decimal? return_money { get; set; }

        /// <summary>
        /// 退款时间
        /// </summary>
        public DateTime? return_time { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String note { get; set; }

    }
}