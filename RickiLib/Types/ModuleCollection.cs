
using System;
using RickiLib.Widgets;

namespace RickiLib.Types
{
	
	
	public class ModuleCollection : System.Collections.Generic.List <Module>
	{
		public event ModuleCollectionEvent Added;
		public event ModuleCollectionEvent Removed;
		
		public ModuleCollection ()
		{
			Added = onAdded;
			Removed = onRemoved;
		}
		
		public new void Add (Module page)
		{
			base.Add (page);
			Added (this, new ModuleCollectionEventArgs (page));
		}
		
		public new void Remove (Module page)
		{
			base.Remove (page);
			Removed (this, new ModuleCollectionEventArgs (page));
		}
		
		protected virtual void OnAdded (Module page)
		{
		}
		
		protected virtual void OnRemoved (Module page)
		{
		}
		
		private void onAdded (object sender, 
			ModuleCollectionEventArgs args)
		{
			OnAdded (args.Page);
		}
		
		private void onRemoved (object sender, 
			ModuleCollectionEventArgs args)
		{
			OnRemoved (args.Page);
		}
	}
}
