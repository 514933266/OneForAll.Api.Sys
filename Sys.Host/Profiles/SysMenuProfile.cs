using AutoMapper;
using Sys.Application.Dtos;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys.Host.Profiles
{
    public class SysMenuProfile : Profile
    {
        public SysMenuProfile()
        {
            CreateMap<SysMenu, SysMenuTreeDto>();
            CreateMap<SysMenu, SysMenuPermissionAggr>();
            CreateMap<SysMenuPermissionAggr, SysMenuTreeDto>()
                .ForMember(t => t.Permissions, a => a.MapFrom(e => e.SysPermissions));

            CreateMap<SysMenuForm, SysMenu>();

            CreateMap<SysMenu, SysMenuTreeAggr>();
        }
    }
}
