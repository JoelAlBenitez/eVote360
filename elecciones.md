Elecciones
Al ingresar a la opción Elecciones desde el menú principal del administrador, el
sistema debe enviar al usuario al módulo de gestión de elecciones.
Este módulo permitirá crear procesos electorales, consultar elecciones registradas,
activar una elección pendiente, finalizar una elección activa y visualizar los
resultados de elecciones finalizadas.
Una elección representa un proceso electoral formal dentro del sistema. Para
preservar la consistencia de los resultados, el sistema no debe crear copias
históricas de partidos políticos, candidatos ni puestos electivos al momento de crear
una elección. Los resultados se calcularán utilizando los datos actuales registrados
en el sistema.
Debido a esto, una vez que un puesto electivo, partido político o candidato haya
participado en una elección, el sistema no debe permitir modificar los campos
principales que afectan la boleta electoral o los resultados.
Para fines de bloqueo de campos críticos, se considera que un puesto electivo,
partido político o candidato participó en una elección desde el momento en que una
elección donde aparece pasa al estado Activa.

Estados de una elección
Toda elección debe manejar uno de los siguientes estados:

Estado 		Descripción
Pendiente La elección fue creada, pero todavía no está disponible para votación.
Activa La elección está disponible para que los ciudadanos puedan votar.
Finalizada La elección terminó y sus resultados pueden ser consultados. No se permite votar ni modificar votos.


Pueden existir múltiples elecciones en estado pendiente.
Solo puede existir una elección en estado Activa a la vez.
Una elección en estado pendiente no permite votación.
Una elección en estado activo permite la votación.
Una elección en estado Finalizada no permite nuevos votos.
Pantalla inicial del módulo
En la pantalla inicial del módulo de elecciones, el sistema debe mostrar un listado
con todas las elecciones registradas, organizadas desde la más reciente hasta la
más antigua.
Si existe una elección en estado Activa, esta debe aparecer de primera en el listado
y debe contar con una etiqueta o color visual que indique claramente que se
encuentra activa.
De cada elección se debe mostrar la siguiente información:

Campo		  Descripción
Nombre de la elección Nombre con el que fue registrado el proceso electoral.
Fecha de realización Fecha informativa asignada a la elección.
Estado Estado actual de la elección: Pendiente, Activa o
Finalizada.
Cantidad de partidos
participantes
Cantidad de partidos políticos que participan en la
elección.

Cantidad de puestos
disputados
Cantidad de puestos electivos incluidos en la
elección.
Cantidad de ciudadanos
que votaron
Total de ciudadanos que finalizaron correctamente
su proceso de votación.

Cada elección listada debe tener acciones según su estado
Pendiente Activar Permite iniciar formalmente el
proceso de votación.
Activa Finalizar Permite cerrar la elección activa.
Finalizada Ver resultados Permite consultar los resultados de
la elección.

Arriba del listado debe existir un botón con el texto Crear elección.
El botón Crear elección sólo debe estar disponible si no existe una elección activa.
Como pueden existir múltiples elecciones pendientes, el sistema sí puede permitir
crear nuevas elecciones en estado Pendiente, siempre que no exista una elección
Activa y que se cumplan las validaciones requeridas.
Crear elección
Al hacer clic sobre el botón Crear elección, el sistema debe enviar al usuario a una
pantalla con un formulario para registrar un nuevo proceso electoral.
Una elección nueva debe crearse inicialmente en estado Pendiente. Esto significa
que, luego de crearla, todavía no estará disponible para votación hasta que el
administrador la active desde el listado de elecciones.
El formulario debe contener los siguientes campos:

Nombre de la
elección
Texto /
string
Sí Nombre que identificará el proceso
electoral.
Fecha de
realización
Date Sí Fecha informativa asociada al
proceso electoral.


