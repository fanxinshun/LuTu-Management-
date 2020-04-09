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
    public class product_tagController : BaseMvcController
    {
        AreaBusiness _areaBusiness = new AreaBusiness();
        product_tagBusiness _product_tagBusiness = new product_tagBusiness();

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new productTagModel() : _product_tagBusiness.GetTheProductTagModel(id);

            return View(theData);
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(string product_type_id, string parent_id, Pagination pagination)
        {
            var dataList = _product_tagBusiness.GetProductTagModel(product_type_id, parent_id, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        /// <summary>
        /// 获取父级城市标签
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetpanentTagname()
        {
            var dataList = _product_tagBusiness.GetProductTagModel();

            return Content(dataList.ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(productTagModel theData)
        {
            product_tag parentTag = _product_tagBusiness.GetTheParentTag(theData.product_type_id, theData.tagname);
            if (parentTag == null)//如果 产品类型+标签不存在，说明这是新的标签，parent_id就是输入的tagname，新增一个标签！
            {
                parentTag = theData.DeepClone();
                parentTag.id = Guid.NewGuid().ToSequentialGuid();
                parentTag.tagname = theData.tagname.IsNullOrEmpty() ? theData.parent_id : theData.tagname;
                parentTag.parent_id = null;
                parentTag.area_code = null;
                parentTag.img_url = null;
                parentTag.create_by = Operator.UserId;
                parentTag.create_time = DateTime.Now;

                _product_tagBusiness.AddData(parentTag);
                theData.parent_id = parentTag.id;
                theData.tagname = parentTag.tagname;
            }
            if (theData.id.IsNullOrEmpty())
            {
                theData.id = Guid.NewGuid().ToSequentialGuid();
                theData.create_by = Operator.UserId;
                theData.create_time = DateTime.Now;
                _product_tagBusiness.AddData(theData);
            }
            else
            {
                theData.update_by = Operator.UserId;
                theData.update_time = DateTime.Now;
                _product_tagBusiness.UpdateData(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            List<string> idList = ids.ToList<string>();
            var theDataList = _product_tagBusiness.GetList();
            foreach (var item in idList)
            {
                var theData = theDataList.Find(x => x.id == item);
                theDataList.Remove(theData);
                _product_tagBusiness.Delete(theData);

                var pObj = theDataList.Find(x => x.parent_id == theData.parent_id);
                if (pObj == null)
                {
                    theDataList.Remove(theDataList.Find(x => x.id == theData.parent_id));
                    _product_tagBusiness.Delete(theData.parent_id);
                }
            }

            return Success("删除成功！");
        }

        /// <summary>
        /// 上传文件到文件系统服务器
        /// </summary>
        /// <param name="UploadType">图片字段</param>
        /// <param name="fileBase64"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult UploadFileToServer(string id, string fileBase64, string fileName)
        {
            string name = FastDFSHelper.UploadFile(fileBase64, fileName);
            if (name.IsNullOrEmpty())
            {
                return Error("上传失败");
            }
            var obj = _product_tagBusiness.GetTheData(id);
            if (obj != null)
            {
                obj.img_url = name;
                _product_tagBusiness.UpdateAny(obj, new List<string>() { "img_url" });
            }
            return Success((object)name);
        }
        #endregion
    }
}