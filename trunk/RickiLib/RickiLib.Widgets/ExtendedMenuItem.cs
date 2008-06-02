// ExtendedMenu.cs created with MonoDevelop
// User: ricki at 12:28 11/21/2007
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class ExtendedMenuItem : Gtk.ImageMenuItem
	{
		private Gtk.Label label;
		private Gtk.Image image;
		
		public ExtendedMenuItem (Gdk.Pixbuf pixbuf, 
			string text, bool useMarkup) :
				this (new Gtk.Image (pixbuf), text, useMarkup)
		{
		}
		
		public ExtendedMenuItem () : this (string.Empty)
		{
		}

		public ExtendedMenuItem (string text) : this ((Gtk.Image) null, text, false) 
		{
		}
		
		public ExtendedMenuItem  (Gtk.Image image, 
			string text, 
			bool useMarkup)
		{
			label = new Label (text);
			label.UseMarkup = useMarkup;
			label.SetAlignment (0f, 0.5f);
			
			this.image = image;
			
			base.Image = image;
			base.Child = label;
		}
		
		public Gtk.Label Label {
			get { return label; }
		}
		
		public new Gtk.Image Image {
			get { return image; }
		}
		
		public bool UseMarkup {
			get { return label.UseMarkup; }
			set { label.UseMarkup = value; }
		}
		
		public string Text {
			get { return label.Text; }
			set {
				if (UseMarkup)
					label.Markup = value;
				else
					label.Text = value;
			}
		}
	}
}
