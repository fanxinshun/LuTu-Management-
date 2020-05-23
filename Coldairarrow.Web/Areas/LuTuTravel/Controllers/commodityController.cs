using Coldairarrow.Business.Common;
using Coldairarrow.Business.LuTuTravel;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Coldairarrow.Util.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Web
{
    [Area("LuTuTravel")]
    public class commodityController : BaseMvcController
    {
        ProductBusiness _productBusiness = null;
        product_dateBusiness _product_dateBusiness = null;
        DictionaryBusiness _dictionaryBusiness = null;
        TeamBusiness _teamBusiness = null;
        public commodityController()
        {
            _productBusiness = new ProductBusiness();
            _product_dateBusiness = new product_dateBusiness();
            _dictionaryBusiness = new DictionaryBusiness();
            _teamBusiness = new TeamBusiness();
        }

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {

            var theProduct = id == null ? new product() : _productBusiness.GetTheData(id);
            List<product_date> theProductData = id == null ? new List<product_date>() : _product_dateBusiness.GetDataList(id);
            ViewData["theProduct"] = theProduct;
            ViewData["theProductData"] = theProductData;
            ViewData["emptyProductData"] = new product_date();

            var _ImagesBusiness = new ImagesBusiness();
            ViewData["ImagesDatas2"] = _ImagesBusiness.GetFilePath(id, "commodity_photo", theProduct.commodity_photo);
            ViewData["ImagesDatas3"] = _ImagesBusiness.GetFilePath(id, "logo", theProduct.logo);
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取商品所属类别
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDictionaryListByCode()
        {
            var dataList = _dictionaryBusiness.GetDictionaryEnabledByCode("commodity");

            return Content(dataList.ToJson());
        }


        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(string product_type_id, string title, DateTime? create_time1, DateTime? create_time2, Pagination pagination, string enable_flag)
        {
            var dataList = _productBusiness.GetDataList(2, product_type_id, title, string.Empty, create_time1, create_time2, pagination, enable_flag);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(product theData, List<product_date> listProductDate)
        {
            //校验价格是否亏本
            if (!_productBusiness.PaymentAmount(theData))
            {
                return Error("价格异常！请检查产品价格及营销价格");
            }
            if (theData.Id == 0)
            {
                theData.enable_flag = "1";
                theData.special_status = 2;
                theData.create_by = Operator.UserId;
                theData.create_time = DateTime.Now;

                _productBusiness.AddData(theData);
            }
            else
            {
                theData.update_by = Operator.UserId;
                theData.update_time = DateTime.Now;
                _productBusiness.UpdateData(theData);
            }

            var addProductDate = new List<product_date>();
            var updateProductDate = new List<product_date>();
            var teamlist = new List<team>();
            //保存或更新开团日期
            foreach (var item in listProductDate)
            {
                item.product_id = theData.Id;
                if (item.product_date_id == 0)
                {
                    addProductDate.Add(item);
                    teamlist.Add(new team() { product_id = theData.Id, start_time = item.open_date, status = 1 });
                }
                else
                {
                    updateProductDate.Add(item);
                }
            }
            _product_dateBusiness.Insert(addProductDate);
            _product_dateBusiness.Update(updateProductDate);
            _teamBusiness.Insert(teamlist);
            return Success();
        }
        #endregion


    }
}