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
    public class dictionaryController : BaseMvcController
    {
        DictionaryBusiness _dictionaryBusiness = new DictionaryBusiness();

        #region 视图功能

        public ActionResult Index(string dictionaryType)
        {
            ViewData["dictionaryType"] = dictionaryType ?? "";
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new dictionary() : _dictionaryBusiness.GetTheData(id);
            var _ImagesBusiness = new ImagesBusiness();
            ViewData["ImagesDatas1"] = _ImagesBusiness.GetFilePath(theData.images);

            return View(theData);
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取DictionaryType
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDictionaryType(string code)
        {
            var dataList = _dictionaryBusiness.GetDictionaryType(code);

            return Content(dataList.ToJson());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(string code, Pagination pagination)
        {
            var dataList = _dictionaryBusiness.GetDataList(code, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(dictionary theData)
        {
            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id = Guid.NewGuid().ToSequentialGuid();

                _dictionaryBusiness.AddData(theData);
            }
            else
            {
                _dictionaryBusiness.UpdateData(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            var listId = ids.ToList<string>();
            List<dictionary> dictionarys = new List<dictionary>();
            foreach (var item in listId)
            {
                dictionarys.Add(new dictionary() { Id = item, enable_flag = "0" });
            }
            _dictionaryBusiness.UpdateAny(dictionarys, new List<string>() { "enable_flag" });

            return Success("删除成功！");
        }

        #endregion
    }
}