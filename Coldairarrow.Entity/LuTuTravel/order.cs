using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// 订单表
    /// </summary>
    [Table("order")]
    public class order
    {

        /// <summary>
        /// 订单表主键id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 团表id
        /// </summary>
        public String team_id { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public String product_id { get; set; }

        /// <summary>
        /// product_name
        /// </summary>
        public String product_name { get; set; }

        /// <summary>
        /// 1成团购买 2单独购买
        /// </summary>
        public Int32 type { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public Int32 num { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public Decimal money { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 下单人联系方式
        /// </summary>
        public String phone { get; set; }

        /// <summary>
        /// 下单人姓名
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// 开团时间
        /// </summary>
        public DateTime start_time { get; set; }

        /// <summary>
        /// 订单状态 1下单成功 2已取消 3下单失败
        /// </summary>
        public Int32 status { get; set; }

        /// <summary>
        /// 下单失败原因
        /// </summary>
        public String fail_reason { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public String member_id { get; set; }

    }
}