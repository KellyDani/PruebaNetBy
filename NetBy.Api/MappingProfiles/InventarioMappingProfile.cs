using AutoMapper;
using NetBy.Common.Request;
using NetBy.Common.Response;
using NetBy.Core.Modelos;
using NetBy.Core.Modelos.Filters;

namespace NetBy.Api.MappingProfiles
{
    public class InventarioMappingProfile : Profile
    {
        public InventarioMappingProfile()
        {
            CreateMap<Productos, ProductosResponse>();
            CreateMap<Categorias, CategoriasResponse>();

            CreateMap<Ventas, VentasResponse>();
            CreateMap<VentasDt, VentasDtResponse>();

            CreateMap<Compras, ComprasResponse>();
            CreateMap<ComprasDt, ComprasDtResponse>();

            CreateMap<Stock, StockResponse>()
                .ForMember(dest => dest.ProductoNombre, opt => opt.MapFrom(src => src.Producto.Nombre))
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.Producto.Categoria.Id))
                .ForMember(dest => dest.CategoriaNombre, opt => opt.MapFrom(src => src.Producto.Categoria.Nombre));

            CreateMap<Kardex, KardexResponse>()
                .ForMember(dest => dest.ProductoNombre, opt => opt.MapFrom(src => src.Producto.Nombre))
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.Producto.Categoria.Id))
                .ForMember(dest => dest.CategoriaNombre, opt => opt.MapFrom(src => src.Producto.Categoria.Nombre));

            CreateMap<FilterQuery, FilterBase>();
            CreateMap<PaginationQuery, PaginationFilter>();
        }
      
    }
}
