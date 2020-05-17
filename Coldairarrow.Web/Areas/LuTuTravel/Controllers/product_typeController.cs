using Coldairarrow.Business.Common;
using Coldairarrow.Business.LuTuTravel;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Dynamitey.DynamicObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Web
{
    [Area("LuTuTravel")]
    public class product_typeController : BaseMvcController
    {
        product_typeBusiness _product_typeBusiness = new product_typeBusiness();

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new product_type() : _product_typeBusiness.GetTheData(id);

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
        public ActionResult GetDataList(string type_name, Pagination pagination)
        {
            var dataList = _product_typeBusiness.GetDataList(type_name, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(product_type theData)
        {
            if (theData.id.IsNullOrEmpty())
            {
                theData.id = Guid.NewGuid().ToSequentialGuid();
                theData.create_by = Operator.UserId;
                theData.create_time = DateTime.Now;
                theData.enable_flag = 1;
                _product_typeBusiness.AddData(theData);
            }
            else
            {
                theData.update_by = Operator.UserId;
                theData.update_time = DateTime.Now;
                _product_typeBusiness.UpdateData(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            List<string> listId = ids.ToList<string>();
            _product_typeBusiness.DeleteData(listId);

            return Success("删除成功！");
        }

        #endregion
    }
}