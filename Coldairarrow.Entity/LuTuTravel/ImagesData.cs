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
            ServicePath = FastDFSHelper.GetServicePath();
        }
        public string ServicePath { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}
