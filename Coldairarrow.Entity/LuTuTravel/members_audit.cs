using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// 审核表
    /// </summary>
    [Table("members_audit")]
    public class members_audit
    {

        /// <summary>
        /// 审核表id
        /// </summary>
        [Key, Column(Order = 1)]
        public String id { get; set; }

        /// <summary>
        /// 小程序会员表id
        /// </summary>
        public String member_id { get; set; }

        /// <summary>
        /// 发起人手机号
        /// </summary>
        public String phone { get; set; }

        /// <summary>
        /// 群图片
        /// </summary>
        public String image_group { get; set; }

        /// <summary>
        /// 群名称
        /// </summary>
        public String image_group_name { get; set; }

        /// <summary>
        /// 0 待审核 1审核通过 3审核拒绝
        /// </summary>
        public Int32? status { get; set; }

        /// <summary>
        /// 邀请码
        /// </summary>
        public String invite_code { get; set; }

    }
}