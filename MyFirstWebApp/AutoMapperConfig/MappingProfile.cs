using AutoMapper;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.AutoMapperConfig
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cashier, CashierViewModel>();
            CreateMap<CashierViewModel, Cashier>();
          
            CreateMap<ShopModel,ShopIncome>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.ShopName))
                .ForMember(d => d.Income, o => o.MapFrom(s => s.ShopIncome));
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
