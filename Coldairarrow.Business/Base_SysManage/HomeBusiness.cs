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

    }


}

public class MainData : EChartData
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

}

/// <summary>
/// 排行榜对象
/// </summary>
public class SaleRank
{
    public string name { get; set; }
    public int num { get; set; }
}