using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;

namespace Coldairarrow.Business.LuTuTravel
{
    public class product_tagBusiness : BaseBusiness<product_tag>
    {
        #region 外部接口

        /// <summary>
        /// 获取数据列表dataTable
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<productTagModel> GetProductTagModel(string product_type_id, string parent_id, Pagination pagination)
        {
            List<DbParameter> paramters = new List<DbParameter>()
            {
                new  MySqlParameter ("@product_type_id",product_type_id),
                new  MySqlParameter ("@parent_id",parent_id)
            };
            DataTable table = GetDataTableWithSql(@"SELECT
	                                                    a.*,
	                                                    b.tagname panentName,
	                                                    c.NAME areaName,
	                                                    d.title product_type_name
                                                    FROM
	                                                    `product_tag` a
	                                                    LEFT JOIN `product_tag` b ON b.id = a.parent_id
	                                                    LEFT JOIN `area` c ON a.area_code = c.`code`
	                                                    LEFT JOIN `product_type` d ON a.product_type_id = d.id 
                                                    WHERE
	                                                    (a.parent_id IS NOT NULL or a.parent_id ='')
                                                        AND (@product_type_id is null or a.product_type_id=@product_type_id )
                                                        AND (@parent_id is null or a.parent_id=@parent_id)", paramters);

            return table.ToList<productTagModel>().GetPagination(pagination).ToList();
        }


        /// <summary>
        /// 获取父级城市标签
        /// </summary>
        /// <returns></returns>
        public List<product_tag> GetProductTagModel()
        {
            return GetIQueryable().Where(x => x.parent_id.IsNullOrEmpty()).ToList();
        }


        /// <summary>
        /// 获取指定的单条数据product_tag
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public product_tag GetTheData(string id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// 获取指定的单条数据--获取城市标签
        /// </summary>
        /// <param name="product_type_id">product_type_id</param>
        /// <returns></returns>
        public product_tag GetTheParentTag(string product_type_id, string tagname)
        {
            return GetIQueryable().Where(x => x.product_type_id == product_type_id && x.tagname == tagname && x.parent_id.IsNullOrEmpty()).FirstOrDefault();
        }

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public productTagModel GetTheProductTagModel(string id)
        {
            List<DbParameter> paramters = new List<DbParameter>()
            {
                new MySqlParameter("@id",id)
            };
            DataTable table = GetDataTableWithSql(@"SELECT
	                                                    a.*,
	                                                    b.tagname panentName,
	                                                    c.NAME areaName,
	                                                    d.title product_type_name
                                                    FROM
	                                                    `product_tag` a
	                                                    LEFT JOIN `product_tag` b ON b.id = a.parent_id
	                                                    LEFT JOIN `area` c ON a.area_code = c.`code`
	                                                    LEFT JOIN `product_type` d ON a.product_type_id = d.id 
                                                    WHERE
	                                                    a.id =@id", paramters);
            if (table == null || table.Rows.Count == 0) return new productTagModel();
            return table.ToList<productTagModel>().ToList()[0];
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public void AddData(product_tag newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(product_tag theData)
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

    }

    #region 数据模型
    public class productTagModel : product_tag
    {
        /// <summary>
        /// 城市标签名
        /// </summary>
        public String panentName { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        public String areaName { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public String product_type_name { get; set; }
    }
    #endregion
}