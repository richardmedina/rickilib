
using System;
using Gtk;


namespace RickiLib.Widgets
{
	
	
	public class CustomMenu : Gtk.MenuItem
	{
		private Gtk.Menu menu;
		private Gtk.AccelGroup _accel;
		
		public AccelFlags AccelFlags = AccelFlags.Visible;
		
		public CustomMenu (Gtk.AccelGroup accel, string text) : base (text)
		{
			_accel = accel;
		
			menu = new Gtk.Menu ();
			this.Submenu = menu;
		}
		
		public void AddAcceleratorTo (Gtk.MenuItem item, Gdk.Key key, Gdk.ModifierType modifier)
		{
			item.AddAccelerator ("activate", Accel, new AccelKey (key, modifier, AccelFlags));
		}
		
		public Gtk.AccelGroup Accel {
			get { return _accel; }
		}
		
		public Gtk.Menu Menu {
			get {
				return menu;
			}
		}
	}
}
