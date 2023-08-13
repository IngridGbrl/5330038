using _5330038.Data;
using _5330038.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Media;
using Microsoft.Win32;

namespace _5330038;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<TodoListPage>();
        //indica que se registra un servicio de tipo TodoListPage, que define una clase la cual muestra los elementos de
		//una lista de tareas.

        builder.Services.AddTransient<TodoItemPage>();
        //registra un servicio de tipo TodoItemPage, que es una clase que define una pagina para crear o editar un elemento
		//de la lista de tareas,
		//el metodo indica que se creara una nnueva instancia del servicio cada vez que se solicite.
        builder.Services.AddSingleton<TodoItemDatabase>();
        //registra un servicio de tipo TodoItemDatabase, que es una clase que se encarga de las operaciones con la base de datos local
		//el metodo indica que se usara el mismo tipo de objeto TodoItemDatabase para todas las solicitudes del servicio
        return builder.Build();
	}
}
