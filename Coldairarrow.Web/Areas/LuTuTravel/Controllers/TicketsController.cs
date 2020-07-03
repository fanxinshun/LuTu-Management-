using Coldairarrow.Business.LuTuTravel;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Web
{
    [Area("LuTuTravel")]
    public class TicketsController : BaseMvcController
    {
        TicketsBusiness _ticketsBusiness = new TicketsBusiness();

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int id)
        {
            var theData = id.IsNullOrEmpty() ? new Tickets() : _ticketsBusiness.GetTheData(id);

            return View(theData);
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="title">门票标题</param>
        /// <param name="notice">购买须知</param
        /// <param name="brief">产品简介</param>
        /// <param name="refund_type">退票类型</param>
        /// <param name="status">上线状态</param>
        /// <returns></returns>
        public ActionResult GetDataList(string title, string notice, string brief, int? refund_type, int? status, Pagination pagination)
        {
            var dataList = _ticketsBusiness.GetDataList(title, notice, brief, refund_type, status, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(Tickets theData)
        {
            if (theData.Id.IsNullOrEmpty())
            {
                _ticketsBusiness.AddData(theData);
            }
            else
            {
                _ticketsBusiness.UpdateData(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public AjaxResult ChangeStatus(List<int> ids)
        {
            return _ticketsBusiness.ChangeStatus(ids);
        }

        #endregion
    }
}