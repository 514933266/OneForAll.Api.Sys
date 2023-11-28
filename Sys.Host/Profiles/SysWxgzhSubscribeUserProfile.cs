using AutoMapper;
using Sys.Application.Dtos;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Models;

namespace Sys.Host.Profiles
{
    public class SysWxgzhSubscribeUserProfile : Profile
    {
        public SysWxgzhSubscribeUserProfile()
        {
            CreateMap<SysWxgzhSubscribeUser, SysWxgzhNotifyUserDto>()
                .ForMember(t => t.UserId, a => a.MapFrom(s => s.SysUserId));
        }
    }
}
