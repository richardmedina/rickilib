
using System;

namespace RickiLib.Types
{
	
	public delegate void CloseEventHandler (object sender, 
		CloseEventArgs args);
	
	public class CloseEventArgs : System.EventArgs
	{
		private bool doClose;
		
		public CloseEventArgs (bool doClose)
		{
			this.doClose = doClose;
		}
		
		public bool DoClose {
			get {
				return doClose;
			}
		}
	}
}
