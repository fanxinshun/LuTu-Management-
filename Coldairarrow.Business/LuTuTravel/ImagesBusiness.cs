﻿using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Coldairarrow.Util.Helper;
using System;
using System.Collections.Generic;
using Coldairarrow.Entity.LuTuTravel;
using System.Text;

namespace Coldairarrow.Business.LuTuTravel
{
    public class ImagesBusiness : BaseBusiness<ImagesData>
    {
        public List<ImagesData> GetFilePath(string fileNames)
        {
            var imagesDatas = new List<ImagesData>() { };
            if (fileNames.IsNullOrEmpty())
            {
                return imagesDatas;
            }
            string[] names = fileNames.Split(',');
            for (int i = 0; i < names.Length; i++)
            {
                var imagesData = new ImagesData();
                imagesData.ImageUrl = imagesData.ServicePath + names[i];
                imagesDatas.Add(imagesData);
            }
            return imagesDatas;
        }

        /// <summary>
        /// 上传文件到文件系统服务器
        /// </summary>
        /// <param name="uploadType">图片字段</param>
        /// <param name="fileBase64"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public AjaxResult UploadFileToServer(string uploadType, string id, string multiple, string fileBase64, string fileName)
        {
            string name = FastDFSHelper.UploadFile(fileBase64, fileName);
            if (name.IsNullOrEmpty())
            {
                return Error("上传失败");
            }
            dynamic obj = null;
            var _ProductBusiness = new ProductBusiness();
            var _product_tagBusiness = new product_tagBusiness();
            switch (uploadType)
            {
                case "images":
                case "logo":
                case "photooffarmers":
                case "commodity_photo":
                    obj = (product)_ProductBusiness.GetEntity(id.ToInt());
                    break;
                case "img_url":
                    obj = (product_tag)_product_tagBusiness.GetEntity(id);
                    break;
                default:
                    break;
            }
            if (obj != null)
            {
                switch (uploadType)
                {
                    case "images"://产品图片
                        obj.images = multiple == "0" ? name : (string.IsNullOrEmpty(obj.images) ? name : obj.images + "," + name);
                        _ProductBusiness.UpdateAny(obj, new List<string>() { "images" });
                        break;
                    case "logo"://logo
                        obj.logo = multiple == "0" ? name : (string.IsNullOrEmpty(obj) ? name : obj.logo + "," + name);
                        _ProductBusiness.UpdateAny(obj, new List<string>() { "logo" });
                        break;
                    case "photooffarmers"://农户照片
                        obj.photooffarmers = multiple == "0" ? name : (string.IsNullOrEmpty(obj.photooffarmers) ? name : obj.photooffarmers + "," + name);
                        _ProductBusiness.UpdateAny(obj, new List<string>() { "photooffarmers" });
                        break;
                    case "commodity_photo"://扶贫商品照片
                        obj.commodity_photo = multiple == "0" ? name : (string.IsNullOrEmpty(obj.commodity_photo) ? name : obj.commodity_photo + "," + name);
                        _ProductBusiness.UpdateAny(obj, new List<string>() { "commodity_photo" });
                        break;
                    case "img_url"://热门城市logo 
                        obj.img_url = multiple == "0" ? name : (string.IsNullOrEmpty(obj.img_url.IsNullOrEmpty) ? name : obj.img_url + "," + name);
                        _product_tagBusiness.UpdateAny(obj, new List<string>() { "img_url" });
                        break;
                    default:
                        break;
                }
            }
            return Success(new ImagesData() { ImageName = name });
        }
    }
}