
using System;

namespace RickiLib.Types
{
	
	
	public class ModuleChooser : Gtk.Viewport
	{
		
		private event ModuleCollectionEvent _selection_changed;
		private event ModuleCollectionEvent _selection_activated;
		
		public ModuleChooser()
		{
			_selection_changed = onSelectionChanged;
			_selection_activated = onSelectionActivated;
		}
		
		protected virtual void OnSelectionChanged (Module module)
		{
			_selection_changed (this, new ModuleCollectionEventArgs (module));
		}
		
		protected virtual void OnSelectionActivated (Module module)
		{
			_selection_activated (this, new ModuleCollectionEventArgs (module));
		}
		
		
		private void onSelectionChanged (object sender, ModuleCollectionEventArgs args)
		{
		}
		
		private void onSelectionActivated (object sender, ModuleCollectionEventArgs args)
		{
		}
		
		public event ModuleCollectionEvent SelectionChanged {
			add { _selection_changed += value; }
			remove { _selection_changed -= value; }
		}
		
		public event ModuleCollectionEvent SelectionActivated {
			add { _selection_activated += value; }
			remove { _selection_activated -= value; }
		}
	}
}
