using AutoMapper;
using BussinessLogicLayer.DTO;
using DataAccessLayer.Entities;


namespace BussinessLogicLayer.Mappers
{
    internal class ProductToProductResponseMappingProfile:Profile
    {
        public ProductToProductResponseMappingProfile()
        {
            CreateMap<ProductAddRequest, Product>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.category))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.QauntityInStock, opt => opt.MapFrom(src => src.QuantiryInStock))
                .ForMember(dest => dest.ProductID, opt => opt.Ignore());
        }
    }
}
