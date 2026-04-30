# TrabajoPrácticoPS2026

## Integrantes
- Valeria (valeria-rama)
- Kevin López (kevin-lopez)

## Organización GitHub
https://github.com/Trabajo-Practico-2026

## Repositorios
- Backend: https://github.com/Trabajo-Practico-2026/TrabajoPracticoPS2026
- Frontend: https://github.com/Trabajo-Practico-2026/Front-End

## Requisitos previos
- Visual Studio 2022 o superior
- .NET 8 SDK
- SQL Server Express
- SQL Server Management Studio (SSMS)

## Pasos para ejecutar el Backend
1. Clonar el repositorio
2. Abrir `TrabajoPracticoPS.sln` en Visual Studio
3. En `appsettings.json` configurar la connection string con tu instancia de SQL Server:
```json
"ConnectionString": "Server=TU_MAQUINA\\SQLEXPRESS;Database=TicketingDB;Trusted_Connection=True;TrustServerCertificate=True;"
```
4. Ejecutar con F5 — la app crea la base de datos y carga los datos automáticamente
5. Swagger disponible en: `https://localhost:7243/swagger`

## Pasos para ejecutar el Frontend
1. Clonar el repositorio Front-End
2. Abrir la carpeta en VS Code
3. Click derecho en `index.html` → Open with Live Server
4. Asegurarse que el backend esté corriendo antes de abrir el frontend

## Tecnologías utilizadas

### Backend
- C# / .NET 8
- ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server Express
- MediatR (patrón CQRS)
- Swagger / OpenAPI

### Frontend
- HTML / CSS / JavaScript (Vanilla)
- Tailwind CSS

## Arquitectura
El proyecto usa Clean Architecture dividida en 4 capas:
- **Domain**: entidades del negocio
- **Application**: casos de uso, queries, commands y handlers
- **Infrastructure**: acceso a datos, repositorios y migraciones
- **Api**: controladores y endpoints REST
