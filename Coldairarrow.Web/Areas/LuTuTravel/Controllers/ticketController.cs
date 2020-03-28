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
    public class TicketController : BaseMvcController
    {
        ProductBusiness _productBusiness = new ProductBusiness();
        DictionaryBusiness _dictionaryBusiness = new DictionaryBusiness();
        AreaBusiness _areaBusiness = new AreaBusiness();
        product_dateBusiness _product_dateBusiness = new product_dateBusiness();
        TeamBusiness _teamBusiness = new TeamBusiness();

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
        /// 获取景区级别
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDictionaryListByCode()
        {
            var dataList = _dictionaryBusiness.GetDictionaryListByCode("special_category");

            return Content(dataList.ToJson());
        }
        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAreaList(string id)
        {
            var dataList = _areaBusiness.GetDataList(id);
            var json = Content(dataList.ToJson());
            return json;
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(string product_type_id, string title, DateTime? create_time1, DateTime? create_time2, Pagination pagination)
        {
            var dataList = _productBusiness.GetDataList(1, product_type_id, title, string.Empty, create_time1, create_time2, pagination);

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
            if (theData.Id == 0)
            {
                theData.enable_flag = "1";
                theData.special_status = 1;
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
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
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
            string resultData = string.Empty;
            var obj = _productBusiness.GetEntity(id);
            string name = FastDFSHelper.UploadFile(fileBase64, fileName);
            if (name.IsNullOrEmpty())
            {
                return Error("上传失败");
            }
            if (UploadType == "images")
            {
                obj.images = obj.images.IsNullOrEmpty() ? name : obj.images + "," + name;
                _productBusiness.UpdateAny(obj, new List<string>() { "images" });
                resultData = obj.images;
            }
            else if (UploadType == "logo")
            {
                obj.logo = name;
                _productBusiness.UpdateAny(obj, new List<string>() { "logo" });
                resultData = obj.logo;
            }
            return Success((object)resultData);
        }
        #endregion
    }
}