using Coldairarrow.Business.Common;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

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
            foreach (var item in tickets)
            {
                if (item.status == 0)//奥米下线的不更新
                {
                    continue;
                }
                item.description = string.Empty;//清空
                var updateTicket = exsitsTickets.Find(x => x.Id == item.Id);
                if (updateTicket != null)
                {
                    if (updateTicket.status == 0)//鹿途下线的也不更新
                    {
                        continue;
                    }
                    updateTicket.supplier_id = item.supplier_id;
                    updateTicket.title = item.title;
                    updateTicket.type = item.type;
                    updateTicket.status = item.status;
                    updateTicket.send_type = item.send_type;
                    updateTicket.pay_type = item.pay_type;
                    updateTicket.amount = item.amount;
                    updateTicket.sort_order = item.sort_order;
                    updateTicket.refund_type = item.refund_type;
                    updateTicket.validity_type = item.validity_type;
                    updateTicket.validity_day = item.validity_day;
                    updateTicket.start_time = item.start_time;
                    updateTicket.expire_time = item.expire_time;
                    updateTicket.sms_content = item.sms_content;
                    updateTicket.mms_content = item.mms_content;
                    updateTicket.print_content = item.print_content;
                    updateTicket.image = item.image;
                    updateTicket.notice = item.notice;
                    updateTicket.description = item.description;
                    updateTicket.brief = item.brief;
                    updateTicket.is_import = item.is_import;
                    updateTicket.nett_price = item.nett_price;
                    updateTicket.back_cash = item.back_cash;
                    updateTicket.original_price = item.original_price;
                    updateTicket.market_price = item.market_price;
                    updateTicket.sku_user_price = item.sku_user_price;
                    updateTicket.sku_market_price = item.sku_market_price;
                    updateTicket.sku_suggest_price = item.sku_suggest_price;
                    updateTicket.nett_price2 = item.nett_price2;
                    updateTicket.must_id_number = item.must_id_number;
                    updateTicket.prov_name = item.prov_name;
                    updateTicket.city_name = item.city_name;
                    //updateTicket.county_name = item.county_name;
                    updateTicket.refund_chanrge_type = item.refund_chanrge_type;
                    updateTicket.refund_chanrge = item.refund_chanrge;
                    updateTicket.self_refund_scale = item.self_refund_scale;
                    updateTicket.self_refund_fixed = item.self_refund_fixed;
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
                if (item.date_prices.GetType() == typeof(JObject))
                {
                    _Tickets_Date_PricesBusiness.SynchronousData(item.Id, (JObject)item.date_prices);
                }
            };
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
        /// 更改上线状态
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public void ChangeStatus(int id)
        {
            var entity = GetEntity(id);
            if (entity != null)
            {
                entity.status = entity.status == 1 ? 0 : 1;
                Update(entity);
            }
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