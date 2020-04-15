using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// 产品表
    /// </summary>
    [Table("product")]
    public class product
    {

        /// <summary>
        /// 产品id
        /// </summary>
        [Key, Column(Order = 1)]
        public int Id { get; set; }

        /// <summary>
        /// 数据有效性
        /// </summary>
        public String enable_flag { get; set; }

        /// <summary>
        /// 产品类型表id
        /// </summary>
        public String product_type_id { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public String supplier { get; set; }

        /// <summary>
        /// 线路名称,景点名称
        /// </summary>
        public String title { get; set; }

        /// <summary>
        /// 景区级别
        /// </summary>
        public String special_category { get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        public String area_code { get; set; }


        /// <summary>
        /// 目的地
        /// </summary>
        public String region { get; set; }

        /// <summary>
        /// 出发地
        /// </summary>
        public String region_start { get; set; }

        /// <summary>
        /// 预定须知
        /// </summary>
        public String content { get; set; }

        /// <summary>
        /// 产品特色介绍
        /// </summary>
        public String features { get; set; }

        /// <summary>
        /// 费用明细
        /// </summary>
        public String cost_detail { get; set; }

        /// <summary>
        /// 景点详情
        /// </summary>
        public String scenic_spot { get; set; }

        /// <summary>
        /// 线路推荐
        /// </summary>
        public String line_recommendation { get; set; }

        /// <summary>
        /// 产品图片地址，多张图片用“,"隔开
        /// </summary>
        public String images { get; set; }

        /// <summary>
        /// 发团时间段
        /// </summary>
        public DateTime? maximum_time_available { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public Decimal origin_price { get; set; }

        /// <summary>
        /// 市场价/建议零售价
        /// </summary>
        public Decimal price { get; set; }

        /// <summary>
        /// 团购价
        /// </summary>
        public Decimal team_price { get; set; }

        /// <summary>
        /// 团长返佣金额
        /// </summary>
        public Decimal team_commission { get; set; }

        /// <summary>
        /// 用户分享所得优惠券面值
        /// </summary>
        public Decimal share_amount { get; set; }

        /// <summary>
        /// 退款提前天数1
        /// </summary>
        public Int32? drawback_days1 { get; set; }
        /// <summary>
        /// 退款额度比例1
        /// </summary>
        public Int32? drawback_money1 { get; set; }

        /// <summary>
        /// 退款提前天数2
        /// </summary>
        public Int32? drawback_days2 { get; set; }
        /// <summary>
        /// 退款额度比例2
        /// </summary>
        public Int32? drawback_money2 { get; set; }

        /// <summary>
        /// 游玩天数
        /// </summary>
        public Int32? days { get; set; }


        /// <summary>
        /// 集合地点
        /// </summary>
        public String address { get; set; }

        /// <summary>
        /// 下单说明
        /// </summary>
        public String ticket_content { get; set; }

        /// <summary>
        /// 砍价时间开关
        /// </summary>
        public String is_bargain { get; set; }

        /// <summary>
        /// 提前预定时间开关
        /// </summary>
        public String advance_booking { get; set; }

        /// <summary>
        /// 提前天数
        /// </summary>
        public Int32? advance_day { get; set; }

        /// <summary>
        /// 提前时间
        /// </summary>
        public String advance_time { get; set; }


        /// <summary>
        /// 产品砍价最低人数
        /// </summary>
        public Int32? people_minnum { get; set; }

        /// <summary>
        /// 门票下单最高人数
        /// </summary>
        public Int32? people_maxnum { get; set; }

        /// <summary>
        /// 适合年龄最小值
        /// </summary>
        public Int32? age_appropriate_min { get; set; }

        /// <summary>
        /// 适合年龄最大值
        /// </summary>
        public Int32? age_appropriate_max { get; set; }

        /// <summary>
        /// 下单风险提示开关
        /// </summary>
        public String issue_risk { get; set; }

        /// <summary>
        /// 下单风险提示内容/门票下单说明
        /// </summary>
        public String issue_risk_content { get; set; }

        /// <summary>
        /// 购票限制
        /// </summary>
        public String ticket_purchase_restrictions { get; set; }

        /// <summary>
        /// 实名制
        /// </summary>
        public String real_name { get; set; }

        /// <summary>
        /// 产品到期时间
        /// </summary>
        public DateTime? product_endtime { get; set; }

        /// <summary>
        /// 最大延迟付款时间
        /// </summary>
        public int? pay_time { get; set; }

        /// <summary>
        /// 是否是特价门票(0否 1是)
        /// </summary>
        public Int32? special_status { get; set; }

        /// <summary>
        /// 产品logo，用于封面展示
        /// </summary>
        public String logo { get; set; }

        /// <summary>
        /// 是否是推荐(0否 1是)
        /// </summary>
        public Int32? recommend_status { get; set; }

        /// <summary>
        /// 是否是特价(0否 1是)
        /// </summary>
        public Int32? bargain_price_status { get; set; }

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

        /// <summary>
        /// 供应商手机号
        /// </summary>
        public String supplier_phone { get; set; }

        /// <summary>
        /// 通知方式多选项
        /// </summary>
        public String notice_item { get; set; }

        /// <summary>
        /// 取票信息
        /// </summary>
        public String tickets_info { get; set; }

        /// <summary>
        /// 取票时间选项
        /// </summary>
        public String tickets_time_item { get; set; }

        /// <summary>
        /// 打印纸票选项
        /// </summary>
        public String tickets_printpaper_item { get; set; }


        /// <summary>
        /// 炸鸡播报选项
        /// </summary>
        public String tickets_broadcast { get; set; }

        /// <summary>
        /// 炸鸡播报内容
        /// </summary>
        public String tickets_broadcast_content { get; set; }

        /// <summary>
        /// 打印门票选项
        /// </summary>
        public String tickets_print_item { get; set; }

        /// <summary>
        /// 打印门票内容
        /// </summary>
        public String tickets_print_content { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public String validity_item { get; set; }

        /// <summary>
        /// 入园验证
        /// </summary>
        public String validity_admission { get; set; }

        /// <summary>
        /// 可验证时间
        /// </summary>
        public String validity_usefultime { get; set; }

        /// <summary>
        /// 延迟验证
        /// </summary>
        public String validity_delay { get; set; }

        /// <summary>
        /// 分终端配置
        /// </summary>
        public String validity_terminal { get; set; }



        /// <summary>
        /// 商品所属类别
        /// </summary>
        public String commodity_type { get; set; }

        /// <summary>
        /// 产地
        /// </summary>
        public String placeofproduction { get; set; }

        /// <summary>
        /// 农户照片
        /// </summary>
        public String photooffarmers { get; set; }

        /// <summary>
        /// 商品介绍
        /// </summary>
        public String commodity_presentations { get; set; }


        /// <summary>
        /// 参考菜谱
        /// </summary>
        public String reference_menu { get; set; }


        /// <summary>
        /// 商品照片
        /// </summary>
        public String commodity_photo { get; set; }

    }
}