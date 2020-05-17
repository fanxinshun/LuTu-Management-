using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.LuTuTravel
{
    public class product_typeBusiness : BaseBusiness<product_type>
    {
        #region 外部接口

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<product_type> GetDataList(string type_name, Pagination pagination)
        {
            var q = GetIQueryable();

            //模糊查询
            if (!type_name.IsNullOrEmpty())
                q = q.Where(x => x.type_name.Contains(type_name));

            return q.GetPagination(pagination).ToList();
        }

        /// <summary>
        /// 获取GetProductType
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public List<product_type> GetList(int? type)
        {
            var q = GetIQueryable();
            if (type != null)
            {
                q = q.Where(x => x.type == type);
            }
            return q.OrderBy(x => x.sort).ToList();
        }
        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public product_type GetTheData(string id)
        {
            return GetEntity(id);
        }
        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public product_type GetTheData(int type, string type_name)
        {
            return GetIQueryable().FirstOrDefault(x => x.type == type && x.type_name == type_name);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public void AddData(product_type newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(product_type theData)
        {
            Update(theData);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public void DeleteData(List<string> ids)
        {
            var list = GetIQueryable().Where(x => ids.Contains(x.id)).ToList();
            list.ForEach(x => x.enable_flag = 0);

            UpdateAny(list, new List<string>() { "enable_flag" });
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}