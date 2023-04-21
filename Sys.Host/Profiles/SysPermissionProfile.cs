using AutoMapper;
using Sys.Application.Dtos;
using Sys.Domain;
using Sys.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Text;
using Sys.Domain.Models;
using Sys.Domain.Aggregates;

namespace Sys.Host.Profiles
{
    public class SysPermissionProfile : Profile
    {
        public SysPermissionProfile()
        {
            CreateMap<SysPermissionAggr, SysPermissionDto>()
                .ForMember(t => t.MenuId, a => a.MapFrom(e => e.SysMenu.Id))
                .ForMember(t => t.MenuName, a => a.MapFrom(e => e.SysMenu.Name));

            CreateMap<SysPermission, SysMenuPermissionDto>();
            CreateMap<SysMenuPermissionForm, SysPermission>();
            CreateMap<SysMenuPermissionForm, SysPermissionForm>();

            CreateMap<SysPermission, SysPermissionDto>();
            CreateMap<SysPermissionForm, SysPermission>()
                .ForMember(t => t.SysMenuId, a => a.MapFrom(e => e.MenuId));
        }
    }
}
