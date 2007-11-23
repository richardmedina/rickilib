
using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class TabulatorSectionTitle : Gtk.EventBox
	{
		private Gtk.HBox hbox;
		private Gtk.Label label;
		private string text;
		
		public TabulatorSectionTitle () : this (string.Empty)
		{
		}
		
		public TabulatorSectionTitle (string title) 
		{
			base.HeightRequest = 20;
			
			this.text = title;
			hbox = new HBox (false, 0);
			
			label = Factory.Label (
				string.Format ("<span font_desc='14'>{0}</span>", 
					text)
			);
						
			hbox.PackStart (label, false, false, 10);
			
			base.Add (hbox);
		}
		
		public string Text {
			get {
				return text;
			}
			set {
				text = value;
				label.Markup = string.Format ("<b>{0}</b>", text);
			}
		}
	}
}
