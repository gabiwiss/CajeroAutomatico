# CajeroAutomaticoAPI
## Desarrollo de sistema tipo ATM. Herramientas utilizadas:
 1. BackEnd: NET 6
 2. FrontEnd: MVC .NET 6
 3. Base de datos: SQL SERVER v16.0.1105.1 

## Implementación
  1. Definir connectionString en API/appsettings.json
  2. Configurar proyectos de inicio simultaneos (API y WEB)
  3. Verificar URL de API en Web/appsettings.json


## Resumen de sistema
  Se requirió un sistema de cajero automático tipo ATM. El mismo fue desarrollado en 3(tres) proyectos:

  1. Vista Web con formato Modelo Vista Controlador.
  2. Librería de clases (DATA) que contiene entidades para implementar ORM (EntityFramework).
  3. API RESTful que consume base de datos (SQL SERVER).
## Descripción de sistema

### Base de datos

#### La base de datos (relacional) consta de tres tablas:
1. Tabla Usuarios: contiene la información de las cuentas.
2. Tabla Operación: contiene información sobre las operaciones realizadas por los usuarios.
3. Tabla TipoOperación: tabla que contiene la definición de los tipos de operación.
   
#### API

La API esta dividida en dos partes: Los controladores y los services (encargados de abstraer la lógica). En los endpoints se reciben DTOs, se pasan a Entities y luego se mandan a DATA con el uso de managers (encargados de ejecutar las consultas con la base de datos)

#### DATA
En DATA se define el contexto de la base de datos utilizando Entity Framework. También se definen los Entities, DTOs , managers.
La carpeta BaseApi contiene todas las funciones a implementar que seran consumidas por la vista Web y apuntaran a los endpoints de la API.

#### Web

En la vista Web se encuentran vistas , controladores y modelos a utilizar según el tipo de objeto que se obtenga a partir del consumo de la API.

