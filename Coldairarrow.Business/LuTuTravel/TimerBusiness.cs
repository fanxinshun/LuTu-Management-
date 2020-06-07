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
        private static DateTime currientTime = DateTime.Now.ToCstTime();//当前时间

        private static DateTime delyTime = DateTime.Parse(currientTime.AddDays(1).ToShortDateString() + " 04:00:00");//延迟到什么时间执行
        private static TimeSpan delyTimeSpan = delyTime.Subtract(currientTime).Duration();//延迟多久(TimeSpan)

        public TimerBusiness()
        {
            _TicketsBusiness.WriteSysLog($"定时任务启动：currientTime:{currientTime}，delyTime:{delyTime}，intervalTime:{intervalTime}，delyTimeSpan:{delyTimeSpan}");
            TimerHelper.SetInterval(GetTickets, intervalTime, delyTimeSpan);//开启定时任务
        }

        /// <summary>
        /// 获取门票数据
        /// </summary>
        public void GetTickets()
        {
            try
            {
                _TicketsBusiness.WriteSysLog("同步门票数据---开始同步");

                Dictionary<string, object> paramters = base.paramters.Copy(0, base.paramters.Count).ToDictionary(k => k.Key, v => v.Value);
                paramters["method"] = "item_list";
                //_TicketsBusiness.DeleteAll();//全删？
                while (true)
                {
                    string resultData = HttpHelper.PostData(postUrl, base.GetParamsAndSig(paramters));
                    _TicketsBusiness.WriteSysLog($"【{paramters["page"]}】{resultData}");//记录每次同步的数据日志

                    ResponseModel model = resultData.ToObject<ResponseModel>();
                    List<Tickets> listTickets = model.list.ToJson().ToList<Tickets>();
                    if (listTickets?.Count == 0) break;

                    _TicketsBusiness.SynchronousData(listTickets);//更新product表门票数据
                    paramters["page"] = int.Parse(paramters["page"].ToString()) + 1;
                }
                _TicketsBusiness.WriteSysLog("同步门票数据---同步完成");
            }
            catch (Exception ex)
            {
                _TicketsBusiness.WriteSysLog($"同步门票数据---发生异常！{ex}");
            }
        }
    }
}
