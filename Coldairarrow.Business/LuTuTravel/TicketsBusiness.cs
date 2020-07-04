using Coldairarrow.Business.Common;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Nest;
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
                var updateTicket = exsitsTickets.Find(x => x.Id == item.Id);
                if (updateTicket != null)
                {
                    if (updateTicket.status == 0)//鹿途下线的不更新
                    {
                        continue;
                    }
                    CopyEntity(item, updateTicket);
                    updateTicket.update_by = "System";
                    updateTicket.update_time = DateTime.Now.ToCstTime();
                    updateTickets.Add(updateTicket);
                }
                else
                {
                    Tickets entity = new Tickets();
                    entity.Id = item.Id;

                    CopyEntity(item, entity);
                    entity.create_by = "System";
                    entity.create_time = DateTime.Now.ToCstTime();
                    insertTickets.Add(entity);
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

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ticket"></param>
        private static void CopyEntity(Tickets item, Tickets ticket)
        {
            ticket.supplier_id = item.supplier_id;
            ticket.title = item.title;
            ticket.type = item.type;
            ticket.status = item.status;
            ticket.send_type = item.send_type;
            ticket.pay_type = item.pay_type;
            ticket.amount = item.amount;
            ticket.sort_order = item.sort_order;
            ticket.refund_type = item.refund_type;
            ticket.validity_type = item.validity_type;
            ticket.validity_day = item.validity_day;
            ticket.start_time = item.start_time;
            ticket.expire_time = item.expire_time;
            ticket.sms_content = item.sms_content;
            ticket.mms_content = item.mms_content;
            ticket.print_content = item.print_content;
            ticket.image = item.image;
            ticket.notice = item.notice;
            ticket.description = string.Empty;//不接收该字段
            ticket.brief = item.brief;
            ticket.is_import = item.is_import;
            ticket.nett_price = item.nett_price;
            ticket.back_cash = item.back_cash;
            ticket.original_price = item.original_price;
            ticket.market_price = item.market_price;
            ticket.sku_user_price = item.sku_user_price;
            ticket.sku_market_price = item.sku_market_price;
            ticket.sku_suggest_price = item.sku_suggest_price;
            ticket.nett_price2 = item.nett_price2;
            ticket.must_id_number = item.must_id_number;
            ticket.prov_name = item.prov_name;
            ticket.city_name = item.city_name;
            //updateTicket.county_name = item.county_name;
            ticket.refund_chanrge_type = item.refund_chanrge_type;
            ticket.refund_chanrge = item.refund_chanrge;
            ticket.self_refund_scale = item.self_refund_scale;
            ticket.self_refund_fixed = item.self_refund_fixed;
            ticket.county_name = $"{item.prov_name}--{item.prov_name}--{item.prov_name}";
        }

        #region 外部接口

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="title">门票标题</param>
        /// <param name="notice">购买须知</param
        /// <param name="brief">产品简介</param>
        /// <param name="refund_type">退票类型</param>
        /// <param name="status">上线状态</param>
        /// <returns></returns>
        public List<Tickets> GetDataList(string title, string notice, string brief, int? refund_type, int? status, Pagination pagination)
        {
            var q = GetIQueryable();

            if (!title.IsNullOrEmpty())
                q = q.Where(x => x.title.Contains(title));

            if (!notice.IsNullOrEmpty())
                q = q.Where(x => x.notice.Contains(notice));

            if (!brief.IsNullOrEmpty())
                q = q.Where(x => x.brief.Contains(brief));

            if (!refund_type.IsNullOrEmpty())
                q = q.Where(x => x.refund_type == refund_type);

            if (!status.IsNullOrEmpty())
                q = q.Where(x => x.status == status);

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
        public AjaxResult ChangeStatus(List<int> ids)
        {
            var entitys = GetIQueryable().Where(x => ids.Contains(x.Id)).ToList();
            var list = entitys.Select(x => x.status);
            if (list.Contains(1) && list.Contains(0))
            {
                return Error("所选记录上线状态不一致！");
            }

            foreach (var item in entitys)
            {
                item.status = item.status == 0 ? 1 : 0;
                item.update_by = Common.Operator.UserId;
                item.update_time = DateTime.Now.ToCstTime();
            }
            Update(entitys);
            return Success();
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