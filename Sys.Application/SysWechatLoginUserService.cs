using AutoMapper;
using OneForAll.Core.ORM;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.Entities;
using Sys.Domain.Interfaces;
using Sys.Domain.Repositorys;
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
        private readonly ISysWechatUserRepository _wxUserRepository;
        private readonly ISysUserRepository _userRepository;
        public SysWechatLoginUserService(IMapper mapper, ISysUserRepository userRepository, ISysWechatUserRepository wxUserRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _wxUserRepository = wxUserRepository;
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
            var wxUser = await _wxUserRepository.GetAsync(w => w.Mobile == mobile);
            if (wxUser == null)
                return user;

            var data = await _userRepository.GetAsync(w => w.Id == wxUser.SysUserId && w.SysTenantId == tenantId);
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
            var wxUser = await _wxUserRepository.GetAsync(w => w.UnionId == unionId);
            if (wxUser == null)
                return user;

            var predicate = PredicateBuilder.Create<SysUser>(w => w.Id == wxUser.SysUserId);
            if (tenantId != Guid.Empty)
            {
                // 由于UnionId具有安全性，因此可以不需要像mobile一样必传tenantId
                predicate = predicate.And(w => w.SysTenantId == tenantId);
            }
            var data = await _userRepository.GetAsync(predicate);
            return _mapper.Map<SysWechatLoginUserDto>(data);
        }
    }
}
