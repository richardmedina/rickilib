
using System;
using Gtk;
using RickiLib.Widgets;

namespace RickiLib.Widgets
{
	
	
	public class DateEntry : SearchEntry
	{
		private DateTime date;
		
		
		public DateEntry () : this (DateTime.Now)
		{
		}
		
		public DateEntry (DateTime date)
		{
			this.date = date;
			this.Entry.Xalign = 0.5f;
			base.Entry.IsEditable = false;
			base.Entry.Text = this.date.ToLongDateString ();
		}
		
		protected override void OnImageFindClicked (Gdk.EventButton event_button)
		{
			SelectDateDialog dialog = new SelectDateDialog ();
			if (dialog.Run () == (int) ResponseType.Ok) {
				this.Date = dialog.Date;	
			}
			
			dialog.Destroy ();
		}
		
		public DateTime Date {
			get {
				return date;
			}
			set {
				date = value;
				base.Entry.Text = date.ToLongDateString ();
			}
		}
		
	}
}
