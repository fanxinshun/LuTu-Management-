using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// 会员表
    /// </summary>
    [Table("members")]
    public class members
    {

        /// <summary>
        /// 会员id(微信唯一标识)
        /// </summary>
        [Key, Column(Order = 1)]
        public String oppen_id { get; set; }

        /// <summary>
        /// 微信绑定手机号
        /// </summary>
        public String phone { get; set; }

        /// <summary>
        /// 其他手机号
        /// </summary>
        public String other_phone { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public String nick_name { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public String head_url { get; set; }

        /// <summary>
        /// 1 男 2女 0未知
        /// </summary>
        public Int32? gender { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime update_time { get; set; }

        /// <summary>
        /// 是否是团长（0否 1是）
        /// </summary>
        public Int32 header_status { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public String idcard_number { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public String address { get; set; }

        /// <summary>
        /// 身份证正面照
        /// </summary>
        public String front_photo { get; set; }

        /// <summary>
        /// 身份证反面照
        /// </summary>
        public String back_photo { get; set; }

        /// <summary>
        /// 是否是plus会员（0否 1是）
        /// </summary>
        public Int32 plus_status { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public String province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public String city { get; set; }

        /// <summary>
        /// 县
        /// </summary>
        public String country { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? last_visit_time { get; set; }

        /// <summary>
        /// 微信session_key
        /// </summary>
        public String session_key { get; set; }

        /// <summary>
        /// 登录标识skey
        /// </summary>
        public String skey { get; set; }

        /// <summary>
        /// 成团长时间
        /// </summary>
        public DateTime? header_time { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? registration_time { get; set; }

        /// <summary>
        /// 团长联系方式
        /// </summary>
        public String header_telephone { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public String real_name { get; set; }

    }
}