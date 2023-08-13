using _5330038.Views;

namespace _5330038;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        //se registra la ruta para navegar a la página TodoItemPage,
		//la cual se crearán o editará un elemento de la lista de tareas
        Routing.RegisterRoute(nameof(TodoItemPage), typeof(TodoItemPage));
	}
}
