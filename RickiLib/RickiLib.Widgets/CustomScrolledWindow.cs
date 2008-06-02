
using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class CustomScrolledWindow : Gtk.Viewport
	{
		private Gtk.ScrolledWindow scroll;
		
		public CustomScrolledWindow()
		{
			scroll = new ScrolledWindow ();
			base.ShadowType = ShadowType.None;
			base.Add (scroll);
		}
		
		public new void Add (Gtk.Widget widget)
		{
			scroll.Add (widget);
		}
		
		
//		public Gtk.ScrolledWindow ScrolledWindow {
//			get {
//				return scrolled;
//			}
//		}
	}
}
