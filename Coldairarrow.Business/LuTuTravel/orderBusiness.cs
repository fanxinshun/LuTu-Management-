using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.LuTuTravel
{
    public class orderBusiness : BaseBusiness<order>
    {
        #region 外部接口

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<order> GetDataList(string product_id, string product_name, string product_area, string start_time1, string start_time2, string create_time1, string create_time2, Pagination pagination)
        {
            var q = GetIQueryable();
            if (!product_id.IsNullOrEmpty())
                q = q.Where(x => x.product_id == product_id);
            if (!product_name.IsNullOrEmpty())
                q = q.Where(x => x.product_name.Contains(product_name));
            if (!product_area.IsNullOrEmpty())
                q = q.Where(x => x.product_name == product_area);//区域问题暂不确定
            //下单时间
            if (!start_time1.IsNullOrEmpty())
            {
                var time = start_time1.ToDateTime();
                q = q.Where(x => x.start_time >= time);
            }
            if (!start_time2.IsNullOrEmpty())
            {
                var time = start_time2.ToDateTime();
                q = q.Where(x => x.start_time <= time);
            }
            //创建时间
            if (!create_time1.IsNullOrEmpty())
            {
                var time = create_time1.ToDateTime();
                q = q.Where(x => x.create_time >= time);
            }
            if (!create_time2.IsNullOrEmpty())
            {
                var time = create_time2.ToDateTime();
                q = q.Where(x => x.create_time <= time);
            }

            var listProduct = new ProductBusiness().GetIQueryable().ToList();//产品清单
            var list = q.GetPagination(pagination).ToList();
            foreach (var item in list)
            {
                item.product_name = listProduct.Find(x => x.Id.ToString() == item.product_id)?.title;
            }
            return list;
        }

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public order GetTheData(string id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public void AddData(order newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(order theData)
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