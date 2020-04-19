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
            yAxisData2 = new List<object>();
            yAxisData3 = new List<object>();
            yAxisDataAmount = new List<decimal>();
            yAxisDataOrders = new List<int>();
            yAxisDataPeoples = new List<int>();
            yAxisDataEstimateProfitMargin = new List<decimal>();
            yAxisDataActualProfitMargin = new List<decimal>();
        }
        /// <summary>
        /// 横坐标--通用
        /// </summary>
        public List<string> xAxisData { get; set; }

        /// <summary>
        /// 横坐标--通用1
        /// </summary>
        public List<object> yAxisData { get; set; }
        /// <summary>
        /// 横坐标--通用2
        /// </summary>
        public List<object> yAxisData2 { get; set; }
        /// <summary>
        /// 横坐标--通用3
        /// </summary>
        public List<object> yAxisData3 { get; set; }

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
        /// <summary>
        /// 纵坐标--预估毛利率
        /// </summary>
        public List<decimal> yAxisDataEstimateProfitMargin { get; set; }
        /// <summary>
        /// 纵坐标--实际毛利率
        /// </summary>
        public List<decimal> yAxisDataActualProfitMargin { get; set; }
    }
}
