using System;
using Gtk;

using RickiLib.Types;

namespace RickiLib.Widgets 
{

	public class IconModuleChooser : ModuleChooser
	{
		private Gtk.Viewport _viewport;
		private Gtk.IconView iconView;
		private Gtk.ListStore listStore;
		private RickiLib.Types.ModuleCollection collection;
		
		private string currentPath;
		
		private Gtk.Label labelPath;

		private static Gdk.Pixbuf pixbuf_folder = 
			Gdk.Pixbuf.LoadFromResource ("folder.png").ScaleSimple (48, 48, Gdk.InterpType.Tiles);
		private static Gdk.Pixbuf pixbuf_module = 
			Gdk.Pixbuf.LoadFromResource ("module.png").ScaleSimple (48, 48, Gdk.InterpType.Tiles);

//		ScaleSimple (int dest_width, int dest_height, InterpType interp_type);
		
		public IconModuleChooser (RickiLib.Types.ModuleCollection col)
		{
			collection = col;
			currentPath = string.Empty;
			
			listStore = new ListStore (
				typeof (Gdk.Pixbuf), // icon
				typeof (string), // text
				typeof (bool), // is module?
				typeof (Module) // page instance
			);
			
			_viewport = new Viewport ();
			
			iconView = new IconView (listStore);
			iconView.KeyPressEvent += iconView_KeyPressEvent;
			iconView.ButtonPressEvent += iconView_ButtonPressEvent;
			iconView.PixbufColumn = 0;
			iconView.TextColumn = 1;
			
			labelPath = new Label (string.Empty);
			
			Gtk.VBox vbox = new VBox (false, 0);
			
			vbox.PackStart (labelPath, false, false, 0);
			vbox.PackStart (iconView);
			
			_viewport.Add (vbox);
			Add (_viewport);
			loadModules (string.Empty);
			collection.Added += collection_Added;
		}
		
		protected override void OnSelectionActivated (RickiLib.Types.Module module)
		{
			base.OnSelectionActivated (module);
		}
		 
		
		private void loadModules (string path)
		{		
			listStore.Clear ();
			
			foreach (Module page in collection) {
				processTabPage2 (path, page);
			}
		}
		
		private void processTabPage2 (string path, Module page)
		{
			Gdk.Pixbuf pixbuf = 
				page.Pixbuf == null?pixbuf_module:page.Pixbuf;

			if (path == page.Path) {
				listStore.AppendValues (
					pixbuf,
					page.Title,
					true,
					page);
			} else {
				if (page.Path.StartsWith (path)) {
					string foldername = 
						getFolderName (
							page.Path, path.Length);
					
					if (!Exists (foldername))
						listStore.AppendValues (
							pixbuf_folder, 
							foldername,
							false,
							null);
				}
			
			}
		}
		
		public void processTabPage (string path, Module page)
		{
			Gdk.Pixbuf pixbuf = 
				page.Pixbuf == null?pixbuf_module:page.Pixbuf;
				
			if (path.Length == 0 && page.Path.Length > 0) {
				string foldername = getFolderName (page.Path, 0);
				if (!Exists (foldername))
					listStore.AppendValues (
						pixbuf_folder,
						foldername);
			}
				
			if (path == page.Path) {
				listStore.AppendValues (
					pixbuf, 
					page.Title);
			} /*else {
				if (path.StartsWith (page.Path)) {
					Debug.WriteLine ("StartsWith");
					string folder = getFolderName (page.Path, path.Length -1);
					listStore.AppendValues (
						pixbuf_module,
						folder);
				}
			}*/
		}
		
		public void ActivateSelection (Gtk.TreeIter iter)
		{
			bool ispage = (bool) listStore.GetValue (iter, 2);
					
			if (ispage) {
				Module page = (Module) listStore.GetValue (iter, 3);
				OnSelectionActivated (page);
			} else {
				string sep = string.Empty;
				currentPath += sep + listStore.GetValue (iter, 1).ToString ();
				loadModules (currentPath);
			}

		}
		
		private string getFolderName (string path, int from)
		{		
			if (from < 0)
				from = 0;
			
			int pos = path.IndexOf ("/", from);
			
			//Debug.WriteLine ("Path : \"{0}\"", path);
			//Debug.WriteLine ("\tfrom : {0}", from);
			//Debug.WriteLine ("\tpos : {0}", pos);
			
			
			
			
			if (pos > -1) {
				if (pos < path.Length) {
					pos ++;
					//Debug.WriteLine ("returning : \"{0}\"", path.Substring (from, pos - from));
					return path.Substring (from, pos - from);
				}
			}
			return path.Substring (from);
		}
		
		private bool Exists (string path)
		{
			TreeIter iter;
			
			if (listStore.GetIterFirst (out iter))
				do {
					string p = (string) listStore.GetValue (iter, 1);
					if (p == path)
						return true;
				} while (listStore.IterNext (ref iter));
			
			return false;
		}
		
		private void collection_Added (object sender, 
			ModuleCollectionEventArgs args)
		{
			//loadModules (currentPath);
			processTabPage2 (currentPath, args.Page);
		}
		
		
		[GLib.ConnectBefore]
		private void iconView_KeyPressEvent (object sender,
			KeyPressEventArgs args)
		{
			if (args.Event.Key == Gdk.Key.Return ||
				args.Event.Key == Gdk.Key.KP_Enter) {
				
				Gtk.TreeIter iter;
				
				if (GetIterSelected (out iter))
					ActivateSelection (iter);
			}
		}
		
		private bool GetIterSelected (out Gtk.TreeIter iter)
		{
			iter = TreeIter.Zero;
			
			if (iconView.SelectedItems.Length > 0) {
				TreePath tpath = iconView.SelectedItems [0];
				//
				
				listStore.GetIterFromString (
					out iter, 
					tpath.ToString ());
				
				return true;
			}
			
			return false;	
		
		}
		
		[GLib.ConnectBefore]
		private void iconView_ButtonPressEvent (object sender,
			ButtonPressEventArgs args)
		{
			if (args.Event.Button == 1)
				if (args.Event.Type == Gdk.EventType.TwoButtonPress) {
					if (iconView.SelectedItems.Length > 0) {
						TreePath tpath = iconView.SelectedItems [0];
						TreeIter iter;
						listStore.GetIterFromString (
							out iter, 
							tpath.ToString ());
				
						ActivateSelection (iter);
					}
				}
		}
		
		public ModuleCollection Collection {
			get { return collection; }
		}
	}
}

