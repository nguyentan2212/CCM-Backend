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
            CreateMap<DappUser, TopUserViewModel>();
            CreateMap<Capital, CapitalDataViewModel>()
                .ForMember(des => des.Creator, opt => opt.MapFrom(src => src.Creator.Id.ToString()))
                .ForMember(des => des.Asset, opt => opt.MapFrom(src => src.Asset.ToString()))
                .ForMember(des => des.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(des => des.CreatorName, opt => opt.MapFrom(src => src.Creator.FullName)); ;
        }
    }
}
