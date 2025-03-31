
using NetBy.Business.Servicios;

namespace NetBy.Api.Installer
{
    public class BusinessServicesInstaller : IInstaller
    {
        public BusinessServicesInstaller()
        {
        }

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IProductosService, ProductosService>();
            services.AddSingleton<ICategoriasService, CategoriasService>();
            services.AddSingleton<IVentasService, VentasService>();
            services.AddSingleton<IComprasService, ComprasService>();
        }
    }
}
