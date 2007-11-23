
using System;

namespace RickiLib.Widgets
{
	
	
	public class CustomCellRendererText : Gtk.CellRendererText
	{
		
		public CustomCellRendererText ()
		{
		}
		
		public new void SetProperty (string key, GLib.Value val)
		{
			base.SetProperty (key, val);
		}
	}
}
