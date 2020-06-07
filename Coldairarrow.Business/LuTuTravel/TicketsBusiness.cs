using Coldairarrow.Business.Common;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.LuTuTravel
{
    public class TicketsBusiness : BaseBusiness<Tickets>
    {
        ProductBusiness _ProductBusiness = new ProductBusiness();

        Tickets_Date_PricesBusiness _Tickets_Date_PricesBusiness = new Tickets_Date_PricesBusiness();


        public override EnumType.LogType LogType { get => EnumType.LogType.同步接口数据; }

        /// <summary>
        /// 更新门票数据
        /// </summary>
        public void SynchronousData(List<Tickets> tickets)
        {
            var insertTickets = new List<Tickets>();
            var updateTickets = new List<Tickets>();
            var allTicketsIds = tickets.Select(x => x.Id).ToList();

            var exsitsTickets = GetIQueryable().Where(x => allTicketsIds.Contains(x.Id)).ToList();
            tickets.ForEach(item =>
            {
                item.description = string.Empty;//清空
                var updateTicket = exsitsTickets.Find(x => x.Id == item.Id);
                if (updateTicket != null)
                {
                    updateTicket = item;
                    updateTicket.county_name = $"{item.prov_name}--{item.prov_name}--{item.prov_name}";
                    updateTicket.update_by = "System";
                    updateTicket.update_time = DateTime.Now.ToCstTime();
                    updateTickets.Add(updateTicket);
                }
                else
                {
                    item.create_by = "System";
                    item.create_time = DateTime.Now.ToCstTime();
                    insertTickets.Add(item);
                }
                if (item.date_prices?.Count > 0)
                {
                    _Tickets_Date_PricesBusiness.SynchronousData(item.Id, item.date_prices);
                }
            });
            if (updateTickets.Count > 0)
                Update(updateTickets);
            if (insertTickets.Count > 0)
                Insert(insertTickets);
        }

        #region 外部接口

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<Tickets> GetDataList(string condition, string keyword, Pagination pagination)
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
        public Tickets GetTheData(int id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public void AddData(Tickets newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(Tickets theData)
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