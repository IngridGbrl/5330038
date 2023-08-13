using _5330038.Data;
using _5330038.Models;
using System.Collections.ObjectModel;

namespace _5330038.Views;

public partial class TodoListPage : ContentPage
{
	//se crea la propiedad que nos permitira acceder a la bd
	TodoItemDatabase database;

    //El tipo ObservableCollection permite actualizar automáticamente la interfaz gráfica cuando se
	//añade o elimina un elemento de la colección se le asigna esta clase a la propiedad Items que
	//almacenara y mostrara la lista de tareas.
    public ObservableCollection<TodoItem> Items { get; set; } = new();

    //Se agrega como parametro la clase TodoItemDatabase  en una propiedad la cual se asigna a la variable
	//database para poder acceder a la base de datos desde esta clase

	//También se asigna BindingContext a this, lo que significa que se usará esta clase como fuente de datos
	//para el enlace entre la interfaz gráfica y el código.
    public TodoListPage(TodoItemDatabase todoItemDatabase)
	{
		InitializeComponent();
		database = todoItemDatabase;
		BindingContext = this;
	}


	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
        //Este método se ejecutará cuando se navega en esta pagina página con args contiene la informcion sobre
		//la navegacion

		//se llama al metodo OnNavigatedTo
        base.OnNavigatedTo(args);

		//se declara una variable que contendra todos los elementos de la lista de tareas de la bd local con await se
		//espera que se complete la tarea
		var items = await database.GetTodoItemsAsync();

        //con este codigo se va  a ejecutar un código en el hilo principal esto es necesario porque se va a modificar la
		//interfaz gráfica, y solo se puede hacer desde el MainThread.
        MainThread.BeginInvokeOnMainThread(() =>
        {
            //se limpia la colección Items para eliminar cualquier elemento anterior. Luego, se recorre la variable items,
			//que contiene los elementos obtenidos desde la bd, y se agregan a la colección y se actualiza la vista de
			//colección con los elementos actuales.
            Items.Clear();
		foreach (var item in items)
			Items.Add(item);
	
		});
	}

    async void ItemAdded_Clicked(object sender, EventArgs e)
    {
        //en este evento del boton, se puede ir agregando las tareas con la clase shell se navega a TodoItemItemPage donde
		//se creara o editara un elemento de la lista de tareas


        //El primer parámetro es el nombre de la página, con el parámetro true se indica si se quiere animar la transición, y
		//el parámetro new es un diccionario que contiene los datos que se quieren pasar a la otra página.
		
		//En este caso, se pasa a Item, que es un nuevo objeto de TodoItem y asi se crea un nuevo elemento vacío que se puede
		//rellenar en la otra página
		//que seria TodoItemPage
        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
		{
			["Item"] = new TodoItem()
		});
    }
	private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //este método se ejecutará cuando se cambie la selección de la vista de colección y espera a que se complete una tarea
		//sin bloquear el hilo principal


        //se usa el patrón de coincidencia de tipos para comprobar si el primer elemento de la selección actual es de TodoItem
		//Si no lo es se saldr'a del método con la palabra clave return
		//Esto ocurre si no hay ningún elemento seleccionado o si el elemento seleccionado no es un elemento de la lista de tareas
        if (e.CurrentSelection.FirstOrDefault() is not TodoItem item)
			return;

		await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>{
			["Item"] = item
		});
    }
}