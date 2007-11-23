
using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	public class AuthenticationDialog : CustomDialog
	{
		private static readonly string helpSection = "authentication";
		
		private AuthenticationWidget authWidget;
		
		public AuthenticationDialog ()
		{
			base.Title = "Iniciar Sesión";
			base.Resize (320, 240);
			base.VBox.Spacing = 5;
			
			authWidget = new AuthenticationWidget ();
			authWidget.UsernameEntry.Changed += entry_Changed;
			authWidget.PasswordEntry.Changed += entry_Changed;
			authWidget.PasswordEntry.Activated += entryPassword_Activated;
			
			base.VBox.PackStart (authWidget);
			base.VBox.ShowAll ();
			//base.AddButton (Stock.Help, ResponseType.Help);
			base.AddButton (Stock.Cancel, ResponseType.Cancel);
			base.AddButton (Stock.Ok, ResponseType.Ok);
			
			base.SetResponseSensitive (ResponseType.Ok, false);
			
			base.ActionArea.ShowAll ();
		}

		public new ResponseType Run ()
		{
			ResponseType response;
			
			do {
				response = (ResponseType) base.Run ();
				if (response == ResponseType.Help) {
					HelpDialog hd = new HelpDialog (helpSection);
					hd.Run ();
					hd.Destroy ();
				}
			} while (response == ResponseType.Help);
			
			return response;
		}
		
		private void entry_Changed (object sender, EventArgs args)
		{
			base.SetResponseSensitive (ResponseType.Ok, 
					((authWidget.UsernameEntry.Text.Trim ().Length > 0) && 
					(authWidget.PasswordEntry.Text.Trim ().Length > 0)));
					
					
		}
		
		private void entryPassword_Activated (object sender, EventArgs args)
		{
			if (authWidget.UsernameEntry.Text.Trim ().Length > 0 && authWidget.PasswordEntry.Text.Trim ().Length > 0)
				base.Respond (ResponseType.Ok);
		}
	}
}
