
using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class SelectDateDialog : Gtk.Dialog
	{
		private Gtk.Calendar calendar;
		
		public SelectDateDialog()
		{
			calendar = new Calendar ();
			calendar.DaySelectedDoubleClick += 
				calendar_DaySelectedDoubleClick;
			base.VBox.PackStart (calendar);
			base.VBox.ShowAll ();
			
			base.AddButton (Stock.Cancel, ResponseType.Cancel);
			base.AddButton (Stock.Ok, ResponseType.Ok);
		}
		
		public DateTime Date {
			get {
				return calendar.Date;
			}
			set {
				calendar.Date = value;
			}
		}
		
		private void calendar_DaySelectedDoubleClick (object sender,
			EventArgs args)
		{
			base.Respond (ResponseType.Ok);
		}
	}
}
