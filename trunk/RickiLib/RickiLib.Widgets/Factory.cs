
using System;
using System.IO;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public static class Factory
	{
		public static Gtk.Label Label (string text)
		{
			Gtk.Label label = new Gtk.Label ();
			label.Markup = text;
			
			return label;
		}
		
		public static Gtk.Label Label (string text, int width, 
			Justification hjustify, Justification vjustify)
		{
			Gtk.Label label = Factory.Label(text);

			if (width > -1)
				label.WidthRequest = width;

			float halign = 0f;
			float valign = 0.5f;
			
			
			if (hjustify == Justification.Center)
				halign = 0.5f;
			else if (hjustify == Justification.Right)
				halign = 1f;
			 
			if (vjustify == Justification.Left)
			 	valign = 0f;
			else if (vjustify == Justification.Right)
			 	valign = 1f;
			 
			label.SetAlignment (halign, valign);
			
			return label;
		}
		
		public static Gtk.Label Label (string text, int width, Justification justify)
		{			
			return Factory.Label (text, width, justify, Justification.Center);
		}
		
		public static Gtk.Button Button (string stock_icon, string text)
		{
			Gtk.Button button = new Gtk.Button ();
			
			Gtk.Image image = new Gtk.Image (stock_icon, IconSize.Button);
			
			HBox hbox = new HBox (false, 5);
			hbox.PackStart (image, false, false, 0);
			hbox.PackStart (Label (text), false, false, 0);
			
			button.Child = hbox;
			
			return button;
			
		}
		
		public static Gtk.Button Button (Gdk.Pixbuf pixbuf, string text)
		{
			Gtk.Button button = new Gtk.Button ();
			
			Gtk.Image image = new Gtk.Image (pixbuf);
			
			HBox hbox = new HBox (false, 5);
			hbox.PackStart (image, false, false, 0);
			hbox.PackStart (Label (text), false, false, 0);
			
			button.ShowAll ();
			
			return button;
		}
/*		
* DEPRECATED *
		public static Gdk.Pixbuf ImageToPixbuf(System.Drawing.Image image)
		{
			using (MemoryStream stream = new MemoryStream ()) {
				image.Save (stream, System.Drawing.Imaging.ImageFormat.Png);
				stream.Position = 0;
				return new Gdk.Pixbuf (stream);
			}
 		}
*/
	}
}
