using AutoMapper;
using Sys.Application.Dtos;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Host.Profiles
{
    public class SysUserProfile : Profile
    {
        public SysUserProfile()
        {
            CreateMap<SysUserAggr, SysUserDto>()
                .ForMember(t => t.TenantId, a => a.MapFrom(s => s.SysTenant.Id))
                .ForMember(t => t.TenantName, a => a.MapFrom(s => s.SysTenant.Name));

            CreateMap<SysUser, SysUserDto>()
                .ForMember(t => t.TenantId, a => a.MapFrom(s => s.SysTenantId));
            CreateMap<SysUserForm, SysUser>()
                .ForMember(t => t.SysTenantId, a => a.MapFrom(s => s.TenantId));
        }
    }
}
