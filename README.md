# PruebaNetBy
#Es necesario tener instalado el sdk de .NET 8
#Si no lo posee puede descargarlo de la pagina oficial
#https://dotnet.microsoft.com/en-us/download/dotnet/8.0
#
#Considerar que .Net 8 solo es compatible en Visual Studio 2022
#
#Para la base de datos se utilizó Sql Server
#
#Para la conexion a la base de datos revisar el archivo appsettings.json de Solucion/Backend/NetBy.Api y colocar sus respectivas credenciales
#"DefaultConnection": "Server=localhost;Database=NetBy;User Id=USUARIO;Password=PASSWORD;TrustServerCertificate=True"
#
#Al ejecutar considerar levantar estos dos proyectos al mismo tiempo
#Solucion/Backend/NetBy.Api y Solucion/FrontEnd/NetBy.WebApp
#
#Considerar que al levantar el WebApp los metodos apuntan al api con BaseAdress https://localhost:7051/
#_proxy.BaseAdress = "https://localhost:7051/" 
#Si el api se levanta en otro puerto, reemplazar con ese puerto
#
