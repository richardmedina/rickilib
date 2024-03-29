
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using Gtk;

using RickiLib.Types;
namespace RickiLib.Widgets
{


	public class DataSetView : Gtk.TreeView
	{
		private Gtk.ListStore _store;
		private Gtk.TreeViewColumn [] _columns = new Gtk.TreeViewColumn [0];
		private Gtk.CellRendererText [] _renders = new Gtk.CellRendererText [0];
		
		private DataSet _dataset;
		
		private string _current_filter = string.Empty;
		public int ColumnId = 0;
		
		public bool AutoSelectable = false;
		
		public event EventHandler Activated;
		
		public bool ViewResponsiveLoading = false;
		
		public string Title = string.Empty;
		
		public bool AllowCopyShortcut = true;
		
		public DataSetView ()
		{
			Activated = onActivated;
			AutoSelectable = true;
			Selection.Mode = SelectionMode.Multiple;
		}
		
		public virtual void SendActivated ()
		{
			Gtk.TreeIter iter;
			if (Selection.GetSelected (out iter))
				OnActivated ();
		}
		
		public virtual void New ()
		{
		}
		
		public virtual void RemoveSelected ()
		{
			TreeIter iter;
			
			if (Selection.GetSelected (out iter)) {
				TreeIter iter_for_removing = iter;
				Store.IterNext (ref iter);
				Store.Remove (ref iter_for_removing);
				if (AutoSelectable)
					if (Store.IterIsValid (iter))
						Selection.SelectIter (iter);
			}
		}
		
		public virtual void EditSelected ()
		{
		}
		
		public virtual void Load ()
		{
		}
		
		public virtual void LoadDataSet (DataSet dataset)
		{
			
			foreach (TreeViewColumn col in Columns) {
				RemoveColumn (col);
			}
					
				_dataset = dataset;
				Type [] types = new Type [dataset.Tables [0].Columns.Count];
			
				_renders = new CellRendererText [types.Length];
				_columns = new TreeViewColumn [types.Length];
			
				for (int i = 0; i < types.Length; i ++) {
					types [i] = typeof (string);
					_renders [i] = new CellRendererText ();
					_columns [i] = new TreeViewColumn (dataset.Tables [0].Columns [i].Caption, 
				                                   _renders [i], "text", i);
				}
			
				_store = new ListStore(types);
			
				for (int i =0; i < types.Length; i ++)
					AppendColumn (_columns [i]);
			
				Model = _store;
		}
		static int counter = 0;
		
		public virtual bool OnRowAdd (string [] fields)
		{
			if (CurrentFilter.Length == 0) {
				AddRow (fields);
			}
			else
				for (int i = 0; i < fields.Length; i ++) {
					if (Columns [i].Visible)
						if (fields [i].ToLower ().IndexOf (CurrentFilter) > -1)
							AddRow (fields);
				}
			
			if (ViewResponsiveLoading)
				while (Application.EventsPending ())
					Application.RunIteration ();
			
			return true;
		}
		
		public virtual void AddRow (string [] row)
		{
			_store.AppendValues (row);
		}
			
		public virtual int Populate ()
		{
			_store.Clear ();
			int count = 0;
			
			foreach (DataRow row in _dataset.Tables [0].Rows) {
				string [] fields = new string [_dataset.Tables [0].Columns.Count]	;
				
				for (int i = 0; i < fields.Length; i ++) {
						fields [i] = row [i].ToString ();
				}
				
				if (OnRowAdd (fields))
					count ++;
			}
			
			Gtk.TreeIter iter;
			
			if (AutoSelectable) {
				if (Store.GetIterFirst (out iter))
					Selection.SelectIter (iter);
			}
			
			return count;
		}
		
		private class StringCollection : List<string> 
		{	
		}
		
		private class Intcollection : List<int> 
		{	
		}
		
		private string filecontent_header_get (FileType filetype)
		{
			Assembly _assembly = Assembly.GetExecutingAssembly();
			/*
			foreach (string filename in _assembly.GetManifestResourceNames()) {
				Console.WriteLine (filename);	
			}
			*/
			Stream stream = _assembly.GetManifestResourceStream ("ExcelTypeDefinition.xml");
			
			string content = string.Empty;
			
			using (StreamReader reader = new StreamReader (stream)) {
				content = reader.ReadToEnd ();
			}
			
			return content;
		}
		
