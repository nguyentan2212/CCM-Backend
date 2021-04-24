using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DappAPI.Models;
using DappAPI.ViewModels;

namespace DappAPI.Mapping
{
    public class ModelToViewModel : Profile
    {
        public ModelToViewModel()
        {
            CreateMap<DappUser, UserInfoViewModel>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
