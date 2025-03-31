using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Common
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Productos
        {
            public const string GetAll = Base + "/Productos/GetAll";          
            public const string Create = Base + "/Productos/Create";
            public const string Update = Base + "/Productos/Update";
            public const string Deactivate = Base + "/Productos/Deactivate";
            public const string Activate = Base + "/Productos/Activate";
            public const string GetInformeStock = Base + "/Productos/GetInformeStock";
            public const string GetInformeKardex = Base + "/Productos/GetInformeKardex";           
        }

        public static class Categorias
        {
            public const string GetAll = Base + "/Categorias/GetAll";
            public const string Create = Base + "/Categorias/Create";
            public const string Update = Base + "/Categorias/Update";
            public const string Deactivate = Base + "/Categorias/Deactivate";
            public const string Activate = Base + "/Categorias/Activate";
        }

        public static class Ventas
        {
            public const string GetAll = Base + "/Ventas/GetAll";
            public const string Create = Base + "/Ventas/Create";
            public const string Update = Base + "/Ventas/Update";
            public const string Deactivate = Base + "/Ventas/Deactivate";
        }

        public static class Compras
        {
            public const string GetAll = Base + "/Compras/GetAll";
            public const string Create = Base + "/Compras/Create";
            public const string Update = Base + "/Compras/Update";
            public const string Deactivate = Base + "/Compras/Deactivate";
        }
    }
}
