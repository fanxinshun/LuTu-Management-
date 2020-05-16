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
    public class ProductController : BaseMvcController
    {
        ProductBusiness _productBusiness = null;
        DictionaryBusiness _dictionaryBusiness = null;
        AreaBusiness _areaBusiness = null;
        product_dateBusiness _product_dateBusiness = null;
        product_typeBusiness _product_typeBusiness = null;
        product_IntroductionBusiness _product_IntroductionBusiness = null;

        TeamBusiness _teamBusiness = null;
        public ProductController()
        {
            _productBusiness = new ProductBusiness();
            _dictionaryBusiness = new DictionaryBusiness();
            _areaBusiness = new AreaBusiness();
            _product_dateBusiness = new product_dateBusiness();
            _product_typeBusiness = new product_typeBusiness();
            _teamBusiness = new TeamBusiness();
            _product_IntroductionBusiness = new product_IntroductionBusiness();
        }

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int? id)
        {
            var theProduct = id == null ? new product() : _productBusiness.GetTheData(id);
            ViewData["theProduct"] = theProduct;

            List<product_date> theProductData = id == null ? new List<product_date>() : _product_dateBusiness.GetDataList(id);
            ViewData["theProductData"] = theProductData;

            var _ImagesBusiness = new ImagesBusiness();
            List<ImagesData> allImageDatas = new List<ImagesData>();
            List<product_Introduction> theProductIntroduction = id == null ? new List<product_Introduction>() { new product_Introduction() { days = 1 } } : _product_IntroductionBusiness.GetDataList(id);
            if (theProductIntroduction == null || theProductIntroduction.Count == 0)
            {
                theProductIntroduction = new List<product_Introduction>() { new product_Introduction() { Id = "", days = 1 } };
            }
            else
            {
                var IntroductionImages = new Dictionary<string, List<ImagesData>>() { };
                foreach (var item in theProductIntroduction)
                {
                    allImageDatas.AddRange(_ImagesBusiness.GetFilePath(id, item.Id, item.picture));
                }
                ViewData["IntroductionImages"] = IntroductionImages;

            }
            ViewData["theProductIntroduction"] = theProductIntroduction;

            ViewData["emptyProductData"] = new product_date();

            allImageDatas.AddRange(_ImagesBusiness.GetFilePath(id, "images", theProduct.images));
            allImageDatas.AddRange(_ImagesBusiness.GetFilePath(id, "logo", theProduct.logo));
            ViewData["allImageDatas"] = allImageDatas;
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSupplieListByCode()
        {
            var dataList = _dictionaryBusiness.GetDictionaryEnabledByCode("supplier");

            return Content(dataList.ToJson());
        }

        /// <summary>
        /// 获取类别
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductType(int? type)
        {
            var dataList = _product_typeBusiness.GetList(type);

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
        public ActionResult GetDataList(string product_type_id, string title, string supplier, DateTime? create_time1, DateTime? create_time2, string enable_flag, Pagination pagination)
        {
            var dataList = _productBusiness.GetDataList(0, product_type_id, title, supplier, create_time1, create_time2, pagination, enable_flag);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(product theData, List<product_date> listProductDate, List<product_Introduction> datalistIntroduction)
        {
            //校验价格是否亏本
            if (!_productBusiness.PaymentAmount(theData))
            {
                return Error("价格异常！请检查产品价格及营销价格");
            }

            var supplier = _dictionaryBusiness.GetTheData(theData.supplier);
            if (supplier == null)//创建供应商
            {
                var su = new dictionary()
                {
                    Id = Guid.NewGuid().ToSequentialGuid(),
                    code = "supplier",
                    name = theData.supplier,
                    sort = 0,
                    enable_flag = "1"
                };
                _dictionaryBusiness.AddData(su);
                theData.supplier = su.Id;
            }
            if (theData.Id == 0)
            {
                theData.enable_flag = "1";
                theData.special_status = 0;
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

            var addIntroductions = new List<product_Introduction>();
            foreach (var item in datalistIntroduction)
            {
                if (item.Id.IsNullOrEmpty())
                {
                    addIntroductions.Add(new product_Introduction()
                    {
                        Id = Guid.NewGuid().ToSequentialGuid(),
                        productId = theData.Id,
                        days = item.days,
                        title = item.title,
                        scheduling = item.scheduling,
                        stay = item.stay,
                        food = item.food,
                        create_by = Operator.UserId,
                        create_time = DateTime.Now
                    });
                }
                else
                {
                    var introduction = new product_Introduction()
                    {
                        Id = item.Id,
                        days = item.days,
                        title = item.title,
                        scheduling = item.scheduling,
                        stay = item.stay,
                        food = item.food,
                        update_by = Operator.UserId,
                        update_time = DateTime.Now
                    };
                    _product_IntroductionBusiness.UpdateAny(introduction, new List<string>() { "days", "title", "scheduling", "stay", "food", "update_by", "update_time" });
                }
            }
            _product_IntroductionBusiness.Insert(addIntroductions);

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

        /// <summary>
        /// 上架/下架产品
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(int id)
        {
            _productBusiness.ChangeStatus(id);
            return Success();
        }
        /// <summary>
        /// 复制产品
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult CopyData(int id)
        {
            var theData = _productBusiness.GetTheData(id).DeepClone();
            var teamlist = new List<team>();
            if (theData != null)
            {
                var listProductDate = _product_dateBusiness.GetDataList(theData.Id);
                theData.Id = 0;
                theData.create_by = Operator.UserId;
                theData.create_time = DateTime.Now;
                _productBusiness.Insert(theData);
                foreach (var item in listProductDate)
                {
                    item.product_date_id = 0;
                    teamlist.Add(new team() { product_id = theData.Id, start_time = item.open_date, status = 1 });
                }
                _product_dateBusiness.Insert(listProductDate);
                _teamBusiness.Insert(teamlist);

            }
            return Success("复制成功！");
        }
        #endregion
    }
}