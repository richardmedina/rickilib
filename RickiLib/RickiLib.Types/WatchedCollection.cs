// WatchedCollection.cs created with MonoDevelop
// User: ricki at 06:23 p 19/10/2007
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;

namespace RickiLib.Types
{
	
	
	public class WatchedCollection <T> : System.Collections.Generic.List <T>
	{
		private event WatchedCollectionEventHandler<T> added;
		private event WatchedCollectionEventHandler<T> removed;
		
		private T elementToFind;
		
		private string name;
		
		
		public WatchedCollection () : this (String.Empty)
		{
		}
		
		public WatchedCollection (string name)
		{
			added = onAdded;
			removed = onRemoved;
			this.name = name;
		}
		
		public new void Add (T t)
		{
			if (OnAddRequest (t)) {
				base.Add (t);
				OnAdded (t);
			}
		}
		
		public new void Remove (T t)
		{
			if (OnRemoveRequest (t)) {
				base.Remove (t);
				OnRemoved (t);
			}
		}
		
		protected virtual bool OnAddRequest (T instance)
		{
			return true;
		}
		
		private bool finderPredicate (T match)
		{
			return elementToFind.Equals (match);
		}
		
		public bool Exists (T instance)
		{
			System.Threading.Mutex mutex = new System.Threading.Mutex ();
			
			mutex.WaitOne ();
			elementToFind = instance;
			bool exists = base.Exists (finderPredicate);
			mutex.ReleaseMutex ();
			
			return exists;
		}
				
		protected virtual void OnAdded (T instance)
		{
			this.added (this, 
				new WatchedCollectionEventArgs<T> (instance));
		}
		
		protected virtual bool OnRemoveRequest (T instance)
		{
			return true;
		}
		
		protected virtual void OnRemoved (T instance)
		{
			this.removed (this, 
				new WatchedCollectionEventArgs<T> (instance));
		}
		
		private void onAdded (object sender, WatchedCollectionEventArgs <T> args)
		{
		}
		
		private void onRemoved (object sender, WatchedCollectionEventArgs <T> args)
		{
		}
		
		public event WatchedCollectionEventHandler<T> Added {
			add { added += value; }
			remove { added -= value; }
		}
		
		public event WatchedCollectionEventHandler<T> Removed {
			add { removed += value; }
			remove { removed -= value; }
		}
		
		public string Name {
			get { return name; }
			set { name = value; }
		}
	}
}
