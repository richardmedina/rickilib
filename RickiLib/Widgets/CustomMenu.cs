
using System;

namespace RickiLib.Widgets
{
	
	
	public class CustomMenu : Gtk.MenuItem
	{
		private Gtk.Menu menu;
		
		public CustomMenu (string text) : base (text)
		{
			menu = new Gtk.Menu ();
			this.Submenu = menu;
		}
		
		public Gtk.Menu Menu {
			get {
				return menu;
			}
		}
	}
}
