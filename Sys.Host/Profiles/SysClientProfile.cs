using AutoMapper;
using Sys.Application.Dtos;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Models;

namespace Sys.Host.Profiles
{
    public class SysClientProfile : Profile
    {
        public SysClientProfile()
        {
            CreateMap<SysClient, SysClientDto>();
            CreateMap<SysClientForm, SysClient>();
        }
    }
}
