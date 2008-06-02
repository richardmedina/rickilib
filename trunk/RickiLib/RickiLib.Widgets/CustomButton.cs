
using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class CustomButton : Gtk.Button
	{
		private Gtk.Label label;
		
		public CustomButton (Gtk.Widget image, string title)
		{
			VBox vbox = new VBox (false, 5);
			
			label = new Label ();
			label.Markup = title;
			
			vbox.PackStart (image, true, false, 0);
			vbox.PackStart (label, true, false, 0);
			base.Child = vbox;
		}
		
		public new string Label {
			get {
				return label.Text;
			}
			set {
				label.Text = value;
			}
		}
		
		public string Markup {
			set {
				label.Markup = value;
			}
		}
	}
}
