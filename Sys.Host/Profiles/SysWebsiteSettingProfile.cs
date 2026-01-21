using AutoMapper;
using Sys.Application.Dtos;
using Sys.Domain.Entities;
using Sys.Domain.Aggregates;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Sys.Host.Profiles
{
    public class SysWebsiteSettingProfile : Profile
    {
        public SysWebsiteSettingProfile()
        {
            CreateMap<SysWebsiteSetting, SysWebsiteSettingDto>()
                .ForMember(t => t.TenantId, a => a.MapFrom(s => s.SysTenantId));
            CreateMap<SysWebsiteSetting, SysWebsiteSettingAggr>();
            CreateMap<SysWebsiteSettingAggr, SysWebsiteSettingDto>()
                .ForMember(t => t.TenantId, a => a.MapFrom(s => s.SysTenantId))
                .ForMember(t => t.Apis, a => a.MapFrom(s => s.SysWebsiteApiSettings));

            CreateMap<SysWebsiteSettingForm, SysWebsiteSetting>()
                .ForMember(t => t.SysTenantId, a => a.MapFrom(s => s.TenantId));
        }
    }
}
