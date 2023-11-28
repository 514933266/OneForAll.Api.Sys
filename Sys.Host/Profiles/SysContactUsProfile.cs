using AutoMapper;
using Sys.Application.Dtos;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Models;

namespace Sys.Host.Profiles
{
    public class SysContactUsProfile : Profile
    {
        public SysContactUsProfile()
        {
            CreateMap<SysContactUs, SysContactUsDto>();
            CreateMap<SysContactUsForm, SysContactUs>();
        }
    }
}
