using AutoMapper;
using Sys.Application.Dtos;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Models;
using System;

namespace Sys.Host.Profiles
{
    public class SysWxgzhSubscribeUserProfile : Profile
    {
        public SysWxgzhSubscribeUserProfile()
        {
            CreateMap<SysWxgzhSubscribeUser, SysWxgzhSubscribeUserTokenDto>()
                .ForMember(t => t.UserId, a => a.MapFrom(s => s.SysUserId));

            CreateMap<SysWxgzhSubscribeUserAggr, SysWxgzhSubscribeUserDto>()
                .ForMember(t => t.UserId, a => a.MapFrom(s => s.SysUser == null ? Guid.Empty : s.SysUser.Id))
                .ForMember(t => t.UserName, a => a.MapFrom(s => s.SysUser == null ? "" : s.SysUser.UserName))
                .ForMember(t => t.UserNickName, a => a.MapFrom(s => s.SysUser == null ? "" : s.SysUser.Name));
        }
    }
}
