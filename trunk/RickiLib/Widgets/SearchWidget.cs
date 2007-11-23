
using System;
using System.Collections;
using Gtk;

namespace RickiLib.Types
{
	public class SearchWidget : Gtk.ScrolledWindow {
		private int findColumn = 0;
		private int retColumn = 0;
		private string [] columns_str;
	
		private Gtk.TreeView view;
		private Gtk.ListStore store;

		private ArrayList columns;
		private ArrayList renderers;
		private ArrayList list;
		
		public EventHandler ColumnChanged;

		public SearchWidget (string [] columns_str)
		{
			findColumn = -1;
			retColumn = -1;
			this.columns_str = new string [columns_str.Length + 1];
			
			//for (int i=0; i < columns_str.Length; i++)
			//	this.columns_str [i] = columns_str [i];
			//this.columns_str [columns_str - 1] = "#FFFFFF"

			Type [] types = System.Type.GetTypeArray (columns_str);
			
			store = new ListStore (types);
			view = new TreeView ();
			view.Model = store;
			view.RulesHint = true;
			
			columns = new ArrayList ();
			renderers = new ArrayList ();
			list = new ArrayList ();
			
			this.columns_str = columns_str;
			
			for (int i = 0; i < columns_str.Length; i++) {
				Gtk.CellRendererText renderer = new CellRendererText ();
				TreeViewColumn column = view.AppendColumn (columns_str [i], renderer, "text", i);
				column.Clickable = true;
				column.Resizable = true;
				column.Sizing = TreeViewColumnSizing.Autosize;
				column.Clicked += new EventHandler (column_Clicked);
				columns.Add (column);
				renderers.Add (renderer);
			}
			
			Add (view);
		}

		public void Append (params string [] values)
		{
			list.Add (values);
			append (values);
			
			TreeIter iter;
			if (list.Count ==1)
				if (store.GetIterFirst (out iter))
					view.Selection.SelectIter (iter);
				
		}

		public void LoadRegisters (string patron)
		{
			store.Clear ();
			foreach (string [] register in list)
				if (register [FindColumn].ToLower ().IndexOf (patron.ToLower ())> -1)
					append (register);
					
			TreeIter iter;
			
			if (store.GetIterFirst (out iter))
				view.Selection.SelectIter (iter);
		}
		
		private void column_Clicked (object sender, EventArgs args)
		{
			int id = columns.IndexOf (sender);
			
			for (int i = 0; i < renderers.Count; i++) {
				if (id == i)
					((CellRendererText) renderers [i]).CellBackground = "#DDDDDD";
				else
					((CellRendererText) renderers [i]).CellBackground = "#FFFFFF";
			}
			if (FindColumn != id)
				FindColumn = id;
		}

		private void append (params string [] values)
		{
			store.AppendValues (values);
		}
		
		public TreeView TreeView {
			get {
				return view;
			}
		}

		public string Id {
			get {
				//Debug.WriteLine ("Returning : {0}", RetColumn);
				TreeIter  iter;
				if (RetColumn > -1)
					if (view.Selection.GetSelected (out iter))
						return store.GetValue (iter, RetColumn).ToString ();
				return string.Empty;
			}
		}
		
		public int FindColumn {
			get { return findColumn; }
			set { 
				if ((value < columns_str.Length) && (value > -1)) {
					findColumn = value;
					((TreeViewColumn) columns [findColumn]).Click ();
					view.QueueDraw ();
				}
			}
		}
		
		public int RetColumn {
			get { return retColumn; }
			set { 
				if ((value < columns_str.Length) && (value > -1))
					retColumn = value;
			}
		}
	}
}
