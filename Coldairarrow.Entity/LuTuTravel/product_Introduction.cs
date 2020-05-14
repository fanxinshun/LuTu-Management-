using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.LuTuTravel
{
    /// <summary>
    /// product_Introduction
    /// </summary>
    [Table("product_Introduction")]
    public class product_Introduction
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public int productId { get; set; }

        /// <summary>
        /// 第几天的
        /// </summary>
        public Int32? days { get; set; }

        /// <summary>
        /// 行程主题
        /// </summary>
        public String title { get; set; }

        /// <summary>
        /// 行程安排
        /// </summary>
        public String scheduling { get; set; }

        /// <summary>
        /// 住宿
        /// </summary>
        public String stay { get; set; }

        /// <summary>
        /// 餐饮
        /// </summary>
        public String food { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public String picture { get; set; }

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