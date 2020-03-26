using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// 优惠券表
    /// </summary>
    [Table("product_coupons")]
    public class product_coupons
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String id { get; set; }

        /// <summary>
        /// 优惠券名称
        /// </summary>
        public String coupons_name { get; set; }

        /// <summary>
        /// 优惠券金额
        /// </summary>
        public Decimal? coupons_money { get; set; }

        /// <summary>
        /// 消费满减金额限制
        /// </summary>
        public Decimal? coupons_limit { get; set; }

        /// <summary>
        /// 可消费类型
        /// </summary>
        public String coupons_type { get; set; }

        /// <summary>
        /// 发放渠道(0:分享程序,1:分享界面,2:分款之后,3:首次注册,4:分享优惠券好友领取)
        /// </summary>
        public String coupons_ditch { get; set; }

        /// <summary>
        /// 发放张数
        /// </summary>
        public Int32? coupons_num { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? overdue_time { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_time { get; set; }

    }
}