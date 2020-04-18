using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Entity.Base_SysManage
{

    public class EChartData
    {
        public EChartData()
        {
            xAxisData = new List<string>();
            yAxisData = new List<object>();
            yAxisDataAmount = new List<decimal>();
            yAxisDataOrders = new List<int>();
            yAxisDataPeoples = new List<int>();
        }
        /// <summary>
        /// 横坐标--通用
        /// </summary>
        public List<string> xAxisData { get; set; }

        /// <summary>
        /// 横坐标--通用
        /// </summary>
        public List<object> yAxisData { get; set; }

        /// <summary>
        /// 纵坐标--销售额
        /// </summary>
        public List<decimal> yAxisDataAmount { get; set; }

        /// <summary>
        /// 纵坐标--订单数
        /// </summary>
        public List<int> yAxisDataOrders { get; set; }

        /// <summary>
        /// 纵坐标--人数
        /// </summary>
        public List<int> yAxisDataPeoples { get; set; }
    }
}
