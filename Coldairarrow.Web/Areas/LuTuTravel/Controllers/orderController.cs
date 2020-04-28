using Coldairarrow.Business.Common;
using Coldairarrow.Business.LuTuTravel;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Coldairarrow.Web
{
    [Area("LuTuTravel")]
    public class orderController : BaseMvcController
    {
        orderBusiness _orderBusiness = new orderBusiness();

        #region 视图功能

        public ActionResult productIndex()
        {
            ViewData["special_status"] = "0";
            return View("Index");
        }
        public ActionResult ticketIndex()
        {
            ViewData["special_status"] = "1";
            return View("Index");
        }
        public ActionResult commodityIndex()
        {
            ViewData["special_status"] = "2";
            return View("Index");
        }
        

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(int? special_status, string product_id, string product_name, string product_area, string start_time1, string start_time2, string create_time1, string create_time2, Pagination pagination)
        {
            var dataList = _orderBusiness.GetDataList(special_status, Operator.UserId, product_id, product_name, product_area, start_time1, start_time2, create_time1, create_time2, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据
         

        #endregion
    }
}