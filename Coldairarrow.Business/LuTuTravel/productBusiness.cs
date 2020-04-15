using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.LuTuTravel
{
    public class ProductBusiness : BaseBusiness<product>
    {
        #region 外部接口

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<product> GetDataList(int special_status, string product_type_id, string title, string supplier, DateTime? create_time1, DateTime? create_time2, Pagination pagination)
        {
            var q = GetIQueryable().Where(x => x.enable_flag == "1" && x.special_status == special_status);

            //模糊查询
            if (!product_type_id.IsNullOrEmpty())
                q = q.Where(x => x.product_type_id == product_type_id);

            if (!title.IsNullOrEmpty())
                q = q.Where(x => x.title.Contains(title));
            if (!supplier.IsNullOrEmpty())
                q = q.Where(x => x.supplier.Contains(supplier));

            if (create_time1 != null)
                q = q.Where(x => x.create_time >= create_time1);
            if (create_time2 != null)
                q = q.Where(x => x.create_time <= create_time2);

            var resList = q.GetPagination(pagination).ToList();
            var areaBusiness = new AreaBusiness();
            resList.ForEach(item => item.area_code = areaBusiness.GetTheData(item.area_code)?.name);

            return resList;
        }
        /// <summary>
        /// 获取所以产品门票数据
        /// </summary>
        /// <returns></returns>
        public List<product> GetDataList()
        {
            return GetIQueryable().Where(x => x.enable_flag == "1").OrderByDescending(x => x.Id).ToList();
        }

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public product GetTheData(int? id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public void AddData(product newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(product theData)
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

        /// <summary>
        /// 校验价格是否异常
        /// </summary>
        /// <returns></returns>
        public bool PaymentAmount(product theData, product_marketing market)
        {
            //bool ck1 = theData.origin_price < theData.price;//成本价 < 市场价
            //bool ck2 = theData.team_price < theData.origin_price;//成团价  < 成本价
            bool ck3 = theData.team_price > theData.origin_price + theData.team_commission + theData.share_amount;//成团价 > 成本价 + 返佣 + 分享所得优惠券

            bool b1 = true, b2 = true, b3 = true;
            if (market != null)
            {
                //b1 = market.price < theData.price;//营销市场价 < 市场价
                //b2 = market.team_price < theData.team_price;//营销团购价 < 团购价
                b3 = market.team_price > theData.origin_price + market.team_commission + theData.share_amount;//营销团购价 > 成本价 + 营销返佣 + 分享所得优惠券
            }
            return ck3 && b3;
        }

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}