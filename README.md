# Documentación Técnica Web Api Aranda

Para el correcto funcionamiento del web api se deben ejecutar los siguientes pasos:

1. **Migración de Base de datos (EF Code First):** Se debe ejecutar los siguientes comandos en la consola de Nugets de Visual Studio para la creación de la base de datos con su respectivas tablas:

~~~CMD
Add-Migration Initial -p Aranda.Repository.SqlServer -s Aranda.Repository.SqlServer
~~~

~~~CMD
Update-database -p Aranda.Repository.SqlServer -s Aranda.Repository.SqlServer
~~~

_NOTA:_ 

1.1. Sino se desea realizar la migración se adjunta script de base datos para su ejecución en Sql Server _(nombre de archivo: ArandaDB.sql)_.
1.2 Si se genera el siguiente error en la migración: <p style="color:Red">**"The name 'Initial' is used by an existing migration."**</p>

Se debe eliminar el contenido de la carpeta **Migrations** en la biblioteca de clases **Aranda.Repository.SqlServer** para su correcto funcionamiento.

5. **Administración de repositorio GitHub:** Se trabajo en la rama de _Developer_ pero también la rama master está homologada.

**Url de repositorio:** https://github.com/Ju4nMa24/Aranda.WebApi.git
