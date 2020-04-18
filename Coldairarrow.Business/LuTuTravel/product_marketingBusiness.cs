using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.LuTuTravel
{
    public class product_marketingBusiness : BaseBusiness<product_marketing>
    {
        #region 外部接口

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<productMarketingModel> GetDataList(int marketingType, string title, Pagination pagination, string product_id = null)
        {
            List<DbParameter> paramters = new List<DbParameter>()
            {
                new  MySqlParameter ("@title", "%" + title + "%"),
                new  MySqlParameter ("@product_id", product_id )
            };
            string sql = string.Empty;
            //marketingType == 0全部
            //marketingType == 1营销产品
            //marketingType == 2非营销产品
            //marketingType == 3返佣产品
            if (marketingType == 0)
            {
                sql = @"SELECT
	                        a.*,
	                        b.Id pId,
	                        b.origin_price porigin_price,
	                        b.price pprice,
	                        b.title ptitle,
	                        b.team_price pteam_price,
	                        b.team_commission pteam_commission,
	                        c.name parea_name 
                        FROM
	                        product_marketing a
	                        RIGHT JOIN product b ON a.product_id = b.Id
                            LEFT JOIN area c on b.area_code = c.code
                        WHERE b.enable_flag = '1' 
                          AND (@product_id is null or b.Id = @product_id)
                          AND (@title is null or b.title like @title) ";
            }
            else if (marketingType == 1)
            {
                sql = @"SELECT
	                        a.*,
	                        b.Id pId,
	                        b.origin_price porigin_price,
	                        b.price pprice,
	                        b.title ptitle,
	                        b.team_price pteam_price,
	                        b.team_commission pteam_commission,
	                        c.name parea_name 
                        FROM
	                        product_marketing a
	                        INNER JOIN product b ON a.product_id = b.Id
                            LEFT JOIN area c on b.area_code = c.code
                        WHERE b.enable_flag = '1' 
                          AND (@product_id is null or b.Id = @product_id)
                          AND (@title is null or b.title like @title) ";
            }
            else if (marketingType == 2)
            {
                sql = @"SELECT
	                        a.*,
	                        b.Id pId,
	                        b.origin_price porigin_price,
	                        b.price pprice,
	                        b.title ptitle,
	                        b.team_price pteam_price,
	                        b.team_commission pteam_commission,
	                        c.name parea_name 
                        FROM
	                        product_marketing a
	                        RIGHT JOIN product b ON a.product_id = b.Id
                            LEFT JOIN area c on b.area_code = c.code
                        WHERE b.enable_flag = '1' 
                          AND (@product_id is null or b.Id = @product_id)
                          AND a.product_marketing_id is null 
                          AND (@title is null or b.title like @title) ";
            }
            else if (marketingType == 3)
            {
                sql = @"SELECT
	                        a.*,
	                        b.Id pId,
	                        b.origin_price porigin_price,
	                        b.price pprice,
	                        b.title ptitle,
	                        b.team_price pteam_price,
	                        b.team_commission pteam_commission,
	                        c.name parea_name 
                        FROM
	                        product_marketing a
	                        RIGHT JOIN product b ON a.product_id = b.Id
                            LEFT JOIN area c on b.area_code = c.code
                        WHERE b.enable_flag = '1' 
                          AND (@product_id is null or b.Id = @product_id)
                          AND b.team_commission >0 
                          AND (@title is null or b.title like @title) ";
            }
            var list = GetListBySql<productMarketingModel>(sql, paramters);

            return list.GetPagination(pagination).ToList();
        }

        /// <summary>
        /// 加载营销和产品门票信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public productMarketingModel GetProductMarketingModel(string id, string product_id)
        {
            List<DbParameter> paramters = new List<DbParameter>()
            {
                new  MySqlParameter ("@product_marketing_id", id == "null" ? null: id),
                new  MySqlParameter ("@product_id",product_id)
            };
            var list = GetListBySql<productMarketingModel>(@"SELECT
	                                                    a.*,
	                                                    b.Id pId,
	                                                    b.origin_price porigin_price,
	                                                    b.price pprice,
	                                                    b.title ptitle,
	                                                    b.team_price pteam_price,
	                                                    b.team_commission pteam_commission,
	                                                    c.name parea_name 
                                                    FROM
	                                                    product_marketing a
	                                         RIGHT JOIN product b ON a.product_id = b.Id AND b.enable_flag = '1'
                                              LEFT JOIN area c on b.area_code = c.code
                                                  WHERE (@product_marketing_id is not null AND a.product_marketing_id=@product_marketing_id)
                                                          OR (@product_marketing_id is null AND @product_id=b.Id )", paramters);
            if (list == null || list.Count == 0) return new productMarketingModel();
            return list[0];
        }


        /// <summary>
        /// 获取产品及最新的营销数据
        /// </summary>
        /// <returns></returns>
        public List<productMarketingModel> GetProductAndMarketingList(string product_type = null)
        {
            List<DbParameter> paramters = new List<DbParameter>()
            {
                new MySqlParameter("@product_type_id", product_type)
            };
            string sql = @"SELECT
	                        a.*,
	                        b.Id pId,
	                        b.origin_price porigin_price,
	                        b.price pprice,
	                        b.title ptitle,
	                        b.team_price pteam_price,
	                        b.team_commission pteam_commission,
	                        t.type_name pproduct_type_name
                        FROM
	                        product_marketing a
	                        RIGHT JOIN product b ON a.product_id = b.Id AND a.create_time =(select MAX(create_time) FROM product_marketing WHERE product_id=a.product_id )
                            LEFT JOIN product_type t on b.product_type_id = t.id
                        WHERE b.enable_flag = '1' 
                           AND (@product_type_id is null OR b.product_type_id = @product_type_id) 
                           AND b.special_status = 0 ";//只查产品?

            return GetListBySql<productMarketingModel>(sql, paramters);
        }
        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <returns></returns>
        public bool CheckTheDataByProductId(product_marketing theData)
        {
            var q = GetIQueryable().Where(x => x.product_id == theData.product_id && x.marketing_endtime >= theData.marketing_starttime);
            return q.ToList().Count > 0;
        }

        /// <summary>
        /// 获取产品最新的营销活动
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public product_marketing GetTheData(int product_id)
        {
            return GetIQueryable().FirstOrDefault(x => x.product_id == product_id && x.marketing_endtime > DateTime.Now);
        }

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public product_marketing GetTheData(string id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public void AddData(product_marketing newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(product_marketing theData)
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
    public class productMarketingModel : product_marketing
    {
        public String pId { get; set; }
        public String ptitle { get; set; }
        public decimal porigin_price { get; set; }
        public decimal pprice { get; set; }
        public decimal pteam_price { get; set; }
        public decimal pteam_commission { get; set; }
        public String parea_name { get; set; }
        public String pproduct_type_name { get; set; }
    }
}