# Agenda+
![](https://i.ibb.co/c21kTCz/logoagenda.png)

> Logo de la aplicación.

**Tabla de Contenidos**

[TOCM]

[TOC]

## Introducción

Debido a la pandemia que azotó al mundo, cambiaron la manera en que se fue organizando las personas. 
Llevando así las jornadas presenciales a virtuales . Por esta nueva modalidad se juntan las tareas tanto laboral como del hogar causando así que las eprsonas no tengan una buena organización.
Para solucionar este problema se propone el desarrollo de una aplicación de una agenda digital en donde le usuario pueda ver sus pendientes, contanctos,
notas y fechas importantes.

## Clonar el proyecto

Pasos para clonar el proyecto 

- Abrir Visual Studio y seleccionar la opción **"Clonar Repositorio"**
- Copiar el link del repositorio en HTTPS

![](https://i.ibb.co/3phVRys/https.png)
>Link del Proyecto.


## Pasos para instalar la aplicación

###Pasos para obtener la base de datos 

Para usar la aplicación se debe tener instalado Microsoft SQL Server Management Studio en su PC

- Descargar **Script.sql** desde el repositorio GitHub
- Abrir Microsoft SQL Server Management Studio y conectarse localmente
- Dar click en **"New Query"** y pegar todo el contenido de **Script.sql**
- Click en **"Execute"**


### Pasos para el funcionamiento de la API

- Abrir la solución **AgendaPlusAPI **
- Ingresar en **"Web.config"**
- Cambiar el código <connectionStrings> con el nombre de su PC en el apartado **"data source"**, lo demás se deja por defecto.

		<connectionStrings>
		<add name="BaseDeDatos" connectionString="data source = DESKTOP-ITRVH8C\SQLEXPRESS;initial catalog=ProyectoFinalDB;integrated security=True;MultipleActivateResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient"/>
		</connectionStrings>

- Recompilar la solución con los cambios hechos
- Ejecutar la API

### Pasos para el funcionamiento de la Aplicación UWP

La aplicación está hecha en resolución 1280x720 px y la solución **AgendaPlusAPI** debe seguir ejecutándose en todo momento que sea usada Agenda+

- Abrir la solución AgendaPlusUWP
- Recompilar la solución
- Ejecutar la aplicación

Si siguió todos los pasos se verá reflejado en su pantalla, el Frame Login de Agenda+

![](https://i.ibb.co/phN18KG/login.png)
>Página Login de Agenda+

Si desea acceder puede usar el siguiente Usuario que ya cuenta con pendientes, contactos, fechas importantes agredados

**Email: camh@udla.edu.ec
Password: Camh123@**

Caso contrario debe dar click en **"Register Now"** para crear un nuevo usuario

![](https://i.ibb.co/xjj8y28/create.png)
>Pantalla para crear un Usuario Nuevo

#### Agenda+ fue Creada por:
- Carlos Mejía
- Caleb Naranjo
- Victor Ponce
#End
