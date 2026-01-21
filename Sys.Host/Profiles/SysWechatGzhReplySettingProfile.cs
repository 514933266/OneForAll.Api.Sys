using AutoMapper;
using OneForAll.Core.Extension;
using Sys.Application.Dtos;
using Sys.Domain.Entities;
using Sys.Domain.Aggregates;
using Sys.Domain.Models;
using Sys.Domain.ValueObjects;
using System.Collections;
using System.Collections.Generic;

namespace Sys.Host.Profiles
{
    public class SysWechatGzhReplySettingProfile : Profile
    {
        public SysWechatGzhReplySettingProfile()
        {
            CreateMap<SysWechatGzhReplySetting, SysWechatGzhReplySettingDto>();
            CreateMap<SysWechatGzhReplySetting, SysWechatGzhReplySettingAggr>();
            CreateMap<SysWechatGzhReplySettingAggr, SysWechatGzhReplySettingDto>();

            CreateMap<SysWechatGzhReplySettingForm, SysWechatGzhReplySetting>()
                .ForMember(t => t.ContentJson, a => a.MapFrom(s => s.Contents.ToJson()));
        }
    }
}

