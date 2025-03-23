using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                    destino.RolDescripcion,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                )
                .ForMember(destino => 
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                    destino.RolDescripcion,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                    destino.IdRolNavigation,
                    opt => opt.Ignore()
                ).ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion

            #region Categoria
            //CreateMap<Categoria, CategoriaDTO>().ReverseMap();

            CreateMap<Categoria, CategoriaDTO>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<CategoriaDTO, Categoria>()
                .ForMember(destino =>
                   destino.EsActivo,
                   opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
               );
            #endregion

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destio =>
                    destio.DrecripcionCategoria,
                    opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                )
                .ForMember(destio =>
                    destio.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-NI")))
                )
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 0 : 1)
                );

            CreateMap<ProductoDTO, Producto>()
               .ForMember(destio =>
                   destio.IdCategoriaNavigation,
                   opt => opt.Ignore()
               )
               .ForMember(destio =>
                   destio.Precio,
                   opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-NI")))
               )
               .ForMember(destino =>
                   destino.EsActivo,
                   opt => opt.MapFrom(origen => origen.EsActivo == 1)
               );
            #endregion

            #region Venta
            CreateMap<Venta, VentaDTO>()
                .ForMember(destio =>
                    destio.TotalTexto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-NI")))
                )
                .ForMember(destio =>
                    destio.FechaRegistro,
                    opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<VentaDTO, Venta>()
                .ForMember(destio =>
                    destio.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-NI")))
                );

            #endregion venta

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino =>
                    destino.DescripcionProducto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )
                .ForMember(destio =>
                    destio.PrecioTexto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-NI")))
                )
                .ForMember(destio =>
                    destio.TotalTexto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-NI")))
                );

            CreateMap<DetalleVentaDTO, DetalleVenta>()
                .ForMember(destio =>
                   destio.Precio,
                   opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-NI")))
                )
                .ForMember(destio =>
                   destio.Total,
                   opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-NI")))
                );
            #endregion

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(destio =>
                    destio.FechaRegistro,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destio =>
                    destio.NumeroDocumento,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
                )
                .ForMember(destio =>
                    destio.TipoPago,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
                )
                .ForMember(destio =>
                    destio.TotalVenta,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-NI")))
                )
                .ForMember(destio =>
                    destio.Producto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )
                .ForMember(destio =>
                    destio.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-NI")))
                )
                .ForMember(destio =>
                    destio.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-NI")))
                );
            #endregion
        }
    }
}