		private string new_cell_data (params string [] fields)
		{
			string row = "   <Row ss:AutoFitHeight=\"0\">\r\n";
			
			foreach (string field in fields) {
				row += string.Format ("    <Cell><Data ss:Type=\"String\">{0}</Data></Cell>\r\n",
				                      field);
			}
			row += "   </Row>\r\n";
			
			return row;
		}
		
		public void Export (string filename, FileType filetype, string author, DateTime created_date)
		{	
			Gtk.TreeIter iter;
			
			StringBuilder strings = new StringBuilder ();
			StringCollection strs = new StringCollection ();
			Intcollection indexes = new Intcollection ();
			
			for (int i = 0; i < Columns.Length; i ++) {
				if (Columns [i].Visible) {
					strs.Add (Columns [i].Title);
					indexes.Add (i);
				}
			}
			
			strings.Append (new_cell_data(strs.ToArray ()));
			
			if (Store.GetIterFirst (out iter))
				do {
					string [] row;
					if (GetRow (iter, out row)) {
						StringCollection fields = new StringCollection ();
						foreach (int index in indexes) {
							fields.Add (row [index]);
						}
						strings.Append (new_cell_data (fields.ToArray ()));
					}
				} while (Store.IterNext (ref iter));
			
			using (StreamWriter writer = new StreamWriter (filename)) {
				string content = filecontent_header_get (filetype);
				content = content.Replace ("${AUTHOR}", author).Replace ("${CREATED_DATE}", created_date.ToString ("yyyy-MM-ddTdhh:mm:ss.fff")).Replace ("${ROWS}", strings.ToString ());
				writer.Write (content);
			}
		}
		
		public void CopyToClipboard ()
		{
			Clipboard clipboard = Clipboard.GetForDisplay (Display, Gdk.Atom.Intern ("CLIPBOARD", false));
			//string buffer = string.Empty;
			
			DataSetRowCollection rows = GetSelectedRows ();
			StringBuilder buffer = new StringBuilder ();
			
			for (int i = 0; i < rows.Count; i ++) {
				string [] fields = rows [i];
				for (int j = 0; j < fields.Length; j ++) {
					buffer.Append (fields [j]);
					if (j < fields.Length -1)
						buffer.Append ("\t");
				}
				buffer.Append ("\n");
			}
			clipboard.Text = buffer.ToString ();
		}
		
		protected virtual void OnActivated ()
		{
			Activated (this, EventArgs.Empty);
		}
		
		protected override bool OnKeyPressEvent (Gdk.EventKey evnt)
		{
			if ((evnt.State & Gdk.ModifierType.ControlMask) > 0)
				if (evnt.Key == Gdk.Key.C || evnt.Key == Gdk.Key.c)
					CopyToClipboard ();
			if (evnt.Key == Gdk.Key.Return || evnt.Key == Gdk.Key.KP_Enter) {
				OnActivated ();
			}
			
			if (evnt.Key == Gdk.Key.Meta_R)
				OnPopupMenu ();
			
			return base.OnKeyPressEvent (evnt);
		}
		
		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			if (evnt.Button == 1 && evnt.Type == Gdk.EventType.TwoButtonPress) {
				
				Gtk.TreeIter iter;
				Gtk.TreePath path;
				
				if (GetPathAtPos ((int) evnt.X, (int) evnt.Y, out path)) {
					if (_store.GetIterFromString (out iter, path.ToString ()))
					    OnActivated ();
				}
			}
			
			if (evnt.Button == 3) {
				OnPopupMenu ();	
			}
			
			return base.OnButtonPressEvent (evnt);
		}
				
		public bool GetSelected (out string [] fields)
		{
			
			fields = null;
			Gtk.TreeIter iter;
			Gtk.TreePath [] paths;
			
			paths = Selection.GetSelectedRows ();
			if (paths.Length == 1) {
				if (Store.GetIterFromString (out iter, paths [0].ToString ()))
					if (GetRow (iter, out fields))
						return true;
			}
			
			return false;
			
			
			//return GetSelectedAtPointer (out fields);
		}
		
