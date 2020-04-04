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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new dictionary() : _dictionaryBusiness.GetTheData(id);

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
        public ActionResult GetDictionaryType()
        {
            var dataList = _dictionaryBusiness.GetDictionaryType();

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
        /// 上传文件到文件系统服务器
        /// </summary>
        /// <param name="UploadType">图片字段</param>
        /// <param name="fileBase64"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult UploadFileToServer(string UploadType, string id, string fileBase64, string fileName)
        {
            string resultData = string.Empty;
            var obj = _dictionaryBusiness.GetEntity(id);
            string name = FastDFSHelper.UploadFile(fileBase64, fileName);
            if (name.IsNullOrEmpty())
            {
                return Error("上传失败");
            }
            if (UploadType == "images")
            {
                obj.images = name;
                _dictionaryBusiness.UpdateAny(obj, new List<string>() { "images" });
                resultData = obj.images;
            }
            return Success((object)resultData);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            _dictionaryBusiness.DeleteData(ids.ToList<string>());

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