using AutoMapper;
using DEPI_ECommerce_API.DTOs;
using DEPI_ECommerce_API.Entities;

namespace DEPI_ECommerce_API.Mapping;

public class ECommerceMappingProfile : Profile
{
    public ECommerceMappingProfile()
    {
        CreateMap<Category, CategoryReadDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();

        CreateMap<Customer, CustomerReadDto>();
        CreateMap<CustomerCreateDto, Customer>();
        CreateMap<CustomerUpdateDto, Customer>();

        CreateMap<Order, OrderReadDto>()
            .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer == null ? null : s.Customer.Name));
        CreateMap<OrderCreateDto, Order>();
        CreateMap<OrderUpdateDto, Order>();

        CreateMap<OrderDetail, OrderDetailReadDto>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product == null ? null : s.Product.Name));
        CreateMap<OrderDetailCreateDto, OrderDetail>();
        CreateMap<OrderDetailUpdateDto, OrderDetail>();
    }
}
