
using System;
using System.Collections;
using Gtk;
using RickiLib.Types;

namespace RickiLib.Widgets
{
	public class SearchDialog : CustomDialog {
	
		private string [] columns;
		
		private Gtk.Entry entryFind;
		private SearchWidget findList;
		
		public SearchDialog (params string [] columns)
		{
			Resizable = true;
			this.columns = columns;

			findList = new SearchWidget (columns);
			findList.TreeView.ButtonPressEvent += new ButtonPressEventHandler (view_ButtonPressEvent);
			findList.TreeView.Selection.Changed += new EventHandler (view_Selection_Changed);
			
			entryFind = new Entry ();
			entryFind.Changed += new EventHandler (entryFind_Changed);
			entryFind.Activated += new EventHandler (entryFind_Activated);
			
			HBox hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Buscar"), false, false, 0);
			hbox.PackStart (entryFind);
			
			VBox.PackStart (hbox, false, false, 0);

			VBox.PackStart (findList);
			VBox.Spacing = 5;
			VBox.ShowAll ();
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
			SetResponseSensitive (ResponseType.Ok, false);
		}

		public void Append (params string [] values)
		{
			findList.Append (values);
		}

		
		private void entryFind_Changed (object sender, EventArgs args)
		{
			if ((FindColumn ==-1) || (RetColumn ==-1) || (FindColumn > columns.Length) || (RetColumn > columns.Length))
				return;
			
			findList.LoadRegisters (entryFind.Text);
		}
		
		private void entryFind_Activated (object sender, EventArgs args)
		{
			TreeIter iter;
			if (findList.TreeView.Selection.GetSelected (out iter))
				Respond (ResponseType.Ok);
		}
				
		[GLib.ConnectBefore]
		private void view_ButtonPressEvent (object sender, ButtonPressEventArgs args)
		{
			TreeIter iter;
			if ((args.Event.Type == Gdk.EventType.TwoButtonPress) && (args.Event.Button == 1))
				if (findList.TreeView.Selection.GetSelected (out iter))
					Respond (ResponseType.Ok);
		}
		
		private void view_Selection_Changed (object sender, EventArgs args)
		{
			TreeIter iter;
			
			SetResponseSensitive (ResponseType.Ok, findList.TreeView.Selection.GetSelected (out iter));
		}
		
		public new string Title {
			get { return base.Title; }
			set { base.Title = value; }
		}
		
		public string Id {
			get {
				return findList.Id;	
			}
		}
		
		public int FindColumn {
			get {
				return findList.FindColumn;
			}
			set {
				findList.FindColumn = value;
			}
		}
		
		public int RetColumn {
			get {
				return findList.RetColumn;
			}
			set {
				findList.RetColumn = value;
			}
		}
	}
}
