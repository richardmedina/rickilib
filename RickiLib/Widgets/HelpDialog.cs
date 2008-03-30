
using System;
using System.IO;
using System.Threading;
using Gtk;
using Gnome;

namespace RickiLib.Widgets
{
	
	public class HelpDialog : Gtk.Dialog
	{
		private Gtk.HTML html;
		private Gtk.ScrolledWindow scrolledWindow;
		
		private string filecontent;
		
		private static string appName = "Unnamed Application";
		
		public HelpDialog (string helpfile)
		{
			base.Title = "Temas de ayuda";
			base.Resize (640, 480);
			base.WindowPosition = Gtk.WindowPosition.Center;
			
			html = new Gtk.HTML ();
			scrolledWindow = new ScrolledWindow ();
	
			filecontent = string.Empty;
			
			try {
				using (StreamReader reader = new StreamReader (helpfile)) {
					filecontent = reader.ReadToEnd ();
				}
			} catch (Exception) {
				filecontent = GetNotFoundString (helpfile);
			}			
			html.LoadFromString (filecontent);
			scrolledWindow.Add (html);
			
			base.VBox.PackStart (scrolledWindow);
			base.VBox.ShowAll ();
			
			base.AddButton (Gtk.Stock.Close, ResponseType.Cancel);
			base.AddButton (Gtk.Stock.Print, ResponseType.Ok);
		}
		private bool canShow = true;
		
		public new ResponseType Run ()
		{
			ResponseType response = ResponseType.Ok;
			
			do {
				if (canShow) {
				response = (ResponseType) base.Run ();
				base.Hide ();

				if (response == ResponseType.Ok) {
					Gnome.PrintJob pj = new Gnome.PrintJob (PrintConfig.Default ());
					Gnome.PrintDialog dialog = new Gnome.PrintDialog (pj, "Help Themes", 0);
					dialog.WindowPosition = Gtk.WindowPosition.Center;

					//string html_content = string.Empty;
					
					// is now (gtk-2.20) removed without advertisment.. :@
					//html.PrintSetMaster (pj);

					//Gnome.PrintContext ctx = pj.Context;
					//html.PrintPage ();

					pj.Close ();

					int print_response = dialog.Run ();
					dialog.Destroy ();

					if (print_response == (int) PrintButtons.Print) {
						pj.Print ();
					} else if (print_response == (int) PrintButtons.Preview) {
						canShow = false;
						GLib.Timeout.Add (50, new GLib.TimeoutHandler (glib_callback));
						PrintJobPreview pjp = new PrintJobPreview (pj, "Help Themes");
						pjp.WindowPosition = WindowPosition.Center;
						pjp.Hidden += new EventHandler (pjp_Hidden);
						pjp.Show ();
					}
				}
				} else
					while (Application.EventsPending ())
						Application.RunIteration ();
			} while (response == ResponseType.Ok);
			
			return response;
		}
		
		private bool glib_callback ()
		{
			return canShow;
		}
		
		private void pjp_Hidden (object sender, EventArgs args)
		{
			canShow = true;
		}
		
		private string GetNotFoundString (string helpFile)
		{
			return string.Format (@"
			<html>
			<body bgcolor='#DDDDFF' text='#A00000'>
			<center>
			<h1> {0} </h1>
			</center>
			<hr>
			<h1>Error</h1>
			The requested topic does not exists.
			</body>
			</html>
			", appName
			);
		}
	}
}
