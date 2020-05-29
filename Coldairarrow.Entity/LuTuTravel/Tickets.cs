using Coldairarrow.Util.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// Tickets
    /// </summary>
    [Table("Tickets")]
    public class Tickets
    {
        /// <summary>
        /// 门票ID
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32? Id { get; set; }

        /// <summary>
        /// 景区ID
        /// </summary>
        public Int32? supplier_id { get; set; }

        /// <summary>
        /// 门票标题
        /// </summary>
        public String title { get; set; }

        /// <summary>
        /// 门票类型;1普通票，2套票，3线路
        /// </summary>
        public Int32? type { get; set; }

        /// <summary>
        /// 短信发送类型;1二维码，2文字码
        /// </summary>
        public Int32? send_type { get; set; }

        /// <summary>
        /// 门票数量
        /// </summary>
        public Int32? amount { get; set; }

        /// <summary>
        /// 门票原价
        /// </summary>
        public Decimal? original_price { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        public Decimal? market_price { get; set; }

        /// <summary>
        /// 门票排序
        /// </summary>
        public Int32? sort_order { get; set; }

        /// <summary>
        /// 退票类型;1可退票，2 不可退票，3 审核退票 
        /// </summary>
        public Int32? refund_type { get; set; }

        /// <summary>
        /// 有效期类型;1有效日期，2固定日期
        /// </summary>
        public Int32? validity_type { get; set; }

        /// <summary>
        /// validity_type=1时下单成功开始有效期，以天计算
        /// </summary>
        public Int32? validity_day { get; set; }

        /// <summary>
        /// validity_type=2时固定开始时间
        /// </summary>
        public Int32? start_time { get; set; }

        /// <summary>
        /// validity_type=2时固定结束时间
        /// </summary>
        public Int32? expire_time { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public String sms_content { get; set; }

        /// <summary>
        /// 彩信内容
        /// </summary>
        public String mms_content { get; set; }

        /// <summary>
        /// 打印内容
        /// </summary>
        public String print_content { get; set; }

        /// <summary>
        /// 封面图片网址
        /// </summary>
        public String image { get; set; }

        /// <summary>
        /// 购买须知（重要信息）
        /// </summary>
        public String notice { get; set; }

        /// <summary>
        /// 介绍信息
        /// </summary>
        //public String description { get; set; }

        /// <summary>
        /// 是否是从第三方导入
        /// </summary>
        public Int32? is_import { get; set; }

        /// <summary>
        /// 实际价格(成人价)
        /// </summary>
        public Decimal? nett_price { get; set; }

        /// <summary>
        /// 实际价格(儿童价)
        /// </summary>
        public String nett_price2 { get; set; }

        /// <summary>
        /// 下单时是否必须提供身份证号码字段 1是，0否
        /// </summary>
        public Int32? must_id_number { get; set; }

    }
}