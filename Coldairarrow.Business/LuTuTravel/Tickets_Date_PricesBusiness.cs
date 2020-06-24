using Coldairarrow.Business.Common;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Coldairarrow.Business.LuTuTravel
{
    public class Tickets_Date_PricesBusiness : BaseBusiness<Tickets_Date_Prices>
    {

        /// <summary>
        /// 更新门票数据
        /// </summary>
        public void SynchronousData(int ticketsId, JObject ticketsDate)
        {
            var insertTicketsDate = new List<Tickets_Date_Prices>();
            var updateTicketsDate = new List<Tickets_Date_Prices>();
            var allTicketsDateIds = ticketsDate.Children().Select(x => x.Path.ToDateTime()).ToList();

            var exsitsTickets = GetIQueryable().Where(x => x.Tickets_Id == ticketsId && allTicketsDateIds.Contains(x.date)).ToList();
            foreach (JProperty item in ticketsDate.Children())
            {
                if (DateTime.Now.AddDays(1).ToCstTime() > item.Name.ToDateTime())//只更新当天及以后的价格日历
                {
                    continue;
                }
                var updateTicket = exsitsTickets.Find(x => x.date == item.Path.ToDateTime());
                if (updateTicket != null)
                {
                    updateTicket.price = item.Value["price"].ToString().ToDecimal();
                    updateTicket.stock = item.Value["stock"].ToString().ToInt();
                    updateTicket.suggest_price = item.Value["suggest_price"].ToString().ToDecimal();
                    updateTicket.market_price = item.Value["market_price"].ToString().ToDecimal();
                    updateTicket.update_by = Operator.UserId;
                    updateTicket.update_time = DateTime.Now.ToCstTime();
                    updateTicketsDate.Add(updateTicket);
                }
                else
                {
                    Tickets_Date_Prices obj = new Tickets_Date_Prices();
                    obj.Tickets_Id = ticketsId;
                    obj.date = item.Name.ToDateTime();
                    obj.price = item.Value["price"].ToString().ToDecimal();
                    obj.stock = item.Value["stock"].ToString().ToInt();
                    obj.suggest_price = item.Value["suggest_price"].ToString().ToDecimal();
                    obj.market_price = item.Value["market_price"].ToString().ToDecimal();
                    obj.create_by = Operator.UserId;
                    obj.create_time = DateTime.Now.ToCstTime();
                    insertTicketsDate.Add(obj);
                }
            }
            if (exsitsTickets.Count > 0)
                Update(updateTicketsDate);
            if (insertTicketsDate.Count > 0)
                Insert(insertTicketsDate);
        }

        #region 外部接口

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<Tickets_Date_Prices> GetDataList(string condition, string keyword, Pagination pagination)
        {
            var q = GetIQueryable();

            //模糊查询
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
                q = q.Where($@"{condition}.Contains(@0)", keyword);

            return q.GetPagination(pagination).ToList();
        }

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public Tickets_Date_Prices GetTheData(string id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public void AddData(Tickets_Date_Prices newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(Tickets_Date_Prices theData)
        {
            Update(theData);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public void DeleteData(List<string> ids)
        {
            Delete(ids);
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}