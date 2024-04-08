using AutoMapper;
using ShoppingCartAPI.Dto;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Products, ProductsDto>();
            CreateMap<ProductsDto, Products>();
            CreateMap<Cart, CartDto>();
            CreateMap<CartDto, Cart>();
            CreateMap<Customers, CustomersDto>();
            CreateMap<CustomersDto, Customers>();
            CreateMap<Login, LoginDto>();
            CreateMap<LoginDto, Login>();
            CreateMap<Orders, OrdersDto>();
            CreateMap<OrdersProducts, OrdersProductsDto>();
            CreateMap<OrdersProductsDto, OrdersProducts>();
        }
    }
}
