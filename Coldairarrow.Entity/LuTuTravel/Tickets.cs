using Coldairarrow.Util.Model;
using Dynamitey.DynamicObjects;
using System;
using System.Collections.Generic;
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
        public Int32 Id { get; set; }

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
        /// 支付类型：1：在线支付；2：景区到付
        /// </summary>
        public Int32? pay_type { get; set; }

        /// <summary>
        /// 门票数量 总库存
        /// </summary>
        public Int32? amount { get; set; }


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
        public String description { get; set; }

        /// <summary>
        /// 产品简介
        /// </summary>
        public String brief { get; set; }

        /// <summary>
        /// 是否是从第三方导入
        /// </summary>
        public Int32? is_import { get; set; }

        #region 价格
        /// <summary>
        /// 实际价格(成人价) //当天的分销价；分销商以此与管理员结算
        /// </summary>
        public Decimal? nett_price { get; set; }

        /// <summary>
        /// 当天的返现：订单完成消费（验证/核销）
        /// </summary>
        public Decimal? back_cash { get; set; }

        /// <summary>
        /// 门票原价 //当天的市场价/门市价/票面原价
        /// </summary>
        public Decimal? original_price { get; set; }

        /// <summary>
        /// 市场价 //当天的指导价/建议价；由管理员设置供分销商参考
        /// </summary>
        public Decimal? market_price { get; set; }

        /// <summary>
        /// SKU默认分销价
        /// </summary>
        public Decimal? sku_user_price { get; set; }

        /// <summary>
        /// SKU默认返现
        /// </summary>
        public Decimal? sku_back_cash { get; set; }

        /// <summary>
        /// SKU默认市场价
        /// </summary>
        public Decimal? sku_market_price { get; set; }

        /// <summary>
        /// SKU默认指导/建议价
        /// </summary>
        public Decimal? sku_suggest_price { get; set; }

        /// <summary>
        /// 实际价格(儿童价)
        /// </summary>
        public String nett_price2 { get; set; }

        #endregion

        /// <summary>
        /// 下单时是否必须提供身份证号码字段 1是，0否
        /// </summary>
        public Int32? must_id_number { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public String prov_name { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public String city_name { get; set; }
        /// <summary>
        /// 县城(省份--城市--县城)
        /// </summary>
        public String county_name { get; set; }

        /// <summary>
        /// 退票手续费类型：1: 按票数计算；2：按订单计算，每退一张票需扣手续费 1/票数；
        /// </summary>
        public Int32? refund_chanrge_type { get; set; }

        /// <summary>
        /// 退票手续费：数值，例 5.00 单位/元；结果等于百分比+固定值
        /// </summary>
        public Decimal? refund_chanrge { get; set; }

        /// <summary>
        /// 退票手续费百分比值：数值 按票价的百分比计算
        /// </summary>
        public Decimal? self_refund_scale { get; set; }

        /// <summary>
        /// 退票手续费固定值，单位/元
        /// </summary>
        public Decimal? self_refund_fixed { get; set; }

        /// <summary>
        /// 日历价格
        /// </summary>
        public List<Tickets_Date_Prices> date_prices { get; set; }


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