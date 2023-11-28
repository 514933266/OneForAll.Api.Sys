using AutoMapper;
using OneForAll.Core.Extension;
using Sys.Application.Dtos;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Models;
using Sys.Domain.ValueObjects;
using System.Collections;
using System.Collections.Generic;

namespace Sys.Host.Profiles
{
    public class SysWxgzhReplySettingProfile : Profile
    {
        public SysWxgzhReplySettingProfile()
        {
            CreateMap<SysWxgzhReplySetting, SysWxgzhReplySettingDto>();
            CreateMap<SysWxgzhReplySettingAggr, SysWxgzhReplySettingDto>()
                .ForMember(t => t.Contents, a => a.MapFrom(s => s.ContentJson.FromJson<List<SysWxgzhReplySettingContentVo>>()));

            CreateMap<SysWxgzhReplySettingForm, SysWxgzhReplySetting>()
                .ForMember(t => t.ContentJson, a => a.MapFrom(s => s.Contents.ToJson()));
        }
    }
}

