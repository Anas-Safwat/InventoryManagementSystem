using AutoMapper;
using InventoryManagementSystem.DTOs.CategoryDTOs;
using InventoryManagementSystem.DTOs.ProductDTOs;
using InventoryManagementSystem.DTOs.SupplierDTOs;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryGetDto>().ReverseMap();
            CreateMap<CategoryAddDto, Category>();

            CreateMap<Supplier, SupplierGetDto>();
            CreateMap<SupplierAddDto, Supplier>();

            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductAddDto, Product>();

            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
