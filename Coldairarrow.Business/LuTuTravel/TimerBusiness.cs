using Coldairarrow.Business.Common;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using Coldairarrow.Util.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coldairarrow.Business.LuTuTravel
{
    public class TimerBusiness : PostDataToSjdzp
    {
        TicketsBusiness _TicketsBusiness = new TicketsBusiness();

        private TimeSpan intervalTime = new TimeSpan(24, 0, 0);//执行间隔
        private static DateTime currientTime = DateTime.Now;//当前时间

        private static DateTime time24 = DateTime.Parse(currientTime.ToShortDateString() + " 23:59:59");//当天24点
        private static TimeSpan delyTimeSpan = time24 - currientTime.AddHours(1);//当天24点-当前时间+1小时，即每天1点开始同步

        public TimerBusiness()
        {
            TimerHelper.SetInterval(GetTickets, intervalTime, delyTimeSpan);//执行定时任务
        }

        /// <summary>
        /// 获取门票数据
        /// </summary>
        public void GetTickets()
        {
            _TicketsBusiness.WriteSysLog("同步门票数据---开始同步");
            _TicketsBusiness.DeleteAll();
            Dictionary<string, object> paramters = base.paramters.Copy(0, base.paramters.Count).ToDictionary(k => k.Key, v => v.Value);
            paramters["method"] = "item_list";

            while (true)
            {
                string resultData = HttpHelper.PostData(postUrl, base.GetParamsAndSig(paramters));
                ResponseModel model = resultData.ToObject<ResponseModel>();
                List<Tickets> listTickets = model.list.ToJson().ToList<Tickets>();
                if (listTickets?.Count == 0) break;

                _TicketsBusiness.Insert(listTickets);//插入数据
                //_TicketsBusiness.SynchronousData(listTickets);//更新product表门票数据
                paramters["page"] = int.Parse(paramters["page"].ToString()) + 1;
            }
            _TicketsBusiness.WriteSysLog("同步门票数据---同步完成");
        }
    }
}
