using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5330038
{
    public static  class Constants
    {
        //se declara una constante (no se podra cambiar luego de declararla) que contendra
        //el nombre del archivo de la bd local y construira la ruta del archivo de esta misma bd
        public const string DatabaseFilename = "TodoSQLite.db3";

        //en esta constante nos permite trabajar con la bd donde se combinan varios valores
        //del tipo enumerado con el operador | (o) a nivel de bits
        public const SQLite.SQLiteOpenFlags Flags =
            //se abre la bd en modo lectura/escritura, lo que nos permitirá
            //leer y modificar los datos
            SQLite.SQLiteOpenFlags.ReadWrite | 
            //se crea la bd si es que no existe
            SQLite.SQLiteOpenFlags.Create |
            //se habilita el acceso a la bd desde varios hilos
            SQLite.SQLiteOpenFlags.SharedCache;


        //FileSystem.AppDataDirectory se usa para acceder a funciones del sistema operativo y devuelve
        //la ruta del directorio donde se almacenan los datos de la aplicación

        //DatabaseFilename que es la constante declarada anteriormente, que contiene el nombre del archivo
        //de la base de datos
        
        //al combinar estas dos cadenas se crea la ruta completa del archivo de la base de datos local
        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }

}
