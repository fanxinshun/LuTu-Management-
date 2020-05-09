using Coldairarrow.Entity.Base_SysManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coldairarrow.Util;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Coldairarrow.Business.LuTuTravel
{
    public class ProductRepBusiness : BaseBusiness<ProductProfitDetail>
    {
        private payBusiness _payBusiness;
        private orderBusiness _orderBusiness;
        private product_marketingBusiness _product_marketingBusiness;
        public ProductRepBusiness()
        {
            _payBusiness = new payBusiness();
            _orderBusiness = new orderBusiness();
            _product_marketingBusiness = new product_marketingBusiness();
        }
        /// <summary>
        /// 首页数据
        /// </summary>
        /// <param name="days"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public MainData GetMainData(int days, Pagination pagination)
        {
            var currentTime = DateTime.Now.ToString("yyyy-MM-dd");
            var yesterdayTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            var data = new MainData();

            var listPays = new payBusiness().GetIQueryable().Where(x => x.status == 1).ToList();//支付记录
            var listProduct = new ProductBusiness().GetIQueryable().ToList();//产品清单
            var listMembers = new membersBusiness().GetIQueryable().ToList();//用户记录
            var listOrders = new orderBusiness().GetIQueryable().Where(x => x.status == 1).ToList().Where(x => listPays.Find(a => a.order_id == x.Id) != null).ToList();//订单 -支付完成的订单
            //浏览数
            data.T_VisitsNum = 0;
            data.Y_VisitsNum = 0;
            data.A_VisitsNum = 0;
            //注册用户数
            data.T_RegisteredUsers = listMembers.Where(x => x.registration_time?.ToString("yyyy-MM-dd") == currentTime).Count();
            data.Y_RegisteredUsers = listMembers.Where(x => x.registration_time?.ToString("yyyy-MM-dd") == yesterdayTime).Count();
            data.A_RegisteredUsers = listMembers.Where(x => x.registration_time != null).Count();
            //团长新增数
            data.T_NewHead = listMembers.Where(x => x.header_time?.ToString("yyyy-MM-dd") == currentTime).Count();
            data.Y_NewHead = listMembers.Where(x => x.header_time?.ToString("yyyy-MM-dd") == yesterdayTime).Count();
            data.A_NewHead = listMembers.Where(x => x.header_time != null).Count();
            //参团数
            data.T_Participants = listOrders.Where(x => x.create_time.ToString("yyyy-MM-dd") == currentTime).Sum(x => x.num);
            data.Y_Participants = listOrders.Where(x => x.create_time.ToString("yyyy-MM-dd") == yesterdayTime).Sum(x => x.num);
            data.A_Participants = listOrders.Where(x => x.create_time != null).Sum(x => x.num);
            //交易额
            data.T_TradingVolume = listPays.Where(x => x.pay_time?.ToString("yyyy-MM-dd") == currentTime).Sum(x => x.money);
            data.Y_TradingVolume = listPays.Where(x => x.pay_time?.ToString("yyyy-MM-dd") == yesterdayTime).Sum(x => x.money);
            data.A_TradingVolume = listPays.Where(x => x.pay_time != null).Sum(x => x.money);

            decimal value = 0;
            for (int i = days - 1; i >= 0; i--)
            {
                value += listPays.Where(x => x.pay_time?.ToString("yyyy-MM-dd") == DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd")).Sum(x => x.money);
                data.xAxisData.Add(DateTime.Now.AddDays(-i).ToString("MM/dd"));
                data.yAxisData.Add(value);
            }

            List<SaleRank> ProductSaleRank = new List<SaleRank>();
            var listProductSaleRank = listOrders.GroupBy(x => x.product_id);
            foreach (var item in listProductSaleRank)
            {
                ProductSaleRank.Add(new SaleRank() { name = listProduct.Find(x => x.Id == int.Parse(item.Key))?.title, num = item.Sum(x => x.num) });
            }
            data.ProductSaleRank = pagination.BuildTableResult_DataGrid(ProductSaleRank.GetPagination(pagination).ToList());

            List<SaleRank> TeamSaleRank = new List<SaleRank>();
            var listTeamSaleRank = listOrders.Where(x => x.share_id != null).GroupBy(x => x.share_id);
            foreach (var item in listTeamSaleRank)
            {
                TeamSaleRank.Add(new SaleRank() { name = listMembers.Find(x => x.oppen_id == item.Key)?.nick_name, num = item.Sum(x => x.num) });
            }
            data.TeamSaleRank = pagination.BuildTableResult_DataGrid(TeamSaleRank.GetPagination(pagination).ToList());
            return data;
        }

        /// <summary>
        /// 获取销售渠道
        /// </summary>
        /// <returns></returns>
        public EChartData GetPurchaseChannels(int days)
        {
            var data = new EChartData();
            var listPays = _payBusiness.GetIQueryable().Where(x => x.status == 1).ToList();//支付记录
            //var listProduct = new ProductBusiness().GetIQueryable().ToList();//产品清单
            var listMembers = new membersBusiness().GetIQueryable().ToList();//用户信息
            var listOrders = _orderBusiness.GetIQueryable().Where(x => x.status == 1).ToList().Where(x => listPays.Find(a => a.order_id == x.Id) != null).ToList();//订单 -支付完成的订单

            var nowTime = DateTime.Now;
            for (int i = days - 1; i >= 0; i--)
            {
                data.xAxisData.Add(nowTime.AddDays(-i).ToString("MM/dd"));
                data.yAxisData.Add(listOrders.Where(x => x.share_id.IsNullOrEmpty() && x.create_time.ToString("yyyy-MM-dd") == nowTime.AddDays(-i).ToString("yyyy-MM-dd")).Count());
                data.yAxisData2.Add(listOrders.Where(x => !x.share_id.IsNullOrEmpty() && listMembers.FirstOrDefault(o => o.oppen_id == x.share_id)?.header_status == 1 && x.create_time.ToString("yyyy-MM-dd") == nowTime.AddDays(-i).ToString("yyyy-MM-dd")).Count());
                data.yAxisData3.Add(listOrders.Where(x => !x.share_id.IsNullOrEmpty() && listMembers.FirstOrDefault(o => o.oppen_id == x.share_id)?.header_status == 0 && x.create_time.ToString("yyyy-MM-dd") == nowTime.AddDays(-i).ToString("yyyy-MM-dd")).Count());
            }
            return data;
        }

        /// <summary>
        /// 获取产品销售额、订单数、人数
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public EChartData GetProductRepData(int days)
        {
            var data = new EChartData();

            var listPays = _payBusiness.GetIQueryable().Where(x => x.status == 1).ToList();//支付记录
            //var listProduct = new ProductBusiness().GetIQueryable().ToList();//产品清单
            //var listMembers = new membersBusiness().GetIQueryable().ToList();//用户信息
            var listOrders = _orderBusiness.GetIQueryable().Where(x => x.status == 1).ToList().Where(x => listPays.Find(a => a.order_id == x.Id) != null).ToList();//订单 -支付完成的订单

            decimal value = 0;
            var nowTime = DateTime.Now;
            for (int i = days - 1; i >= 0; i--)
            {
                data.xAxisData.Add(nowTime.AddDays(-i).ToString("MM/dd"));

                value += listPays.Where(x => x.pay_time?.ToString("yyyy-MM-dd") == nowTime.AddDays(-i).ToString("yyyy-MM-dd")).Sum(x => x.money);
                data.yAxisDataAmount.Add(value);

                data.yAxisDataOrders.Add(listOrders.Where(x => x.create_time.ToString("yyyy-MM-dd") == nowTime.AddDays(-i).ToString("yyyy-MM-dd")).Count());
                data.yAxisDataPeoples.Add(listOrders.Where(x => x.create_time.ToString("yyyy-MM-dd") == nowTime.AddDays(-i).ToString("yyyy-MM-dd")).Sum(x => x.num));
            }

            return data;
        }
        /// <summary>
        /// 获取利润图表数据
        /// </summary>
        /// <returns></returns>
        public EChartData GetProductProfit()
        {
            EChartData eChartData = new EChartData();
            var productMarketingModels = _product_marketingBusiness.GetProductAndMarketingList();

            eChartData.xAxisData.Add("全部");
            var obj = CalculateProductProfit(productMarketingModels);
            eChartData.yAxisDataEstimateProfitMargin.Add(obj.EstimateProfitMargin);
            eChartData.yAxisDataActualProfitMargin.Add(obj.ActualProfitMargin);

            //各产品类别预估毛利率
            var productTypeName = productMarketingModels.GroupBy(x => x.pproduct_type_name);
            foreach (var item in productTypeName)
            {
                eChartData.xAxisData.Add(item.Key);
                var o = CalculateProductProfit(item.ToList());
                eChartData.yAxisDataEstimateProfitMargin.Add(o.EstimateProfitMargin);
                eChartData.yAxisDataActualProfitMargin.Add(o.ActualProfitMargin);
            }
            return eChartData;
        }

        /// <summary>
        /// 计算预估毛利率
        /// </summary>
        /// <param name="productMarketingModels"></param>
        /// <returns></returns>
        private static ProductProfitDetail CalculateProductProfit(List<productMarketingModel> productMarketingModels)
        {
            ProductProfitDetail profit = new ProductProfitDetail();
            //总预估成团价
            decimal amountTeam_price = (decimal)productMarketingModels.Sum(x => x.product_marketing_id.IsNullOrEmpty() ? x.pteam_price : x.team_price);
            //总预估成本价
            decimal amountOrigin_price = productMarketingModels.Sum(x => x.porigin_price);

            //总预估毛利
            profit.EstimateProfit = (amountTeam_price - amountOrigin_price);//总预估成团价 - 总预估成本价
            //总预估毛利率
            profit.EstimateProfitMargin = amountTeam_price == 0 ? 0 : Math.Round(profit.EstimateProfit / amountTeam_price * 100, 0, MidpointRounding.AwayFromZero);

            //总实际支付金额
            decimal money = productMarketingModels.Sum(x => x.money / (x.num ?? 1));
            //总实际毛利
            profit.ActualProfit = money == 0 ? 0 : (money - amountOrigin_price);//付款金额/订单人数 - 成本价
            //总实际毛利率
            profit.ActualProfitMargin = money == 0 ? 0 : Math.Round(profit.ActualProfit / money * 100, 0, MidpointRounding.AwayFromZero);
            return profit;
        }
        /// <summary>
        /// 计算预估毛利率
        /// </summary>
        /// <param name="productMarketingModels"></param>
        /// <returns></returns>
        private static ProductProfitDetail CalculateProductProfit(productMarketingModel x)
        {
            ProductProfitDetail profit = new ProductProfitDetail();
            //预估成团价
            decimal amountTeam_price = (decimal)(x.product_marketing_id.IsNullOrEmpty() ? x.pteam_price : x.team_price);
            //成本价
            decimal amountOrigin_price = x.porigin_price;

            //预估毛利
            profit.EstimateProfit = (amountTeam_price - amountOrigin_price);//预估成团价 - 成本价
            //预估毛利率
            profit.EstimateProfitMargin = amountTeam_price == 0 ? 0 : Math.Round(profit.EstimateProfit / amountTeam_price * 100, 0, MidpointRounding.AwayFromZero);

            //实际支付金额
            decimal money = x.money / (x.num ?? 1);
            //实际毛利
            profit.ActualProfit = money == 0 ? 0 : (money - amountOrigin_price);//付款金额/订单人数 - 成本价
            //实际毛利率
            profit.ActualProfitMargin = money == 0 ? 0 : Math.Round(profit.ActualProfit / money * 100, 0, MidpointRounding.AwayFromZero);
            profit.Id = x.pId;
            profit.ObjName = x.ptitle;
            return profit;
        }



        /// <summary>
        /// 获取利润排行数据
        /// </summary>
        /// <param name="product_type"></param>
        /// <returns></returns>
        public List<ProductProfitDetail> GetProductProfitDetail(string product_type, Pagination pagination)
        {
            if (product_type != "全部")
            {
                product_type = new product_typeBusiness().GetTheData(1, product_type)?.id;
            }
            else
            {
                product_type = null;
            }
            var productMarketingModels = _product_marketingBusiness.GetProductAndMarketingList(product_type);
            //总预估成团价
            decimal amountTeam_price = (decimal)productMarketingModels.Sum(x => x.product_marketing_id.IsNullOrEmpty() ? x.pteam_price : x.team_price);
            //总预估成本价
            decimal amountOrigin_price = productMarketingModels.Sum(x => x.porigin_price);
            List<ProductProfitDetail> list = new List<ProductProfitDetail>();
            productMarketingModels.ForEach(item =>
            {
                var profit = CalculateProductProfit(item);
                list.Add(profit);

                //毛利配占比
                profit.EstimateProfitRatio = Math.Round(profit.EstimateProfit / (amountTeam_price - amountOrigin_price) * 100, 0, MidpointRounding.AwayFromZero);
            });
            return list.GetPagination(pagination).ToList();
        }

    }

    /// <summary>
    /// 商品利润表
    /// </summary>
    public class ProductProfitDetail
    {
        /// <summary>
        /// 计算对象Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 计算对象名称
        /// </summary>
        public string ObjName { get; set; }
        /// <summary>
        /// 预估毛利
        /// </summary>
        public decimal EstimateProfit { get; set; }
        /// <summary>
        /// 预估毛利率
        /// </summary>
        public decimal EstimateProfitMargin { get; set; }
        /// <summary>
        /// 实际毛利
        /// </summary>
        public decimal ActualProfit { get; set; }
        /// <summary>
        /// 实际毛利率
        /// </summary>
        public decimal ActualProfitMargin { get; set; }
        /// <summary>
        /// 毛利配占比
        /// </summary>
        public decimal EstimateProfitRatio { get; set; }
    }

}
