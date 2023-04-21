using AutoMapper;
using Sys.Application.Dtos;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Host.Profiles
{
    public class SysWebsiteApiSettingProfile : Profile
    {
        public SysWebsiteApiSettingProfile()
        {
            CreateMap<SysWebsiteApiSetting, SysWebsiteApiSettingDto>();

            CreateMap<SysWebsiteApiSettingForm, SysWebsiteApiSetting>();
        }
    }
}
