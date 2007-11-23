
using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class TabulatorSection : Gtk.EventBox
	{
		private Gtk.VBox vbox;
		private Gtk.VBox main_vbox;
		private TabulatorSectionTitle title;
		private Gtk.HButtonBox action_area;
		
		public TabulatorSection (string text)
		{
			vbox = new VBox (false, 5);
			vbox.BorderWidth = 5;
			
			main_vbox = new VBox (false, 2);
			main_vbox.BorderWidth = 5;
			
			title = new TabulatorSectionTitle (text);
			
			action_area = new HButtonBox ();
			action_area.Spacing = 5;
			action_area.Layout = ButtonBoxStyle.End;
			
			vbox.PackStart (title, false, false, 0);
			vbox.PackStart (main_vbox, true, true, 5);
			vbox.PackEnd (action_area, false, false, 0);
			
			base.Add (vbox);
		}
		
		public TabulatorSectionTitle Title {
			get {
				return title;
			}
		}
		
		public Gtk.VBox VBox {
			get {
				return main_vbox;
			}
		}
		
		public Gtk.HButtonBox ActionArea {
			get {
				return action_area;
			}
		}
	}
}
