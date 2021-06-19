using AutoMapper;
using DappAPI.Models;
using DappAPI.ViewModels;
using System;

namespace DappAPI.Mapping
{
    public class ViewModelToModel : Profile
    {
        public ViewModelToModel()
        {
            Random random = new Random();

            CreateMap<RegisterViewModel, DappUser>()
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.PublicAddress))
                .ForMember(des => des.CreationDate, opt => opt.MapFrom(src => DateTime.Today))
                .ForMember(des => des.Nonce, opt => opt.MapFrom(src => random.Next(10000, 100000)))
                .ForMember(des => des.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
            CreateMap<UpdateAccountViewModel, DappUser>();
            CreateMap<CreateCapitalViewModel, Capital>();
            CreateMap<UpdateCapitalViewModel, Capital>();
        }
    }
}
