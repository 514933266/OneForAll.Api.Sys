using AutoMapper;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.Entities;
using Sys.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application
{
    /// <summary>
    /// 微信用户
    /// </summary>
    public class SysWechatLoginUserService : ISysWechatLoginUserService
    {
        private readonly IMapper _mapper;
        private readonly ISysWechatUserQueryService _wxUserQuery;
        private readonly ISysUserQueryService _userQuery;
        public SysWechatLoginUserService(IMapper mapper, ISysUserQueryService userQuery, ISysWechatUserQueryService wxUserQuery)
        {
            _mapper = mapper;
            _userQuery = userQuery;
            _wxUserQuery = wxUserQuery;
        }

        /// <summary>
        /// 根据电话获取微信登录用户
        /// </summary>
        /// <param name="tenantId">机构id</param>
        /// <param name="mobile">手机号码</param>
        /// <returns>用户列表</returns>
        public async Task<SysWechatLoginUserDto> GetByMobileAsync(Guid tenantId, string mobile)
        {
            var user = new SysWechatLoginUserDto();
            var wxUser = await _wxUserQuery.GetByMobileAsync(mobile);
            if (wxUser == null)
                return user;

            var data = await _userQuery.GetByIdAndTenantAsync(wxUser.SysUserId, tenantId);
            return _mapper.Map<SysWechatLoginUserDto>(data);
        }

        /// <summary>
        /// 根据UnionId获取微信登录用户
        /// </summary>
        /// <param name="tenantId">机构id</param>
        /// <param name="unionId">微信unionId</param>
        /// <returns>用户列表</returns>
        public async Task<SysWechatLoginUserDto> GetByUnionIdAsync(Guid tenantId, string unionId)
        {
            var user = new SysWechatLoginUserDto();
            var wxUser = await _wxUserQuery.GetByUnionIdAsync(unionId);
            if (wxUser == null)
                return user;

            // 由于UnionId具有安全性，因此可以不需要像mobile一样必传tenantId
            var data = tenantId != Guid.Empty
                ? await _userQuery.GetByIdAndTenantAsync(wxUser.SysUserId, tenantId)
                : await _userQuery.GetByIdAsync(wxUser.SysUserId);
            return _mapper.Map<SysWechatLoginUserDto>(data);
        }
    }
}
