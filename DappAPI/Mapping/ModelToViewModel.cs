using AutoMapper;
using DappAPI.Extensions.Enums;
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
                .ForMember(des => des.Asset, opt => opt.MapFrom(src => src.Asset.ToDescriptionString()))
                .ForMember(des => des.Type, opt => opt.MapFrom(src => src.Type.ToDescriptionString()))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => src.Status.ToDescriptionString()));
        }
    }
}