Descripción de campos
Nombre de la elección
Representa el nombre con el que se identificará el proceso electoral dentro del
sistema.
Ejemplo:
● Elecciones Municipales 2026
● Elecciones Congresuales 2026
● Elecciones Presidenciales 2028
Este nombre será mostrado en el listado de elecciones, en la pantalla de resultados
y en el resumen enviado al ciudadano luego de finalizar su votación.
Fecha de realización
Representa la fecha oficial o referencial asociada al proceso electoral.
Ejemplo:
15/05/2026
20/02/2028
La fecha de realización es informativa y se utiliza para organización, consulta por
año y reportes.
La fecha de realización no activa automáticamente la elección.
La activación de la elección será manual y deberá realizarla el administrador
mediante el botón Activar.
El sistema no debe impedir activar una elección por estar antes o después de la
fecha de realización, siempre que se cumplan las validaciones de activación.
Validaciones para crear elección
El formulario de creación debe cumplir las siguientes validaciones:
● El nombre de la elección es requerido.
● La fecha de realización es requerida.
● No debe existir una elección activa.
● Debe existir al menos un puesto electivo activo.
● Deben existir al menos dos partidos políticos activos.
● Cada partido político activo debe tener un candidato activo asignado para
cada puesto electivo activo.
● No debe permitirse crear una elección si no existen puestos electivos activos.
● No debe permitirse crear una elección si no existen suficientes partidos
políticos activos.
● No debe permitirse crear una elección si algún partido político activo no tiene
candidatos activos asignados para todos los puestos electivos activos.
Si no existen puestos electivos activos, el sistema debe mostrar el siguiente
mensaje:
“No hay puestos electivos activos para realizar una elección.”
Si no existen al menos dos partidos políticos activos, el sistema debe mostrar el
siguiente mensaje:
“No hay suficientes partidos políticos para realizar una elección.”
Si existen partidos políticos activos, pero alguno de ellos no tiene candidatos
activos asignados para todos los puestos electivos activos, el sistema debe mostrar
un mensaje por cada partido con el siguiente formato:
“El partido político [nombre del partido] ([siglas del partido]) no tiene
candidatos activos asignados para los siguientes puestos electivos:
[listado de puestos].”
Si el usuario intenta crear una elección mientras existe una elección activa, el
sistema debe mostrar el siguiente mensaje:
“No se puede crear una nueva elección mientras exista una elección
activa.”
Al final del formulario debe haber dos botones:
● Volver atrás: devuelve al usuario al listado de elecciones sin guardar
cambios.
● Crear elección: guarda el proceso electoral en el sistema.
Al hacer clic en Crear elección, si los datos son válidos, el sistema debe crear la
elección en estado Pendiente y redirigir al usuario al listado de elecciones.
Configuración dinámica de elecciones pendientes
Mientras una elección esté en estado Pendiente, su configuración se considera
dinámica.
Esto significa que la elección pendiente no congela partidos políticos, candidatos ni
puestos electivos.
La configuración válida para activar una elección será la configuración existente en
el sistema al momento de presionar el botón Activar.
Por esta razón, aunque una elección haya sido creada correctamente en estado
Pendiente, el sistema debe volver a validar toda la configuración electoral antes de
activarla.
El sistema debe validar nuevamente:
● Que no exista otra elección activa.
● Que existan puestos electivos activos.
● Que existan al menos dos partidos políticos activos.
● Que cada partido político activo tenga candidatos activos asignados para
todos los puestos electivos activos.
● Que no existan inconsistencias en las asignaciones de candidatos a puestos.
● Que los candidatos asignados se encuentren activos.
● Que los partidos de los candidatos asignados se encuentren activos.
● Que los puestos electivos asignados se encuentren activos.
Relación de la elección con los datos actuales del sistema
Al crear una elección, el sistema no debe crear copias históricas de puestos
electivos, partidos políticos ni candidatos.
La elección debe trabajar con los registros actuales existentes en los módulos de:
● Puestos electivos.
● Partidos políticos.
● Candidatos.
● Asignación de candidatos a puestos.
Esto significa que la boleta electoral y los resultados utilizarán los datos actuales
registrados en el sistema.
Para evitar inconsistencias, desde el momento en que una elección pasa a estado
Activa, el sistema debe bloquear la modificación de los campos principales de los
puestos electivos, partidos políticos y candidatos que forman parte de esa elección.
Activar elección
Una elección en estado Pendiente debe mostrar un botón con el texto Activar.
Al hacer clic sobre este botón, el sistema debe enviar al usuario a una pantalla de
confirmación.
La pantalla debe mostrar el siguiente mensaje:
“¿Está seguro que desea activar esta elección?”
Debajo del mensaje debe haber dos botones:
● Cancelar
● Aceptar
Si el usuario pulsa Cancelar, el sistema debe regresar al listado de elecciones sin
realizar cambios.
Si el usuario pulsa Aceptar, el sistema debe intentar cambiar el estado de la
elección de Pendiente a Activa.
La activación de una elección es manual. La fecha de realización no activa la
elección automáticamente.
Validaciones para activar elección
Antes de activar una elección, el sistema debe validar:
● La elección debe existir.
● La elección debe estar en estado Pendiente.
● No debe existir otra elección activa.
● Debe existir al menos un puesto electivo activo.
● Deben existir al menos dos partidos políticos activos.
● Cada partido político activo debe tener candidatos activos asignados para
todos los puestos electivos activos.
● La configuración electoral actual debe seguir cumpliendo las condiciones
necesarias para iniciar el proceso de votación.
Si ya existe una elección activa, el sistema debe mostrar el siguiente mensaje:
“No se puede activar esta elección porque ya existe una elección activa.”
Si la elección no está en estado Pendiente, el sistema debe mostrar un mensaje
como:
“Solo se pueden activar elecciones en estado pendiente.”
Si no existen puestos electivos activos, el sistema debe mostrar el siguiente
mensaje:
“No hay puestos electivos activos para activar esta elección.”
Si no existen al menos dos partidos políticos activos, el sistema debe mostrar el
siguiente mensaje:
“No hay suficientes partidos políticos para activar esta elección.”
Si existen partidos políticos activos, pero alguno de ellos no tiene candidatos
activos asignados para todos los puestos electivos activos, el sistema debe mostrar
un mensaje por cada partido con el siguiente formato:
“El partido político [nombre del partido] ([siglas del partido]) no tiene
candidatos activos asignados para los siguientes puestos electivos:
[listado de puestos].”
Si la configuración electoral actual no cumple con las condiciones necesarias para
activar la elección, el sistema debe mostrar un mensaje como:
“No se puede activar esta elección porque la configuración electoral
actual no está completa.”
Si todas las validaciones se cumplen, el sistema debe activar la elección y redirigir al
usuario al listado de elecciones.
A partir de ese momento, los ciudadanos habilitados podrán iniciar el proceso de
votación.
Desde ese momento, los puestos, partidos y candidatos que forman parte de la
elección se consideran participantes, por lo que deben aplicarse las restricciones de
modificación de campos críticos.
Finalizar elección
Una elección en estado Activa debe mostrarse de primera en el listado y debe tener
una etiqueta visual que indique que se encuentra activa.
Para la elección activa no debe mostrarse el botón Ver resultados. En su lugar,
debe mostrarse un botón con el texto Finalizar.
Al hacer clic sobre el botón Finalizar, el sistema debe enviar al usuario a una
pantalla de confirmación.
La pantalla debe mostrar el siguiente mensaje:
“¿Está seguro que desea finalizar esta elección?”
Debajo del mensaje debe haber dos botones:
● Cancelar
● Aceptar
Si el usuario pulsa Cancelar, el sistema debe regresar al listado de elecciones sin
finalizar la elección.
Si el usuario pulsa Aceptar, el sistema debe cambiar el estado de la elección de
Activa a Finalizada y redirigir al usuario nuevamente al listado de elecciones.
Validaciones para finalizar elección
Antes de finalizar una elección, el sistema debe validar:
● La elección debe existir.
● La elección debe estar en estado Activa.
● La elección no debe estar ya finalizada.
Si la elección ya está finalizada, el sistema debe mostrar un mensaje como:
“Esta elección ya se encuentra finalizada.”
Si la elección no está activa, el sistema debe mostrar un mensaje como:
“Solo se pueden finalizar elecciones activas.”
Al finalizar una elección:
● No se deben permitir nuevos votos.
● No se deben permitir modificaciones a votos ya emitidos.
● Se debe habilitar la consulta de resultados.
● El botón Finalizar debe dejar de mostrarse.
● Debe mostrarse el botón Ver resultados.
Ver resultados
Una elección en estado Finalizada debe mostrar un botón con el texto Ver
resultados.
Al hacer clic sobre este botón, el sistema debe enviar al usuario a una pantalla
donde pueda visualizar los resultados de la elección seleccionada.
La pantalla de resultados debe mostrar los puestos disputados en esa elección y,
dentro de cada puesto, los candidatos que participaron, incluyendo la opción
Ninguno.
Los resultados deben calcularse usando los registros actuales del sistema. Por esta
razón, el sistema debe impedir que se modifiquen los datos principales de puestos,
partidos y candidatos que ya hayan participado en una elección.
Información a mostrar en resultados
Para cada puesto electivo disputado en la elección, el sistema debe mostrar un
listado de resultados ordenado desde la opción con mayor cantidad de votos hasta
la de menor cantidad de votos.
De cada opción votable se debe mostrar la siguiente información:

