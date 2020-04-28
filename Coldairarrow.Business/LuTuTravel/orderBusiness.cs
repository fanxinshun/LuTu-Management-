using Coldairarrow.Business.Base_SysManage;
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
    public class orderBusiness : BaseBusiness<order>
    {
        #region 外部接口

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<orderModule> GetDataList(int? special_status, string userId, string product_id, string product_name, string product_area, string start_time1, string start_time2, string create_time1, string create_time2, Pagination pagination)
        {
            var supplier = new Base_UserBusiness().GetIQueryable().FirstOrDefault(x => x.UserId == userId)?.Supplier;
            List<DbParameter> paramters = new List<DbParameter>()
            {
                new  MySqlParameter ("@special_status",special_status),
                new  MySqlParameter ("@product_id",product_id),
                new  MySqlParameter ("@product_name",product_name),
                new  MySqlParameter ("@start_time1",start_time1),
                new  MySqlParameter ("@start_time2",start_time2),
                new  MySqlParameter ("@create_time1",create_time1),
                new  MySqlParameter ("@create_time2",create_time2),
                new  MySqlParameter ("@supplier",supplier)
            };
            DataTable table = GetDataTableWithSql(@"SELECT o.*,
				                                            p.title productName,
				                                            m.real_name realName,
				                                            m.real_name nickName,
				                                            py.`status` payStatus
					                                   FROM `order` o
		                                         INNER JOIN product p ON o.product_id = p.Id
		                                          LEFT JOIN members m ON o.member_id = m.oppen_id
		                                          LEFT JOIN pay py ON py.order_id = o.Id
				                                      WHERE p.special_status = @special_status 
                                                        AND (@product_id IS NULL OR p.Id = @product_id)
				                                        AND (@product_name IS NULL OR p.title LIKE @product_name)
				                                        AND (@start_time1 IS NULL OR o.start_time >= @start_time1)
				                                        AND (@start_time2 IS NULL OR o.start_time <= @start_time2)
				                                        AND (@create_time1 IS NULL OR o.create_time >= @create_time1)
				                                        AND (@create_time2 IS NULL OR o.create_time <= @create_time2)
				                                        AND (@supplier IS NULL OR p.supplier = @supplier)", paramters);

            return table.ToList<orderModule>().GetPagination(pagination).ToList();
        }


        #endregion


    }

    #region 数据模型
    public class orderModule : order
    {
        public string productName { get; set; }
        public string nickName { get; set; }
        public string realName { get; set; }
        public int payStatus { get; set; }
    }
    #endregion
}