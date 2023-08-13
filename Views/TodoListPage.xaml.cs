using _5330038.Data;
using _5330038.Models;
using System.Collections.ObjectModel;

namespace _5330038.Views;

public partial class TodoListPage : ContentPage
{
	//se crea la propiedad que nos permitira acceder a la bd
	TodoItemDatabase database;

    //El tipo ObservableCollection permite actualizar autom�ticamente la interfaz gr�fica cuando se
	//a�ade o elimina un elemento de la colecci�n se le asigna esta clase a la propiedad Items que
	//almacenara y mostrara la lista de tareas.
    public ObservableCollection<TodoItem> Items { get; set; } = new();

    //Se agrega como parametro la clase TodoItemDatabase  en una propiedad la cual se asigna a la variable
	//database para poder acceder a la base de datos desde esta clase

	//Tambi�n se asigna BindingContext a this, lo que significa que se usar� esta clase como fuente de datos
	//para el enlace entre la interfaz gr�fica y el c�digo.
    public TodoListPage(TodoItemDatabase todoItemDatabase)
	{
		InitializeComponent();
		database = todoItemDatabase;
		BindingContext = this;
	}


	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
        //Este m�todo se ejecutar� cuando se navega en esta pagina p�gina con args contiene la informcion sobre
		//la navegacion

		//se llama al metodo OnNavigatedTo
        base.OnNavigatedTo(args);

		//se declara una variable que contendra todos los elementos de la lista de tareas de la bd local con await se
		//espera que se complete la tarea
		var items = await database.GetTodoItemsAsync();

        //con este codigo se va  a ejecutar un c�digo en el hilo principal esto es necesario porque se va a modificar la
		//interfaz gr�fica, y solo se puede hacer desde el MainThread.
        MainThread.BeginInvokeOnMainThread(() =>
        {
            //se limpia la colecci�n Items para eliminar cualquier elemento anterior. Luego, se recorre la variable items,
			//que contiene los elementos obtenidos desde la bd, y se agregan a la colecci�n y se actualiza la vista de
			//colecci�n con los elementos actuales.
            Items.Clear();
		foreach (var item in items)
			Items.Add(item);
	
		});
	}

    async void ItemAdded_Clicked(object sender, EventArgs e)
    {
        //en este evento del boton, se puede ir agregando las tareas con la clase shell se navega a TodoItemItemPage donde
		//se creara o editara un elemento de la lista de tareas


        //El primer par�metro es el nombre de la p�gina, con el par�metro true se indica si se quiere animar la transici�n, y
		//el par�metro new es un diccionario que contiene los datos que se quieren pasar a la otra p�gina.
		
		//En este caso, se pasa a Item, que es un nuevo objeto de TodoItem y asi se crea un nuevo elemento vac�o que se puede
		//rellenar en la otra p�gina
		//que seria TodoItemPage
        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
		{
			["Item"] = new TodoItem()
		});
    }
	private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //este m�todo se ejecutar� cuando se cambie la selecci�n de la vista de colecci�n y espera a que se complete una tarea
		//sin bloquear el hilo principal


        //se usa el patr�n de coincidencia de tipos para comprobar si el primer elemento de la selecci�n actual es de TodoItem
		//Si no lo es se saldr'a del m�todo con la palabra clave return
		//Esto ocurre si no hay ning�n elemento seleccionado o si el elemento seleccionado no es un elemento de la lista de tareas
        if (e.CurrentSelection.FirstOrDefault() is not TodoItem item)
			return;

		await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>{
			["Item"] = item
		});
    }
}