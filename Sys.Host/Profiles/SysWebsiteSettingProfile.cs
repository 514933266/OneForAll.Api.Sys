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
    public class SysWebsiteSettingProfile : Profile
    {
        public SysWebsiteSettingProfile()
        {
            CreateMap<SysWebsiteSetting, SysWebsiteSettingDto>();
            CreateMap<SysWebsiteSettingAggr, SysWebsiteSettingDto>()
                .ForMember(t => t.Apis, a => a.MapFrom(s => s.SysWebsiteApiSettings));

            CreateMap<SysWebsiteSettingForm, SysWebsiteSetting>();
        }
    }
}
