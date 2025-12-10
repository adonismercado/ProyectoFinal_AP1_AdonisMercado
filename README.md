Usuarios:

user@user.com - User123!
admin@admin.com - Admin123!

Guia Proyecto Final Programacion Aplicada 1 3-2025


El programa consiste en una aplicacion para la empresa de vehiculos AutoFranklin, en el que sus empleados podran hacer pedidos a otras empresas encargadas de distribuir los vehiculos en el pais.

Al iniciar el programa, tendremos el menu de Login donde se podra iniciar sesion en el programa introduciendo el correo electronico y la contraseña. Al dar click a "Iniciar Sesion" se redirige automaticamente a la
pantalla principal dependiendo de que rol tenga el usuario con el que se haya iniciado sesion.

- ROL USUARIO
En el rol Usuario tendremos los siguientes registros: Distribuidores, Pedidos, Documentos.
Al iniciar sesion con un usuario, tendremos una pagina principal donde se podran observar algunas estadisticas como la cantidad de distribuidores agregados, la cantidad de pedidos y la cantidad de pedidos enviados. Ademas, unos botones de acceso
rapido a los 3 registros, y una tabla donde se podran observar los ultimos 5 pedidos que han sido registrados al sistema.

Registros:

Distribuidores:
Esta es la entidad que se encarga de distribuir los vehiculos. Los pedidos se realizan a un distribuidor.
Consulta:
En la pagina principal de distribuidores, aparece la opcion de poder filtrar los distribuidores agregados por ID, Nombre, Correo Electronico, Telefono y Fax. Ademas, un input buscar para digitar la palabra clave a filtrar.
Tambien esta el boton Agregar Distribuidor, que por como su nombre indica el usuario tendra la posibilidad de poder agregar un distribuidor al sistema. Se crea con los siguientes campos:

- DistribuidorID (valor entero, colocado automaticamente por el sistema).
- Nombre (valor string, se permiten letras y espacios).
- Email (valor string, @ obligatoria).
- Telefono (valor string, solo se permiten numeros).
- Fax (valor string, solo se permiten numeros).

Luego de llenado los campos se procede a guardar el distribuidor.

Tambien en la pantalla principal aparece la tabla donde estan los distribuidores que tenemos agregados, con sus respectivos campos. Al final, aparecen las siguientes opciones:

- Editar Distribuidor: donde se podran editar los campos de un distribuidor ya agregado.
- Deshabilitar Distribuidor : donde se podra deshabilitar al distribuidor y ocultarlo del sistema.
- Ver Pedidos: donde se podran visualizar los pedidos que han sido ordenados al distribuidor.

Pedidos:
Esta es la entidad que se encarga de tener todos los datos necesarios para crear un pedido. Los empleados podran hacer pedidos a los distribuidores, con X cantidad de vehiculos.
Consulta:
En la pagina principal de pedidos, aparece la opcion de poder filtrar los pedidos agregados por Fecha, ID, Nombre, y el Distribuidor. Ademas, se podra filtrar por la fecha en el que el pedido fue realizado conjuntamente con el campo seleccionado.
Tambien contiene un input de busqueda para escribir la palabra clave a filtrar. Al lado aparece la opcion de Agregar Pedido al sistema. Se crea con los siguientes campos:

- Distribuidor (obligatorio, debe de estar agregado al sistema).
- PedidoID (valor entero, obligatorio, colocado automaticamente por el sistema).
- Fecha (valor fecha, obligatorio, en formato dia/mes/año).
- Nombre (valor string, obligatorio, se permiten letras y espacios).
- Estado (valor string, obligatorio, se inicializa en "No enviado").

Despues de llenar estos campos, se procede a agregar el o los vehiculos al pedido. Debajo aparece una tabla de Vehiculos, donde aparece en la parte derecha la opcion de Seleccionar Vehiculo.
En Seleccionar Vehiculo, aparece una lista de vehiculos que ha sido agregado al sistema, con una imagen respectiva del vehiculo, alguna de sus caracteristicas y dos opciones: 

- Detalles: opcion para ver las caracteristicas completas del vehiculo seleccionado.
- Seleccionar: opcion para agregar el vehiculo al pedido. Al darle click a seleccionar aparece una ventana emergente donde se especificara la cantidad del vehiculo a agregar, y el precio (no para colocar el precio, solo para referencia).
  Luego de especificada la cantidad, se le da click a Agregar, y el vehiculo se agrega al pedido. Se pueden añadir multiples vehiculos al mismo pedido.

Ya luego de agregar el vehiculo, en la tabla de Vehiculos aparecen las especificaciones completas del vehiculo, y al final esta la opcion de poder remover el vehiculo del pedido.

Ya con los vehiculos agregados se puede completar la creacion del pedido dandole al boton "Guardar" en la parte de debajo.

En la pagina principal aparece la tabla donde se podran observar los pedidos realizados, con sus campos. Y al final de cada pedido se encuentran las siguientes opciones:

