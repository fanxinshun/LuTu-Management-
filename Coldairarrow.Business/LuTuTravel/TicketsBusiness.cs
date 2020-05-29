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
        ProductBusiness _ProductBusiness;
        public TicketsBusiness()
        {
            _ProductBusiness = new ProductBusiness();
        }

        public override EnumType.LogType LogType { get => EnumType.LogType.同步接口数据; }

        /// <summary>
        /// 更新门票数据
        /// </summary>
        public void SynchronousData(List<Tickets> tickets)
        {
            var ticketsIds = tickets.Select(t => t.Id).ToList();
            var productTickets = _ProductBusiness.GetIQueryable().Where(x => ticketsIds.Contains(x.ref_Id)).ToList();

            var currientTime = DateTime.Now;
            tickets.ForEach(t =>
            {
                var productTicket = productTickets.Find(x => x.ref_Id == t.Id);
                productTicket = productTicket == null ? new product() : productTicket;

                productTicket.title = t.title;

                productTicket.enable_flag = "1";
                productTicket.special_status = 1;
                if (productTicket.Id == 0)
                {
                    productTicket.create_by = Operator.UserId; ;
                    productTicket.create_time = currientTime;
                    _ProductBusiness.Insert(productTicket);
                }
                else
                {
                    productTicket.update_by = Operator.UserId;
                    productTicket.update_time = currientTime;
                }
            });
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
        public Tickets GetTheData(string id)
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