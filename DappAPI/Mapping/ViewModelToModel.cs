using AutoMapper;
using DappAPI.Models;
using DappAPI.ViewModels;

namespace DappAPI.Mapping
{
    public class ViewModelToModel : Profile
    {
        public ViewModelToModel()
        {
            CreateMap<RegisterViewModel, DappUser>();
            CreateMap<UpdateAccountViewModel, DappUser>();
            CreateMap<CreateCapitalViewModel, Capital>();
            CreateMap<UpdateCapitalViewModel, Capital>();
        }
    }
}
