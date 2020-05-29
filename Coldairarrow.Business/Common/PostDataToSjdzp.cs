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
        public readonly string postUrl = string.Empty;
        public readonly string authcode = string.Empty;
        public Dictionary<string, object> paramters = new Dictionary<string, object>();

        Base_AppSecretBusiness _Base_AppSecretBusiness = new Base_AppSecretBusiness();

        public PostDataToSjdzp()
        {
            List<Base_AppSecret> sjdzpParamters = _Base_AppSecretBusiness.GetDataList(new string[] { "sjdzp_Url", "sjdzp_pid", "sjdzp_authcode" });
            postUrl = sjdzpParamters.Find(x => x.AppId == "sjdzp_Url").AppSecret;
            authcode = sjdzpParamters.Find(x => x.AppId == "sjdzp_authcode").AppSecret;
            paramters.Add("_pid", sjdzpParamters.Find(x => x.AppId == "sjdzp_pid").AppSecret);

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
