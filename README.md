# Products API – .NET 8 (Clean Architecture)

Este proyecto es una **API REST** desarrollada en **.NET 8** que implementa el patrón de **Clean Architecture** (Arquitectura Limpia) para crear, consultar, actualizar y eliminar productos.

---

##  Tecnologías y Estructura

###  Tecnologías Utilizadas

* **.NET 8 Web API**
* **Entity Framework Core** (Uso principal para escritura y actualizaciones)
* **SQL Server**
* **Dapper** (para consultas directas y optimizadas)
* **Swagger/OpenAPI**
* **Clean Architecture**

###  Arquitectura por Capas

El proyecto está organizado en las siguientes capas siguiendo la arquitectura limpia:

* **`Products.Api`**: Capa de **Presentación** (Controladores, configuración de la API).
* **`Application`**: Contiene la **Lógica de Negocio** (Servicios, DTOs, interfaces de servicios).
* **`Domain`**: Contiene las **Entidades** centrales del negocio.
* **`Infrastructure`**: Contiene la **Implementación** del acceso a datos (DbContext, SQL Server, implementaciones concretas de servicios).


---

##  Base de Datos

El proyecto utiliza una tabla central para la gestión de productos:

### `Products`

Asegúrate de que la tabla exista en tu base de datos. Puedes usar el siguiente script SQL para crearla:


NOTA: El proyecto puede coexistir con un stored procedure existente (Catalogo.sp_GetProductos), pero la API opera directamente sobre la tabla Products.
```sql
CREATE TABLE Products (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18,2),
    ImageBase64 NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL
);
```

### Configuración


```sql
JSON

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVER;Database=TU_DB;User Id=TU_USUARIO;Password=TU_PASSWORD;Encrypt=False;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"

  ```

## Cómo Ejecutar el Proyecto

Abrir la solución en Visual Studio.

Configurar la cadena de conexión en el archivo appsettings.json.

Establecer el proyecto Products.Api como proyecto de inicio.

Ejecutar con el comando:

```sql
dotnet run
```
Acceder a la documentación de Swagger para probar los endpoints (el puerto puede variar):


https://localhost:7021/swagger
