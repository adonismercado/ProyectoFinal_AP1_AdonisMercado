# 🚗 AutoFranklin - Sistema de Gestión de Pedidos

> Sistema de gestión de pedidos de vehículos para AutoFranklin, permitiendo a los empleados realizar pedidos a distribuidores de manera eficiente y organizada.

## 📖 Descripción

AutoFranklin es una aplicación diseñada para facilitar la gestión de pedidos de vehículos entre la empresa y sus distribuidores. El sistema permite administrar distribuidores, crear pedidos con múltiples vehículos, gestionar documentación y mantener un control completo del inventario.

## 🔐 Credenciales de Acceso

### Usuario Estándar
- **Email:** `user@user.com`
- **Contraseña:** `User123!`

### Administrador
- **Email:** `admin@admin.com`
- **Contraseña:** `Admin123!`

## ✨ Características Principales

- 📊 Dashboard con estadísticas en tiempo real
- 🏢 Gestión completa de distribuidores
- 📦 Sistema de pedidos multi-vehículo
- 📄 Manejo de documentos legales (Commercial Invoice, Bill of Lading)
- 🚙 Catálogo de vehículos con imágenes
- 🔍 Filtros avanzados de búsqueda
- ♻️ Sistema de habilitación/deshabilitación de entidades

## 👥 Roles de Usuario

### 🔵 ROL USUARIO

Acceso a las siguientes funcionalidades:

#### **Dashboard Principal**
- Estadísticas de distribuidores agregados
- Total de pedidos realizados
- Pedidos enviados
- Accesos rápidos a registros
- Tabla con los últimos 5 pedidos

#### **📍 Distribuidores**

**Gestión:**
- Agregar nuevos distribuidores
- Editar información existente
- Deshabilitar distribuidores
- Ver pedidos por distribuidor

**Campos:**
- `DistribuidorID` (automático)
- `Nombre` (letras y espacios)
- `Email` (formato válido con @)
- `Teléfono` (solo números)
- `Fax` (solo números)

**Filtros disponibles:**
- ID
- Nombre
- Correo Electrónico
- Teléfono
- Fax

#### **📦 Pedidos**

**Proceso de creación:**
1. Seleccionar distribuidor
2. Asignar fecha y nombre al pedido
3. Agregar vehículos (múltiples permitidos)
4. Especificar cantidad por vehículo
5. Guardar pedido

> Nota: Al agregar un vehiculo al pedido, se le dira al usuario la cantidad de stock que existe de ese vehiculo.

**Campos:**
- `Distribuidor` (obligatorio, debe existir)
- `PedidoID` (automático)
- `Fecha` (formato DD/MM/AAAA)
- `Nombre` (letras y espacios)
- `Estado` (inicial: "No enviado". "Enviado": cuando se le envia al distribuidor. "Completado": cuando esta enviado y tiene sus 2 documentos)

**Acciones disponibles:**
- ✅ Enviar pedido
- 👁️ Visualizar detalles completos
- ✏️ Editar pedido
- 🚫 Deshabilitar pedido

**Filtros disponibles:**
- Fecha
- ID
- Nombre
- Distribuidor

#### **📄 Documentos**

Gestión de documentos legales para pedidos enviados.

**Tipos de documentos:**
- **Commercial Invoice:** Contrato de venta/transacción
- **Bill of Lading (B/L):** Recibo legal de mercancías

**Acciones:**
- 📤 Subir documento
- 👁️ Ver documento
- 🗑️ Eliminar documento

> ⚠️ **Nota:** Solo se pueden agregar documentos a pedidos en estado "Enviado" o "Completado"

---

### 🔴 ROL ADMIN

Acceso a funcionalidades avanzadas de administración:

#### **Dashboard Principal**
- Total de vehículos agregados
- Vehículos activos
- Vehículos deshabilitados
- Accesos rápidos a registros
- Tabla con los últimos 5 vehículos agregados

#### **♻️ Deshabilitados**

Control de entidades deshabilitadas por usuarios.

**Funcionalidades:**
- Ver distribuidores deshabilitados
- Ver pedidos deshabilitados
- Habilitar entidades nuevamente
- Eliminar permanentemente del sistema

#### **🚙 Vehículos**

**Gestión completa del catálogo:**

**Campos del vehículo:**
- `VehiculoID` (automático)
- `Stock` (incrementa o decrementa automaticamente)
- `Marca` (texto)
- `Modelo` (texto y números)
- `Color` (texto)
- `Número de Chasis` (alfanumérico)
- `Año de Fabricación` (numérico)
- `Motor` (alfanumérico)
- `Transmisión` (alfanumérico)
- `Tracción` (alfanumérico)
- `Número de Puertas` (numérico)
- `Kilometraje` (numérico)
- `Estado` (texto)
- `Tipo de Combustible` (texto)
- `Precio` (decimal)
- `Imagen` (formatos: .jpg, .png, .pdf, .webp, .jpeg)

> Nota: al agregar el vehiculo, se coloca el stock automaticamente en 0. Para agregar stock al vehiculo se debe realizar un pedido.

**Acciones disponibles:**
- ➕ Agregar nuevo vehículo
- ✏️ Editar vehículo existente
- 🚫 Deshabilitar vehículo
- ✅ Habilitar vehículo
- 🗑️ Eliminar permanentemente

**Filtros avanzados:**
- ID, Marca, Modelo, Color
- Número de Chasis, Año
- Motor, Transmisión, Tracción
- Puertas, Kilometraje
- Combustible, Precio
- Mostrar/Ocultar deshabilitados

## 📝 Validaciones del Sistema

### Distribuidores
- ✅ Email con formato válido (@)
- ✅ Teléfono y Fax solo numéricos
- ✅ Nombre con letras y espacios

### Pedidos
- ✅ Distribuidor debe existir en el sistema
- ✅ Fecha en formato correcto
- ✅ Al menos un vehículo agregado
- ✅ Cantidad especificada por vehículo

### Vehículos
- ✅ Año de fabricación numérico
- ✅ Precio en formato decimal
- ✅ Imagen en formatos permitidos
- ✅ Campos alfanuméricos según especificación
