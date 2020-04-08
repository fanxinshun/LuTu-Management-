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
        ProductBusiness _productBusiness = new ProductBusiness();
        product_dateBusiness _product_dateBusiness = new product_dateBusiness();
        DictionaryBusiness _dictionaryBusiness = new DictionaryBusiness();

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
            var dataList = _dictionaryBusiness.GetDictionaryListByCode("commodity");

            return Content(dataList.ToJson());
        }


        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(string product_type_id, string title, DateTime? create_time1, DateTime? create_time2, Pagination pagination)
        {
            var dataList = _productBusiness.GetDataList(2, string.Empty, title, string.Empty, create_time1, create_time2, pagination);

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
            if (!_productBusiness.PaymentAmount(theData.Id))
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
            //保存或更新开团日期
            foreach (var item in listProductDate)
            {
                item.product_id = theData.Id;
                if (item.product_date_id == 0)
                {
                    addProductDate.Add(item);
                }
                else
                {
                    updateProductDate.Add(item);
                }
            }
            _product_dateBusiness.Insert(addProductDate);
            _product_dateBusiness.Update(updateProductDate);
            return Success();
        }

        /// <summary>
        /// 下架数据
        /// </summary>
        /// <param name="theData">下架数据</param>
        public ActionResult DeleteData(string ids)
        {
            var listId = ids.ToList<int>();
            List<product> products = new List<product>();
            foreach (var item in listId)
            {
                products.Add(new product() { Id = item, enable_flag = "0", update_by = Operator.UserId, update_time = DateTime.Now });
            }
            _productBusiness.UpdateAny(products, new List<string>() { "enable_flag" });

            return Success("下架成功！");
        }

        /// <summary>
        /// 上传文件到文件系统服务器
        /// </summary>
        /// <param name="UploadType">图片字段</param>
        /// <param name="fileBase64"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult UploadFileToServer(string UploadType, int id, string fileBase64, string fileName)
        {
            string name = FastDFSHelper.UploadFile(fileBase64, fileName);
            if (name.IsNullOrEmpty())
            {
                return Error("上传失败");
            }
            var obj = _productBusiness.GetEntity(id);
            if (obj != null)
            {
                if (UploadType == "photooffarmers")
                {
                    obj.photooffarmers = obj.photooffarmers.IsNullOrEmpty() ? name : obj.photooffarmers + "," + name;
                    _productBusiness.UpdateAny(obj, new List<string>() { "photooffarmers" });
                }
                else if (UploadType == "commodity_photo")
                {
                    obj.commodity_photo = obj.commodity_photo.IsNullOrEmpty() ? name : obj.commodity_photo + "," + name;
                    _productBusiness.UpdateAny(obj, new List<string>() { "commodity_photo" });
                }
            }
            return Success((object)name);
        }
        #endregion


    }
}