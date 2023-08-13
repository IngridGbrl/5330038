using _5330038.Data;
using _5330038.Models;

namespace _5330038.Views;


//se que indica que esta página puede recibir un parámetro
//llamado Item cuando se navega a ella usando Shell.

[QueryProperty("Item","Item")]
public partial class TodoItemPage : ContentPage
{
    TodoItem item;

    //Este parámetro es una instancia de la clase TodoItem, que
    //representa una tarea pendiente con propiedades de la clase
    //se almacena la instancia de TodoItem que se pasa como parámetro.
    public TodoItem Item
    {
        //se devuelve o asigna el valor del campo item al contexto de enlace
        //(BindingContext) de la página. 
        get => BindingContext as TodoItem;
        set => BindingContext = value;
    }

    //se almacena una instancia de la clase TodoItemDatabase, que
    //representa la base de datos de tareas pendientes esta misma clase
    //tiene métodos para obtener, guardar y eliminar
    //tareas pendientes de forma asincrónica.
    TodoItemDatabase database;

    //en el constuctor se carga un parámetro para
    //TodoItemDatabase y se asigna como  database.
    public TodoItemPage(TodoItemDatabase todoItemDatabase)
	{
		InitializeComponent();
        database = todoItemDatabase;

	}

    async void Save_Clicked(object sender, EventArgs e)
    {
        //Este método guarda o actualiza la tarea pendiente en la bd
        // se verifica si el nombre de la tarea pendiente está vacío o no.
        // Si está vacío, muestra una alerta pidiendo que se ingrese un nombre sino va a ir guardar
        //o actualizr la tarea
        if (string.IsNullOrWhiteSpace(Item.Name))
        {
            await DisplayAlert("Name Required", "Please enter a name for the todo item.", "OK");
            return;
        }
        await database.SaveItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

   async void Delete_Clicked(object sender, EventArgs e)
    {
        //se verifica si el id de la tarea pendiente es cero o no  si es cero,
        //significa que la tarea pendiente es nueva y no se hace nada. Si no es cero, significa que
        //la tarea pendiente existe en la bd y se eliminará
        if (Item.Id == 0)
            return;
        await database.DeleteItemAsync(Item);
        await Shell.Current.GoToAsync("..");

    }
    async void Cancel_Clicked(object sender, EventArgs e)
    {
        //con este metodo nada más se llama a la siguiente funcion
        await Shell.Current.GoToAsync("..");
    }
}