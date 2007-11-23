
using System;
using RickiLib.Widgets;

namespace RickiLib.Types
{
	
	public delegate void ModuleCollectionEvent (object sender, 
		ModuleCollectionEventArgs args);
		
	public class ModuleCollectionEventArgs
	{
		private Module page;
		
		public ModuleCollectionEventArgs (Module page)
		{
			this.page = page;
		}
		
		public Module Page {
			get {
				return page;
			}
		}
	}
}
