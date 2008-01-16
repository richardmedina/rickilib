// WindowSettings.cs created with MonoDevelop
// User: ricki at 21:43 01/08/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace RickiLib
{
	
	
	public class WindowSettings
	{
		private int _x = 0;
		private int _y = 0;
		private int _width = 640;
		private int _height = 480;
		
		private bool maximized = false;
		
		private string name = "unnamed_application";
		
		private Gtk.Window _window;
		
		public WindowSettings (Gtk.Window window, string conf_name)
		{
			name = conf_name;
			_window = window;
		}
		
		private void Apply (Gtk.Window window)
		{
			window.GdkWindow.MoveResize (_x, _y, _width, _height);
			if (maximized)
				window.Maximize ();
		}
		
		public void Save (Gtk.Window window)
		{
		
		}
	}
}
