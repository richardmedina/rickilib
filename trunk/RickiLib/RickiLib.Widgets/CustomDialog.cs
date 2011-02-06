
using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class CustomDialog : Gtk.Dialog {
		
		private EventHandler _help_request;
		private Button buttonHelp;
		
		public CustomDialog ()
		{	
			base.WindowPosition = WindowPosition.CenterOnParent;
			base.BorderWidth = 5;
			base.VBox.Spacing = 5;
			base.Resize (640, 480);
			base.Resizable = false;
			
			//_help_clicked = onHelpClicked;
			_help_request = onHelpRequest;
			
			buttonHelp = (Gtk.Button) base.AddButton (Stock.Help, (int) ResponseType.Help);
		}
		
		public new virtual ResponseType Run ()
		{
			ResponseType response;
			string message;
			
			do {
				response = (ResponseType) base.Run ();
				if (response == ResponseType.Help)
					OnHelpRequest ();
				
				if (response == ResponseType.Ok)
						if (!OnValidate (out message)) {
							MessageDialog msg = new MessageDialog (Globals.MainWindow,
					                                       DialogFlags.Modal, MessageType.Error,
					                                       ButtonsType.Ok, message);
							msg.Title = Globals.FormatWindowTitle ("Error");
							msg.Run ();
							msg.Destroy ();
							response = ResponseType.Help;
							continue;
						}
			}while (response == ResponseType.Help);
			
			return response;
		}
		
		protected virtual bool OnValidate (out string message)
		{
			message = string.Empty;
			return true;
		}
		
		protected virtual void OnHelpRequest ()
		{
			HelpDialog dialog = new HelpDialog();
			dialog.Run ();
			dialog.Destroy ();
			_help_request (this, EventArgs.Empty);	
		}
		
		private void onHelpRequest (object sender, EventArgs args)
		{	
		}
	}
}