
using System;
using System.Collections.Generic;
using Gtk;
using RickiLib.Types;

namespace RickiLib.Widgets
{
	
	
	public class NotebookModuleChooser : Gtk.Notebook
	{
		private ModuleCollection collection;
		
		public NotebookModuleChooser (ModuleCollection tabcol)
		{	
			base.Scrollable = true;
			base.EnablePopup = true;
			
			collection = tabcol;
			collection.Added += collection_Added;
			
			loadPagesFromCollection ();
		}
		
		public void Add (Module page)
		{
			page.Position = base.AppendPage (
				page,
				new Label (page.Title)
			);
			
			page.ShowAll ();
		}
		
		private void loadPagesFromCollection ()
		{
			foreach (Module page in collection)
				Add (page);
		}
		/* Is not used?
		private new void SelectPage (int instance)
		{
			foreach (Module page in collection) {
				if (page.Instance == instance)
					base.Page = page.Position;
			}
		}
		*/
		private void collection_Added (object sender,
			ModuleCollectionEventArgs args)
		{
			Add (args.Page);
		}
		
		public ModuleCollection Collection {
			get {
				return collection;
			}
		}
	}
}
