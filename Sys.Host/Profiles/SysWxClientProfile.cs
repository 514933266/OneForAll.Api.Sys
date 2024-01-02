using AutoMapper;
using Sys.Application.Dtos;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Models;

namespace Sys.Host.Profiles
{
    public class SysWxClientProfile : Profile
    {
        public SysWxClientProfile()
        {
            CreateMap<SysWxClient, SysWxClientDto>();
            CreateMap<SysWxClientAggr, SysWxClientDto>()
                .ForMember(t => t.ClientId, a => a.MapFrom(s => s.SysClient.Id))
                .ForMember(t => t.ClientName, a => a.MapFrom(s => s.SysClient.ClientName));
            CreateMap<SysWxClientForm, SysWxClient>();
        }
    }
}
