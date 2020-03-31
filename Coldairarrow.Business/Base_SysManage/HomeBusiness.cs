using Coldairarrow.Business.Common;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Coldairarrow.Business.LuTuTravel;
using Coldairarrow.Entity.LuTuTravel;

namespace Coldairarrow.Business.Base_SysManage
{
    public class HomeBusiness : BaseBusiness<Base_User>, IHomebusiness
    {
        public AjaxResult SubmitLogin(string userName, string password)
        {
            if (userName.IsNullOrEmpty() || password.IsNullOrEmpty())
                return Error("账号或密码不能为空！");
            password = password.ToMD5String();
            var theUser = GetIQueryable().Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
            if (theUser != null)
            {
                Operator.Login(theUser.UserId);
                return Success();
            }
            else
                return Error("账号或密码不正确！");
        }
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

            data.xAxisData = new List<string>();
            data.yAxisData = new List<decimal>();
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

    }


}

public class MainData
{
    /// <summary>
    /// 今日浏览数
    /// </summary>
    public int T_VisitsNum { get; set; }
    /// <summary>
    /// 昨日浏览数
    /// </summary>
    public int Y_VisitsNum { get; set; }
    /// <summary>
    /// 总浏览数
    /// </summary>
    public int A_VisitsNum { get; set; }

    /// <summary>
    /// 今日注册用户数
    /// </summary>
    public int T_RegisteredUsers { get; set; }
    /// <summary>
    /// 昨日注册用户数
    /// </summary>
    public int Y_RegisteredUsers { get; set; }
    /// <summary>
    /// 总注册用户数
    /// </summary>
    public int A_RegisteredUsers { get; set; }

    /// <summary>
    /// 今日团长新增数
    /// </summary>
    public int T_NewHead { get; set; }
    /// <summary>
    /// 昨日团长新增数
    /// </summary>
    public int Y_NewHead { get; set; }
    /// <summary>
    /// 总团长新增数
    /// </summary>
    public int A_NewHead { get; set; }


    /// <summary>
    /// 今日参团数
    /// </summary>
    public int T_Participants { get; set; }
    /// <summary>
    /// 昨日参团数
    /// </summary>
    public int Y_Participants { get; set; }
    /// <summary>
    /// 总参团数
    /// </summary>
    public int A_Participants { get; set; }


    /// <summary>
    /// 今日交易额
    /// </summary>
    public decimal T_TradingVolume { get; set; }
    /// <summary>
    /// 昨日交易额
    /// </summary>
    public decimal Y_TradingVolume { get; set; }
    /// <summary>
    /// 总交易额
    /// </summary>
    public decimal A_TradingVolume { get; set; }

    /// <summary>
    /// 产品销售排行榜数据源
    /// </summary>
    public object ProductSaleRank { get; set; }
    /// <summary>
    /// 团长销售排行榜数据源
    /// </summary>
    public object TeamSaleRank { get; set; }

    /// <summary>
    /// 横坐标绑定数据
    /// </summary>
    public List<string> xAxisData { get; set; }
    /// <summary>
    /// 纵坐标绑定数据
    /// </summary>
    public List<decimal> yAxisData { get; set; }


}

/// <summary>
/// 排行榜对象
/// </summary>
public class SaleRank
{
    public string name { get; set; }
    public int num { get; set; }
}