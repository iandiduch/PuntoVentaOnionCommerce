# Sistema de Punto de Venta y Caja para Negocios

Este proyecto es un sistema de punto de venta y caja diseñado para pequeñas y medianas empresas. El sistema permite gestionar clientes, proveedores, realizar compras y ventas, generar cotizaciones, informes estadísticos, gestionar inventario, y más.

## Características Principales

- **Gestión de Clientes**: Agregar, modificar y eliminar información de clientes.
- **Gestión de Proveedores**: Manejo completo de proveedores, sus productos y precios.
- **Compras y Ventas**: Registro de compras y ventas, con control de inventario en tiempo real.
- **Cotizaciones**: Creación y gestión de cotizaciones para los clientes.
- **Informes**: Generación de informes estadísticos de ventas y compras.
- **Inventario**: Control detallado del inventario de productos y su disponibilidad.

## Estado del Proyecto

El sistema está en **desarrollo activo**. Algunas funcionalidades están aún en progreso, como el formulario de ventas (`Frm_Ventas.cs`), que no está completamente implementado.

## ¡IMPORTANTE! Exclusiones del Repositorio

Para mantener el enfoque en la lógica backend del sistema, se han excluido los siguientes archivos y carpetas:

- Archivos: 
  - `*.Designer.cs` 
  - `*.resx`
  - `*.csproj`
  - `*.config`
  - `*.rpt`
- Directorios: 
  - `bin`, `obj`, `Properties`, `.vs`, `Resources`, entre otros.
  
Por lo cual solo se proporciona el codigo .cs, se excluyo todo lo que tenga que ver con el proyecto en visual studio y visuales.

## Tecnologías Utilizadas

- **Lenguaje de Programación**: C#
- **Framework**: .NET Framework (WinForms)
- **Base de Datos**: MySQL
- **Patrón de Diseño**: Capas (Capa de Datos, Capa de Negocio, Capa de Entidad, Capa de Vista)

## Estructura del Proyecto

El sistema está dividido en varias capas para una mejor organización:
Capa_Datos
├── BD_Caja.cs
├── BD_Categoria.cs
├── BD_Cliente.cs
├── BD_Conexion.cs
├── ...
├── Util
│   ├── Error.cs
│   ├── ExpandableText.cs
│   ├── ...
Capa_Entidad
├── ApiResponseDolar.cs
├── EN_Caja.cs
├── EN_Cliente.cs
├── ...
Capa_Negocio
├── RN_Caja.cs
├── RN_Categoria.cs
├── RN_Cliente.cs
├── ...
Capa_Vista
├── Frm_Login.cs
├── Frm_Principal.cs
├── Cliente
│   ├── ...
├── Ventas
│   ├── ...
├── Productos
│   ├── ...
├── ...
Scripts_Base_Datos
├── ABC_Caja.sql
├── ABC_Cliente.sql
├── ABC_Cotizacion.sql
├── ...


