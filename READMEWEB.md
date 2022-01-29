# Agenda+

> Logo de la aplicación.
## Aplicación Web
#Introducción

Debido a la pandemia que azotó al mundo, cambiaron la manera en que se fue organizando las personas. Llevando así las jornadas
presenciales a virtuales . Por esta nueva modalidad se juntan las tareas tanto laboral como del hogar causando así que las personas
no tengan una buena organización. Para solucionar este problema se propone el desarrollo de una aplicación de una agenda digital en
donde le usuario pueda ver sus pendientes, contanctos, notas y fechas importantes.

#Clonar el proyecto

Pasos para clonar el proyecto

- Abrir Visual Studio y seleccionar la opción "Clonar Repositario"
- Copiar el link del repositori en HTTPS

> Link del proyecto.
## Pasos para instalar la aplicación
### Pasos para obtener la base de datos
- Descargar **Script.sql** desde el repositorio GitHub
- Abrir Microsoft SQL Server Management Studio y conectarse localmente
- Dar click en  **New Query** y pegar todo el contenido de **Script.sql**
- Click en **Execute**
### Pasos para el funcionamiento de la Base de Datos
- Abrir la solución **AgendaPlusWeb**
- Ingresar en el archivo **"Web.config"**
- Cambiar el código \*<connectionString> con el nombre de su PC en el apartado **"data source"**, y lo demás se deja por defecto.

      <connectionStrings>
      <add name="BaseDeDatos" connectionString="data source = DESKTOP-ITRVH8C\SQLEXPRESS;initial catalog=ProyectoFinalDB;integrated security=True;MultipleActivateResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient"/>
      </connectionStrings>
- Recompilar la solución con los cambios hechos
- Ejecutar la Aplicación
### Funcionamiento Correcto de la Aplicación
Si siguió todos los pasos se verá reflejado en su Explorador Web, el Frame Login de Agenda+

> Página Login de Agenda+

Si desea acceder puede usar el siguiente Usuario que ya cuenta con pendientes, contactos, fechas importantes agregados

**Email: camh@udla.edu.ec
Password: Camh123@**

Caso contrario debe dar click en **"Register Now"** para crear un nuevo usuario

> Pantalla para crear un Usuario Nuevo
#### Agenda+ fue Creada por:
- Carlos Mejía
- Caleb Naranjo
- Victor Ponce

#End
