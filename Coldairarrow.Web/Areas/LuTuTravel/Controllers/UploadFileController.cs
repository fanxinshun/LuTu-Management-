using Coldairarrow.Business.LuTuTravel;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Coldairarrow.Util.Helper;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Coldairarrow.Web.Controllers
{
    [Area("LuTuTravel")]
    public class UploadFileController : BaseMvcController
    {
        ImagesBusiness _ImagesBusiness;
        public UploadFileController()
        {
            _ImagesBusiness = new ImagesBusiness();
        }

        #region 视图

        public ActionResult UploadFileForm(string UploadType, string Id, string Multiple)
        {
            ViewData["Id"] = Id;
            ViewData["UploadType"] = UploadType;
            ViewData["Multiple"] = Multiple;
            return View();
        }

        #endregion

        #region 接口

        /// <summary>
        /// 上传文件到文件系统服务器
        /// </summary>
        /// <param name="UploadType">图片字段</param>
        /// <param name="fileBase64"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult UploadFileToServer(string uploadType, string id, string multiple, string fileBase64, string fileName)
        {
            AjaxResult res = _ImagesBusiness.UploadFileToServer(uploadType, id, multiple, fileBase64, fileName);
            return Content(res.ToJson());
        }
        /// <summary>
        /// 删除文件及记录
        /// </summary>
        /// <param name="UploadType">图片字段</param>
        /// <param name="fileBase64"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult DeleteFileToServer(string deleteType, string id, string fileName)
        {
            AjaxResult res = _ImagesBusiness.DeleteFileToServer(deleteType, id, fileName);
            return Content(res.ToJson());
        }

        #endregion
    }
}