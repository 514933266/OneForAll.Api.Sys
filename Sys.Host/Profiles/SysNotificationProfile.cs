﻿using AutoMapper;
using Sys.Application.Dtos;
using Sys.Domain;
using Sys.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Text;
using Sys.Domain.Models;

namespace Sys.Host.Profiles
{
    public class SysNotificationProfile : Profile
    {
        public SysNotificationProfile()
        {
            CreateMap<SysNotification, SysNotificationDto>();
            CreateMap<SysNotificationForm, SysNotification>();
        }
    }
}
