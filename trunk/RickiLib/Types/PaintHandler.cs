
using System;
using System.Drawing;

namespace RickiLib.Types
{
	public delegate void PaintHandler (object sender, PaintArgs args);
	
	public class PaintArgs : System.EventArgs {
		
		private System.Drawing.Graphics graphics;
		
		public PaintArgs (System.Drawing.Graphics graphics)
		{
			this.graphics = graphics;
		}
		
		public System.Drawing.Graphics Graphics {
			get {
				return graphics;
			}
		}
	}
	
}
