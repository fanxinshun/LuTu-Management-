using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Business.LuTuTravel;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Coldairarrow.Util.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coldairarrow.Business.Common
{
    public class PostDataToSjdzp
    {
        public readonly string postUrl = ConfigHelper.GetValue("AOMIAPI", "url");
        public readonly string authcode = ConfigHelper.GetValue("AOMIAPI", "authcode");
        public readonly string pid = ConfigHelper.GetValue("AOMIAPI", "pid");
        public Dictionary<string, object> paramters = new Dictionary<string, object>();

        Base_AppSecretBusiness _Base_AppSecretBusiness = new Base_AppSecretBusiness();

        public PostDataToSjdzp()
        {
            paramters.Add("_pid", pid);
            paramters.Add("format", "json");
            paramters.Add("method", string.Empty);
            paramters.Add("page", 1);
            paramters.Add("size", 10);

        }

        /// <summary>
        /// 获取签名加参数
        /// </summary>
        /// <param name="P"></param>
        /// <param name="S"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetParamsAndSig(Dictionary<string, object> P)
        {
            string body = authcode + "&" + HttpHelper.BuildBody(P, ContentType.Form) + "&" + authcode;
            Dictionary<string, object> sig = new Dictionary<string, object>
            {
                { "_sig", body.ToMD5String() }
            };

            return P.Concat(sig).ToDictionary(k => k.Key, v => v.Value);
        }
    }
}
