
using System;
using Gtk;

using RickiLib.Types;

namespace RickiLib.Widgets
{
	
	
	public abstract class INotebookPage
	{
		private string title;
		private int position;
		private bool closable;
		
		private Gtk.Widget titleWidget;
		
		public event CloseEventHandler Close;
		
		public INotebookPage ()
		{
			title = string.Empty;
			position = -1;
			closable = true;
			
			Close = onClose;
		}
	
		public string Title { 
			get { return title; }
			set { title = value; }
		}
		
		protected virtual void OnClose (bool do_close)
		{
			Close (this, new CloseEventArgs (do_close));
		}
		
		public int Position { 
			get { return position; } 
			set { position = value; }
		}
		//event EventHandler Activated { get; }
		
		public bool Closable { 
			get { return closable; }
			set { closable = false; }
		}
		
		public abstract Gtk.Widget Widget { get; }
		
		public virtual Gtk.Widget TitleWidget { 
			get { return titleWidget; }
			set { titleWidget = value; }
		}
		
		private void onClose (object sender, CloseEventArgs args)
		{
		}
	}
}
