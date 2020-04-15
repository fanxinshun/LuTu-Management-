using Coldairarrow.Business.Common;
using Coldairarrow.Business.LuTuTravel;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Coldairarrow.Web
{
    [Area("LuTuTravel")]
    public class product_marketingController : BaseMvcController
    {
        ProductBusiness _productBusiness = new ProductBusiness();
        product_marketingBusiness _product_marketingBusiness = new product_marketingBusiness();

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id, string pId)
        {
            var theData = _product_marketingBusiness.GetProductMarketingModel(id, pId);

            return View(theData);
        }
        /// <summary>
        /// 获取未参加营销的产品/门票数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNoProductMarketingList()
        {
            //var dataList = _product_marketingBusiness.GetNoProductMarketingList();
            var dataList = _productBusiness.GetDataList();

            return Content(dataList.ToJson());
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="title">产品/门票名称</param>
        /// <returns></returns>
        public ActionResult GetDataList(int marketingType, string title, Pagination pagination)
        {
            var dataList = _product_marketingBusiness.GetDataList(marketingType, title, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(product_marketing theData)
        {
            if (theData.marketing_starttime > theData.marketing_endtime)
            {
                return Error("结束时间必须大于开始时间");
            }
            product product = _productBusiness.GetTheData(theData.product_id);
            //校验价格是否亏本
            if (!_productBusiness.PaymentAmount(product, theData))
            {
                return Error("价格异常！请检查产品价格及营销价格");
            }
            //校验一下该产品能否参加营销
            //if (_product_marketingBusiness.CheckTheDataByProductId(theData))
            //{
            //    return Error("当前时间段正在营销中，请检查");
            //}

            if (theData.product_marketing_id.IsNullOrEmpty())
            {
                theData.product_marketing_id = Guid.NewGuid().ToSequentialGuid();
                theData.create_by = Operator.UserId;
                theData.create_time = DateTime.Now;
                _product_marketingBusiness.AddData(theData);
            }
            else
            {
                theData.update_by = Operator.UserId;
                theData.update_time = DateTime.Now;
                _product_marketingBusiness.UpdateData(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            _product_marketingBusiness.DeleteData(ids.ToList<string>());

            return Success("删除成功！");
        }

        #endregion
    }
}