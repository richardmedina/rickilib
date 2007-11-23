
using System;
using Gtk;
using RickiLib.Types;
namespace RickiLib.Widgets
{
	
//	FIXME: must use ModuleCollectionEvent for thrown selections
	
	public class TreeModuleChooser : Gtk.ScrolledWindow
	{
		private Gtk.TreeView view;
		private Gtk.TreeStore store;
		
		public event ModuleCollectionEvent SelectionChanged;
		
		private static Gdk.Pixbuf pixbuf_folder = 
			Gdk.Pixbuf.LoadFromResource ("folder.png");
		private static Gdk.Pixbuf pixbuf_module = 
			Gdk.Pixbuf.LoadFromResource ("module.png");
		
		private ModuleCollection collection;
		
		public TreeModuleChooser (ModuleCollection tabcol)
		{
			SelectionChanged = onSelectionChanged;
			
			store = new TreeStore (
				typeof (Gdk.Pixbuf), // icon
				typeof (string), // text
				typeof (int) // instance
			);
			
			view = new TreeView (store);
			view.ButtonPressEvent += view_ButtonPressEvent;
			view.Shown += tree_Shown;
			view.SearchColumn = 1;
			view.EnableSearch = true;
			//view.HeadersVisible = false;

			TreeViewColumn col = new TreeViewColumn ();
			col.Title = "Módulos";
			
			Gtk.CellRenderer r = new CellRendererPixbuf ();
			
			col.PackStart (r, false);
			
			col.AddAttribute (r,
				"pixbuf", 
				0);
			
			r = new CellRendererText () ;
			
			col.PackStart (r, true);
			col.AddAttribute (
				r,
				"text",
				1);
			
			view.AppendColumn (col);
			
			base.Add (view);
			
			collection = tabcol;
			collection.Added += collection_Added;
			collection.Removed += collection_Removed;
			
			loadFromCollection ();
		}
				
		public Gtk.TreeIter Add (Gtk.TreeIter iter, string category)
		{
			TreeIter new_iter;
			
			if (store.IterIsValid (iter))
				new_iter = store.AppendValues (
					iter,
					pixbuf_folder, category, 
					0);
			else
				new_iter = store.AppendValues (
					pixbuf_folder,
					category, 
					0);
			
			return new_iter;
		}
		
		public void Add (Module page)
		{	
			if (page.Pixbuf == null)
				page.Pixbuf = pixbuf_module;
			
			if (page.Path.Length == 0)
				store.AppendValues (page.Pixbuf, 
					page.Title, 
					page.Instance);
			else {
				string [] path = page.Path.Split (
					"/".ToCharArray ());

				Gtk.TreeIter iter;

				if (!store.GetIterFirst (out iter)) {
					foreach (string cat in path)
						iter = Add (iter, cat);
			
					store.AppendValues (
						iter,
						page.Pixbuf,
						page.Title,
						page.Instance);
				} 
				else
				for (int i = 0; i < path.Length; i ++) {
					while (i < path.Length && 
					 	SearchCategory (ref iter,path [i])) {
					 	i ++;
					}
					
					if (i == 0) {
						iter = this.Add (
							TreeIter.Zero,
							path [i ++]
						);
					}
						
					if (i < path.Length) {
						do {
							iter = this.Add (
								iter,
								path [i]);
						} while (++i < path.Length);
					}
						
					store.AppendValues (
						iter,
						page.Pixbuf,
						page.Title,
						page.Instance
					);
				}
			}
		}
		
		protected virtual bool SearchCategory (ref TreeIter iter, 
			string category_name )
		{
			TreeIter iter_original = iter;
			
			TreeIter child;
			
			do {
				string t = (string) store.GetValue (iter, 1);
				int i = (int) store.GetValue (iter, 2);
				
				if (i == 0 && t == category_name) {
					return true;
				}
				
				if (store.IterChildren (out child, iter))
				do {
					string text = (string) store.GetValue (child, 1);
					int instance = (int) store.GetValue (child, 2);
					
					if (instance == 0 && text == category_name) {
						iter = child;
						return true;
					}
				} while (store.IterNext (ref child));
				
			} while (store.IterNext (ref iter));
			
			iter = iter_original;
			return false;
		}
		
		private void loadFromCollection ()
		{
			foreach (Module page in collection)
				Add (page);
		}
		
		private void collection_Added (object sender, 
			ModuleCollectionEventArgs args)
		{
			Add (args.Page);
		}
				
		private void collection_Removed (object sender, 
			ModuleCollectionEventArgs args)
		{
			
		}

		private void tree_Shown (object sender, EventArgs args)
		{
			view.ExpandAll ();
		}
		
		[GLib.ConnectBefore]
		private void view_ButtonPressEvent (object sender,
			ButtonPressEventArgs args)
		{
			TreeIter iter;
			TreePath path;
			
			if (args.Event.Type == Gdk.EventType.TwoButtonPress)
				if (view.GetPathAtPos (
					(int) args.Event.X,
					(int) args.Event.Y, 
					out path)){
					store.GetIterFromString (out iter, 
						path.ToString ());
					//int instance = (int) store.GetValue (
					//	iter, 2);
					
		// ERROR: must be fixed!!
					this.SelectionChanged (
						this,
						new ModuleCollectionEventArgs (null)
					);
				}
		}
		
		private void onSelectionChanged (object sender, 
			ModuleCollectionEventArgs args)
		{
		}
		
		public Gtk.TreeView TreeView {
			get { return view; }
		}
		
		public Gtk.TreeStore TreeStore {
			get { return store; }
		}
		
		public ModuleCollection Collection {
			get { return collection; }
		}
	}

}
