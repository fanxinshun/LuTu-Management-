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
        product_marketingBusiness _product_marketingBusiness = new product_marketingBusiness();

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new productMarketingModel() : _product_marketingBusiness.GetProductMarketingModel(id);

            return View(theData);
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="title">产品/门票名称</param>
        /// <returns></returns>
        public ActionResult GetDataList(string marketingType, string title, Pagination pagination)
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