
using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class CustomDialog : Gtk.Dialog {
		private Button buttonHelp;
		
		public CustomDialog ()
		{	
			base.WindowPosition = WindowPosition.CenterOnParent;
			base.BorderWidth = 5;
			base.VBox.Spacing = 5;
			base.Resize (640, 480);
			base.Resizable = false;
			
			buttonHelp = (Gtk.Button) base.AddButton (Stock.Help, (int) ResponseType.Help);
		}
		
		public new ResponseType Run ()
		{
			ResponseType response;
			do {
				response = (ResponseType) base.Run ();
				if (response == ResponseType.Help) {
					base.Hide ();
					HelpDialog helpDialog = new HelpDialog (HelpSection);
					helpDialog.Run ();
					helpDialog.Destroy ();
				}
			} while (response == ResponseType.Help);
			return response;
		}
		
		protected Button ButtonHelp {
			get {
				return buttonHelp;
			}
		}
		
		protected virtual string HelpSection {
			get {
				return "home";
			}
		}
	
	}
}
