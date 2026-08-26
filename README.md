\# DistribuidoraSystem



Sistema web de gestión comercial para una distribuidora de productos de limpieza e insumos para el hogar.



\## Backend



El backend está desarrollado con:



\- .NET 10

\- ASP.NET Core

\- Entity Framework Core

\- SQL Server

\- Swagger / OpenAPI



\## Arquitectura



La solución está dividida en:



\- `Distribuidora.Domain`

\- `Distribuidora.Application`

\- `Distribuidora.Infrastructure`

\- `Distribuidora.API`



\### Dependencias entre capas



```text

Domain

&#x20; ↑

Application

&#x20; ↑

Infrastructure

&#x20; ↑

API