Campo Descripción
Puesto electivo Nombre actual del puesto disputado.
Candidato / opción
Nombre actual del candidato participante o la opción
Ninguno.
Partido político
Partido por el cual participó el candidato. Para la
opción Ninguno, debe mostrarse “No aplica”.
Cantidad de votos Total de votos recibidos por el candidato u opción.
Porcentaje de votos
Porcentaje obtenido sobre el total de votos emitidos
para ese puesto.
Resultado
Indica si es la opción ganadora del puesto o si existe
empate.

Cálculo del porcentaje de votos
El porcentaje de votos debe calcularse tomando como base la cantidad total de
votos emitidos para el puesto electivo consultado.
Fórmula:
Porcentaje = (Cantidad de votos de la opción / Total de votos del puesto) *
100
Ejemplo:
Si para el puesto Senador votaron 100 ciudadanos y el candidato Juan Pérez
obtuvo 30 votos:
Porcentaje = (30 / 100) * 100 = 30%
Por lo tanto, el sistema debe mostrar:
Juan Pérez - 30 votos - 30%
La opción Ninguno debe incluirse en el cálculo de porcentaje.
Ejemplo:
Opción Votos Porcentaje
Juan Pérez 45 45%
María Gómez 35 35%
Ninguno 20 20%



Determinación del ganador
Para cada puesto electivo, el sistema debe identificar como ganador al candidato u
opción con mayor cantidad de votos.
Ninguna opción Ninguno participa en el resultado. Si ninguno obtiene la mayor
cantidad de votos en un puesto, debe mostrarse como la opción más votada.
El sistema debe mostrar los resultados ordenados de mayor a menor cantidad de
votos.
Empates
Si dos o más candidatos u opciones obtienen la misma cantidad de votos en el
primer lugar, el sistema debe mostrar que existe un empate.
Mensaje sugerido:
“Existe un empate en el primer lugar para este puesto electivo.”
En este caso, el sistema no debe marcar un único ganador.
Botón Volver atrás
En la pantalla de resultados debe existir un botón con el texto Volver atrás.
Al hacer clic en este botón, el sistema debe regresar al listado de elecciones.
Reglas adicionales del módulo
● Solo los usuarios con rol Administrador pueden acceder al módulo de
elecciones.
● Una elección sólo puede tener uno de estos estados: Pendiente, Activa o
Finalizada.
● Pueden existir múltiples elecciones en estado pendiente.
● Solo puede existir una elección activa a la vez.
● Al crear una elección, esta debe quedar en estado pendiente.
● Una elección pendiente no permite votar.
● Mientras una elección esté en estado pendiente, su configuración se
considera dinámica.
● Antes de activar una elección pendiente, el sistema debe validar nuevamente
la configuración electoral actual.
● La fecha de realización es informativa.
● La fecha de realización no activa automáticamente la elección.
● La activación de la elección es manual mediante el botón Activar.
● Para permitir la votación, el administrador debe activar manualmente la
elección.
● Al activar una elección, los ciudadanos habilitados podrán iniciar el proceso
de votación.
● Al finalizar una elección, no se deben permitir nuevos votos.
● El botón Crear elección sólo debe estar disponible si no existe una elección
activa.
● El botón Activar solo debe mostrarse para elecciones en estado pendiente.
● El botón Finalizar solo debe mostrarse para elecciones en estado Activa.
● El botón Ver resultados solo debe mostrarse para elecciones en estado
Finalizada.
● El sistema no debe crear copias históricas de partidos, candidatos ni puestos
al crear una elección.
● Los resultados deben calcularse usando los datos actuales del sistema.
● Para fines de bloqueo de campos críticos, un puesto electivo, partido político
o candidato se considera participante desde el momento en que una elección
donde aparece pasa a estado Activa.
● No se deben permitir modificaciones a los campos principales de partidos,
candidatos o puestos que ya hayan participado en una elección activa o
finalizada.
● La opción Ninguno debe aparecer en los resultados como una opción más
votable.
● El porcentaje de votos debe calcularse sobre el total de votos emitidos para
cada puesto.
● La cantidad de ciudadanos que votaron no debe confundirse con la cantidad
total de votos por puesto.
● El listado de elecciones debe estar organizado desde la más reciente hasta la
más antigua.
● Si existe una elección activa, debe aparecer de primera en el listado.
● El módulo debe usar el mismo layout general de la aplicación.
