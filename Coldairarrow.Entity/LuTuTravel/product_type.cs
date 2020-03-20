using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// 产品类型表
    /// </summary>
    [Table("product_type")]
    public class product_type
    {

        /// <summary>
        /// 产品类型主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String id { get; set; }

        /// <summary>
        /// 产品标识(1 热门 2西北等,可与产品协商确定)
        /// </summary>
        public Int32? type { get; set; }

        /// <summary>
        /// 产品类型(华东,华南等)
        /// </summary>
        public String type_name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public String create_by { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime update_time { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public String update_by { get; set; }

        /// <summary>
        /// 产品标题
        /// </summary>
        public String title { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public Int32 sort { get; set; }

    }
}