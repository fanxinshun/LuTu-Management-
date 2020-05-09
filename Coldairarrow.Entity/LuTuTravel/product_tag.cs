using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// 产品城市标签表
    /// </summary>
    [Table("product_tag")]
    public class product_tag
    {

        /// <summary>
        /// 主键id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 产品类型表id
        /// </summary>
        public String product_type_id { get; set; }

        /// <summary>
        /// 标签名
        /// </summary>
        public String tagname { get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        public String area_code { get; set; }

        /// <summary>
        /// 标签logo
        /// </summary>
        public String img_url { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public String parent_id { get; set; }


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
        public DateTime? update_time { get; set; }


        /// <summary>
        /// 0普通标签 1精品旅游限时推荐标签 2门票限时推荐标签 3商品限时推荐标签
        /// </summary>
        public int? flag_type { get; set; }
    }
}