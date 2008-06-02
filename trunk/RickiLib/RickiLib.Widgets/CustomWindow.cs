
using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class CustomWindow : Gtk.Window
	{
		private Gtk.VBox vbox;
		
		public CustomWindow (WindowType win_type) :
			this (string.Empty, win_type)
		{
		}
		
		public CustomWindow () : this (string.Empty)
		{
		}
		
		public CustomWindow (string title) : 
			this (title, WindowType.Toplevel)
		{
		}
		
		public CustomWindow (string title, Gtk.WindowType win_type) : base (win_type)
		{
			base.Title = title;
			base.Resize (800, 600);
			base.WindowPosition = WindowPosition.Center;
			
			vbox = new VBox ();
			base.Add (vbox);
		}
		
		protected override bool OnDeleteEvent (Gdk.Event args)
		{
			Application.Quit ();
			return false;
		}
		
		public VBox VBox {
			get {
				return vbox;
			}
		}
	}
}
