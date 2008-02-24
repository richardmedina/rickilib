
using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class LinkWidget : Gtk.EventBox
	{
		private Gtk.Label label;
		private string text;
		private Gdk.Cursor cursorOver;
		
		public event EventHandler Clicked;
		
		public LinkWidget (string text)
		{
			this.text = text;
			this.Clicked += new EventHandler (onClicked);
			
			cursorOver = new Gdk.Cursor (Gdk.CursorType.Hand2);
			
			label = new Label ();
			updateLabelText ();
			base.Add (label);
		}
		
		protected override bool OnEnterNotifyEvent (Gdk.EventCrossing args)
		{
			base.GdkWindow.Cursor = cursorOver;
			return true;		
		}

		protected override bool OnButtonReleaseEvent (Gdk.EventButton args)
		{
			if (args.Button == 1) {
				Clicked (this, EventArgs.Empty);
			}
			
			return false;
		}
		
		private void onClicked (object sender, EventArgs args)
		{
			// Nothing for default handler
		}
		
		private void updateLabelText ()
		{
			label.Markup = string.Format (
				"<span font_desc='8'><b>{0}</b></span>", 
				text
				);
		}
		
		public string Text {
			get {
				return text;
			}
			set {
				text = value;
				updateLabelText ();
			}
		}
	}
}