		public DataSetRowCollection GetSelectedRows ()
		{
			TreePath [] paths = Selection.GetSelectedRows ();
			
			DataSetRowCollection rows = new DataSetRowCollection ();
			
			for (int i = 0; i < paths.Length; i ++) {
				
				Gtk.TreeIter iter;
				string [] row;
				if (Store.GetIterFromString (out iter, paths [i].ToString ()))
					if (GetRow (iter, out row))
						rows.Add (row);
			}
			
			return rows;
		}
		
		public bool GetSelectedAtPointer (out string [] fields)
		{
			bool result = false;
			int x, y;
			Gdk.ModifierType modifier;
			Gtk.TreePath path;
			Gtk.TreeIter iter;
			
			fields = null;
			
			GdkWindow.GetPointer (out x, out y, out modifier);
			
			if (GetPathAtPos (x, y, out path)) {
				if (Store.GetIterFromString (out iter, path.ToString ())) {
					if (GetRow (iter, out fields))
					    result = true;
				}
			}
			
			return result;
		}
		
		public bool GetRow (Gtk.TreeIter iter, out string [] fields)
		{
			fields = new string [Renders.Length];
			
			if (!Store.IterIsValid (iter))
				return false;
			
			for (int i = 0; i < fields.Length; i ++)
				fields [i] = (string) Store.GetValue (iter, i);
			
			return true;
		}
		
		public bool GetLastRow (out string [] fields)
		{
			Gtk.TreeIter iter;
			Gtk.TreeIter last_iter = TreeIter.Zero;
			fields = null;
			
			if (Store.GetIterFirst (out iter))
				do {
					last_iter = iter;
				} while (Store.IterNext (ref iter));
			
			if (GetRow (last_iter, out fields)) {
					return true;
			}
			
			return false;
		}
		
		private int sort_func (TreeModel model, TreeIter a, TreeIter b)
		{
			int sort_id;
			SortType sort_type;
			
			//Console.WriteLine ("Sorting...");
			if (_store.GetSortColumnId (out sort_id, out sort_type)) {
				string astr = (string) _store.GetValue (a, sort_id);
				string bstr = (string) _store.GetValue (b, sort_id);
				
				if (sort_id == 1)
					return byInt (astr, bstr) * -1;
				if (sort_id == 2)
					return byString (astr, bstr) * -1;
				if (sort_id == 3)
					return byCurrency (astr, bstr) * -1;
				if (sort_id == 4)
					return byDateTime (astr, bstr) * -1;
				
			}
			
			return 0;
		}
		
		private int byDateTime (string a, string b)
		{
			DateTime adate = DateTime.Parse (a);
			DateTime bdate = DateTime.Parse (b);
			
			if (adate > bdate)
				return -1;
			if (bdate > adate)
				return 1;
			
			return 0;
		}
		
		private int byCurrency (string a, string b)
		{
			a = a.Replace ("$", string.Empty).Replace ("(", string.Empty).Replace (")", string.Empty).Replace (",", string.Empty);
			b = b.Replace ("$", string.Empty).Replace ("(", string.Empty).Replace (")", string.Empty).Replace (",", string.Empty);
			
			double adouble = double.Parse (a);
			double bdouble = double.Parse (b);
			if (adouble == bdouble)
				return 0;
			if (adouble > bdouble)
				return -1;
			
			return 1;
		}
		
		private int byString (string a, string b)
		{
			return string.Compare (a, b);	
		}
		
		private int byInt (string a, string b)
		{
			int aint = int.Parse (a);
			int bint = int.Parse (b);
				
			if (aint == bint)
				return 0;
			if (aint > bint)
				return -1;
			
			return 1;
		}
				
		private void onActivated (object sender, EventArgs args)
		{
		}
		
		public new TreeViewColumn [] Columns {
			get { return _columns; }
		}
		
		public CellRendererText [] Renders {
			get { return _renders; }
		}
		
		public ListStore Store {
			get { return _store; }
		}
		
		public DataSet Dataset {
			get { return _dataset; }
		}
		
		public string CurrentFilter {
			get { return _current_filter; }
			set { 
				if (_current_filter != value.ToLower ()) {
					_current_filter = value.ToLower (); 
					//if (_current_filter.Length > 2)
					Populate ();
				}
			}
		}
	}
}
