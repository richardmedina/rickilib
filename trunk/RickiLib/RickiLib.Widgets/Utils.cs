
using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public static class Utils
	{
		public static void TableShrinkAttach (Gtk.Table table, Gtk.Widget widget, uint col, uint row)
		{
			table.Attach (widget, col, col +1, row, row +1, 
				AttachOptions.Shrink , AttachOptions.Shrink,
				0, 0);
		}
		
		public static void TableFillAttach (Gtk.Table table, Gtk.Widget widget, uint col, uint row)
		{
			table.Attach (widget, col, col + 1, row, row +1,
				AttachOptions.Fill | AttachOptions.Expand, AttachOptions.Shrink,
				0, 0);
		}
		
		public static void RunOnGtkThread (ReadyEvent re)
		{
			new ThreadNotify (re).WakeupMain ();
		}
		
	}
}
