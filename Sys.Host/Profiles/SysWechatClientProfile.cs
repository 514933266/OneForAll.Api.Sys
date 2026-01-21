using AutoMapper;
using Sys.Application.Dtos;
using Sys.Domain.Entities;
using Sys.Domain.Aggregates;
using Sys.Domain.Models;

namespace Sys.Host.Profiles
{
    public class SysWechatClientProfile : Profile
    {
        public SysWechatClientProfile()
        {
            CreateMap<SysWechatClient, SysWechatClientDto>();
            CreateMap<SysWechatClientAggr, SysWechatClientDto>()
                .ForMember(t => t.ClientId, a => a.MapFrom(s => s.SysClient.Id))
                .ForMember(t => t.ClientName, a => a.MapFrom(s => s.SysClient.ClientName));
            CreateMap<SysWechatClientForm, SysWechatClient>();
        }
    }
}
