// AuthenticationWidget.cs created with MonoDevelop
// User: ricki at 04:55 p 10/10/2007
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class AuthenticationWidget : Gtk.VBox
	{
		private Gtk.Entry entryUsername;
		private Gtk.Entry entryPassword;
//		private Gtk.Button buttonOk;
//		private Gtk.Button buttonHelp;
		
		public AuthenticationWidget()
		{
			base.Spacing = 5;
			this.entryUsername = new Entry ();
			this.entryUsername.Activated += 
				delegate { 
					if (entryUsername.Text.Trim ().Length > 0) 
						entryPassword.GrabFocus (); 
				};
			
			this.entryPassword = new Entry ();
			this.entryPassword.Visibility = false;
			
			//this.buttonOk = new Gtk.Button (Stock.Ok);
			//this.buttonHelp = new Gtk.Button (Stock.Help);
			
			Gtk.Image image = Image.LoadFromResource ("dialog_auth_icon.png");
			Gtk.HBox hbox = new HBox (false, 0); 
			
			hbox.PackStart (image, false, false, 0);
			hbox.PackStart (Factory.Label ("<span font_desc='14'><b>Introduzca su nombre de \nusuario y contraseña</b></span>\n" +
						"Para Acceder a la aplicación necesita \nproporcionar su nombre de usuario y su contraseña"));
			
			base.PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackEnd (entryUsername, false, false, 0);
			hbox.PackEnd (Factory.Label ("Usuario"), false, false, 0);			
			base.PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackEnd (entryPassword, false, false, 0);
			hbox.PackEnd (Factory.Label ("Contraseña"), false, false, 0);			
			base.PackStart (hbox, false, false, 0);
		}
		
		public Gtk.Entry UsernameEntry {
			get { return this.entryUsername; }
		}
		
		public Gtk.Entry PasswordEntry {
			get { return this.entryPassword; }
		}
	}
}
