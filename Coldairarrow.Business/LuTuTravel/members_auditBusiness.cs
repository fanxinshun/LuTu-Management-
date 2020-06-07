using Coldairarrow.Entity.LuTuTravel;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.LuTuTravel
{
    public class members_auditBusiness : BaseBusiness<members_audit>
    {
        #region 外部接口

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<members_audit> GetDataList(string condition, string keyword, Pagination pagination)
        {
            var q = GetIQueryable();

            //模糊查询
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
                q = q.Where($@"{condition}.Contains(@0)", keyword);

            var memberids = q.Select(x => x.member_id).ToList();
            var members = new membersBusiness().GetIQueryable().Where(x => memberids.Contains(x.oppen_id)).ToList();
            var list = q.ToList();
            list.ForEach(x => x.member_id = members.FirstOrDefault(y => y.oppen_id == x.member_id)?.nick_name);

            return list.GetPagination(pagination).ToList();
        }

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public members_audit GetTheData(string id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public void AddData(members_audit newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(members_audit theData)
        {
            Update(theData);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public void DeleteData(List<string> ids)
        {
            Delete(ids);
        }

        /// <summary>
        /// 同意团长审核
        /// </summary>
        /// <param name="id"></param>
        public void ChangeOperateStatusAgree(string id)
        {
            var theData = GetTheData(id);
            theData.status = 1;

            var _membersBusiness = new membersBusiness();
            var member = _membersBusiness.GetTheData(theData.member_id);
            if (theData.invite_code.IsNullOrEmpty() || theData.invite_code.Length == 6)//一级团长 生成8位邀请码
            {
                member.header_status = 1;
                member.header_time = DateTime.Now.ToCstTime();
                member.header_level = 1;
                member.invite_code = RandomHelper.GenRandom(8);
                if (!theData.invite_code.IsNullOrEmpty())
                {
                    member.share_invite_code = theData.invite_code;
                }
            }
            if (theData.invite_code.Length == 8) //二级团长 不生成邀请码
            {
                member.header_status = 1;
                member.header_time = DateTime.Now.ToCstTime();
                member.header_level = 2;
                member.share_invite_code = theData.invite_code;
            }
            UpdateData(theData);
            _membersBusiness.Update(member);
        }
        /// <summary>
        /// 拒绝团长审核
        /// </summary>
        /// <param name="id"></param>
        public void ChangeOperateStatusDisAgree(string id)
        {
            var theData = GetTheData(id);
            theData.status = 3;
            UpdateData(theData);
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}