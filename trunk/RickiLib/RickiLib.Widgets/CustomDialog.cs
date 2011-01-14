
using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class CustomDialog : Gtk.Dialog {
		
		private EventHandler _help_clicked;
		private Button buttonHelp;
		
		public CustomDialog ()
		{	
			base.WindowPosition = WindowPosition.CenterOnParent;
			base.BorderWidth = 5;
			base.VBox.Spacing = 5;
			base.Resize (640, 480);
			base.Resizable = false;
			
			_help_clicked = onHelpClicked;
			
			buttonHelp = (Gtk.Button) base.AddButton (Stock.Help, (int) ResponseType.Help);
		}
		
		public virtual new ResponseType Run ()
		{
			ResponseType response;
			do {
				response = (ResponseType) base.Run ();
				
			} while (response == ResponseType.Help);
			
			return response;
		}
		
		protected virtual void OnHelpClicked ()
		{
			_help_clicked (this, EventArgs.Empty);
		}
		
		private void onHelpClicked (object sender, EventArgs args)
		{
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
	
		public event EventHandler HelpClicked {
			add { _help_clicked += value; }
			remove { _help_clicked -= value; }
		}
	}
}
