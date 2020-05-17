using Coldairarrow.Business.LuTuTravel;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Web
{
    [Area("LuTuTravel")]
    public class members_auditController : BaseMvcController
    {
        members_auditBusiness _members_auditBusiness = new members_auditBusiness();

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new members_audit() : _members_auditBusiness.GetTheData(id);

            var _ImagesBusiness = new ImagesBusiness();
            ViewData["ImagesDatas2"] = _ImagesBusiness.GetFilePath(id, "commodity_photo", theData.image_group);
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
        public ActionResult GetDataList(string condition, string keyword, Pagination pagination)
        {
            var dataList = _members_auditBusiness.GetDataList(condition, keyword, pagination);
            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(members_audit theData)
        {
            if (theData.id.IsNullOrEmpty())
            {
                theData.id = Guid.NewGuid().ToSequentialGuid();

                _members_auditBusiness.AddData(theData);
            }
            else
            {
                _members_auditBusiness.UpdateData(theData);
            }

            return Success();
        }


        /// <summary>
        /// 同意团长审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChangeOperateStatusAgree(string id)
        {
            _members_auditBusiness.ChangeOperateStatusAgree(id);
            return Success();
        }

        /// <summary>
        /// 拒绝团长审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChangeOperateStatusDisAgree(string id)
        {
            _members_auditBusiness.ChangeOperateStatusDisAgree(id);
            return Success();
        }

        #endregion
    }
}