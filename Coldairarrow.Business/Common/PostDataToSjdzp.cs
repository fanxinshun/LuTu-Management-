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
        private readonly string postUrl = string.Empty;
        private readonly string authcode = string.Empty;
        private int page = 1;
        Dictionary<string, object> paramters = new Dictionary<string, object>();
        Dictionary<string, object> sig = new Dictionary<string, object>();

        Base_AppSecretBusiness _Base_AppSecretBusiness = new Base_AppSecretBusiness();
        TicketsBusiness _TicketsBusiness = new TicketsBusiness();
        public PostDataToSjdzp()
        {
            List<Base_AppSecret> sjdzpParamters = _Base_AppSecretBusiness.GetDataList(new string[] { "sjdzp_Url", "sjdzp_pid", "sjdzp_authcode" });
            postUrl = sjdzpParamters.FirstOrDefault(x => x.AppId == "sjdzp_Url")?.AppSecret;
            paramters.Add("_pid", sjdzpParamters.FirstOrDefault(x => x.AppId == "sjdzp_pid")?.AppSecret);
            paramters.Add("format", "json");
            paramters.Add("method", string.Empty);
            paramters.Add("page", page);
            paramters.Add("size", 50);
            authcode = sjdzpParamters.FirstOrDefault(x => x.AppId == "sjdzp_authcode")?.AppSecret;

        }

        /// <summary>
        /// 获取门票数据
        /// </summary>
        public void GetTickets()
        {
            _TicketsBusiness.DeleteAll();
            paramters["method"] = "item_list";
            while (true)
            {
                paramters["page"] = page;
                string body = authcode + "&" + HttpHelper.BuildBody(paramters, ContentType.Form) + "&" + authcode;
                sig["_sig"] = body.ToMD5String();

                string resultData = HttpHelper.PostData(postUrl, paramters.Concat(sig).ToDictionary(k => k.Key, v => v.Value));
                ResponseModel model = resultData.ToObject<ResponseModel>();
                List<Tickets> listTickets = model.list.ToJson().ToList<Tickets>();
                if (listTickets?.Count == 0) break;

                _TicketsBusiness.Insert(listTickets);
                page++;
            }
        }
    }
}
