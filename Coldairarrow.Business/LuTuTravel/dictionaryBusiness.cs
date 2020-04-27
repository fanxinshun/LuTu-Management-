using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.LuTuTravel
{
    public class DictionaryBusiness : BaseBusiness<dictionary>
    {
        #region 外部接口

        /// <summary>
        /// 获取DictionaryType
        /// </summary>
        /// <returns></returns>
        public List<dictionary> GetDictionaryType(string code)
        {
            List<dictionary> result = new List<dictionary>();
            var list = GetIQueryable().Where(x => x.enable_flag == "1" && (code.IsNullOrEmpty() || x.code == code)).ToList().GroupBy(x => x.code);
            foreach (var item in list)
            {
                result.Add(new dictionary() { code = item.Key });
            }
            return result;
        }

        /// <summary>
        /// 获取所有的数据列表 根据Type
        /// </summary>
        /// <returns></returns>
        public List<dictionary> GetDictionaryAllByCode(string code)
        {
            return GetIQueryable().Where(x => x.code == code).OrderBy(x => x.sort).ToList();
        }
        /// <summary>
        /// 获取有效的数据列表 根据Type
        /// </summary>
        /// <returns></returns>
        public List<dictionary> GetDictionaryEnabledByCode(string code)
        {
            return GetIQueryable().Where(x => x.code == code && x.enable_flag == "1").OrderBy(x => x.sort).ToList();
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="code">关键字</param>
        /// <returns></returns>
        public List<dictionary> GetDataList(string code, Pagination pagination)
        {
            var q = GetIQueryable();//.Where(x => x.enable_flag == "1");

            //模糊查询
            if (!code.IsNullOrEmpty())
                q = q.Where($@"code.Contains(@0)", code);

            return q.GetPagination(pagination).ToList();
        }

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public dictionary GetTheData(string id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public void AddData(dictionary newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(dictionary theData)
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