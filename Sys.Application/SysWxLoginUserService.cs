using AutoMapper;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
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
    public class SysWxLoginUserService : ISysWxLoginUserService
    {
        private readonly IMapper _mapper;
        private readonly ISysWechatUserRepository _wxUserRepository;
        private readonly ISysUserRepository _userRepository;
        public SysWxLoginUserService(IMapper mapper, ISysUserRepository userRepository, ISysWechatUserRepository wxUserRepository)
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
        public async Task<SysWxLoginUserDto> GetByMobileAsync(Guid tenantId, string mobile)
        {
            var user = new SysWxLoginUserDto();
            var wxUser = await _wxUserRepository.GetAsync(w => w.Mobile == mobile);
            if (wxUser == null)
                return user;

            var data = await _userRepository.GetAsync(w => w.Id == wxUser.SysUserId && w.SysTenantId == tenantId);
            return _mapper.Map<SysWxLoginUserDto>(data);
        }
    }
}
