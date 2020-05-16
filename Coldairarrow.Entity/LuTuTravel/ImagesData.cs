using Coldairarrow.Util;
using Coldairarrow.Util.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Entity.LuTuTravel
{
    public class ImagesData
    {
        public ImagesData()
        {
            ServicePath = GlobalSwitch.fastdfs_downloadServer;
        }
        public object ImgObject { get; set; }
        public string ImgType { get; set; }
        public string ServicePath { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}
