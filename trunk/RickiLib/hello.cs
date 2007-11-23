using System;
using Gtk;
using RickiLib.Types;
using RickiLib.Widgets;

public class Hello {

	private static ModuleCollection tab;

	static void Main ()
	{
		Application.Init ();
		CustomWindow win = new CustomWindow ("Test");
		tab = new ModuleCollection ();
		tab.Add(new Module("Salir"));
		tab.Add(new Module("source.cs", "home/ricki/progros"));
		
		HBox hbox = new HBox (false, 0);
		hbox.PackStart (new TreeModuleChooser (tab));
		hbox.PackStart (new IconModuleChooser (tab));

		Gtk.Button button = new Button (Stock.Help);
		
		button.Clicked += delegate { CustomDialog d = new CustomDialog (); d.Resizable = true; d.Run ();};
		win.VBox.PackStart (button, false, false, 0);
		win.VBox.PackStart (hbox);
		win.VBox.PackStart (new NotebookModuleChooser (tab), false, false, 0);
		
		win.ShowAll ();
		
		//GLib.Timeout.Add (1000, glib_timer);
		for (int i = 0; i < 10; i ++)
			tab.Add (new MyPage ());
		
		Application.Run ();
	}

	private static bool glib_timer ()
	{
		tab.Add (new MyPage ());
		return true;
	}

}

public class MyPage : Module {

	static int instance = 0;
	static Random r = new Random ();
	public MyPage (): base ("MyTabPage " + (++ instance).ToString (), (r.Next (4) + 1).ToString ())
	{
//		base.Path = (r.Next (5)).ToString ();
		base.Add (new Label (instance.ToString ()));
	}

}
