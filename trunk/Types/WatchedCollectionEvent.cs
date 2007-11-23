// WatchedCollectionEvent.cs created with MonoDevelop
// User: ricki at 06:42 p 19/10/2007
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace RickiLib.Types
{
	
	public delegate void WatchedCollectionEventHandler<U> (object sender,
		WatchedCollectionEventArgs<U> args);
	
	public class WatchedCollectionEventArgs <T>
	{
		private T t;
		
		public WatchedCollectionEventArgs (T t)
		{
			this.t  = t;
		}
		
		public T Instance {
			get { return t; }
		}
	}
}
