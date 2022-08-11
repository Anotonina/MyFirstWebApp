using AutoMapper;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.AutoMapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cashier, CashierViewModel>();
          
            CreateMap<ShopModel,ShopIncome>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.ShopName))
                .ForMember(d => d.Income, o => o.MapFrom(s => s.ShopIncome));
        }

    }
}
