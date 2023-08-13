using _5330038.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5330038.Data
{

    public class TodoItemDatabase
    {
        //se declara una propiedad que contendra la instancia de la siguiente clase
        SQLiteAsyncConnection DataBase;
        public TodoItemDatabase()
        {

        }
        //se inicializa DataBase usando Init la cual es una funcion asincronica para inicializar el campo
        async Task Init()
        {
            if (DataBase is not null)
                return;

            DataBase = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
           //se espera la operacion asincronica antes de continuar usando await
           //Se manda a llamar la clase TodoItem con CreateTableAsync para crear una tabla en bd
            var result = await DataBase.CreateTableAsync<TodoItem>();
        }

        public async Task<List<TodoItem>> GetTodoItemsAsync()
        {

            // con este metodo que es una funcion asincronica se manda a llamar una lista que contendrá 
            //los datos almacenados en  la clase TodoItem
            await Init();
            return await DataBase.Table<TodoItem>().ToListAsync();
        }

        public async Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            //Este metodo hace lo mismo como en el anterior pero usando la clausula WHERE para filtar
            //resultados con la siguiente condicion la propiedad Done recibe un parametro y devuelve
            //el valor de su propiedad
            await Init();
            return await DataBase.Table<TodoItem>().Where(t => t.Done).ToListAsync();
            //SQL queries are also possible
            //return await DataBase.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0"];
        }

        public async Task<TodoItem> GetItemAsync(int id)
        {
            //En este metodo se recibe el parámetro id y devuelverá la propiedad Id de la clase TodoItem con ese id, o
            //null si no existe
            //Este método hace lo mismo que el método GetTodoItemsAsync,
            //pero se añade una cláusula Where para filtrar los resultados para la siguiente condicion y llama al método
            //FirstOrDefaultAsync, que ejecuta la consulta y devuelve el primer resultado o null si no hay ninguno

            await Init();
            return await DataBase.Table<TodoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();

        }


        public async Task<int> SaveItemAsync(TodoItem item)
        {
            //Se recibe un parámetro item de la propiedad Id de TodoItem y devuelve un entero que indica el número de filas
            //afectadas por la operación y se llama al método Init y luego se verifica si el item tiene un id
            //distinto de cero Si es así, significa que el item ya existe en la bd y se debe actualizar
            await Init();
            if(item.Id != 0)
            {
                return await DataBase.UpdateAsync(item);
            }
            else
            {
                //si el item es nuevo entonces se guardará
                return await DataBase.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(TodoItem item)
        {
            //Con este metodo se manda a llamar Init y el item se eliminará de la bd
            await Init();
            return await DataBase.DeleteAsync(item);
        }
    }
}