- Enviar Pedido: donde se confirmara el envio del pedido al distribuidor correspondiente.
- Visualizar Pedido: donde se podran observar el detalle completo del pedido, con los datos del distribuidor, los datos del pedido y los datos de los vehiculos agregados al pedido.
- Editar Pedido: donde se podran editar los campos del pedido ya realizado.
- Deshabilitar Pedido: donde se podra deshabilitar al pedido y ocultarlo del sistema.

Documentos:
Esta es la entidad que contiene los documentos relacionados a un pedido en especifico. Para poder agregar documentos a un pedido, el pedido debe estar en el estado "Enviado", debido a que los documentos necesarios para un pedido los provee el
Distribuidor luego del pedido ser procesado.
Consulta:
En la pagina principal de documentos, aparece la opcion de poder filtrar los pedidos que han sido enviados por: ID, Nombre. Ademas, esta el input de Buscar donde se digitara la palabra clave a filtrar.
En la tabla, apareceran los pedidos que han sido enviados por el usuario al distribuidor. Apareceran tambien 2 tipos de documentos:

- Commercial Invoice: Es el contrato de venta o una especie de transacción.
- Bill of Lading (B/L): Documento legal que sirve como recibo de las mercancías.

Y debajo de cada uno las siguientes opciones:

- Subir Documento: donde se podra subir el documento especificado.
- Ver Documento: donde se podra ver el documento subido. Dentro de esta opcion se encuentra tambien la opcion de eliminar el documento seleccionado.

- ROL ADMIN
En el rol admin tenemos los siguientes registros: Deshabilitados y Vehiculos.
Al iniciar sesion como admin tendremos una pantalla principal donde se podran observar alguna de las estadisticas como la cantidad de vehiculos agregados, la cantidad de vehiculos activos habilitados y la cantidad de
vehiculos deshabilitados. Ademas, un acceso rapido a los registros de Deshabilitados y Vehiculos, y una tabla donde se podran observar los ultimos 5 vehiculos agregados al sistema.

Deshabilitados:
En este registro el admin tendra la capacidad de volver a habilitar un distribuidor o un pedido que haya sido deshabilitado por un usuario. Ademas, se podra tambien eliminar completamente un distribuidor o pedido deshabilitado
Consulta:
Al inicio tendremos la opcion de cambiar a los Distribuidores deshabilitados o Pedidos deshabilitados. Debajo se encuentra una tabla donde aparecen las entidades deshabilitadas por el usuario, con sus atributos.
Al final de cada entidad tendremos estas opciones:

- Habilitar Entidad: donde se podra volver a habilitar la entidad y hacerla visible en el sistema.
- Eliminar Entidad: donde se podra eliminar completamente la entidad del sistema.

Vehiculos:
Esta es la entidad con la que se crean los pedidos, cada vehiculo puede pertenecer a 1 o mas pedidos. En el registro de vehiculos tenemos la opcion de filtrar los vehiculos que han sido agregados por: ID, Marca, Modelo, 
Color, Numero de Chasis, Año de Fabricacion, Motor, Transmision, Traccion, Puertas, Kilometraje, Combustible y Precio. Tambien esta el input de Buscar para poder filtrar por X palabra clave. Y tambien aparecera la opcion
de poder mostrar los vehiculos que han sido deshabilitados. Y la opcion de Agregar Vehiculo. Se crea con los siguientes campos:

- VehiculoID (valor entero, colocado automaticamente por el sistema).
- Marca (valor string, se permite usar letras).
- Modelo (valor string, se permite usar letras y numeros).
- Color (valor string, se permite usar letras).
- Numero Chasis (valor string, se permite usar letras y numeros).
- Año Fabricacion (valor int, solo numeros).
- Motor (valor string, se permite letras y numeros).
- Transmision (valor string, se permite letras y numeros).
- Traccion (valor string, se permite letras y numeros).
- Numero Puertas (valor int, solo numeros).
- Kilometraje (valor int, solo numeros).
- Estado (valor string, se permite usar letras).
- Tipo Combustible (valor string, se permite usar letras).
- Precio (valor decimal, solo numeros).
- Imagen (archivo, se permite .jpg, .png, .pdf, .webp, .jpeg).

Despues de ingresado los datos, se procede a guardar el vehiculo dandole al boton "Guardar" en la parte de debajo.

Luego de agregar el vehiculo, aparecera en la pantalla principal de vehiculos que estan agregados al sistema. Al final del vehiculo se encuentran las siguientes opciones:

- Editar Vehiculo: donde se podran gestionar los campos de un vehiculo agregado.
- Deshabilitar Pedido: donde se podra deshabilitar un vehiculo y ocultarlo a un User.

Si activamos la opcion "Mostrar Deshabilitados" en la pagina principal de vehiculos, apareceran los vehiculos que han sido desactivados por el admin. Ademas, se encuentran las siguientes opciones:

- Habilitar Vehiculo: donde se permite volver a habilitar un vehiculo deshabilitado.
- Eliminar Vehiculo: donde se podra eliminar completamente un vehiculo del sistema.
