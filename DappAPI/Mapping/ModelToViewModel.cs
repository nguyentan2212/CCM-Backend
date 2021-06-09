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
            CreateMap<DappUser, UserDataViewModel>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<Capital, CapitalDataViewModel>()
                .ForMember(des => des.Creator, opt => opt.MapFrom(src => src.Creator.PublicAddress))
                .ForMember(des => des.Approver, opt => opt.MapFrom(src => src.Approver.PublicAddress));
        }
    }
}
