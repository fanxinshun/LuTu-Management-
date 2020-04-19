using Coldairarrow.Business.Common;
using Coldairarrow.Business.LuTuTravel;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Coldairarrow.Util.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Web
{
    [Area("LuTuTravel")]
    public class productRepController : BaseMvcController
    {
        ProductRepBusiness _productBusiness;
        public productRepController()
        {
            _productBusiness = new ProductRepBusiness();
        }
        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profit()
        {
            return View();
        }
        public ActionResult PurchaseChannels()
        {
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductRepData(int days)
        {
            var data = _productBusiness.GetProductRepData(days);

            return Success(data);
        }

        /// <summary>
        /// 获取利润图表数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductProfit()
        {
            var data = _productBusiness.GetProductProfit();

            return Success(data);
        }
        /// <summary>
        /// 获取利润排行数据
        /// </summary>
        /// <param name="product_type"></param>
        /// <returns></returns>
        public ActionResult GetProductProfitDetail(string product_type, Pagination pagination)
        {
            var dataList = _productBusiness.GetProductProfitDetail(product_type, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }
        /// <summary>
        /// 获取销售渠道
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPurchaseChannels(int days)
        {
            var data = _productBusiness.GetPurchaseChannels(days);
            return Success(data);
        }
        #endregion

        #region 提交数据

        #endregion
    }
}